# Lesson 3 - IEnumerable and Iterators

## `IEnumerable` and `IEnumerable<T>`

In the early days of .NET (1.0 and 1.1), there were no generics. All collections were effectively type erased. .NET 2.0 added generics, in the single largest breaking change to .NET ever. As such, even though `IEnumerable` and `System.Collections` still exist, they're considered deprecated, and shouldn't be used in favor of `IEnumerable<T>` and `System.Collections.Generic`.

## Custom implementations of `IEnumerable<T>`

As you found out in the previous exercise, `IEnumerable<T>` requires one method: `IEnumerator<T> GetEnumerator()`. `IEnumerable<T>` implements `IEnumerable` (the non-generic version) for compatibility reasons. It allows generic collections to be passed to methods that are not generic.

Originally, `IEnumerable<T>` required you to also implement `IEnumerator<T>`. `IEnumerator<T>` requires three things: a property called `Current`, a method named `MoveNext()`, and a method named `Reset()`. Using these things, an `IEnumerator<T>` can move through a collection.

However, C# has added a concept of custom iterators, using the keyword `yield return`. While it is unlikely that you will ever need to implement a custom iterator, it is helpful to know how they work under the hood.

```csharp
// we'll specify the generic type parameter so we don't have to worry about comparing things
public class SortedList : IEnumerable<int> {
  private List<int> ints { get; set; }

  // to create a SortedList, you need to give it an unsorted list
  public SortedList(List<int> unsortedInts) {
    ints = unsortedInts;
  }

  // using yield return means that C# will automatically generate a proper implementation of 
  // IEnumerator<T>
  public IEnumerator<int> GetEnumerator() {
    // sort the list so we can live up to our name
    ints.Sort(); // this will use the default comparer

    // loop through our now-sorted list, allowing foreach to take each number individually
    foreach (var num in ints) {
      yield return num;
    }
  }

  // the non-generic implementation just piggybacks off of the generic implementation
  public IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
```

## WHYEnumerable

`IEnumerable<T>` is the basis for essentially every collection in C#. As we move forward with our lessons on LINQ and collections, please keep in mind that almost everything is implemented in terms of `IEnumerable<T>`.

# Exercise

No real exercise for this, however there are a couple of things to reference

1. [Example code on custom iterators using `yield`](https://code.msdn.microsoft.com/Generics-Sample-C-9b41a192/sourcecode?fileId=46476&pathId=1364935593)
2. (Optional, but recommended) [Eric Lippert's series on iterator blocks](https://blogs.msdn.microsoft.com/ericlippert/tag/iterators/). Eric Lippert was the head of the C# design team until C# 5. This series goes fairly in-depth about iterators and how they work in C#.

[Next Lesson](4-Static.md)