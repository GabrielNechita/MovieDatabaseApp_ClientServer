using System.Collections.Generic;
using MovieDatabase.DataAccess.Repository;
using MovieDatabase.Model;

namespace MovieDatabase.Domain.Managers
{
    public class MoviesManager
    {
        private readonly IMovieRepository _movieRepository;
        public MoviesManager(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public IList<Movie> GetMovies()
        {
            return _movieRepository.GetMovies();
        }

        public void AddMovie(Movie newMovie)
        {
            _movieRepository.AddMovie(newMovie);
        }

        public void DeleteMovie(int movieId)
        {
            _movieRepository.DeleteMovie(movieId);
        }

        public void UpdateMovie(Movie movie)
        {
            _movieRepository.UpdateMovie(movie);
        }

        public void SaveRating(Movie movie, double calculatedScore)
        {
            _movieRepository.SaveRating(movie, calculatedScore);
        }

        public List<Movie> GetPersonalizedMovies(List<WatchedMovie> watchedMovies)
        {
            var personalizedMovies = new List<Movie>();

            foreach (var watched in watchedMovies)
            {
                var genre = _movieRepository.GetGenreByMovieId(watched.MovieId);

                var moviesByGenre = _movieRepository.GetMoviesByGenre(genre);

                foreach (var movie in moviesByGenre)
                {
                    personalizedMovies.Add(movie);
                }

            }

            return personalizedMovies;
        }
    }
}
