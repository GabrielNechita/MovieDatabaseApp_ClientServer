using MovieDatabase.Model;
using System.Collections.Generic;

namespace MovieDatabase.DataAccess.Repository
{
    public interface IMovieRepository
    {
        List<Movie> GetMovies();

        void AddMovie(Movie movie);

        void DeleteMovie(int movieId);

        void UpdateMovie(Movie movie);

        void SaveRating(Movie movie, double calculatedMovieScore);

        string GetGenreByMovieId(int movieId);

        List<Movie> GetMoviesByGenre(string genre);
    }
}