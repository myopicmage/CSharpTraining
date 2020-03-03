# Lesson 1 - Exceptions

Exceptions are the main way of handling errors in C#.

## `try`/`catch`

If you know something may `throw` an exception, you can prepare for it using `try`/`catch`. Wrap the potentially dangerous code in a `try` block, and put the error handling code inside of an appropriate `catch` block. You can have multiple `catch` blocks for the same `try`, each one handling a different type of exception. They will be evaluated in order.

If an exception is never caught, it will crash your application. This is not usually an issue in web apps, since they're usually in a separate thread, but can be an issue if there are too many uncaught exceptions, or you're not running in a web server setting.

```csharp
// Please note this code *will not* compile, and is an incorrect use of HttpClient.
// Correct use is more complicated, and uses concepts we haven't covered yet.
public void GetResource(string url) {
  try {
    var client = new HttpClient(); // HttpClient is for making http requests
    
    var response = client.GetString(url);
  } catch (SocketException ex) {
    LogError(ex);
  } catch (HttpRequestException ex) {
    // Anything that inherits from System.Exception will have a Message property that has a human readable
    // explanation of the exception
    TellUser($"An issue occurred. {ex.Message}. Please try your request again.");
    LogError(ex);
  } catch (Exception ex) {
    // Exception, or System.Exception, is the top level exception. Catching Exception
    // is a way of saying "catch every exception"
    LogError(ex);
  }
}
```

**Note**: `try` creates a new scope. You will thus need to use a pattern like this occasionally

```csharp
public string[] ReadFromFile(string path) {
  // declare the variable you're going to use outside of the try
  string[] content;

  try {
    content = File.ReadAllLines(path);
  } catch (FileNotFoundException ex) {
    DoSomethingWithException(ex);
  }

  return content;
}
```

## `finally`

`try`/`catch` blocks optionally accept a third block: `finally`. `finally` is **guaranteed** to run after the `try`/`catch`, even if an exception occurs. It's very useful for cleanup operations. This pattern is actually so common that it has syntax sugar in the form of a `using` statement.

```csharp
public void ProcessFile(string path) {
  StreamReader sr;

  try {
    sr = File.OpenText(path);

    string s;

    // keep reading from the file until there's nothing more to read
    while ((s = sr.ReadLine()) != null) {
      ProcessLine(s);
    }
  } catch (FileNotFoundException ex) {
    ComplainFileWasNotFound(ex);
  } catch (Exception ex) {
    LogException(ex);
  } finally {
    // this will run even if an exception is thrown, guaranteeing the stream is closed 
    sr.Close();
  }
}
```

## Throwing your own exceptions

If you want to trigger an exception for yourself, you can use the `throw` keyword. `throw` will end the current function. 

```csharp
public string Process(string inputPath, string outputPath) {
  if (inputPath is null) {
    // nameof takes the variable and outputs the name in string form
    throw new ArgumentNullException(nameof(inputPath));
  }

  if (outputPath is null) {
    throw new ArgumentNullException(nameof(outputPath));
  }

  if (!IsPath(inputPath)) {
    throw new ArgumentException($"{nameof(inputPath)} must be a valid path", nameof(inputPath));
  }

  // etc
}
```

## Bubbling

Exceptions will bubble up the stack until caught. That is, if an exception is thrown, it will be caught by the nearest enclosing `try`/`catch`.

```csharp
public void IThrow() => throw new Exception("Oh no!"); // throw can be used as an expression

public void DoOrDoNot() {
  DoAThing();

  IThrow();

  // since IThrow throws an exception, this will never be reached. The exception will continue
  // up the stack until it finds a try
  DoSomeThingElse();
}

public void IHaveATry() {
  try {
    DoOrDoNot();
  } catch (Exception ex) {
    LogException(ex); // the exception from IThrow will be caught here
  }
}
```

## Defining your own exception

Exceptions are just classes that inherit from `System.Exception`. It is uncommon to define your own exception type, but it can be done.

Usually, custom exceptions are defined for particularly custom functionality by library authors. The purpose would be to catch a custom exception type and handle it differently than another, more general exception.

# Exercise

1. Try to read a file from a path you know doesn't exist.
2. Add a `try`/`catch` around the file read. What exception do you catch?
3. Try to reference a `null` value. 
4. Hover over a file read method so that the Visual Studio documentation appears. What does it say?

[Next Lesson](2-Interfaces.md)