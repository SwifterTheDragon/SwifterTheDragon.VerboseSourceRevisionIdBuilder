// Copyright SwifterTheDragon, and the SwifterTheDragon.VerboseSourceRevisionIdBuilder contributors, 2024-2025. All Rights Reserved.
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
    /// <include
    /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
    /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="AdditionalTextOptionParser"]/Description/*'/>
    internal static class AdditionalTextOptionParser
    {
        #region Fields & Properties
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="AdditionalTextOptionParser"]/Field[@name="s_defaultSeparators"]/*'/>
        private static readonly string[] s_defaultSeparators = new[]
        {
            ", "
        };
        #endregion Fields & Properties
        #region Methods
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="AdditionalTextOptionParser"]/Method[@name="ParseOptions(Microsoft.CodeAnalysis.AdditionalText,System.Threading.CancellationToken)"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="AdditionalTextOptionParser"]/Method[@name="GetValue(System.Collections.Generic.Dictionary{System.String,System.String},System.String,System.Collections.ObjectModel.ReadOnlyCollection{System.String})"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="AdditionalTextOptionParser"]/Method[@name="GetValue(System.Collections.Generic.Dictionary{System.String,System.String},System.String,System.String)"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="AdditionalTextOptionParser"]/Method[@name="GetValue(System.Collections.Generic.Dictionary{System.String,System.String},System.String,int)"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="AdditionalTextOptionParser"]/Method[@name="GetValue``1(System.Collections.Generic.Dictionary{System.String,System.String},System.String,``0)"]/*'/>
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
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="AdditionalTextOptionParser"]/Method[@name="TryParseLine(System.String,System.String@,System.String@)"]/*'/>
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
