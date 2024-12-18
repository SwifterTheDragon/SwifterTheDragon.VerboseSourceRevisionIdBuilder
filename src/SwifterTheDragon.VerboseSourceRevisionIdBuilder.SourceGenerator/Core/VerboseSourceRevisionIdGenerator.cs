// Copyright SwifterTheDragon, 2024. All Rights Reserved.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core
{
    /// <summary>
    /// Generates a single file providing the current verbose source revision ID.
    /// </summary>
    [Generator]
    internal class VerboseSourceRevisionIdGenerator : IIncrementalGenerator
    {
        #region Fields & Properties
        /// <summary>
        /// The default name of the generated file containing the verbose source revision ID.
        /// </summary>
        private string DefaultGeneratedFileName
        {
            get
            {
                return "VerboseSourceRevisionIdBuilder.Generated.cs";
            }
        }
        /// <summary>
        /// The default name of the generated namespace containing the verbose source revision ID.
        /// </summary>
        private string DefaultGeneratedNamespace
        {
            get
            {
                return "SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core";
            }
        }
        /// <summary>
        /// The default name of the generated class containing the verbose source revision ID.
        /// </summary>
        private string DefaultGeneratedClassName
        {
            get
            {
                return "VerboseSourceRevisionIdBuilder";
            }
        }
        /// <summary>
        /// The default name of the generated field containing the verbose source revision ID.
        /// </summary>
        private string DefaultFieldName
        {
            get
            {
                return "VerboseSourceRevisionId";
            }
        }
        #endregion Fields & Properties
        #region Methods
        public void Initialize(
            IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(
                callback: postInitializationContext =>
            {
                postInitializationContext.AddSource(
                    hintName: DefaultGeneratedFileName,
                    sourceText: SourceText.From(
                        text: @"// Copyright SwifterTheDragon, 2024. All Rights Reserved.

namespace "
                            + DefaultGeneratedNamespace
                            + @"
{
    internal static class "
                            + DefaultGeneratedClassName
                            + @"
    {
        internal const string "
                            + DefaultFieldName
                            + @" = ""1.2.3"";
    }
}
",
                        encoding: Encoding.UTF8));
            });
        }
        #endregion
    }
}
