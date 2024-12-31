// Copyright SwifterTheDragon, 2024. All Rights Reserved.

using System.Collections.ObjectModel;
using System.Globalization;

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core
{
    /// <summary>
    /// Provides Git related utilities,
    /// such as fetching the current branch name.
    /// </summary>
    internal static class GitHelper
    {
        #region Fields & Properties
        /// <summary>
        /// The minimum amount of hexadecimal digits that a fresh repo uses
        /// to describe abbreviated object names with.
        /// </summary>
        private static int AbbrevMinimum
        {
            get
            {
                return 4;
            }
        }
        /// <summary>
        /// The maximum amount of hexadecimal digits that a
        /// SHA-1 hash can be represented with.
        /// </summary>
        private static int AbbrevMaximum
        {
            get
            {
                return 40;
            }
        }
        #endregion Fields & Properties
        #region Methods
        /// <summary>
        /// Runs a verbose git describe command and returns the output.
        /// </summary>
        /// <param name="dirtyMark">
        /// The label for a working tree with local modification.
        /// </param>
        /// <param name="brokenMark">
        /// The label for a corrupt repository.
        /// </param>
        /// <param name="invalidHeadLabel">
        /// The label for an invalid <c>HEAD</c> state.
        /// </param>
        /// <param name="gitReferenceType">
        /// The type of Git references to describe <c>HEAD</c> with.
        /// </param>
        /// <param name="candidateAmount">
        /// The amount of most recent tags to describe <c>HEAD</c> with.
        /// </param>
        /// <param name="abbrevLength">
        /// The amount of hexadecimal digits to describe the
        /// abbreviated object name with.
        /// Disables <c>--long</c> if <c>0</c>.
        /// </param>
        /// <param name="firstParentOnly">
        /// Determines if only the first parent commit of
        /// a merge commit should be followed or not.
        /// </param>
        /// <param name="matchPatterns">
        /// The list of match patterns for filtering references with.
        /// </param>
        /// <param name="excludePatterns">
        /// The list of exclude patterns for filtering references with.
        /// </param>
        /// <param name="contains">
        /// Determines if references containing <c>HEAD</c> should be used
        /// instead of references predating <c>HEAD</c>.
        /// </param>
        /// <param name="gitRepositoryRootDirectoryPath">
        /// The path to the root directory of the Git repository.
        /// </param>
        /// <returns>
        /// The output of a verbose Git describe command
        /// with trailing white space trimmed.
        /// If output is blank, then
        /// <c><see cref="GetGitDescribeFallback"/></c> is used instead.
        /// </returns>
        internal static string GetVerboseGitDescribe(
            string dirtyMark,
            string brokenMark,
            string invalidHeadLabel,
            GitReferenceType gitReferenceType,
            int candidateAmount,
            string abbrevLength,
            bool firstParentOnly,
            ReadOnlyCollection<string> matchPatterns,
            ReadOnlyCollection<string> excludePatterns,
            bool contains,
            string gitRepositoryRootDirectoryPath)
        {
            string command = "git describe";
            switch (gitReferenceType)
            {
                case GitReferenceType.Tags:
                    command += " --tags";
                    break;
                case GitReferenceType.All:
                    command += " --all";
                    break;
                case GitReferenceType.None:
                case GitReferenceType.AnnotatedTags:
                default:
                    break;
            }
            if (!string.IsNullOrWhiteSpace(
                value: dirtyMark))
            {
                string escapedDirtyMark = EscapeMark(
                    unescapedMark: dirtyMark);
                command += " --dirty="
                    + escapedDirtyMark;
            }
            if (!string.IsNullOrWhiteSpace(
                value: brokenMark))
            {
                string escapedBrokenMark = EscapeMark(
                    unescapedMark: brokenMark);
                command += " --broken="
                    + escapedBrokenMark;
            }
            if (!string.IsNullOrWhiteSpace(
                value: abbrevLength)
                    && int.TryParse(
                        s: abbrevLength,
                        style: NumberStyles.Integer,
                        provider: CultureInfo.InvariantCulture,
                        result: out int parsedAbbrevLength))
            {
                if (parsedAbbrevLength > 0)
                {
                    if (parsedAbbrevLength > AbbrevMaximum)
                    {
                        parsedAbbrevLength = AbbrevMaximum;
                    }
                    if (parsedAbbrevLength < AbbrevMinimum)
                    {
                        parsedAbbrevLength = AbbrevMinimum;
                    }
                    command += " --abbrev="
                        + parsedAbbrevLength.ToString(
                            provider: CultureInfo.InvariantCulture)
                        + " --long";
                }
            }
            else
            {
                command += " --long";
            }
            command += " --candidates="
                + candidateAmount;
            if (firstParentOnly)
            {
                command += " --first-parent";
            }
            command += AddPatterns(
                patternsToAdd: matchPatterns,
                patternArgument: "--match");
            command += AddPatterns(
                patternsToAdd: excludePatterns,
                patternArgument: "--exclude");
            if (contains)
            {
                command += " --contains";
            }
            string verboseGitDescribe = CommandLineUtilities.ExecuteCommandLineCommand(
                command: command,
                directory: gitRepositoryRootDirectoryPath);
            if (string.IsNullOrEmpty(
                value: verboseGitDescribe))
            {
                verboseGitDescribe = GetGitDescribeFallback(
                    invalidHeadLabel: invalidHeadLabel,
                    gitRepositoryRootDirectoryPath: gitRepositoryRootDirectoryPath);
            }
            return verboseGitDescribe;
        }
        /// <summary>
        /// Runs a command to fetch the current
        /// git branch name and returns the output.
        /// </summary>
        /// <param name="detachedHeadLabel">
        /// The label for a detached HEAD state.
        /// </param>
        /// <param name="repositoryRootDirectoryPath">
        /// The path to the root directory of the Git repository.
        /// </param>
        /// <returns>
        /// The output of <c>git branch --show-current</c>.
        /// If output is blank, then
        /// <c><paramref name="detachedHeadLabel"/></c> is used instead.
        /// </returns>
        internal static string GetCurrentGitBranchName(
            string detachedHeadLabel,
            string repositoryRootDirectoryPath)
        {
            string currentBranchName = CommandLineUtilities.ExecuteCommandLineCommand(
                command: "git branch --show-current",
                directory: repositoryRootDirectoryPath);
            if (string.IsNullOrEmpty(
                value: currentBranchName))
            {
                return detachedHeadLabel;
            }
            return currentBranchName;
        }
        /// <summary>
        /// Runs a fallback git describe command and returns the output.
        /// </summary>
        /// <param name="invalidHeadLabel">
        /// A text value representing an invalid HEAD state.
        /// </param>
        /// <param name="gitRepositoryRootDirectoryPath">
        /// The path to the root directory of the Git repository.
        /// </param>
        /// <returns>
        /// The output of
        /// <c>git describe --always</c> with trailing white space trimmed.
        /// If output is blank, then
        /// <c><paramref name="invalidHeadLabel"/></c> is returned instead.
        /// </returns>
        private static string GetGitDescribeFallback(
            string invalidHeadLabel,
            string gitRepositoryRootDirectoryPath)
        {
            string gitDescribeFallback = CommandLineUtilities.ExecuteCommandLineCommand(
                command: "git describe --always",
                directory: gitRepositoryRootDirectoryPath);
            if (string.IsNullOrEmpty(
                value: gitDescribeFallback))
            {
                return invalidHeadLabel;
            }
            return gitDescribeFallback;
        }
        /// <summary>
        /// Makes a list of pattern arguments.
        /// </summary>
        /// <param name="patternsToAdd">
        /// The patterns to be added to the arguments.
        /// </param>
        /// <param name="patternArgument">
        /// The argument taking a pattern.
        /// </param>
        /// <example>
        /// Given a pattern argument <c>--match</c>
        /// and patterns <c>pattern1</c> &amp; <c>pattern2</c>, the resulting string
        /// will be <c> --match "pattern1" --match "pattern2"</c>.
        /// </example>
        /// <returns>
        /// Each pattern will be added to the argument taking a pattern.
        /// Given a pattern argument <c>--match</c>
        /// and patterns <c>pattern1</c> &amp; <c>pattern2</c>, the resulting string
        /// will be <c> --match "pattern1" --match "pattern2"</c>.
        /// </returns>
        private static string AddPatterns(
            ReadOnlyCollection<string> patternsToAdd,
            string patternArgument)
        {
            if (patternsToAdd is null || patternsToAdd.Count is 0)
            {
                return null;
            }
            string patterns = null;
            foreach (string pattern in patternsToAdd)
            {
                if (string.IsNullOrWhiteSpace(
                    value: pattern))
                {
                    continue;
                }
                patterns += ' '
                    + patternArgument
                    + " \""
                    + pattern
                    + '\"';
            }
            return patterns;
        }
        /// <summary>
        /// Escapes backslashes and double quotation marks for <c>--dirty</c>
        /// and <c>--broken</c> arguments from <c>git describe</c>,
        /// then pads the result in double quotation marks.
        /// </summary>
        /// <param name="unescapedMark">
        /// The mark to be escaped.
        /// </param>
        /// <example>
        /// <c>\"</c> becomes <c>"\\\""</c>.
        /// </example>
        /// <returns>
        /// <c><paramref name="unescapedMark"/></c> with backslashes and double
        /// quotation marks escaped, then surrounds the result in double
        /// quotation marks.
        /// </returns>
        private static string EscapeMark(
            string unescapedMark)
        {
            if (string.IsNullOrWhiteSpace(
                value: unescapedMark))
            {
                return null;
            }
            // --dirty and --broken arguments from Git describe accept double quotation marks and backslashes and thus must be escaped properly.
            string escapedMark = unescapedMark
                .Replace(
                    oldValue: "\\",
                    newValue: "\\\\")
                .Replace(
                oldValue: "\"",
                newValue: "\\\"");
            return '\"' + escapedMark + '\"';
        }
        #endregion Methods
    }
}
