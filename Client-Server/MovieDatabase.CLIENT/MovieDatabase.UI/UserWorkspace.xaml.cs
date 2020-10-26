using MovieDatabase.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MovieDatabase.UI
{
    /// <summary>
    /// Interaction logic for UserWorkspace.xaml
    /// </summary>
    public partial class UserWorkspace
    {

        private UserViewModel _userViewModel;                  

        public UserWorkspace()
        {
            InitializeComponent();

            LoadMovies();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            _userViewModel.Search();

            RefreshMovieList();
        }

        private void SaveRating_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = SearchListBox.SelectedItem;

            if (selectedItem != null)
            {
                _userViewModel.SaveRating();
                RefreshMovieList();
            }
            else
            {
                Window popup = new Window();
                popup.Title = "Warning!";
                popup.Width = 300;
                popup.Height = 150;
                popup.Content = "\n\n\n \t Select a movie!";
                popup.ShowDialog();
            }

        }

        private void LoadMovies()
        {
            _userViewModel = new UserViewModel();

            RefreshMovieList();
        }

        private void RefreshMovieList()
        {
            DataContext = null;

            DataContext = _userViewModel;
        }

        private void RatingVisibility(object sender, SelectionChangedEventArgs e)
        {
            RatingButton.IsEnabled = _userViewModel.RatingVisibilityTest() != null ? false : true;
        }

        private void AddFavoriteMovie(object sender, RoutedEventArgs e)
        {
            var selectedItem = SearchListBox.SelectedItem;

            if (selectedItem != null)
            {
                _userViewModel.AddFavoriteMovie();
                LoadMovies();
            }
            else
            {
                Window popup = new Window();
                popup.Title = "Warning!";
                popup.Width = 300;
                popup.Height = 150;
                popup.Content = "\n\n\n \t Select a movie!";
                popup.ShowDialog();
            }
        }

        private void RemoveFavoriteMovie(object sender, RoutedEventArgs e)
        {
            var selectedItem = FavoriteListBox.SelectedItem;

            if (selectedItem != null)
            {
                _userViewModel.RemoveFavoriteMovie();
                LoadMovies();
            }
            else
            {
                Window popup = new Window();
                popup.Title = "Warning!";
                popup.Width = 300;
                popup.Height = 150;
                popup.Content = "\n\n\n \t Select Favorite!";
                popup.ShowDialog();
            }
            
            
        }

        private void ToReviewsPage(object sender, RoutedEventArgs e)
        {
            var selectedItem = SearchListBox.SelectedItem;

            if (selectedItem != null)
            {
                
                NavigationService?.Navigate(new ReviewsPage(_userViewModel.SelectedMovie.Id));
            }
            else
            {
                Window popup = new Window();
                popup.Title = "Warning!";
                popup.Width = 300;
                popup.Height = 150;
                popup.Content = "\n\n\n \t Select a movie!";
                popup.ShowDialog();
            }
        }

        private void AddWatchedMovie(object sender, RoutedEventArgs e)
        {
            var selectedItem = SearchListBox.SelectedItem;

            if (selectedItem != null)
            {
                _userViewModel.AddWatchedMovie();
                LoadMovies();
            }
            else
            {
                Window popup = new Window();
                popup.Title = "Warning!";
                popup.Width = 300;
                popup.Height = 150;
                popup.Content = "\n\n\n \t Select a movie!";
                popup.ShowDialog();
            }
        }

        private void RemoveWatchedMovie(object sender, RoutedEventArgs e)
        {
            var selectedItem = WatchedListBox.SelectedItem;

            if (selectedItem != null)
            {
                _userViewModel.RemoveWatchedMovie();
                LoadMovies();
            }
            else
            {
                Window popup = new Window();
                popup.Title = "Warning!";
                popup.Width = 300;
                popup.Height = 150;
                popup.Content = "\n\n\n \t Select Watched Movie!";
                popup.ShowDialog();
            }

        }

        private void PersonalizedMovies(object sender, RoutedEventArgs e)
        {
            Window popup = new Window();
            popup.Title = "Personalized Movies!";
            popup.Width = 300;
            popup.Height = 150;
            popup.Content = _userViewModel.GetPersonalizedMovies();
            popup.ShowDialog();
        }

        
    }
}