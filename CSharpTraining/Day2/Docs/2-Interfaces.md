# Lesson 2 - Interfaces

`interface`s define APIs that a class must implement. 

## Defining an interface

Defining an `interface` is very similar to defining a `class`, `struct`, or `enum`. By convention, interfaces start with a capital I, and implementing classes are named however they want to be, in contrast to Java with its -able and -er suffixes.

Since interfaces define APIs, methods do not have access modifiers. It only makes sense for them to be `public`.

```CSharp
public interface IWriter {
  // interfaces can contain public properties
  string Topic { get; set; }

  // also methods
  string Write(); 
}
```

`interface`s can also contain events and indexers, but those are rarely used in our code. You can read the [official docs](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface) for more information.

## `interface` v. `abstract class`

`interface` and `abstract class` have a lot of crossover. They both cannot be instantiated directly, serve as a generic way of accepting multiple different types, and specify specific behavior. 

- A `class` can only inherit from **one** `abstract class`, but can implement as many `interface`s as they want
- `struct`s cannot inherit from `abstract class`es, but they can implement `interface`s. 
- `abstract class`es can specify default behavior, but `interface`s cannot*

**Note**: As of C# 8.0, `interface`s can provide some default implementations of their methods, but **we cannot use any C# 8 features**.

## Using an interface

Implementing an `interface` does not have any special syntax the way it does in Java. There's no `implements` keyword. The only caveat is the ordering.

Anything that implements an interface must implement the **entire** interface. Visual Studio will actively help with this.

```csharp
// it looks just like inheriting from a class!
class Writer : IWriter { }

// you can inherit from/implement multiple interfaces
class Writer : IWriter, IDoer, IService { }

// if you are also inheriting from a base class, it must come FIRST
class Writer : BaseOutputter, IWriter { }

// structs can implement interfaces, even though they can't use inheritance
struct SimpleWriter : IWriter { }
```

## Some common interfaces

### `IEnumerable<T>`

Possibly the single most common interface you will encounter in real-world C# is `IEnumerable<T>`. This is a different (newer, better) interface than `IEnumerable`. The `<T>` is a type parameter. 

Common collections like `List<T>` and arrays implement `IEnumerable<T>`, meaning that if you want to write a method that takes in most of the common collections, you can do so like this. Implementing `IEnumerable<T>` allows a collection to use `foreach`.

```csharp
public void Process(IEnumerable<Something> collection) {

}
```

### `IDisposable`

`IDisposable` is an `interface` that marks something can be cleaned up. It is used in conjunction with `using` statements, like so

```csharp
public void Process() {
  using (var reader = File.OpenText(somePath)) {
    DoThingWithReader(reader);
  } // reader will be automatically closed once the using scope ends
}
```

`IDisposable` requires that one method be implemented: `void Dispose()`. Any cleanup work, such as closing a file handle, should be done in the `Dispose()` method.

## Explicit interfaces

If your class implements an interface method, C# is smart enough to match the method to the interface without any explicit connection.

```csharp
interface IWriter {
  void Write();
}

class Writer : IWriter {
  // satisfies IWriter
  public void Write() { }
}
```

If you have two `interface`s that require a method with the same signature, both interfaces will be satisfied by the same method.

```csharp
interface IWriter {
  void Write();
}

interface IFileHandler {
  void Write();
}

class Writer : IWriter, IFileHandler {
  // satisfies BOTH IWriter and IFileHandler
  public void Write() { }
}
```

In the case that you need to have separate implementations for the same signature, you can explicitly mark them

```csharp
class Writer : IWriter, IFileHandler {
  public void IWriter.Write() { }
  public void IFileHandler.Write() { }
}
```

If you choose to use the helpers that Visual Studio gives for implementing an interface, this is what it means when it says "implement interface explicitly." Like many optional things in C#, this is avoided by convention.

# Exercise

1. Create a simple class called `Employee`. Give it `FirstName` and `LastName` properties.
2. Create a class called `Company` with an `Employee[] Employees` property.
3. Try to implement `IEnumerable<Employee>` for `Company`. What does it require you to do?
4. Implement `IDisposable` for `Company`. What does it require you to do?

[Next Lesson](3-IEnumerable.md)