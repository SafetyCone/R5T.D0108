using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.Magyar;

using R5T.T0064;
using R5T.T0100;
using R5T.T0100.X002;


namespace R5T.D0108.I001
{
    [ServiceImplementationMarker]
    public class FileBasedExtensionMethodBaseRepository : IFileBasedExtensionMethodBaseRepository, IServiceImplementation
    {
        #region Static

        private static Dictionary<ExtensionMethodBaseNameSelection, WasFound<ExtensionMethodBaseNameSelection>> HasExtensionMethodBaseNameSelections(
            IEnumerable<ExtensionMethodBaseNameSelection> extensionMethodBaseNameSelections,
            IEnumerable<ExtensionMethodBaseNameSelection> newExtensionMethodBaseNameSelections)
        {
            var output = Instances.Operation.GetWasFoundByValue(
                extensionMethodBaseNameSelections,
                newExtensionMethodBaseNameSelections,
                FileBasedExtensionMethodBaseRepository.HasExtensionMethodBaseNameSelection);

            return output;
        }

        private static Dictionary<ExtensionMethodBase, WasFound<ExtensionMethodBase>> HasExtensionMethodBases(IEnumerable<ExtensionMethodBase> extensionMethodBases, IEnumerable<ExtensionMethodBase> newExtensionMethodBases)
        {
            var output = Instances.Operation.GetWasFoundByValue(
                extensionMethodBases,
                newExtensionMethodBases,
                FileBasedExtensionMethodBaseRepository.HasExtensionMethodBase);

            return output;
        }

        private static Dictionary<ExtensionMethodBaseToProjectMapping, WasFound<ExtensionMethodBaseToProjectMapping>> HasToProjectMappings(
            IEnumerable<ExtensionMethodBaseToProjectMapping> extensionMethodBaseToProjectMappings,
            IEnumerable<ExtensionMethodBaseToProjectMapping> newExtensionMethodBaseToProjectMappings)
        {
            var output = Instances.Operation.GetWasFoundByValue(
                extensionMethodBaseToProjectMappings,
                newExtensionMethodBaseToProjectMappings,
                FileBasedExtensionMethodBaseRepository.HasToProjectMapping);

            return output;
        }

        private static WasFound<ExtensionMethodBaseToProjectMapping> HasToProjectMapping(IEnumerable<ExtensionMethodBaseToProjectMapping> toProjectMappings,
            ExtensionMethodBaseToProjectMapping mapping)
        {
            var output = FileBasedExtensionMethodBaseRepository.HasToProjectMapping(toProjectMappings,
                mapping.ExtensionMethodBaseIdentity,
                mapping.ProjectIdentity);

            return output;
        }

        private static WasFound<ExtensionMethodBaseNameSelection> HasExtensionMethodBaseNameSelection(
            IEnumerable<ExtensionMethodBaseNameSelection> extensionMethodBaseNameSelections,
            ExtensionMethodBaseNameSelection extensionMethodBaseNameSelection)
        {
            var output = extensionMethodBaseNameSelections.FindSingleByIdentityOrThenName(extensionMethodBaseNameSelection);
            return output;
        }

        private static WasFound<ExtensionMethodBase> HasExtensionMethodBase(IEnumerable<ExtensionMethodBase> extensionMethodBases,
            ExtensionMethodBase extensionMethodBase)
        {
            var output = extensionMethodBases.FindSingleByIdentityOrThenFilePath(extensionMethodBase);
            return output;
        }

        private static ExtensionMethodBaseToProjectMapping[] HasToProjectMappingsByProject(IEnumerable<ExtensionMethodBaseToProjectMapping> toProjectMappings,
            Guid projectIdentity)
        {
            var mappings = toProjectMappings.FindArrayByExternalIdentity(projectIdentity);
            return mappings;
        }

        private static WasFound<ExtensionMethodBaseToProjectMapping> HasToProjectMappingByExtensionMethodBase(IEnumerable<ExtensionMethodBaseToProjectMapping> toProjectMappings,
            Guid extensionMethodBaseIdentity)
        {
            var output = toProjectMappings.FindSingleByLocalIdentity(extensionMethodBaseIdentity);
            return output;
        }

        private static WasFound<ExtensionMethodBaseToProjectMapping> HasToProjectMapping(IEnumerable<ExtensionMethodBaseToProjectMapping> toProjectMappings,
            Guid extensionMethodBaseIdentity, Guid projectIdentity)
        {
            var output = toProjectMappings.FindSingleByIdentities(extensionMethodBaseIdentity, projectIdentity);
            return output;
        }

        private static WasFound<string> HasIgnoredExtensionMethodBaseNamespacedTypeName(IEnumerable<string> ignoredExtensionMethodBaseNamespacedTypeNames,
            string extensionMethodBaseNamespacedTypeName)
        {
            var output = ignoredExtensionMethodBaseNamespacedTypeNames.FindSingleByValue(extensionMethodBaseNamespacedTypeName);
            return output;
        }

        private static ExtensionMethodBase[] HasExtensionMethodBasesByNamespacedTypeName(IEnumerable<ExtensionMethodBase> extensionMethodBases, string namespacedTypeName)
        {
            var output = extensionMethodBases.FindArrayByName(namespacedTypeName);
            return output;
        }

        private static ExtensionMethodBase[] HasExtensionMethodBasesByCodeFilePath(IEnumerable<ExtensionMethodBase> extensionMethodBases, string codeFilePath)
        {
            var output = extensionMethodBases.FindArrayByFilePath(codeFilePath);
            return output;
        }

