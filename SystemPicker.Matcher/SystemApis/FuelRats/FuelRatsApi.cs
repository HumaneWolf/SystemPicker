using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;

namespace SystemPicker.Matcher.SystemApis.FuelRats
{
    public class FuelRatsApi : ISystemApi
    {
        private readonly HttpClient _client;

        public FuelRatsApi(HttpClient client)
        {
            _client = client;
        }

        public async Task<SystemMatch> GetKnownMatch(string systemName)
        {
            var url = $"https://system.api.fuelrats.com/search?type=fulltext&name={HttpUtility.HtmlEncode(systemName)}";
            var data = await _client.GetFromJsonAsync<FuelRatsResponse>(url);

            var system = data?.Data.FirstOrDefault();
            if (system != null && system.Name.ToLower() == systemName.ToLower())
            {
                return new SystemMatch(system.Name, system.Id64);
            }
            return null;
        }
    }
}