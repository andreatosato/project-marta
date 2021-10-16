using Microsoft.CognitiveServices.Speech;
using System.IO;
using System.Threading.Tasks;

namespace ProjectMarta.Services
{
    public class SpeechService : ISpeechService
    {
        private readonly SpeechSynthesizer Synthesizer;
        private readonly SpeechRecognizer Recognizer;
        private Plugin.SimpleAudioPlayer.ISimpleAudioPlayer AudioPlayer => Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;

        public SpeechService()
        {
            var language = "it-IT";
            var config = SpeechConfig.FromSubscription(Constants.CognitiveServicesApiKey, Constants.CognitiveServicesRegion);
            config.SpeechSynthesisLanguage = language;
            config.SpeechRecognitionLanguage = language;
            Synthesizer = new SpeechSynthesizer(config);
            Recognizer = new SpeechRecognizer(config);
        }

        public async Task SpeechAsync(string text)
        {
            var result = await Synthesizer.SpeakTextAsync(text);
            AudioPlayer.Load(new MemoryStream(result.AudioData));
        }

        public SpeechRecognizer GetRecognizer() => Recognizer;
    }
}
