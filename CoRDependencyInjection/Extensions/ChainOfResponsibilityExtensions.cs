using Microsoft.Extensions.DependencyInjection;

namespace CoRDependencyInjection.Extensions
{
    /// <summary>
    /// Extension methods for creating a new <see cref="ChainOfResponsibilityBuilder{TChain}"/>.
    /// </summary>
    public static class ChainOfResponsibilityExtensions
    {
        /// <summary>
        /// Creates a new instance of the chain builder of the provided chain interface with the provided service
        /// collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <typeparam name="TChain">The chain interface.</typeparam>
        /// <returns>The chain builder.</returns>
        public static ChainOfResponsibilityBuilder<TChain> AddChain<TChain>(this IServiceCollection services)
            where TChain : class
        {
            return new ChainOfResponsibilityBuilder<TChain>(services);
        }
    }
}