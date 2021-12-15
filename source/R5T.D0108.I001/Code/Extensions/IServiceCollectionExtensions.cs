using System;

using Microsoft.Extensions.DependencyInjection;
using R5T.T0063;

using R5T.T0064;


namespace R5T.D0108.I001
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="FileBasedExtensionMethodBaseRepository"/> implementation of <see cref="IFileBasedExtensionMethodBaseRepository"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddFileBasedExtensionMethodBaseRepository(this IServiceCollection services,
            IServiceAction<IExtensionMethodBaseRepositoryFilePathsProvider> extensionMethodBaseRepositoryFilePathsProviderAction)
        {
            services
                .Run(extensionMethodBaseRepositoryFilePathsProviderAction)
                .AddSingleton<IFileBasedExtensionMethodBaseRepository, FileBasedExtensionMethodBaseRepository>();

            return services;
        }

        /// <summary>
        /// Forwards the <see cref="IFileBasedExtensionMethodBaseRepository"/> service to <see cref="IExtensionMethodBaseRepository"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection ForwardToIExtensionMethodBaseRepository(this IServiceCollection services,
            IServiceAction<IFileBasedExtensionMethodBaseRepository> fileBasedExtensionMethodBaseRepositoryAction)
        {
            services
                .Run(fileBasedExtensionMethodBaseRepositoryAction)
                .AddSingleton<IExtensionMethodBaseRepository>(sp => sp.GetRequiredService<IFileBasedExtensionMethodBaseRepository>());

            return services;
        }
    }
}