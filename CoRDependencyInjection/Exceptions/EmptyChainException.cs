using System;

namespace CoRDependencyInjection.Exceptions
{
    /// <summary>
    /// Thrown when the chain is empty and build was requested.
    /// </summary>
    public class EmptyChainException : CoRDependencyInjectionException
    {
        public EmptyChainException(string message) : base(message)
        {
        }
        
        public EmptyChainException()
        {
        }

        public EmptyChainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}