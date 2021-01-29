using System.Web;

namespace SystemPicker.Matcher.Models
{
    public class SystemMatch
    {
        public string Name { get; }
        public long Id64 { get; }
        
        public string EDDBUrl => $"https://eddb.io/system/name/{HttpUtility.HtmlEncode(Name)}";
        public string EDSMUrl => $"https://www.edsm.net/en/system/id/_/name/{HttpUtility.HtmlEncode(Name)}";
        public string InaraUrl => $"https://inara.cz/galaxy-starsystem/?search={HttpUtility.HtmlEncode(Name)}";
        public string SpanshUrl => $"https://spansh.co.uk/system/{HttpUtility.HtmlEncode(Id64)}";

        public SystemMatch(string name, long id64)
        {
            Name = name;
            Id64 = id64;
        }
    }
}