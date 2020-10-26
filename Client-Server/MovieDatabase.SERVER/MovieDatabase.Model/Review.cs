using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Model
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int UserId { get; set; }

        public string Content { get; set; }

        public int ThumbsUp { get; set; }

        public int ThumbsDown { get; set; }

        public override string ToString()
        {
            return $"User {UserId} said: {Content}";
        }
    }
}
