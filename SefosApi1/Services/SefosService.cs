using SefosApi1.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace SefosApi1.Services
{
    public class SefosService
    {
        private HttpClient client;
        private readonly string sefosToken;
        public SefosService(IConfiguration configuration)
        {
            sefosToken = configuration.GetValue<string>("SefosToken");
            client = new HttpClient();
            client.BaseAddress = new Uri("https://test-meaplus.sefos.se/server/rest/api/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sefosToken);
        }
        public async Task<SefosResponse> sendSefosMessage(SefosMessage message)
        {
            message.functionbox_uuid = "f7b3c5e0-5b7b-11eb-8e6f-0242ac130004";
            message.settings.loa_level = 0;
            message.settings.require_response = 0;
            message.sefos_participants.Add(new SefosParticipant { email = "funktion2@fatg.se" });

            HttpContent content = new StringContent(JsonSerializer.Serialize(message), Encoding.UTF8, "application/json"); // skapar en content som vi kan skicka med vår request. httpbody
            var response = await client.PostAsync("secure", content);

            if (response.IsSuccessStatusCode)
            {
                SefosResponse? responseContent = await response.Content.ReadFromJsonAsync<SefosResponse>(); //den tar respons och konverterar den till vår model
                if (responseContent == null)
                {
                    throw new Exception("Unknow contentResponse, could not be converted");
                }
                return responseContent;
            }
            else
            {
                throw new Exception("Failed to send message");
            }
        }
    }
}
