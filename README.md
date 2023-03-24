# Chain of Responsibility Dependency Injection
![Nuget](https://img.shields.io/nuget/dt/CoRDependencyInjection)

This library allows you to use constructor dependency injection (and perform Inversion of Control) with the Chain of Responsibility pattern (see [Wikipedia page](https://en.wikipedia.org/wiki/Chain-of-responsibility_pattern) or read [GoF book's _Design Patterns_](https://en.wikipedia.org/wiki/Design_Patterns)).
It is entirely written in C# and targets .NET Standard 2.0.

After three year, I have updated the library to .NET Standard 2.0 and I active support it.
Feel free to contribute!

## Installation
Simply use this nuget command:

```Install-Package CoRDependencyInjection```

## Usage
Basic usage (and the only one 😉):

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddTransientChain<ITestChain>()
        .WithHandler<FirstHandler>()
        .WithHandler<SecondHandler>()
        .WithHandler<LastHandler>()
        .BuildChain();
}
```

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingletonChain<ITestChain>()
        .WithHandler<FirstHandler>()
        .WithHandler<SecondHandler>()
        .WithHandler<LastHandler>()
        .BuildChain();
}
```

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddScopedChain<ITestChain>()
        .WithHandler<FirstHandler>()
        .WithHandler<SecondHandler>()
        .WithHandler<LastHandler>()
        .BuildChain();
}
```

The chain will build in the order you provide the handlers.
Remember to do not request the next handler in the last one!

Example handler:

```csharp
public class FirstHandler : ITestChain
{
    private readonly ITestHandler _next;

    private readonly ShouldContinue _shouldContinue;

    public FirstHandler(ITestHandler next, IOption<ShouldContinue> shouldContinueOption)
    {
        _next = next;
        _shouldContinue = shouldContinueOption.Value;
    }

    public bool Handle()
    {
        if(_shouldContinue.Continue)
        {
            return _next.Handle();
        }
        
        return false;
    }
}
```

Example chain handle request:

```csharp
public class HomeController : Controller
{
    private readonly ITestHandler _chain;

    public FirstHandler(ITestHandler chain)
    {
        _chain = chain;
    }

    [HttpPost]
    public bool Index()
    {
        return _chain.Handle();
    }
}
```

## Exceptions
All exceptions inherit from ```CoRDependencyInjectionException```.

 - ```EmptyChainException```: Thrown when the chain is empty and build was requested.
 - ```MissingPublicConstructorException```: Thrown when no public constructor is available.
 - ```RequestedNextHandlerInTheLastOneException```: Thrown when was next handler was requested in the last one.