        private static WasFound<ExtensionMethodBaseNameSelection> HasExtensionMethodBaseNameSelection(IEnumerable<ExtensionMethodBaseNameSelection> extensionMethodBaseNameSelections,
            Guid extensionMethodBaseIdentity)
        {
            var output = extensionMethodBaseNameSelections.FindSingleByIdentity(extensionMethodBaseIdentity);
            return output;
        }

        private static WasFound<ExtensionMethodBaseNameSelection> HasExtensionMethodBaseNameSelection(IEnumerable<ExtensionMethodBaseNameSelection> extensionMethodBaseNameSelections,
            string extensionMethodBaseNamespacedTypeName)
        {
            var output = extensionMethodBaseNameSelections.FindSingleByName(extensionMethodBaseNamespacedTypeName);
            return output;
        }

        private static WasFound<ExtensionMethodBase> HasExtensionMethodBase(IEnumerable<ExtensionMethodBase> extensionMethodBases, string namespacedTypeName, string codeFilePath)
        {
            var output = extensionMethodBases.FindSingleByNameAndFilePath(namespacedTypeName, codeFilePath);
            return output;
        }

        private static WasFound<ExtensionMethodBaseNameSelection> HasDuplicateExtensionMethodBaseNameSelection(
            IEnumerable<ExtensionMethodBaseNameSelection> duplicateExtensionMethodBaseNameSelections,
            string extensionMethodBaseNamespacedTypeName)
        {
            var output = duplicateExtensionMethodBaseNameSelections.FindSingleByName(extensionMethodBaseNamespacedTypeName);
            return output;
        }

        private static WasFound<ExtensionMethodBaseNameSelection> HasDuplicateExtensionMethodBaseNameSelection(
            IEnumerable<ExtensionMethodBaseNameSelection> duplicateExtensionMethodBaseNameSelections,
            Guid extensionMethodBaseIdentity)
        {
            var output = duplicateExtensionMethodBaseNameSelections.FindSingleByIdentity(extensionMethodBaseIdentity);
            return output;
        }

        private static WasFound<ExtensionMethodBase> HasExtensionMethodBase(IEnumerable<ExtensionMethodBase> extensionMethodBases, Guid identity)
        {
            var output = extensionMethodBases.FindSingleByIdentity(identity);
            return output;
        }

        #endregion


        private IExtensionMethodBaseRepositoryFilePathsProvider ExtensionMethodBaseRepositoryFilePathsProvider { get; }


        public FileBasedExtensionMethodBaseRepository(
            IExtensionMethodBaseRepositoryFilePathsProvider extensionMethodBaseRepositoryFilePathsProvider)
        {
            this.ExtensionMethodBaseRepositoryFilePathsProvider = extensionMethodBaseRepositoryFilePathsProvider;
        }

        //private bool IsEmpty()
        //{
        //    var output = true
        //        && this.ExtensionMethodBases.None()
        //        && this.IgnoredExtensionMethodBaseNamespacedTypeNames.None()
        //        && this.DuplicateExtensionMethodBaseNameSelections.None()
        //        && this.ExtensionMethodBaseNameSelections.None()
        //        && this.ToProjectMappings.None();
        //    ;

        //    return output;
        //}

        //public void ClearAll()
        //{
        //    this.ExtensionMethodBaseNameSelections.Clear();
        //    this.DuplicateExtensionMethodBaseNameSelections.Clear();
        //    this.IgnoredExtensionMethodBaseNamespacedTypeNames.Clear();
        //    this.ExtensionMethodBases.Clear();
        //    this.ToProjectMappings.Clear();
        //}

        ///// <summary>
        ///// If the repository is not empty, empties it.
        ///// </summary>
        //public void EnsureIsEmpty()
        //{
        //    var isEmpty = this.IsEmpty();
        //    if (!isEmpty)
        //    {
        //        this.ClearAll();
        //    }
        //}

        ///// <summary>
        ///// Throws an exception if the repository is not empty.
        ///// </summary>
        //public void VerifyIsEmpty()
        //{
        //    var isEmpty = this.IsEmpty();
        //    if (!isEmpty)
        //    {
        //        throw new Exception("Repository is not empty.");
        //    }
        //}

        #region Files Load/Save

        private async Task<ExtensionMethodBase[]> LoadExtensionMethodBases()
        {
            var listingJsonFilePath = await this.ExtensionMethodBaseRepositoryFilePathsProvider.GetExtensionMethodBasesListingJsonFilePath();

            var extensionMethodBases = Instances.FileSystemOperator.FileExists(listingJsonFilePath)
                ? Instances.FileSystemOperator.LoadExtensionMethodBasesFromJsonFile(listingJsonFilePath)
                : Array.Empty<ExtensionMethodBase>()
                ;

            return extensionMethodBases;
        }

        private async Task SaveExtensionMethodBases(IEnumerable<ExtensionMethodBase> extensionMethodBases)
        {
            var listingJsonFilePath = await this.ExtensionMethodBaseRepositoryFilePathsProvider.GetExtensionMethodBasesListingJsonFilePath();

            Instances.FileSystemOperator.WriteToJsonFile(
                listingJsonFilePath,
                extensionMethodBases
                    .OrderAlphabetically(xExtensionMethodBase => xExtensionMethodBase.NamespacedTypeName));
        }

