using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Model
{
    public class Thumb
    {
        [Key]
        public int Id { get; set; }

        public string Direction { get; set; }

        public int ReviewId { get; set; }

        public int UserId { get; set; }
    }
}
