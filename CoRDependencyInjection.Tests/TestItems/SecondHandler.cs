using System;

namespace CoRDependencyInjection.Tests.TestItems
{
    public class SecondHandler : ITestChain
    {
        public Type Handle()
        {
            return GetType();
        }
    }
}