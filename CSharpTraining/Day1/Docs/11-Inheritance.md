# Lesson 10 - Inheritance

Inheritance is a way for classes/objects to share code, data, and behavior. C# supports single-inheritance, meaning classes can have at most one base class. All classes in C# implicitly inherit from `System.Object`.

## Inheritance in C#

In C#, `extends` is spelled `:`. Parent (or super) classes are referred to as base classes.

```csharp
class Base
{

}

// Child inherits from Base
class Child : Base
{

}
```

Methods are **not** `virtual` by default in C#. To allow a method to be overridden by a child class, you must explicitly mark it as such.

```csharp
class Base
{
  public void Spam() { }
  public virtual void Eggs() { }
}

class Child : Base
{
  // this explicitly overrides Eggs() from Base
  public override void Eggs() 
  { 
    // to call the base class's method, use the base keyword
    base.Eggs();
  }

  // this is invalid, since Spam() is not virtual
  public override void Spam() { }
}
```

To prevent a class from being inherited from, you can use `sealed`.

```csharp
sealed class FinalClass
{

}

// this is invalid, since FinalClass is `sealed`
class NotFinal : FinalClass { }
```

To call a parent's constructor, use `: base()`. Child classes **do not** inherit their base class's constructor, but they can call them.

```csharp
class Publication
{
  // since none of these are marked private, they will all be inherited by 
  // any child classes
  public string Title { get; set; }
  public string Author { get; set; }
  public DateTime Copyright { get; set; }

  // this constructor will NOT be inherited, but 
  public Publication(string title, string author, DateTime copyright)
  {
    Title = title;
    Author = author;
    Copyright = copyright;
  }
}

class Book : Publication
{
  // Book inherits Publication's properties
  public int NumPages { get; set; }

  // by using : base(), the base class's constructor will be called FIRST
  // then Book's constructor will be called
  // It is a bad idea to rely on this ordering.
  public Book(string title, string author, DateTime copyright, int numPages) 
    : base(title, author, copyright)
  {
    NumPages = numPages;
  }
}
```

## `abstract`

If you have a class intended to be a base class for many classes, but it does not make sense for it to exist on its own, it is a good idea to use an `abstract` class. `abstract` classes cannot be instantiated. They **must** be derived from. They can provide base implementations of methods.

In an `abstract class`, methods are **not** implicitly `virtual`. You must still mark them to be able to override them.

You can mark methods inside of an `abstract class` `abstract` so that you don't need to implement them, and you will force your derived classes to implement them.

Unsurprisingly, `abstract class`es cannot be marked `sealed`.

```csharp
// The Publication class we used before doesn't really have a reason to be instantiated,
// but it does have methods and properties that make sense to re-use. 
abstract class Publication
{
  public string Title { get; set; }
  public string Author { get; set; }
  public DateTime Copyright { get; set; }

  public Publication(string title, string author, DateTime copyright)
  {
    Title = title;
    Author = author;
    Copyright = copyright;
  }

  // this method must be overridden and implemented by any child classes
  abstract public string GetShelvingInfo();

  // this method (and its implementation) are available to all child classes,
  // though since it is virtual, it can be overridden.
  public virtual string GetBaseInfo() => $"{Title} by {Author}. Copyright {Copyright}";

  // Just like a regular class, this method cannot be overridden.
  public void Foo() { }
}
```

## Polymorphism

Polymorphism is the idea that an object can act differently depending on the circumstances. You may have heard the terms "covariance" and "contravariance." This is better explained by example.

```csharp
// there's no such thing as a generic Animal, so we'll mark it as abstract
abstract class Animal 
{
  public string Name { get; set; }

  public abstract void Speak();
}

// a Dog is an Animal
class Dog : Animal 
{ 
  public override void Speak()
  {
    Console.WriteLine("Bark!");
  }
}

// a Cat is an Animal
class Cat : Animal
{
  public override void Speak()
  {
    Console.WriteLine("Meow!");
  }
}

// since Animal is guaranteed to have a name, this is safe
public string GetName(Animal a) => a.Name;

public static void Main(string[] args)
{
  var dog = new Dog();

  // you can assign a child (derived) class to a parent type
  Animal fuzzy = new Cat();

  dog.Speak(); // "Bark!"
  fuzzy.Speak(); // "Meow!"

  GetName(dog);
  GetName(fuzzy);

  // Note, this does not work
  Dog fido = new Animal();

  // This also does not work
  Cat snowball = new Dog();
}
```

## Inheritance v. Composition

C# does not support multiple inheritance. That is, there can only ever be one base class for a derived (child) class. It also does not have re-usable blocks of code like PHP's `trait`s. The closest thing it has are `interface`s. A class can implement any number of `interface`s. All actual implementation is left up to the class, so there's less chance of overwriting functionality.

Composition is the pattern of re-using small pieces of related functionality (like PHP `trait`s, ruby `mixin`s, etc). C# accomplishes this using `interface`s, though there is less actual code re-use since `interface`s only require an api and do not supply default implementations.

There will be a lesson on `interface`s tomorrow, or you can see [this page](../../Day2/Docs/2-Interfaces.md) to get an early look.

## A note about inheritance

"Classical" inheritance, that is large inheritance trees that are supposed to model real world concepts with large amounts of shared code, is rapidly becoming frowned upon. There's still a use for the code sharing aspects of inheritance. It is unlikely that `abstract` classes will be going anywhere anytime soon. 

# Exercise

1. Create an `abstract class` called `Person`
2. Create three child classes named `Developer`, `Teacher`, and `Vet`
3. Add an `abstract` method to `Person` called `IncreaseAge()`. What happens to the three derived classes?
4. Implement `IncreaseAge()` by adding a property to `Person`, and then adding the functionality to each derived class.
5. Add a method to `Person` called `DoWork()`. Give it some base functionality.
6. Override `DoWork()` in each of the derived classes. Have it print each class's job and some of the work they do to the console.

[Next Lesson](12-References-and-Values.md)