        private async Task<ExtensionMethodBaseNameSelection[]> LoadExtensionMethodBaseNameSelections()
        {
            var nameSelectionsTextFilePath = await this.ExtensionMethodBaseRepositoryFilePathsProvider.GetExtensionMethodBaseNameSelectionsTextFilePath();

            var nameSelectionValues = Instances.FileSystemOperator.FileExists(nameSelectionsTextFilePath)
                ? await Instances.DuplicateValuesOperator.LoadDuplicateValueSelections(nameSelectionsTextFilePath)
                : new Dictionary<string, string>()
                ;

            // File is formatted as {extension method base namespaced type name}| {identity}, which sure, is inconvenient for human analysis, but is required since code file path is not unique. (TODO: provide some other file export of this data.)
            var nameSelections = nameSelectionValues
                .Select(xPair => new ExtensionMethodBaseNameSelection
                {
                    ExtensionMethodBaseIdentity = Instances.GuidOperator.FromStringStandard(xPair.Value),
                    ExtensionMethodBaseNamespacedTypeName = xPair.Key
                })
                .ToArray()
                ;

            return nameSelections;
        }

        private async Task SaveExtensionMethodBaseNameSelections(IEnumerable<ExtensionMethodBaseNameSelection> extensionMethodBaseNameSelections)
        {
            var nameSelectionsTextFilePath = await this.ExtensionMethodBaseRepositoryFilePathsProvider.GetExtensionMethodBaseNameSelectionsTextFilePath();

            // File is formatted as {extension method base namespaced type name}| {identity}, which sure, is inconvenient for human analysis, but is required since code file path is not unique. (TODO: provide some other file export of this data.)
            var nameSelectionValues = extensionMethodBaseNameSelections
                .OrderAlphabetically(xExtensionMethodBaseNameSelection => xExtensionMethodBaseNameSelection.ExtensionMethodBaseNamespacedTypeName)
                .ToDictionary(
                    xExtensionMethodBaseNameSelection => xExtensionMethodBaseNameSelection.ExtensionMethodBaseNamespacedTypeName,
                    xExtensionMethodBaseNameSelection => Instances.GuidOperator.ToStringStandard(xExtensionMethodBaseNameSelection.ExtensionMethodBaseIdentity));

            await Instances.DuplicateValuesOperator.SaveDuplicateValueSelections(
                nameSelectionsTextFilePath,
                nameSelectionValues);
        }

        private async Task<string[]> LoadIgnoredExtensionMethodBaseNamespacedTypeNames()
        {
            var ignoredNamesTextFilepath = await this.ExtensionMethodBaseRepositoryFilePathsProvider.GetIgnoredExtensionMethodBaseNamesTextFilePath();

            var ignoredNamespacedTypeNames = Instances.FileSystemOperator.FileExists(ignoredNamesTextFilepath)
                ? await Instances.IgnoredValuesOperator.LoadIgnoredValues(ignoredNamesTextFilepath)
                : new HashSet<string>()
                ;

            var output = ignoredNamespacedTypeNames.ToArray();
            return output;
        }

        private async Task SaveIgnoredExtensionMethodBaseNamespacedTypeNames(IEnumerable<string> ignoredExtensionMethodBaseNamespacedTypeNames)
        {
            var ignoredNamesTextFilepath = await this.ExtensionMethodBaseRepositoryFilePathsProvider.GetIgnoredExtensionMethodBaseNamesTextFilePath();

            await Instances.IgnoredValuesOperator.SaveIgnoredValues(
                ignoredNamesTextFilepath,
                ignoredExtensionMethodBaseNamespacedTypeNames
                    .OrderAlphabetically());
        }

        private async Task<ExtensionMethodBaseNameSelection[]> LoadDuplicateExtensionMethodBaseNameSelections()
        {
            // Duplicate extension method base name selections.
            var duplicateNameSelectionsTextFilePath = await this.ExtensionMethodBaseRepositoryFilePathsProvider.GetDuplicateExtensionMethodBaseNamesTextFilePath();

            var duplicateNameSelections = await Instances.Operation.LoadDuplicateExtensionMethodBaseNameSelections(duplicateNameSelectionsTextFilePath);
            return duplicateNameSelections;
        }

        private async Task SaveDuplicateExtensionMethodBaseNameSelections(IEnumerable<ExtensionMethodBaseNameSelection> duplicateExtensionMethodBaseNameSelections)
        {
            var duplicateNameSelectionsTextFilePath = await this.ExtensionMethodBaseRepositoryFilePathsProvider.GetDuplicateExtensionMethodBaseNamesTextFilePath();

            await Instances.Operation.SaveExtensionMethodBaseNameSelections(
                duplicateNameSelectionsTextFilePath,
                duplicateExtensionMethodBaseNameSelections);
        }

        private async Task<ExtensionMethodBaseToProjectMapping[]> LoadToProjectMappings()
        {
            // To project mappings.
            var toProjectMappingsJsonFilePath = await this.ExtensionMethodBaseRepositoryFilePathsProvider.GetToProjectMappingsJsonFilePath();

            var toProjectMappings = Instances.FileSystemOperator.FileExists(toProjectMappingsJsonFilePath)
                ? Instances.FileSystemOperator.LoadExtensionMethodBaseToProjectMappingsFromJsonFile(toProjectMappingsJsonFilePath)
                : Array.Empty<ExtensionMethodBaseToProjectMapping>()
                ;

            return toProjectMappings;
        }

