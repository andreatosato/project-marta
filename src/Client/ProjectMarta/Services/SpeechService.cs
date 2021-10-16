using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace ProjectMarta.Services
{
    public class SpeechService : ISpeechService
    {
        private SpeechSynthesizer Synthesizer;
        private Plugin.SimpleAudioPlayer.ISimpleAudioPlayer AudioPlayer => Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;

        public SpeechService()
        {
            var config = SpeechConfig.FromSubscription(Constants.CognitiveServicesApiKey, Constants.CognitiveServicesRegion);
            Synthesizer = new SpeechSynthesizer(config);
        }

        public async Task SpeechAsync(string text)
        {
            var result = await Synthesizer.SpeakTextAsync(text);
            AudioPlayer.Load(new MemoryStream(result.AudioData));
        }
    }
}
