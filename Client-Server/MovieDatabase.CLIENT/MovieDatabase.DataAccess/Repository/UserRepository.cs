using MovieDatabase.Model;
using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private MovieDatabaseContext _context;

        public UserRepository(MovieDatabaseContext context)
        {
            _context = context;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(x => x.Username == username);
        }

        public User GetUserByUserId(int userId)
        {
            return _context.Users.FirstOrDefault(x => x.Id == userId);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);

            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (dbUser != null)
            {               
                dbUser.Username = user.Username;
                dbUser.Password = user.Password;
                dbUser.Role = user.Role;

                _context.SaveChanges();
            }
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }


    }
}