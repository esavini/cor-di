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
            _builder = new ChainOfResponsibilityBuilder<ITestChain>(_serviceCollectionMock.Object,
                ServiceLifetime.Transient);
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

        [Theory]
        [InlineData(ServiceLifetime.Transient)]
        [InlineData(ServiceLifetime.Scoped)]
        [InlineData(ServiceLifetime.Singleton)]
        public void BuildChain_Success_NoCondition(ServiceLifetime serviceLifetime)
        {
            var builder = new ChainOfResponsibilityBuilder<ITestChain>(_serviceCollectionMock.Object, serviceLifetime);

            _serviceCollectionMock.Setup(exp => exp.Add(It.Is<ServiceDescriptor>(descriptor =>
                    descriptor.Lifetime == serviceLifetime &&
                    descriptor.ServiceType == typeof(ITestChain) &&
                    descriptor.ImplementationFactory != null
                )));

            var builtChain = builder.WithHandler<EmptyConstructorHandler>()
                .BuildChain();
            Assert.IsAssignableFrom<IServiceCollection>(builtChain);

            _serviceCollectionMock.Verify(exp => exp.Add(It.Is<ServiceDescriptor>(descriptor =>
                descriptor.Lifetime == serviceLifetime &&
                descriptor.ServiceType == typeof(ITestChain) &&
                descriptor.ImplementationFactory != null)
            ), Times.Once);

            _serviceCollectionMock.VerifyNoOtherCalls();
        }
    }
}