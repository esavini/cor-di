using System;
using System.Diagnostics.CodeAnalysis;

namespace CoRDependencyInjection.Tests.TestItems
{
    [ExcludeFromCodeCoverage]
    public class OnlyNextHandlerConstructor : ITestChain
    {
        public OnlyNextHandlerConstructor(ITestChain next)
        {
        }

        public Type Handle()
        {
            throw new NotImplementedException();
        }
    }
}