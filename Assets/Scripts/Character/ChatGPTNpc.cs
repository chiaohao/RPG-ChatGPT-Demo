using Cysharp.Threading.Tasks;
using RpgChatGPTDemo.Network;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RpgChatGPTDemo.Character
{
    public class ChatGPTNpc : MonoBehaviour, INpc
    {
        public enum Role
        { 
            User,
            Assistant,
        }

        [System.Serializable]
        public struct Message
        {
            public Role Role;
            public string Content;
        }

        [SerializeField] private Message[] _pretrainedMessages;

        private ChatGPTNetwork _network;
        private IReadOnlyList<ChatGPTNetwork.ChatCompletionMessage> _reservedMessages;

        public void Prepare(ChatGPTNetwork network)
        {
            _network = network;
            _reservedMessages = _pretrainedMessages
                .Select(message => message.ConvertToNetworkMessage())
                .ToList();
        }

        async UniTask<string> INpc.Communicate(string content)
        {
            var messages = new List<ChatGPTNetwork.ChatCompletionMessage>(_reservedMessages);
            messages.Add(
                new Message()
                {
                    Role = Role.User,
                    Content = content
                }
                .ConvertToNetworkMessage());

            var result = await _network.PostChatCompletion(messages);
            return result.Choices.First().Message["content"];
        }
    }

    public static class ChatCompletionMessageExtention
    {
        public static ChatGPTNetwork.ChatCompletionMessage ConvertToNetworkMessage(this ChatGPTNpc.Message message)
        {
            return new ChatGPTNetwork.ChatCompletionMessage()
            {
                Role = message.Role switch
                {
                    ChatGPTNpc.Role.User => "user",
                    ChatGPTNpc.Role.Assistant => "assistant",
                    _ => throw new System.NotImplementedException()
                },
                Content = message.Content
            };
        }
    }
}