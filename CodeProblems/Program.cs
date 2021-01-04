using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeProblems
{
    class Program
    {
        static void Main(string[] args)
        {

            BasicCalculator();
            TextJustification();

            Console.WriteLine("Select problem number:");
            foreach(string q in Problems()) { Console.WriteLine($"  {q}\n"); }


            switch (Console.ReadLine())
            {
                case "1":
                    BasicCalculator();
                    break;
                case "2":
                    TextJustification()
                    break;

            }

        }
        public static List<string> Problems()
        {
            return new List<string>
            {
                "1: Basic Calculator",
                "2: Text Justification"
            };
        }

        public static void TextJustification()
        {
            Console.WriteLine("Input: words = [\"This\", \"is\", \"an\", \"example\", \"of\", \"text\", \"justification.\"], maxWidth = 16");
            string[] words = new string[] { "This", "is", "an", "example", "of", "text", "justification." };
            int maxWidth = 16;


            List<string> lines = new List<string>();
            int i = 0;
            int wordsLength = 0;                
            List<string> lineWords = new List<string>(); 
            while (i <= words.Count() -1)
            {       
                
                if(words[i].Length <= maxWidth - wordsLength)
                {
                    lineWords.Add(words[i]);
                    wordsLength = wordsLength + words[i].Length + 1;
                    i++;
                }
                else
                {
                    int spaceMinLength = Convert.ToInt32(decimal.Floor((maxWidth - wordsLength)/ lineWords.Count()));
                    int extraSpaceLength = Convert.ToInt32(decimal.Floor((maxWidth - wordsLength - (spaceMinLength * lineWords.Count)) % lineWords.Count));

                    char[] line = new char[maxWidth];
                    int lineLength = 0;
                    for(int w = 0; w < lineWords.Count; w++)
                    {
                        lineWords[w].CopyTo(0, line, lineLength, lineWords[w].Length);
                        lineLength += lineWords[w].Length;
                        if(w < lineWords.Count - 1 )
                        {
                            for (int n = 0; n <= spaceMinLength; n++)
                            {
                                line[lineLength] = ' ';
                                lineLength++;
                            }
                        }
                        if(extraSpaceLength > 0)
                        {
                            line[lineLength] = ' ';
                            lineLength++;
                            extraSpaceLength--;
                        }
                    }

                    string sline = "";
                    for(int l = 0; l <= maxWidth - 1; l++)
                    {
                        sline += line[l];
                    }
                    lines.Add(sline);
                    wordsLength = 0;
                    lineWords = new List<string>();
                }
            }
            lines.ForEach(l => Console.WriteLine(l));
        }

       


        /// <summary>
        /// 1: Basic Calculator
        /// </summary>
        public static void BasicCalculator()
        {
            /*            
                 Implement a basic calculator to evaluate a simple expression string.
                 The expression string may contain open ( and closing parentheses ), the plus + or minus sign -, non-negative integers and empty spaces .   
            */
            do {
                Console.WriteLine("Enter input:");
                string input = Console.ReadLine();
                Console.WriteLine($"={deconstruct(input)}");        
            } while (!Equals(Console.ReadKey(), ConsoleKey.Escape));
            
        }

        public static string deconstruct(string input)
        {
            string fx = input;
            string matchPattern = "\\(\\-*\\d+(\\+|\\-)\\d+\\)";
            while (Regex.IsMatch(fx, matchPattern))
            {
                var match = Regex.Match(fx, "\\-*\\d+(\\+|\\-)\\d+").ToString();
                fx = Regex.Replace(fx, matchPattern, calculate(match).ToString());
                //Console.WriteLine(fx);
            }
            //Console.WriteLine(fx);
            return calculate(fx).ToString();          
        }

        public static int calculate(string input)
        {
            string fx = input;
            int i = int.TryParse(Regex.Match(fx, "^-*\\d+").Value, out int mv) ? mv : 0;
            fx = Regex.Replace(fx, "^-*\\d+", string.Empty);
            //Console.WriteLine(fx);
            Regex.Matches(fx, "(\\-|\\+)\\d+").ToList().ForEach(m =>
            {
                i = i + int.Parse(m.Value);
            });
            return i;
        }
    }
}
