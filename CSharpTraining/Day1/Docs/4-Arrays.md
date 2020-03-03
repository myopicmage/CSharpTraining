# Lesson 4 - Arrays

C# has a very handy array type. They have a fixed size, automatically bounds checked, and can be created to hold basically anything.

**Unlike php, arrays are not maps/dictionaries.** They're much closer to Java or Javascript arrays, in that they're ordered and occupy contiguous memory (though unlike Javascript, C# arrays are not dynamically sized).

## Notes

Arrays are created with a type, and can only hold elements of that type. When created, they contain the `default` value of that type. They are created in a few different ways.

```csharp
int[] array0; // declare, but don't initialize

// use an explicit type. This creates an array with 10 elements
int[] array1 = new int[10];

// var is also acceptable
var array2 = new int[10];

// if you know what you're going to put into the array at compile time, you can
// declare and initialize it at the same time. notice, we don't need to specify a size
var array3 = new int[] { 1, 2, 3};

// the type is actually optional
var array4 = new [] { 1, 2, 3 };

// if you type the variable itself, you can use initializer syntax
int[] array5 = { 1, 2, 3 };

// however, this does *not* work with `var`
var array6 = { 1, 2, 3 }; // invalid
```

Arrays in C# are zero-indexed, and use square bracket index syntax.

```csharp
var element = myArray[0]; // get the first element of the array
```

Like Java, C# will throw an exception if you try to access an index not in the array.

```csharp
var myArray = new [] { 1, 2, 3 };
var element = myArray[4]; // ArrayIndexOutOfBounds exception
```

Arrays can be single-dimension, multi-dimension, or jagged. Single dimension is a list of elements. Multi-dimension, in this case, means multiple dimensions of **equal size**. Jagged is closer to the idea of an "array of arrays," in that they can be different sizes.

```csharp
// single dimension
int[] single = new int[] { 1, 2, 3 };

// multi-dimension. declares a 3x3 grid. 
// note the comma syntax.
int[,] multi = new int[3, 3];

// jagged
// note the separate []
int[][] jagged = new int[3][2];

// you can also directly initialize them
// note the comma
var multi2 = new [,] { { 1, 2, 3 }, { 4, 5, 6} };

// Initializing a jagged array requires you initialize each array individually.
var jagged2 = new int[3][]; // create an array of 3 in one dimension

// initialize each sub-array individually
for (int i = 0; i < jagged2.Length; i++) {
  jagged2[i] = new int[i];
}
```

To get the length of the array, you can use `yourArray.Length`. 

# Exercise

1. Make an array of `int`s.
2. Print out the array using the `PrintArray()` method.
3. Try putting a `string` or a `double` into the array. What happens?
4. Try accessing an invalid index and running your code.

[Next Lesson](5-Control-Flow.md)