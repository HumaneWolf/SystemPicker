using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using SystemPicker.Matcher.Models;
using SystemPicker.Matcher.Utils;

namespace SystemPicker.Matcher.SystemApis.EDSM
{
    public class EDSMApi : ISystemApi
    {
        private readonly HttpClient _client;

        public EDSMApi(HttpClient client)
        {
            _client = client;
        }

        public async Task<SystemMatch> GetKnownMatch(string systemName)
        {
            var url = $"https://www.edsm.net/api-v1/system?showId=1&systemName={HttpUtility.HtmlEncode(systemName)}";
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (content.Trim() != "[]")
            {
                var data = ConfiguredJson.Deserialize<EDSMResponse>(content);
                return new SystemMatch(data.Name, data.Id64);
            }

            return null;
        }
    }
}