        private async Task SaveToProjectMappings(IEnumerable<ExtensionMethodBaseToProjectMapping> extensionMethodBaseToProjectMappings)
        {
            var toProjectMappingsJsonFilePath = await this.ExtensionMethodBaseRepositoryFilePathsProvider.GetToProjectMappingsJsonFilePath();

            Instances.FileSystemOperator.WriteToJsonFile(
                toProjectMappingsJsonFilePath,
                extensionMethodBaseToProjectMappings);
        }

        #endregion

        public async Task AddDuplicateExtensionMethodBaseNameSelections(IEnumerable<ExtensionMethodBaseNameSelection> newDuplicateExtensionMethodBaseNameSelections)
        {
            var duplicateExtensionMethodBaseNameSelections = await this.LoadDuplicateExtensionMethodBaseNameSelections();

            foreach (var duplicateExtensionMethodBaseNameSelection in duplicateExtensionMethodBaseNameSelections)
            {
                var hasDuplicateNameSelection = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBaseNameSelection(
                    duplicateExtensionMethodBaseNameSelections,
                    duplicateExtensionMethodBaseNameSelection);

                if (hasDuplicateNameSelection)
                {
                    throw new Exception("Duplicate extension method base already exists.");
                }
            }

            // Else, modify and save.
            var modifiedDuplicateExtensionMethodBaseNameSelections = duplicateExtensionMethodBaseNameSelections
                .AppendRange(newDuplicateExtensionMethodBaseNameSelections)
                ;

            await this.SaveDuplicateExtensionMethodBaseNameSelections(modifiedDuplicateExtensionMethodBaseNameSelections);
        }

        public async Task AddExtensionMethodBase(ExtensionMethodBase extensionMethodBase)
        {
            extensionMethodBase.SetIdentityIfNotSet();

            var extensionMethodBases = await this.LoadExtensionMethodBases();

            var hasExtensionMethodBase = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBase(extensionMethodBases, extensionMethodBase);
            if (hasExtensionMethodBase)
            {
                throw new Exception("Extension method base already exists.");
            }

            // Else, modify and save.
            var modifiedExtensionMethodBases = extensionMethodBases
                .Append(extensionMethodBase)
                ;

            await this.SaveExtensionMethodBases(modifiedExtensionMethodBases);
        }

        public async Task AddExtensionMethodBases(IEnumerable<ExtensionMethodBase> newExtensionMethodBases)
        {
            newExtensionMethodBases.SetIdentitiesIfNotSet();

            var extensionMethodBases = await this.LoadExtensionMethodBases();

            // Do the new extension method bases already exist?
            var hasExtensionMethodBaseByExtensionMethodBase = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBases(extensionMethodBases, newExtensionMethodBases);

            var anyExtensionMethodBasesExist = hasExtensionMethodBaseByExtensionMethodBase
                .Where(xPair => xPair.Value.Exists)
                .Any();

            if (anyExtensionMethodBasesExist)
            {
                throw new Exception("Some extension method bases already exist.");
            }

            var modifiedExtensionMethodBases = extensionMethodBases.AppendRange(newExtensionMethodBases);

            await this.SaveExtensionMethodBases(modifiedExtensionMethodBases);
        }

        public async Task AddExtensionMethodBaseNameSelection(ExtensionMethodBaseNameSelection extensionMethodBaseNameSelection)
        {
            var extensionMethodBaseNameSelections = await this.LoadExtensionMethodBaseNameSelections();

            var hasExtensionMethodBaseNameSelection = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBaseNameSelection(
                extensionMethodBaseNameSelections,
                extensionMethodBaseNameSelection);
            if (hasExtensionMethodBaseNameSelection)
            {
                throw new Exception("Extension method base name selection already exists.");
            }

            // Else, modify and save.
            var modifiedExtensionMethodBaseNameSelections = extensionMethodBaseNameSelections
                .Append(extensionMethodBaseNameSelection)
                ;

            await this.SaveExtensionMethodBaseNameSelections(modifiedExtensionMethodBaseNameSelections);
        }

        public async Task AddExtensionMethodBaseNameSelections(IEnumerable<ExtensionMethodBaseNameSelection> newExtensionMethodBaseNameSelections)
        {
            var extensionMethodBaseNameSelections = await this.LoadExtensionMethodBaseNameSelections();

            var hasExtensionMethodBaseNameSelectionByExtensionMethodBaseNameSelection = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBaseNameSelections(
                extensionMethodBaseNameSelections,
                newExtensionMethodBaseNameSelections);

            var anyExtensionMethodBaseNameSelectionsExist = hasExtensionMethodBaseNameSelectionByExtensionMethodBaseNameSelection
                .Where(xPair => xPair.Value.Exists)
                .Any();

            if (anyExtensionMethodBaseNameSelectionsExist)
            {
                throw new Exception("Some extension method base name selections already exist.");
            }

            var modifiedExtensionMethodBaseNameSelections = extensionMethodBaseNameSelections.AppendRange(newExtensionMethodBaseNameSelections);

            await this.SaveExtensionMethodBaseNameSelections(modifiedExtensionMethodBaseNameSelections);
        }

