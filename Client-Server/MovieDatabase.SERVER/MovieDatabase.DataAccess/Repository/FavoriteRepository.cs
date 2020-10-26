using MovieDatabase.Model;
using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.DataAccess.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly MovieDatabaseContext _context;

        public FavoriteRepository(MovieDatabaseContext context)
        {
            _context = context;
        }

        public List<Favorite> GetFavoritesByUserId(int userId)
        {

            return _context.Favorites.Where(x => x.UserId == userId).ToList();
        }

        public Favorite GetFavoriteByUserIdAndMovieId(int userId, int movieId)
        {
            return _context.Favorites.FirstOrDefault(x => x.UserId == userId && x.MovieId == movieId);
        }

        public void AddFavorite(Favorite newFavorite)
        {
            _context.Favorites.Add(newFavorite);

            _context.SaveChanges();
        }

        public void RemoveFavorite(int favoriteId)
        {
            var favoriteToDelete = _context.Favorites.FirstOrDefault(favorite => favorite.Id == favoriteId);
            if (favoriteToDelete != null)
            {
                _context.Favorites.Remove(favoriteToDelete);

                _context.SaveChanges();
            }
        }

    }
}
