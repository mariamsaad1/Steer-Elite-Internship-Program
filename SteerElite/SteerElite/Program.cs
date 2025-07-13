using System;
using System.Collections.Generic;

namespace mm
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = null;
            int max = 10;

            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i] == "-c")
                    code = args[i + 1];
                if (args[i] == "-t")
                    int.TryParse(args[i + 1], out max);
            }

            if (string.IsNullOrEmpty(code))
                code = MakeRandom();

            Console.WriteLine("Can you break the code? Enter a valid guess.");

            int turn = 0;
            while (turn < max)
            {
                Console.WriteLine("Round " + turn);
                Console.Write(">");

                string input = Console.ReadLine();

                if (input == null)
                {
                    Console.WriteLine("EOF. Exiting...");
                    break;
                }

                if (!CheckGuess(input))
                {
                    Console.WriteLine("Wrong input!");
                    continue;
                }

                if (input == code)
                {
                    Console.WriteLine("Congratz! You did it!");
                    break;
                }

                int good = 0;
                int bad = 0;

                for (int i = 0; i < 4; i++)
                {
                    if (input[i] == code[i])
                        good++;
                    else
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (i != j && input[i] == code[j])
                            {
                                bad++;
                                break;
                            }
                        }
                    }
                }

                Console.WriteLine("Well placed pieces: " + good);
                Console.WriteLine("Misplaced pieces: " + bad);
                turn++;
            }

            if (turn == max)
                Console.WriteLine("You've used all attempts. Game over!");
        }

        static string MakeRandom()
        {
            string d = "012345678";
            List<char> pool = new List<char>(d.ToCharArray());
            Random rnd = new Random();
            string result = "";

            while (result.Length < 4)
            {
                int idx = rnd.Next(pool.Count);
                result += pool[idx];
                pool.RemoveAt(idx);
            }

            return result;
        }

        static bool CheckGuess(string s)
        {
            if (s.Length != 4)
                return false;

            for (int i = 0; i < 4; i++)
            {
                if (s[i] < '0' || s[i] > '8')
                    return false;

                for (int j = i + 1; j < 4; j++)
                {
                    if (s[i] == s[j])
                        return false;
                }
            }

            return true;
        }
    }
}
