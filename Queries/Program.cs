using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = MyLinq.Random().Where(n => n > 0.5).Take(10).ToList();
            numbers.ForEach(n => Console.WriteLine(n));

            var movies = new List<Movie>
            {
                new Movie {Title = "The Dark Night",     Rating = 8.9f, Year = 2008 },
                new Movie {Title = "The King's Speech",  Rating = 8.0f, Year = 2010 },
                new Movie {Title = "Casablanca",         Rating = 8.5f, Year = 1942 },
                new Movie {Title = "Star Wars V",        Rating = 8.7f, Year = 1980 }
            };

            var selectedMovies = movies.Where(m => m.Year > 1950)
                                       .OrderByDescending(m => m.Rating)
                                       .ToList();

            // var selectedMovies = movies.Filter(m => m.Year > 2000).ToList();
            // selectedMovies.ForEach(m => Console.WriteLine($"{m.Title,-20} {m.Rating,5} {m.Year,5}"));

            //var enumerator = selectedMovies.GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    Console.WriteLine(enumerator.Current.Title);
            //}

            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadLine();
        }
    }
}
