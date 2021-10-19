using Microsoft.CognitiveServices.Speech;
using ProjectMarta.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectMarta.ViewModels
{
    public class CommandsViewModel : BaseViewModel
    {
        #region Variables
        private bool isTranscribing = false;
        private SpeechRecognizer SpeachRecognizer;

        public ICommand RecordingCommand { get; set; }

        private string recordingButtonText;
        public string RecordingButtonText
        {
            get => recordingButtonText;
            set => SetProperty(ref recordingButtonText, value);
        }

        private Color recordingButtonColor;
        public Color RecordingButtonColor
        {
            get => recordingButtonColor;
            set => SetProperty(ref recordingButtonColor, value);
        }

        private bool loadingValue;
        public bool LoadingValue
        {
            get => loadingValue;
            set => SetProperty(ref loadingValue, value);
        }

        private bool commandFound;
        public bool CommandFound
        {
            get => commandFound;
            set => SetProperty(ref commandFound, value);
        }

        private string transcriptText;
        public string TranscriptText
        {
            get => transcriptText;
            set => SetProperty(ref transcriptText, value);
        }
        private string recognizedText;
        public string RecognizedText
        {
            get => recognizedText;
            set => SetProperty(ref recognizedText, value);
        }

        private string imageUrl;
        public string ImageUrl
        {
            get => imageUrl;
            set => SetProperty(ref imageUrl, value);
        }
        #endregion

        public CommandsViewModel()
        {
            Title = Resx.AppResources.Tab2Name;

            RecordingCommand = new Command(async () => await StartRecordingAsync());
            RecordingButtonText = Resx.AppResources.CommandInitialText;
            RecordingButtonColor = Color.Blue;
            TranscriptText = Resx.AppResources.CommandInitialTranscriptText;

            SpeachRecognizer = SpeechService.GetRecognizer();
            SpeachRecognizer.Recognized += async (obj, args) =>
            {
                var textExtracted = args.Result.Text.Replace(".", "");
                if (string.IsNullOrEmpty(textExtracted))
                {
                    return;
                }
                RecognizedText = textExtracted;
                var findElement = await GalleryItemDataStore.SearchCommandAsync(textExtracted);
                if (findElement == null)
                {
                    return;
                }
                UpdateImage(findElement);
                UpdateTranscription(textExtracted);
            };
        }

        private async Task StartRecordingAsync()
        {
            bool isMicEnabled = await MicrophoneService.GetPermissionAsync();

            if (!isMicEnabled)
            {
                UpdateTranscription(Resx.AppResources.CommandPhoneConsent);
                return;
            }

            LoadingValue = true;
            CommandFound = false;
            RecordingButtonColor = Color.Green;
            // initialize speech recognizer 

            if (!isTranscribing)
            {
                await SpeachRecognizer.StartContinuousRecognitionAsync();
                Device.BeginInvokeOnMainThread(() =>
                {
                    RecordingButtonColor = Color.Red;
                    RecordingButtonText = Resx.AppResources.CommandButtonStop;
                    LoadingValue = true;
                    CommandFound = false;
                    TranscriptText = string.Empty;
                    RecognizedText = string.Empty;
                });
            }
            else
            {
                await SpeachRecognizer.StopContinuousRecognitionAsync();
                Device.BeginInvokeOnMainThread(() =>
                {
                    RecordingButtonColor = Color.Green;
                    RecordingButtonText = Resx.AppResources.CommandButtonStart;
                    LoadingValue = false;
                });
            }

            isTranscribing = !isTranscribing;
        }

        void UpdateTranscription(string newText)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (!string.IsNullOrWhiteSpace(newText))
                {
                    TranscriptText = newText;
                }
            });
        }

        void UpdateImage(GalleryItem item)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ImageUrl = item.ImageUrl;
                CommandFound = true;
            });
        }

        public async Task OnAppearing()
        {
        }
    }
}
