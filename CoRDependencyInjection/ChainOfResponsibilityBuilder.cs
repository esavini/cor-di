using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CoRDependencyInjection.Exceptions;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("CoRDependencyInjection.Tests")]

namespace CoRDependencyInjection
{
    /// <summary>
    /// Build a new Chain of Responsibility in the provided service collection.
    /// </summary>
    /// <typeparam name="TChain">The chain interface.</typeparam>
    public class ChainOfResponsibilityBuilder<TChain> where TChain : class
    {
        private readonly IServiceCollection _services;

        private readonly ServiceLifetime _serviceLifetime;

        private readonly List<Type> _handlers;

        internal ChainOfResponsibilityBuilder(IServiceCollection services, ServiceLifetime serviceLifetime)
        {
            _services = services;
            _serviceLifetime = serviceLifetime;
            _handlers = new List<Type>();
        }

        /// <summary>
        /// Adds an handler to the chain.
        /// </summary>
        /// <typeparam name="THandler">The handler.</typeparam>
        /// <returns>The current chain builder.</returns>
        public ChainOfResponsibilityBuilder<TChain> WithHandler<THandler>() where THandler : class, TChain
        {
            _handlers.Add(typeof(THandler));
            return this;
        }

        /// <summary>
        /// Builds the chain.
        /// </summary>
        /// <returns>The service collection with the chain.</returns>
        /// <exception cref="EmptyChainException">When the chain is empty.</exception>
        /// <exception cref="MissingPublicConstructorException">When the handler does not have any public constructor.</exception>
        /// <exception cref="RequestedNextHandlerInTheLastOneException">When the last handler requests the next one.</exception>
        public IServiceCollection BuildChain()
        {
            if (!_handlers.Any()) throw new EmptyChainException();

            var serviceDescriptor = new ServiceDescriptor(typeof(TChain),
                serviceProvider => InstantiateRecursively(serviceProvider), _serviceLifetime);

            _services.Add(serviceDescriptor);
            return _services;
        }

        /// <summary>
        /// Creates recursively a new instance of every chain member.
        /// </summary>
        /// <param name="services">The service provider.</param>
        /// <param name="current">The index of the current handler.</param>
        /// <returns>The instantiated handler.</returns>
        /// <exception cref="MissingPublicConstructorException">When the handler does not have any public constructor.</exception>
        /// <exception cref="RequestedNextHandlerInTheLastOneException">When the last handler requests the next one.</exception>
        private TChain InstantiateRecursively(IServiceProvider services, int current = 0)
        {
            var constructor = _handlers[current].GetConstructors()
                .Where(c => c.IsPublic)
                .OrderByDescending(c => c.GetParameters().Length)
                .FirstOrDefault();

            if (constructor is null)
            {
                throw new MissingPublicConstructorException(_handlers[current].FullName);
            }

            var constructorParameters = constructor.GetParameters();

            var parametersInstances = new List<object>();

            foreach (var parameter in constructorParameters)
            {
                var type = parameter.ParameterType;

                if (type == typeof(TChain))
                {
                    if (current == _handlers.Count - 1)
                    {
                        throw new RequestedNextHandlerInTheLastOneException();
                    }

                    parametersInstances.Add(InstantiateRecursively(services, current + 1));
                }
                else
                {
                    parametersInstances.Add(services.GetService(type));
                }
            }

            return (TChain)Activator.CreateInstance(_handlers[current], parametersInstances.ToArray());
        }
    }
}