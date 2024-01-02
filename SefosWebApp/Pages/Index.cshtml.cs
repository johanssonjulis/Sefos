using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SefosWebApp.Models;

namespace SefosWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public void OnPost() 
        {
            string emailSender = Request.Form["emailSender"];
            string emailSubject = Request.Form["emailSubject"];
            string emailBody = Request.Form["emailBody"];

            Email email = new Email(emailSender, emailSubject, emailBody);

            HttpClient client = new HttpClient();
            var response = client.PostAsJsonAsync("https://localhost:7027/api/message", email).GetAwaiter().GetResult();
        }
    }
}