using SystemPicker.Matcher;
using Xunit;

namespace SystemPicker.Tests.Matcher
{
    public class CatalogFinderTests
    {
        private bool IsCatalogSystem(string text)
        {
            var matcher = new CatalogFinder();
            return matcher.IsCatalogSystem(text);
        }
        
        [Fact]
        public void Is_catalog_1()
        {
            var result = IsCatalogSystem("HIP 1000");
            Assert.True(result);
        }
        
        [Fact]
        public void Is_catalog_2()
        {
            var result = IsCatalogSystem("Gliese 105.2");
            Assert.True(result);
        }
        
        [Fact]
        public void Is_catalog_3()
        {
            var result = IsCatalogSystem("Gliese 110");
            Assert.True(result);
        }
        
        [Fact]
        public void Is_catalog_4()
        {
            var result = IsCatalogSystem("HD 100015");
            Assert.True(result);
        }
        
        [Fact]
        public void Is_not_catalog_1()
        {
            var result = IsCatalogSystem("Ceeckaea AA-O b40-0");
            Assert.False(result);
        }
        
        [Fact]
        public void Is_not_catalog_2()
        {
            var result = IsCatalogSystem("Colonia");
            Assert.False(result);
        }
    }
}