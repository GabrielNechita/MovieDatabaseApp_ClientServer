using MovieDatabase.UI.ViewModels;
using System.Windows;

namespace MovieDatabase.UI
{
    /// <summary>
    /// Interaction logic for AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage
    {
        private readonly AdminViewModel _adminViewModel;

        public AddUserPage(AdminViewModel adminViewModel)
        {
            InitializeComponent();
            _adminViewModel = adminViewModel;
        }

        private void AddUser_Button(object sender, RoutedEventArgs e)
        {
            _adminViewModel.AddUser(Username.Text, Password.Text, Role.Text);
            NavigationService?.Navigate(new AdminWorkspace());
        }
    }
}