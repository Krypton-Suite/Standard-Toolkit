#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web.Script.Serialization;

namespace Krypton.Toolkit.Utilities.UpdateHelper;

/// <summary>
/// Applies a zip update after the host exits, then restarts the host when asked.
/// </summary>
internal static class Program
{
    private static int Main(string[] args)
    {
        var resultPath = string.Empty;
        try
        {
            var jobPath = GetJobPath(args);
            resultPath = jobPath + ".result";
            var job = UpdateJob.Load(jobPath);
            if (job.ProcessId > 0)
            {
                WaitForProcess(job.ProcessId, job.WaitTimeoutSeconds);
            }

            ExtractZip(job.ZipPath, job.DestinationDirectory);
            if (!string.IsNullOrWhiteSpace(job.RestartExecutable))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = job.RestartExecutable,
                    Arguments = job.RestartArguments ?? string.Empty,
                    UseShellExecute = true,
                    WorkingDirectory = job.DestinationDirectory
                });
            }

            TryDelete(job.ZipPath);
            TryDelete(jobPath);
            WriteResult(resultPath, "ok");
            return 0;
        }
        catch (Exception exception)
        {
            try
            {
                if (!string.IsNullOrEmpty(resultPath))
                {
                    WriteResult(resultPath, exception.ToString());
                }

                File.AppendAllText(
                    Path.Combine(Path.GetTempPath(), "KryptonApplicationUpdater-helper.log"),
                    DateTime.UtcNow.ToString("o") + " " + exception + Environment.NewLine);
            }
            catch (Exception)
            {
                // The host reads the result file when it is still running. A logging failure still returns the error code.
            }

            return 1;
        }
    }

    private static string GetJobPath(string[] args)
    {
        if (args.Length >= 2 && string.Equals(args[0], "--krypton-update-job", StringComparison.OrdinalIgnoreCase))
        {
            return args[1];
        }

        if (args.Length == 1)
        {
            return args[0];
        }

        throw new InvalidOperationException("Expected --krypton-update-job <path>.");
    }

    private static void WaitForProcess(int processId, int timeoutSeconds)
    {
        try
        {
            using var process = Process.GetProcessById(processId);
            var timeout = timeoutSeconds <= 0 ? 120000 : timeoutSeconds * 1000;
            if (!process.WaitForExit(timeout))
            {
                throw new TimeoutException("The application did not exit before the update helper timed out.");
            }
        }
        catch (ArgumentException)
        {
            // The host has already exited, so the install directory is free.
        }
    }

    /// <summary>
    /// Extracts <paramref name="zipPath"/> into <paramref name="destination"/> and rejects entries that escape that directory.
    /// </summary>
    private static void ExtractZip(string zipPath, string destination)
    {
        var root = Path.GetFullPath(destination);
        if (!root.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
        {
            root += Path.DirectorySeparatorChar;
        }

        Directory.CreateDirectory(root);
        using var archive = ZipFile.OpenRead(zipPath);
        foreach (var entry in archive.Entries)
        {
            var relative = entry.FullName.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
            if (string.IsNullOrWhiteSpace(relative))
            {
                continue;
            }

            var target = Path.GetFullPath(Path.Combine(root, relative));
            if (!target.StartsWith(root, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidDataException("Zip entry escapes the destination: " + entry.FullName);
            }

            if (relative.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal) || string.IsNullOrEmpty(entry.Name))
            {
                Directory.CreateDirectory(target);
                continue;
            }

            var directory = Path.GetDirectoryName(target);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            entry.ExtractToFile(target, overwrite: true);
        }
    }

    private static void TryDelete(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch (Exception)
        {
            // The temp file is left for the user profile to clean up.
        }
    }

    private static void WriteResult(string path, string text) =>
        File.WriteAllText(path, text, Encoding.UTF8);
}

/// <summary>Job file written by <c>KryptonApplicationUpdater</c>.</summary>
internal sealed class UpdateJob
{
    public int ProcessId { get; set; }

    public string ZipPath { get; set; } = string.Empty;

    public string DestinationDirectory { get; set; } = string.Empty;

    public string? RestartExecutable { get; set; }

    public string? RestartArguments { get; set; }

    public int WaitTimeoutSeconds { get; set; }

    public static UpdateJob Load(string path)
    {
        var serializer = new JavaScriptSerializer();
        var data = serializer.Deserialize<Dictionary<string, object>>(File.ReadAllText(path));
        if (data == null)
        {
            throw new InvalidDataException("The update job file is empty.");
        }

        return new UpdateJob
        {
            ProcessId = GetInt(data, "processId"),
            ZipPath = GetString(data, "zipPath") ?? string.Empty,
            DestinationDirectory = GetString(data, "destinationDirectory") ?? string.Empty,
            RestartExecutable = GetString(data, "restartExecutable"),
            RestartArguments = GetString(data, "restartArguments"),
            WaitTimeoutSeconds = GetInt(data, "waitTimeoutSeconds")
        };
    }

    private static string? GetString(Dictionary<string, object> data, string name)
    {
        if (!data.TryGetValue(name, out var value) || value == null)
        {
            return null;
        }

        return Convert.ToString(value);
    }

    private static int GetInt(Dictionary<string, object> data, string name)
    {
        if (!data.TryGetValue(name, out var value) || value == null)
        {
            return 0;
        }

        if (value is int number)
        {
            return number;
        }

        if (value is long longNumber)
        {
            return (int)longNumber;
        }

        if (value is ArrayList)
        {
            return 0;
        }

        return Convert.ToInt32(value);
    }
}
