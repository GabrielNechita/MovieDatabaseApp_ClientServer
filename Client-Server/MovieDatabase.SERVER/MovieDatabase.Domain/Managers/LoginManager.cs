using MovieDatabase.DataAccess.Repository;
using System.Text;

namespace MovieDatabase.Domain.Managers
{
    public class LoginManager
    {
        private readonly IUserRepository _userRepository;

        public LoginManager(IUserRepository userRepository)
        {        
            _userRepository = userRepository;
        }

        public byte[] GetLoggedInUserRole(string[] userArray)
        {
            string role = "";

            var user = _userRepository.GetUserByUsername(userArray[1]);
            if (user != null && userArray[1] == user.Username && userArray[2] == user.Password)
            {
                role = user.Role;

                byte[] data = Encoding.ASCII.GetBytes(user.Id + ";" + role);
                return data;
            }

            return new byte[1];
            
        }
    }
}
