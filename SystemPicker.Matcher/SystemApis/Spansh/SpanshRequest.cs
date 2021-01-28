using System.Collections.Generic;

namespace SystemPicker.Matcher.SystemApis.Spansh
{
    // {"filters":{"name":{"value":"Sol"}},"sort":[{"distance":{"direction":"asc"}}],"size":10,"page":0}
    
    class SpanshRequest
    {
        public SpanshFilter Filters { get; } = new();
        public SpanshSort Sort { get; } = new();

        public long Size { get; set; } = 10;
        public long Page { get; set; } = 0;

        public SpanshRequest(string systemName)
        {
            Filters.Name.Value = systemName;
            Sort.Name.Direction = "asc";
        }
    }

    class SpanshFilter
    {
        public SpanshFilterValue Name { get; } = new();
    }

    class SpanshFilterValue
    {
        public string Value { get; set; }        
    }
    
    class SpanshSort
    {
        public SpanshSortDirection Name { get; } = new();
    }

    class SpanshSortDirection
    {
        public string Direction { get; set; }
    }
}