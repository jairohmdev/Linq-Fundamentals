using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessFile("fuel.csv");

            var query = cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
                            .OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name)
                            .Take(10)
                            .ToList();

            var query2 = (from car in cars
                          where car.Manufacturer == "BMW" && car.Year == 2016
                          orderby car.Combined descending, car.Name
                          select car).Take(10).ToList();
            
            query.ForEach(c => Console.WriteLine($"{c.Manufacturer} {c.Name,-25} : {c.Combined}"));

            var isFound = cars.Any(c => c.Manufacturer == "Ford");
            var areAllCarsFord = cars.All(c => c.Manufacturer == "Ford");
            Console.WriteLine(areAllCarsFord);

            Console.ReadLine();
        }

        private static void UsingFirstOrDefault(List<Car> cars)
        {
            var bestBMW = cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
                                        .OrderByDescending(c => c.Combined)
                                        .ThenBy(c => c.Name)
                                        .FirstOrDefault();

            var bestBMW2 = cars.OrderByDescending(c => c.Combined)
                             .ThenBy(c => c.Name)
                             .FirstOrDefault(c => c.Manufacturer == "BMW" && c.Year == 2016);

            // Console.WriteLine(Equals(bestBMW, bestBMW2).ToString());
        }

        private static void GetAListOfAnonymousObjects(List<Car> cars)
        {
            // Get a list of anonymous objects w/ only 3 properties
            var query3 = (from car in cars
                          where car.Manufacturer == "BMW" && car.Year == 2016
                          orderby car.Combined descending, car.Name
                          select new
                          {
                              car.Manufacturer,
                              car.Name,
                              car.Combined
                          }).Take(10).ToList();

            var query3a = cars.Select(c => new { c.Manufacturer, c.Name, c.Combined, c.Year })
                            .Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
                            .OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name)
                            .Take(10)
                            .ToList();
        }

        private static List<Car> ProcessFile(string path)
        {
            return File.ReadAllLines(path)
                .Skip(1)
                .Where(l => !string.IsNullOrEmpty(l))
                //.Select(l => Car.ParseFromCsv(l))
                .ToCar()
                .ToList();
        }
    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source.ToList())
            {
                var columns = line.Split(',');

                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}
