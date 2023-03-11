using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace RpgChatGPTDemo.UI
{
    public class SpeakingDialogView : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private float _wordSpeedInSeconds = 0.01f;

        public async UniTask Render(string content)
        {
            if (content == null)
                return;
            var length = 0;
            while (length <= content.Length)
            {
                _text.text = content.Substring(0, length);
                ++length;
                await UniTask.Delay((int)(_wordSpeedInSeconds * 10e3));
            }
        }
    }
}