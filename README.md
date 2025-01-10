# VerboseSourceRevisionIdBuilder

Gives a more verbose source revision ID than what Source Link offers and exposes it via an incremental source generator so it can be used in AssemblyInformationalVersionAttributes without MSBuild, allowing use in Unity projects.

# Format

This repository provides a valid [semantic version](https://semver.org/spec/v2.0.0.html) string as a constant field.

`Major.Minor.Patch+BuildMetadata` is used on the default Git branch
and `Major.Minor.Patch-PreRelease+BuildMetadata` is used on non-default Git branches.

The main semantic version labels (`major`, `minor`, and `patch`) are always configured manually.

The pre-release label is only used is the current Git branch is not the default,
and is filled in by the output of `git branch --show-current`.

A detached `HEAD` label will be used instead in the event that the current branch cannot be named.

The build metadata label is always used,
and is filled in by the output of a verbose Git describe command.

`git describe --always` will be used instead in the event that Git describe command fails.

If `git describe --always` also fails, an invalid `HEAD` label will be used instead.

Since Git describe can output characters that are invalid for Semantic Versioning,
they are replaced with alternative representations:

- A build metadata label that would have started with '`.`' becomes "`-Period-`".
- '`/`' becomes "`-ForwardSlash-`".
- '`~`' becomes "`-Tilde-`".
- '`^`' becomes "`-Caret-`".

# Configuration

See [Configuration.md](./docs/Configuration.md) for more details.

# Example

`1.2.3+v1.2.3-0-g8f9a0b1c`

---

Copyright SwifterTheDragon, 2024-2025. All Rights Reserved.
