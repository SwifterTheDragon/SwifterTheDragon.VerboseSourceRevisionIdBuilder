// Copyright SwifterTheDragon, 2024. All Rights Reserved.

using System;
using System.Diagnostics;

namespace SwifterTheDragon.VerboseSourceRevisionIdBuilder.GitUtilities.Core
{
    /// <summary>
    /// Provides command line utilities, such as executing a command.
    /// </summary>
    internal static class CommandLineUtilities
    {
        #region Methods
        /// <summary>
        /// Executes a command through <c>cmd.exe</c>.
        /// </summary>
        /// <param name="command">The command to be passed to <c>cmd.exe</c>.</param>
        /// <returns>The output of the command that was executed.</returns>
        internal static string ExecuteCommandLineCommand(
            string command)
        {
            if (string.IsNullOrWhiteSpace(
                value: command))
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
                    WorkingDirectory = Environment.CurrentDirectory
                };
                _ = cmdProcess.Start();
                output = cmdProcess.StandardOutput.ReadToEnd().TrimEnd();
                cmdProcess.WaitForExit();
            }
            return output;
        }
        #endregion Methods
    }
}
