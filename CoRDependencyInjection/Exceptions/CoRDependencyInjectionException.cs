using System;

namespace CoRDependencyInjection.Exceptions
{
    /// <summary>
    /// Base library exception.
    /// </summary>
    public abstract class CoRDependencyInjectionException : Exception
    {
        public CoRDependencyInjectionException()
        {
        }
        
        public CoRDependencyInjectionException(string message) : base(message)
        {
        }

        public CoRDependencyInjectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}