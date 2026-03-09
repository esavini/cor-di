using System;
using System.Diagnostics.CodeAnalysis;

namespace CoRDependencyInjection.Tests.TestItems
{
    [ExcludeFromCodeCoverage]
    public class FirstHandler : ITestChain
    {
        private readonly ITestChain _next;
        
        public FirstHandler(ITestChain next)
        {
            _next = next;
        }

        public Type Handle()
        {
            return _next.Handle();
        }
    }
}