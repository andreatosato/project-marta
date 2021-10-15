using ProjectMarta.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjectMarta.ViewModels
{
    public class GalleryViewModel : BaseViewModel
    {
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
                return;

            //TODO: Speach-To-Text
            
        }
    }
}