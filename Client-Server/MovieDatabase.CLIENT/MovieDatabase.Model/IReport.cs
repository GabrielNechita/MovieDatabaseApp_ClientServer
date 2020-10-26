using System.Collections.Generic;

namespace MovieDatabase.Model
{
    public interface IReport
    {
        void GenerateReport(List<Movie> movieList);
    }
}
