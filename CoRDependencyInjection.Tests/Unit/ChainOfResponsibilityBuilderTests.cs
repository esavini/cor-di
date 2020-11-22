using System;
using CoRDependencyInjection.Exceptions;
using CoRDependencyInjection.Tests.TestItems;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace CoRDependencyInjection.Tests.Unit
{
    public class ChainOfResponsibilityBuilderTests
    {
        private readonly Mock<IServiceCollection> _serviceCollectionMock;

        private readonly ChainOfResponsibilityBuilder<ITestChain> _builder;

        public ChainOfResponsibilityBuilderTests()
        {
            _serviceCollectionMock = new Mock<IServiceCollection>();
            _builder = new ChainOfResponsibilityBuilder<ITestChain>(_serviceCollectionMock.Object);
        }
        
        [Fact]
        public void WithHandler_Success_NoCondition()
        {
            var builder = _builder.WithHandler<EmptyConstructorHandler>();

            Assert.NotNull(builder);
            Assert.Same(_builder, builder);

            _serviceCollectionMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void BuildChain_ThrowsEmptyChainException_IfNoHandlerWasAdded()
        {
            Assert.Throws<EmptyChainException>(() => _builder.BuildChain());

            _serviceCollectionMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void BuildChain_Success_NoCondition()
        {
            _serviceCollectionMock.Setup(_ => _.Add(It.Is<ServiceDescriptor>(descriptor =>
                descriptor.Lifetime == ServiceLifetime.Transient &&
                descriptor.ServiceType == typeof(ITestChain) &&
                descriptor.ImplementationFactory.GetType() == typeof(Func<IServiceProvider, ITestChain>)
            )));
            
            Assert.IsAssignableFrom<IServiceCollection>(_builder.WithHandler<EmptyConstructorHandler>().BuildChain());
            
            _serviceCollectionMock.Verify(_ => _.Add(It.Is<ServiceDescriptor>(descriptor =>
                descriptor.Lifetime == ServiceLifetime.Transient &&
                descriptor.ServiceType == typeof(ITestChain) &&
                descriptor.ImplementationFactory.GetType() == typeof(Func<IServiceProvider, ITestChain>)
            )), Times.Once);
            _serviceCollectionMock.VerifyNoOtherCalls();
        }
    }
}