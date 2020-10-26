using MovieDatabase.Model;

namespace MovieDatabase.UI.ViewModel
{
  public class LoginViewModel
  {
    public string UsernameValue { get; set; }

    public string PasswordValue { get; set; }

    public bool IsValidLogin()
    {
      var user = new User();

      if (UsernameValue == user.Username && PasswordValue == user.Password)
        return true;

      return false;
    }
  }
}
