using MovieDatabase.DataAccess.Repository;
using MovieDatabase.Model;
using System.Collections.Generic;

namespace MovieDatabase.Domain.Managers
{
    public class FavoritesManager
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoritesManager(IFavoriteRepository favoriteRepository)
        {            
            _favoriteRepository = favoriteRepository;
        }

        public IList<Favorite> GetFavorites(int userId)
        {
            return _favoriteRepository.GetFavoritesByUserId(userId);
        }

        public void AddFavorite(Favorite favorite)
        {
            _favoriteRepository.AddFavorite(favorite);
        }

        public void RemoveFavorite(int favoriteMovieId)
        {
            _favoriteRepository.RemoveFavorite(favoriteMovieId);
        }
    }
}
