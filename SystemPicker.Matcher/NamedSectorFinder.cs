using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SystemPicker.Matcher
{
    public static class NamedSectorFinder
    {
        public static Regex NamedSectorSystemRegex = new($@"\b.+ sector {ProcGenFinder.SystemIdRegex}\b",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static Regex FullWordRegex = new($@"^.+ sector {ProcGenFinder.SystemIdRegex}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static readonly Regex ExtractSectorRegex = new($@"^(.+) sector {ProcGenFinder.SystemIdRegex}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
        public static void SetSectors(IEnumerable<string> sectors)
        {
            var sectorsGroup = $@"(?:{string.Join('|', sectors)})";

            NamedSectorSystemRegex = new($@"\b{sectorsGroup} sector {ProcGenFinder.SystemIdRegex}\b",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            
            FullWordRegex = new($@"^{sectorsGroup} sector {ProcGenFinder.SystemIdRegex}$",
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