using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using MovieDatabase.DataAccess;
using MovieDatabase.DataAccess.Repository;
using MovieDatabase.Domain.Managers;
using MovieDatabase.Model;
using Newtonsoft.Json;

namespace MovieDatabase.Server
{
    public class Program
    {
        private static readonly Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> ClientSockets = new List<Socket>();
        private const int BUFFER_SIZE = 2048;
        private const int PORT = 100;
        private static readonly byte[] Buffer = new byte[BUFFER_SIZE];

        private static LoginManager _loginManager;
        private static MoviesManager _movieManager;
        private static UserManager _userManager;
        private static ScoreManager _scoreManager;
        private static FavoritesManager _favoritesManager;
        private static ReviewsManager _reviewsManager;
        private static WatchedManager _watchedManager;
        private static MovieDatabaseContext _movieDatabaseContext;
        private static IConnection _connection;

        static void Main()
        {
            _movieDatabaseContext = new MovieDatabaseContext();
            //Load Managers
            _loginManager = new LoginManager(new UserRepository(_movieDatabaseContext));
            _movieManager = new MoviesManager(new MovieRepository(_movieDatabaseContext));
            _userManager = new UserManager(new UserRepository(_movieDatabaseContext));
            _scoreManager = new ScoreManager(new ScoreRepository(_movieDatabaseContext));
            _favoritesManager = new FavoritesManager(new FavoriteRepository(_movieDatabaseContext));
            _reviewsManager = new ReviewsManager(new ReviewRepository(_movieDatabaseContext));
            _watchedManager = new WatchedManager(new WatchedRepository(_movieDatabaseContext));
            _connection = new Connection();

            Console.Title = "Server";
            SetupServer();
            Console.ReadLine(); 
            CloseAllSockets();
        }

        private static void SetupServer()
        {
            Console.WriteLine("Setting up server...");
            ServerSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            ServerSocket.Listen(0);
            ServerSocket.BeginAccept(AcceptCallback, null);
            Console.WriteLine("Server setup complete");
        }

