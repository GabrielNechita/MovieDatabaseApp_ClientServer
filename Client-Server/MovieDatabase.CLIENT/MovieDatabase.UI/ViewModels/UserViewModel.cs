using MovieDatabase.Model;
using MovieDatabase.UI.Socket;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.UI.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            var getMoviesResponse = SocketHelper.ExecuteRequest("getMovies");
            Movies = JsonConvert.DeserializeObject<List<Movie>>(getMoviesResponse,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            var getFavoriteMoviesResponse = SocketHelper.ExecuteRequest("getFavoriteMovies;" + LoggedInUser.UserId);
            FavoriteMoviesList = JsonConvert.DeserializeObject<List<Favorite>>(getFavoriteMoviesResponse,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            var getWatchedMoviesResponse = SocketHelper.ExecuteRequest("getWatchedMovies;" + LoggedInUser.UserId);
            WatchedMoviesList = JsonConvert.DeserializeObject<List<WatchedMovie>>(getWatchedMoviesResponse,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
        }

        public List<Movie> Movies { get; set; }

        public string SearchCriteria { get; set; }

        public string SearchQuery { get; set; }

        public Movie SelectedMovie { get; set; }

        public string SelectedRating { get; set; }

        public List<Favorite> FavoriteMoviesList { get; set; }

        public Favorite SelectedFavoriteMovie { get; set; }

        public List<WatchedMovie> WatchedMoviesList { get; set; }

        public WatchedMovie SelectedWatchedMovie { get; set; }

        public void Search()
        {

            var getMoviesResponse = SocketHelper.ExecuteRequest("getMovies");
            Movies = JsonConvert.DeserializeObject<List<Movie>>(getMoviesResponse,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            switch (SearchCriteria)
            {

                case "Genre":
                    {
                        if (!string.IsNullOrEmpty(SearchQuery))
                        {                   
                            Movies = Movies.Where(x => x.Genre != null && x.Genre.ToLower().StartsWith(SearchQuery.ToLower())).ToList();
                        }
                        break;
                    }
                case "Title":
                    {
                        if (!string.IsNullOrEmpty(SearchQuery))
                        {
                            Movies = Movies.Where(x => x.Title.ToLower().StartsWith(SearchQuery.ToLower())).ToList();
                        }
                        break;
                    }                
            }
        }

        public void SaveRating()
        {
            int.TryParse(SelectedRating, out int score);

            var jsonMovieSerialized = JsonConvert.SerializeObject(SelectedMovie, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            SocketHelper.ExecuteRequest("addScoreToMovie;" + jsonMovieSerialized + ";" + LoggedInUser.UserId + ";" + score );

            var getMoviesResponse = SocketHelper.ExecuteRequest("getMovies");
            Movies = JsonConvert.DeserializeObject<List<Movie>>(getMoviesResponse,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
        }

        public Score RatingVisibilityTest()
        {
            if (SelectedMovie != null)
            {
                var getScoreResponse = SocketHelper.ExecuteRequest("getScore;" + SelectedMovie.Id + ";" + LoggedInUser.UserId);
                var score = JsonConvert.DeserializeObject<Score>(getScoreResponse,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                return score;
            }
            return null;
        }
       
        public void AddFavoriteMovie()
        {
            var favorite = FavoriteMoviesList.FirstOrDefault(f => f.UserId == LoggedInUser.UserId && f.MovieId == SelectedMovie.Id);
            
            if (favorite == null)
            {
                var newFavorite = new Favorite()
                {
                    UserId = LoggedInUser.UserId,
                    MovieId = SelectedMovie.Id,
                    MovieTitle = SelectedMovie.Title
                };

                var jsonFavoriteSerialized = JsonConvert.SerializeObject(newFavorite, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                SocketHelper.ExecuteRequest("addFavoriteMovie;" + jsonFavoriteSerialized);                
            }
        }

        public void RemoveFavoriteMovie()
        {
            SocketHelper.ExecuteRequest("removeFavoriteMovie;" + SelectedFavoriteMovie.Id);            
        }

        public void AddWatchedMovie()
        {
            var watchedMovie = WatchedMoviesList.FirstOrDefault(w => w.UserId == LoggedInUser.UserId && w.MovieId == SelectedMovie.Id);

            if (watchedMovie == null)
            {
                var newWatchedMovie = new WatchedMovie()
                {
                    UserId = LoggedInUser.UserId,
                    MovieId = SelectedMovie.Id,
                    MovieTitle = SelectedMovie.Title
                };

                var jsonWatchedSerialized = JsonConvert.SerializeObject(newWatchedMovie, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                SocketHelper.ExecuteRequest("addWatchedMovie;" + jsonWatchedSerialized);

            }
        }

        public void RemoveWatchedMovie()
        {
            SocketHelper.ExecuteRequest("removeWatchedMovie;" + SelectedWatchedMovie.Id);
        }

        public string GetPersonalizedMovies()
        {
            var jsonWatchedSerialized = JsonConvert.SerializeObject(WatchedMoviesList, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            var getPersonalizedResponse = SocketHelper.ExecuteRequest("getPersonalizedMovies;" + jsonWatchedSerialized);

            var personalizedMovies = JsonConvert.DeserializeObject<List<Movie>>(getPersonalizedResponse,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            string personalizedMoviesString = "";
            foreach (var movie in personalizedMovies)
            {
                personalizedMoviesString += movie.Title;
                personalizedMoviesString += "\n";
            }

            return personalizedMoviesString;
        }
    }
}
