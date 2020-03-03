# Lesson 5 - Control Flow

You've probably already seen `if` in the Pluralsight videos, but there's more control flow constructs than that. 

## Statements vs. expressions

C#, like many imperative languages before it, is more statement-oriented than expression-oriented.

- A statement is something that does not return a value ([Official Definition](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/statements))
- An expression returns a value ([Official Definition](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expressions))

## `if`

Your basic conditional. Accepts an expression that evaluates to `bool`. C# does not have a concept of "truthiness" or "falsiness," so you may need to do more explicit comparisons than other languages.

`if` is a statement.

```csharp
var x = 0;
var y = 1;

// This is invalid. x cannot be implicitly converted to `bool`
if (x) 
{

}

// This is valid. < returns a bool
if (x < y)
{

}

// This is valid, but the expression returns `false`
if (x == y)
{
  doA();
}
else
{
  doB(); // doB() will be run
}

// if/else if/else
if (x == 2)
{

}
else if (x == 3)
{

}
else
{

}

// if you're performing a very simple operation, you can use a ternary expression
var AOrB = cond == "A" ? "A" : "B";

// equivalent to
var AOrB = "";
if (cond) 
{
  AOrB = "A";
} 
else
{
  AOrB = "B";
}
```

## `switch`

`switch` is a very powerful construct in C#. Switch `case`s **require** break statements if the `case` does not `return`. C# only allows fall-through if a `case` is empty. We'll talk more about `switch` later.

```csharp
// this is called the constant pattern. It matches based on the value of `someStr`
switch (someStr) {
  case "A":
    doA();
    break;
  case "B":
    // if you return, you don't need a `break`
    return doB();
  case "C": // fall-through is allowed if the `case` is empty
  case "D": 
    doD();
  default: { // you can wrap your cases in braces, but this is uncommon
    panic();
    break;
  }
}
```

## `while`

`while` loops are the most basic loop in C#. They work just like they do in php, java, and javascript, though the condition may require more explicit checks, since it requires a bool value.

```csharp
var counter = 1;

while (counter <= 10)
{
  Console.WriteLine($"Counter: {counter}");
  counter++;
}
```

## `do`/`while`

A `do`/`while` loop is just like a `while` loop, except the condition runs at the end of the loop instead of the beginning, guaranteeing at least one iteration of the loop.

```csharp
var counter = 1;

do
{
  Console.WriteLine($"Counter: {counter}");
  counter++;
} while (counter <= 10);
```

## `for`

A `for` loop is likely the second-most common loop in C#. Given an index variable, a condition, and an increment expression, it will iterate a fixed number of times.

```csharp
// index variable; condition; increment expression
for (var index = 1; i <= 10; i++) 
{
  Console.WriteLine($"Counter: {index}");
}
```

## `foreach`

A `foreach` loop is likely the most common loop in C#. It iterates over anything that implements the `IEnumerable<T>` interface, which is most collections in C#.

`IEnumerable<T>` is a generic way of referring to something that can be enumerated (counted). We'll go into more detail tomorrow.

```csharp
var myArray = new [] { 1, 2, 3 };

foreach (var element in myArray)
{
  Console.WriteLine($"Element: {element}");
}

// equivalent to
for (var i = 0; i < myArray.Length; i++)
{
  var element = myArray[i];
  Console.WriteLine($"Element: {element}");
}
```

Please note the syntax is backwards compared to php's `foreach`. Also, the `element` variable created for the loop only exists inside the scope created by the loop.

### A note on `IEnumerable<T>`

`IEnumerable<T>` is an interface used for every enumerable collection in C#. It is very common in any code that uses collections of any sort. We'll talk more about it later.

# Exercise

1. Create an array of `int`s. Sum it.
2. Iterate through an array in reverse.
3. You can index into a `string` the same way you would an array (`str[index]`). Implement the `Reverse()` method.

[Next](6-Project-1.md)