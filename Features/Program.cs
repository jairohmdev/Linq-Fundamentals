using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double, double> square = x => x * x;
            Func<double, double, double, double> rectangleArea = (l, w, h) => l * w * h;
            Func<double, double, double> triangleArea = (b, h) =>
            {
                double area = .5 * (b * h);
                return area;
            };

            Action<int> write = x => Console.WriteLine(x);

            write(1);

            Console.WriteLine(square(3));
            Console.WriteLine(rectangleArea(4, 5, 6));

            var developers = new Employee[] {
                new Employee { Id=1, Name="Jairoh" },
                new Employee { Id=2, Name="Deb"},
                new Employee { Id=3, Name="Jack"}
            };

            var sales = new List<Employee>()
            {
                new Employee { Id=3, Name="Darl" }
            };

            // IEnumerator<Employee> enumerator = developers.GetEnumerator();
            // while (enumerator.MoveNext())
            // {
            //     var employee = enumerator.Current;
            //     Console.WriteLine(employee.Name);
            // }

            // Method Syntax (using lambda expressions)
            var jDevelopers = developers.Where(d => d.Name.StartsWith("J"))
                                        .OrderBy(d => d.Name).ToList();

            jDevelopers.ForEach(d => Console.WriteLine($"{d.Id} {d.Name}"));

            // Query Syntax
            var jDevelopers2 = (from dev in developers
                                where dev.Name.StartsWith("J")
                                orderby dev.Name
                                select dev).ToList();

            Console.ReadLine();
        }
    }
}
