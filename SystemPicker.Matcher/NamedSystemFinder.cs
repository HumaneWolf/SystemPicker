using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SystemPicker.Matcher
{
    public static class NamedSystemFinder
    {
        private static List<string> _systemNames = new(); // Never allow it to be null.

        private static readonly Regex WordRegex =
            new Regex(@"^[a-zA-Z0-9]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static void SetSystems(IEnumerable<string> systems)
        {
            _systemNames = systems.ToList();
        }
        
        public static List<string> FindPossibleSystems(string text)
        {
            // Find all words in text.
            var lowerText = text.ToLower();
            var words = WordRegex.Matches(lowerText).Select(m => m.Value);
            
            // Find possible matches
            var candidates = _systemNames.Where(n => words.Any(w => n == w || n.StartsWith(w)));

            return candidates.Where(lowerText.Contains).ToList();
        }
    }
}