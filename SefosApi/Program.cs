using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackEndProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public MessagesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MessageDto messageDto)
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "<Your Bearer Token Here>");

            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                functionboxUuid = "u95yt3zx933p:09pf5h9o",
                sefos_participants = new[] { "funktion2@fatg.se" },
                external_participants = new[] { messageDto.Email },
                subject = messageDto.Subject,
                body = messageDto.Message
            }), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://test-meaplus.sefos.se/server/rest/api/secure", content);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            return BadRequest();
        }
    }

    public class MessageDto
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}