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
                new GalleryItem { Id= "1", Name = "Porta", Details="Porta interna", TextToSpeach="Una porta", ImageUrl="https://t4.ftcdn.net/jpg/00/82/36/09/240_F_82360918_lpY2q2DkAujjNvR9MGnDJEwwUIuHXFW9.jpg" },
                new GalleryItem { Id= "2", Name = "Tavolo", Details="Tavolo da pranzo", TextToSpeach="Un tavolo", ImageUrl="https://t4.ftcdn.net/jpg/02/56/22/93/240_F_256229326_jQA0y6xQrqolau1er4Ii620KBbs6Uwl4.jpg" },
                new GalleryItem { Id= "3", Name = "Bicchiere d'acqua", Details="Bicchiere d'acqua", TextToSpeach="Un bicchiere d'acqua", ImageUrl="https://t4.ftcdn.net/jpg/01/74/18/97/240_F_174189786_bnzrHDTjS09k7a6kSnnYf5xfTcsLUuwv.jpg" },
                new GalleryItem { Id= "4", Name = "", Details="", TextToSpeach="", ImageUrl="" },
                new GalleryItem { Id= "5", Name = "", Details="", TextToSpeach="", ImageUrl="" },
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
    }
}