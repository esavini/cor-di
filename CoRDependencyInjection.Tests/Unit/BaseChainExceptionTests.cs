using System;
using CoRDependencyInjection.Exceptions;
using Xunit;

namespace CoRDependencyInjection.Tests.Unit
{
    public abstract class BaseChainExceptionTests<TException> where TException : CoRDependencyInjectionException, new()
    {
        [Fact]
        public void EmptyConstructor_NoCondition_Success()
        {
            var exception = new TException();

            Assert.NotNull(exception.Message);
        }

        [Fact]
        public void StringConstructor_NoCondition_Success()
        {
            var exception = (TException) Activator.CreateInstance(typeof(TException), "")!;

            Assert.Empty(exception!.Message);
        }
        
        [Fact]
        public void StringAndInnerExceptionConstructor_NoCondition_Success()
        {
            var exception = (TException) Activator.CreateInstance(typeof(TException), "", new ArgumentException())!;

            Assert.NotNull(exception!.Message);
            Assert.IsType<ArgumentException>(exception!.InnerException);
        }
    }
}