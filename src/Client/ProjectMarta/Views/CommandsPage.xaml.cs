using ProjectMarta.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectMarta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommandsPage : ContentPage
    {
        readonly CommandsViewModel _viewModel;
        public CommandsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CommandsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}