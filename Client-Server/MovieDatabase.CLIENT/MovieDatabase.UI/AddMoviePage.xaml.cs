using MovieDatabase.UI.ViewModels;
using System.Windows;

namespace MovieDatabase.UI
{
    /// <summary>
    /// Interaction logic for AddMoviePage.xaml
    /// </summary>
    public partial class AddMoviePage
    {
        private readonly AdminViewModel _adminViewModel;

        public AddMoviePage(AdminViewModel adminViewModel)
        {
            InitializeComponent();
            _adminViewModel = adminViewModel;
        }

        private void AddMovie(object sender, RoutedEventArgs e)
        {
            _adminViewModel.AddMovie(Title.Text, Genre.Text, Actors.Text, Status.Text);
            NavigationService?.Navigate(new AdminWorkspace());
        }
    }
}
