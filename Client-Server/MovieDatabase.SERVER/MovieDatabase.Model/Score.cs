using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Model
{
    public class Score
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MovieId { get; set; }

        public int ScoreValue { get; set; }
    }
}
