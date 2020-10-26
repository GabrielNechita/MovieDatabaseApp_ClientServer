using MovieDatabase.DataAccess.Repository;
using MovieDatabase.Model;

namespace MovieDatabase.Domain.Managers
{
    public class ScoreManager
    {
        private readonly IScoreRepository _scoreRepository;

        public ScoreManager(IScoreRepository scoreRepository)
        {          
            _scoreRepository = scoreRepository;
        }

        public void AddScore(int userId, int movieId, int scoreValue)
        {
            _scoreRepository.AddMovieScore(userId, movieId, scoreValue);
        }

        public double GetMovieScore(int movieId)
        {
            return _scoreRepository.GetMovieScore(movieId);
        }

        public Score GetScore(int movieId, int userId)
        {
            return _scoreRepository.GetScore(movieId, userId);
        }
    }
}
