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
    }
}