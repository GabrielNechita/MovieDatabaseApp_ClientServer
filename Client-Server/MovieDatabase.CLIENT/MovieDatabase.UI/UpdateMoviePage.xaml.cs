using MovieDatabase.UI.ViewModels;
using System.Windows;

namespace MovieDatabase.UI
{
    /// <summary>
    /// Interaction logic for UpdateMoviePage.xaml
    /// </summary>
    public partial class UpdateMoviePage
    {
        private readonly AdminViewModel _adminViewModel;

        public UpdateMoviePage(AdminViewModel adminViewModel)
        {
            InitializeComponent();

            _adminViewModel = adminViewModel;

            DataContext = _adminViewModel;
        }

        private void UpdateMovie(object sender, RoutedEventArgs e)
        {
            _adminViewModel.UpdateMovie();

            NavigationService?.Navigate(new AdminWorkspace());
        }
    }
}
