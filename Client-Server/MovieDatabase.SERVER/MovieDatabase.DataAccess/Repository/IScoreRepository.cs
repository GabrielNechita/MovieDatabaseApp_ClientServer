using MovieDatabase.Model;

namespace MovieDatabase.DataAccess.Repository
{
    public interface IScoreRepository
    {
        double GetMovieScore(int movieId);

        void AddMovieScore(int userId, int movieId, int scoreValue);

        Score GetScore(int movieId, int userId);

    }
}