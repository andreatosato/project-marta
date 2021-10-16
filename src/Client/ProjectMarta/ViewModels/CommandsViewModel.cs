using Microsoft.CognitiveServices.Speech;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectMarta.ViewModels
{
    public class CommandsViewModel : BaseViewModel
    {
        private SpeechRecognizer recognizer;
        private bool isTranscribing = false;

        public string RecordingButtonText { get; set; }
        public Color RecordingButtonColor { get; set; }
        public ICommand RecordingCommand { get; set; }
        public bool LoadingValue { get; set; }
        public string TranscriptText { get; set; }

        public CommandsViewModel()
        {
            Title = "Commands";
            RecordingCommand = new Command(async () => await StartRecordingAsync());
            RecordingButtonText = "Premi e parla ;-)";
            RecordingButtonColor = Color.Green;
            TranscriptText = "Dimmi qualcosa!";
        }

        private async Task StartRecordingAsync()
        {
            bool isMicEnabled = await MicrophoneService.GetPermissionAsync();

            // EARLY OUT: make sure mic is accessible
            if (!isMicEnabled)
            {
                UpdateTranscription("Please grant access to the microphone!");
                return;
            }

            TranscriptText = string.Empty;
            // initialize speech recognizer 
            if (recognizer == null)
            {
                var config = SpeechConfig.FromSubscription(Constants.CognitiveServicesApiKey, Constants.CognitiveServicesRegion);
                recognizer = new SpeechRecognizer(config);
                recognizer.Recognized += (obj, args) =>
                {
                    var findElement = GalleryItemDataStore.SearchCommandAsync(args.Result.Text);
                    if (findElement == null)
                    {
                        return;
                    }

                    UpdateTranscription(args.Result.Text);
                };
            }

            // if already transcribing, stop speech recognizer
            if (isTranscribing)
            {
                try
                {
                    await recognizer.StopContinuousRecognitionAsync();
                }
                catch (Exception ex)
                {
                    UpdateTranscription(ex.Message);
                }
                isTranscribing = false;
            }

            // if not transcribing, start speech recognizer
            else
            {
                //Device.BeginInvokeOnMainThread(() =>
                //{
                //    //InsertDateTimeRecord();
                //});
                try
                {
                    await recognizer.StartContinuousRecognitionAsync();
                }
                catch (Exception ex)
                {
                    UpdateTranscription(ex.Message);
                }
                isTranscribing = true;
            }
            UpdateDisplayState();
        }

        void UpdateTranscription(string newText)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (!string.IsNullOrWhiteSpace(newText))
                {
                    TranscriptText += $"{newText}\n";
                }
            });
        }

        void UpdateDisplayState()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (isTranscribing)
                {
                    RecordingButtonText = "Stop";
                    RecordingButtonColor = Color.Red;
                    LoadingValue = true;
                }
                else
                {
                    RecordingButtonText = "Transcribe";
                    RecordingButtonColor = Color.Green;
                    LoadingValue = false;
                }
            });
        }

        public async Task OnAppearing()
        {
        }
    }
}
