using MovieDatabase.Model;
using System.Collections.Generic;

namespace MovieDatabase.DataAccess.Repository
{
    public interface IReviewRepository
    {
        List<Review> GetReviewsByMovieId(int movieId);

        void AddReview(Review newReview);

        Review GetReviewByMovieIdUserId(int movieId, int userId);

        void UpdateReview(Review review);

        Thumb GetThumbByReviewIdUserId(int reviewId, int userId, string direction);

        void DeleteReviewThumb(int thumbId);

        void AddThumb(Thumb thumb);
    }
}