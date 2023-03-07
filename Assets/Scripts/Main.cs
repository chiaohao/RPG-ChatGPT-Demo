using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace RpgChatGPTDemo
{
    public class Main : MonoBehaviour
    {
        private enum GameState
        { 
            Idle,
            CommunicateNpc,
        }

        [SerializeField] private Asset.GameSetting _gameSetting;
        [SerializeField] private Character.ChatGPTNpc[] _npcs;

        private GameState _gameState;

        private async void Start()
        {
            var network = new Network.ChatGPTNetwork(_gameSetting.OpenAiApiKey, _gameSetting.ChatGptModel);

            foreach (var npc in _npcs)
                npc.Prepare(network);
            _gameState = GameState.Idle;

            await _GameLoop(_npcs);
        }

        private async UniTask _GameLoop(
            IReadOnlyCollection<Character.INpc> npcs)
        {
            while (true)
            {
                try
                {
                    if (_gameState != GameState.Idle)
                        Debug.Log(_gameState);
                    switch (_gameState)
                    {
                        case GameState.Idle:
                            await UniTask.Yield();
                            break;

                        case GameState.CommunicateNpc:
                            foreach (var npc in npcs)
                            {
                                var response = await npc.Communicate("���");
                                Debug.Log(response);
                            }
                            _gameState = GameState.Idle;
                            break;

                        default:
                            await UniTask.Yield();
                            break;
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

        private void Update()
        {
            if (Input.anyKeyDown && _gameState == GameState.Idle)
            {
                _gameState = GameState.CommunicateNpc;
            }
        }
    }
}