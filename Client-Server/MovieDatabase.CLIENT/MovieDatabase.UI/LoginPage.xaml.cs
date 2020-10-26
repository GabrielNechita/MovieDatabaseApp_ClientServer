using MovieDatabase.UI.ViewModels;
using System.Windows;

namespace MovieDatabase.UI
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage
    {
        private readonly LoginViewModel _loginViewModel;

        public LoginPage()
        {
            InitializeComponent();

            _loginViewModel = new LoginViewModel();

            DataContext = _loginViewModel;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            var role = _loginViewModel.GetLogInUserRole();

            if (role == "Admin")
            {
                NavigationService?.Navigate(new AdminWorkspace());
            }

            if(role == "User")
            {
                NavigationService?.Navigate(new UserWorkspace());
            }
        }

       
    }
}