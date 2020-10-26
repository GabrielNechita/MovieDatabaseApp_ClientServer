using MovieDatabase.Model;
using MovieDatabase.UI.Socket;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MovieDatabase.UI.ViewModels
{

    public class ReviewViewModel
    {
        private int _movieId;

        public ReviewViewModel(int movieId)
        {
            _movieId = movieId;

            var getReviewsResponse = SocketHelper.ExecuteRequest("getMovieReviews;" + _movieId);
            ReviewsList = JsonConvert.DeserializeObject<List<Review>>(getReviewsResponse,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            var getMyReviewResponse = SocketHelper.ExecuteRequest("getReviewByMovieIdUserId;" + _movieId + ";" + LoggedInUser.UserId);
            MyReview = JsonConvert.DeserializeObject<Review>(getMyReviewResponse,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
        }

        public List<Review> ReviewsList { get; set; }

        public Review MyReview { get; set; }

        public Review SelectedMovieReview { get; set; }

        public void AddReview(string reviewContent)
        {
            var newReview = new Review()
            {
                MovieId = _movieId,
                UserId = LoggedInUser.UserId,
                Content = reviewContent
            };

            var jsonReviewSerialized = JsonConvert.SerializeObject(newReview, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            SocketHelper.ExecuteRequest("addMovieReview;" + jsonReviewSerialized);          

        }

        public void Thumb(string direction)
        {
            var getThumbResponse = SocketHelper.ExecuteRequest("getThumb;" + SelectedMovieReview.Id + ";" + LoggedInUser.UserId + ";" + direction);
            var thumb = JsonConvert.DeserializeObject<Thumb>(getThumbResponse,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

            if (thumb == null)
            {
                var newThumb = new Thumb()
                {
                    ReviewId = SelectedMovieReview.Id,
                    UserId = LoggedInUser.UserId,
                    Direction = direction
                };

                var jsonThumbSerialized = JsonConvert.SerializeObject(newThumb, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                SocketHelper.ExecuteRequest("AddReviewThumb;" + jsonThumbSerialized);                
            }
            else
            {
                SocketHelper.ExecuteRequest("DeleteReviewThumb;" + thumb.Id);
            }

            
        }

        public void EditReview(string reviewContent)
        {
            if (MyReview != null)
            {
                MyReview.Content = reviewContent;

                var jsonReviewSerialized = JsonConvert.SerializeObject(MyReview, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });

                SocketHelper.ExecuteRequest("editMovieReview;" + jsonReviewSerialized);
            }
                        
        }
    }
}
