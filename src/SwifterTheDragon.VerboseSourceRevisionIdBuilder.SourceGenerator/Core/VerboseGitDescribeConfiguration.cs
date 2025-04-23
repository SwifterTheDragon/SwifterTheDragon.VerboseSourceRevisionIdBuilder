// Copyright SwifterTheDragon and the SwifterTheDragon.VerboseSourceRevisionIdBuilder contributors, 2024-2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

using System.Collections.ObjectModel;

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core
{
    /// <summary>
    /// Configuration data for verbose Git describe commands.
    /// </summary>
    internal sealed class VerboseGitDescribeConfiguration
    {
        #region Fields & Properties
        /// <summary>
        /// The backing store for <c><see cref="DirtyMark"/></c>.
        /// </summary>
        private string i_dirtyMark;
        /// <summary>
        /// The value for the <c>--dirty[=&lt;mark&gt;]</c> argument labelling a
        /// working tree with local modification.
        /// </summary>
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
        /// <summary>
        /// The backing store for <c><see cref="BrokenMark"/></c>.
        /// </summary>
        private string i_brokenMark;
        /// <summary>
        /// The value for the <c>--broken[=&lt;mark&gt;]</c> argument labelling a
        /// corrupt repository.
        /// </summary>
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
        /// <summary>
        /// The backing store for <c><see cref="InvalidHeadLabel"/></c>.
        /// </summary>
        private string i_invalidHeadLabel;
        /// <summary>
        /// A label for an invalid <c>HEAD</c> state, such as unborn branches.
        /// </summary>
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
        /// <summary>
        /// The backing store for <c><see cref="GitReferenceType"/></c>.
        /// </summary>
        private GitReferenceType i_gitReferenceType;
        /// <summary>
        /// The value for the arguments that specify which reference types
        /// are allowed.
        /// </summary>
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
        /// <summary>
        /// The backing store for <c><see cref="CandidateAmount"/></c>.
        /// </summary>
        private int i_candidateAmount;
        /// <summary>
        /// The value for the <c>--candidates=&lt;n&gt;</c> argument, specifying
        /// the amount of most recent tags to describe <c>HEAD</c> with.
        /// </summary>
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
        /// <summary>
        /// The backing store for <c><see cref="AbbrevLength"/></c>.
        /// </summary>
        private string i_abbrevLength;
        /// <summary>
        /// The value for the <c>--abbrev=&lt;n&gt;</c> argument, specifying
        /// how many hexadecimal digits of the abbreviated object name to use,
        /// suppressing <c>--long</c> if <c>0</c>.
        /// </summary>
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
        /// <summary>
        /// The backing store for <c><see cref="ParentCommitType"/></c>.
        /// </summary>
        private ParentCommitType i_parentCommitType;
        /// <summary>
        /// Specifies if the <c>--first-parent</c> argument should be included
        /// or omitted, controlling if only the first parent of a merge commit
        /// should be followed.
        /// </summary>
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
        /// <summary>
        /// The backing store for <c><see cref="MatchPatterns"/></c>.
        /// </summary>
        private ReadOnlyCollection<string> i_matchPatterns;
        /// <summary>
        /// A collection of patterns for the <c>--match &lt;pattern&gt;</c>
        /// argument, specifying glob patterns that references must match.
        /// </summary>
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
        /// <summary>
        /// The backing store for <c><see cref="ExcludePatterns"/></c>.
        /// </summary>
        private ReadOnlyCollection<string> i_excludePatterns;
        /// <summary>
        /// A collection of patterns for the <c>--exclude &lt;pattern&gt;</c>
        /// argument, specifying glob patterns that references must not match.
        /// </summary>
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
        /// <summary>
        /// The backing store for <c><see cref="GitTagState"/></c>.
        /// </summary>
        private GitTagState i_gitTagState;
        /// <summary>
        /// Specifies if the <c>--contains</c> argument should be included or
        /// omitted, controlling if tags containing <c>HEAD</c> should be used,
        /// or tags predating <c>HEAD</c>.
        /// </summary>
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
        /// <summary>
        /// The backing store for <c><see cref="GitRepositoryRootDirectoryPath"/></c>.
        /// </summary>
        private string i_gitRepositoryRootDirectoryPath;
        /// <summary>
        /// The absolute file path to the Git repository root directory.
        /// </summary>
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
        /// <summary>
        /// The default constructor, present solely to reduce accessibility.
        /// </summary>
        internal VerboseGitDescribeConfiguration()
        {
            // Do nothing.
        }
        #endregion Methods
    }
}
