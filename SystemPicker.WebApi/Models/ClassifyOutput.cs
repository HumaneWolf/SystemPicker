namespace SystemPicker.WebApi.Models
{
    public class ClassifyOutput
    {
        public string Classification { get; set; }
        
        public ClassifyOutput(string classification)
        {
            Classification = classification;
        }
    }
}