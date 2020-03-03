using System;
using System.IO;

namespace CSharpTraining.Day2.Lesson1
{
    public class Lesson1
    {
        public void Exercise()
        {
            try {
                Console.WriteLine("hello");
            } catch (Exception ex) {
                Console.WriteLine("oh no!!!");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
