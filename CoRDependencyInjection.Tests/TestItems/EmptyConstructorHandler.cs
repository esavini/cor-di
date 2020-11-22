using System;
using System.Diagnostics.CodeAnalysis;

namespace CoRDependencyInjection.Tests.TestItems
{
    [ExcludeFromCodeCoverage]
    public class EmptyConstructorHandler : ITestChain
    {
        public Type Handle()
        {
            throw new NotImplementedException();
        }
    }
}