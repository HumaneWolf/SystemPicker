using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace SystemPicker.Matcher.Storage
{
    public class NamedSystemStorage
    {
        private readonly IDatabase _redis;
        private readonly string _namedSystemsSet;

        public NamedSystemStorage(IDatabase redisDatabase)
        {
            _redis = redisDatabase;
            _namedSystemsSet = "named-systems-1";
        }

        public async Task AddSystem(string systemName)
        {
            await _redis.SetAddAsync(_namedSystemsSet, systemName.ToLower());
        }

        public async Task<IEnumerable<string>> GetAllSystems()
        {
            return (await _redis.SetMembersAsync(_namedSystemsSet)).Select(m => m.ToString());
        }
    }
}
