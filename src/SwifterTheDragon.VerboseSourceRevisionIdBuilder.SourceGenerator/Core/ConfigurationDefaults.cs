// Copyright SwifterTheDragon, 2025. All Rights Reserved.

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core
{
    /// <summary>
    /// The default values for configuration.
    /// </summary>
    internal static class ConfigurationDefaults
    {
        #region Fields & Properties
        /// <summary>
        /// The default semantic version major version configuration value.
        /// </summary>
        internal static int DefaultSemanticVersionMajorVersion
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// The default semantic version minor version configuration value.
        /// </summary>
        internal static int DefaultSemanticVersionMinorVersion
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// The default semantic version patch version configuration value.
        /// </summary>
        internal static int DefaultSemanticVersionPatchVersion
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// The default semantic version prefix configuration value.
        /// </summary>
        internal static string DefaultSemanticVersionPrefix
        {
            get
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// The default semantic version suffix configuration value.
        /// </summary>
        internal static string DefaultSemanticVersionSuffix
        {
            get
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// The default generated file name configuration value.
        /// </summary>
        internal static string DefaultGeneratedFileName
        {
            get
            {
                return "VerboseSourceRevisionIdBuilder.Generated.cs";
            }
        }
        /// <summary>
        /// The default generated namespace configuration value.
        /// </summary>
        internal static string DefaultGeneratedNamespace
        {
            get
            {
                return "SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core";
            }
        }
        /// <summary>
        /// The default generated type name configuration value.
        /// </summary>
        internal static string DefaultGeneratedTypeName
        {
            get
            {
                return "VerboseSourceRevisionIdBuilder";
            }
        }
        /// <summary>
        /// The default generated field name configuration value.
        /// </summary>
        internal static string DefaultGeneratedFieldName
        {
            get
            {
                return "VerboseSourceRevisionId";
            }
        }
        /// <summary>
        /// The default dirty mark configuration value for labelling a working
        /// tree with local modification.
        /// </summary>
        internal static string DefaultDirtyMark
        {
            get
            {
                return "-dirty";
            }
        }
        /// <summary>
        /// The default broken mark configuration value for labelling a corrupt
        /// repository.
        /// </summary>
        internal static string DefaultBrokenMark
        {
            get
            {
                return "-broken";
            }
        }
        /// <summary>
        /// The default detached <c>HEAD</c> label
        /// configuration value for anonymous
        /// branches.
        /// </summary>
        internal static string DefaultDetachedHeadLabel
        {
            get
            {
                return "DETACHED-HEAD";
            }
        }
        /// <summary>
        /// The default invalid <c>HEAD</c> label configuration value for unborn
        /// branches.
        /// </summary>
        internal static string DefaultInvalidHeadLabel
        {
            get
            {
                return "INVALID-HEAD";
            }
        }
        /// <summary>
        /// The default default Git branch name configuration value.
        /// </summary>
        internal static string DefaultDefaultGitBranchName
        {
            get
            {
                return "main";
            }
        }
        /// <summary>
        /// The default repository root directory relative
        /// to configuration file path.
        /// </summary>
        internal static string DefaultRepositoryRootDirectoryRelativeToConfigurationFilePath
        {
            get
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// The default Git reference type to be described.
        /// </summary>
        internal static GitReferenceType DefaultGitReferenceType
        {
            get
            {
                return GitReferenceType.All;
            }
        }
        /// <summary>
        /// The default amount of most recent tags to describe HEAD with.
        /// </summary>
        internal static int DefaultCandidateAmount
        {
            get
            {
                return 10;
            }
        }
        /// <summary>
        /// The default amount of hexadecimal digits to describe the abbreviated
        /// object name with.
        /// </summary>
        internal static string DefaultAbbrevLength
        {
            get
            {
                return "Default";
            }
        }
        /// <summary>
        /// The default value determining if only the first parent commit
        /// of a merge commit should be followed or not.
        /// </summary>
        internal static bool DefaultFirstParentOnly
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// The default state of if references containing <c>HEAD</c> should
        /// be used instead of references predating <c>HEAD</c>.
        /// </summary>
        internal static bool DefaultContains
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// The default match patterns for filtering references with.
        /// </summary>
        internal static ReadOnlyCollection<string> DefaultMatchPatterns
        {
            get
            {
                return new ReadOnlyCollection<string>(
                    list: new List<string>());
            }
        }
        /// <summary>
        /// The default exclude patterns for filtering references with.
        /// </summary>
        internal static ReadOnlyCollection<string> DefaultExcludePatterns
        {
            get
            {
                return new ReadOnlyCollection<string>(
                    list: new List<string>());
            }
        }
        #endregion Fields & Properties
    }
}
