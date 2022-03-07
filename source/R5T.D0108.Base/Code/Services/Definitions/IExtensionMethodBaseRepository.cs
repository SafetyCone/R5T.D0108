using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.Magyar;

using R5T.T0064;
using R5T.T0100;


namespace R5T.D0108
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// In general:
    /// * Add methods for atomics will check if the object already has an identity value, and if so, will make sure it's unique, and if not, create a new identity value and set the object's identity value.
    /// </remarks>
    [ServiceDefinitionMarker]
    public interface IExtensionMethodBaseRepository : IServiceDefinition
    {
        #region Extension Method Base

        Task AddExtensionMethodBase(ExtensionMethodBase extensionMethodBase);

        Task AddExtensionMethodBases(IEnumerable<ExtensionMethodBase> extensionMethodBases);

        Task<WasFound<ExtensionMethodBase>> HasExtensionMethodBase(ExtensionMethodBase extensionMethodBase);

        /// <summary>
        /// Identity is unique.
        /// </summary>
        Task<WasFound<ExtensionMethodBase>> HasExtensionMethodBase(Guid identity);

        Task<Dictionary<Guid, WasFound<ExtensionMethodBase>>> HasExtensionMethodBases(
            IEnumerable<Guid> identities);

        /// <summary>
        /// Namespaced type name is not unique. (There might be multiple projects with the same namespaced type name, unfortunately.)
        /// </summary>
        Task<ExtensionMethodBase[]> HasExtensionMethodBasesByNamespacedTypeName(string namespacedTypeName);

        /// <summary>
        /// Code file path is not unique. (There might be multiple extension method base interface types defined in a single code file.)
        /// </summary>
        Task<ExtensionMethodBase[]> HasExtensionMethodBasesByCodeFilePath(string codeFilePath);

        /// <summary>
        /// The combination of namespaced type name and code file path is unique. (There cannot be two identically namespaced named types in the same compilation unit, and a file cannot be broken into multiple compilation units.)
        /// </summary>
        Task<WasFound<ExtensionMethodBase>> HasExtensionMethodBase(string namespacedTypeName, string codeFilePath);

        Task<ExtensionMethodBase[]> GetAllExtensionMethodBases();
        Task<string[]> GetAllDistinctExtensionMethodBaseCodeFilePaths();

        // No update.

        Task<bool> DeleteExtensionMethodBase(Guid identity);

        Task<Dictionary<Guid, bool>> DeleteExtensionMethodBases(IEnumerable<Guid> extensionMethodBaseIdentities);

        Task<bool> DeleteExtensionMethodBase(string namespacedTypeName, string codeFilePath);

        Task ClearAllExtensionMethodBases();

        #endregion

        #region Extension Method Base Name Selections

        Task AddExtensionMethodBaseNameSelection(ExtensionMethodBaseNameSelection extensionMethodBaseNameSelection);

        Task AddExtensionMethodBaseNameSelections(IEnumerable<ExtensionMethodBaseNameSelection> extensionMethodBaseNameSelections);

        Task<WasFound<ExtensionMethodBaseNameSelection>> HasExtensionMethodBaseNameSelection(ExtensionMethodBaseNameSelection extensionMethodBaseNameSelection);

        /// <summary>
        /// Extension method base namespaced type name is unique in extension method base name selections.
        /// </summary>
        Task<WasFound<ExtensionMethodBaseNameSelection>> HasExtensionMethodBaseNameSelection(string extensionMethodBaseNamespacedTypeName);

        Task<WasFound<ExtensionMethodBaseNameSelection>> HasExtensionMethodBaseNameSelection(Guid extensionMethodBaseIdentity);

        Task<ExtensionMethodBaseNameSelection[]> GetAllExtensionMethodBaseNameSelections();

        Task UpdateExtensionMethodBaseNameSelection(Guid extensionMethodBaseIdentity, string newExtensionMethodBaseNamespacedTypeName);
        Task UpdateExtensionMethodBaseNameSelection(string extensionMethodBaseNamespacedTypeName, Guid newExtensionMethodBaseIdentity);

        Task<bool> DeleteExtensionMethodBaseNameSelection(string extensionMethodBaseNamespacedTypeName);

        Task<Dictionary<string, bool>> DeleteExtensionMethodBaseNameSelections(IEnumerable<string> extensionMethodBaseNamespacedTypeNames);

        Task<bool> DeleteExtensionMethodBaseNameSelection(Guid extensionMethodBaseIdentity);

        Task ClearAllExtensionMethodBaseNameSelections();

        #endregion

        #region Ignored Extension Method Base Namespaced Type Names

        Task AddIgnoredExtensionMethodBaseNamespacedTypeNames(IEnumerable<string> extensionMethodBaseNamespacedTypeName);

        Task<WasFound<string>> HasIgnoredExtensionMethodBaseNamespacedTypeName(string extensionMethodBaseNamespacedTypeName);

        Task<string[]> GetAllIgnoredExtensionMethodBaseNamespacedTypeNames();

        // No update.

        Task<bool> DeleteIgnoredExtensionMethodBaseNamespacedTypeName(string extensionMethodBaseNamespacedTypeName);

        Task ClearAllIgnoredExtensionMethodBaseNamespacedTypeNames();

        #endregion

        #region Duplicate Extension Method Base Name Selections

        Task AddDuplicateExtensionMethodBaseNameSelections(IEnumerable<ExtensionMethodBaseNameSelection> extensionMethodBaseNameSelections);

        Task<WasFound<ExtensionMethodBaseNameSelection>> HasDuplicateExtensionMethodBaseNameSelection(ExtensionMethodBaseNameSelection extensionMethodBaseNameSelection);

        /// <summary>
        /// Extension method base namespaced type name is unique in duplicate extension method base selections.
        /// </summary>
        Task<WasFound<ExtensionMethodBaseNameSelection>> HasDuplicateExtensionMethodBaseNameSelection(string extensionMethodBaseNamespacedTypeName);

        Task<WasFound<ExtensionMethodBaseNameSelection>> HasDuplicateExtensionMethodBaseNameSelection(Guid extensionMethodBaseidentity);

        Task<ExtensionMethodBaseNameSelection[]> GetAllDuplicateExtensionMethodBaseNameSelections();

        Task UpdateDuplicateExtensionMethodBaseNameSelection(string extensionMethodBaseNamespacedTypeName, Guid newExtensionMethodBaseIdentity);

        Task<bool> DeleteDuplicateExtensionMethodBaseNameSelection(string extensionMethodBaseNamespacedTypeName);

        Task<bool> DeleteDuplicateExtensionMethodBaseNameSelection(Guid extensionMethodBaseidentity);

        Task ClearAllDuplicateExtensionMethodBaseNameSelections();

        #endregion

        #region To Project Mappings

        Task AddToProjectMapping(ExtensionMethodBaseToProjectMapping toProjectMappings);

        Task AddToProjectMappings(IEnumerable<ExtensionMethodBaseToProjectMapping> toProjectMappings);

        Task<WasFound<ExtensionMethodBaseToProjectMapping>> HasToProjectMapping(ExtensionMethodBaseToProjectMapping toProjectMappings);

        /// <summary>
        /// Extension method base identity is unique. (Assume the same extension method base cannot be in multiple projects, which is not technically correct. For example, multiple project files can exist in the same project directory, covering the same code file. But make the assumption.)
        /// </summary>
        Task<WasFound<ExtensionMethodBaseToProjectMapping>> HasToProjectMappingByExtensionMethodBase(Guid extensionMethodBaseIdentity);

        /// <summary>
        /// Project identity is not unique. (There can be multiple extension method bases in a single project.)
        /// </summary>
        Task<ExtensionMethodBaseToProjectMapping[]> HasToProjectMappingsByProject(Guid projectIdentity);

        /// <summary>
        /// The pair of extension method base identity and project identity is unique.
        /// </summary>
        Task<WasFound<ExtensionMethodBaseToProjectMapping>> HasToProjectMapping(Guid extensionMethodBaseIdentity, Guid projectIdentity);

        Task<ExtensionMethodBaseToProjectMapping[]> GetAllToProjectMappings();

        // No update.

        Task<bool> DeleteToProjectMapping(Guid extensionMethodBaseIdentity);

        Task<Dictionary<Guid, bool>> DeleteToProjectMappings(IEnumerable<Guid> extensionMethodBaseIdentities);

        Task ClearAllToProjectMappings();

        #endregion
    }
}