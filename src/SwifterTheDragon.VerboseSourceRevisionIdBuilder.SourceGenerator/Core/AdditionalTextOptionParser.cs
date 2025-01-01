// Copyright SwifterTheDragon, 2025. All Rights Reserved.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core
{
    /// <summary>
    /// Provides utilities for AdditionalTexts, such as parsing key-value pairs.
    /// </summary>
    internal static class AdditionalTextOptionParser
    {
        #region Fields & Properties
        private static readonly string[] s_defaultSeparators = new string[]
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
            AdditionalText additionalText)
        {
            if (additionalText is null)
            {
                return null;
            }
            var output = new Dictionary<string, string>();
            foreach (TextLine textLine in additionalText.GetText().Lines)
            {
                string line = textLine.ToString().Trim();
                if (string.IsNullOrWhiteSpace(
                    value: line))
                {
                    continue;
                }
                if (line.StartsWith(
                    value: ";",
                    comparisonType: StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }
                if (line.StartsWith(
                    value: "#",
                    comparisonType: StringComparison.InvariantCultureIgnoreCase))
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
                    for (int equalsSeparatedValueIndex = 2; equalsSeparatedValueIndex < equalsSeparatedValues.Length; equalsSeparatedValueIndex++)
                    {
                        dirtyConfigurationValue += '='
                            + equalsSeparatedValues[equalsSeparatedValueIndex];
                    }
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
        /// Retrieves a collection of strings from
        /// <c><paramref name="options"/></c> at <c><paramref name="key"/></c>.
        /// </summary>
        /// <param name="options">
        /// The options to retrieve the value from.
        /// </param>
        /// <param name="key">
        /// The key to retrieve the value with.
        /// </param>
        /// <param name="defaultValue">
        /// The default value to use in the event that
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
            Dictionary<string, string> options,
            string key,
            ReadOnlyCollection<string> defaultValue)
        {
            if (options is null)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(
                value: key))
            {
                return null;
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
        /// The default value to use in the event that
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
            Dictionary<string, string> options,
            string key,
            string defaultValue)
        {
            if (options is null)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(
                value: key))
            {
                return null;
            }
            if (options.TryGetValue(
                key: key.ToUpperInvariant(),
                value: out string parsedValue))
            {
                return parsedValue;
            }
            return defaultValue;
        }
        internal static bool GetValue(
            Dictionary<string, string> options,
            string key,
            bool defaultValue)
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
                value: out string parsedValue)
                && bool.TryParse(
                    value: parsedValue,
                    result: out bool desiredValue))
            {
                return desiredValue;
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
        /// <c>true</c> if a valid value was found at
        /// <c><paramref name="key"/></c> in <c><paramref name="options"/></c>.
        /// Otherwise, <c>false</c>.
        /// </returns>
        internal static bool TryGetValue(
            Dictionary<string, string> options,
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
            if (options.TryGetValue(
                key: key.ToUpperInvariant(),
                value: out string parsedValue)
                && int.TryParse(
                    s: parsedValue,
                    style: NumberStyles.Integer,
                    provider: CultureInfo.InvariantCulture,
                    result: out int desiredValue))
            {
                result = desiredValue;
                return true;
            }
            return false;
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
        /// <c>true</c> if a valid value was found at
        /// <c><paramref name="key"/></c> in <c><paramref name="options"/></c>.
        /// Otherwise, <c>false</c>.
        /// </returns>
        internal static bool TryGetValue(
            Dictionary<string, string> options,
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
            if (options.TryGetValue(
                key: key.ToUpperInvariant(),
                value: out string parsedValue))
            {
                result = parsedValue;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Attempts to retrieve a value from <c><paramref name="options"/></c>
        /// at <c><paramref name="key"/></c>.
        /// </summary>
        /// <typeparam name="T">
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
        /// <c>true</c> if a valid value was found at
        /// <c><paramref name="key"/></c> in <c><paramref name="options"/></c>.
        /// Otherwise, <c>false</c>.
        /// </returns>
        internal static bool TryGetValue<T>(
            Dictionary<string, string> options,
            string key,
            out T result) where T : struct, Enum
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
            if (options.TryGetValue(
                key: key.ToUpperInvariant(),
                value: out string parsedValue)
                && Enum.TryParse(
                    value: parsedValue,
                    ignoreCase: true,
                    result: out T desiredValue)
                && !desiredValue.Equals(
                    obj: default)
                && Enum.IsDefined(
                    enumType: typeof(T),
                    value: desiredValue))
            {
                result = desiredValue;
                return true;
            }
            return false;
        }
        #endregion Methods
    }
}
