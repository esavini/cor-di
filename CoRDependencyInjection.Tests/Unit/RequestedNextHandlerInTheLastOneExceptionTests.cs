using CoRDependencyInjection.Exceptions;
using Xunit;

namespace CoRDependencyInjection.Tests.Unit
{
    public class
        RequestedNextHandlerInTheLastOneExceptionTests : BaseChainExceptionTests<
            RequestedNextHandlerInTheLastOneException>
    {
        [Fact]
        public void StringAndStringConstructor_NoCondition_Success()
        {
            var exception = new RequestedNextHandlerInTheLastOneException("", "");

            Assert.NotEmpty(exception.Message);
        }
    }
}