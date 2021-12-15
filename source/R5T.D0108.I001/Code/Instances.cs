using System;

using R5T.T0044;
using R5T.T0055;
using R5T.T0057;
using R5T.T0060;
using R5T.T0098;


namespace R5T.D0108.I001
{
    public static class Instances
    {
        public static IDuplicateValuesOperator DuplicateValuesOperator { get; } = T0057.DuplicateValuesOperator.Instance;
        public static IFileSystemOperator FileSystemOperator { get; set; } = T0044.FileSystemOperator.Instance;
        public static IGuidOperator GuidOperator { get; set; } = T0055.GuidOperator.Instance;
        public static IIgnoredValuesOperator IgnoredValuesOperator { get; } = T0057.IgnoredValuesOperator.Instance;
        public static IPredicate Predicate { get; } = T0060.Predicate.Instance;
        public static IOperation Operation { get; } = T0098.Operation.Instance;
    }
}
