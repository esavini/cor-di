using System;
using System.Diagnostics.CodeAnalysis;

namespace CoRDependencyInjection.Tests.TestItems
{
    [ExcludeFromCodeCoverage]
    public class PrivateInvalidConstructor : ITestChain
    {
        private PrivateInvalidConstructor(ITestChain next)
        {
        }

        public Type Handle()
        {
            throw new NotImplementedException();
        }
    }
}