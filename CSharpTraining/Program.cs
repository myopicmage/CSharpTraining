using System;
using System.Linq;

namespace CSharpTraining
{
    public class Program
    {
        public static void Main()
        {
            var utils = new Utils();
            string day;

            do
            {
                Console.Write("Select a day (1, 2): ");
                day = Console.ReadLine();
            } while (!new[] { "1", "2" }.Contains(day));

            var lessons = utils.GetLessons(day);

            Console.WriteLine($"Day {day}");

            for (var i = 0; i < lessons.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {lessons[i]}");
            }

            Console.Write("Select a lesson: ");
            var lesson = Console.ReadLine();

            Console.WriteLine();

            utils.RunLesson(day, lessons[int.Parse(lesson) - 1]);

            Console.ReadLine();
        }
    }
}