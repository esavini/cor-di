using System;

namespace CoRDependencyInjection.Exceptions
{
    /// <summary>
    /// Thrown when no public constructor is available.
    /// </summary>
    public class MissingPublicConstructorException : CoRDependencyInjectionException
    {
        public MissingPublicConstructorException(string message) : base(message)
        {
        }

        public MissingPublicConstructorException()
        {
        }
        
        public MissingPublicConstructorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}