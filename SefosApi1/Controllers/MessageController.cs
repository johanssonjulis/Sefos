using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SefosApi1.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using SefosApi1.Services;
using System.Runtime.CompilerServices;


//tre lagers arkitektur. Vill inte ha affärslogik i controllern. Därför skapar vi en service som vi kan anropa från controllern.
// Har du mer än en metod i controllern så är det dags att skapa en service
namespace SefosApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase 
    {
        private readonly SefosService _sefosService;
        
        public MessageController(SefosService sefosService)
        {
           _sefosService = sefosService;
        }
        [HttpPost]// metoden som vår frontend kommer att kalla på
        public async Task<IActionResult> Post([FromBody] SefosMessage message)
        {
            try 
            {
                var response = await _sefosService.sendSefosMessage(message);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
