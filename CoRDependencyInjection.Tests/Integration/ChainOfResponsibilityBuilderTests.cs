using System;
using CoRDependencyInjection.Exceptions;
using CoRDependencyInjection.Tests.TestItems;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CoRDependencyInjection.Tests.Integration
{
    public class ChainOfResponsibilityBuilderTests
    {
        private readonly IServiceCollection _services;

        private readonly ChainOfResponsibilityBuilder<ITestChain> _builder;

        public ChainOfResponsibilityBuilderTests()
        {
            _services = new ServiceCollection();
            _builder = new ChainOfResponsibilityBuilder<ITestChain>(_services);
        }

        [Fact]
        public void BuildChain_Success_WhenNoConstructorIsProvided()
        {
            _builder.WithHandler<EmptyConstructorHandler>().BuildChain();
            IServiceProvider serviceProvider = _services.BuildServiceProvider();

            serviceProvider.GetService<ITestChain>();
        }

        [Fact]
        public void BuildChain_ThrowsRequestedNextHandlerInTheLastOneException_WhenIsRequestedNextHandlerInTheLastOne()
        {
            _builder.WithHandler<OnlyNextHandlerConstructor>().BuildChain();
            IServiceProvider serviceProvider = _services.BuildServiceProvider();

            Assert.Throws<RequestedNextHandlerInTheLastOneException>(() => serviceProvider.GetService<ITestChain>());
        }

        [Fact]
        public void BuildChain_ThrowsMissingPublicConstructorException_WhenAValidPublicConstructorIsNotAvailable()
        {
            _builder.WithHandler<PrivateInvalidConstructor>().BuildChain();
            IServiceProvider serviceProvider = _services.BuildServiceProvider();

            Assert.Throws<MissingPublicConstructorException>(() => serviceProvider.GetService<ITestChain>());
        }

        [Fact]
        public void BuildChain_Success_NoCondition()
        {
            var singleton = new TestSingleton();
            singleton.Guid = Guid.NewGuid().ToString();

            _builder.WithHandler<FirstHandler>()
                .WithHandler<SecondHandler>()
                .WithHandler<EmptyConstructorHandler>().BuildChain()
                .AddSingleton(singleton);

            IServiceProvider serviceProvider = _services.BuildServiceProvider();

            var scope = serviceProvider.CreateScope();
            var firstHandler = scope.ServiceProvider.GetRequiredService<ITestChain>();

            Assert.Equal(typeof(SecondHandler), firstHandler.Handle());
            Assert.Equal(singleton.Guid, ((FirstHandler) firstHandler).GetSingleton().Guid);
        }
    }
}