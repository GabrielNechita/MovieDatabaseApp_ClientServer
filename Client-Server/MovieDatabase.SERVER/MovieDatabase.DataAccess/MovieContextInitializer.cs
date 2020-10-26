using MovieDatabase.Model;
using SQLite.CodeFirst;
using System.Data.Entity;

namespace MovieDatabase.DataAccess
{
    public class MovieContextInitializer : SqliteCreateDatabaseIfNotExists<MovieDatabaseContext>
    {

        public MovieContextInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
        {
            modelBuilder.Entity<Movie>();
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Score>();
            modelBuilder.Entity<Favorite>();
            modelBuilder.Entity<Review>();
            modelBuilder.Entity<Thumb>();
            modelBuilder.Entity<WatchedMovie>();
        }

        protected override void Seed(MovieDatabaseContext context)
        {
            Movie movie1 = new Movie
            {
                Title = "Movie 1"
            };
            Movie movie2 = new Movie
            {
                Title = "Movie 2"
            };

            context.Movies.Add(movie1);
            context.Movies.Add(movie2);

            var user = new User() { Password = "1234", Username = "user", Role = "User" };
            var admin = new User { Password = "1234", Username = "admin", Role = "Admin" };

            context.Users.Add(user);
            context.Users.Add(admin);

            context.SaveChanges();
        }
    }
}
