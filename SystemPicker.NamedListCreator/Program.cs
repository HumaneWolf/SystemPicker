using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using SystemPicker.Matcher;
using SystemPicker.Matcher.Models;
using SystemPicker.Matcher.Storage;
using CsvHelper;
using StackExchange.Redis;

namespace SystemPicker.NamedListCreator
{
    static class Program
    {
        private static ConnectionMultiplexer _redisMultiplexer;

        // Locked
        private static string _lock = "locked";
        private static CsvReader _csvReader;
        // End locked
        
        static async Task Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("EDDB systems.csv file is required.");
                return;
            }

            lock (_lock)// Strictly not needed, but helps satisfy basic analysis. 
            {
                var streamReader = File.OpenText(args[0]);
                _csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

                _redisMultiplexer = ConnectionMultiplexer.Connect("localhost");
                
                _csvReader.Read();
                _csvReader.ReadHeader();                
            }

            // Run 4 workers.
            Console.WriteLine($"Starting workers at {DateTime.UtcNow.ToShortTimeString()}.");
            var tasks = new List<Task>();
            for (var i = 0; i < 4; i++)
            {
                var localWorkerNumber = i; // Prevents us from modifying the number in the outer scope before Worker executes, causing all to be given the same.
                tasks.Add(Task.Run(() => Worker(localWorkerNumber)));
            }
            
            await Task.WhenAll(tasks);
            
            Console.WriteLine($"Done at {DateTime.UtcNow.ToShortTimeString()}.");
        }

        private static async Task Worker(int workerNumber)
        {
            var batchNumber = 0L;
            while (true)
            {
                var batch = new List<CsvSystem>();

                // Get our batch.
                lock (_lock)
                {
                    while (_csvReader.Read() && batch.Count < 100_000)
                    {
                        var data = _csvReader.GetRecord<CsvSystem>();
                        batch.Add(data);
                    }
                    Console.WriteLine($"Got batch {workerNumber}-{batchNumber} of size {batch.Count}.");
                }

                // If no batch available, we're done.
                if (batch.Count == 0)
                {
                    return;
                }

                // Process it.
                foreach (var system in batch)
                {
                    await CheckIfValid(system);
                }
                
                // done
                Console.WriteLine($"Processed batch {workerNumber}-{batchNumber} of size {batch.Count}.");
                batch.Clear();
                batchNumber++;
            }
        }

        private static async Task AddNamedSystems(SystemMatch system)
        {
            var redisDb = _redisMultiplexer.GetDatabase();
            var storage = new NamedSystemStorage(redisDb);
            await storage.AddSystem(system.Name);
        }

        private static async Task CheckIfValid(CsvSystem system)
        {
            if (system.EDSystemAddress != null && !CatalogFinder.IsCatalogSystem(system.Name) && !ProcGenFinder.IsProcGen(system.Name))
            {
                await AddNamedSystems(new SystemMatch(system.Name, system.EDSystemAddress ?? 0));                
            }
        }
    }
}