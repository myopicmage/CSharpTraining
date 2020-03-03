# Lesson 7 - Methods

Methods are the fundamental unit of reusable code in C#. Always associated with a larger type, methods are special in that they have access to all of their enclosing context.

## A basic method

Methods always follow the same structure

```csharp
access_modifier other_modifiers return_type Name(arguments)
{
  // code
}

// access modifier
public 
// other modifiers
static
// return type
void
// Name
Main
// Arguments
(string[] args)
{

}
```

If no access modifier is specified, methods are **private** by default. 

## Method to the madness

Methods are always encapsulated within a scope. Please note, methods are called using `.` syntax, not `->` as in PHP.

```csharp
class MyClass 
{
  public void MyMethod()
  {

  }
}

class OtherClass
{
  public void OtherMethod()
  {
    // Incorrect
    MyMethod();

    // Correct
    var myClass = new MyClass();
    myClass.MyMethod();
  }
}
```

Because of the scoping rules, methods have access to a class's internals.

```csharp
class MyClass
{
  // private properties are not accessible outside of the class
  private string secret { get; set; }

  // however, you can get it and set it using methods
  public string GetSecret()
  {
    return secret;
  }

  public void SetSecret(string value)
  {
    secret = value;
  }
}
```

C#, since 6, has started focusing on syntax sugar to help developer productivity. If your method contains nothing but an expression, you can use `=>` syntax

```csharp
class MyClass
{
  private string thing { get; set; }

  // this will immediately return `thing`
  public string GetThing() => thing;

  // immediately returns the result of `a + b`
  public int Add(int a, int b) => a + b;
}
```

Method arguments can be optional, by specifying a default value. The default value has to be a compile time constant. Optional arguments must come **after** required positional arguments.

```csharp
// valid, "" is known at compile time
public void DoThing(string useThis = "") { }

// valid, null is known at compile time
public void TryThing(string useThis = null) { }

// invalid, GetString() is not a compile-time value
public void DefaultThing(string useThis = GetString()) { }

// invalid, optional arguments must be specified after required positional arguments
public void WrongThing(string first = "", string second) { }

// to call any of these methods, you can specify the argument, or skip it
DoThing("hello"); // okay!
DoThing(); // also okay!
```

Finally, methods can be called using named arguments. This is really handy for methods that take several `bool`s for configuration flags, or if you want to skip optional arguments

```csharp
public Result Configure(bool flag1 = true, bool flag2 = false, bool flag3 = true) { }

// will call Configure with default `flag1` and `flag3`
Configure(flag2: true);

// equivalent to
Configure(true, true, true);
```

If you specify a named argument, it has to come **after** all of the positional arguments

```csharp
// valid
Configure(false, flag3: false);

// invalid
Configure(flag3: false, true);
```

## Overloading

If, for some reason, you need a method with the same name that accepts different parameters, C# supports method overloading. Common use cases are math operations that deal with different types of numbers, or a method that needs to accept different numbers of parameters.

C# will automatically pick the correct overload, based on the arguments.

```csharp
public int Add(int a, int b) { }
public double Add(double a, double b) { }

// Different argument sets
// Note: this is a contrived example that could be replicated with default arguments, but it serves as an example
public Result Calculate(Config a, Arg b, Dog fido) { }
public Result Calculate(Config a, Arg b) { }
public Result Calculate(Config a, Dog fido) { }

// Different number of arguments
// this is less common for methods, but it does happen
public string Join(string a, string b) { }
public string Join(string a, string b, string c) { }
public string Join(string a, string b, string c, string d) { }
```

## `static`

In PHP, you can call any method statically using `Class::Method()`. C# does not allow this. To use a static method (that is, without an object instance), the method must be explicitly marked static. This comes with a few caveats

1. The method must be called on the class directly
2. Static methods cannot use `this`
3. Static methods cannot reference non-static members (but non-static members can reference static methods)

```csharp
class Person
{
  public static string GetSpecies() => "Human";

  // note: non-static
  public string FullName { get; set; }

  // valid, non-static referencing a static
  public string GetName() => $"{FullName} is a {GetSpecies()}";

  // invalid, static referencing non-static
  public static string GetInfo() => $"{FullName} is a {GetSpecies() who works at {Company}}";
}

public static void Main(string[] args)
{
  var steven = new Person {
    FullName = "Steven"
  };

  // instance method
  Console.WriteLine(steven.GetName());

  // static method. note it's called the same way, just directly on the class
  Console.WriteLine(Person.GetSpecies());
}
```

## A note about static

`static` is a keyword used in a few different places. You can mark properties, methods, and even classes as `static`, but each one has its own caveats.

In general, except for the helper class pattern, it is best to avoid `static` when possible. We'll talk more about it [tomorrow](../../Day2/Docs/4-Static.md).

# Exercise

1. Create a class to translate between Fahrenheit and Celsius
2. Write `ToFahrenheit()`. It should accept a double.
3. Write `ToCelsius()`. It should accept a `double` or an `int`.
4. Write your methods using `=>` syntax.

Extra credit: what does the `params` keyword do? Write a method that uses it.

[Next lesson](9-Properties.md)