        public async Task AddIgnoredExtensionMethodBaseNamespacedTypeNames(IEnumerable<string> extensionMethodBaseNamespacedTypeNames)
        {
            var ignoredNamespacedTypeNames = await this.LoadIgnoredExtensionMethodBaseNamespacedTypeNames();

            foreach (var extensionMethodBaseNamespacedTypeName in extensionMethodBaseNamespacedTypeNames)
            {
                var hasIgnoredNamespacedTypeName = FileBasedExtensionMethodBaseRepository.HasIgnoredExtensionMethodBaseNamespacedTypeName(
                    ignoredNamespacedTypeNames,
                    extensionMethodBaseNamespacedTypeName);

                if (hasIgnoredNamespacedTypeName)
                {
                    throw new Exception("Ignored namespaced type name already exists.");
                }
            }

            // Else, modify and save.
            var modifiedIgnoredNamespacedTypeName = ignoredNamespacedTypeNames
                .AppendRange(extensionMethodBaseNamespacedTypeNames)
                ;

            await this.SaveIgnoredExtensionMethodBaseNamespacedTypeNames(modifiedIgnoredNamespacedTypeName);
        }

        public async Task AddToProjectMapping(ExtensionMethodBaseToProjectMapping mapping)
        {
            var toProjectMappings = await this.LoadToProjectMappings();

            var hasToProjectMapping = FileBasedExtensionMethodBaseRepository.HasToProjectMapping(
                toProjectMappings,
                mapping);
            if (hasToProjectMapping)
            {
                throw new Exception("To project mapping already exists.");
            }

            // Else modify and save.
            var modifiedToProjectMappings = toProjectMappings
                .Append(mapping)
                ;

            await this.SaveToProjectMappings(modifiedToProjectMappings);
        }

        public async Task AddToProjectMappings(IEnumerable<ExtensionMethodBaseToProjectMapping> newToProjectMappings)
        {
            var toProjectMappings = await this.LoadToProjectMappings();

            var hasToProjectMappingsByToProjectMapping = FileBasedExtensionMethodBaseRepository.HasToProjectMappings(
                toProjectMappings,
                newToProjectMappings);

            var anyToProjectMappingsExist = hasToProjectMappingsByToProjectMapping
                .Where(xPair => xPair.Value.Exists)
                .Any();

            if (anyToProjectMappingsExist)
            {
                throw new Exception("Some to-project mappings already exist.");
            }

            var modifiedToProjectMappings = toProjectMappings.AppendRange(newToProjectMappings);

            await this.SaveToProjectMappings(modifiedToProjectMappings);
        }

        public async Task<bool> DeleteDuplicateExtensionMethodBaseNameSelection(string extensionMethodBaseNamespacedTypeName)
        {
            var duplicateExtensionMethodBaseNameSelections = await this.LoadDuplicateExtensionMethodBaseNameSelections();

            var wasFound = FileBasedExtensionMethodBaseRepository.HasDuplicateExtensionMethodBaseNameSelection(
                duplicateExtensionMethodBaseNameSelections,
                extensionMethodBaseNamespacedTypeName);
            {
                var modifiedDuplicateExtensionMethodBaseNameSelections = duplicateExtensionMethodBaseNameSelections
                    .Where(x => x.ExtensionMethodBaseNamespacedTypeName != extensionMethodBaseNamespacedTypeName)
                    ;

                await this.SaveDuplicateExtensionMethodBaseNameSelections(modifiedDuplicateExtensionMethodBaseNameSelections);
            }

            return wasFound;
        }

        public async Task<bool> DeleteDuplicateExtensionMethodBaseNameSelection(Guid extensionMethodBaseidentity)
        {
            var duplicateExtensionMethodBaseNameSelections = await this.LoadDuplicateExtensionMethodBaseNameSelections();

            var wasFound = FileBasedExtensionMethodBaseRepository.HasDuplicateExtensionMethodBaseNameSelection(
                duplicateExtensionMethodBaseNameSelections,
                extensionMethodBaseidentity);
            if (wasFound)
            {
                var modifiedDuplicateExtensionMethodBaseNameSelections = duplicateExtensionMethodBaseNameSelections
                    .Where(x => x.ExtensionMethodBaseIdentity != extensionMethodBaseidentity)
                    ;

                await this.SaveDuplicateExtensionMethodBaseNameSelections(modifiedDuplicateExtensionMethodBaseNameSelections);
            }

            return wasFound;
        }

        public async Task<bool> DeleteExtensionMethodBase(Guid identity)
        {
            var extensionMethodBases = await this.LoadExtensionMethodBases();

            var wasFound = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBase(
                extensionMethodBases,
                identity);
            if (wasFound)
            {
                var modifiedExtensionMethodBases = extensionMethodBases
                    .Where(x => x.Identity != identity)
                    ;

                await this.SaveExtensionMethodBases(modifiedExtensionMethodBases);
            }

            return wasFound;
        }

        public async Task<bool> DeleteExtensionMethodBase(string namespacedTypeName, string codeFilePath)
        {
            var extensionMethodBases = await this.LoadExtensionMethodBases();

            var wasFound = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBase(
                extensionMethodBases,
                namespacedTypeName, codeFilePath);
            if (wasFound)
            {
                var modifiedExtensionMethodBases = extensionMethodBases
                    .Where(x => x.CodeFilePath != codeFilePath && x.NamespacedTypeName != namespacedTypeName)
                    ;

                await this.SaveExtensionMethodBases(extensionMethodBases);
            }

            return wasFound;
        }

