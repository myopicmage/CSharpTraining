# Lesson 1 - Basic Syntax

C# is an object-oriented, C-style language. It uses braces to define blocks of code, and uses a great deal of syntax that you will likely find familiar if you have programmed before. It is intentionally modeled after Java, though the two languages have diverged somewhat in recent years.

Like every other language, C# has several keywords, some basic ones being explained here.

## Terms

A useful one right off the bat: `Console.WriteLine()` is how you print to the console. There's the sibling `Console.Write()` which does not add a new line after the output.

- `Console.WriteLine()` is equivalent to `System.out.println()` in java
- `Console.Write()` is roughly equivalent to `print` and `echo` in php, and `System.out.print()` in java
- Both `Console.Write()` and `Console.WriteLine()` accept input similar to php and java's `printf()` functions. The reference is [here](https://docs.microsoft.com/en-us/dotnet/api/system.console.writeline?view=netframework-4.8#System_Console_WriteLine_System_String_System_Object_System_Object_)

### Comments

Comments are a very important part of every programming language. C# allows:

- Single line comments with `//`
- Multi-line comments with `/* */`
- XMLDoc comments with `///` (these are roughly equivalent to `/** */` comments in java and phpdoc)

### `class`

A `class` is the basic unit of code in C#. Like Java, C# is fully object-oriented. All code must be contained in a class. A `class` defines behavior, and is used by creating new objects.

### `namespace`

`namespace` opens a new namespace, which is a way of grouping code. Code in the same namespace does not require a `using` to be able to use it. Like java, but unlike php, `namespace`s are separated by `.`, not `/`.

ex. 

```csharp
// AClass.cs
namespace A {
  public class AClass {

  }
}

// BClass.cs
namespace B {
  public class BClass {
    // this name does not need to be qualified
    public AClass MyClass { get; set; }
  }
}
```
By convention, namespaces follow folder hierarchy. If this file were a C# file, its namespace would be `CSharpTraining.Day1.Lesson1`. You can see an example of this in the associated `.cs` file.

Namespaces can be nested, but parent namespaces do not automatically contain child namespaces.

### `using`

`using` is how you can reference code from other namespaces. You can choose to fully qualify the name (`new System.DateTime()`) or you can use a `using` to bring the namespace into the current scope:

```csharp
using System;

namespace Example {
  public class ExampleClass {
    static void TestMethod() {
      var myDate = new DateTime();
    }
  }
}
```

`using` has two other uses:

1. You can use it to automatically clean up a resource once you're done with it.

```csharp
using (var myFile = new StreamReader(someFileName)) {
  doStuffWithFile(myFile);
}
// once the code exits this block, the stream will be closed
```

2. You can rename imports like so, usually to avoid name collisions. This is uncommon, but valid.

```csharp
using S = Some.Long.Namespace.That.Is.Weird;
```

### `static`

`static` has several implications depending on where it's used, but in this case it means that the method `DoAThing()` can be used without an object. In other languages, `static` methods are sometimes called class methods. We'll talk more about `static` later.

### `void`

`void` is a return type. It means this method will not return anything. 

## Summary

If you look at `Program.cs` again, you'll notice a very specific structure.

```csharp
using System;

namespace CSharpTraining {
  class Program {
    static void Main(string[] args) {
      Console.WriteLine("Hello, world!");
    }
  }
}
```

Every .NET program will look for a `static` method named `Main()` as its entry point, the first piece of user code to be called. It doesn't need to be  in a specific namespace or class, but it must be a `static` method named `Main()`. The `string[] args` is optional. 

**Advanced note**: for async programming purposes, there is a different signature for `Main()`, but that's for a later lesson.

# Exercise

Visual Studio is known for its good, immediate feedback. Try breaking the file, and reading some error messages.

1. Remove a brace or two. What happens?
2. What happens if you change the word `void` to `int` or `string`?
3. Put your cursor on a new line above `Exercise()`. Type three `///`. What happens?

[Next Lesson](2-Variables-and-Types.md)