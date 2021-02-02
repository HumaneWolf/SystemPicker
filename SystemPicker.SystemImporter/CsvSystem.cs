using CsvHelper.Configuration.Attributes;

namespace SystemPicker.SystemImporter
{
    public class CsvSystem
    {
        [Name("ed_system_address")]
        public long? EDSystemAddress { get; set; }
        
        [Name("name")]
        public string Name { get; set; }
    }
}