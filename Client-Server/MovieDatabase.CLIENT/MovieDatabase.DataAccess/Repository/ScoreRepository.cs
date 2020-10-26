using MovieDatabase.Model;
using System.Linq;

namespace MovieDatabase.DataAccess.Repository
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly MovieDatabaseContext _context;

        public ScoreRepository(MovieDatabaseContext context)
        {
            _context = context;
        }

        public double GetMovieScore(int movieId)
        {
            var a = (double)_context.Scores.Where(x => x.MovieId == movieId).Sum(x => x.ScoreValue);
            var b = (double)_context.Scores.Where(x => x.MovieId == movieId).Count();

            var rating = a / b;

            return rating;
        }

        public void AddMovieScore(int userId, int movieId, int scoreValue)
        {
            var score = new Score { MovieId = movieId, UserId = userId, ScoreValue = scoreValue };

            _context.Scores.Add(score);

            _context.SaveChanges();
        }

        public Score GetScoreId(int movieId, int userId)
        {
            Score score;

            try
            {
                score = _context.Scores.Where(x => x.MovieId == movieId && x.UserId == userId).First();
            }
            catch (System.InvalidOperationException)
            {

                return null;
            }
            

            return score;
        }

    }
}
