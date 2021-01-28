using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SystemPicker.Matcher
{
    public class TextMatcher
    {
        private readonly HttpClient _client;

        public TextMatcher(HttpClient client)
        {
            _client = client;
        }

        public async Task<SystemMatch> FindSystemMatches(string text)
        {
            throw new NotImplementedException();
        }

        public List<string> FindNamedSystemCandidates(string text)
        {
            throw new NotImplementedException();
        }
        
        public List<string> FindProcGenSystemCandidates(string text)
        {
            throw new NotImplementedException();
        }
    }
}