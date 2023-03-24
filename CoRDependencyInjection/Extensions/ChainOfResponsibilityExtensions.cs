using System;
using Microsoft.Extensions.DependencyInjection;

namespace CoRDependencyInjection.Extensions
{
    /// <summary>
    /// Extension methods for creating a new <see cref="ChainOfResponsibilityBuilder{TChain}"/>.
    /// </summary>
    public static class ChainOfResponsibilityExtensions
    {
        /// <summary>
        /// Creates a new instance of the chain builder of the provided chain interface in the provided service
        /// collection with the specified lifetime.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="serviceLifetime">The service lifetime.</param>
        /// <typeparam name="TChain">The chain interface.</typeparam>
        /// <returns>The chain builder.</returns>
        public static ChainOfResponsibilityBuilder<TChain> AddChain<TChain>(this IServiceCollection services,
            ServiceLifetime serviceLifetime)
            where TChain : class => new(services, serviceLifetime);

        /// <summary>
        /// Creates a new instance of the chain builder of the provided chain interface in the service
        /// collection with a transient lifetime.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <typeparam name="TChain">The chain interface.</typeparam>
        /// <returns>The chain builder.</returns>
        [Obsolete("Use AddTransientChain instead or specify the service lifetime.")]
        public static ChainOfResponsibilityBuilder<TChain> AddChain<TChain>(this IServiceCollection services)
            where TChain : class => AddChain<TChain>(services, ServiceLifetime.Transient);
        
        /// <summary>
        /// Creates a new instance of the chain builder of the provided chain interface in the service
        /// collection with a transient lifetime.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <typeparam name="TChain">The chain interface.</typeparam>
        /// <returns>The chain builder.</returns>
        public static ChainOfResponsibilityBuilder<TChain> AddTransientChain<TChain>(this IServiceCollection services)
            where TChain : class => AddChain<TChain>(services, ServiceLifetime.Transient);
        
        /// <summary>
        /// Creates a new instance of the chain builder of the provided chain interface in the service
        /// collection with a transient lifetime.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <typeparam name="TChain">The chain interface.</typeparam>
        /// <returns>The chain builder.</returns>
        public static ChainOfResponsibilityBuilder<TChain> AddScopedChain<TChain>(this IServiceCollection services)
            where TChain : class => AddChain<TChain>(services, ServiceLifetime.Scoped);
        
        /// <summary>
        /// Creates a new instance of the chain builder of the provided chain interface in the service
        /// collection with a singleton lifetime.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <typeparam name="TChain">The chain interface.</typeparam>
        /// <returns>The chain builder.</returns>
        public static ChainOfResponsibilityBuilder<TChain> AddSingletonChain<TChain>(this IServiceCollection services)
            where TChain : class => AddChain<TChain>(services, ServiceLifetime.Singleton);
    }
}