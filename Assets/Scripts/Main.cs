using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace RpgChatGPTDemo
{
    public class Main : MonoBehaviour
    {
        [Serializable]
        private struct NpcInfo
        {
            [SerializeField] private string _id;
            [SerializeField] private Character.ChatGPTNpc _npc;

            public string Id { get => _id; }
            public Character.ChatGPTNpc Npc { get => _npc; }
        }

        private enum GameState
        { 
            Idle,
            CommunicateNpc,
        }

        [SerializeField] private Asset.GameSetting _gameSetting;
        [SerializeField] private Character.Protagonist _protagonist;
        [SerializeField] private NpcInfo[] _npcInfos;

        private Character.ProtagonistInputController _inputController;
        private GameState _gameState;
        private string _triggeredNpcId;

        private void Start()
        {
            _inputController = new Character.ProtagonistInputController();
            _inputController.RegisterWASDInput(inputValue =>
            {
                if (_gameState == GameState.Idle)
                    _protagonist?.Move(inputValue);
            });
            _inputController.RegisterMouseMoveInput(inputValue =>
            {
                if (_gameState == GameState.Idle)
                    _protagonist?.Rotate(inputValue);
            });
            _inputController.RegisterWhitespaceInput(() =>
            {
                if (_gameState == GameState.Idle && _triggeredNpcId != null)
                    _gameState = GameState.CommunicateNpc;
            });

            var network = new Network.ChatGPTNetwork(_gameSetting.OpenAiApiKey, _gameSetting.ChatGptModel);

            foreach (var npcInfo in _npcInfos)
            {
                npcInfo.Npc.Prepare(network);
                npcInfo.Npc.RegisterColliderEnterTrigger(_ =>
                {
                    _triggeredNpcId = npcInfo.Id;
                });
                npcInfo.Npc.RegisterColliderExitTrigger(_ =>
                {
                    if (_triggeredNpcId == npcInfo.Id)
                        _triggeredNpcId = null;
                });
            }
            _gameState = GameState.Idle;

            _GameLoop(
                _npcInfos.ToDictionary(
                    info => info.Id, 
                    info => info.Npc as Character.INpc))
                .Forget();
            _inputController.LoopActions().Forget();
        }

        private async UniTask _GameLoop(
            IReadOnlyDictionary<string, Character.INpc> npcDicts)
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
                            if (npcDicts.TryGetValue(_triggeredNpcId, out var npc))
                            {
                                var answer = await npc.Communicate("Communicate");
                                Debug.Log(answer);
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
    }
}