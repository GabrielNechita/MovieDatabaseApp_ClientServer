using MovieDatabase.Model;
using System.Data.Entity;

namespace MovieDatabase.DataAccess
{
    public class MovieDatabaseContext : DbContext
    {
        public MovieDatabaseContext() : base("UserContext")
        {
            Database.Initialize(true);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new MovieContextInitializer(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }

        public IDbSet<Movie> Movies { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Score> Scores { get; set; }
        public IDbSet<Favorite> Favorites { get; set; }
        public IDbSet<Review> Reviews { get; set; }
        public IDbSet<Thumb> Thumbs { get; set; }
        public IDbSet<WatchedMovie> WatchedMovies { get; set; }
    }
}