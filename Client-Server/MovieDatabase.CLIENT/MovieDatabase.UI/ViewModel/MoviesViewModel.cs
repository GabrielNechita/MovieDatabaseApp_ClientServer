using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieDatabase.Model;

namespace MovieDatabase.UI.ViewModel
{
  public class MoviesViewModel
  {
    public List<Movie> Movies { get; set; }

    public string SearchQuery { get; set; }

    public Movie GetMovieBySearchQuery(string searchQuery)
    {
      //Asta e un exemplu de LINQ - asa se numeste partea asta de enhancement in C#, ai mai sus System.Linq...si exista 2 Linq Method Query (care ii asta
      //ce l-am folosit eu ...is extensions methods(una dupa alta)) sau exista Linq Expression Query...care ii aprox la fel cu SQL Querys
      //Poti citi putin despre LINQ.
      return Movies.Where(x => x.Title == searchQuery && x.Genre == "" && x.Score == 2).FirstOrDefault();

  
    }
  }
}
