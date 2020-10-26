using MovieDatabase.UI.Socket;

namespace MovieDatabase.UI.ViewModels
{
    public class LoginViewModel
    {

        public string UsernameValue { get; set; }
        public string PasswordValue { get; set; }

        public string GetLogInUserRole()
        {
            var data = SocketHelper.ExecuteRequest("login;" + UsernameValue + ";" + PasswordValue).Split(';');
            if(data.Length == 2)
            {
                int.TryParse(data[0], out int userId);

                LoggedInUser.UserId = userId;

                return data[1];
            }

            return string.Empty;
        }
    }
}
