using System.Collections.Generic;

namespace SystemPicker.Matcher.SystemApis.Spansh
{
    class SpanshResponse
    {
        public long Count { get; set; }
        public long From { get; set; }
        
        public List<SpanshSystem> Results { get; set; }
    }

    class SpanshSystem
    {
        public long Id64 { get; set; }
        public string Name { get; set; }
    }
}