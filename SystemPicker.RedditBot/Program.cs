using System;
using SystemPicker.Matcher;

namespace SystemPicker.RedditBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var procGen = new ProcGenExpressionGenerator();
            var expressions = procGen.GenerateProcGenRegex();

            foreach (var e in expressions)
            {
                Console.WriteLine(e);
                Console.WriteLine();
            }
        }
    }
}