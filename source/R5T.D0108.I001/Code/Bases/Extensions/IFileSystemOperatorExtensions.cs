﻿using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using R5T.Magyar.IO;

using R5T.T0044;

using R5T.T0100;


namespace System
{
    public static class IFileSystemOperatorExtensions
    {
        public static ExtensionMethodBase[] LoadExtensionMethodBasesFromJsonFile(this IFileSystemOperator _,
            string jsonFilePath)
        {
            var output = JsonFileHelper.LoadFromFile<ExtensionMethodBase[]>(jsonFilePath);
            return output;
        }

        public static void WriteToJsonFile(this IFileSystemOperator _,
            string jsonFilePath,
            IEnumerable<ExtensionMethodBase> extensionMethodBases,
            bool overwrite = IOHelper.DefaultOverwriteValue)
        {
            JsonFileHelper.WriteToFile(jsonFilePath, extensionMethodBases, overwrite: overwrite);
        }

        public static ExtensionMethodBaseToProjectMapping[] LoadExtensionMethodBaseToProjectMappingsFromJsonFile(this IFileSystemOperator _,
            string jsonFilePath)
        {
            var output = JsonFileHelper.LoadFromFile<ExtensionMethodBaseToProjectMapping[]>(jsonFilePath);
            return output;
        }

        public static void WriteToJsonFile(this IFileSystemOperator _,
            string jsonFilePath,
            IEnumerable<ExtensionMethodBaseToProjectMapping> mappings,
            bool overwrite = IOHelper.DefaultOverwriteValue)
        {
            JsonFileHelper.WriteToFile(jsonFilePath, mappings, overwrite: overwrite);
        }
    }
}
