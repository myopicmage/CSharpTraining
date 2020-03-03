# Lesson 5 - Extension Methods

Extension methods are C#'s way of allowing you to modify classes you do not control.

## Why extension methods?

C# generally does not allow a class to be re-opened once it has been defined (the one exception being `partial` classes). Also, many of the built-in classes are `sealed`, meaning you can't inherit from them. This is by design. Ask any ruby developer about monkey-patching, and they will likely be able to tell you horror stories. 

## Enter extension methods. 

Extension methods allow you to extend the functionality of another class. They're contained in their own namespace, so they do not globally affect your application. They look just like instance methods (aside from a small mark in intellisense). Extension methods are `static`, and defined in a `static class`.

Extension methods are considered lower priority than methods defined directly on the class. That is, if you define an extension method with the same signature as a method defined directly on the class, **your extension method will never be called**.

## Defining an extension method

Extension methods are `static` methods defined inside of a `static class`. The class they extend is always the first parameter to the method, and marked with the `this` keyword.

```csharp
// Extensions.cs
namespace MyApp.Extensions {
  public static class Extensions {
    // the extension method needs to be visible to the calling code, hence public
    public static int WordCount(this string str) =>
      str
        // split the string on every instance of space, ., or ?, removing any empty entries
        .Split(new [] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries) 
        .Length;

    // extension methods can take arguments
    public static bool IsTheSameAs(this string str, string str2) => str == str2;
  }
}

// Main.cs
using MyApp.Extensions; // we need to make sure the Extensions class is in scope

namespace MyApp {
  public class Program {
    public static void Main() {
      var words = Console.ReadLine();

      // WordCount() becomes a method usable like any instance method of a string
      Console.WriteLine($"You entered {words.WordCount()} words");

      // Same with IsTheSameAs()
      words.IsTheSameAs("this is a sentence");
    }
  }
}
```

# Exercise

1. Take the code you wrote to reverse a string yesterday and implement it as an extension method
2. In your Lesson5.cs file, create a new array. 
3. On a new line, reference your array, and press `.` (assuming an `int[]` named `ints`, your line should be `int.`). Visual Studio intellisense should appear. How many methods do you see?
4. Add `using System.Linq;` to the top of the file, and bring up intellisense on your array again. What do you see now?
5. Create an extension method on `int` called `Plus()` that takes in an `int` and adds the two numbers together. Can you chain calls to this method?

[Next Lesson](6-Lambdas.md)