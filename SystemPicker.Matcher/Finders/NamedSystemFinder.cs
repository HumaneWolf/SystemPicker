using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SystemPicker.Matcher.Finders
{
    public static class NamedSystemFinder
    {
        private static List<string> _systems = new();
        
        public static Regex NamedSystemRegex = 
            new(@"\b[a-zA-Z0-9]\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
        private static readonly Regex WordRegex = 
            new(@"\b(?:\B)+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static void SetSystems(IEnumerable<string> systems)
        {
            _systems = systems.Select(s => s.ToLower()).ToList();

            // todo: We need a better solution.
            systems = _systems.Select(s => s
                .Replace("(", @"\(")
                .Replace(")", @"\)")
                .Replace("[", @"\[")
                .Replace("]", @"\]")
                .Replace("{", @"\{")
                .Replace("}", @"\}")
                .Replace(".", @"\.")
                .Replace("+", @"\+")
                .Replace("*", @"\*"));
            var nameGroup = $@"(?:{string.Join('|', systems)})";
            NamedSystemRegex = 
                new($@"\b{nameGroup}\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public static List<string> FindCandidates(string text)
        {
            var lowerText = text.ToLower();

            // See if any system might be in the text.
            var candidates = _systems.Where(s => lowerText.Contains(s));

            // Filter out "false positives", where system name is part of larger word.
            return GetDynamicRegex(candidates).Matches(text).Select(m => m.Value).ToList();
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
    }
}