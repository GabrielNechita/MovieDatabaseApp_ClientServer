
using MovieDatabase.DataAccess;
using MovieDatabase.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.UI.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDatabaseContext _context;

        public MovieRepository(MovieDatabaseContext context)
        {
            _context = context;
        }

        public List<Movie> GetMovies()
        {
            return _context.Movies.ToList();
        }

        public void AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);

            _context.SaveChanges();
        }

        public void DeleteMovie(int movieId)
        {
            var movieToDelete = _context.Movies.FirstOrDefault(movie => movie.Id == movieId);
            if (movieToDelete != null)
            {
                _context.Movies.Remove(movieToDelete);

                _context.SaveChanges();
            }
        }

        public void UpdateMovie(Movie movie)
        {
            var dbMovie = _context.Movies.FirstOrDefault(m => m.Id == movie.Id);
            if (dbMovie != null)
            {
                dbMovie.Title = movie.Title;
                dbMovie.Actors = movie.Actors;
                dbMovie.Genre = movie.Genre;
                dbMovie.Status = movie.Status;

                _context.SaveChanges();
            }
        }

        public void SaveRating(Movie movie, double calculatedMovieScore)
        {
            var dbMovie = _context.Movies.FirstOrDefault(m => m.Id == movie.Id);
            if (dbMovie != null)
            {
                dbMovie.Score = calculatedMovieScore;

                _context.SaveChanges();
            }
        }
    }
}