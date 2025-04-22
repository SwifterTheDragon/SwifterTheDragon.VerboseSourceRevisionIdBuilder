// Copyright SwifterTheDragon, 2024-2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core
{
    /// <summary>
    /// Generates a single file providing the current verbose source revision ID.
    /// </summary>
    [Generator]
    // This diagnostic only shows up in build output logs, for reasons unknown.
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
    internal sealed class VerboseSourceRevisionIdGenerator : IIncrementalGenerator
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
    {
        #region Fields & Properties
        /// <summary>
        /// The name of the configuration file for this source generator.
        /// </summary>
        internal static string ConfigurationFileName
        {
            get
            {
                return "VerboseSourceRevisionIdConfig.txt";
            }
        }
        #endregion Fields & Properties
        #region Methods
        public void Initialize(
            IncrementalGeneratorInitializationContext context)
        {
            IncrementalValuesProvider<(
                string semanticVersion,
                string generatedFileName,
                string generatedNamespace,
                string generatedTypeName,
                string generatedFieldName)> configurationProvider = FilterConfigFile(
                    context.AdditionalTextsProvider)
                    .Select(
                        selector: (additionalText, cancellationToken) =>
                        {
                            Dictionary<string, string> dictionary = AdditionalTextOptionParser.ParseOptions(
                                additionalText: additionalText,
                                cancellationToken: cancellationToken);
                            string semanticVersion = ParseSemanticVersion(
                                options: dictionary,
                                configurationFilePath: additionalText.Path);
                            string generatedFileName = ParseGeneratedFileName(
                                options: dictionary);
                            string generatedNamespace = ParseGeneratedNamespace(
                                options: dictionary);
                            string generatedTypeName = ParseGeneratedTypeName(
                                options: dictionary);
                            string generatedFieldName = ParseGeneratedFieldName(
                                options: dictionary);
                            return (semanticVersion, generatedFileName, generatedNamespace, generatedTypeName, generatedFieldName);
                        });
            RegisterOutput(
                context: context,
                source: configurationProvider);
        }
        /// <summary>
        /// Filters additional texts by this generator's
        /// configuration file name.
        /// </summary>
        /// <param name="additionalTextsProvider">
        /// The unfiltered additional texts provider to run the filter on.
        /// </param>
        /// <returns>
        /// A provider of all additional texts with a file name of
        /// <c><see cref="ConfigurationFileName"/></c>.
        /// </returns>
        private static IncrementalValuesProvider<AdditionalText> FilterConfigFile(
            in IncrementalValuesProvider<AdditionalText> additionalTextsProvider)
        {
            return additionalTextsProvider.Where(
                predicate: (additionalText) =>
                {
                    return Path.GetFileName(
                        path: additionalText.Path)
                        .Equals(
                            value: ConfigurationFileName,
                            comparisonType: System.StringComparison.Ordinal);
                });
        }
        /// <summary>
        /// Parses the semantic version prefix from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.SemanticVersionPrefix"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.SemanticVersionPrefix"/></c>
        /// is used instead.
        /// </returns>
        private static string ParseSemanticVersionPrefix(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            return AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.SemanticVersionPrefix,
                defaultValue: ConfigurationDefaults.SemanticVersionPrefix);
        }
        /// <summary>
        /// Parses the semantic version major version label from configuration
        /// data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.SemanticVersionMajorVersionLabel"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.SemanticVersionMajorVersion"/></c>
        /// is used instead.
        /// </returns>
        private static int ParseSemanticVersionMajorVersion(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            int defaultSemanticVersionMajorVersionLabel = ConfigurationDefaults.SemanticVersionMajorVersion;
            int parsedSemanticVersionMajorVersionLabel = AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.SemanticVersionMajorVersionLabel,
                defaultValue: defaultSemanticVersionMajorVersionLabel);
            if (parsedSemanticVersionMajorVersionLabel < 0)
            {
                return defaultSemanticVersionMajorVersionLabel;
            }
            return parsedSemanticVersionMajorVersionLabel;
        }
        /// <summary>
        /// Parses the semantic version minor version label from configuration
        /// data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <param name="semanticVersionMajorVersionLabel">
        /// The semantic version major version label.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.SemanticVersionMinorVersionLabel"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist
        /// &amp; <c><paramref name="semanticVersionMajorVersionLabel"/></c>
        /// is <c>0</c>, then <c>1</c> is returned.
        /// Otherwise,
        /// <c><see cref="ConfigurationDefaults.SemanticVersionMinorVersion"/></c>
        /// is used.
        /// </returns>
        private static int ParseSemanticVersionMinorVersion(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options,
#pragma warning restore S3242 // Method parameters should be declared with base types
            int semanticVersionMajorVersionLabel)
        {
            int defaultSemanticVersionMinorVersionLabel = ConfigurationDefaults.SemanticVersionMinorVersion;
            if (semanticVersionMajorVersionLabel > 0)
            {
                defaultSemanticVersionMinorVersionLabel = 0;
            }
            int parsedSemanticVersionMinorVersionLabel = AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.SemanticVersionMinorVersionLabel,
                defaultValue: defaultSemanticVersionMinorVersionLabel);
            if (parsedSemanticVersionMinorVersionLabel < 0)
            {
                return defaultSemanticVersionMinorVersionLabel;
            }
            return parsedSemanticVersionMinorVersionLabel;
        }
        /// <summary>
        /// Parses the semantic version patch version label from
        /// configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.SemanticVersionPatchVersionLabel"/></c>
        /// in <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.SemanticVersionPatchVersion"/></c>
        /// is used instead.
        /// </returns>
        private static int ParseSemanticVersionPatchVersion(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            int defaultSemanticVersionPatchVersionLabel = ConfigurationDefaults.SemanticVersionPatchVersion;
            int parsedSemanticVersionPatchVersionLabel = AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.SemanticVersionPatchVersionLabel,
                defaultValue: defaultSemanticVersionPatchVersionLabel);
            if (parsedSemanticVersionPatchVersionLabel < 0)
            {
                return defaultSemanticVersionPatchVersionLabel;
            }
            return parsedSemanticVersionPatchVersionLabel;
        }
        /// <summary>
        /// Parses major, minor, and patch semantic version labels from
        /// configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <example>
        /// <c>1.2.3</c>
        /// </example>
        /// <returns>
        /// A combination of
        /// <c><see cref="ParseSemanticVersionMajorVersion(Dictionary{string, string})"/></c>,
        /// <c><see cref="ParseSemanticVersionMinorVersion(Dictionary{string, string}, int)"/></c>,
        /// and
        /// <c><see cref="ParseSemanticVersionPatchVersion(Dictionary{string, string})"/></c>,
        /// formatted as
        /// "<c>MAJOR</c>.<c>MINOR</c>.<c>PATCH</c>".
        /// </returns>
        private static string ParseMainSemanticVersionLabels(
            Dictionary<string, string> options)
        {
            int semanticVersionMajorVersionLabel = ParseSemanticVersionMajorVersion(
                options: options);
            int semanticVersionMinorVersionLabel = ParseSemanticVersionMinorVersion(
                options: options,
                semanticVersionMajorVersionLabel: semanticVersionMajorVersionLabel);
            int semanticVersionPatchVersionLabel = ParseSemanticVersionPatchVersion(
                options: options);
            return semanticVersionMajorVersionLabel.ToString(
                provider: CultureInfo.InvariantCulture)
                + '.'
                + semanticVersionMinorVersionLabel.ToString(
                    provider: CultureInfo.InvariantCulture)
                + '.'
                + semanticVersionPatchVersionLabel.ToString(
                    provider: CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Parses the detached <c>HEAD</c> label from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.DetachedHeadLabel"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.DetachedHeadLabel"/></c> is
        /// used instead.
        /// </returns>
        private static string ParseDetachedHeadLabel(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            return AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.DetachedHeadLabel,
                defaultValue: ConfigurationDefaults.DetachedHeadLabel);
        }
        /// <summary>
        /// Parses the current Git branch name.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <param name="configurationFilePath">
        /// The file path of this generator's configuration file.
        /// </param>
        /// <returns>
        /// The output of
        /// <c><see cref="GitHelper.GetCurrentGitBranchName(string, string)"/></c>.
        /// </returns>
        private static string ParseCurrentGitBranchName(
            Dictionary<string, string> options,
            string configurationFilePath)
        {
            string detachedHeadLabel = ParseDetachedHeadLabel(
                options: options);
            string gitRepositoryRootDirectory = ParseGitRepositoryRootDirectoryPath(
                options: options,
                configurationFilePath: configurationFilePath);
            return GitHelper.GetCurrentGitBranchName(
                detachedHeadLabel: detachedHeadLabel,
                repositoryRootDirectoryPath: gitRepositoryRootDirectory);
        }
        /// <summary>
        /// Parses the default Git branch name from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.DefaultGitBranchName"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.DefaultGitBranchName"/></c>
        /// is used instead.
        /// </returns>
        private static string ParseDefaultGitBranchName(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            return AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.DefaultGitBranchName,
                defaultValue: ConfigurationDefaults.DefaultGitBranchName);
        }
        /// <summary>
        /// Parses the semantic version pre-release label from
        /// configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <param name="configurationFilePath">
        /// The file path of this generator's configuration file.
        /// </param>
        /// <example>
        /// "<c>-beta</c>", if the default Git branch name is "<c>main</c>"
        /// and the current Git branch name is "<c>beta</c>".
        /// </example>
        /// <returns>
        /// An empty string if
        /// <c><see cref="ParseCurrentGitBranchName(Dictionary{string, string}, string)"/></c>
        /// matches
        /// <c><see cref="ParseDefaultGitBranchName(Dictionary{string, string})"/></c>.
        /// Otherwise,
        /// <c><see cref="ParseCurrentGitBranchName(Dictionary{string, string}, string)"/></c>,
        /// prefixed with <c>-</c>.
        /// </returns>
        private static string ParseSemanticVersionPreReleaseLabel(
            Dictionary<string, string> options,
            string configurationFilePath)
        {
            string currentGitBranchName = ParseCurrentGitBranchName(
                options: options,
                configurationFilePath: configurationFilePath);
            string defaultGitBranchName = ParseDefaultGitBranchName(
                options: options);
            string semanticVersionPreReleaseLabel = string.Empty;
            if (!currentGitBranchName.Equals(
                value: defaultGitBranchName,
                comparisonType: System.StringComparison.Ordinal))
            {
                semanticVersionPreReleaseLabel = '-'
                    + currentGitBranchName;
            }
            return semanticVersionPreReleaseLabel;
        }
        /// <summary>
        /// Parses the dirty mark from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.DirtyMark"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.DirtyMark"/></c>
        /// is used instead.
        /// </returns>
        private static string ParseDirtyMark(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            return AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.DirtyMark,
                defaultValue: ConfigurationDefaults.DirtyMark);
        }
        /// <summary>
        /// Parses the broken mark from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.BrokenMark"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.BrokenMark"/></c>
        /// is used instead.
        /// </returns>
        private static string ParseBrokenMark(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            return AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.BrokenMark,
                defaultValue: ConfigurationDefaults.BrokenMark);
        }
        /// <summary>
        /// Parses the invalid <c>HEAD</c> label from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.InvalidHeadLabel"/></c> in
        /// <paramref name="options"/>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.InvalidHeadLabel"/></c>
        /// is used instead.
        /// </returns>
        private static string ParseInvalidHeadLabel(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            return AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.InvalidHeadLabel,
                defaultValue: ConfigurationDefaults.InvalidHeadLabel);
        }
        /// <summary>
        /// Parses the <c><see cref="GitReferenceType"/></c> from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.GitReferenceType"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.GitReferenceType"/></c>
        /// is used instead.
        /// </returns>
        private static GitReferenceType ParseGitReferenceType(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            return AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.GitReferenceType,
                defaultValue: ConfigurationDefaults.GitReferenceType);
        }
        /// <summary>
        /// Parses the candidate amount from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.CandidateAmount"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.CandidateAmount"/></c>
        /// is used instead.
        /// </returns>
        private static int ParseCandidateAmount(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            int defaultCandidateAmount = ConfigurationDefaults.CandidateAmount;
            int parsedCandidateAmount = AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.CandidateAmount,
                defaultValue: defaultCandidateAmount);
            if (parsedCandidateAmount < 0)
            {
                return defaultCandidateAmount;
            }
            return parsedCandidateAmount;
        }
        /// <summary>
        /// Parses the abbrev length from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.AbbrevLength"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.AbbrevLength"/></c>
        /// is used instead.
        /// </returns>
        private static string ParseAbbrevLength(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            string defaultAbbrevLength = ConfigurationDefaults.AbbrevLength.ToString(
                provider: CultureInfo.InvariantCulture);
            string parsedAbbrevLength = AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.AbbrevLength,
                defaultValue: defaultAbbrevLength);
            if (int.TryParse(
                s: parsedAbbrevLength,
                style: NumberStyles.Integer,
                provider: CultureInfo.InvariantCulture,
                result: out int _)
                || parsedAbbrevLength.Equals(
                    value: "Dynamic",
                    comparisonType: System.StringComparison.OrdinalIgnoreCase))
            {
                return parsedAbbrevLength;
            }
            return defaultAbbrevLength;
        }
        /// <summary>
        /// Parses the <c><see cref="ParentCommitType"/></c> from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.ParentCommitType"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.ParentCommitType"/></c>
        /// is used instead.
        /// </returns>
        private static ParentCommitType ParseParentCommitType(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            return AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.ParentCommitType,
                defaultValue: ConfigurationDefaults.ParentCommitType);
        }
        /// <summary>
        /// Parses match patterns from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.MatchPatterns"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.MatchPatterns"/></c>
        /// is used instead.
        /// </returns>
        private static ReadOnlyCollection<string> ParseMatchPatterns(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            return AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.MatchPatterns,
                defaultValue: ConfigurationDefaults.MatchPatterns);
        }
        /// <summary>
        /// Parses exclude patterns from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.ExcludePatterns"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.ExcludePatterns"/></c>
        /// is used instead.
        /// </returns>
        private static ReadOnlyCollection<string> ParseExcludePatterns(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            return AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.ExcludePatterns,
                defaultValue: ConfigurationDefaults.ExcludePatterns);
        }
        /// <summary>
        /// Parses the <c><see cref="GitTagState"/></c> from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.GitTagState"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.GitTagState"/></c>
        /// is used instead.
        /// </returns>
        private static GitTagState ParseGitTagState(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            return AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.GitTagState,
                defaultValue: ConfigurationDefaults.GitTagState);
        }
        /// <summary>
        /// Parses the Git repository root directory path from
        /// configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <param name="configurationFilePath">
        /// The file path to this generator's configuration file.
        /// </param>
        /// <returns>
        /// An absolute file path to the Git repository's root directory.
        /// </returns>
        private static string ParseGitRepositoryRootDirectoryPath(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options,
#pragma warning restore S3242 // Method parameters should be declared with base types
            string configurationFilePath)
        {
            string gitRepositoryRootRelativeToConfigurationFilePath = AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.RepositoryRootDirectoryRelativeToConfigurationFilePath,
                defaultValue: ConfigurationDefaults.RepositoryRootDirectoryRelativeToConfigurationFilePath);
            return Path.GetFullPath(
                path: Path.Combine(
                    path1: configurationFilePath,
                    path2: gitRepositoryRootRelativeToConfigurationFilePath));
        }
        /// <summary>
        /// Parses a verbose git describe command based on configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <param name="configurationFilePath">
        /// The file path to this generator's configuration file.
        /// </param>
        /// <returns>
        /// The output of
        /// <c><see cref="GitHelper.GetVerboseGitDescribe(VerboseGitDescribeConfiguration)"/></c>,
        /// with '<c>/</c>' replaced with '<c>-</c>'.
        /// </returns>
        private static string ParseVerboseGitDescribe(
            Dictionary<string, string> options,
            string configurationFilePath)
        {
            string dirtyMark = ParseDirtyMark(
                options: options);
            string brokenMark = ParseBrokenMark(
                options: options);
            string invalidHeadLabel = ParseInvalidHeadLabel(
                options: options);
            GitReferenceType gitReferenceType = ParseGitReferenceType(
                options: options);
            int candidateAmount = ParseCandidateAmount(
                options: options);
            string abbrevLength = ParseAbbrevLength(
                options: options);
            ParentCommitType parentCommitType = ParseParentCommitType(
                options: options);
            ReadOnlyCollection<string> matchPatterns = ParseMatchPatterns(
                options: options);
            ReadOnlyCollection<string> excludePatterns = ParseExcludePatterns(
                options: options);
            GitTagState gitTagState = ParseGitTagState(
                options: options);
            string gitRepositoryRootDirectoryPath = ParseGitRepositoryRootDirectoryPath(
                options: options,
                configurationFilePath: configurationFilePath);
            var configuration = new VerboseGitDescribeConfiguration
            {
                DirtyMark = dirtyMark,
                BrokenMark = brokenMark,
                InvalidHeadLabel = invalidHeadLabel,
                GitReferenceType = gitReferenceType,
                CandidateAmount = candidateAmount,
                AbbrevLength = abbrevLength,
                ParentCommitType = parentCommitType,
                MatchPatterns = matchPatterns,
                ExcludePatterns = excludePatterns,
                GitTagState = gitTagState,
                GitRepositoryRootDirectoryPath = gitRepositoryRootDirectoryPath
            };
            return GitHelper.GetVerboseGitDescribe(
                configuration: configuration);
        }
        /// <summary>
        /// Parses the semantic version build metadata label from
        /// configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <param name="configurationFilePath">
        /// The file path to this generator's configuration file.
        /// </param>
        /// <example>
        /// "<c>+v1.2.3-4-ga1b2c3d4</c>".
        /// </example>
        /// <returns>
        /// The output of
        /// <c><see cref="ParseVerboseGitDescribe(Dictionary{string, string}, string)"/></c>,
        /// prefixed with <c>+</c>.
        /// </returns>
        private static string ParseSemanticVersionBuildMetadataLabel(
            Dictionary<string, string> options,
            string configurationFilePath)
        {
            return '+'
                + ParseVerboseGitDescribe(
                    options: options,
                    configurationFilePath: configurationFilePath);
        }
        /// <summary>
        /// Parses the semantic version suffix from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.SemanticVersionSuffix"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.SemanticVersionSuffix"/></c>
        /// is used instead.
        /// </returns>
        private static string ParseSemanticVersionSuffix(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            return AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.SemanticVersionSuffix,
                defaultValue: ConfigurationDefaults.SemanticVersionSuffix);
        }
        /// <summary>
        /// Parses the entire semantic version from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <param name="configurationFilePath">
        /// The file path to this generator's configuration file.
        /// </param>
        /// <example>
        /// "<c>prefix-1.2.3-beta+v1.2.3-beta-4-ga1b2c3d4-dirty-suffix</c>".
        /// </example>
        /// <returns>
        /// Combines
        /// <c><see cref="ParseSemanticVersionPrefix(Dictionary{string, string})"/></c>,
        /// <c><see cref="ParseMainSemanticVersionLabels(Dictionary{string, string})"/></c>,
        /// <c><see cref="ParseSemanticVersionPreReleaseLabel(Dictionary{string, string}, string)"/></c>,
        /// <c><see cref="ParseSemanticVersionBuildMetadataLabel(Dictionary{string, string}, string)"/></c>,
        /// and
        /// <c><see cref="ParseSemanticVersionSuffix(Dictionary{string, string})"/></c>,
        /// then escapes quotes for use in a verbatim string literal.
        /// </returns>
        private static string ParseSemanticVersion(
            Dictionary<string, string> options,
            string configurationFilePath)
        {
            string semanticVersion = string.Empty;
            semanticVersion += ParseSemanticVersionPrefix(
                options: options);
            semanticVersion += ParseMainSemanticVersionLabels(
                options: options);
            semanticVersion += ParseSemanticVersionPreReleaseLabel(
                options: options,
                configurationFilePath: configurationFilePath);
            semanticVersion += ParseSemanticVersionBuildMetadataLabel(
                options: options,
                configurationFilePath: configurationFilePath);
            semanticVersion += ParseSemanticVersionSuffix(
                options: options);
            // The semantic version will be used in a verbatim string literal, so double quotation marks must be escaped.
            return semanticVersion.Replace(
                oldValue: "\"",
                newValue: "\"\"");
        }
        /// <summary>
        /// Parses the generated file name from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.GeneratedFileName"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.GeneratedFileName"/></c>
        /// is used instead.
        /// </returns>
        private static string ParseGeneratedFileName(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            return AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.GeneratedFileName,
                defaultValue: ConfigurationDefaults.GeneratedFileName);
        }
        /// <summary>
        /// Parses the generated namespace from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.GeneratedNamespace"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.GeneratedNamespace"/></c>
        /// is used instead.
        /// </returns>
        private static string ParseGeneratedNamespace(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            return AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.GeneratedNamespace,
                defaultValue: ConfigurationDefaults.GeneratedNamespace);
        }
        /// <summary>
        /// Parses the generated type name from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.GeneratedTypeName"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.GeneratedTypeName"/></c>
        /// is used instead.
        /// </returns>
        private static string ParseGeneratedTypeName(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            return AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.GeneratedTypeName,
                defaultValue: ConfigurationDefaults.GeneratedTypeName);
        }
        /// <summary>
        /// Parses the generated field name from configuration data.
        /// </summary>
        /// <param name="options">
        /// The configuration data to parse from.
        /// </param>
        /// <returns>
        /// The value at the key of
        /// <c><see cref="ConfigurationKeys.GeneratedFieldName"/></c> in
        /// <c><paramref name="options"/></c>, if it exists.
        /// If it does not exist, then
        /// <c><see cref="ConfigurationDefaults.GeneratedFieldName"/></c>
        /// is used instead.
        /// </returns>
        private static string ParseGeneratedFieldName(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options)
