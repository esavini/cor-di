using System;
using System.Diagnostics.CodeAnalysis;

namespace CoRDependencyInjection.Tests.TestItems
{
    [ExcludeFromCodeCoverage]
    public class FirstHandler : ITestChain
    {
        private readonly ITestChain _next;

        private readonly TestSingleton _singleton;

        public FirstHandler(ITestChain next, TestSingleton singleton)
        {
            _next = next;
            _singleton = singleton;
        }

        public Type Handle()
        {
            return _next.Handle();
        }

        public TestSingleton GetSingleton()
        {
            return _singleton;
        }
    }
}