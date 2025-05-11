// Copyright SwifterTheDragon, and the SwifterTheDragon.VerboseSourceRevisionIdBuilder contributors, 2024-2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core
{
    /// <include
    /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
    /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Description/*'/>
    [Generator]
    // This diagnostic only shows up in build output logs, for reasons unknown.
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
    internal sealed class VerboseSourceRevisionIdGenerator : IIncrementalGenerator
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
    {
        #region Fields & Properties
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Property[@name="ConfigurationFileName"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="FilterConfigFile(Microsoft.CodeAnalysis.IncrementalValuesProvider{Microsoft.CodeAnalysis.AdditionalText}@)"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseSemanticVersionPrefix(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseSemanticVersionMajorVersion(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseSemanticVersionMinorVersion(System.Collections.Generic.Dictionary{System.String,System.String},System.Int32)"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseSemanticVersionPatchVersion(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseMainSemanticVersionLabels(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseDetachedHeadLabel(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseCurrentGitBranchName(System.Collections.Generic.Dictionary{System.String,System.String},System.String)"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseDefaultGitBranchName(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseSemanticVersionPreReleaseLabel(System.Collections.Generic.Dictionary{System.String,System.String},System.String)"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseDirtyMark(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseBrokenMark(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseInvalidHeadLabel(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseGitReferenceType(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseCandidateAmount(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseAbbrevLength(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseParentCommitType(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseMatchPatterns(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseExcludePatterns(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseGitTagState(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseGitRepositoryRootDirectoryPath(System.Collections.Generic.Dictionary{System.String,System.String},System.String)"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseVerboseGitDescribe(System.Collections.Generic.Dictionary{System.String,System.String},System.String)"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseSemanticVersionBuildMetadataLabel(System.Collections.Generic.Dictionary{System.String,System.String},System.String)"]/*'/>
        private static string ParseSemanticVersionBuildMetadataLabel(
            Dictionary<string, string> options,
            string configurationFilePath)
        {
            return '+'
                + ParseVerboseGitDescribe(
                    options: options,
                    configurationFilePath: configurationFilePath);
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseSemanticVersionSuffix(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseSemanticVersion(System.Collections.Generic.Dictionary{System.String,System.String},System.String)"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseGeneratedFileName(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseGeneratedNamespace(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseGeneratedTypeName(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="ParseGeneratedFieldName(System.Collections.Generic.Dictionary{System.String,System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="VerboseSourceRevisionIdGenerator"]/Method[@name="RegisterOutput(Microsoft.CodeAnalysis.IncrementalGeneratorInitializationContext@,Microsoft.CodeAnalysis.IncrementalValuesProvider{System.ValueTuple{System.String,System.String,System.String,System.String,System.String}}@)"]/*'/>
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
                    StringBuilder sourceBuilder = new StringBuilder()
                        .AppendLine(
                            value: GeneratorHelper.MakeAutoGeneratedCodeHeader())
                        .AppendLine(
                            value: "// Copyright SwifterTheDragon, and the SwifterTheDragon.VerboseSourceRevisionIdBuilder contributors, 2024-2025. All Rights Reserved.")
                        .AppendLine(
                            value: "// SPDX-License-Identifier: MIT")
                        .AppendLine()
                        .AppendLine(
                            value: "using System.CodeDom.Compiler;")
                        .AppendLine()
                        .Append(
                            value: "namespace ")
                        .AppendLine(
                            value: configuration.generatedNamespace)
                        .AppendLine(
                            value: "{")
                        .AppendLine(
                            value: "    /// <summary>")
                        .Append(
                            value: "    /// This class is incrementally source generated by \"<c>")
                        .Append(
                            value: GeneratorHelper.ToolName)
                        .AppendLine(
                            value: "</c>\".")
                        .AppendLine(
                            value: "    /// </summary>")
                        .AppendLine(
                            value: "    [GeneratedCode(")
                        .Append(
                            value: "        tool: \"")
                        .Append(
                            value: GeneratorHelper.ToolName)
                        .AppendLine(
                            value: "\",")
                        .Append(
                            value: "        version: \"")
                        .Append(
                            value: GeneratorHelper.ToolVersion)
                        .AppendLine(
                            value: "\")]")
                        .Append(
                            value: "    internal static class ")
                        .AppendLine(
                            value: configuration.generatedTypeName)
                        .AppendLine(
                            value: "    {")
                        .AppendLine(
                            value: "        /// <summary>")
                        .AppendLine(
                            value: "        /// The current verbose source revision ID,")
                        .Append(
                            value: "        /// incrementally generated by \"<c>")
                        .Append(
                            value: GeneratorHelper.ToolName)
                        .AppendLine(
                            value: "\"</c>.")
                        .AppendLine(
                            value: "        /// </summary>")
                        .Append(
                            value: "        internal const string ")
                        .Append(
                            value: configuration.generatedFieldName)
                        .Append(
                            value: " = @\"")
                        .Append(
                            value: configuration.semanticVersion)
                        .AppendLine(
                            value: "\";")
                        .AppendLine(
                            value: "    }")
                        .AppendLine(
                            value: "}")
                        .AppendLine()
                        .Append(
                            value: GeneratorHelper.MakeAutoGeneratedFooter(
                                generatorClassName: nameof(VerboseSourceRevisionIdGenerator)));
                    sourceProductionContext.AddSource(
                        hintName: configuration.generatedFileName,
                        sourceText: SourceText.From(
                            text: sourceBuilder.ToString(),
                            encoding: Encoding.UTF8));
                });
        }
        #endregion Methods
    }
}
