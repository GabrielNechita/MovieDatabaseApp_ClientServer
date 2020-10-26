using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Model
{
    public class Movie
    {
        public Movie()
        {
            Score = 0;
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public string Actors { get; set; }

        public double Score { get; set; }

        public string Status { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Title: {Title}, Genre: {Genre}, Actors: {Actors}, Score: {Score}, Status: {Status}";
        }
    }
}
