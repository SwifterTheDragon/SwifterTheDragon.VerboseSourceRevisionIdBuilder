// Copyright SwifterTheDragon, and the SwifterTheDragon.VerboseSourceRevisionIdBuilder contributors, 2024-2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core
{
    /// <include
    /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
    /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Description/*'/>
    internal static class ConfigurationDefaults
    {
        #region Fields & Properties
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="SemanticVersionMajorVersion"]/*'/>
        internal static int SemanticVersionMajorVersion
        {
            get
            {
                return 0;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="SemanticVersionMinorVersion"]/*'/>
        internal static int SemanticVersionMinorVersion
        {
            get
            {
                return 1;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="SemanticVersionPatchVersion"]/*'/>
        internal static int SemanticVersionPatchVersion
        {
            get
            {
                return 0;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="SemanticVersionPrefix"]/*'/>
        internal static string SemanticVersionPrefix
        {
            get
            {
                return string.Empty;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="SemanticVersionSuffix"]/*'/>
        internal static string SemanticVersionSuffix
        {
            get
            {
                return string.Empty;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="GeneratedFileName"]/*'/>
        internal static string GeneratedFileName
        {
            get
            {
                return "VerboseSourceRevisionIdBuilder.Generated.cs";
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="GeneratedNamespace"]/*'/>
        internal static string GeneratedNamespace
        {
            get
            {
                return "SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core";
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="GeneratedTypeName"]/*'/>
        internal static string GeneratedTypeName
        {
            get
            {
                return "VerboseSourceRevisionIdBuilder";
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="GeneratedFieldName"]/*'/>
        internal static string GeneratedFieldName
        {
            get
            {
                return "VerboseSourceRevisionId";
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="DirtyMark"]/*'/>
        internal static string DirtyMark
        {
            get
            {
                return "-dirty";
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="BrokenMark"]/*'/>
        internal static string BrokenMark
        {
            get
            {
                return "-broken";
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="DetachedHeadLabel"]/*'/>
        internal static string DetachedHeadLabel
        {
            get
            {
                return "DETACHED-HEAD";
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="InvalidHeadLabel"]/*'/>
        internal static string InvalidHeadLabel
        {
            get
            {
                return "INVALID-HEAD";
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="DefaultGitBranchName"]/*'/>
        internal static string DefaultGitBranchName
        {
            get
            {
                return "main";
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="RepositoryRootDirectoryRelativeToConfigurationFilePath"]/*'/>
        internal static string RepositoryRootDirectoryRelativeToConfigurationFilePath
        {
            get
            {
                return string.Empty;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="GitReferenceType"]/*'/>
        internal static GitReferenceType GitReferenceType
        {
            get
            {
                return GitReferenceType.All;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="CandidateAmount"]/*'/>
        internal static int CandidateAmount
        {
            get
            {
                return 10;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="AbbrevLength"]/*'/>
        internal static int AbbrevLength
        {
            get
            {
                return 40;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="ParentCommitType"]/*'/>
        internal static ParentCommitType ParentCommitType
        {
            get
            {
                return ParentCommitType.Any;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="GitTagState"]/*'/>
        internal static GitTagState GitTagState
        {
            get
            {
                return GitTagState.PredatesHead;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="MatchPatterns"]/*'/>
        internal static ReadOnlyCollection<string> MatchPatterns
        {
            get
            {
                return new ReadOnlyCollection<string>(
                    list: new List<string>());
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="ConfigurationDefaults"]/Property[@name="ExcludePatterns"]/*'/>
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
