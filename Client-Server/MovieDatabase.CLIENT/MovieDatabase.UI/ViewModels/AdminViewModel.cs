using MovieDatabase.Model;
using System.Collections.Generic;
using MovieDatabase.UI.Socket;
using Newtonsoft.Json;

namespace MovieDatabase.UI.ViewModels
{
    public class AdminViewModel
    {
        public AdminViewModel()
        {
            var getMoviesResponse = SocketHelper.ExecuteRequest("getMovies");
            Movies = JsonConvert.DeserializeObject<List<Movie>>(getMoviesResponse,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            var getUsersResponse = SocketHelper.ExecuteRequest("getUsers");
            Users = JsonConvert.DeserializeObject<List<User>>(getUsersResponse,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
        }

        public List<Movie> Movies { get; set; }
        public List<User> Users { get; set; }

        public Movie SelectedMovie { get; set; }
        public User SelectedUser { get; set; }

        public void AddMovie(string title, string genre, string actors, string status)
        {
            Movie newMovie = new Movie
            {
                Title = title,
                Genre = genre,
                Actors = actors,
                Status = status
            };

            var jsonMovieSerialized = JsonConvert.SerializeObject(newMovie, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            SocketHelper.ExecuteRequest("addMovie;" + jsonMovieSerialized);
        }

        public void AddUser(string username, string password, string role)
        {
            User newUser = new User
            {
                Username = username,
                Password = password,
                Role = role
            };

            var jsonUserSerialized = JsonConvert.SerializeObject(newUser, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            SocketHelper.ExecuteRequest("addUser;" + jsonUserSerialized);
        }

        public void DeleteUser(int userId)
        {
            SocketHelper.ExecuteRequest("deleteUser;" + userId);            
        }

        public void DeleteMovie(int movieId)
        {
            SocketHelper.ExecuteRequest("deleteMovie;" + movieId);
        }

        public void UpdateMovie()
        {
            var jsonMovieSerialized = JsonConvert.SerializeObject(SelectedMovie, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            SocketHelper.ExecuteRequest("updateMovie;" + jsonMovieSerialized);
           
        }

        public void UpdateUser()
        {
            var jsonUserSerialized = JsonConvert.SerializeObject(SelectedUser, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            SocketHelper.ExecuteRequest("updateUser;" + jsonUserSerialized);
            
        }

        public void GenerateReport(string reportType)
        {
            var reportFactory = new ReportFactory();
            var report = reportFactory.GetReport(reportType);
            report.GenerateReport(Movies);

        }
    }
}