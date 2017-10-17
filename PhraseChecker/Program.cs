using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhraseChecker
{
    using System.Runtime.CompilerServices;

    class Program
    {
        static void Main(string[] args)
        {
            char[] bag = new[]
            {
                '#',
                '&',
                '?',
                ';',
                '!',
                '@',
                ':',
                '0',
                '1',
                'A',
                'A',
                'A',
                'A',
                'B',
                'B',
                'B',
                'C',
                'C',
                'D',
                'D',
                'E',
                'E',
                'E',
                'E',
                'F',
                'F',
                'F',
                'G',
                'G',
                'H',
                'H',
                'I',
                'I',
                'I',
                'J',
                'J',
                'K',
                'K',
                'L',
                'L',
                'L',
                'M',
                'M',
                'N',
                'N',
                'O',
                'O',
                'O',
                'O',
                'P',
                'P',
                'Q',
                'R',
                'R',
                'R',
                'S',
                'S',
                'S',
                'T',
                'T',
                'T',
                'U',
                'U',
                'V',
                'V',
                'W',
                'W',
                'W',
                'X',
                'Y',
                'Y',
                'Y',
                'Z'
            };
            // usage: PhraseChecker "phrase to check"
            // the phrase passed in will be evaluated to see if it can be made from the letters and symbols in the bag.

            char[,] board = new char[3, 8];

            if (args.Length > 1)
            {
                Console.WriteLine($"Usage: PhraseChecker \"phrase to evaluate\"");
                Environment.Exit(0);
            }

            string phrase = args[0].ToUpperInvariant().Trim();

            Console.WriteLine($"Checking to see if \"{phrase}\" can fit on the board...");


            List<char> workingBag = new List<char>(bag);

            List<string> words = new List<string>();

            List<char> currentWord = new List<char>();

            // first see if we have the letters available
            foreach (char c in phrase)
            {
                if (c == ' ')
                {
                    // new word
                    if (currentWord.Count > 0)
                    {
                        words.Add(new string(currentWord.ToArray()));
                        currentWord = new List<char>();
                    }

                    continue;
                }

                if (workingBag.Contains(c))
                {
                    workingBag.Remove(c);
                    currentWord.Add(c);
                }
                else
                {
                    Console.WriteLine($"Unfortunately, the phrase \"{phrase}\" cannot be produced because there aren't enough of the '{c}' in the bag.");
                    Environment.Exit(-1);
                }
            }

            if (currentWord.Count > 0)
            {
                words.Add(new string(currentWord.ToArray()));
            }

            Console.WriteLine($"Congratualations! There are enough letters in the bag to put \"{phrase}\" on the board.");

            int maxWordLength = board.GetUpperBound(1) + 1;
            int maxRows = board.GetUpperBound(0) + 1;

            int i = 0;
            int j = 0;

            // now see if the words will fit on the board
            foreach (string word in words)
            {
                if (word.Length > maxWordLength)
                {
                    Console.WriteLine($"Unfortunately, the phrase \"{phrase}\" cannot be produced because the word \"{word}\" is longer than a single line can hold of {maxWordLength} characters.");
                    Environment.Exit(-1);
                }

                // make sure word will fit on this row, if not, move to the next row.
                if (word.Length > maxWordLength - j)
                {
                    i++;
                    j = 0;
                }

                // make sure we didn't run out of rows
                if (i >= maxRows)
                {
                    Console.WriteLine($"Unfortunately, the phrase \"{phrase}\" won't fit on the board because it would take more than {maxRows} lines.");
                    Environment.Exit(-1);
                }

                // write the word to the board.
                for (int k = 0; k < word.Length; k++)
                {
                    board[i, j] = word[k];

                    j++;
                }

                // add a space if we're not at the end of the line
                if (j < maxWordLength - 1)
                {
                    board[i, j] = ' ';
                    j++;
                }
            }

            Console.WriteLine($"Congratualations! \"{phrase}\" will fit on the board like so:");

            Console.WriteLine("+--------+");

            for (i = 0; i < maxRows; i++)
            {
                Console.Write("|");
                for (j = 0; j < maxWordLength; j++)
                {
                    Console.Write(board[i, j]);
                }

                Console.WriteLine("|");
            }

            Console.WriteLine("+--------+");
        }
    }
}
