using System.Net.Http;
using System.Threading.Tasks;
using SystemPicker.Matcher.SystemApis.Spansh;
using Xunit;

namespace SystemPicker.Tests.Matcher.SystemApis
{
    public class SpanshTests
    {
        [Fact]
        public async Task Sol_Exists()
        {
            var client = new HttpClient();
            var api = new SpanshApi(client);
            var match = await api.GetKnownMatch("Sol");
            
            Assert.NotNull(match);
            Assert.Equal("Sol", match.Name);
            Assert.Equal(10477373803, match.Id64);
        }
        
        [Fact]
        public async Task Colonia_Exists()
        {
            var client = new HttpClient();
            var api = new SpanshApi(client);
            var match = await api.GetKnownMatch("cOLonia");
            
            Assert.NotNull(match);
            Assert.Equal("Colonia", match.Name);
            Assert.Equal(3238296097059, match.Id64);
        }
        
        [Fact]
        public async Task Col_does_not_exist()
        {
            var client = new HttpClient();
            var api = new SpanshApi(client);
            var match = await api.GetKnownMatch("Col");

            Assert.Null(match);
        }
        
        [Fact]
        public async Task Sol2_does_not_exist()
        {
            var client = new HttpClient();
            var api = new SpanshApi(client);
            var match = await api.GetKnownMatch("Sol2");

            Assert.Null(match);
        }
    }
}