using Microsoft.AspNetCore.Mvc;

namespace SefosApi.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MessageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] string message)
        {
            if (message == null || string.IsNullOrWhiteSpace(message.Body))
            {
                return BadRequest("Invalid message data");
            }
            

            var result = await _messageService.SendToSefosAsync(message);

            if (result.Sucess)
            {
                return Ok("Message sent to Sefos API successfully");
            }
            else
            {
                return StatusCode(500, "Failed to send message to Sefos API");
            }
        }
    }
}