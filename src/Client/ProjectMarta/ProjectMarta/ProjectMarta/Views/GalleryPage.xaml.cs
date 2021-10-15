using ProjectMarta.Models;
using ProjectMarta.ViewModels;
using ProjectMarta.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectMarta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GalleryPage : ContentPage
    {
        GalleryViewModel _viewModel;

        public GalleryPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new GalleryViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}