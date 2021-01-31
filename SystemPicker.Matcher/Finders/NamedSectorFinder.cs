using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SystemPicker.Matcher.Finders
{
    public static class NamedSectorFinder
    {
        public static Regex NamedSectorSystemRegex = new($@"\b.+ (?:sector|region) {ProcGenFinder.SystemIdRegex}\b",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static Regex FullWordRegex = new($@"^.+ (?:sector|region) {ProcGenFinder.SystemIdRegex}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static readonly Regex ExtractSectorRegex = new($@"^(.+) (?:sector|region) {ProcGenFinder.SystemIdRegex}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
        public static void SetSectors(IEnumerable<string> sectors)
        {
            var sectorsGroup = $@"(?:{string.Join('|', sectors)})";

            NamedSectorSystemRegex = new($@"\b{sectorsGroup} (?:sector|region) {ProcGenFinder.SystemIdRegex}\b",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            
            FullWordRegex = new($@"^{sectorsGroup} (?:sector|region) {ProcGenFinder.SystemIdRegex}$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public static string ExtractSectorName(string systemName)
        {
            var match = ExtractSectorRegex.Match(systemName);
            if (match.Success && match.Groups.Count >= 1)
            {
                return match.Groups[0].Value;
            }
            return null;
        }
    }
}