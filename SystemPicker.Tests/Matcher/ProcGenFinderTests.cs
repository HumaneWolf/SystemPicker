using SystemPicker.Matcher;
using SystemPicker.Matcher.Finders;
using Xunit;

namespace SystemPicker.Tests.Matcher
{
    public class ProcGenFinderTests
    {
        private bool IsProcGen(string text)
        {
            return ProcGenFinder.IsProcGen(text);
        }
        
        [Fact]
        public void Is_proc_gen_1()
        {
            var result = IsProcGen("wregoe aa-A b14-1");
            Assert.True(result);
        }
        
        [Fact]
        public void Is_proc_gen_2()
        {
            var result = IsProcGen("Synoagoae IO-O d7-0");
            Assert.True(result);
        }
        
        [Fact]
        public void Is_proc_gen_3()
        {
            var result = IsProcGen("Ceeckaea AA-O b40-0");
            Assert.True(result);
        }
        
        [Fact]
        public void Is_not_proc_gen_1()
        {
            var result = IsProcGen("Sol");
            Assert.False(result);
        }
        
        [Fact]
        public void Is_not_proc_gen_2()
        {
            var result = IsProcGen("Colonia");
            Assert.False(result);
        }
        
        [Fact]
        public void Is_not_proc_gen_3()
        {
            var result = IsProcGen("HIP 100002");
            Assert.False(result);
        }
        
        [Fact]
        public void Is_not_proc_gen_4()
        {
            var result = IsProcGen("HD 100015");
            Assert.False(result);
        }
    }
}