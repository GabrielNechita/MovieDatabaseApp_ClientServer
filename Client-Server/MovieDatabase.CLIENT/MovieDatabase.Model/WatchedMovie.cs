using System.ComponentModel.DataAnnotations;


namespace MovieDatabase.Model
{
    public class WatchedMovie
    {
        [Key]
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int UserId { get; set; }

        public string MovieTitle { get; set; }

        public override string ToString()
        {
            return $"movieId: {MovieId}, MovieTitle: {MovieTitle}";
        }
    }
}
