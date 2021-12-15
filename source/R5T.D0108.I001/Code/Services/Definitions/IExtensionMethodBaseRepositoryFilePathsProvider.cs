using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.D0108.I001
{
    [ServiceDefinitionMarker]
    public interface IExtensionMethodBaseRepositoryFilePathsProvider : IServiceDefinition
    {
        Task<string> GetExtensionMethodBasesListingJsonFilePath();
        Task<string> GetExtensionMethodBaseNameSelectionsTextFilePath();
        Task<string> GetIgnoredExtensionMethodBaseNamesTextFilePath();
        Task<string> GetDuplicateExtensionMethodBaseNamesTextFilePath();
        Task<string> GetToProjectMappingsJsonFilePath();
    }
}
