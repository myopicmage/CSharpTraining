using System;
using System.Linq;
using System.Reflection;

namespace CSharpTraining
{
    public class Utils
    {
        public string[] GetLessons(string day) =>
          Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.IsPublic && x.IsClass && x.Namespace.Contains($"Day{day}"))
            .Select(x => x.Name)
            .OrderBy(x => x)
            .ToArray();

        public void RunLesson(string day, string lesson)
        {
            var l = Assembly.GetExecutingAssembly()
              .GetTypes()
              .Where(x =>
                x.IsPublic &&
                x.IsClass &&
                x.Namespace.Contains($"Day{day}") &&
                x.Name == lesson
              )
              .SingleOrDefault();

            if (l is null)
            {
                return;
            }

            var instance = Activator.CreateInstance(l);

            l.InvokeMember(
              "Exercise",
              BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod,
              null,
              instance,
              null
            );
        }
    }
}