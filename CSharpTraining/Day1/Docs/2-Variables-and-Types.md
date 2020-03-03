# Lesson 2 - Variables and Types

C# has a strong, static type system. Everything has a type, and variables must remain the same type once declared. Types are generally not automatically coerced, except in certain cases where there would be no data loss, to ease developer productivity. 

This is a good example of the C# team's focus when designing the language. They aim for correctness, but try to stay out of your way.

## Built-in types

C# has several common built-in types:

- `int` - 32-bit signed integer
- `long` - 64-bit signed integer
- `double` - double-precision floating-point number
- `decimal` - high precision floating point number considered safe for finance
- `bool` - a boolean value, either `true` or `false`
- `string` - immutable, automatically interned string type, defaulting to UTF-16
- `char` - a single UTF-16 code point (character)

There are several more (including unsigned variants and smaller-bit numbers), but these are the most common. They're also aliases for their "true" type, as defined by .NET:

- `int` - `System.Int32`
- `long` - `System.Int64`
- `double` - `System.Double`
- `decimal` - `System.Decimal`
- `bool` - `System.Boolean`
- `string` - `System.String`
- `char` - `System.Char`

The full list can be found [here](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/built-in-types-table).

Convention says to use the lower case variants for the built-in types. 

**Note**: Unlike more dynamic languages, C# does not have a concept of "truthiness" or "falsiness."

## Defining a variable

Variable declarations are C/Java-style.

```csharp
type name = value;

int zero = 0;
double megaZero = 0.0;
DateTime year2000 = new DateTime(2000, 1, 1);
```

That's a lot of repetition! C# thankfully has the `var` keyword, which allows for simple type inference.

```csharp
// int
var zero = 0;
// double
var megaZero = 0.0;
// DateTime
var year2000 = new DateTime(2000, 1, 1);
```

All of these examples will be inferred to have the correct type. `var` gets very handy when you're working with things like `Dictionary<string, MyObject>`.

`var`, however, is slightly controversial, in that it can make it more difficult to scan code outside of the context of an IDE. Even though visual studio will tell you what the result of `var foo = bar();` is, it may not be immediately obvious what the return type of `bar()` is to someone who is just reading the code. **While var is very commonly used, follow your team's standards, and sometimes it's necessary to use your best judgement.**

## A note on scope

In C#, variables are block scoped. This is like Java and variables declared with `let` or `const` in Javascript. 

```csharp
public void Foo() 
{
  var x = 0;

  if (true) 
  { // a { marks the opening of a new scope
    x = x + 1; // x is valid here

    var y = 1;

    Bar(y); // y is valid here
  } // end of scope

  Baz(y); // this is invalid. y is not in scope
}

public void OtherFoo() 
{
  x = x + 1; // x is not in scope here
}
```

Scopes enclose other scopes. That is, an outer scope is valid inside of an inner scope, and all variables and functions from the outer scope are available in the inner scope. Unlike PHP, you do not need a keyword like `global` to use a variable from an enclosing scope. 

```csharp
public class MyClass
{
  // this instance variable is available to every member of the class
  private string InstanceVar { get; set; }

  public void MyFunc()
  {
    // x is available to all of MyFunc, but not OtherFunc
    var x = 0;

    if (true) 
    {
      // you cannot re-declare a variable inside of an inner scope
      var x = 1; // this is invalid
    }
  }

  public void OtherFunc()
  {
    // InstanceVar is valid here
    Console.WriteLine(InstanceVar);
  }
}
```

## Strings

Strings in C# are:

- immutable. Operations on strings generally return a new string, sometimes invisibly.
- interned. Every copy of a string has exactly one representation in memory. This has performance implications.
- UTF-16.
- defined by "". '' defines a `char`.

C# allows for 3 kinds of strings:

- Regular strings, defined with `""`. They allow common escape sequences, such as `\n`, and must be single line.
- Literal strings, defined with `@""`. Literal strings are allowed to be multi-line, and will print every character inside of them literally, except for `"`.
- Interpolated strings, defined with `$""`. Instead of concatenating strings and variables together (`var hello = "Hello, " + name;`), you can put variables inside of `{}` in an interpolated string to define it inline: `var hello = $"Hello, {name}";`

You can combine literal and interpolated strings with `$@""`

# Exercise

1. Open `Lesson2.cs`
2. Inside of `Exercise()`, create a few variables. Use `Console.Write()` or `Console.WriteLine()` to print them to the console.
3. Hover over their names. Are they the type you expect?

[Next Lesson](3-Operators.md)