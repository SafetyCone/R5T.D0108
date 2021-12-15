using System;

using R5T.T0062;
using R5T.T0063;


namespace R5T.D0108.I001
{
    public static class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="FileBasedExtensionMethodBaseRepository"/> implementation of <see cref="IFileBasedExtensionMethodBaseRepository"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IFileBasedExtensionMethodBaseRepository> AddFileBasedExtensionMethodBaseRepositoryAction(this IServiceAction _,
            IServiceAction<IExtensionMethodBaseRepositoryFilePathsProvider> extensionMethodBaseRepositoryFilePathsProviderAction)
        {
            var serviceAction = _.New<IFileBasedExtensionMethodBaseRepository>(services => services.AddFileBasedExtensionMethodBaseRepository(
                extensionMethodBaseRepositoryFilePathsProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Forwards the <see cref="IFileBasedExtensionMethodBaseRepository"/> service to <see cref="IExtensionMethodBaseRepository"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IExtensionMethodBaseRepository> ForwardToIExtensionMethodBaseRepositoryAction(this IServiceAction _,
            IServiceAction<IFileBasedExtensionMethodBaseRepository> IFileBasedExtensionMethodBaseRepositoryAction)
        {
            var serviceAction = _.New<IExtensionMethodBaseRepository>(services => services.ForwardToIExtensionMethodBaseRepository(
                IFileBasedExtensionMethodBaseRepositoryAction));

            return serviceAction;
        }
    }
}
