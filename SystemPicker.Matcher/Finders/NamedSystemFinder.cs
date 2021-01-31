using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SystemPicker.Matcher.Finders
{
    public static class NamedSystemFinder
    {
        public static Regex NamedSystemRegex = 
            new(@"\b[a-zA-Z0-9]\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static void SetSystems(IEnumerable<string> systems)
        {
            var nameGroup = $@"(?:{string.Join('|', systems)})";
            NamedSystemRegex = 
                new($@"\b{nameGroup}\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }
    }
}