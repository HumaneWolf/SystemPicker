using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SystemPicker.Matcher;
using SystemPicker.Matcher.Models;
using CsvHelper;

namespace SystemPicker.NamedListCreator
{
    static class Program
    {
        private static StreamWriter _output;
        private const string Lock = "namedSystemsLock";

        private static CatalogFinder Catalog = new();
        private static ProcGenFinder ProcGen = new();
        

        static async Task Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("EDDB systems.csv file and output file path is required.");
                return;
            }

            using var streamReader = File.OpenText(args[0]);
            using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

            lock (Lock) // Lock is strictly not necessary here, mainly there to satisfy basic code analysis.
            {
                _output = File.CreateText(args[1]);
            }

            var tasks = new List<Task>();
            csvReader.Read();
            csvReader.ReadHeader();
            while (csvReader.Read())
            {
                var data = csvReader.GetRecord<CsvSystem>();
                tasks.Add(Task.Run(() => CheckIfValid(data)));

                if (tasks.Count % 1_000_000 == 0)
                {
                    Console.WriteLine($"Processed {tasks.Count} systems.");
                    lock (Lock) // Lock is strictly not necessary here, mainly there to satisfy basic code analysis.
                    {
                        _output.Flush();
                        _output.BaseStream.Flush();
                    }
                }
            }

            await Task.WhenAll(tasks);
            
            lock (Lock)
            {
                _output.Close();
            }

            Console.WriteLine("Done.");
        }

        private static void AddNamedSystem(SystemMatch system)
        {
            // Console.WriteLine($"Named system: {system.Name}.");
            lock (Lock)
            {
                _output.WriteLine(system.Name);
            }
        }

        private static Task CheckIfValid(CsvSystem system)
        {
            if (system.EDSystemAddress != null && !Catalog.IsCatalogSystem(system.Name) && !ProcGen.IsProcGen(system.Name))
            {
                AddNamedSystem(new SystemMatch(system.Name, system.EDSystemAddress ?? 0));
            }

            return Task.CompletedTask;
        }
    }
}