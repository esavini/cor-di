using System;

namespace CoRDependencyInjection.Tests.TestItems
{
    public class LastHandler : ITestChain
    {
        public Type Handle()
        {
            return GetType();
        }
    }
}