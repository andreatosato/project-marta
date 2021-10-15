using ProjectMarta.Models;
using ProjectMarta.Services;
using Xamarin.Forms;

namespace ProjectMarta
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<IDataStore<GalleryItem>, GalleryDataStore>();
            DependencyService.Register<IMicrophoneService, MicrophoneService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
