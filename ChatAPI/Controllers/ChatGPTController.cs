using ChatAPI.ChatGPT;
using ChatAPI.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChatAPI.Controllers
{
    public class ChatGPTController : Controller
    {
        private readonly string _openAiApiKey;
        private readonly string _openAiApiUrl = "https://api.openai.com/v1/chat/completions";

        public ChatGPTController()
        {
            _openAiApiKey = "sk- key của bạn !"; // đổi key của bạn tại đây
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new ChatGPTModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ChatGPTModel model)
        {
            var answer = await ChatGPTAnswer(model.Question);
            model.Answer = answer;

            // phiên bản cũ hơn.

            //var chatGPTIntegration = new ChatGPTIntegration();
            //string response = await chatGPTIntegration.GetChatGPTResponse(model.Question);
            //model.Answer = response;

            return View(model);
        }

        private async Task<string> ChatGPTAnswer(string question)
        {
            var client = new RestClient(_openAiApiUrl);
            var request = new RestRequest();

            request.Method = Method.Post; // Gán phương thức là POST

            request.AddHeader("Authorization", $"Bearer {_openAiApiKey}");
            request.AddHeader("Content-Type", "application/json");

            // cấu hình lại theo tài khoản của bạn.
            JObject jsonBody = new JObject();
            jsonBody.Add("model", "model bạn muốn!"); //recommend: gpt-3.5-turbo
            jsonBody.Add("prompt", question);
            jsonBody.Add("max_tokens", 100);
            jsonBody.Add("temperature", 0.5);

            request.AddParameter("application/json", jsonBody.ToString(), ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                JObject jsonResponse = JObject.Parse(response.Content);
                return jsonResponse["choices"][0]["text"].ToString();
            }
            else
            {
                return $"{response.Content}";
            }
        }
    }
}