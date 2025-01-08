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
        internal static int SemanticVersionMajorVersion
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// The default semantic version minor version configuration value.
        /// </summary>
        internal static int SemanticVersionMinorVersion
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// The default semantic version patch version configuration value.
        /// </summary>
        internal static int SemanticVersionPatchVersion
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// The default semantic version prefix configuration value.
        /// </summary>
        internal static string SemanticVersionPrefix
        {
            get
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// The default semantic version suffix configuration value.
        /// </summary>
        internal static string SemanticVersionSuffix
        {
            get
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// The default generated file name configuration value.
        /// </summary>
        internal static string GeneratedFileName
        {
            get
            {
                return "VerboseSourceRevisionIdBuilder.Generated.cs";
            }
        }
        /// <summary>
        /// The default generated namespace configuration value.
        /// </summary>
        internal static string GeneratedNamespace
        {
            get
            {
                return "SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core";
            }
        }
        /// <summary>
        /// The default generated type name configuration value.
        /// </summary>
        internal static string GeneratedTypeName
        {
            get
            {
                return "VerboseSourceRevisionIdBuilder";
            }
        }
        /// <summary>
        /// The default generated field name configuration value.
        /// </summary>
        internal static string GeneratedFieldName
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
        internal static string DirtyMark
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
        internal static string BrokenMark
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
        internal static string DetachedHeadLabel
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
        internal static string InvalidHeadLabel
        {
            get
            {
                return "INVALID-HEAD";
            }
        }
        /// <summary>
        /// The default, default Git branch name configuration value.
        /// </summary>
        internal static string DefaultGitBranchName
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
        internal static string RepositoryRootDirectoryRelativeToConfigurationFilePath
        {
            get
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// The default Git reference type to be described.
        /// </summary>
        internal static GitReferenceType GitReferenceType
        {
            get
            {
                return GitReferenceType.All;
            }
        }
        /// <summary>
        /// The default amount of most recent tags to describe <c>HEAD</c> with.
        /// </summary>
        internal static int CandidateAmount
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
        internal static string AbbrevLength
        {
            get
            {
                return "Default";
            }
        }
        /// <summary>
        /// The default parent commit type for following merge commits.
        /// </summary>
        internal static ParentCommitType ParentCommitType
        {
            get
            {
                return ParentCommitType.Any;
            }
        }
        /// <summary>
        /// The default state of if tags containing <c>HEAD</c> should
        /// be used instead of tags predating <c>HEAD</c>.
        /// </summary>
        internal static GitTagState GitTagState
        {
            get
            {
                return GitTagState.PredatesCommit;
            }
        }
        /// <summary>
        /// The default match patterns for filtering references with.
        /// </summary>
        internal static ReadOnlyCollection<string> MatchPatterns
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
        internal static ReadOnlyCollection<string> ExcludePatterns
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
