# Lesson 11 - References and Values

C# is a language chock full of references. It also has values (unsurprisingly). Unfortunately, they act a little differently. 

## References v. values

There are two basic types in C#: references and values. 

The basic reference type is a `class`. References point to values stored on the heap. References **can** be `null`.

The basic value type is a `struct`. Values are stored on the stack. "Primitive" types and literals are all values. Values **cannot** be `null` unless explicitly marked nullable.

Values by example

```csharp
// values
var a = 2; // a is a value that holds 1
var b = a; // a is copied into b

// a: 2 b: 2
Console.WriteLine($"a: {a} b: {b}");
a += 1;

// a: 3 b: 2
Console.WriteLine($"a: {a} b: {b}");
```

References by example

```csharp
class A 
{
  public int num { get; set; } = 1;
}

var a = new A();

// the reference to a is copied into b. b now points to the same
// value as a
var b = a; 

// a: 1 b: 1
Console.WriteLine($"a: {a.num} b: {b.num}");
a.num += 1;

// a: 2 b: 2
Console.WriteLine($"a: {a.num} b: {b.num}");
```

## Defining a value type

To define a value type, use `struct`. `struct`s act a lot like classes, except they are allocated on the stack and do not allow inheritance. `struct`s are lightweight, and passed by value, but the benefit of using a `struct` decreases the more members you give it.

```csharp
public struct Coordinates
{
  public int x;
  public int y;

  public Coordinates(int x, int y)
  {
    this.x = x;
    this.y = y;
  }
}
```

Everything inside of a `struct` must be initialized. If it's not, it is initialized with its default value according to its type (so an `int` would be 0, a `class` would be `null`, etc.).

## Passing things around

Unless explicitly marked otherwise, every function call in C# passes its arguments by value.

```csharp
public void Increment(int a)
{
  a += 1;
  Console.WriteLine($"Incremented: {a}");
}

public static void Main()
{
  var a = 1;

  Console.WriteLine(a); // 1
  Increment(a); // Incremented: 2
  Console.WriteLine(a); // 1
}
```

To pass an argument by reference, there are two keywords to remember: `out` and `ref`

`out` parameters are used when a method needs to return more than one value. They're very common in the `TryParse()` methods found on most of the built-in data types. `out` parameters must be declared at both method definition and call site. The variable passed as an `out` parameter does not need to be initialized.

```csharp
// very common use case
var input = Console.ReadLine();

// pre-C#6
int result = 0;
if (int.TryParse(input, out result))

// Modern C# allows you to declare the out parameter inline, along with using
// var instead of the actual type
if (int.TryParse(input, out var result)) 
{
  // int.TryParse() returns a `bool` that says whether or not the parse succeeded
  // the parsed int is put into result, which is available in scope
}
```

`ref` parameters, by contrast, are for when you want to pass an existing variable by reference. `ref` parameters **must** be initialized before use. This is likely the closest keyword to PHP's `&`. `ref` is fairly uncommon, and is mostly a performance optimization or partial refactor.

```csharp
var myConfig = new AppConfig(); // some big, complicated object

Modify(ref myConfig); // `ref` must be specified at the call site

myConfig.PrintToConsole(); // this will reflect any changes made by Modify()
```

**A note about `ref`**: `ref` has recently been upgraded to allow for more use cases. `ref struct` and `ref return` are low-level performance optimizations and can be ignored for the purposes of app code.

## Other value types

The other big value type is the `enum`. `enum`s are sets of named integer values. They're very handy when you want to cheaply name an integer value in a type-safe way. They are zero-indexed, and increase based on source order.

```csharp
public enum DayOfWeek
{
  Monday, // 0
  Tuesday, // 1 
  Wednesday, // 2
  Thursday,
  Friday,
  Saturday,
  Sunday
}

public void SendEmail(DayOfWeek dayToSend) { }

// the compiler will enforce that you pass in a DayOfWeek
SendEmail(DayOfWeek.Friday);
```

As a note, you can explicitly set the values of enum members

```csharp
public enum Game
{
  ApexLegends = 2,
  BioShock = 0,
  Borderlands = 1,
  TheOuterWorlds = 3, // trailing commas are allowed, and encouraged in some circles
}
```

## `null`

You're probably familiar with the concept of `null` already. Any reference type in C# will be `null` if it is uninitialized. Value types can be explicitly marked nullable. Checking for `null` is pretty simple, depending on what type you're dealing with. Defensive coding in C# says to check for `null` early and often.

```csharp
Dog fido = null; // any reference type can be null
int? numDogs = null; // to mark a value type as nullable, use the ?

// to check a reference type for null

// this is the tried and true, bread and butter way
if (fido == null)

// modern C# recommendation is to use the is operator, since it bypasses 
// any possible overloading of the == operator
if (fido is null)

// if a value type is nullable, the underlying type changes slightly
// you gain access to two new properties: HasValue and Value
if (numDogs.HasValue)
{
  DoSomeThingWith(numDogs.Value);
}

// C# gotcha: bool? cannot be implicitly converted to bool
bool works = true;
bool? maybeWorks = true;

if (works) // this is true

if (maybeWorks) // this will not compile! bool? cannot be implicitly converted to bool

if (maybeWorks == true) // this works, since it's an explicit check
if (maybeWorks.HasValue && maybeWorks.Value) // this also works
```

There are a couple of convenience operators for dealing with `null`: `?.` and `??`

`?.` is called the "null conditional operator." You will occasionally hear it referred to as "monadic null checking" in some older documentation. Put simply, it allows you to collapse null checking to a single check in a chain potentially containing `null`.

```csharp
class Department {
  public string Name { get; set; } // string is a reference type, and can therefore be null
}

class Person {
  public Department Department { get; set; } // Department is a reference type and can also be null
}

public string GetName(Person p) {
  // old style
  if (p.Department != null) {
    if (p.Department.Name != null) {
      return p.Department.Name;
    }
  }

  // new style
  if (p?.Department?.Name != null) {
    return p.Department.Name;
  }

  return "<no name>";
}
```

`??` is called the null coalescing operator. It is shorthand for "if null." It is a handy way of returning the right hand side of the operator if the left hand side is null.

```csharp
public string GetName(Person p) {
  // if style
  if (p?.Department?.Name != null) {
    return p.Department.Name;
  }

  return "<no name>";

  // using ??
  return p?.Department?.Name ?? "<no name>"; // return <no name> if p.Department.Name is null
}
```

Please see [this](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-) for more information on `?.` and [this](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator) for more information on `??`.

**Note**: `??=` is a C#8 feature, and should be ignored.

# Exercise

1. Write an `Increment()` method that takes in a `ref int a`
2. Write a `GetNumber()` method that takes in an `out int num` and puts a random number into `num`
3. Write a simple class called `Developer` (or copy yours from a previous exercise). In `Exercise()`, create a `Developer` variable and set it to null, then try to access one of its properties. Run the application. What happens?
4. Create an `enum` named `SkillLevel` and it give it values of `Novice`, `Intermediate`, `Advanced`, and `RockStarNinjaGuru`. 
5. Add a property to your `Developer` class that will keep track of a developer's skill level using the enum you just created.

[Next](13-Tying-It-All-Together.md)