// Copyright SwifterTheDragon, and the SwifterTheDragon.VerboseSourceRevisionIdBuilder contributors, 2024-2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

using System.Collections.ObjectModel;

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core
{
    /// <include
    /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
    /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Description/*'/>
    internal sealed class VerboseGitDescribeConfiguration
    {
        #region Fields & Properties
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Field[@name="i_dirtyMark"]/*'/>
        private string i_dirtyMark;
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Property[@name="DirtyMark"]/*'/>
        internal string DirtyMark
        {
            get
            {
                return i_dirtyMark;
            }
            set
            {
                i_dirtyMark = value;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Field[@name="i_brokenMark"]/*'/>
        private string i_brokenMark;
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Property[@name="BrokenMark"]/*'/>
        internal string BrokenMark
        {
            get
            {
                return i_brokenMark;
            }
            set
            {
                i_brokenMark = value;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Field[@name="i_invalidHeadLabel"]/*'/>
        private string i_invalidHeadLabel;
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Property[@name="InvalidHeadLabel"]/*'/>
        internal string InvalidHeadLabel
        {
            get
            {
                return i_invalidHeadLabel;
            }
            set
            {
                i_invalidHeadLabel = value;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Field[@name="i_gitReferenceType"]/*'/>
        private GitReferenceType i_gitReferenceType;
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Property[@name="GitReferenceType"]/*'/>
        internal GitReferenceType GitReferenceType
        {
            get
            {
                return i_gitReferenceType;
            }
            set
            {
                i_gitReferenceType = value;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Field[@name="i_candidateAmount"]/*'/>
        private int i_candidateAmount;
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Property[@name="CandidateAmount"]/*'/>
        internal int CandidateAmount
        {
            get
            {
                return i_candidateAmount;
            }
            set
            {
                i_candidateAmount = value;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Field[@name="i_abbrevLength"]/*'/>
        private string i_abbrevLength;
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Property[@name="AbbrevLength"]/*'/>
        internal string AbbrevLength
        {
            get
            {
                return i_abbrevLength;
            }
            set
            {
                i_abbrevLength = value;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Field[@name="i_parentCommitType"]/*'/>
        private ParentCommitType i_parentCommitType;
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Property[@name="ParentCommitType"]/*'/>
        internal ParentCommitType ParentCommitType
        {
            get
            {
                return i_parentCommitType;
            }
            set
            {
                i_parentCommitType = value;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Field[@name="i_matchPatterns"]/*'/>
        private ReadOnlyCollection<string> i_matchPatterns;
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Property[@name="MatchPatterns"]/*'/>
        internal ReadOnlyCollection<string> MatchPatterns
        {
            get
            {
                return i_matchPatterns;
            }
            set
            {
                i_matchPatterns = value;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Field[@name="i_excludePatterns"]/*'/>
        private ReadOnlyCollection<string> i_excludePatterns;
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Property[@name="ExcludePatterns"]/*'/>
        internal ReadOnlyCollection<string> ExcludePatterns
        {
            get
            {
                return i_excludePatterns;
            }
            set
            {
                i_excludePatterns = value;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Field[@name="i_gitTagState"]/*'/>
        private GitTagState i_gitTagState;
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Property[@name="GitTagState"]/*'/>
        internal GitTagState GitTagState
        {
            get
            {
                return i_gitTagState;
            }
            set
            {
                i_gitTagState = value;
            }
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Field[@name="i_gitRepositoryRootDirectoryPath"]/*'/>
        private string i_gitRepositoryRootDirectoryPath;
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Property[@name="GitRepositoryRootDirectoryPath"]/*'/>
        internal string GitRepositoryRootDirectoryPath
        {
            get
            {
                return i_gitRepositoryRootDirectoryPath;
            }
            set
            {
                i_gitRepositoryRootDirectoryPath = value;
            }
        }
        #endregion Fields & Properties
        #region Methods
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseGitDescribeConfiguration"]/Method[@name="#ctor"]/*'/>
        internal VerboseGitDescribeConfiguration()
        {
            // Do nothing.
        }
        #endregion Methods
    }
}
