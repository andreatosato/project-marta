using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjectMarta.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = Resx.AppResources.Tab3Name;
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/project-marta"));
        }

        public ICommand OpenWebCommand { get; }
    }
}