#pragma warning restore S3242 // Method parameters should be declared with base types
        {
            return AdditionalTextOptionParser.GetValue(
                options: options,
                key: ConfigurationKeys.GeneratedFieldName,
                defaultValue: ConfigurationDefaults.GeneratedFieldName);
        }
        /// <summary>
        /// Registers source code output to this generator's context,
        /// to provide a constant field containing the current value of the
        /// verbose source revision ID.
        /// </summary>
        /// <param name="context">
        /// The context of this generator.
        /// </param>
        /// <param name="source">
        /// The configuration data provider.
        /// </param>
        private static void RegisterOutput(
            in IncrementalGeneratorInitializationContext context,
            in IncrementalValuesProvider<(
                string semanticVersion,
                string generatedFileName,
                string generatedNamespace,
                string generatedTypeName,
                string generatedFieldName)> source)
        {
            context.RegisterSourceOutput(
                source: source,
                action: (sourceProductionContext, configuration) =>
                {
                    AssemblyName assemblyName = typeof(VerboseSourceRevisionIdGenerator).Assembly.GetName();
                    string toolName = assemblyName.Name;
                    string toolVersion = assemblyName.Version.ToString(
                        fieldCount: 4);
                    sourceProductionContext.AddSource(
                        hintName: configuration.generatedFileName,
                        sourceText: SourceText.From(
                            text: GeneratorHelper.MakeAutoGeneratedCodeHeader(
                                toolName: toolName,
                                toolVersion: toolVersion)
                                + "\n// Copyright SwifterTheDragon, 2024-2025. All Rights Reserved.\n\n"
                                + "using System.CodeDom.Compiler;\n\n"
                                + "namespace "
                                + configuration.generatedNamespace
                                + "\n{\n"
                                + "    [GeneratedCode(\n"
                                + "        tool: \""
                                + toolName
                                + "\",\n"
                                + "        version: \""
                                + toolVersion
                                + "\")]\n"
                                + "    internal static class "
                                + configuration.generatedTypeName
                                + "\n    {\n"
                                + "        internal const string "
                                + configuration.generatedFieldName
                                + " = @\""
                                + configuration.semanticVersion
                                + "\";\n"
                                + "    }\n"
                                + "}\n\n"
                                + GeneratorHelper.MakeAutoGeneratedFooter(
                                    toolName: toolName,
                                    generatorClassName: nameof(VerboseSourceRevisionIdGenerator)),
                            encoding: Encoding.UTF8));
                });
        }
        #endregion Methods
    }
}
