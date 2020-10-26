using MovieDatabase.Model;
using System.Collections.Generic;

namespace MovieDatabase.DataAccess.Repository
{
    public interface IFavoriteRepository
    {
        List<Favorite> GetFavoritesByUserId(int userId);

        Favorite GetFavoriteByUserIdAndMovieId(int userId, int movieId);

        void AddFavorite(Favorite newFavorite);

        void RemoveFavorite(int favoriteId);
    }
}