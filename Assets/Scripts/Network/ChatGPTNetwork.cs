using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace RpgChatGPTDemo.Network
{
    public class ChatGPTNetwork
    {
        public struct ChatCompletionRequest
        {
            [JsonProperty("model")]
            public string Model;
            [JsonProperty("messages")]
            public IReadOnlyCollection<ChatCompletionMessage> Messages;
        }

        public struct ChatCompletionMessage
        {
            [JsonProperty("role")]
            public string Role;
            [JsonProperty("content")]
            public string Content;
        }

        public struct ChatCompletionResponse
        {
            public struct TokenUsage
            {
                [JsonProperty("prompt_tokens")]
                public string PromptTokens;
                [JsonProperty("completion_tokens")]
                public string CompletionTokens;
                [JsonProperty("total_tokens")]
                public string TotalTokens;
            }

            public struct Choice
            {
                [JsonProperty("message")]
                public Dictionary<string, string> Message;
                [JsonProperty("finish_reason")]
                public string FinishReason;
                [JsonProperty("index")]
                public int Index;
            }

            [JsonProperty("id")]
            public string Id;
            [JsonProperty("object")]
            public string Object;
            [JsonProperty("created")]
            public string CreatedAt;
            [JsonProperty("model")]
            public string Model;
            [JsonProperty("usage")]
            public TokenUsage Usage;
            [JsonProperty("choices")]
            public Choice[] Choices;
        }

        public enum ChatRole
        {
            User,
            Assistant,
        }

        private static string URL = "https://api.openai.com/v1/chat/completions";

        private readonly string _token;
        private readonly string _model;

        public ChatGPTNetwork(string token, string model)
        {
            _token = token;
            _model = model;
        }

        public async UniTask<ChatCompletionResponse> PostChatCompletion(IReadOnlyCollection<ChatCompletionMessage> messages)
        {
            var body = new ChatCompletionRequest()
            {
                Model = _model,
                Messages = messages
            };

            var request = new UnityWebRequest(URL, "POST");
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes( JsonConvert.SerializeObject(body)));
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {_token}");

            var response = await request.SendWebRequest();

            if (response.result != UnityWebRequest.Result.Success)
            {
                throw new System.Exception(request.error);
            }

            return JsonConvert.DeserializeObject<ChatCompletionResponse>(response.downloadHandler.text);
        }
    }
}