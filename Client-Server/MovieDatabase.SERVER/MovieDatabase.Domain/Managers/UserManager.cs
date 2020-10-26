using System.Collections.Generic;
using MovieDatabase.DataAccess.Repository;
using MovieDatabase.Model;

namespace MovieDatabase.Domain.Managers
{
    public class UserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IList<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public void AddUser(User newUser)
        {
            _userRepository.AddUser(newUser);
        }

        public void DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }
    }
}
