// Copyright SwifterTheDragon, 2025. All Rights Reserved.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading;

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core
{
    /// <summary>
    /// Provides utilities for AdditionalTexts, such as parsing key-value pairs.
    /// </summary>
    internal static class AdditionalTextOptionParser
    {
        #region Fields & Properties
        /// <summary>
        /// The default separators to split a collection of values with.
        /// </summary>
        private static readonly string[] s_defaultSeparators = new[]
        {
            ", "
        };
        #endregion Fields & Properties
        #region Methods
        /// <summary>
        /// Parses a collection of case-insensitive keys and
        /// case-sensitive values from <c><paramref name="additionalText"/></c>.
        /// </summary>
        /// <param name="additionalText">
        /// The file to parse options from.
        /// </param>
        /// <param name="cancellationToken">
        /// Propagates notification that operations should be cancelled.
        /// </param>
        /// <example>
        /// <c>   Key1  =  = Value1 </c> would be parsed as "key1"
        /// being the key with "= Value1" being the value.
        /// </example>
        /// <returns>
        /// A collection of case-insensitive keys and case-sensitive values
        /// parsed from <c><paramref name="additionalText"/></c>.
        /// Leading and trailing whitespace is trimmed from
        /// both keys and values before parsing.
        /// </returns>
        internal static Dictionary<string, string> ParseOptions(
            AdditionalText additionalText,
            CancellationToken cancellationToken)
        {
            var output = new Dictionary<string, string>(
                comparer: StringComparer.Ordinal);
            if (additionalText is null)
            {
                return output;
            }
            foreach (TextLine textLine in additionalText.GetText(
                cancellationToken: cancellationToken).Lines)
            {
                string line = textLine.ToString().Trim();
                if (IsEmptyOrCommentLine(
                    line: line))
                {
                    continue;
                }
                string[] equalsSeparatedValues = line.Split(
                    separator: '=');
                if (equalsSeparatedValues.Length < 2)
                {
                    continue;
                }
                string configurationKey = equalsSeparatedValues[0].TrimEnd().ToUpperInvariant();
                if (string.IsNullOrWhiteSpace(
                    value: configurationKey))
                {
                    continue;
                }
                string dirtyConfigurationValue = equalsSeparatedValues[1];
                if (equalsSeparatedValues.Length > 2)
                {
                    var fullDirtyConfigurationValueBuilder = new StringBuilder();
                    for (
                        int equalsSeparatedValueIndex = 2;
                        equalsSeparatedValueIndex < equalsSeparatedValues.Length;
                        equalsSeparatedValueIndex++)
                    {
                        fullDirtyConfigurationValueBuilder.Append(
                            value: '=')
                            .Append(
                                value: equalsSeparatedValues[equalsSeparatedValueIndex]);
                    }
                    dirtyConfigurationValue += fullDirtyConfigurationValueBuilder.ToString();
                }
                string configurationValue = dirtyConfigurationValue.TrimStart();
                if (output.ContainsKey(
                    key: configurationKey))
                {
                    output[configurationKey] = configurationValue;
                    continue;
                }
                output.Add(
                    key: configurationKey,
                    value: configurationValue);
            }
            return output;
        }
        /// <summary>
        /// Retrieves a <c><see cref="ReadOnlyCollection{T}"/></c> of <see langword="string"/>s from
        /// <c><paramref name="options"/></c> at <c><paramref name="key"/></c>.
        /// </summary>
        /// <param name="options">
        /// The options to retrieve the value from.
        /// </param>
        /// <param name="key">
        /// The key to retrieve the value with.
        /// </param>
        /// <param name="defaultValue">
        /// The default value to use instead if
        /// <c><paramref name="key"/></c> does not exist in
        /// <c><paramref name="options"/></c>.
        /// </param>
        /// <returns>
        /// The value stored at <c><paramref name="key"/></c> within
        /// <c><paramref name="options"/></c>.
        /// If no such value exists, <c><paramref name="defaultValue"/></c> is
        /// used instead.
        /// </returns>
        internal static ReadOnlyCollection<string> GetValue(
            IDictionary<string, string> options,
            string key,
            ReadOnlyCollection<string> defaultValue)
        {
            if (options is null)
            {
                return defaultValue;
            }
            if (string.IsNullOrWhiteSpace(
                value: key))
            {
                return defaultValue;
            }
            if (options.TryGetValue(
                key: key.ToUpperInvariant(),
                value: out string parsedValue))
            {
                return new ReadOnlyCollection<string>(
                    list: new List<string>(
                        collection: parsedValue.Split(
                            separator: s_defaultSeparators,
                            options: StringSplitOptions.RemoveEmptyEntries)));
            }
            return defaultValue;
        }
        /// <summary>
        /// Retrieves a string from <c><paramref name="options"/></c> at
        /// <c><paramref name="key"/></c>.
        /// </summary>
        /// <param name="options">
        /// The options to retrieve the value from.
        /// </param>
        /// <param name="key">
        /// The key to retrieve the value with.
        /// </param>
        /// <param name="defaultValue">
        /// The default value to use instead if
        /// <c><paramref name="key"/></c> does not exist in
        /// <c><paramref name="options"/></c>.
        /// </param>
        /// <returns>
        /// The value stored at <c><paramref name="key"/></c>
        /// within <c><paramref name="options"/></c>.
        /// If no such value exists,
        /// <c><paramref name="defaultValue"/></c> is returned instead.
        /// </returns>
        internal static string GetValue(
            IDictionary<string, string> options,
            string key,
            string defaultValue)
        {
            if (options is null)
            {
                return defaultValue;
            }
            if (string.IsNullOrWhiteSpace(
                value: key))
            {
                return defaultValue;
            }
            if (options.TryGetValue(
                key: key.ToUpperInvariant(),
                value: out string parsedValue))
            {
                return parsedValue;
            }
            return defaultValue;
        }
        /// <summary>
        /// Attempts to retrieve a value from <c><paramref name="options"/></c>
        /// at <c><paramref name="key"/></c>.
        /// </summary>
        /// <param name="options">
        /// The options to retrieve the value from.
        /// </param>
        /// <param name="key">
        /// The key to try and retrieve the value with.
        /// </param>
        /// <param name="result">
        /// The resulting parsed value, if successful.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if a valid value was found at
        /// <c><paramref name="key"/></c> in <c><paramref name="options"/></c>.
        /// Otherwise, <see langword="false"/>.
        /// </returns>
        internal static bool TryGetValue(
            IDictionary<string, string> options,
            string key,
            out int? result)
        {
            result = null;
            if (options is null)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(
                value: key))
            {
                return false;
            }
            if (!options.TryGetValue(
                key: key.ToUpperInvariant(),
                value: out string parsedValue)
                || !int.TryParse(
                    s: parsedValue,
                    style: NumberStyles.Integer,
                    provider: CultureInfo.InvariantCulture,
                    result: out int desiredValue))
            {
                return false;
            }
            result = desiredValue;
            return true;
        }
        /// <summary>
        /// Attempts to retrieve a value from <c><paramref name="options"/></c>
        /// at <c><paramref name="key"/></c>.
        /// </summary>
        /// <param name="options">
        /// The options to retrieve the value from.
        /// </param>
        /// <param name="key">
        /// The key to try and retrieve the value with.
        /// </param>
        /// <param name="result">
        /// The resulting parsed value, if successful.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if a valid value was found at
        /// <c><paramref name="key"/></c> in <c><paramref name="options"/></c>.
        /// Otherwise, <see langword="false"/>.
        /// </returns>
        internal static bool TryGetValue(
            IDictionary<string, string> options,
            string key,
            out string result)
        {
            result = null;
            if (options is null)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(
                value: key))
            {
                return false;
            }
            if (!options.TryGetValue(
                key: key.ToUpperInvariant(),
                value: out string parsedValue))
            {
                return false;
            }
            result = parsedValue;
            return true;
        }
        /// <summary>
        /// Attempts to retrieve a <c><typeparamref name="TEnum"/></c> from <c><paramref name="options"/></c>
        /// at <c><paramref name="key"/></c>.
        /// </summary>
        /// <typeparam name="TEnum">
        /// The Enum type to parse the value as.
        /// </typeparam>
        /// <param name="options">
        /// The options to retrieve the value from.
        /// </param>
        /// <param name="key">
        /// The key to try and retrieve the value with.
        /// </param>
        /// <param name="result">
        /// The resulting parsed value, if successful.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if a valid value was found at
        /// <c><paramref name="key"/></c> in <c><paramref name="options"/></c>.
        /// Otherwise, <see langword="false"/>.
        /// </returns>
        internal static bool TryGetValue<TEnum>(
            IDictionary<string, string> options,
            string key,
            out TEnum result) where TEnum : struct, Enum
        {
            result = default;
            if (options is null)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(
                value: key))
            {
                return false;
            }
            if (!options.TryGetValue(
                key: key.ToUpperInvariant(),
                value: out string parsedValue)
                || !Enum.TryParse(
                    value: parsedValue,
                    ignoreCase: true,
                    result: out TEnum desiredValue)
                || desiredValue.Equals(
                    obj: default)
                || !Enum.IsDefined(
                    enumType: typeof(TEnum),
                    value: desiredValue))
            {
                return false;
            }
            result = desiredValue;
            return true;
        }
        /// <summary>
        /// Determines if a string is empty or a comment.
        /// </summary>
        /// <param name="line">
        /// The line to check for being empty or a comment.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="line"/> starts with
        /// <c>#</c> or <c>;</c>, or is empty.
        /// Otherwise, <see langword="false" />.
        /// </returns>
        private static bool IsEmptyOrCommentLine(
            string line)
        {
            if (string.IsNullOrWhiteSpace(
                value: line))
            {
                return true;
            }
            if (line.StartsWith(
                value: "#",
                comparisonType: StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (line.StartsWith(
                value: ";",
                comparisonType: StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
        #endregion Methods
    }
}
