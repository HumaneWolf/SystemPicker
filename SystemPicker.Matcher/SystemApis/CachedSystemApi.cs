using System;
using System.Net.Http;
using System.Threading.Tasks;
using SystemPicker.Matcher.Models;
using SystemPicker.Matcher.Utils;
using StackExchange.Redis;

namespace SystemPicker.Matcher.SystemApis
{
    public class CachedSystemApi : ISystemApi
    {
        private readonly IDatabase _redis;
        private readonly ISystemApi _systemApi;

        private readonly string _namePrefix = "cached-match";

        public CachedSystemApi(HttpClient client, IDatabase redis)
        {
            _redis = redis;
            _systemApi = new RandomSystemApi(client);
        }

        public async Task<SystemMatch> GetMatchOrNull(string name)
        {
            var raw = await _redis.StringGetAsync($"{_namePrefix}:{name}");
            return string.IsNullOrEmpty(raw) ? null : ConfiguredJson.Deserialize<SystemMatch>(raw);
        }

        public Task SaveMatch(string name, SystemMatch match)
        {
            var raw = ConfiguredJson.Serialize(match);
            return _redis.StringSetAsync($"{_namePrefix}:{name}", raw, expiry: TimeSpan.FromDays(7));
        }

        public async Task<SystemMatch> GetKnownMatch(string systemName)
        {
            var match = await GetMatchOrNull(systemName);
            if (match == null)
            {
                match = await _systemApi.GetKnownMatch(systemName);
                if (match != null) await SaveMatch(systemName, match);
            }
            return match;
        }
    }
}