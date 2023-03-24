using CoRDependencyInjection.Extensions;
using CoRDependencyInjection.Tests.TestItems;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CoRDependencyInjection.Tests.Unit
{
    public class ChainOfResponsibilityExtensionsTests
    {
        [Fact]
        public void AddChain_Success_NoCondition()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            Assert.IsType<ChainOfResponsibilityBuilder<ITestChain>>(serviceCollection.AddChain<ITestChain>());
        }
        
        [Fact]
        public void AddChainWithServiceLifetime_Success_NoCondition()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            Assert.IsType<ChainOfResponsibilityBuilder<ITestChain>>(serviceCollection.AddChain<ITestChain>(ServiceLifetime.Singleton));
        }
        
        [Fact]
        public void AddTransientChain_Success_NoCondition()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            Assert.IsType<ChainOfResponsibilityBuilder<ITestChain>>(serviceCollection.AddTransientChain<ITestChain>());
        }
        
        [Fact]
        public void AddScopedChain_Success_NoCondition()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            Assert.IsType<ChainOfResponsibilityBuilder<ITestChain>>(serviceCollection.AddScopedChain<ITestChain>());
        }
        
        [Fact]
        public void AddSingletonChain_Success_NoCondition()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            Assert.IsType<ChainOfResponsibilityBuilder<ITestChain>>(serviceCollection.AddSingletonChain<ITestChain>());
        }
    }
}