using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SystemPicker.Matcher;
using SystemPicker.Matcher.Models;
using SystemPicker.Matcher.SystemApis;
using Xunit;

namespace SystemPicker.Tests.Matcher
{
    public class TextMatcherTests
    {
        private Task<List<SystemMatch>> ScanText(string text)
        {
            var matcher = new TextMatcher(new RandomSystemApi(new HttpClient()));
            return matcher.FindSystemMatches(text);
        }
        
        [Fact]
        public async Task String_is_system_name_procgen_1()
        {
            var result = await ScanText("wregoe aa-A b14-1");
            Assert.Single(result);
            
            var match = result.First();
            Assert.Equal(2882057413753, match.Id64);
            Assert.Equal("Wregoe AA-A b14-1", match.Name);
        }
        
        [Fact]
        public async Task String_is_system_name_procgen_2()
        {
            var result = await ScanText("Praea Euq JL-J c23-3");
            Assert.Single(result);
            
            var match = result.First();
            Assert.Equal(908620731338, match.Id64);
            Assert.Equal("Praea Euq JL-J c23-3", match.Name);
        }
        
        [Fact]
        public async Task String_is_system_name_procgen_3()
        {
            var result = await ScanText("Blu Thua IH-B b1-0");
            Assert.Single(result);
            
            var match = result.First();
            Assert.Equal(672834529289, match.Id64);
            Assert.Equal("Blu Thua IH-B b1-0", match.Name);
        }
        
        [Fact]
        public async Task String_is_system_name_procgen_4()
        {
            var result = await ScanText("Blu Thua NE-G c11-3");
            Assert.Single(result);
            
            var match = result.First();
            Assert.Equal(908754916450, match.Id64);
            Assert.Equal("Blu Thua NE-G c11-3", match.Name);
        }
        
        [Fact]
        public async Task String_is_system_name_catalog()
        {
            var result = await ScanText("hip 100002");
            Assert.Single(result);
            
            var match = result.First();
            Assert.Equal(44786747779, match.Id64);
            Assert.Equal("HIP 100002", match.Name);
        }
        
        [Fact(Skip = "Not yet implemented.")]
        public async Task String_is_system_name_named()
        {
            var result = await ScanText("sol");
            Assert.Single(result);
            
            var match = result.First();
            Assert.Equal(10477373803, match.Id64);
            Assert.Equal("Sol", match.Name);
        }

        [Fact]
        public async Task Extract_from_comment_1()
        {
            // https://www.reddit.com/r/EliteDangerous/comments/l5z277/sorcery_14_stars_and_3_black_holes_in_1_system/gkxb3df/
            const string text = "I may wear glasses but i can clearly tell it says hip 63835";
            var result = await ScanText(text);
            Assert.Single(result);

            var match = result.First();
            Assert.Equal(27754647, match.Id64);
            Assert.Equal("HIP 63835", match.Name);
        }
        
        [Fact]
        public async Task Extract_from_comment_2()
        {
            // https://www.reddit.com/r/EliteDangerous/comments/l5z277/sorcery_14_stars_and_3_black_holes_in_1_system/gkx9py7/
            const string text = "Title means \"first BH\" as in Black Hole. brain fart.HIP 63835";
            var result = await ScanText(text);
            Assert.Single(result);

            var match = result.First();
            Assert.Equal(27754647, match.Id64);
            Assert.Equal("HIP 63835", match.Name);
        }
        
        [Fact]
        public async Task Extract_from_comment_3()
        {
            // https://www.reddit.com/r/EliteDangerous/comments/l6e1jf/im_lucky_i_didnt_blow_up/gl055oz/
            var result = await ScanText(TestPosts.TestComment3);
            Assert.Empty(result);
        }
        
        [Fact]
        public async Task Extract_from_comment_4()
        {
            // https://www.reddit.com/r/EliteDangerous/comments/l6e1jf/im_lucky_i_didnt_blow_up/gl00jcp/
            var result = await ScanText(TestPosts.TestComment4);
            Assert.Empty(result);
        }
        
        [Fact]
        public async Task Extract_from_comment_5()
        {
            var result = await ScanText("Something is going on in Synoagoae IO-O d7-0");
            Assert.Single(result);
            
            var match = result.First();
            Assert.Equal(14033852867, match.Id64);
            Assert.Equal("Synoagoae IO-O d7-0", match.Name);
        }
        
        [Fact(Skip = "Need to rethink how we handle sector/region names.")]
        public async Task Extract_from_comment_6()
        {
            var result = await ScanText("Something dark is happening over in Coalsack Dark Region AA-Q b5-5");
            Assert.Single(result);
            
            var match = result.First();
            Assert.Equal(11672781465113, match.Id64);
            Assert.Equal("Coalsack Dark Region AA-Q b5-5", match.Name);
        }
        
        [Fact]
        public async Task Extract_from_post_1()
        {
            // https://www.reddit.com/r/EliteDangerous/comments/l67wpu/flying_to_sag_a_without_a_fuel_scoop_day_2/
            var result = await ScanText(TestPosts.Post1);
            Assert.Equal(3, result.Count);

            Assert.Contains(result, m => m.Id64 == 908620731338 && m.Name == "Praea Euq JL-J c23-3");
            Assert.Contains(result, m => m.Id64 == 672834529289 && m.Name == "Blu Thua IH-B b1-0");
            Assert.Contains(result, m => m.Id64 == 908754916450 && m.Name == "Blu Thua NE-G c11-3");
        }
        
        [Fact(Skip = "Not yet implemented.")]
        public async Task Extract_from_post_2()
        {
            // https://www.reddit.com/r/EliteDangerous/comments/l5tqcj/flying_to_sag_a_without_a_fuel_scoop_day_1/
            var result = await ScanText(TestPosts.Post2);
            Assert.Equal(2, result.Count);

            Assert.Contains(result, m => m.Id64 == 358864950034 && m.Name == "Quince");
            Assert.Contains(result, m => m.Id64 == 908620731338 && m.Name == "Praea Euq JL-J c23-3");
        }
        
        [Fact(Skip = "Need to rethink how we handle sector/region names.")]
        public void ProcGen_Potential_Confusion_test_1()
        {
            var textMatcher = new TextMatcher(new RandomSystemApi(new HttpClient()));
            var result = textMatcher.FindProcGenSystemCandidates("Coalsack Dark Region AA-Q b5-5");
            Assert.Single(result);
            
            var match = result.First();
            Assert.Equal("Coalsack Dark Region AA-Q b5-5", match);
        }
        
        [Fact(Skip = "Not ready yet")]
        public void ProcGen_Potential_Confusion_test_2()
        {
            var textMatcher = new TextMatcher(new RandomSystemApi(new HttpClient()));
            var result = textMatcher.FindProcGenSystemCandidates("North America Sector AA-Q b5-0 and America Sector AA-Q b5-0");
            Assert.Equal(2, result.Count);
            
            var match = result.First();
            Assert.Equal("North America Sector AA-Q b5-0", match);
            match = result.Last();
            Assert.Equal("America Sector AA-Q b5-0", match);
        }
    }
}