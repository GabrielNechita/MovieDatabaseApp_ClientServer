using System.Collections.Generic;
using System.IO;

namespace MovieDatabase.Model
{
    public class TxtReport : IReport
    {
        public void GenerateReport(List<Movie> movieList)
        {
            FileInfo f = new FileInfo("Report.txt");
            StreamWriter w = f.CreateText();
            foreach (var movie in movieList)
            {
                w.WriteLine(movie.ToString());
            }

            w.Close();
        }
    }
}
