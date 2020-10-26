using MovieDatabase.Model;
using System.Collections.Generic;

namespace MovieDatabase.DataAccess.Repository
{
    public interface IWatchedRepository
    {
        List<WatchedMovie> GetWatchedByUserId(int userId);

        void AddWatched(WatchedMovie newWatchedMovie);

        void RemoveWatched(int watchedMovieId);
    }
}