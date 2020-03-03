using System;
using System.Collections.Generic;

namespace CSharpTraining.Day1.Lesson5
{
    public class Lesson5
    {
        public void Exercise()
        {
            // your code

            // PrintArray is generic, but you thankfully do not need to worry about that
            // PrintArray(yourArray);

            // var reversed = Reverse(originalStr);
        }

        private string Reverse(string original)
        {
            var reversed = "";

            return reversed;
        }

        // don't worry about this for now. we'll be getting to loops and collections soon!
        private void PrintArray<T>(IEnumerable<T> array)
        {
            foreach (var element in array)
            {
                Console.WriteLine(element);
            }
        }
    }
}