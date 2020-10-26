using MovieDatabase.Model;
using System.Collections.Generic;

namespace MovieDatabase.UI.Repository
{
    public interface IMovieRepository
    {
        List<Movie> GetMovies();

        void AddMovie(Movie movie);

        void DeleteMovie(int movieId);

        void UpdateMovie(Movie movie);

        void SaveRating(Movie movie, double calculatedMovieScore);
    }
}