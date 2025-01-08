// Copyright SwifterTheDragon, 2025. All Rights Reserved.

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
        /// <param name="configuration">
        /// The configuration data for the verbose Git describe command.
        /// </param>
        /// <returns>
        /// The output of a verbose Git describe command
        /// with trailing white space trimmed.
        /// If output is blank, then
        /// <c><see cref="GetGitDescribeFallback"/></c> is used instead.
        /// </returns>
        internal static string GetVerboseGitDescribe(
            VerboseGitDescribeConfiguration configuration)
        {
            if (configuration is null)
            {
                return string.Empty;
            }
            string command = "git describe";
            command += AddReferenceType(
                gitReferenceType: configuration.GitReferenceType);
            if (!string.IsNullOrWhiteSpace(
                value: configuration.DirtyMark))
            {
                string escapedDirtyMark = EscapeMark(
                    unescapedMark: configuration.DirtyMark);
                command += " --dirty="
                    + escapedDirtyMark;
            }
            if (!string.IsNullOrWhiteSpace(
                value: configuration.BrokenMark))
            {
                string escapedBrokenMark = EscapeMark(
                    unescapedMark: configuration.BrokenMark);
                command += " --broken="
                    + escapedBrokenMark;
            }
            command += AddFormatLength(
                abbrevLength: configuration.AbbrevLength);
            command += " --candidates="
                + configuration.CandidateAmount.ToString(
                    provider: CultureInfo.InvariantCulture);
            if (configuration.ParentCommitType == ParentCommitType.FirstOnly)
            {
                command += " --first-parent";
            }
            command += AddPatterns(
                patternsToAdd: configuration.MatchPatterns,
                patternArgument: "--match");
            command += AddPatterns(
                patternsToAdd: configuration.ExcludePatterns,
                patternArgument: "--exclude");
            if (configuration.GitTagState == GitTagState.ContainsCommit)
            {
                command += " --contains";
            }
            string verboseGitDescribe = CommandLineUtilities.ExecuteCommandLineCommand(
                command: command,
                directory: configuration.GitRepositoryRootDirectoryPath);
            if (string.IsNullOrEmpty(
                value: verboseGitDescribe))
            {
                verboseGitDescribe = GetGitDescribeFallback(
                    invalidHeadLabel: configuration.InvalidHeadLabel,
                    gitRepositoryRootDirectoryPath: configuration.GitRepositoryRootDirectoryPath);
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
        /// </returns>
        private static string AddPatterns(
#pragma warning disable S3242 // Method parameters should be declared with base types
            ReadOnlyCollection<string> patternsToAdd,
#pragma warning restore S3242 // Method parameters should be declared with base types
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
        /// <summary>
        /// Gives a reference type argument.
        /// </summary>
        /// <param name="gitReferenceType">
        /// The reference type to transform into an argument.
        /// </param>
        /// <returns>
        /// "<c> --tags</c>" if <c><paramref name="gitReferenceType"/></c> is
        /// <see cref="GitReferenceType.Tags"/>,
        /// "<c> --all</c>" if <c><paramref name="gitReferenceType"/></c> is
        /// <see cref="GitReferenceType.All"/>,
        /// otherwise an empty string.
        /// </returns>
        private static string AddReferenceType(
            GitReferenceType gitReferenceType)
        {
            switch (gitReferenceType)
            {
                case GitReferenceType.Tags:
                    return " --tags";
                case GitReferenceType.All:
                    return " --all";
                case GitReferenceType.None:
                case GitReferenceType.AnnotatedTags:
                default:
                    break;
            }
            return string.Empty;
        }
        /// <summary>
        /// Gives arguments that specify the format length.
        /// </summary>
        /// <param name="abbrevLength">
        /// How long the formatted output should be.
        /// </param>
        /// <returns>
        /// If <c><paramref name="abbrevLength"/></c> is below <c>1</c>,
        /// an empty string.
        /// If <c><paramref name="abbrevLength"/></c> is above <c>0</c>,
        /// <c><paramref name="abbrevLength"/></c> is clamped to
        /// (<c><see cref="AbbrevMinimum"/></c>
        /// -<c><see cref="AbbrevMaximum"/></c>) and "
        /// <c> --abbrev=<paramref name="abbrevLength"/> --long</c>" is used.
        /// Otherwise (if <c><paramref name="abbrevLength"/></c> is not a
        /// valid integer), "<c> --long</c>".
        /// </returns>
        private static string AddFormatLength(
            string abbrevLength)
        {
            if (!string.IsNullOrWhiteSpace(
                value: abbrevLength)
                && int.TryParse(
                    s: abbrevLength,
                    style: NumberStyles.Integer,
                    provider: CultureInfo.InvariantCulture,
                    result: out int parsedAbbrevLength))
            {
                if (parsedAbbrevLength < 1)
                {
                    return string.Empty;
                }
                if (parsedAbbrevLength > AbbrevMaximum)
                {
                    parsedAbbrevLength = AbbrevMaximum;
                }
                if (parsedAbbrevLength < AbbrevMinimum)
                {
                    parsedAbbrevLength = AbbrevMinimum;
                }
                return " --abbrev="
                    + parsedAbbrevLength.ToString(
                        provider: CultureInfo.InvariantCulture)
                    + " --long";
            }
            return " --long";
        }
        #endregion Methods
    }
}
