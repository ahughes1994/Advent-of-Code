using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.DayOne
{
    class Program
    {
        static void Part2Main(string[] args)
        {
            var usedFreqs = new List<int>();
            var total = 0;
            int? repeat = null;
            usedFreqs.Add(total);

            while (repeat == null)
            {
                using (var input = new StreamReader(@"input2.txt"))
                {
                    string line;
                    while ((line = input.ReadLine()) != null)
                    {
                        if (int.TryParse(line, out var result))
                        {
                            total += result;
                            if (usedFreqs.Contains(total))
                            {
                                repeat = total;
                                break;
                            }
                            else
                            {
                                usedFreqs.Add(total);
                            }
                        }
                    }
                }
            }

            Console.WriteLine(total.ToString());
            Console.ReadLine();
        }

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
