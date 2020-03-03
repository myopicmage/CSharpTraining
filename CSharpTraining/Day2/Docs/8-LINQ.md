# Lesson 8 - LINQ

This is it, folks. The big one. LINQ!

## What is LINQ?

LINQ stands for Language INtegrated Query. Microsoft is still bad at naming things. LINQ is **both** the method and query syntax, since both of them compile down to the same API.

LINQ's power comes from the fact that it unifies the collection APIs for multiple different collection types. LINQ is implemented as a set of extension methods on `IEnumerable<T>`, but it also works on

- XML
- SQL
- JSON
- Whatever decides to implement it

LINQ works entirely in-memory, except when it doesn't (like LINQ to SQL or Entity Framework, where it compiles to actual SQL queries). Effectively, you can use the same API across basically everything.

## That sounds both great and complicated

Yes.

## How do I use it?

If you know SQL, you'll have a big head start with LINQ syntax. If you've used... basically any modern collection API, you'll probably have a good grasp on it.

LINQ is a set of higher-order methods. They all take in lambdas. The handy thing is that they all return `IEnumerable<T>`, so they can be nicely chained.

```csharp
// this is code from _this very repo_
Assembly.GetExecutingAssembly() // ignore this
  .GetTypes() // ignore this
  // Where == filter
  .Where(x => x.IsPublic && x.IsClass && x.Namespace.Contains($"Day{day}"))
  // Select == map
  .Select(x => x.Name)
  // OrderBy == SQL order by or lodash sortBy
  .OrderBy(x => x)
  // ToArray changes the IEnumerable<T> to an explicit array and forces the query to execute
  .ToArray();
```

One thing to note is that LINQ is lazily evaluated. Most of the methods will not actually perform their operations until the data is actually needed. This can have performance implications (which will be further explained in this lesson's exercise), but it also means that a single query does **not** need to be defined all at once.

```csharp
public IEnumerable<Dog> Search(string name = null, int? maxAge = null, bool adopted = false, int maxResults = 10) {
  // we'll ignore error checking

  // when building queries like this, the type changes. This Select will be optimized away,
  // but it will properly change the type and set up our work
  var foundDogs = this.dogs.Select(x => x); 

  if (!string.IsNullOrEmpty(name)) {
    foundDogs = foundDogs.Where(x => x.Name.Contains(name));
  }

  if (maxAge.HasValue) {
    foundDogs = foundDogs.Where(x => x.Age <= maxAge);
  }

  if (!adopted) {
    foundDogs = foundDogs.Where(x => x.AdoptionStatus != AdoptionStatus.Adopted);
  } else {
    foundDogs = foundDogs.Where(x => x.AdoptionStatus == AdoptionStatus.Adopted);
  }

  return foundDogs
    .OrderBy(x => x.Name)
    .Take(maxResults) // Take() gives you the first members of the collection
    .ToList(); // this ToList() forces the query we've built up over the course of the method to actually execute
}
```

You could, in theory, pass an `IQueryable<T>` around between methods, but it is uncommon, since intermediate LINQ types can get strange quickly. The query in the above method will **not** actually execute until the final `ToList()`. This allows for optimizations to be performed (such as removing the initial useless `Select()`), and for the query to be built up in small pieces over time.

This is not the most efficient search in the world (in fact it gets very unwieldy very quickly if you add more filters), but it is handy, and reasonably performant if it is defined properly.

Let's go into a little more detail about the individual methods.

### `Select()` 

Performs an operation on each item in the `IEnumerable<T>`, and returns a new `IEnumerable<T>` with the results

```csharp
new [] { 1, 2, 3 }.Select(x => x + 1)
// [2, 3, 4]
```

### `Where()`

Returns a new `IEnumerable<T>` with the elements from the original `IEnumerable<T>` that meet a condition.

```csharp
new [] { 1, 2, 3 }.Where(x => x > 1)
// [2, 3]
```

### `OrderBy()`

Returns a new `IEnumerable<T>` which has been sorted by a specific key in the collection.

```csharp
var dogs = new List<Dog> {
  new Dog("riley", 3),
  new Dog("mason", 5),
  new Dog("nestle", 5),
};

dogs.OrderBy(x => x.Name);
// mason, nestle, riley

dogs.OrderByDescending(x => x.Name);
// riley, nestle, mason

dogs
  .OrderBy(x => x.Age)
  .ThenBy(x => x.Name);
// riley, mason, nestle
```

### `Aggregate()`

`Aggregate()` is Microsoft's name for `reduce()` or `fold()`.

```csharp
var ints = new [] { 1, 2, 3, 4, 5 };
var sum = ints.Aggregate(0, (accumulator, current) => accumulator += current);
Console.WriteLine($"Sum: {sum}");
// Sum: 15
```

### `ToList()`, `ToArray()`, etc

The various `To*()` methods will convert the `IEnumerable<T>` to an explicit type. The main benefit of this is that you can force your LINQ query to run when you need it to.

Converting to an array can be an expensive operation, but it is a more efficient data type than a `List<T>`.

## Query syntax

If method syntax is not your forte, LINQ can also be used in a SQL-like query syntax.

```csharp
var dogs = new List<Dog> {
  new Dog("riley", 3),
  new Dog("mason", 5),
  new Dog("nestle", 5),
};

var older = from x in dogs
            where x.Age > 3
            select x;

// equivalent to
var older = dogs.Where(x => x.Age > 3);
```

Query syntax is equivalent to method syntax, performance-wise. Almost every operation can be expressed in either query or method syntax. 

# Exercise

1. Do this [excellent tutorial](https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/working-with-linq) from Microsoft.

[Project time](9-Project-1.md)