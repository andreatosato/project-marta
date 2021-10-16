using ProjectMarta.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectMarta.ViewModels
{
    public class CommandsViewModel : BaseViewModel
    {
        private bool isTranscribing = false;

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

        private string imageUrl;
        public string ImageUrl
        {
            get => imageUrl;
            set => SetProperty(ref imageUrl, value);
        }

        public CommandsViewModel()
        {
            Title = "Commands";
            RecordingCommand = new Command(async () => await StartRecordingAsync());
            RecordingButtonText = "Premi e parla ;-)";
            RecordingButtonColor = Color.Blue;
            TranscriptText = "Dimmi qualcosa!";
        }

        private async Task StartRecordingAsync()
        {
            bool isMicEnabled = await MicrophoneService.GetPermissionAsync();

            if (!isMicEnabled)
            {
                UpdateTranscription("Please grant access to the microphone!");
                return;
            }

            LoadingValue = true;
            CommandFound = false;
            RecordingButtonColor = Color.Green;
            RecordingButtonText = "Ora puoi parlare!";
            TranscriptText = string.Empty;
            // initialize speech recognizer 
            var recognizer = SpeechService.GetRecognizer();
            recognizer.Recognized += async (obj, args) =>
            {
                var textExtracted = args.Result.Text.Replace(".", "");
                if (string.IsNullOrEmpty(textExtracted))
                {
                    return;
                }

                var findElement = await GalleryItemDataStore.SearchCommandAsync(textExtracted);
                if (findElement == null)
                {
                    return;
                }
                UpdateImage(findElement);
                UpdateTranscription(textExtracted);
            };
            if (!isTranscribing)
            {
                await recognizer.StartContinuousRecognitionAsync();
                Device.BeginInvokeOnMainThread(() =>
                {
                    RecordingButtonColor = Color.Red;
                    RecordingButtonText = "Stop";
                    LoadingValue = true;
                });
            }
            else
            {
                await recognizer.StopContinuousRecognitionAsync();
                Device.BeginInvokeOnMainThread(() =>
                {
                    RecordingButtonColor = Color.Green;
                    RecordingButtonText = "Start";
                    LoadingValue = false;
                    CommandFound = false;
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
