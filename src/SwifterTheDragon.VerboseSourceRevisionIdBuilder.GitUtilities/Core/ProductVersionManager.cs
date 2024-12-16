// Copyright SwifterTheDragon, 2024. All Rights Reserved.

using System.Text;

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.GitUtilities.Core
{
    /// <summary>
    /// Puts together the current product version.
    /// </summary>
    internal class ProductVersionManager
    {
        #region Fields & Properties
        /// <summary>
        /// The default name of the default git branch.
        /// </summary>
        private static string DefaultDefaultGitBranchName
        {
            get
            {
                return "main";
            }
        }
        #endregion Fields & Properties
        #region Methods
        /// <summary>
        /// Retrieves the current product version.
        /// </summary>
        /// <returns><c>MAJOR.MINOR.PATCH+<see cref="GitHelper.GetVerboseGitDescribe"/></c> if the current branch name matches <see cref="DefaultDefaultGitBranchName"/>.
        /// Otherwise, <c>MAJOR.MINOR.PATCH-<see cref="GitHelper.GetCurrentGitBranchName"/></c>.</returns>
        /// <example>
        /// <para>
        /// Default branch: <c>1.2.3+v1.2.3-4-g4fc0dc5</c>.
        /// </para>
        /// <para>
        /// Non-default branch: <c>1.2.3-beta+v1.2.3-beta-5-g6bc019c</c>.
        /// </para>
        /// <para>
        /// Anonymous branch AKA detached HEAD: <c>1.2.3-DETACHED-HEAD+heads-main-0-g5d6a83b</c>.
        /// </para>
        /// <para>
        /// Unborn branch AKA invalid HEAD: <c>1.2.3-DETACHED-HEAD+INVALID-HEAD</c>.
        /// </para>
        /// </example>
        internal static string GetProductVersion()
        {
            var productVersionBuilder = new StringBuilder();
            string semanticVersionPlaceholder = "1.2.3";
            _ = productVersionBuilder.Append(
                value: semanticVersionPlaceholder);
            string currentBranchName = GitHelper.GetCurrentGitBranchName();
            string verboseGitDescribe = GitHelper.GetVerboseGitDescribe()
                // Git check-ref-format accepts forward slashes, but Semantic Versioning 2.0.0 does not.
                .Replace(
                oldChar: '/',
                newChar: '-');
            if (!currentBranchName.Equals(
                value: DefaultDefaultGitBranchName,
                comparisonType: System.StringComparison.Ordinal))
            {
                _ = productVersionBuilder.Append(
                    value: '-')
                    .Append(
                    value: currentBranchName);
            }
            _ = productVersionBuilder.Append(
                value: verboseGitDescribe);
            return productVersionBuilder.ToString();
        }
        #endregion Methods
    }
}
