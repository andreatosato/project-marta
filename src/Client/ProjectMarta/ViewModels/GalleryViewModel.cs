
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
        public ObservableCollection<GalleryItem> GalleryItems { get; }
        public Command<GalleryItem> ItemTapped { get; }
        private bool IsPlaying = false;

        public GalleryViewModel()
        {
            Title = "Gallery";
            GalleryItems = new ObservableCollection<GalleryItem>();

            ItemTapped = new Command<GalleryItem>(OnItemSelected);
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

        async void OnItemSelected(GalleryItem item)
        {
            if (item == null || IsPlaying)
            {
                return;
            }
            IsPlaying = true;
            await SpeechService.SpeechAsync(item.TextToSpeach);
            IsPlaying = false;
        }
    }
}