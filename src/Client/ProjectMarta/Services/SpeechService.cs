using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
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
                var config = SpeechConfig.FromSubscription(Constants.CognitiveServicesApiKey, Constants.CognitiveServicesRegion);

                var sourceLanguageConfig = SourceLanguageConfig.FromLanguage("it-IT"); //("en-US");

                Synthesizer = new SpeechSynthesizer(config);
                Recognizer = new SpeechRecognizer(config, sourceLanguageConfig);
        }

        public async Task SpeechAsync(string text)
        {
            var result = await Synthesizer.SpeakTextAsync(text);
            AudioPlayer.Load(new MemoryStream(result.AudioData));
        }

        public SpeechRecognizer GetRecognizer() => Recognizer;
    }
}
