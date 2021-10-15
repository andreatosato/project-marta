using ProjectMarta.Services;
using ProjectMarta.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectMarta
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<GalleryDataStore>();
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
