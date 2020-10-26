using MovieDatabase.Model;
using System.Collections.Generic;

namespace MovieDatabase.DataAccess.Repository
{
    public interface IUserRepository
    {
        List<User> GetUsers();

        User GetUserByUsername(string username);

        void AddUser(User user);

        void UpdateUser(User user);

        void DeleteUser(int userId);

        User GetUserByUserId(int userId);
    }
}