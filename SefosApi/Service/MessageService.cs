// MessageService.cs
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackendApiProject.Services
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;

        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse> SendToSefosAsync(MessageModel message)
        {
            try
            {
                // Förbered meddelandet att skicka till Sefos-API
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/secure/CreateMessage");

                // Lägg till Bearer-token för att autentisera mot Sefos-API
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "DittBearerTokenHär");

                // Skapa ett objekt som matchar Sefos API-specifikationen för CreateMessage
                var sefosMessage = new
                {
                    functionboxUuid = "u95yt3zx933p:09pf5h9o",
                    sefos_participants = new[] { "registrerad_participant1", "registrerad_participant2" },
                    external_participants = new[] { message.SenderEmail },
                    subject = message.Subject,
                    body = message.Body
                };

                // Konvertera objektet till JSON och lägg till det i request-body
                var jsonContent = new StringContent(JsonSerializer.Serialize(sefosMessage), Encoding.UTF8, "application/json");
                requestMessage.Content = jsonContent;

                // Utför anropet till Sefos-API
                var response = await _httpClient.SendAsync(requestMessage);

                // Hantera svaret från Sefos-API
                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse { Success = true, Message = "Meddelande skickat till Sefos-API" };
                }
                else
                {
                    // Läs felmeddelandet från Sefos-API:s svar och returnera det
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    return new ApiResponse { Success = false, Message = $"Fel från Sefos-API: {errorResponse}" };
                }
            }
            catch (Exception ex)
            {
                // Fångar eventuella undantag vid anropet
                return new ApiResponse { Success = false, Message = $"Fel vid anrop till Sefos-API: {ex.Message}" };
            }
        }
    }
}
