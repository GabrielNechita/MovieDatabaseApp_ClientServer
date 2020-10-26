using MovieDatabase.UI.ViewModels;
using System.Windows;

namespace MovieDatabase.UI
{
    /// <summary>
    /// Interaction logic for UpdateUserPage.xaml
    /// </summary>
    public partial class UpdateUserPage
    {
        private readonly AdminViewModel _adminViewModel;
        public UpdateUserPage(AdminViewModel adminViewModel)
        {
            InitializeComponent();

            _adminViewModel = adminViewModel;

            DataContext = _adminViewModel;
        }

        private void UpdateUser_Button(object sender, RoutedEventArgs e)
        {
            _adminViewModel.UpdateUser();
            NavigationService?.Navigate(new AdminWorkspace());
        }
    }
}
