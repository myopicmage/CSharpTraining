# Lesson 9 - Constructors

Constructors are special methods that return a new instance of a class.

## Constructors

Constructors are always named the **exact same thing** as their class. They follow most of the same rules as methods, but with a few differences. If a class has multiple constructors, it just considers them overloads of the same constructor. This means that only one constructor will be called, unless you explicitly call another one. 

C# will automatically generate empty, parameterless constructors for you, but only if you do not supply any constructors of your own. If you do supply any other constructor, you must supply a parameterless constructor explicitly if you want to have it.

```csharp
class MyClass
{
  public string MyProperty { get; set; }

  // C# will automatically generate this constructor for you 
  // IF you don't supply any constructors
  // Notice that it looks just like a method without a return type
  public MyClass() { }

  // You can have multiple constructors for a class
  public MyClass(string prop)
  {
    MyProperty = prop;
  }

  // you can even mark your constructors as private or protected
  // this is handy for immutable classes, or classes using the factory pattern
  private MyClass(int secret) { }

  // If you leave the access modifier off of a constructor, it is marked internal
  // meaning public to the assembly
  MyClass() { }

  // If, for some reason, you want one constructor to call another constructor
  // you can use this syntax. It will choose the appropriate constructor based on 
  // the same rules as method overloading. In this case, it will call MyClass(string prop)
  MyClass(string arg1, string arg2) : this(arg1) { }
}
```

The primary use of a constructor is to do the minimum amount of setup required for a class. **Constructors should be as short as possible, and should not be able to fail**. Constructors are a good place to set default or initial values for your properties, but should do little-to-no other work.

```csharp
class Processor
{
  private Config someConfig { get; set; }
  private List<Jobs> jobQueue { get; set; }

  // Good. Just makes sure the properties will not be null
  public Processor()
  {
    someConfig = new Config();
    jobQueue = new List<Jobs>();
  }

  // Not good. These methods may fail or take too much time
  public Processor(Job firstJob, Config initialConfig)
  {
    someConfig = initialConfig.GenerateConfig();
    jobQueue = firstJob.ToQueue();
  }
}
```

## Auto clean up

If you don't want to rely on the garbage collector to clean up a class when you're done with it, you can use an `interface` called `IDisposable` (more on interfaces soon). This is the closest thing to PHP's `__destruct()`. C# **does** have destructors similar to C++'s destructors (though they're called Finalizers), but they're only for unmanaged code and should not be used.

```csharp
// This is preferred
class MyClass : IDisposable
{
  MyClass() { }

  public void Dispose()
  {
    // implement cleanup code here
  }
}
```

# Exercise

1. Create a `Car` class.
2. Give it `string make`, `string model`, and `string[] features` properties.
3. Give it these public constructors
    1. Parameterless constructor
    2. Constructor that takes in a make
    3. Constructor that takes in a make and model
    4. Constructor that takes in make, model, and features
4. Give it a method that lists its features. What happens if you forget to initialize `features`?
5. Implement `ToString()` for `Car`.
6. Mark all of the constructors as `private`. What happens to your code?
7. Create a `static` method called `MakeCar()` that takes in a make, model, and array of features and returns a new `Car`.. What is its return type? How do you get it to make a new `Car`?

[Next Lesson](11-Inheritance.md)