// Copyright SwifterTheDragon and the SwifterTheDragon.VerboseSourceRevisionIdBuilder contributors, 2024-2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core
{
    /// <summary>
    /// Specifies if tags predating or containing the commit should be used.
    /// </summary>
    internal enum GitTagState
    {
        /// <summary>
        /// The default value.
        /// This should never be used intentionally.
        /// </summary>
        None = 0,
        /// <summary>
        /// Only use tags that predate <c>HEAD</c>.
        /// </summary>
        PredatesHead = 1,
        /// <summary>
        /// Only use tags that contain <c>HEAD</c>.
        /// </summary>
        ContainsHead = 2
    }
}
