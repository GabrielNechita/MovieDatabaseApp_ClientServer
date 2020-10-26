using MovieDatabase.UI.ViewModels;
using System;
using System.Windows;

namespace MovieDatabase.UI
{
    /// <summary>
    /// Interaction logic for AdminWorkspace.xaml
    /// </summary>
    public partial class AdminWorkspace 
    {
        //private MovieDatabaseContext _movieDbContext;
        private AdminViewModel _adminViewModel;

        public AdminWorkspace()
        {
            InitializeComponent();

            LoadMovieList();
        }

        private void AddMovie_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddMoviePage(_adminViewModel));
        }

        private void UpdateMovie_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = MoviesListBox.SelectedItem;

            if (selectedItem != null)
            {
                NavigationService?.Navigate(new UpdateMoviePage(_adminViewModel));
            }
            else
            {
                Window popup = new Window();
                popup.Title = "Warning!";
                popup.Width = 300;
                popup.Height = 150;
                popup.Content = "\n\n\n \t Select Movie!";
                popup.ShowDialog();
            }                   
        }

        private void DeleteMovie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = MoviesListBox.SelectedItem.GetType().GetProperty("Id");
                var id = (int)selectedItem.GetValue(MoviesListBox.SelectedItem, null);

                _adminViewModel.DeleteMovie(id);

                LoadMovieList();
            }
            catch (NullReferenceException)
            {

                Window popup = new Window();
                popup.Title = "Warning!";
                popup.Width = 300;
                popup.Height = 150;
                popup.Content = "\n\n\n \t Select Movie!";
                popup.ShowDialog();
            }
            
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddUserPage(_adminViewModel));
        }

        private void UpdateUser_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = UsersListBox.SelectedItem;

            if (selectedItem != null)
            {
                NavigationService?.Navigate(new UpdateUserPage(_adminViewModel));
            }
            else
            {
                Window popup = new Window();
                popup.Title = "Warning!";
                popup.Width = 300;
                popup.Height = 150;
                popup.Content = "\n\n\n \t Select User!";
                popup.ShowDialog();
            }           
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = UsersListBox.SelectedItem.GetType().GetProperty("Id");
                var id = (int)selectedItem.GetValue(UsersListBox.SelectedItem, null);

                _adminViewModel.DeleteUser(id);

                LoadMovieList();
            }
            catch (NullReferenceException)
            {

                Window popup = new Window();
                popup.Title = "Warning!";
                popup.Width = 300;
                popup.Height = 150;
                popup.Content = "\n\n\n \t Select User!";
                popup.ShowDialog();
            }
            
        }

        private void LoadMovieList()
        {
            _adminViewModel = new AdminViewModel();

            DataContext = _adminViewModel;
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            _adminViewModel.GenerateReport(ReportType.Text);
        }


    }
}
