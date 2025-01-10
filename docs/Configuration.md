# Configuration

This source generator can be configured via an [AdditionalFile](https://github.com/dotnet/roslyn/blob/main/docs/analyzers/Using%20Additional%20Files.md) named "`VerboseSourceRevisionIdConfig.txt`".

The configuration files are in an INI-like format.
They are read one line at a time, from beginning to end. For each line:

1. All leading and trailing white space is trimmed.
2. The remaining text is processed as specified for the line type below.

The types of lines are:

- Blank: Contains nothing. Blank lines are ignored.
- Comment: Starts with a `#` or a `;`. Comment lines are ignored.
- Key-Value Pair (or Pair): Contains a key and a value, separated by an `=`.
    - Key: The part before the first `=` on the line.
    - Value: The part, if any, after the first `=` on the line.
    - Keys and values are trimmed of leading and trailing white space, but include any white space that is between non-white space characters.
    - If a value is not provided, then the value is an empty string (i.e., `""`).

Any line that is not one of the above is invalid.

UTF-8 encoding must be used, with LF or CRLF line separators.

# Supported Pairs

Here is a list of all keys defined by this version, and the supported values associated with them:

| Key | Description | Supported values | Default value |
| --- | --- | --- | --- |
| `MajorVersionLabel` | The semantic version major version label. | An integer value above `-1`. | `0` |
| `MinorVersionLabel` | The semantic version minor version label. | An integer value above `-1`. | `1`, unless the value at the key of `MajorVersionLabel` is `0`, wherein `1` will be used instead. |
| `PatchVersionLabel` | The semantic version patch version label. | An integer value above `-1`. | `0` |
| `Prefix` | A prefix for the verbose source revision ID. | A string value. | An empty string, `""`. |
| `Suffix` | A suffix string for the verbose source revision ID. | A string value. | An empty string, `""`. |
| `RepositoryRootDirectoryRelativeToConfigurationFilePath` | The file path to the repository's root directory relative to the configuration file. | A relative file path. | An empty string, `""`. |
| `DirtyMark` | The mark passed to the `--dirty[=<mark>]` argument for labelling a working tree with local modification, used in the semantic version build metadata label. | A string value. | `-dirty` |
| `BrokenMark` | The mark passed to the `--broken[=<mark>]` argument for labelling a corrupt repository, used in the semantic version build metadata label. | A string value. | `-broken` |
| `DetachedHeadLabel` | A detached `HEAD` state will be labelled in the semantic version pre-release label with this value, such as anonymous branches. | A string value. | `DETACHED-HEAD` |
| `InvalidHeadLabel` | An invalid `HEAD` state will be labelled with this value in the semantic version build metadata label with this value, such as unborn branches. | A string value. | `INVALID-HEAD` |
| `DefaultGitBranchName` | The name of the default Git branch name for the repository, causing the semantic version pre-release label to be omitted if the current Git branch name matches this value. | A string value. | `main` |
| `GitReferenceType` | Specifies which reference types can be used to describe `HEAD` with, used in the semantic version build metadata label. | "`AnnotedTags`", "`Tags`", and "`All`". Case-insensitive. | "`All`" |
| `Candidates` | Specifies the amount of most recent tags to describe `HEAD` with, used in the semantic version build metadata label. | An integer value above `-1`. | `10` |
| `AbbrevLength` | Specifies the amount of hexadecimal digits of the abbreviated object name to describe `HEAD` with, used in the semantic version build metadata label. | An integer value between `4` through `40` (inclusive), `0` to suppress the `--long` argument, or "`Default`" for default, dynamic length behaviour. | "`Default`" |
| `ParentCommitType` | Specifies if the `--first-only` argument should be included or omitted, controlling if only the first parent of a merge commit should be followed. | "`Any`", and "`FirstOnly`". Case-insensitive. | "`Any`" |
| `GitTagState` | Specifies if the `--contains` argument should be included or omitted, controlling if only tags containing `HEAD` should be used, or only tags predating `HEAD`. | "`PredatesHead`", and "`ContainsHead`". Case-insensitive. | `PredatesCommit` |
| `MatchPatterns` | A collection of patterns for the `--match <pattern>` argument, specifying glob patterns that referneces must match. | String values, separated by `, `. | An empty string, `""`. |
| `ExcludePatterns` | A collection of patterns for the `--exclude <pattern>` argument, specigying glob patterns that references must not match. When combined with the `--match <pattern>` argument, references matching at least one `--match` pattern and do not match any `--exclude` patterns will be used. | String values, separated by `, `. | An empty string, `""`. |
| `GeneratedFileName` | The name of the file the verbose source revision ID will be placed in. | A file name. | "`VerboseSourceRevisionIdBuilder.Generated.cs`" |
| `GeneratedNamespace` | The namespace the verbose source revision ID will be placed in. | A namespace. | "`SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core`" |
| `GeneratedTypeName` | The name of the type the verbose source revision ID will be placed in. | A type name. | "`VerboseSourceRevisionIdBuilder`" |
| `GeneratedFieldName` | The name of the field the verbose source revision ID will be placed in. | A field name. | "`VerboseSourceRevisionId`" |

---

Copyright SwifterTheDragon, 2024-2025. All Rights Reserved.
