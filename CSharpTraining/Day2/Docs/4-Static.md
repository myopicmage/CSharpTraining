# Lesson 4 - Static

`static` is a keyword you'll see here and there throughout code. Depending on where it is, it has different implications.

## Basic definition

`static`, in most cases, means that there is only ever one copy of something in memory. It will be initialized the first time it is referenced, which may not be when you expect. In many cases, `static` members are initialized **far** earlier than their associated objects. This can lead to unintended behavior, especially in a long-running program like a web app.

Consider this example, paraphrased from an actual bug we found in our tests

```csharp
[Test] // this is an attribute. essentially, a flag that can be seen via reflection
class SomeTest
{
  // DateTime.Now gets the current day/time according to the local machine
  // **at the moment it is called**
  public static DateTime runTime { get; set; } = DateTime.Now;

  // code

  if (runTime.Day == DateTime.Now.Day)
  {
    Assert(ItWorked());
  }
}
```

The `runTime` property will be referenced during the test runner's scan for tests, meaning that `DateTime.Now` will be whatever date/time will be during the initial scan for tests, **not** when the test was actually run. In the case of this bug, the actual run time was several hours later, the next day (the test run was started around 11pm).

## Places you can use `static`

### `static class`

A `static class` means there will only ever be one copy of it in memory **for the entire program, including across threads**. `static class`es **cannot** be instantiated. They're a handy way of implementing the Singleton pattern. All members of a `static class` are implicitly `static`.

```csharp
public static class DangerHelpers { }
```

### `static` property

You can mark any property in a `class` or `struct` as `static`. There will only ever be one copy of them in memory, meaning that they are not thread safe, and it is dangerous to consider them mutable. A common pattern is to declare a `public static const` property for things like names or configuration values.

```csharp
public class SomeClass {
  public static string StaticProp { get; set; } // leaving this mutable is dangerous

  // this is safer since it is immutable and immediately initialized
  public static string ConfigKey { get; } = "SomeClassKey"; 

  // if you have an unchanging value like this, though, it may be better to just mark it const
  // const and static are mutually exclusive. const is a compile-time idea, while static is a run-time idea
  public const string ConfigKey = "SomeClassKey";
}
```

### `static` method

You've seen `static` methods already. They're associated with the class, instead of with an object. They cannot reference any non-`static` members. You cannot call a static method from a non-static context.

**Note**: Of all of the places `static` is used, `static` methods are the "safest," in that they are least likely to cause memory issues. They should still not be considered thread-safe, but that's fine as long as they don't modify any state.

```csharp
public class Helpers {
  public static double DoMath(double a, double b) { }
}

// used as
Helpers.DoMath(1.6, 2.8);

// this is invalid
new Helpers().DoMath(1.6, 2.8);
```

### `static` constructor

`static` constructors are optional constructors for a class. They are used to initialize any state your `static` members might need. The same rules and caveats about initialization time apply. **It is difficult, sometimes impossible, to reason about when your static constructor will run. It is better to avoid them if at all possible.**

```csharp
public class Helpers {
  // readonly properties can only be set in the constructor
  public static readonly string SomeProperty { get; }

  // who knows when this will run
  static Helpers() {
    SomeProperty = "fred";
  }
}
```

## `static` and web apps

`static` is a dangerous concept in web apps, especially ASP.NET. Each request in ASP.NET gets its own thread, which is usually enough to guarantee that requests will not interfere with each other, but **anything marked `static` should be considered thread-unsafe.** Memory and data can cross thread boundaries without you realizing it, and state can be kept when you did not mean to do it. There are some legitimate use cases for `static` (such as `HttpClient` instances, and potentially instances of `Random`), but `static` requires a great deal of care.

**If at all possible, avoid static unless you absolutely need to.**

# Exercise

1. Create a `class` called `Helpers` that implements a few basic math operations.
2. Create a `static class`. Don't mark anything inside of it as static. Try to use it. Add the static keyword to places where it would normally go. What does visual studio say?
3. Modify your `Helpers` class so that it has some non-static members.
4. Add a `static` constructor to `Helpers` with a `Console.WriteLine()` in it.
5. Add a non-`static` constructor to `Helpers`, also with a `Console.WriteLine()`.
6. Create a new instance of your `Helpers` class. When does the `static` constructor run compared to the non-`static`?

[Next Lesson](5-Extension-Methods.md)