using System.Collections.Generic;

namespace MovieDatabase.Model
{
    public interface Report
    {
        void generateReport(List<Movie> movieList);
    }
}