        private static void CloseAllSockets()
        {
            foreach (Socket socket in ClientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            ServerSocket.Close();
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            Socket socket = _connection.AcceptCallback(ar, ServerSocket);

            ClientSockets.Add(socket);
            socket.BeginReceive(Buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            Console.WriteLine("Client connected, waiting for request...");
            ServerSocket.BeginAccept(AcceptCallback, null);
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {

            Socket current = (Socket)ar.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(ar);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                current.Close();
                ClientSockets.Remove(current);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(Buffer, recBuf, received);

            string request = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine("Received Request: " + request); 

            var dataArray = request.Split(';');
            var instruction = dataArray[0];

            switch (instruction)
            {
                case "login":
                    {
                        var data = _loginManager.GetLoggedInUserRole(dataArray);
                        current.Send(data);
                        break;
                    }
                case "getMovies":
                    {
                        var movies = _movieManager.GetMovies();
                        var jsonMoviesSerialized = JsonConvert.SerializeObject(movies, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        byte[] data = Encoding.ASCII.GetBytes(jsonMoviesSerialized);

                        current.Send(data);
                        break;
                    }
                case "getUsers":
                    {
                        var users = _userManager.GetUsers();
                        var jsonUsersSerialized = JsonConvert.SerializeObject(users, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        byte[] data = Encoding.ASCII.GetBytes(jsonUsersSerialized);

                        current.Send(data);
                        break;
                    }
                case "addMovie":
                    {
                        
                        var newMovie = JsonConvert.DeserializeObject<Movie>(dataArray[1],
                            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        _movieManager.AddMovie(newMovie);

                        byte[] data = Encoding.ASCII.GetBytes("Ok");

                        current.Send(data);
                        break;
                    }
                case "addUser":
                    {
                        
                        var newUser = JsonConvert.DeserializeObject<User>(dataArray[1],
                            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        _userManager.AddUser(newUser);

                        byte[] data = Encoding.ASCII.GetBytes("Ok");

                        current.Send(data);
                        break;
                    }
                case "deleteMovie":
                    {
                        int.TryParse(dataArray[1], out int movieId);

                        _movieManager.DeleteMovie(movieId);

                        byte[] data = Encoding.ASCII.GetBytes("Ok");

                        current.Send(data);
                        break;
                    }
                case "deleteUser":
                    {

                        int.TryParse(dataArray[1], out int userId);

                        _userManager.DeleteUser(userId);

                        byte[] data = Encoding.ASCII.GetBytes("Ok");

                        current.Send(data);
                        break;
                    }
                case "updateMovie":
                    {
                        
                        var movie = JsonConvert.DeserializeObject<Movie>(dataArray[1],
                            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        _movieManager.UpdateMovie(movie);

                        byte[] data = Encoding.ASCII.GetBytes("Ok");

                        current.Send(data);
                        break;
                    }
                case "updateUser":
                    {
                        
                        var user = JsonConvert.DeserializeObject<User>(dataArray[1],
                            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        _userManager.UpdateUser(user);

                        byte[] data = Encoding.ASCII.GetBytes("Ok");

                        current.Send(data);
                        break;
                    }
                case "addScoreToMovie":
                    {
                        
                        var selectedMovie = JsonConvert.DeserializeObject<Movie>(dataArray[1],
                            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        int.TryParse(dataArray[2], out int userId);
                        int.TryParse(dataArray[3], out int scoreValue);

                        _scoreManager.AddScore(userId, selectedMovie.Id, scoreValue);

                        var calculatedScore = _scoreManager.GetMovieScore(selectedMovie.Id);


                        _movieManager.SaveRating(selectedMovie, calculatedScore);

                        byte[] data = Encoding.ASCII.GetBytes("Ok");

                        current.Send(data);

                        break;
                    }
                case "getScore":
                    {
                        int.TryParse(dataArray[1], out int movieId);
                        int.TryParse(dataArray[2], out int userId);

                        var score = _scoreManager.GetScore(movieId, userId);
                        var jsonScoreSerialized = JsonConvert.SerializeObject(score, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        byte[] data = Encoding.ASCII.GetBytes(jsonScoreSerialized);

                        current.Send(data);

                        break;
                    }
                case "getFavoriteMovies":
                    {
                        int.TryParse(dataArray[1], out int userId);

                        var favorites = _favoritesManager.GetFavorites(userId);
                        var jsonFavoritesSerialized = JsonConvert.SerializeObject(favorites, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        byte[] data = Encoding.ASCII.GetBytes(jsonFavoritesSerialized);

                        current.Send(data);
                        break;
                    }
                case "addFavoriteMovie":
                    {
                       
                        var favorite = JsonConvert.DeserializeObject<Favorite>(dataArray[1],
                            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        _favoritesManager.AddFavorite(favorite);

                        byte[] data = Encoding.ASCII.GetBytes("Ok");

                        current.Send(data);
                        break;
                    }
                case "removeFavoriteMovie":
                    {

                        int.TryParse(dataArray[1], out int favoriteMovieId);

                        _favoritesManager.RemoveFavorite(favoriteMovieId);

                        byte[] data = Encoding.ASCII.GetBytes("Ok");

                        current.Send(data);
                        break;
                    }
                case "getMovieReviews":
                    {
                        int.TryParse(dataArray[1], out int movieId);

                        var reviews = _reviewsManager.GetReviews(movieId);
                        var jsonReviewsSerialized = JsonConvert.SerializeObject(reviews, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        byte[] data = Encoding.ASCII.GetBytes(jsonReviewsSerialized);

                        current.Send(data);
                        break;
                    }
                case "addMovieReview":
                    {
                       
                        var review = JsonConvert.DeserializeObject<Review>(dataArray[1],
                            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        _reviewsManager.AddReview(review);

                        byte[] data = Encoding.ASCII.GetBytes("Ok");

                        current.Send(data);
                        break;
                    }

                case "AddReviewThumb":
                    {
                        var thumb = JsonConvert.DeserializeObject<Thumb>(dataArray[1],
                           new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        _reviewsManager.AddThumb(thumb);

                        byte[] data = Encoding.ASCII.GetBytes("Ok");

                        current.Send(data);
                        break;
                    }
                
                case "getReviewByMovieIdUserId":
                    {
                        int.TryParse(dataArray[1], out int movieId);
                        int.TryParse(dataArray[2], out int userId);

                        var review = _reviewsManager.GetReviewByMovieIdUserId(movieId, userId);
                        var jsonReviewSerialized = JsonConvert.SerializeObject(review, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        byte[] data = Encoding.ASCII.GetBytes(jsonReviewSerialized);

                        current.Send(data);
                        break;
                    }
                case "editMovieReview":
                    {
                        
                        var review = JsonConvert.DeserializeObject<Review>(dataArray[1],
                            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        _reviewsManager.UpdateReview(review);

                        byte[] data = Encoding.ASCII.GetBytes("Ok");

                        current.Send(data);
                        break;
                    }
                case "getThumb":
                    {
                        int.TryParse(dataArray[1], out int reviewId);
                        int.TryParse(dataArray[2], out int userId);

                        var thumb = _reviewsManager.GetThumbByReviewIdUserId(reviewId, userId, dataArray[3]);
                        var jsonThumbSerialized = JsonConvert.SerializeObject(thumb, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        byte[] data = Encoding.ASCII.GetBytes(jsonThumbSerialized);

                        current.Send(data);
                        break;
                    }
                case "DeleteReviewThumb":
                    {

                        int.TryParse(dataArray[1], out int thumbId);

                        _reviewsManager.DeleteReviewThumb(thumbId);

                        byte[] data = Encoding.ASCII.GetBytes("Ok");

                        current.Send(data);
                        break;
                    }
                case "getWatchedMovies":
                    {
                        int.TryParse(dataArray[1], out int userId);

                        var watchedMovies = _watchedManager.GetWatchedMovies(userId);
                        var jsonWatchedMoviesSerialized = JsonConvert.SerializeObject(watchedMovies, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        byte[] data = Encoding.ASCII.GetBytes(jsonWatchedMoviesSerialized);

                        current.Send(data);
                        break;
                    }
                case "addWatchedMovie":
                    {
                       
                        var watchedMovie = JsonConvert.DeserializeObject<WatchedMovie>(dataArray[1],
                            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        _watchedManager.AddWatched(watchedMovie);

                        byte[] data = Encoding.ASCII.GetBytes("Ok");

                        current.Send(data);
                        break;
                    }
                case "removeWatchedMovie":
                    {

                        int.TryParse(dataArray[1], out int watchedMovieId);

                        _watchedManager.RemoveWatched(watchedMovieId);

                        byte[] data = Encoding.ASCII.GetBytes("Ok");

                        current.Send(data);
                        break;
                    }
                case "getPersonalizedMovies":
                    {
                        var watchedMovies = JsonConvert.DeserializeObject<List<WatchedMovie>>(dataArray[1],
                            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        var personalizedMovies = _movieManager.GetPersonalizedMovies(watchedMovies);

                        var jsonPersonalizedMoviesSerialized = JsonConvert.SerializeObject(personalizedMovies, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                        byte[] data = Encoding.ASCII.GetBytes(jsonPersonalizedMoviesSerialized);

                        current.Send(data);
                        break;
                    }

            }

            current.BeginReceive(Buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }
    }
}
