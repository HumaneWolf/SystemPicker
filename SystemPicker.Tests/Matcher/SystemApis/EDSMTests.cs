using System.Net.Http;
using System.Threading.Tasks;
using SystemPicker.Matcher.SystemApis.EDSM;
using Xunit;

namespace SystemPicker.Tests.Matcher.SystemApis
{
    public class EDSMTests
    {
        [Fact]
        public async Task Sol_Exists()
        {
            var client = new HttpClient();
            var edsm = new EDSMApi(client);
            var match = await edsm.GetKnownMatch("Sol");
            
            Assert.Equal("Sol", match.Name);
            Assert.Equal(10477373803, match.Id64);
        }
        
        [Fact]
        public async Task Colonia_Exists()
        {
            var client = new HttpClient();
            var edsm = new EDSMApi(client);
            var match = await edsm.GetKnownMatch("cOLonia");
            
            Assert.Equal("Colonia", match.Name);
            Assert.Equal(3238296097059, match.Id64);
        }
        
        [Fact]
        public async Task Col_does_not_exist()
        {
            var client = new HttpClient();
            var edsm = new EDSMApi(client);
            var match = await edsm.GetKnownMatch("Col");

            Assert.Null(match);
        }
        
        [Fact]
        public async Task Sol2_does_not_exist()
        {
            var client = new HttpClient();
            var edsm = new EDSMApi(client);
            var match = await edsm.GetKnownMatch("Sol2");

            Assert.Null(match);
        }
    }
}