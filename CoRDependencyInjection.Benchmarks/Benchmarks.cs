using BenchmarkDotNet.Attributes;
using CoRDependencyInjection.Extensions;
using CoRDependencyInjection.Tests.TestItems;
using Microsoft.Extensions.DependencyInjection;

namespace CoRDependencyInjection.Benchmarks;

[MemoryDiagnoser]
public class Benchmarks
{
    private IServiceProvider _serviceProvider = null!;

    [GlobalSetup]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddTransientChain<ITestChain>()
            .WithHandler<FirstHandler>()
            .WithHandler<LastHandler>()
            .BuildChain();

        serviceCollection.AddScopedChain<IScopedTestChain>()
            .WithHandler<ScopedFirstHandler>()
            .WithHandler<ScopedLastHandler>()
            .BuildChain();

        serviceCollection.AddSingletonChain<ISingletonTestChain>()
            .WithHandler<SingletonFirstHandler>()
            .WithHandler<SingletonLastHandler>()
            .BuildChain();

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    [Benchmark]
    public void TransientChain() => _serviceProvider.GetRequiredService<ITestChain>();

    [Benchmark]
    public void ScopedChain()
    {
        using var scope = _serviceProvider.CreateScope();
        scope.ServiceProvider.GetRequiredService<IScopedTestChain>();
    }

    [Benchmark]
    public void SingletonChain() => _serviceProvider.GetRequiredService<ISingletonTestChain>();
}

public interface IScopedTestChain : ITestChain { }
public class ScopedFirstHandler(IScopedTestChain next) : FirstHandler(next), IScopedTestChain;
public class ScopedLastHandler() : LastHandler, IScopedTestChain;

public interface ISingletonTestChain : ITestChain { }
public class SingletonFirstHandler(ISingletonTestChain next) : FirstHandler(next), ISingletonTestChain;
public class SingletonLastHandler() : LastHandler, ISingletonTestChain;
