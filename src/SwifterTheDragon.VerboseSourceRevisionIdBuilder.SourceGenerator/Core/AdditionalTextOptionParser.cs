// Copyright SwifterTheDragon, 2024-2025. All Rights Reserved.
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
            // This diagnostic only shows up in build output logs, for reasons unknown.
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options,
#pragma warning restore S3242 // Method parameters should be declared with base types
            string key,
            out int? result)
        {
            result = null;
            if (options is null
                || string.IsNullOrWhiteSpace(
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
            if (options is null
                || string.IsNullOrWhiteSpace(
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
            // This diagnostic only shows up in build output logs, for reasons unknown.
#pragma warning disable S3242 // Method parameters should be declared with base types
            Dictionary<string, string> options,
#pragma warning restore S3242 // Method parameters should be declared with base types
            string key,
            out TEnum result) where TEnum : struct, Enum
        {
            result = default;
            if (options is null
                || string.IsNullOrWhiteSpace(
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
            if (equalsSeparatedParts.Length < 3)
            {
                string value = equalsSeparatedParts[1].TrimStart();
                if (string.IsNullOrWhiteSpace(
                    value: value))
                {
                    return false;
                }
                parsedKey = key.ToUpperInvariant();
                parsedValue = value;
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
            parsedKey = key.ToUpperInvariant();
            parsedValue = equalsSeparatedParts[1].TrimStart() + remainingValueBuilder;
            return true;
        }
        #endregion Methods
    }
}
