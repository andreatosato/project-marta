using Microsoft.CognitiveServices.Speech;
using System.Threading.Tasks;

namespace ProjectMarta.Services
{
    public interface ISpeechService
    {
        Task SpeechAsync(string text);
        SpeechRecognizer GetRecognizer();
    }
}
