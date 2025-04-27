// Copyright SwifterTheDragon, and the SwifterTheDragon.VerboseSourceRevisionIdBuilder contributors, 2024-2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core
{
    /// <summary>
    /// The keys to configuration values.
    /// </summary>
    internal static class ConfigurationKeys
    {
        #region Fields & Properties
        /// <summary>
        /// The key to the semantic version major version label configuration value.
        /// </summary>
        internal static string SemanticVersionMajorVersionLabel
        {
            get
            {
                return "MajorVersionLabel";
            }
        }
        /// <summary>
        /// The key to the semantic version minor version label configuration value.
        /// </summary>
        internal static string SemanticVersionMinorVersionLabel
        {
            get
            {
                return "MinorVersionLabel";
            }
        }
        /// <summary>
        /// The key to the semantic version patch version label configuration value.
        /// </summary>
        internal static string SemanticVersionPatchVersionLabel
        {
            get
            {
                return "PatchVersionLabel";
            }
        }
        /// <summary>
        /// The key to the generated file name configuration value.
        /// </summary>
        internal static string GeneratedFileName
        {
            get
            {
                return "GeneratedFileName";
            }
        }
        /// <summary>
        /// The key to the generated namespace configuration value.
        /// </summary>
        internal static string GeneratedNamespace
        {
            get
            {
                return "GeneratedNamespace";
            }
        }
        /// <summary>
        /// The key to the generated type name configuration value.
        /// </summary>
        internal static string GeneratedTypeName
        {
            get
            {
                return "GeneratedTypeName";
            }
        }
        /// <summary>
        /// The key to the generated field name value.
        /// </summary>
        internal static string GeneratedFieldName
        {
            get
            {
                return "GeneratedFieldName";
            }
        }
        /// <summary>
        /// The key to the semantic version prefix configuration value.
        /// </summary>
        internal static string SemanticVersionPrefix
        {
            get
            {
                return "Prefix";
            }
        }
        /// <summary>
        /// The key to the semantic version suffix configuration value.
        /// </summary>
        internal static string SemanticVersionSuffix
        {
            get
            {
                return "Suffix";
            }
        }
        /// <summary>
        /// The key to the dirty mark configuration value for labelling a
        /// working tree with local modification.
        /// </summary>
        internal static string DirtyMark
        {
            get
            {
                return "DirtyMark";
            }
        }
        /// <summary>
        /// The key to the broken mark configuration value for labelling a
        /// corrupt repository.
        /// </summary>
        internal static string BrokenMark
        {
            get
            {
                return "BrokenMark";
            }
        }
        /// <summary>
        /// The key to the detached <c>HEAD</c> label
        /// configuration value for anonymous branches.
        /// </summary>
        internal static string DetachedHeadLabel
        {
            get
            {
                return "DetachedHeadLabel";
            }
        }
        /// <summary>
        /// The key to the invalid <c>HEAD</c> label
        /// configuration value for unborn branches.
        /// </summary>
        internal static string InvalidHeadLabel
        {
            get
            {
                return "InvalidHeadLabel";
            }
        }
        /// <summary>
        /// The key to the default Git branch name configuration value.
        /// </summary>
        internal static string DefaultGitBranchName
        {
            get
            {
                return "DefaultGitBranchName";
            }
        }
        /// <summary>
        /// The key to the repository root directory relative to configuration
        /// file path configuration value.
        /// </summary>
        internal static string RepositoryRootDirectoryRelativeToConfigurationFilePath
        {
            get
            {
                return "RepositoryRootRelativeToConfigurationFilePath";
            }
        }
        /// <summary>
        /// The key to the Git reference type configuration value.
        /// </summary>
        internal static string GitReferenceType
        {
            get
            {
                return "GitReferenceType";
            }
        }
        /// <summary>
        /// The key to the candidate (most recent tag) amount configuration
        /// value.
        /// </summary>
        internal static string CandidateAmount
        {
            get
            {
                return "CandidateAmount";
            }
        }
        /// <summary>
        /// The key to the abbrev length (amount of hexadecimal digits to
        /// describe an abbreviated object name with)
        /// configuration value.
        /// </summary>
        internal static string AbbrevLength
        {
            get
            {
                return "AbbrevLength";
            }
        }
        /// <summary>
        /// The key to the parent commit type configuration value.
        /// </summary>
        internal static string ParentCommitType
        {
            get
            {
                return "ParentCommitType";
            }
        }
        /// <summary>
        /// The key to the Git tag state configuration value.
        /// </summary>
        internal static string GitTagState
        {
            get
            {
                return "GitTagState";
            }
        }
        /// <summary>
        /// The key to the match pattern configuration value.
        /// </summary>
        internal static string MatchPatterns
        {
            get
            {
                return "MatchPatterns";
            }
        }
        /// <summary>
        /// The key to the exclude pattern configuration value.
        /// </summary>
        internal static string ExcludePatterns
        {
            get
            {
                return "ExcludePatterns";
            }
        }
        #endregion Fields & Properties
    }
}
