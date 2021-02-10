using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SystemPicker.Matcher.Finders
{
    public static class NamedSystemFinder
    {
        public static List<string> Systems = new();

        public static void SetSystems(IEnumerable<string> systems)
        {
            Systems = systems.Select(s => s.ToLower()).ToList();
        }

        public static List<string> FindCandidates(string text)
        {
            var lowerText = text.ToLower();

            // See if any system might be in the text.
            var candidates = Systems.Where(s => lowerText.Contains(s)).ToList();

            if (!candidates.Any())
            {
                return new();
            }

            // Filter out "false positives", where system name is part of larger word.
            return GetDynamicRegex(candidates).Matches(text).Where(m => m.Success).Select(m => m.Value).ToList();
        }
        
        private static Regex GetDynamicRegex(IEnumerable<string> systems)
        {
            // todo: We need a better solution.
            systems = systems.Select(s => s
                .Replace("(", @"\(")
                .Replace(")", @"\)")
                .Replace("[", @"\[")
                .Replace("]", @"\]")
                .Replace("{", @"\{")
                .Replace("}", @"\}")
                .Replace(".", @"\.")
                .Replace("+", @"\+")
                .Replace("*", @"\*")
                .Replace("|", @"\|"));
            var nameGroup = $@"(?:{string.Join('|', systems)})";
            return new($@"\b{nameGroup}\b", RegexOptions.IgnoreCase);
        }

        public static bool IsNamedSystem(string name)
        {
            return Systems.Any(x => x == name.ToLower());
        }
    }
}