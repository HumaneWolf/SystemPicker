using System.Web;

namespace SystemPicker.Matcher
{
    public class SystemMatch
    {
        public string Name { get; }
        public long Id64 { get; }
        
        // public string EDDBUrl => $"todo:{HttpUtility.HtmlEncode(Name)}";
        public string EDSMUrl => $"https://www.edsm.net/en/system/id/_/name/{HttpUtility.HtmlEncode(Name)}";
        // public string InaraUrl => $"todo:{HttpUtility.HtmlEncode(Name)}";
        public string SpanshUrl => $"https://spansh.co.uk/system/{HttpUtility.HtmlEncode(Id64)}";

        public SystemMatch(string name, long id64)
        {
            Name = name;
            Id64 = id64;
        }
    }
}