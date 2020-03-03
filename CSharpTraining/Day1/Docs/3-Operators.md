# Lesson 3 - Operators

Like any good programming language, C# has several operators available to you.

## Basics

Some common operators, in no particular order

| Type       | Symbol      |
| ----       | -------     |
| Assignment | =           |
| Math       | + - * / %   |
| Comparison | < > <= >=   |
| Equality   | == !=       |
| Negation   | !           |
| Shorthand  | += -= *= /= |
| AND        | &&          |
| OR         | \|\|        |
| Ternary    | ? :         |
| Increment  | ++          |
| Decrement  | --          |

**Please note that if you're looking at the markdown source for the above table, the `||` operator will be escaped with `\`. This is because markdown uses `|` for tables.**

The full list (and explanations) can be found [here](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/). C# has **many** operators, and many of them will be discussed later. 

The math operators in C# follow mathematical order of operations. Otherwise, precedence can be found in [this table](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/#operator-precedence). Again, please note that there are **many** more operators here than we're discussing right now.

When performing operations on different number types, C# will **generally** convert types, if there will not be any data loss. 

```csharp
// safe
int x = 10;
int y = 2;
var z = 10 / 2; // will be int

decimal a = 10.0m; // the m sigil marks a decimal literal, otherwise it would be a double
double b = 3.3;

// this will not compile due to data loss
var result = a / b;
```

## Weak typing

C# is strongly typed, in that it will **generally** not coerce types to match when trying to compare them. Any coercion is through a defined implicit conversion (such as when attempting to compare an `int` to a `double`).

This means that C# does not have php or javascript-style `===` and `!==` operators. `==` and `!=` will (usually) perform reference equality. `==` is by far the easiest way to compare things in C#.

### Strings

Unlike java, it is perfectly safe (and correct!) to use `==` to compare strings. Since all strings are interned, all instances of the same string (case sensitive) will point to the same reference, meaning reference equality comparisons will work properly for them.

Meaning: use `==` on strings.

## A note

C# does support operator overloading. The number of operators is less than that of C++ (for instance, `()` is not considered an operator and thus cannot be overloaded), but there is support for it. If you'd like to know more, you can read the [official docs](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading).

# Exercise

1. Open `Lesson3.cs`, and go to the `Exercise()` method. 
2. Add an `int` to an `int`
3. Add a `double` to a `double`
4. Add a `decimal` to a `decimal`
5. Add a `decimal` to an `int`
6. Divide an `int` by a `double`

[Next Lesson](4-Arrays.md)