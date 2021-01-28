using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using SystemPicker.Matcher.SystemApis.EDSM;
using SystemPicker.Matcher.SystemApis.FuelRats;
using SystemPicker.Matcher.SystemApis.Spansh;

namespace SystemPicker.Matcher.SystemApis
{
    public class RandomSystemApi : ISystemApi
    {
        private readonly HttpClient _client;

        public RandomSystemApi(HttpClient client)
        {
            _client = client;
        }

        private ISystemApi GetRandomApi()
        {
            var num = RandomNumberGenerator.GetInt32(3);
            switch (num)
            {
                case 0: return new EDSMApi(_client);
                case 1: return new FuelRatsApi(_client);
                case 2: return new SpanshApi(_client);
                
                // Should never be returned, but here so it doesn't cause random problems later.
                default:
                    Console.Error.WriteLine($"Returned default random api, num {num}.");
                    return new EDSMApi(_client);
            }
        }
        
        public Task<SystemMatch> GetKnownMatch(string systemName)
        {
            return GetRandomApi().GetKnownMatch(systemName);
        }
    }
}