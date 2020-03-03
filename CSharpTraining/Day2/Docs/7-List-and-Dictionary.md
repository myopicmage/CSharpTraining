# Lesson 7 - List and Dictionary

We're almost there... the two most common collections in .NET

## `List<T>`

You're already quite familiar with arrays. Arrays are efficient, but they are not dynamic. `List<T>` is a dynamic list, allowing elements to be added and removed at will. Similar to `ArrayList<T>` in Java, but it doesn't require explicit boxing and unboxing.

`List<T>` must be instantiated with a type. Once given a type, only elements of that type can be added to it.

```csharp
var myDogs = new List<Dog>();

myDogs.Add(fido); // add a single item
myDogs.Add(rex);

myDogs.AddRange(new [] { snowball, snuffles, spike, nestle, riley, mason }); // add multiple items at once

// oh snap it's initializer syntax!!
var myCats = new List<Cat> {
  nyancy,
  frisky,
  garfield,
  heathcliff,
  felix,
  spock,
  tali
};

// get an item from the list. If the index is out of range, an exception will be thrown.
var e = myDogs.ElementAt(1);

// *OrDefault() methods will return the default value of the type of the 
// collection if it can't find the value.
var eOrDefault = myDogs.ElementAtOrDefault(1);

// you can use indexer syntax ([]) on a `List`, but it's not guaranteed to 
// work on every `IEnumerable`, so ElementAt() is considered safer
var indexed = myDogs[1];
```

It's uncommon to get an element from a `List`. Most of the time, you'll be performing operations on them using linq.

## `Dictionary<TKey, TValue>`

Dictionaries are .NET's key/value pair collection. The type may look scary if you're not used to generic conventions, but it just defines the type of the key and the type of the value.

```csharp
var officePets = new Dictionary<string, Pet>();

officePets.Add("Kevin", mason);
officePets.Add("Aleks", potato);
officePets.Add("Danielle", bunny);

// adding a duplicate key throws an ArgumentException
officePets.Add("Kevin", nestle);

// initializer syntax looks a little different, but it's just
// { key, value }
var moreOfficePets = new Dictionary<string, Pet> {
  { "Kevin", mason },
  { "Danielle", bunny },
  { "Li", zeus }
};

// you can use indexer syntax on dictionaries
// if the key is not in the dictionary, it will throw a KeyNotFoundException
var kevinPet = moreOfficePets["Kevin"];

// it's safer to do this
// this pattern of Try* methods is very common in C#
if (officePets.TryGetValue("Aleks", out var aleksPet)) {
  PrintPet(aleksPet);
}

// you can also just check for a key beforehand
if (officePets.ContainsKey("Steven")) {
  PrintPet(officePets["Steven"]);
} else {
  Console.WriteLine("Oh no Steven does not have a pet :(");
}
```

## Our old friend `IEnumerable<T>`

**Both** `List<T>` and `Dictionary<TKey, TValue>` implement `IEnumerable<T>`.

"But how do you enumerate through a dictionary?"

`Dictionary<TKey, TValue>` can be thought of as an `IEnumerable<KeyValuePair<TKey, TValue>>`. It's `T`urtles all the way down.

"Why would you do it that way?"

Ask again after the lesson on linq.

# Exercise

1. Make a `List<SantaGift>`. Check it twice (using a `for` and a `foreach`).
2. Santa would probably prefer a `Dictionary<Child, Gift>`, so he could more easily associate children with gifts. In this case, Santa will have to settle for a `Dictionary<string, string>`. Give some gifts to some kids.
3. Either press F12, right click and go to definition, or control click on the word `List`. All of these should take you to the definition of the `List<T>` type. What interfaces does it implement? 

**You're probably tired of this, so optional**: Reverse a string, but use a `List<char>` this time.

[Next Lesson](8-LINQ.md)