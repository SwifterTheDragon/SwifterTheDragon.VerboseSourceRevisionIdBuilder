// Copyright SwifterTheDragon and the SwifterTheDragon.VerboseSourceRevisionIdBuilder contributors, 2024-2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

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
            TextLineCollection textLines = additionalText.GetText(
                cancellationToken: cancellationToken).Lines;
            if (textLines.Count is 0)
            {
                return output;
            }
            foreach (TextLine textLine in textLines)
            {
                if (!TryParseLine(
                    input: textLine.ToString(),
                    out string parsedKey,
                    out string parsedValue))
                {
                    continue;
                }
                if (output.ContainsKey(
                    key: parsedKey))
                {
                    output[parsedKey] = parsedValue;
                    continue;
                }
                output.Add(
                    key: parsedKey,
                    value: parsedValue);
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
            // This diagnostic only shows up in build output logs, for reasons unknown.
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options,
#pragma warning restore S3242 // Method parameters should be declared with base types
            string key,
            ReadOnlyCollection<string> defaultValue)
        {
            if (options is null
                || options.Count is 0
                || string.IsNullOrWhiteSpace(
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
            // This diagnostic only shows up in build output logs, for reasons unknown.
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options,
#pragma warning restore S3242 // Method parameters should be declared with base types
            string key,
            string defaultValue)
        {
            if (options is null
                || options.Count is 0
                || string.IsNullOrWhiteSpace(
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
        /// Retrieves an integer from <c><paramref name="options"/></c> at
        /// <c><paramref name="key"/></c>.
        /// </summary>
        /// <param name="options">
        /// The options to retrieve the value from.
        /// </param>
        /// <param name="key">
        /// The key to retrieve the value with.
        /// </param>
        /// <param name="defaultValue">
        /// The default value to use instead if <c><paramref name="key"/></c>
        /// does not exist in <c><paramref name="options"/></c>.
        /// </param>
        /// <returns>
        /// The value stored at <c><paramref name="key"/></c> in
        /// <c><paramref name="options"/></c>.
        /// If no such value exists, <c><paramref name="defaultValue"/></c> is
        /// used instead.
        /// </returns>
        internal static int GetValue(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options,
#pragma warning restore S3242 // Method parameters should be declared with base types
            string key,
            int defaultValue)
        {
            if (options is null
                || options.Count is 0
                || string.IsNullOrWhiteSpace(
                    value: key))
            {
                return defaultValue;
            }
            if (options.TryGetValue(
                key: key.ToUpperInvariant(),
                value: out string parsedValue)
                && int.TryParse(
                    s: parsedValue,
                    style: NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out int desiredValue))
            {
                return desiredValue;
            }
            return defaultValue;
        }
        /// <summary>
        /// Retrieves a <c><typeparamref name="TEnum"/></c>
        /// from <c><paramref name="options"/></c> at
        /// <c><paramref name="key"/></c>.
        /// </summary>
        /// <typeparam name="TEnum">
        /// The <c><see langword="enum"/></c> type to parse the value as.
        /// </typeparam>
        /// <param name="options">
        /// The options to retrieve the value from.
        /// </param>
        /// <param name="key">
        /// The key to retrieve the value with.
        /// </param>
        /// <param name="defaultValue">
        /// The default value to use instead if <c><paramref name="key"/></c>
        /// does not exist in <c><paramref name="options"/></c>.
        /// </param>
        /// <returns>
        /// The value stored at <c><paramref name="key"/></c> in
        /// <c><paramref name="options"/></c>.
        /// If no such value exists, <c><paramref name="defaultValue"/></c> is
        /// returned instead.
        /// </returns>
        internal static TEnum GetValue<TEnum>(
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options,
#pragma warning restore S3242 // Method parameters should be declared with base types
            string key,
            TEnum defaultValue) where TEnum : struct, Enum
        {
            if (options is null
                || options.Count is 0
                || string.IsNullOrWhiteSpace(
                    value: key))
            {
                return defaultValue;
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
                return defaultValue;
            }
            return desiredValue;
        }
        /// <summary>
        /// Try to parse a <c><paramref name="parsedKey"/></c> and
        /// <c><paramref name="parsedValue"/></c> from
        /// <c><paramref name="input"/></c>.
        /// </summary>
        /// <param name="input">
        /// The line to parse a key and value from.
        /// </param>
        /// <param name="parsedKey">
        /// The resulting key, if successful.
        /// </param>
        /// <param name="parsedValue">
        /// The resulting value, if successful.
        /// </param>
        /// <returns>
        /// <c><see langword="null"/></c> if <c><paramref name="input"/></c> is
        /// null, whitespace, begins with '<c>#</c>' or '<c>;</c>', lacks both a
        /// key and a value, or if the key itself is whitespace. Otherwise,
        /// <c><paramref name="parsedKey"/></c> is the parsed case-insensitive
        /// key, and <c><paramref name="parsedValue"/></c> is the case-sensitive
        /// value.
        /// </returns>
        private static bool TryParseLine(
            string input,
            out string parsedKey,
            out string parsedValue)
        {
            parsedKey = null;
            parsedValue = null;
            if (string.IsNullOrWhiteSpace(
                value: input))
            {
                return false;
            }
            string trimmedLine = input.Trim();
            if (trimmedLine.StartsWith(
                value: "#",
                comparisonType: StringComparison.Ordinal)
                || trimmedLine.StartsWith(
                    value: ";",
                    comparisonType: StringComparison.Ordinal))
            {
                return false;
            }
            string[] equalsSeparatedParts = trimmedLine.Split(
                separator: '=');
            if (equalsSeparatedParts.Length < 2)
            {
                return false;
            }
            string key = equalsSeparatedParts[0].TrimEnd();
            if (string.IsNullOrWhiteSpace(
                value: key))
            {
                return false;
            }
            parsedKey = key.ToUpperInvariant();
            parsedValue = equalsSeparatedParts[1].TrimStart();
            if (equalsSeparatedParts.Length < 3)
            {
                return true;
            }
            string[] valueParts = new string[equalsSeparatedParts.Length - 2];
            const int RemainingValuePartsStartingIndex = 2;
            Array.Copy(
                sourceArray: equalsSeparatedParts,
                sourceIndex: RemainingValuePartsStartingIndex,
                destinationArray: valueParts,
                destinationIndex: valueParts.GetLowerBound(
                    dimension: 0),
                length: equalsSeparatedParts.Length - RemainingValuePartsStartingIndex);
            var remainingValueBuilder = new StringBuilder();
            foreach (string valuePart in valueParts)
            {
                remainingValueBuilder.Append(
                    value: '=')
                    .Append(
                        value: valuePart);
            }
            parsedValue += remainingValueBuilder;
            return true;
        }
        #endregion Methods
    }
}
