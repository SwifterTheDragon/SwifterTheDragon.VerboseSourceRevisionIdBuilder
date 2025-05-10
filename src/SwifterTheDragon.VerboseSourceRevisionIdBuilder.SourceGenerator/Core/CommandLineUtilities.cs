// Copyright SwifterTheDragon, and the SwifterTheDragon.VerboseSourceRevisionIdBuilder contributors, 2024-2025. All Rights Reserved.
// SPDX-License-Identifier: MIT

using System.Diagnostics;
using System.IO;

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.Core
{
    /// <include
    /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
    /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="CommandLineUtilities"]/Description/*'/>
    internal static class CommandLineUtilities
    {
        #region Methods
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="CommandLineUtilities"]/Method[@name="ExecuteCommandLineCommand(System.String,System.String)"]/*'/>
        internal static string ExecuteCommandLineCommand(
            string command,
            string directory)
        {
            if (string.IsNullOrWhiteSpace(
                value: command))
            {
                return string.Empty;
            }
            if (string.IsNullOrWhiteSpace(
                value: directory))
            {
                return string.Empty;
            }
            if (!TryGetWorkingDirectory(
                path: directory,
                out string workingDirectory))
            {
                return string.Empty;
            }
            string output = string.Empty;
            using (var cmdProcess = new Process())
            {
                cmdProcess.StartInfo = new ProcessStartInfo(
                    fileName: "cmd.exe",
                    arguments: "/c "
                        + command)
                {
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    WorkingDirectory = workingDirectory
                };
                _ = cmdProcess.Start();
#pragma warning disable MA0045 // Do not use blocking calls in a sync method (need to make calling method async)
                output = cmdProcess.StandardOutput.ReadToEnd().TrimEnd();
#pragma warning restore MA0045 // Do not use blocking calls in a sync method (need to make calling method async)
                cmdProcess.WaitForExit();
            }
            return output;
        }
        /// <include
        /// file='../../docs/SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator.xml'
        /// path='Assembly[@name="SwifterTheDragon.VerboseSourceRevisionIdBuilder.SourceGenerator"]/Namespace[@name="Core"]/Type[@name="CommandLineUtilities"]/Method[@name="TryGetWorkingDirectory(System.String,System.String@)"]/*'/>
        private static bool TryGetWorkingDirectory(
            string path,
            out string workingDirectory)
        {
            workingDirectory = string.Empty;
            if (Directory.Exists(
                path: path))
            {
                workingDirectory = path;
                return true;
            }
            string directoryName = Path.GetDirectoryName(
                path: path);
            if (!Directory.Exists(
                path: directoryName))
            {
                return false;
            }
            workingDirectory = directoryName;
            return true;
        }
        #endregion Methods
    }
}
