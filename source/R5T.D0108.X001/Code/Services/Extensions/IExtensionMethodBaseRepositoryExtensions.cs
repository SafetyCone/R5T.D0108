using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.Magyar;

using R5T.D0108;
using R5T.T0100;


namespace System
{
    public static class IExtensionMethodBaseRepositoryExtensions
    {
        public static async Task AddDuplicateExtensionMethodBaseNameSelection(this IExtensionMethodBaseRepository extensionMethodBaseRepository,
            ExtensionMethodBaseNameSelection duplicateExtensionMethodBaseNameSelection)
        {
            await extensionMethodBaseRepository.AddDuplicateExtensionMethodBaseNameSelections(EnumerableHelper.From(duplicateExtensionMethodBaseNameSelection));
        }

        public static async Task AddIgnoredExtensionMethodBaseNamespacedTypeName(this IExtensionMethodBaseRepository extensionMethodBaseRepository,
            string extensionMethodBaseNamespacedTypeName)
        {
            await extensionMethodBaseRepository.AddIgnoredExtensionMethodBaseNamespacedTypeNames(EnumerableHelper.From(extensionMethodBaseNamespacedTypeName));
        }

        public static async Task<ExtensionMethodBase[]> GetExpectedUniqueExtensionMethodBases(this IExtensionMethodBaseRepository extensionMethodBaseRepository)
        {
            var extensionMethodBases = await extensionMethodBaseRepository.GetAllExtensionMethodBases();
            var duplicateExtensionMethodBaseNameSelections = await extensionMethodBaseRepository.GetAllDuplicateExtensionMethodBaseNameSelections();
            var ignoredNamespacedTypeNames = await extensionMethodBaseRepository.GetAllIgnoredExtensionMethodBaseNamespacedTypeNames();

            var uniqueExtensionMethodBases = extensionMethodBases.GetUniqueItems(
                duplicateExtensionMethodBaseNameSelections,
                ignoredNamespacedTypeNames);

            return uniqueExtensionMethodBases;
        }

        public static async Task UpdateExtensionMethodBaseNameSelections(this IExtensionMethodBaseRepository extensionMethodBaseRepository)
        {
            var expectedUniqueExtensionMethodBases = await extensionMethodBaseRepository.GetExpectedUniqueExtensionMethodBases();

            var expectedExtensionMethodBaseNameSelections = expectedUniqueExtensionMethodBases
                .Select(x => new ExtensionMethodBaseNameSelection
                {
                    ExtensionMethodBaseIdentity = x.Identity,
                    ExtensionMethodBaseNamespacedTypeName = x.NamespacedTypeName
                })
                ;

            // Just clear and add all.
            await extensionMethodBaseRepository.ClearAllExtensionMethodBaseNameSelections();

            await extensionMethodBaseRepository.AddExtensionMethodBaseNameSelections(expectedExtensionMethodBaseNameSelections);
        }

        public static async Task<Dictionary<Guid, bool>> DeleteExtensionMethodBaseOnly(this IExtensionMethodBaseRepository extensionMethodBaseRepository,
            IEnumerable<ExtensionMethodBase> extensionMethodBases)
        {
            extensionMethodBases.VerifyAllIdentitiesAreSet();

            var embIdentities = extensionMethodBases
                .Select(xProject => xProject.Identity)
                ;

            var output = await extensionMethodBaseRepository.DeleteExtensionMethodBases(embIdentities);
            return output;
        }

        public static async Task<Dictionary<Guid, bool>> DeleteExtensionMethodBasesAndDependentData(this IExtensionMethodBaseRepository extensionMethodBaseRepository,
            IEnumerable<ExtensionMethodBase> extensionMethodBases)
        {
            var output = await extensionMethodBaseRepository.DeleteExtensionMethodBaseOnly(extensionMethodBases);

            var extensionMethodBaseNamespacedTypeNames = extensionMethodBases
                .Select(xEmb => xEmb.NamespacedTypeName)
                ;

            await extensionMethodBaseRepository.DeleteExtensionMethodBaseNameSelections(extensionMethodBaseNamespacedTypeNames);

            return output;
        }

        public static async Task<Dictionary<Guid, bool>> DeleteExtensionMethodBases(this IExtensionMethodBaseRepository extensionMethodBaseRepository,
            IEnumerable<ExtensionMethodBase> extensionMethodBases)
        {
            var output = await extensionMethodBaseRepository.DeleteExtensionMethodBasesAndDependentData(extensionMethodBases);
            return output;
        }

        public static async Task<Dictionary<Guid, bool>> DeleteToProjectMappings(this IExtensionMethodBaseRepository extensionMethodBaseRepository,
            IEnumerable<ExtensionMethodBaseToProjectMapping> mappings)
        {
            var embIdentities = mappings
               .Select(x => x.ExtensionMethodBaseIdentity)
               ;

            var output = await extensionMethodBaseRepository.DeleteToProjectMappings(embIdentities);
            return output;
        }

        public static async Task<ExtensionMethodBase[]> GetSelectedExtensionMethodBases(this IExtensionMethodBaseRepository extensionMethodBaseRepository)
        {
            var (embs, selectedEmbNames) = await TaskHelper.WhenAll(
                extensionMethodBaseRepository.GetAllExtensionMethodBases(),
                extensionMethodBaseRepository.GetAllExtensionMethodBaseNameSelections());

            var selectedEmbIdentitiesHash = new HashSet<Guid>(selectedEmbNames
                .Select(x => x.ExtensionMethodBaseIdentity));

            var selectedEmbs = embs
                .Where(xEmb => selectedEmbIdentitiesHash.Contains(xEmb.Identity))
                .ToArray();

            return selectedEmbs;
        }
    }
}