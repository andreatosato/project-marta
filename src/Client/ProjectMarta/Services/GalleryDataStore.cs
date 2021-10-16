using ProjectMarta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMarta.Services
{
    public class GalleryDataStore : IDataStore<GalleryItem>
    {
        readonly List<GalleryItem> galleryItems;

        public GalleryDataStore()
        {
            galleryItems = new List<GalleryItem>()
            {
                new GalleryItem { Id= "1", Name = "Accendere la luce", Details="Accendere la luce", TextToSpeach="Accendi la luce", ImageUrl="https://projectmarta.blob.core.windows.net/images/Commands/turnonthelight.gif" },
                new GalleryItem { Id= "2", Name = "Spegnere la luce", Details="Spegnere la luce", TextToSpeach="Spegni la luce", ImageUrl="https://projectmarta.blob.core.windows.net/images/Commands/turnoffthelight.jpg" },
                new GalleryItem { Id= "3", Name = "Mangiare", Details="Vorrei mangiare", TextToSpeach="Vorrei mangiare", ImageUrl="https://projectmarta.blob.core.windows.net/images/Commands/mangiare.jpg" },
                new GalleryItem { Id= "4", Name = "Uscire fuori", Details="Uscire fuori", TextToSpeach="Vorrei uscire fuori", ImageUrl="https://projectmarta.blob.core.windows.net/images/Commands/letsgoout.jpg" },
                new GalleryItem { Id= "5", Name = "Alzarsi", Details="Alzarsi", TextToSpeach="Vorrei alzarmi", ImageUrl="https://projectmarta.blob.core.windows.net/images/Commands/getup.gif" },
                new GalleryItem { Id= "6", Name = "Alzare il volume", Details="Alzare il volume", TextToSpeach="Alzare il volume", ImageUrl="https://projectmarta.blob.core.windows.net/images/Commands/alzare%20il%20volume.jpg" },
                new GalleryItem { Id= "7", Name = "Abbassare il volume", Details="Abbassare il volume", TextToSpeach="Abbassare il volume", ImageUrl="https://projectmarta.blob.core.windows.net/images/Commands/abbassare%20il%20volume.png" },
                new GalleryItem { Id= "8", Name = "50", Details="Cinquanta", TextToSpeach="50", ImageUrl="https://projectmarta.blob.core.windows.net/images/Gallery/cinquanta.jpg" },
                new GalleryItem { Id= "9", Name = "Luna", Details="Luna", TextToSpeach="Luna", ImageUrl="https://projectmarta.blob.core.windows.net/images/Gallery/luna.jpg" },
                new GalleryItem { Id= "10", Name = "Gatto", Details="Gatto", TextToSpeach="Gatto", ImageUrl="https://projectmarta.blob.core.windows.net/images/Gallery/gatti.jpg" },
                new GalleryItem { Id= "11", Name = "Latte", Details="Latte", TextToSpeach="Latte", ImageUrl="https://projectmarta.blob.core.windows.net/images/Gallery/latte.jpg" },
                new GalleryItem { Id= "12", Name = "Rock", Details="Rock", TextToSpeach="Rock", ImageUrl="https://projectmarta.blob.core.windows.net/images/Gallery/rock.jpg" },
                new GalleryItem { Id= "13", Name = "Tetti", Details="Tetti", TextToSpeach="Tetti", ImageUrl="https://projectmarta.blob.core.windows.net/images/Gallery/tetti.jpg" },
            };
        }

        public async Task<bool> AddItemAsync(GalleryItem item)
        {
            galleryItems.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(GalleryItem item)
        {
            var oldItem = galleryItems.Where((GalleryItem arg) => arg.Id == item.Id).FirstOrDefault();
            galleryItems.Remove(oldItem);
            galleryItems.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = galleryItems.Where((GalleryItem arg) => arg.Id == id).FirstOrDefault();
            galleryItems.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<GalleryItem> GetItemAsync(string id)
        {
            return await Task.FromResult(galleryItems.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<GalleryItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(galleryItems);
        }

        public async Task<GalleryItem> SearchCommandAsync(string command)
        {
            return await Task.FromResult(galleryItems.FirstOrDefault(t => string.Compare(t.Name, command, StringComparison.InvariantCultureIgnoreCase) == 0));
        }
    }
}