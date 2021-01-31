using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemPicker.Matcher.Models;
using SystemPicker.Matcher.SystemApis;

namespace SystemPicker.Matcher
{
    public class TextMatcher
    {
        private readonly ISystemApi _systemApi;

        public TextMatcher(ISystemApi systemApi)
        {
            _systemApi = systemApi;
        }

        public async Task<List<SystemMatch>> FindSystemMatches(string text)
        {
            var candidates = FindProcGenSystemCandidates(text);
            candidates.AddRange(FindCatalogSystemCandidates(text));
            candidates.AddRange(FindNamedSystemCandidates(text));
            candidates.AddRange(FindNamedSectorCandidates(text));

            // duplicate prevention.
            var matches = new Dictionary<string, SystemMatch>();
            foreach (var candidate in candidates)
            {
                if (!matches.ContainsKey(candidate.ToLower()))
                {
                    var match = await _systemApi.GetKnownMatch(candidate);
                    if (match != null)
                    {
                        matches.Add(candidate.ToLower(), match);
                    }                    
                }
            }

            return matches.Values.ToList();
        }

        public List<string> FindNamedSystemCandidates(string text)
        {
            // todo
            return new ();
        }
        
        public List<string> FindNamedSectorCandidates(string text)
        {
            // todo
            return new ();
        }

        public List<string> FindCatalogSystemCandidates(string text)
        {
            return CatalogFinder.CatalogRegex.Matches(text).Select(x => x.Value).ToList();
        }
        
        public List<string> FindProcGenSystemCandidates(string text)
        {
            return ProcGenFinder.ProcGenRegex.Matches(text).Select(x => x.Value).ToList();
        }
    }
}