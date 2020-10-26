using MovieDatabase.DataAccess.Repository;
using MovieDatabase.Model;
using System.Collections.Generic;

namespace MovieDatabase.Domain.Managers
{
    public class WatchedManager
    {
        private readonly IWatchedRepository _watchedRepository;

        public WatchedManager(IWatchedRepository watchedRepository)
        {
            _watchedRepository = watchedRepository;
        }

        public IList<WatchedMovie> GetWatchedMovies(int userId)
        {
            return _watchedRepository.GetWatchedByUserId(userId);
        }

        public void AddWatched(WatchedMovie watched)
        {
            _watchedRepository.AddWatched(watched);
        }

        public void RemoveWatched(int watchedMovieId)
        {
            _watchedRepository.RemoveWatched(watchedMovieId);
        }
    }
}
