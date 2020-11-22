using System;

namespace CoRDependencyInjection.Exceptions
{
    /// <summary>
    /// Thrown when was next handler was requested in the last one.
    /// </summary>
    public class RequestedNextHandlerInTheLastOneException : CoRDependencyInjectionException
    {
        public RequestedNextHandlerInTheLastOneException(string message) : base(message)
        {
        }
        
        public RequestedNextHandlerInTheLastOneException()
        {
        }

        public RequestedNextHandlerInTheLastOneException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        public RequestedNextHandlerInTheLastOneException(string parameterName, string handlerName) : this(
            $"Requested next handler in parameter {parameterName} in the last handler {handlerName}.")
        {
        }
    }
}