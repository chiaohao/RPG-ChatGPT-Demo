using Cysharp.Threading.Tasks;

namespace RpgChatGPTDemo.Character
{
    public interface INpc
    {
        UniTask<string> Communicate(string content);
    }
}