        public async Task<bool> DeleteExtensionMethodBaseNameSelection(string extensionMethodBaseNamespacedTypeName)
        {
            var extensionMethodBaseNameSelections = await this.LoadExtensionMethodBaseNameSelections();

            var wasFound = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBaseNameSelection(
                extensionMethodBaseNameSelections,
                extensionMethodBaseNamespacedTypeName);
            if (wasFound)
            {
                var modifiedExtensionMethodBaseNameSelections = extensionMethodBaseNameSelections
                    .Where(x => x.ExtensionMethodBaseNamespacedTypeName != extensionMethodBaseNamespacedTypeName)
                    ;

                await this.SaveExtensionMethodBaseNameSelections(modifiedExtensionMethodBaseNameSelections);
            }

            return wasFound;
        }

        public async Task<bool> DeleteExtensionMethodBaseNameSelection(Guid extensionMethodBaseIdentity)
        {
            var extensionMethodBaseNameSelections = await this.LoadExtensionMethodBaseNameSelections();

            var wasFound = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBaseNameSelection(
                extensionMethodBaseNameSelections,
                extensionMethodBaseIdentity);
            if (wasFound)
            {
                var modifiedExtensionMethodBaseNameSelections = extensionMethodBaseNameSelections
                    .Where(x => x.ExtensionMethodBaseIdentity != extensionMethodBaseIdentity)
                    ;

                await this.SaveExtensionMethodBaseNameSelections(modifiedExtensionMethodBaseNameSelections);
            }

            return wasFound;
        }

        public async Task<bool> DeleteIgnoredExtensionMethodBaseNamespacedTypeName(string extensionMethodBaseNamespacedTypeName)
        {
            var ignoredExtensionMethodBaseNamespacedTypeNames = await this.LoadIgnoredExtensionMethodBaseNamespacedTypeNames();
            
            var wasFound = FileBasedExtensionMethodBaseRepository.HasIgnoredExtensionMethodBaseNamespacedTypeName(
                ignoredExtensionMethodBaseNamespacedTypeNames,
                extensionMethodBaseNamespacedTypeName);
            if (wasFound)
            {
                var modifiedIgnoredExtensionMethodBaseNamespaceTypeNames = ignoredExtensionMethodBaseNamespacedTypeNames
                    .Where(x => x != extensionMethodBaseNamespacedTypeName)
                    ;

                await this.SaveIgnoredExtensionMethodBaseNamespacedTypeNames(modifiedIgnoredExtensionMethodBaseNamespaceTypeNames);
            }

            return wasFound;
        }

        public async Task<bool> DeleteToProjectMapping(Guid extensionMethodBaseIdentity)
        {
            var toProjectMappings = await this.LoadToProjectMappings();

            var wasFound = FileBasedExtensionMethodBaseRepository.HasToProjectMappingByExtensionMethodBase(
                toProjectMappings,
                extensionMethodBaseIdentity);
            if (wasFound)
            {
                var modifiedToProjectMappings = toProjectMappings
                    .Where(x => x.ExtensionMethodBaseIdentity != extensionMethodBaseIdentity)
                    ;

                await this.SaveToProjectMappings(modifiedToProjectMappings);
            }

            return wasFound;
        }

        public async Task<ExtensionMethodBaseNameSelection[]> GetAllDuplicateExtensionMethodBaseNameSelections()
        {
            var output = await this.LoadDuplicateExtensionMethodBaseNameSelections();
            return output;
        }

        public async Task<string[]> GetAllDistinctExtensionMethodBaseCodeFilePaths()
        {
            var extensionMethodBases = await this.LoadExtensionMethodBases();

            var output = extensionMethodBases
                .Select(xExtensionMethodBase => xExtensionMethodBase.CodeFilePath)
                .Distinct()
                .ToArray();

            return output;
        }

        public async Task<ExtensionMethodBaseNameSelection[]> GetAllExtensionMethodBaseNameSelections()
        {
            var output = await this.LoadExtensionMethodBaseNameSelections();
            return output;
        }

        public async Task<ExtensionMethodBase[]> GetAllExtensionMethodBases()
        {
            var output = await this.LoadExtensionMethodBases();
            return output;
        }

        public async Task<string[]> GetAllIgnoredExtensionMethodBaseNamespacedTypeNames()
        {
            var output = await this.LoadIgnoredExtensionMethodBaseNamespacedTypeNames();
            return output;
        }

        public async Task<ExtensionMethodBaseToProjectMapping[]> GetAllToProjectMappings()
        {
            var output = await this.LoadToProjectMappings();
            return output;
        }

        public async Task<WasFound<ExtensionMethodBaseNameSelection>> HasDuplicateExtensionMethodBaseNameSelection(ExtensionMethodBaseNameSelection extensionMethodBaseNameSelection)
        {
            var extensionMethodBaseNameSelections = await this.LoadExtensionMethodBaseNameSelections();

            var output = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBaseNameSelection(extensionMethodBaseNameSelections, extensionMethodBaseNameSelection);
            return output;
        }

        public async Task<WasFound<ExtensionMethodBaseNameSelection>> HasDuplicateExtensionMethodBaseNameSelection(string extensionMethodBaseNamespacedTypeName)
        {
            var duplicateExtensionMethodBaseNameSelections = await this.LoadDuplicateExtensionMethodBaseNameSelections();

            var output = FileBasedExtensionMethodBaseRepository.HasDuplicateExtensionMethodBaseNameSelection(duplicateExtensionMethodBaseNameSelections, extensionMethodBaseNamespacedTypeName);
            return output;
        }

