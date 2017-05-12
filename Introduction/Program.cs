using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction
{
    class Program
    {
        private static readonly string ROOT_PATH = @"C:\windows";

        static void Main(string[] args)
        {
            ShowLargeFilesWithLinq(ROOT_PATH);

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        private static void ShowLargeFilesWithLinq(string path)
        {
            var files = from file in new DirectoryInfo(path).GetFiles()
                        orderby file.Length descending
                        select file;

            var files2 = new DirectoryInfo(path).GetFiles()
                            .OrderByDescending(f => f.Length)
                            .Take(5);

            foreach (var file in files2.Take(5))
            {
                Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0} bytes");
            }
        }

        private static void ShowLargeFilesWithoutLinq(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            var files = directory.GetFiles();
            Array.Sort(files, new FileComparer());

            for (int index = 0; index < 5; index++)
            {
                FileInfo file = files[index];
                Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0} bytes");
            }
        }
    }

    public class FileComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }
}
