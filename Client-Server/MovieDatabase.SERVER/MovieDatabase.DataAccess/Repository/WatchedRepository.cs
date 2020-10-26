using MovieDatabase.Model;
using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.DataAccess.Repository
{
    public class WatchedRepository : IWatchedRepository
    {
        private readonly MovieDatabaseContext _context;

        public WatchedRepository(MovieDatabaseContext context)
        {
            _context = context;
        }

        public List<WatchedMovie> GetWatchedByUserId(int userId)
        {
            return _context.WatchedMovies.Where(x => x.UserId == userId).ToList();
        }

        public void AddWatched(WatchedMovie newWatchedMovie)
        {
            _context.WatchedMovies.Add(newWatchedMovie);

            _context.SaveChanges();
        }

        public void RemoveWatched(int watchedMovieId)
        {
            var watchedToDelete = _context.WatchedMovies.FirstOrDefault(watched => watched.Id == watchedMovieId);
            if (watchedToDelete != null)
            {
                _context.WatchedMovies.Remove(watchedToDelete);

                _context.SaveChanges();
            }
        }
    }
}