        public async Task<WasFound<ExtensionMethodBaseNameSelection>> HasDuplicateExtensionMethodBaseNameSelection(Guid extensionMethodBaseIdentity)
        {
            var duplicateExtensionMethodBaseNameSelections = await this.LoadDuplicateExtensionMethodBaseNameSelections();

            var output = FileBasedExtensionMethodBaseRepository.HasDuplicateExtensionMethodBaseNameSelection(duplicateExtensionMethodBaseNameSelections, extensionMethodBaseIdentity);
            return output;
        }

        public async Task<WasFound<ExtensionMethodBase>> HasExtensionMethodBase(ExtensionMethodBase extensionMethodBase)
        {
            var extensionMethodBases = await this.LoadExtensionMethodBases();

            var output = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBase(extensionMethodBases, extensionMethodBase);
            return output;
        }

        public async Task<WasFound<ExtensionMethodBase>> HasExtensionMethodBase(Guid identity)
        {
            var extensionMethodBases = await this.LoadExtensionMethodBases();

            var output = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBase(extensionMethodBases, identity);
            return output;
        }

        public async Task<WasFound<ExtensionMethodBase>> HasExtensionMethodBase(string namespacedTypeName, string codeFilePath)
        {
            var extensionMethodBases = await this.LoadExtensionMethodBases();

            var output =  FileBasedExtensionMethodBaseRepository.HasExtensionMethodBase(extensionMethodBases, namespacedTypeName, codeFilePath);
            return output;
        }

        public async Task<WasFound<ExtensionMethodBaseNameSelection>> HasExtensionMethodBaseNameSelection(ExtensionMethodBaseNameSelection extensionMethodBaseNameSelection)
        {
            var extensionMethodBaseNameSelections = await this.LoadExtensionMethodBaseNameSelections();

            var output = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBaseNameSelection(extensionMethodBaseNameSelections, extensionMethodBaseNameSelection);
            return output;
        }            

        public async Task<WasFound<ExtensionMethodBaseNameSelection>> HasExtensionMethodBaseNameSelection(string extensionMethodBaseNamespacedTypeName)
        {
            var extensionMethodBaseNameSelections = await this.LoadExtensionMethodBaseNameSelections();

            var output = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBaseNameSelection(extensionMethodBaseNameSelections, extensionMethodBaseNamespacedTypeName);
            return output;
        }

        public async Task<WasFound<ExtensionMethodBaseNameSelection>> HasExtensionMethodBaseNameSelection(Guid extensionMethodBaseIdentity)
        {
            var extensionMethodBaseNameSelections = await this.LoadExtensionMethodBaseNameSelections();

            var output = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBaseNameSelection(extensionMethodBaseNameSelections, extensionMethodBaseIdentity);
            return output;
        }

        public async Task<ExtensionMethodBase[]> HasExtensionMethodBasesByCodeFilePath(string codeFilePath)
        {
            var extensionMethodBases = await this.LoadExtensionMethodBases();

            var output = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBasesByCodeFilePath(extensionMethodBases, codeFilePath);
            return output;
        }

        public async Task<ExtensionMethodBase[]> HasExtensionMethodBasesByNamespacedTypeName(string namespacedTypeName)
        {
            var extensionMethodBases = await this.LoadExtensionMethodBases();

            var output = FileBasedExtensionMethodBaseRepository.HasExtensionMethodBasesByNamespacedTypeName(extensionMethodBases, namespacedTypeName);
            return output;
        }

        public async Task<WasFound<string>> HasIgnoredExtensionMethodBaseNamespacedTypeName(string extensionMethodBaseNamespacedTypeName)
        {
            var ignoredExtensionMethodBaseNamespacedTypeNames = await this.LoadIgnoredExtensionMethodBaseNamespacedTypeNames();

            var output = FileBasedExtensionMethodBaseRepository.HasIgnoredExtensionMethodBaseNamespacedTypeName(ignoredExtensionMethodBaseNamespacedTypeNames, extensionMethodBaseNamespacedTypeName);
            return output;
        }

        public async Task<WasFound<ExtensionMethodBaseToProjectMapping>> HasToProjectMapping(ExtensionMethodBaseToProjectMapping mapping)
        {
            var output = await this.HasToProjectMapping(
                mapping.ExtensionMethodBaseIdentity,
                mapping.ProjectIdentity);

            return output;
        }

        public async Task<WasFound<ExtensionMethodBaseToProjectMapping>> HasToProjectMapping(Guid extensionMethodBaseIdentity, Guid projectIdentity)
        {
            var toProjectMappings = await this.LoadToProjectMappings();

            var output = FileBasedExtensionMethodBaseRepository.HasToProjectMapping(toProjectMappings, extensionMethodBaseIdentity, projectIdentity);
            return output;
        }

        public async Task<WasFound<ExtensionMethodBaseToProjectMapping>> HasToProjectMappingByExtensionMethodBase(Guid extensionMethodBaseIdentity)
        {
            var toProjectMappings = await this.LoadToProjectMappings();

            var output = FileBasedExtensionMethodBaseRepository.HasToProjectMappingByExtensionMethodBase(toProjectMappings, extensionMethodBaseIdentity);
            return output;
        }

        public async Task<ExtensionMethodBaseToProjectMapping[]> HasToProjectMappingsByProject(Guid projectIdentity)
        {
            var toProjectMappings = await this.LoadToProjectMappings();

            var output = FileBasedExtensionMethodBaseRepository.HasToProjectMappingsByProject(toProjectMappings, projectIdentity);
            return output;
        }

