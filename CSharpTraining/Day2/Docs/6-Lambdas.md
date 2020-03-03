# Lesson 6 - Lambdas

Lambdas, closures, anonymous methods... So many names for the same basic idea!

## Lambda expressions

If you've done any modern javascript, you'll probably recognize the basic syntax of a C# lambda

```csharp
// expression lambda
var func1 = (ParameterList) => expression;

// statement lambda
var func2 = (ParameterList) => {
  some();
  series();
  of();
  statements();
};

// like javascript, the () around the parameters is optional if there's only one of them
var func3 = x => DoThingWith(x);
```

As you've seen, `=>` is overloaded in that it's also used for expression-bodied members.

Unlike Javascript, however, you cannot use a statement-bodied lambda as the body of an expression-bodied member. That is

```csharp
public int Add(int a, int b) => a + b; // this is valid

// this is invalid
public int Add2(int a, int b, int c) => {
  var x = a + b;
  return x + c;
}
```

## Lambdas and types

Explicitly typing a lambda gets complicated quickly. 9 times out of 10, you'll be defining lambdas in-line as callbacks to a function, but if you do need to type them, there are two types you need to know about.

If your lambda returns a value, you use the `Func<>` type. `Func<>` takes in a variable number of parameters: 1 for each of the arguments to the lambda, and then the final type being the return type.

```csharp
Func<int, int> Square = x => x * x;
Func<int, int, int> Add = (x, y) => x + y;
Func<string, int, string> Stringify = (name, i) => $"{name}, you entered {i}";
```

If your lambda does not return a value (is essentially a void method), you can use `Action<>`, where the type parameters are the types of the arguments to the lambda.

```csharp
Action<string> SendToServer = url => NetworkCall(url);
Action<string> Greet = name => {
  var greeting = "Hello, {name}!";
  Console.WriteLine(greeting);
};
```

## Capturing and scope

A lambda will capture variables in scope, and hold onto them even if the variables in question go out of scope. That is

```csharp
public Func<int, int> GetAdder(int addTo) {
  return x => x + addTo;
}

// alternately written
public Func<int, int> GetAdder(int addTo) => x => x + addTo;

var Adder = GetAdder(10);
var result = Adder(11); // 21
```

Some rules apply.

1. **Any variable captured by a lambda will not be garbage collected until the lambda goes out of scope**
2. Lambdas cannot capture `out` or `ref` parameters
3. Normal scoping rules apply. Variables declared inside of a lambda are not visible outside of the lambda.
4. `return`ing from a lambda does not `return` from the enclosing function.

## Usage

Most of the time, lambdas are declared inline, as arguments to a method.

```csharp
// Task.Run() is a way of spawning another thread and forgetting about it
Task.Run(() => LogMessage("hello from the other side"));

// OrderBy() sorts a list according to the property of the object chosen
employees.OrderBy(x => x.LastName);
```

## A note on expression-bodied member syntax

Many things in C# allow `=>` to be used if they contain only a single expression. This is not defining these members as lambdas, just using the `=>` syntax in places where its "return the result of the next expression" meaning makes sense.

```csharp
public class Example {
  // get-only computed properties
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string FullName => $"{FirstName} {LastName}";

  public List<string> ExampleList { get; set; }

  // single-line constructors
  public Example() => ExampleList = new List<string>();

  // single-expression methods
  public string GetFullName() => FullName;
}
```

# Exercise

1. There is a common higher order function called `map()`, which takes in a collection and performs an operation on each element in the collection. Implement `map()` for `int[]`.
2. Implement `map()` as an extension method on `int[]`
3. Implement `filter()` as an extension method on `int[]`

**Extra credit**: Implement `map()` and/or `filter()` as extension methods on `IEnumerable<T>`

[Next Lesson](7-List-and-Dictionary.md)