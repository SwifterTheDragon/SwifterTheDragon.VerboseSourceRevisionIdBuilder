// Copyright SwifterTheDragon, 2025. All Rights Reserved.

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
    internal sealed class VerboseSourceRevisionIdGenerator : IIncrementalGenerator
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
            IncrementalValuesProvider<AdditionalText> configurationFileProvider = context.AdditionalTextsProvider.Where(
                predicate: (additionalText) =>
                {
                    return Path.GetFileName(
                            path: additionalText.Path).Equals(
                        value: ConfigurationFileName,
                        comparisonType: System.StringComparison.Ordinal);
                });
            IncrementalValuesProvider<(string semanticVersion, string generatedFileName, string generatedNamespace, string generatedTypeName, string generatedFieldName, string toolName, string toolVersion, string generatorClassName)> configurationProvider = configurationFileProvider.Select(
                selector: (additionalText, cancellationToken) =>
                {
                    SourceText configurationSourceText = additionalText.GetText(
                        cancellationToken: cancellationToken);
                    Dictionary<string, string> dictionary = AdditionalTextOptionParser.ParseOptions(
                        additionalText: additionalText,
                        cancellationToken: cancellationToken);
                    string semanticVersion = string.Empty;
                    string semanticVersionPrefix = AdditionalTextOptionParser.GetValue(
                        options: dictionary,
                        key: ConfigurationKeys.SemanticVersionPrefix,
                        defaultValue: ConfigurationDefaults.DefaultSemanticVersionPrefix);
                    semanticVersion += semanticVersionPrefix;
                    string repositoryRootDirectoryRelativeToConfigurationFilePath = AdditionalTextOptionParser.GetValue(
                        options: dictionary,
                        key: ConfigurationKeys.RepositoryRootDirectoryRelativeToConfigurationFilePath,
                        defaultValue: ConfigurationDefaults.DefaultRepositoryRootDirectoryRelativeToConfigurationFilePath);
                    string repositoryRootDirectoryPath = Path.GetFullPath(
                        Path.Combine(
                            additionalText.Path,
                            repositoryRootDirectoryRelativeToConfigurationFilePath));
                    int semanticVersionMajorVersion = ConfigurationDefaults.DefaultSemanticVersionMajorVersion;
                    int semanticVersionMinorVersion = ConfigurationDefaults.DefaultSemanticVersionMinorVersion;
                    int semanticVersionPatchVersion = ConfigurationDefaults.DefaultSemanticVersionPatchVersion;
                    if (AdditionalTextOptionParser.TryGetValue(
                        options: dictionary,
                        key: ConfigurationKeys.SemanticVersionMajorVersion,
                        result: out int? parsedSemanticVersionMajorVersion)
                        && parsedSemanticVersionMajorVersion.Value > -1)
                    {
                        semanticVersionMajorVersion = parsedSemanticVersionMajorVersion.Value;
                        if (semanticVersionMajorVersion > 0)
                        {
                            semanticVersionMinorVersion = 0;
                        }
                    }
                    if (AdditionalTextOptionParser.TryGetValue(
                        options: dictionary,
                        key: ConfigurationKeys.SemanticVersionMinorVersion,
                        result: out int? parsedSemanticVersionMinorVersion)
                        && parsedSemanticVersionMinorVersion.Value > -1)
                    {
                        semanticVersionMinorVersion = parsedSemanticVersionMinorVersion.Value;
                    }
                    if (AdditionalTextOptionParser.TryGetValue(
                        options: dictionary,
                        key: ConfigurationKeys.SemanticVersionPatchVersion,
                        result: out int? parsedSemanticVersionPatchVersion)
                        && parsedSemanticVersionPatchVersion.Value > -1)
                    {
                        semanticVersionPatchVersion = parsedSemanticVersionPatchVersion.Value;
                    }
                    semanticVersion += semanticVersionMajorVersion.ToString(
                        provider: CultureInfo.InvariantCulture)
                        + '.'
                        + semanticVersionMinorVersion.ToString(
                            provider: CultureInfo.InvariantCulture)
                        + '.'
                        + semanticVersionPatchVersion.ToString(
                            provider: CultureInfo.InvariantCulture);
                    string detachedHeadLabel = AdditionalTextOptionParser.GetValue(
                        options: dictionary,
                        key: ConfigurationKeys.DetachedHeadLabel,
                        defaultValue: ConfigurationDefaults.DefaultDetachedHeadLabel);
                    string currentGitBranchName = GitHelper.GetCurrentGitBranchName(
                        detachedHeadLabel: detachedHeadLabel,
                        repositoryRootDirectoryPath: repositoryRootDirectoryPath);
                    string defaultGitBranchName = AdditionalTextOptionParser.GetValue(
                        options: dictionary,
                        key: ConfigurationKeys.DefaultGitBranchName,
                        defaultValue: ConfigurationDefaults.DefaultDefaultGitBranchName);
                    if (!currentGitBranchName.Equals(
                        value: defaultGitBranchName,
                        comparisonType: System.StringComparison.Ordinal))
                    {
                        semanticVersion += '-'
                            + currentGitBranchName;
                    }
                    string dirtyMark = AdditionalTextOptionParser.GetValue(
                        options: dictionary,
                        key: ConfigurationKeys.DirtyMark,
                        defaultValue: ConfigurationDefaults.DefaultDirtyMark);
                    string brokenMark = AdditionalTextOptionParser.GetValue(
                        options: dictionary,
                        key: ConfigurationKeys.BrokenMark,
                        defaultValue: ConfigurationDefaults.DefaultBrokenMark);
                    string invalidHeadLabel = AdditionalTextOptionParser.GetValue(
                        options: dictionary,
                        key: ConfigurationKeys.InvalidHeadLabel,
                        defaultValue: ConfigurationDefaults.DefaultInvalidHeadLabel);
                    GitReferenceType gitReferenceType = ConfigurationDefaults.DefaultGitReferenceType;
                    if (AdditionalTextOptionParser.TryGetValue(
                        options: dictionary,
                        key: ConfigurationKeys.GitReferenceType,
                        result: out GitReferenceType parsedGitReferenceType))
                    {
                        gitReferenceType = parsedGitReferenceType;
                    }
                    int candidateAmount = ConfigurationDefaults.DefaultCandidateAmount;
                    if (AdditionalTextOptionParser.TryGetValue(
                        options: dictionary,
                        key: ConfigurationKeys.CandidateAmount,
                        result: out int? parsedCandidateAmount))
                    {
                        candidateAmount = parsedCandidateAmount.Value;
                    }
                    string abbrevLength = ConfigurationDefaults.DefaultAbbrevLength;
                    if (AdditionalTextOptionParser.TryGetValue(
                        options: dictionary,
                        key: ConfigurationKeys.AbbrevLength,
                        result: out int? parsedAbbrevLength))
                    {
                        abbrevLength = parsedAbbrevLength.Value.ToString(
                            provider: CultureInfo.InvariantCulture);
                    }
                    ParentCommitType parentCommitType = ConfigurationDefaults.DefaultParentCommitType;
                    if (AdditionalTextOptionParser.TryGetValue(
                        options: dictionary,
                        key: ConfigurationKeys.ParentCommitType,
                        result: out ParentCommitType parsedParentCommitType))
                    {
                        parentCommitType = parsedParentCommitType;
                    }
                    ReadOnlyCollection<string> matchPatterns = AdditionalTextOptionParser.GetValue(
                        options: dictionary,
                        key: ConfigurationKeys.MatchPatterns,
                        defaultValue: ConfigurationDefaults.DefaultMatchPatterns);
                    ReadOnlyCollection<string> excludePatterns = AdditionalTextOptionParser.GetValue(
                        options: dictionary,
                        key: ConfigurationKeys.ExcludePatterns,
                        defaultValue: ConfigurationDefaults.DefaultExcludePatterns);
                    GitTagState gitTagState = ConfigurationDefaults.DefaultGitTagState;
                    if (AdditionalTextOptionParser.TryGetValue(
                        options: dictionary,
                        key: ConfigurationKeys.GitTagState,
                        result: out GitTagState parsedGitTagState))
                    {
                        gitTagState = parsedGitTagState;
                    }
                    string verboseGitDescribe = GitHelper.GetVerboseGitDescribe(
                        dirtyMark: dirtyMark,
                        brokenMark: brokenMark,
                        invalidHeadLabel: invalidHeadLabel,
                        gitReferenceType: gitReferenceType,
                        candidateAmount: candidateAmount,
                        abbrevLength: abbrevLength,
                        parentCommitType: parentCommitType,
                        matchPatterns: matchPatterns,
                        excludePatterns: excludePatterns,
                        gitTagState: gitTagState,
                        gitRepositoryRootDirectoryPath: repositoryRootDirectoryPath)
                        // Git check-ref-format & git rev-parse use forward slashes, but Semantic Versioning 2.0.0 does not.
                        .Replace(
                            oldChar: '/',
                            newChar: '-');
                    semanticVersion += '+'
                        + verboseGitDescribe;
                    string semanticVersionSuffix = AdditionalTextOptionParser.GetValue(
                        options: dictionary,
                        key: ConfigurationKeys.SemanticVersionSuffix,
                        defaultValue: ConfigurationDefaults.DefaultSemanticVersionSuffix);
                    semanticVersion += semanticVersionSuffix;
                    // The semantic version will be used in a verbatim string literal, so double quotation marks must be escaped.
                    semanticVersion = semanticVersion.Replace(
                        oldValue: "\"",
                        newValue: "\"\"");
                    string generatedFileName = ConfigurationDefaults.DefaultGeneratedFileName;
                    if (AdditionalTextOptionParser.TryGetValue(
                        options: dictionary,
                        key: ConfigurationKeys.GeneratedFileName,
                        result: out string parsedGeneratedFileName)
                        && !string.IsNullOrWhiteSpace(
                            value: parsedGeneratedFileName))
                    {
                        generatedFileName = parsedGeneratedFileName;
                    }
                    string generatedNamespace = ConfigurationDefaults.DefaultGeneratedNamespace;
                    if (AdditionalTextOptionParser.TryGetValue(
                        options: dictionary,
                        key: ConfigurationKeys.GeneratedNamespace,
                        result: out string parsedGeneratedNamespace)
                        && !string.IsNullOrWhiteSpace(
                            value: parsedGeneratedNamespace))
                    {
                        generatedNamespace = parsedGeneratedNamespace;
                    }
                    string generatedTypeName = ConfigurationDefaults.DefaultGeneratedTypeName;
                    if (AdditionalTextOptionParser.TryGetValue(
                        options: dictionary,
                        key: ConfigurationKeys.GeneratedTypeName,
                        result: out string parsedGeneratedTypeName)
                        && !string.IsNullOrWhiteSpace(
                            value: parsedGeneratedTypeName))
                    {
                        generatedTypeName = parsedGeneratedTypeName;
                    }
                    string generatedFieldName = ConfigurationDefaults.DefaultGeneratedFieldName;
                    if (AdditionalTextOptionParser.TryGetValue(
                        options: dictionary,
                        key: ConfigurationKeys.GeneratedFieldName,
                        result: out string parsedGeneratedFieldName)
                        && !string.IsNullOrWhiteSpace(
                            value: parsedGeneratedFieldName))
                    {
                        generatedFieldName = parsedGeneratedFieldName;
                    }
                    AssemblyName assemblyName = typeof(VerboseSourceRevisionIdGenerator).Assembly.GetName();
                    string toolName = assemblyName.Name;
                    string toolVersion = assemblyName.Version.ToString(
                        fieldCount: 4);
                    return (semanticVersion, generatedFileName, generatedNamespace, generatedTypeName, generatedFieldName, toolName, toolVersion, generatorClassName: nameof(VerboseSourceRevisionIdGenerator));
                });
            context.RegisterSourceOutput(
                source: configurationProvider,
                action: (sourceProductionContext, configuration) =>
                {
                    sourceProductionContext.AddSource(
                        hintName: configuration.generatedFileName,
                        sourceText: SourceText.From(
                            text: GeneratorHelper.MakeAutoGeneratedCodeHeader(
                                toolName: configuration.toolName,
                                toolVersion: configuration.toolVersion)
                                + "\n// Copyright SwifterTheDragon, 2025. All Rights Reserved.\n\n"
                                + "using System.CodeDom.Compiler;\n\n"
                                + "namespace "
                                + configuration.generatedNamespace
                                + "\n{\n"
                                + "    [GeneratedCode(\n"
                                + "        tool: \""
                                + configuration.toolName
                                + "\",\n"
                                + "        version: \""
                                + configuration.toolVersion
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
                                    toolName: configuration.toolName,
                                    generatorClassName: configuration.generatorClassName),
                            encoding: Encoding.UTF8));
                });
        }
        #endregion Methods
    }
}
