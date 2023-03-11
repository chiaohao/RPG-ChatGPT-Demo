using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RpgChatGPTDemo.UI
{
    public class InteractableDialogView : MonoBehaviour
    {
        private static readonly string WAIT_CONTENT = "...";

        [SerializeField] private SpeakingDialogView _coreDialogView;

        [Header("Options")]
        [SerializeField] private Transform _optionRoot;
        [SerializeField] private DialogOptionButton _optionButtonPrefab;

        private bool _isWhiteSpaceButtonPressed;
        private List<DialogOptionButton> _optionButtonPool;

        public void Initialize(
            Character.ProtagonistInputController protagonistInputController)
        {
            protagonistInputController.RegisterWhitespaceInput(
                () => _isWhiteSpaceButtonPressed = true);
            _optionButtonPool = new List<DialogOptionButton>();
        }

        public async UniTask Render(
            Func<string, UniTask<string>> contextGenerator,
            string initKeyWord,
            IReadOnlyCollection<IReadOnlyCollection<string>> keyWordSequence)
        {
            _coreDialogView.gameObject.SetActive(true);

            await _RenderContext(contextGenerator, initKeyWord);

            if (keyWordSequence != null)
            {
                foreach (var seq in keyWordSequence)
                {
                    if (seq != null && seq.Count() > 0)
                    {
                        var userOption = await _GetUserOption(seq);
                        await _RenderContext(contextGenerator, userOption);
                    }
                }
            }

            _isWhiteSpaceButtonPressed = false;
            await UniTask.WaitWhile(() => !_isWhiteSpaceButtonPressed);
            _coreDialogView.gameObject.SetActive(false);
        }

        private async UniTask _RenderContext(
            Func<string, UniTask<string>> contextGenerator,
            string key)
        {
            var content = string.Empty;
            await UniTask.WhenAll(
                new[]
                {
                    contextGenerator.Invoke(key).ContinueWith(result => content = result),
                    _coreDialogView.Render(WAIT_CONTENT)
                });
            await _coreDialogView.Render(content);
        }

        private async UniTask<string> _GetUserOption(IReadOnlyCollection<string> seq)
        {
            _optionRoot.gameObject.SetActive(true);

            string userOption = null;
            var options = seq.ToArray();
            while (_optionButtonPool.Count() < options.Count())
                _optionButtonPool.Add(Instantiate(_optionButtonPrefab, _optionRoot));
            for (var i = 0; i < _optionButtonPool.Count(); ++i)
            {
                if (i < options.Count())
                {
                    var index = i;
                    _optionButtonPool[i].gameObject.SetActive(true);
                    _optionButtonPool[i].Render(options[i], () => userOption = options[index]);
                }
                else
                {
                    _optionButtonPool[i].gameObject.SetActive(false);
                }
            }

            await UniTask.WaitWhile(() => userOption == null);

            _optionRoot.gameObject.SetActive(false);

            return userOption;
        }
    }
}