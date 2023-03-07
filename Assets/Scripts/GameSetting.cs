using UnityEngine;

namespace RpgChatGPTDemo.Asset
{
    [CreateAssetMenu]
    public class GameSetting : ScriptableObject
    {
        [SerializeField] private string _openAiApiKey;
        [SerializeField] private string _chatGptModel;

        public string OpenAiApiKey { get => _openAiApiKey; }
        public string ChatGptModel { get => _chatGptModel; }
    }
}