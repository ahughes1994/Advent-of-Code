using System;
using System.IO;

namespace AdventOfCode.DayOne
{
    class Program
    {
        static void Main(string[] args)
        {
            var total = 0;
            using (var input = new StreamReader(@"input.txt"))
            {
                string line;
                while ((line = input.ReadLine()) != null)
                {
                    if (int.TryParse(line, out var result))
                    {
                        total += result;
                    }
                }
            }

            Console.WriteLine(total.ToString());
            Console.ReadLine();
        }
    }
}
