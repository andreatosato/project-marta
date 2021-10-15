using Microsoft.CognitiveServices.Speech;
using ProjectMarta.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectMarta.ViewModels
{
    public class GalleryViewModel : BaseViewModel
    {
        private SpeechSynthesizer synthesizer;
        public ObservableCollection<GalleryItem> GalleryItems { get; }

        public Command<Item> ItemTapped { get; }

        public GalleryViewModel()
        {
            Title = "Gallery";
            GalleryItems = new ObservableCollection<GalleryItem>();

            ItemTapped = new Command<Item>(OnItemSelected);
        }

        public async Task OnAppearing()
        {
            await ExecuteLoadItemsCommand();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                GalleryItems.Clear();
                var items = await GalleryItemDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    GalleryItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
            {
                return;
            }

            //TODO: Speech-To-Text
            // initialize speech recognizer 
            if (synthesizer == null)
            {
                var config = SpeechConfig.FromSubscription(Constants.CognitiveServicesApiKey, Constants.CognitiveServicesRegion);
                synthesizer = new SpeechSynthesizer(config);
            }
            var result = await synthesizer.SpeakTextAsync(item.Text);
            var stream = AudioDataStream.FromResult(result);

            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load(new MemoryStream(result.AudioData));
        }
    }
}