using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace SystemPicker.Matcher.Storage
{
    public class NamedSectorStorage
    {
        private readonly IDatabase _redis;
        private readonly string _namedSectorsHash;

        public NamedSectorStorage(IDatabase redisDatabase)
        {
            _redis = redisDatabase;
            _namedSectorsHash = "named-sectors-1";
        }

        public async Task AddSector(string sectorName)
        {
            await _redis.HashSetAsync(_namedSectorsHash, sectorName.ToLower(), "1");
        }

        public async Task<IEnumerable<string>> GetAllSectors()
        {
            return (await _redis.HashKeysAsync(_namedSectorsHash)).Select(m => m.ToString());
        }
    }
}