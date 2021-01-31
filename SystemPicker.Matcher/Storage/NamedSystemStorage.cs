using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace SystemPicker.Matcher.Storage
{
    public class NamedSystemStorage
    {
        private readonly IDatabase _redis;
        private readonly string _namedSystemsHash;

        public NamedSystemStorage(IDatabase redisDatabase)
        {
            _redis = redisDatabase;
            _namedSystemsHash = "named-systems-1";
        }

        public async Task AddSystem(string systemName)
        {
            await _redis.HashSetAsync(_namedSystemsHash, systemName.ToLower(), "1");
        }

        public async Task<IEnumerable<string>> GetAllSystems()
        {
            return (await _redis.HashKeysAsync(_namedSystemsHash)).Select(m => m.ToString());
        }
    }
}
