namespace SefosWebApp.Models
{
    public class Email
    {
        public Email(string emailSender, string emailSubject, string emailBody)
        {
            this.external_participants.Add(new ExternalParticipant
            {
                email = emailSender,
                language = "SE",
                configured = true
            });
            this.subject = emailSubject;
            this.body = emailBody;
        }
        public List<ExternalParticipant> external_participants { get; set; } = new List<ExternalParticipant>();
        public string subject { get; set; }
        public string body { get; set; }


    }
    public class ExternalParticipant
    {
        public string email { get; set; }
        public string language { get; set; }
        public string? authentication_method { get; set; }
        public string? authentication_identifier { get; set; }
        public bool configured { get; set; }
    }
}
