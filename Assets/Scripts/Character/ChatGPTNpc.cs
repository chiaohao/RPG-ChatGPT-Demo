using Cysharp.Threading.Tasks;
using RpgChatGPTDemo.Network;
using System;
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
        private Action<Collider> _enterTriggerAction;
        private Action<Collider> _exitTriggerAction;

        public void Prepare(ChatGPTNetwork network)
        {
            _network = network;
            _reservedMessages = _pretrainedMessages
                .Select(message => message.ConvertToNetworkMessage())
                .ToList();
        }

        public void RegisterColliderEnterTrigger(Action<Collider> action)
        {
            _enterTriggerAction += action;
        }

        public void UnregisterColliderEnterTrigger(Action<Collider> action)
        {
            _enterTriggerAction -= action;
        }

        public void RegisterColliderExitTrigger(Action<Collider> action)
        {
            _exitTriggerAction += action;
        }

        public void UnregisterColliderExitTrigger(Action<Collider> action)
        {
            _exitTriggerAction -= action;
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

        private void OnTriggerEnter(Collider other)
        {
            _enterTriggerAction?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            _exitTriggerAction?.Invoke(other);
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