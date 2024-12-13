// Copyright SwifterTheDragon, 2024. All Rights Reserved.

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.GitUtilities.Core
{
    /// <summary>
    /// Provides Git related utilities, such as fetching the current branch name.
    /// </summary>
    internal static class GitHelper
    {
        #region Fields & Properties
        /// <summary>
        /// A text value representing a detached <c>HEAD</c> state.
        /// </summary>
        private static string DefaultDetachedHeadLabel
        {
            get
            {
                return "DETACHED-HEAD";
            }
        }
        /// <summary>
        /// A text value representing an invalid <c>HEAD</c> state.
        /// </summary>
        private static string DefaultInvalidHeadLabel
        {
            get
            {
                return "INVALID-HEAD";
            }
        }
        #endregion Fields & Properties
        #region Methods
        /// <summary>
        /// Runs a verbose git describe command and returns the output.
        /// </summary>
        /// <returns>The output of <c>git describe --all --long --dirty=-dirty --broken=-broken</c> with trailing white space.
        /// If output is blank, then <c><see cref="GetGitDescribeFallback"/></c> is used instead.
        /// </returns>
        internal static string GetVerboseGitDescribe()
        {
            string verboseGitDescribe = CommandLineUtilities.ExecuteCommandLineCommand(
                command: "git describe --all --long --dirty=-dirty --broken=-broken");
            if (string.IsNullOrEmpty(
                value: verboseGitDescribe))
            {
                verboseGitDescribe = GetGitDescribeFallback();
            }
            return verboseGitDescribe;
        }
        /// <summary>
        /// Runs a fallback git describe command and returns the output.
        /// </summary>
        /// <returns>The output of <c>git describe --always</c> with trailing white space trimmed.
        /// If output is blank, then <c><see cref="DefaultInvalidHeadLabel"/></c> is used instead.
        /// </returns>
        internal static string GetGitDescribeFallback()
        {
            string gitDescribeFallback = CommandLineUtilities.ExecuteCommandLineCommand(
                command: "git describe --always");
            if (string.IsNullOrEmpty(
                value: gitDescribeFallback))
            {
                gitDescribeFallback = DefaultInvalidHeadLabel;
            }
            return gitDescribeFallback;
        }
        /// <summary>
        /// Runs a command to fetch the current git branch name and returns the output.
        /// </summary>
        /// <returns>
        /// The output of <c>git branch --show-current</c>.
        /// If output is blank, then <c><see cref="DefaultDetachedHeadLabel"/></c> is used instead.
        /// </returns>
        internal static string GetCurrentGitBranchName()
        {
            string currentBranchName = CommandLineUtilities.ExecuteCommandLineCommand(
                command: "git branch --show-current");
            if (string.IsNullOrEmpty(
                value: currentBranchName))
            {
                currentBranchName = DefaultDetachedHeadLabel;
            }
            return currentBranchName;
        }
        #endregion Methods
    }
}
