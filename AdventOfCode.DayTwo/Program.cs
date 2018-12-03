using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.DayTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseIds = new List<string>();
            var correctIds = new ConcurrentBag<string>();

            var start = DateTime.Now;

            using (var input = new StreamReader(@"input.txt"))
            {
                string line;
                while ((line = input.ReadLine()) != null)
                {
                    baseIds.Add(line);
                }
            }

            var threads = 128;

            var tasks = new Task[threads];

            for (var i = 0; i < threads; i++)
            {
                var segment = new List<string>();

                segment = baseIds.Skip((baseIds.Count / threads) * i).Take(baseIds.Count / threads).ToList();

                tasks[i] = Task.Factory.StartNew(() =>
                {
                    var checkedLinePairs = new List<(string, string)>();
                    foreach (var line in baseIds)
                    {
                        foreach (var line2 in segment)
                        {
                            if (line != line2)
                            {
                                if (checkedLinePairs.Contains((line, line2)) || checkedLinePairs.Contains((line2, line)))
                                {
                                    continue;
                                }

                                checkedLinePairs.Add((line, line2));

                                if (ProcessLinePart2((line, line2), out var result))
                                {
                                    correctIds.Add(result);
                                }
                            }
                        }
                    }
                });
            }

            while (tasks.Any(t => !t.IsCompleted)) { } //spin wait

            var outlist = correctIds.ToList().OrderByDescending(x => x.Length).ToList();

            var duration = DateTime.Now - start;

            Console.WriteLine(outlist[0]);
            Console.WriteLine(duration.ToString());
            Console.ReadLine();
        }

        static void Part1Main(string[] args)
        {
            var twoOfAnyLetter = new List<string>();
            var threeOfAnyLetter = new List<string>();

            using (var input = new StreamReader(@"input.txt"))
            {
                string line;
                while ((line = input.ReadLine()) != null)
                {
                    if (StringHasXDuplicateLetters(line, 2))
                    {
                        twoOfAnyLetter.Add(line);
                    }

                    if (StringHasXDuplicateLetters(line, 3))
                    {
                        threeOfAnyLetter.Add(line);
                    }
                }
            }

            Console.WriteLine((twoOfAnyLetter.Count * threeOfAnyLetter.Count).ToString());
            Console.ReadLine();
        }

        private static bool ProcessLinePart2((string a, string b) input, out string result)
        {
            var letterList1 = input.a.ToCharArray().ToList();
            var letterList2 = input.b.ToCharArray().ToList();

            var output = string.Empty;

            for (var c = 0; c < letterList1.Count; c++)
            {
                if (letterList1[c] == letterList2[c])
                {
                    output += letterList1[c];
                }
            }

            result = output;

            return !string.IsNullOrEmpty(output);
        }

        private static bool StringHasXDuplicateLetters(string line, int letterCount)
        {
            var chars = new Dictionary<char, int>();
            foreach (var chr in line)
            {
                if (chars.ContainsKey(chr))
                {
                    chars[chr]++;
                }
                else
                {
                    chars[chr] = 1;
                }
            }

            foreach (var kvp in chars)
            {
                if (kvp.Value == letterCount)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
