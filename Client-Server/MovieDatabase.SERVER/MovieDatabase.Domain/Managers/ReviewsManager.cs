using MovieDatabase.DataAccess.Repository;
using MovieDatabase.Model;
using System.Collections.Generic;

namespace MovieDatabase.Domain.Managers
{
    public class ReviewsManager
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewsManager(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public IList<Review> GetReviews(int movierId)
        {
            return _reviewRepository.GetReviewsByMovieId(movierId);
        }

        public void AddReview(Review review)
        {
            _reviewRepository.AddReview(review);
        }

        public void AddThumb(Thumb thumb)
        {
            _reviewRepository.AddThumb(thumb);
        }

        public Review GetReviewByMovieIdUserId(int movieId, int userId)
        {
            return _reviewRepository.GetReviewByMovieIdUserId(movieId, userId);
        }

        public void UpdateReview(Review review)
        {
            _reviewRepository.UpdateReview(review);
        }

        public Thumb GetThumbByReviewIdUserId(int reviewId, int userId, string direction)
        {
            return _reviewRepository.GetThumbByReviewIdUserId(reviewId, userId, direction);
        }

        public void DeleteReviewThumb(int thumbId)
        {
            _reviewRepository.DeleteReviewThumb(thumbId);
        }
    }
}
