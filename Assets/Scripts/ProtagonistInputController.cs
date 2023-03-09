using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace RpgChatGPTDemo.Character
{
    public class ProtagonistInputController
    {
        private CharacterAction _characterAction;

        private Action<Vector2> _wasdAction;
        private Action<Vector2> _mouseMoveAction;

        public void RegisterWASDInput(Action<Vector2> action)
        {
            _wasdAction += action;
        }

        public void UnregisterWASDInput(Action<Vector2> action)
        {
            _wasdAction -= action;
        }

        public void RegisterMouseMoveInput(Action<Vector2> action)
        {
            _mouseMoveAction += action;
        }

        public void UnregisterMouseMoveInput(Action<Vector2> action)
        {
            _mouseMoveAction -= action;
        }

        public async UniTask LoopActions()
        {
            if (_characterAction == null)
            {
                _characterAction = new CharacterAction();
                _characterAction.Enable();
            }
            while (true)
            {
                var wasdInputValue = _characterAction.Protagonist.Movement.ReadValue<Vector2>();
                _wasdAction?.Invoke(wasdInputValue);
                var mouseMoveInputValue = _characterAction.Protagonist.Rotation.ReadValue<Vector2>();
                _mouseMoveAction?.Invoke(mouseMoveInputValue);
                await UniTask.Yield();
            }
        }
    }
}