using System.Collections.Generic;

namespace SystemPicker.Matcher.SystemApis.FuelRats
{
    public class FuelRatsResponse
    {
        public FuelRatsMeta Meta { get; set; }
        public List<FuelRatsSystem> Data { get; set; }
    }
    
    public class FuelRatsSystem
    {
        public string Name { get; set; }
        public double Similarity { get; set; }
        public long Id64 { get; set; }
    }

    public class FuelRatsMeta
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}