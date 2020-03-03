# Lesson 6 - Classes

A `class` is the basic unit of code in C#. They can contain both code and data. They create new types, and also help with organization.

## Structure

A `class` is declared with the `class` keyword. By convention, classes and public class members are in PascalCase.

```csharp
class MyClass 
{

}
```

Classes can contain both code and data.

```csharp
class MyClass
{
  string MyString { get; set; }

  string GetString() 
  {
    return this.MyString;
  }
}
```

Classes help with encapsulation through access control. Classes themselves, and all of their members, can be declared `public`, `private`, `protected`, or `internal`. There are others, but these are by far the most common.

```csharp
// this class is visible to everything, so long as it is in scope
public class MyClass
{
  // all of the following conventions are used in different places for 
  // member variables. A leading _ is still occasionally recommended by Microsoft.
  // `private` means that the member is only visible to this class's internals.
  private string myString { get; set; }
  private string _myString { get; set; }
  private string m_MyString { get; set; }

  // `protected` means that this member is only visible to this class
  // and classes that inherit from it
  protected SomeConfig ThisConfig { get; set; }

  // `internal` means that this member is only visible to members of the same assembly (think project)
  internal string MyProperty { get; set; }
}
```

It's considered good form to organize your classes according to certain conventions. Generally, most classes follow the form:

1. Dependencies
2. Fields/properties
3. Constructors
4. Public methods
5. Private methods

## Using a class

Classes are blueprints, and explain how to create objects, which are instances of a class. Classes define reference types, and objects are stored on the heap.

```csharp
class Book
{
  // code
}

class Program
{
  static void Main(string[] args)
  {
    // objects are created with `new`. Since C# is a managed language, 
    // you don't need to worry about deleting them
    var myBook = new Book();

    // calling `new Book()` is like calling a function that returns a Book,
    // so you can put arguments into the () if the class allows it
    var mobyDick = new Book("Moby Dick", "Herman Melville");

    // you can set public properties using . syntax
    myBook.Title = "The Phantom Tollbooth";

    // you can read properties using the same syntax
    Console.WriteLine($"{mobyDick.Title} is by {mobyDick.Author}");

    // you've seen method syntax
    var summary = mobyDick.GenerateSummary();
  } // once an object is no longer in scope, it will be garbage collected
}
```

If your class has properties that need to be initialized at creation time, you can use object initializer syntax

```csharp
// this is just like calling new Book(), but the () become optional
var phantomTollbooth = new Book {
  Title = "The Phantom Tollbooth",
  Author = "Norton Juster"
};
```

## `this`

When defining a class, if you want to refer to other members of the class, you can use the `this` keyword. It will always refer to the current object. `this` is usually unnecessary, as C# will understand context, unless you have name collisions.

```csharp
class MyClass
{
  public int ConfigFlag { get; set; }
  private Config config { get; set; }

  public void SetConfigFlag(int ConfigFlag)
  {
    // the name of the property and the name of the parameter are the same,
    // so you need to reference the property with `this`
    this.ConfigFlag = ConfigFlag;

    // however, since there's nothing named `config`, the `this.` is unnecessary
    config.Update(this.ConfigFlag);
  }
}
```

## Free stuff

Unlike PHP, C# does not have magic methods. But, because every class automatically inherits from `System.Object`, there are some methods you get for free.

1. `Equals()`
2. `GetHashCode()`
3. `ToString()`

If you'd like to implement your own versions of these methods (usually `ToString()`), you can `override` them.

```csharp
class Person
{
  public string FirstName { get; set; }
  public string LastName { get; set; }

  // System.Object already contains a method named ToString, so you must tell the compiler
  // explicitly that you are overriding it.
  public override string ToString()
  {
    return $"{FirstName} {LastName}";
  }
}

public static void Main(string[] args)
{
  var p = new Person 
  {
    FirstName = "Some",
    LastName = "Person"
  };

  Console.WriteLine(p.ToString());
  // "Some Person"
}
```

# Exercise

1. Create a `public` class called `Animal`. Give it properties that you think make sense (like height, weight, name, species)
2. Create a `private` class called `Vehicle`. Try to use the class from inside of `Animal`. 
3. Create a class _inside_ of `Animal`. How can you access it?

[Next lesson](8-Methods.md)