using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.DayThree
{
    class Program
    {
        static int[,] Map = new int[1000,1000];
        static List<Claim> Claims = new List<Claim>();

        static void Main(string[] args)
        {
            using (var input = new StreamReader(@"input.txt"))
            {
                string line;
                while ((line = input.ReadLine()) != null)
                {
                    int claim, x, y, width, height;

                    var split = line.Split('@');
                    claim = int.Parse(split[0].Trim().Replace("#", string.Empty));
                    split = split[1].Split(':');
                    x = int.Parse(split[0].Split(',')[0]);
                    y = int.Parse(split[0].Split(',')[1]);
                    width = int.Parse(split[1].Split('x')[0]);
                    height = int.Parse(split[1].Split('x')[1]);

                    var clm = new Claim
                    {
                        Id = claim,
                        X = x,
                        Y = y,
                        Width = width,
                        Height = height
                    };

                    Claims.Add(clm);
                    CreateClaim(clm);
                }
            }

            Console.ReadLine();
        }

        static void CreateClaim(Claim claim)
        {
            for (var y = 0; y < claim.Height; y++)
            {
                for (var x = 0; x < claim.Width; x++)
                {
                    var current = Map[x + claim.X, y + claim.Y];
                    Map[x + claim.X, y + claim.Y] = current == 0 ? claim.Id : -1;
                }
            }
        }

        static void PartOne()
        {
            var count = 0;

            for (var y = 0; y < 1000; y++)
            {
                for (var x = 0; x < 1000; x++)
                {
                    if (Map[x, y] == -1)
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine(count);
        }

        static void PartTwo()
        {
            Claims.ForEach(clm =>
            {
                for (var y = 0; y < clm.Height; y++)
                {
                    for (var x = 0; x < clm.Width; x++)
                    {
                        if (Map[x + clm.X, y + clm.Y] == -1)
                        {
                            clm.IsOverlapped = true;
                        }
                    }
                }
            });

            var notOverlapped = Claims.FirstOrDefault(x => x.IsOverlapped == false);

            Console.WriteLine(notOverlapped.Id);
        }

        static void WriteMap()
        {

            if (!File.Exists(@"map.map"))
            {
                var fs = File.Create(@"map.map");
                fs.Close();
            }

            using (var sw = new StreamWriter(@"map.map"))
            {
                for (var y = 0; y < 1000; y++)
                {
                    for (var x = 0; x < 1000; x++)
                    {
                        sw.Write($"{Map[x, y].ToString().PadLeft(4)}, ");
                    }
                    sw.Write("\n");
                    sw.Flush();
                }
            }
        }        
    }
}
