namespace SefosApi1.Models
{
    public class SefosMessage
    {
        public string? functionbox_uuid { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public List<Attachment>? attachments { get;  set; } = new List<Attachment>();
        public string? external_text { get; set; } = string.Empty;
        public List<SefosParticipant>? sefos_participants { get; set; } = new List<SefosParticipant>();
        public List<ExternalParticipant> external_participants { get; set; } = new List<ExternalParticipant>();
        public Settings? settings { get; set; } = new Settings();
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Attachment
    {
        public string content { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class ExternalParticipant
    {
        public string email { get; set; }
        public string language { get; set; }
        public string? authentication_method { get; set; }
        public string? authentication_identifier { get; set; }
        public bool configured { get; set; }
    }


    public class SefosParticipant
    {
        public string email { get; set; }
    }

    public class Settings
    {
        public int loa_level { get; set; }
        public int require_response { get; set; }
    }


}
