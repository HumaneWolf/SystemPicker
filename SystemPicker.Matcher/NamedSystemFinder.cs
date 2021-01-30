using System.Threading.Tasks;
using StackExchange.Redis;

namespace SystemPicker.Matcher
{
    public class NamedSystemFinder
    {
        private readonly IDatabase _redis;
        private readonly string _prefix;

        public NamedSystemFinder(IDatabase redisDatabase)
        {
            _redis = redisDatabase;
            _prefix = "named-system";
        }

        public Task AddSystem(string systemName)
        {
           return _redis.StringSetAsync($"{_prefix}:{systemName.ToLower()}", "1");
        }

        public async Task DeleteSystem(string systemName)
        {
            await _redis.KeyDeleteAsync($"{_prefix}:{systemName.ToLower()}");
        }

        public async Task<bool> IsNamed(string systemName)
        {
            var val = await _redis.StringGetAsync($"{_prefix}:{systemName.ToLower()}");
            return !string.IsNullOrEmpty(val);
        }
    }
}