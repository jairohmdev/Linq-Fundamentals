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

            var bestBMW = cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
                            .OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name)
                            .First();

            var query = cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
                            .OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name)
                            .Take(10)
                            .ToList();

            var query2 = (from car in cars
                          where car.Manufacturer == "BMW" && car.Year == 2016
                          orderby car.Combined descending, car.Name
                          select car).Take(10).ToList();

            query2.ForEach(c => Console.WriteLine($"{c.Manufacturer} {c.Name,-25} : {c.Combined}"));

            Console.ReadLine();
        }

        private static List<Car> ProcessFile(string path)
        {
            return File.ReadAllLines(path)
                .Skip(1)
                .Where(line => !string.IsNullOrEmpty(line))
                .Select(Car.ParseFromCsv)
                .ToList();
        }
    }
}
