using MovieDatabase.Model;
using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.DataAccess.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MovieDatabaseContext _context;

        public ReviewRepository(MovieDatabaseContext context)
        {
            _context = context;
        }

        public List<Review> GetReviewsByMovieId(int movieId)
        {

            return _context.Reviews.Where(x => x.MovieId == movieId).ToList();
        }

        public void AddReview(Review newReview)
        {
            _context.Reviews.Add(newReview);

            _context.SaveChanges();
        }

        public void AddThumb(Thumb thumb)
        {
            if (thumb.Direction == "up")
            {
                _context.Reviews.FirstOrDefault(r => r.Id == thumb.ReviewId).ThumbsUp++;
            }
            else
            {
                _context.Reviews.FirstOrDefault(r => r.Id == thumb.ReviewId).ThumbsDown++;
            }

            _context.Thumbs.Add(thumb);

            _context.SaveChanges();
        }

        public Review GetReviewByMovieIdUserId(int movieId, int userId)
        {
            return _context.Reviews.FirstOrDefault(r => r.MovieId == movieId && r.UserId == userId);
        }

        public void UpdateReview(Review review)
        {
            var dbReview = _context.Reviews.FirstOrDefault(r => r.Id == review.Id);
            if (dbReview != null)
            {
                dbReview.Content = review.Content;

                _context.SaveChanges();
            }
        }

        public Thumb GetThumbByReviewIdUserId(int reviewId, int userId, string direction)
        {
            return _context.Thumbs.FirstOrDefault(t => t.ReviewId == reviewId && t.UserId == userId && t.Direction == direction);
        }

        public void DeleteReviewThumb(int thumbId)
        {
            var thumbToDelete = _context.Thumbs.FirstOrDefault(t => t.Id == thumbId);           

            if (thumbToDelete != null)
            {
                if (thumbToDelete.Direction == "up")
                {
                    _context.Reviews.FirstOrDefault(r => r.Id == thumbToDelete.ReviewId).ThumbsUp--;
                }
                else
                {
                    _context.Reviews.FirstOrDefault(r => r.Id == thumbToDelete.ReviewId).ThumbsDown--;
                }

                _context.Thumbs.Remove(thumbToDelete);

                _context.SaveChanges();
            }
        }
    }
}
