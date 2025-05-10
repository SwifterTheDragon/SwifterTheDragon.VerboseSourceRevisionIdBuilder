// Copyright SwifterTheDragon, and the SwifterTheDragon.VerboseSourceRevisionIdBuilder contributors, 2024-2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core
{
    /// <include
    /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
    /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="GitHelper"]/Description/*'/>
    internal static class GitHelper
    {
        #region Fields & Properties
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="GitHelper"]/Property[@name="AbbrevMaximum"]/*'/>
        private static int AbbrevMaximum
        {
            get
            {
                return 40;
            }
        }
        #endregion Fields & Properties
        #region Methods
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="GitHelper"]/Method[@name="GetVerboseGitDescribe(SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core.VerboseGitDescribeConfiguration)"]/*'/>
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
            if (configuration.GitTagState == GitTagState.ContainsHead)
            {
                command += " --contains";
            }
            string verboseGitDescribe = NormaliseIllegalSemanticVersionCharacters(
                input: CommandLineUtilities.ExecuteCommandLineCommand(
                    command: command,
                    directory: configuration.GitRepositoryRootDirectoryPath));
            if (string.IsNullOrEmpty(
                value: verboseGitDescribe))
            {
                verboseGitDescribe = GetGitDescribeFallback(
                    invalidHeadLabel: configuration.InvalidHeadLabel,
                    gitRepositoryRootDirectoryPath: configuration.GitRepositoryRootDirectoryPath);
            }
            return verboseGitDescribe;
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="GitHelper"]/Method[@name="GetCurrentGitBranchName(System.String,System.String)"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="GitHelper"]/Method[@name="GetGitDescribeFallback(System.String,System.String)"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="GitHelper"]/Method[@name="AddPatterns(System.Collections.ObjectModel.ReadOnlyCollection{System.String},System.String)"]/*'/>
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
            var patternBuilder = new StringBuilder();
            foreach (string pattern in patternsToAdd)
            {
                if (string.IsNullOrWhiteSpace(
                    value: pattern))
                {
                    continue;
                }
                patternBuilder.Append(
                    value: ' ')
                    .Append(
                        value: patternArgument)
                    .Append(
                        value: " \"")
                    .Append(
                        value: pattern)
                    .Append(
                        value: '\"');
            }
            return patternBuilder.ToString();
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="GitHelper"]/Method[@name="EscapeMark(System.String)"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="GitHelper"]/Method[@name="AddReferenceType(SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core.GitReferenceType)"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="GitHelper"]/Method[@name="AddFormatLength(System.String)"]/*'/>
        private static string AddFormatLength(
            string abbrevLength)
        {
            if (string.IsNullOrWhiteSpace(
                value: abbrevLength)
                || abbrevLength.Equals(
                    value: "Dynamic",
                    comparisonType: System.StringComparison.OrdinalIgnoreCase))
            {
                return " --long";
            }
            if (int.TryParse(
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
                return " --abbrev="
                    + parsedAbbrevLength.ToString(
                        provider: CultureInfo.InvariantCulture)
                    + " --long";
            }
            return " --long --abbrev="
                + ConfigurationDefaults.AbbrevLength.ToString(
                    provider: CultureInfo.InvariantCulture);
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="GitHelper"]/Method[@name="NormaliseIllegalSemanticVersionCharacters(System.String)"]/*'/>
        private static string NormaliseIllegalSemanticVersionCharacters(
            string input)
        {
            if (string.IsNullOrWhiteSpace(
                value: input))
            {
                return string.Empty;
            }
            string output = input
                .Replace(
                    oldValue: "/",
                    newValue: "-ForwardSlash-")
                .Replace(
                    oldValue: "~",
                    newValue: "-Tilde-")
                .Replace(
                    oldValue: "^",
                    newValue: "-Caret-");
            if (output.StartsWith(
                value: ".",
                comparisonType: System.StringComparison.OrdinalIgnoreCase))
            {
                return "-Period-"
                    + output.Substring(
                        startIndex: 1);
            }
            return output;
        }
        #endregion Methods
    }
}
