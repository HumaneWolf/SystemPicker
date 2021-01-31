using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace SystemPicker.Matcher.Storage
{
    public class NamedSectorStorage
    {
        private readonly IDatabase _redis;
        private readonly string _namedSectorsSet;

        public NamedSectorStorage(IDatabase redisDatabase)
        {
            _redis = redisDatabase;
            _namedSectorsSet = "named-sectors-1";
        }

        public async Task AddSector(string sectorName)
        {
            await _redis.SetAddAsync(_namedSectorsSet, sectorName.ToLower());
        }

        public async Task<IEnumerable<string>> GetAllSectors()
        {
            return (await _redis.SetMembersAsync(_namedSectorsSet)).Select(m => m.ToString());
        }
    }
}