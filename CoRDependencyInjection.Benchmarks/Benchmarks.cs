using BenchmarkDotNet.Attributes;
using CoRDependencyInjection.Extensions;
using CoRDependencyInjection.Tests.TestItems;
using Microsoft.Extensions.DependencyInjection;

namespace CoRDependencyInjection.Benchmarks;

public class Benchmarks
{
    private readonly IServiceProvider _serviceProvider;
    
    public Benchmarks()
    {
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddTransientChain<ITestChain>()
            .WithHandler<FirstHandler>()
            .WithHandler<FirstHandler>()
            .WithHandler<FirstHandler>()
            .WithHandler<LastHandler>()
            .BuildChain();
        
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    [Benchmark]
    public void TransitChain() => _serviceProvider.GetRequiredService<ITestChain>();
}
