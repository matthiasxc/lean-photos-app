using CommunityToolkit.Mvvm.DependencyInjection;
using leanPhotos.Logic.ViewModels;
using Windows.UI.Xaml.Controls;

namespace LeanPhotos
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<MainPageViewModel>();

            ViewModel.InitPhotosAsyncCommand.Execute(null);
        }

        public MainPageViewModel ViewModel => (MainPageViewModel)DataContext;

        private void GridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.IsItemSelected = e.AddedItems.Count > 0;
        }
    }
}
