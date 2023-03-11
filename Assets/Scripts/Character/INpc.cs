using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace RpgChatGPTDemo.Character
{
    public interface INpc
    {
        UniTask<string> Communicate(string content);
        string InitialCommunicationContent { get; }
        IReadOnlyCollection<string> OptionCommunicationContent { get; }
    }
}