        public async Task UpdateDuplicateExtensionMethodBaseNameSelection(string extensionMethodBaseNamespacedTypeName, Guid newExtensionMethodBaseIdentity)
        {
            var wasFound = await this.HasDuplicateExtensionMethodBaseNameSelection(extensionMethodBaseNamespacedTypeName);
            if (!wasFound)
            {
                throw new Exception("Duplicate extension method base selection did not exist.");
            }

            wasFound.Result.ExtensionMethodBaseIdentity = newExtensionMethodBaseIdentity;
        }

        public async Task UpdateExtensionMethodBaseNameSelection(string extensionMethodBaseNamespacedTypeName, Guid newExtensionMethodBaseIdentity)
        {
            var wasFound = await this.HasExtensionMethodBaseNameSelection(extensionMethodBaseNamespacedTypeName);
            if (!wasFound)
            {
                throw new Exception("Extension method base selection did not exist.");
            }

            wasFound.Result.ExtensionMethodBaseIdentity = newExtensionMethodBaseIdentity;
        }

        public async Task UpdateExtensionMethodBaseNameSelection(Guid extensionMethodBaseIdentity, string newExtensionMethodBaseNamespacedTypeName)
        {
            var wasFound = await this.HasExtensionMethodBaseNameSelection(extensionMethodBaseIdentity);
            if (!wasFound)
            {
                throw new Exception("Extension method base selection did not exist.");
            }

            wasFound.Result.ExtensionMethodBaseNamespacedTypeName = newExtensionMethodBaseNamespacedTypeName;
        }

        public async Task ClearAllExtensionMethodBaseNameSelections()
        {
            await this.SaveExtensionMethodBaseNameSelections(EnumerableHelper.Empty<ExtensionMethodBaseNameSelection>());
        }

        public async Task ClearAllToProjectMappings()
        {
            await this.SaveToProjectMappings(EnumerableHelper.Empty<ExtensionMethodBaseToProjectMapping>());
        }

        public async Task ClearAllExtensionMethodBases()
        {
            await this.SaveExtensionMethodBases(EnumerableHelper.Empty<ExtensionMethodBase>());
        }

        public async Task ClearAllIgnoredExtensionMethodBaseNamespacedTypeNames()
        {
            await this.SaveIgnoredExtensionMethodBaseNamespacedTypeNames(EnumerableHelper.Empty<string>());
        }

        public async Task ClearAllDuplicateExtensionMethodBaseNameSelections()
        {
            await this.SaveDuplicateExtensionMethodBaseNameSelections(EnumerableHelper.Empty<ExtensionMethodBaseNameSelection>());
        }

        public async Task<Dictionary<Guid, bool>> DeleteExtensionMethodBases(IEnumerable<Guid> extensionMethodBaseIdentities)
        {
            var extensionMethodBases = await this.LoadExtensionMethodBases();

            var extensionMethodBaseIdentitiesHash = new HashSet<Guid>(extensionMethodBaseIdentities);

            var output = new Dictionary<Guid, bool>();

            var modifiedExtensionMethodBases = extensionMethodBases
                .Where(xExtensionMethodBase =>
                {
                    var identity = xExtensionMethodBase.Identity;

                    var removeExtensionMethodBase = extensionMethodBaseIdentitiesHash.Contains(identity);
                    if (removeExtensionMethodBase)
                    {
                        output.Add(identity, true);
                    }
                    else
                    {
                        output.Add(identity, false);
                    }

                    var keepExtensionMethodBase = !removeExtensionMethodBase;
                    return keepExtensionMethodBase;
                })
                ;

            await this.SaveExtensionMethodBases(modifiedExtensionMethodBases);

            return output;
        }

        public async Task<Dictionary<string, bool>> DeleteExtensionMethodBaseNameSelections(IEnumerable<string> extensionMethodBaseNamespacedTypeNames)
        {
            var extensionMethodBaseNameSelections = await this.LoadExtensionMethodBaseNameSelections();

            var extensionMethodBaseNamesHash = new HashSet<string>(extensionMethodBaseNamespacedTypeNames);

            var output = new Dictionary<string, bool>();

            var modifiedExtensionMethodBaseNameSelections = extensionMethodBaseNameSelections
                .Where(xExtensionMethodBaseNameSelection =>
                {
                    var name = xExtensionMethodBaseNameSelection.ExtensionMethodBaseNamespacedTypeName;

                    var removeExtensionMethodBaseNameSelection = extensionMethodBaseNamesHash.Contains(name);
                    if (removeExtensionMethodBaseNameSelection)
                    {
                        output.Add(name, true);
                    }
                    else
                    {
                        output.Add(name, false);
                    }

                    var keepExtensionMethodBaseNameSelection = !removeExtensionMethodBaseNameSelection;
                    return keepExtensionMethodBaseNameSelection;
                });

            await this.SaveExtensionMethodBaseNameSelections(modifiedExtensionMethodBaseNameSelections);

            return output;
        }

        public async Task<Dictionary<Guid, bool>> DeleteToProjectMappings(IEnumerable<Guid> extensionMethodBaseIdentities)
        {
            var toProjectMappings = await this.LoadToProjectMappings();

            var output = new Dictionary<Guid, bool>();

            var modifiedToProjectMappings = toProjectMappings
                .ExceptWhere(Instances.Predicate.LocalIdentityIs<ExtensionMethodBaseToProjectMapping>(
                    extensionMethodBaseIdentities,
                    output));

            await this.SaveToProjectMappings(modifiedToProjectMappings);

            return output;
        }
    }
}