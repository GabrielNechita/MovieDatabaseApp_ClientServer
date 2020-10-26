using MovieDatabase.UI.ViewModels;
using System.Windows;

namespace MovieDatabase.UI
{
    /// <summary>
    /// Interaction logic for ReviewsPage.xaml
    /// </summary>
    public partial class ReviewsPage
    {
        private ReviewViewModel _reviewViewModel;

        private int _movieId;

        public ReviewsPage(int movieId)
        {
            InitializeComponent();

            _movieId = movieId;

            LoadReviews();

        }

        private void LoadReviews()
        {
            _reviewViewModel = new ReviewViewModel(_movieId);

            RefreshMovieList();

            ReviewVisibility();
        }

        private void RefreshMovieList()
        {
            DataContext = null;

            DataContext = _reviewViewModel;

        }

        private void AddReview(object sender, RoutedEventArgs e)
        {
            _reviewViewModel.AddReview(NewReview.Text);

            LoadReviews();
        }

        private void ThumbsUpButton(object sender, RoutedEventArgs e)
        {
            var selectedItem = ReviewsListBox.SelectedItem;

            if (selectedItem != null)
            {
                _reviewViewModel.Thumb("up");
                LoadReviews();
            }
            else
            {
                Window popup = new Window();
                popup.Title = "Warning!";
                popup.Width = 300;
                popup.Height = 150;
                popup.Content = "\n\n\n \t Select a review!";
                popup.ShowDialog();
            }
            
        }

        private void ThumbsDownButton(object sender, RoutedEventArgs e)
        {
            var selectedItem = ReviewsListBox.SelectedItem;

            if (selectedItem != null)
            {
                _reviewViewModel.Thumb("down");
                LoadReviews();
            }
            else
            {
                Window popup = new Window();
                popup.Title = "Warning!";
                popup.Width = 300;
                popup.Height = 150;
                popup.Content = "\n\n\n \t Select a review!";
                popup.ShowDialog();
            }
            
        }

        private void ReviewVisibility()
        {
            AddReviewButton.IsEnabled = _reviewViewModel.MyReview != null ? false : true;           
        }

        private void EditReview(object sender, RoutedEventArgs e)
        {
            _reviewViewModel.EditReview(NewReview.Text);

            LoadReviews();
        }
    }
}
