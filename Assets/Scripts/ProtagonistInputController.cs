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
        private Action _whitespaceAction;

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

        public void RegisterWhitespaceInput(Action action)
        {
            _whitespaceAction += action;
        }

        public void UnregisterWhitespaceInput(Action action)
        {
            _whitespaceAction -= action;
        }

        public async UniTask LoopActions()
        {
            if (_characterAction == null)
            {
                _characterAction = new CharacterAction();
                _characterAction.Enable();
                _characterAction.Protagonist.Action.started += _ =>
                {
                    _whitespaceAction?.Invoke();
                };
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