using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SystemPicker.Matcher.Utils;

namespace SystemPicker.Matcher.SystemApis.Spansh
{
    public class SpanshApi : ISystemApi
    {
        private readonly HttpClient _client;

        public SpanshApi(HttpClient client)
        {
            _client = client;
        }
        
        public async Task<SystemMatch> GetKnownMatch(string systemName)
        {
            var payload = ConfiguredJson.Serialize(new SpanshRequest(systemName));
            var url = "https://spansh.co.uk/api/systems/search";
            var response = await _client.PostAsync(url, new StringContent(payload));
            var data = ConfiguredJson.Deserialize<SpanshResponse>(await response.Content.ReadAsStringAsync());

            var system = data.Results.FirstOrDefault(x => x.Name.ToLower() == systemName.ToLower());
            if (system != null)
            {
                return new SystemMatch(system.Name, system.Id64);
            }
            return null;
        }
    }
}