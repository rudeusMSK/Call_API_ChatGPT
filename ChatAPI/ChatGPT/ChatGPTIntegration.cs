using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ChatAPI.ChatGPT
{
    public class ChatGPTIntegration
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> GetChatGPTResponse(string question)
        {
            string apiKey = "sk- key của bạn"; // đổi key của bạn tại đây
            string model = "model bạn muốn"; // recommend: gpt-3.5-turbo

            string apiUrl = "https://api.openai.com/v1/chat/completions";
            string requestData = "{\"model\": \"" + model + "\", \"prompt\": \"" + question + "\"}";

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
            var content = new StringContent(requestData, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);

            string responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }
    }
}