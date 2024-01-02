namespace SefosApi1.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Links
    {
        public List<string> sent { get; set; }
        public List<string> not_sent { get; set; }
    }

    public class SefosResponse
    {
        public string message_uuid { get; set; }
        public Links links { get; set; }
        public bool all_sent { get; set; }
    }


}
