using System;
using UnityEngine;
using UnityEngine.UI;

namespace RpgChatGPTDemo.UI
{
    public class DialogOptionButton : MonoBehaviour
    {
        [SerializeField] Button _button;
        [SerializeField] Text _text;

        private Action _onButtonClicked;

        private void Awake()
        {
            _button.onClick.AddListener(_OnButtonClicked);
        }

        public void Render(
            string context,
            Action onButtonClicked)
        {
            _text.text = context;
            _onButtonClicked = onButtonClicked;
        }

        private void _OnButtonClicked() => _onButtonClicked?.Invoke();
    }
}