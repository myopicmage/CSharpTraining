# Lesson 1 - `async`/`await`

If you know modern Javascript, you've probably seen the `async`/`await` construct.

## Async, a primer

First, two definitions. With examples!

- **Concurrent**: things that happen roughly at the same time, though **not** simultaneously. For example, if you (as a human) ever multi-task, you are doing two (or more) things "at once," though your concentration can only ever be on one thing at a time. **Concurrent does NOT imply parallel**. 
  - Single-core green threads
  - Async code
- **Parallel**: things that happen at **exactly** the same time. Parallelism does imply concurrency. 
  - M:N green threads (user-land threads mapped onto OS threads. Think Go or Erlang)
  - OS threads

There are three (and a half) main ways of writing async code

1. Just wait on it (bad, not actually async)
2. Callbacks
3. Promises/Futures
4. `async`/`await`

## Async, a history

The first form of async programing

## `Task<T>`

In Javascript, `async`/`await` is just sugar for promises. C# has something roughly similar to promises or futures in the form of `Task<T>`. A `Task` can be thought of as a promise, in that it represents work to be done, but the programming model is a little bit different from there. It's not as easy (or common) to use the equivalent of `then()`, but it's also considered less necessary.

A `Task<T>` is a promise of a calculation, where `T` is the return type. This is similar to promises in javascript, and extremely similar to the promise API in Typescript (which makes promises generic in the form of `Promise<T>`).

Unlike Javascript, C# is not limited to a single thread. `Task`s are not guaranteed to run on the same thread, especially when used in conjunction with `async`/`await`, though the scheduler will try to reduce unnecessary overhead, which may mean either using a different thread or weaving it in between other `async` methods. It is rare that you will need to care about exactly where your `Task` is running. If you do, it's arguably better to use a lower level construct like raw threads or the thread pool.

### `Task.Run()`

`Task.Run()` is a fire-and-forget method. It accepts an `Action<>`, usually in the form of a lambda, and runs it outside of the current context. This can (and often does) mean it runs on another thread, but it can run on the same thread.

### `Task.FromResult()`

Roughly equivalent to Javascript's `Promise.resolve()`, `Task.FromResult()` takes a value and returns it wrapped in a `Task<T>`. It's a handy way of using synchronous code in an async context.

## `async`

`async` marks a method that **could** execute asynchronously. (Huge surprise). It's a modifier to the method like `static` or `override`. Methods marked `async` will not run asynchronously until there is an `await` put inside of them, but **the compiler will still generate the necessary code**, making `async` methods without an `await` in them a useless performance loss.

```csharp
// it is convention to name your async methods with an Async suffix, especially if you provide both 
// a synchronous and asynchronous api
public async Task<string> GetStringAsync() { }
```

## `await`

`await` is the secret sauce to async in C#. In english, it equates to "run the method up to this point. The method we're `await`ing will let us know when it's done, and we can continue execution from there."

```csharp
public async Task<Config> GetConfigAsync() {
                   // execution of this method pauses here
  var jsonConfig = await _client.GetStringAsync(configApiUrl);

  return Config.Parse(jsonConfig); 
}
```

```typescript
// roughly equivalent javascript
const getConfig = () =>
  fetch(configApiUrl)
    .then(response => {
      if (response.ok) {
        return response.json();
      }
    })
    .then(config => parse(config));
```

Since this method is `async`, it can be `await`ed, allowing execution of the entire application to continue while it waits on its network call.

## Values

`async` methods can return 3 things:

1. `void`
2. `Task`
3. `Task<T>`

`void` is useful for "top-level" `async` methods, like event handlers. It should not be used otherwise, since it makes it difficult to keep track of when an `async` method has completed.

`Task` is used when you want to keep track of the `Task` being run, potentially for things like canceling it.

`Task<T>` is used when you want to return a value.

[Next Lesson](2-MVC-music-store.md)