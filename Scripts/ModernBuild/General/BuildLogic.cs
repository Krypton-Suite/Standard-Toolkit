#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Build;

/// <summary>
/// Provides core build logic and orchestration for the Krypton Modern Build tool.
/// This class handles MSBuild process management, NuGet operations, path discovery,
/// and build workflow coordination.
/// </summary>
internal static class BuildLogic
{
    /// <summary>
    /// Discovers the root directory of the Krypton repository by searching for build project files.
    /// Searches upward from the application's base directory to find the Scripts/Project-Files/build.proj file.
    /// </summary>
    /// <returns>The full path to the repository root directory.</returns>
    internal static string FindRepoRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            string probe = Path.Combine(dir.FullName, "Scripts", "Project-Files", "build.proj");
            if (File.Exists(probe))
            {
                return dir.FullName;
            }
            dir = dir.Parent;
        }
        try
        {
            var probe = new DirectoryInfo(AppContext.BaseDirectory).Parent?.Parent?.Parent?.Parent;
            if (probe != null)
            {
                string candidate = Path.Combine(probe.FullName, "Scripts", "Project-Files", "build.proj");
                if (File.Exists(candidate))
                {
                    return probe.FullName;
                }
            }
        }
        catch {}
        return new DirectoryInfo(AppContext.BaseDirectory).FullName;
    }

    /// <summary>
    /// Gets the project file path for the specified build channel.
    /// Searches in both Scripts and Scripts/Project-Files directories for the appropriate .proj file.
    /// </summary>
    /// <param name="rootPath">The root path of the repository.</param>
    /// <param name="channel">The build channel (Nightly, Canary, Stable, LTS).</param>
    /// <returns>The path to the project file for the specified channel.</returns>
    private static string GetProjFileForChannel(string rootPath, ChannelType channel)
    {
        string name = channel switch
        {
            ChannelType.Nightly => "nightly.proj",
            ChannelType.Canary  => "canary.proj",
            _                   => "build.proj"
        };
        string pfProjectFiles = Path.Combine(rootPath, "Scripts", "Project-Files", name);
        string pfScripts = Path.Combine(rootPath, "Scripts", name);
        if (File.Exists(pfScripts))
        {
            return pfScripts;
        }
        if (File.Exists(pfProjectFiles))
        {
            return pfProjectFiles;
        }
        return pfScripts;
    }

    /// <summary>
    /// Locates the MSBuild executable on the system.
    /// First attempts to use vswhere.exe to find the latest Visual Studio installation,
    /// then falls back to common installation paths for Visual Studio 2022.
    /// </summary>
    /// <returns>The full path to the MSBuild executable.</returns>
    /// <exception cref="InvalidOperationException">Thrown when MSBuild.exe cannot be found.</exception>
    internal static string LocateMSBuildExecutable()
    {
        try
        {
            string vswhere = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Microsoft Visual Studio",
                "Installer",
                "vswhere.exe");
            if (File.Exists(vswhere))
            {
                var psi = new ProcessStartInfo
                {
                    FileName = vswhere,
                    Arguments = "-latest -products * -requires Microsoft.Component.MSBuild -find MSBuild\\Current\\Bin\\MSBuild.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };
                using var p = Process.Start(psi)!;
                string? path = p.StandardOutput.ReadLine();
                p.WaitForExit(3000);
                if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                {
                    return path!;
                }
            }
        }
        catch {}

        string[] candidates = new[]
        {
            "C:\\Program Files\\Microsoft Visual Studio\\2022\\Enterprise\\MSBuild\\Current\\Bin\\MSBuild.exe",
            "C:\\Program Files\\Microsoft Visual Studio\\2022\\Professional\\MSBuild\\Current\\Bin\\MSBuild.exe",
            "C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\MSBuild\\Current\\Bin\\MSBuild.exe"
        };

        foreach (string c in candidates)
        {
            if (File.Exists(c))
            {
                return c;
            }
        }
        throw new InvalidOperationException("Could not find MSBuild.exe. Please ensure Visual Studio 2022 is installed.");
    }

    /// <summary>
    /// Gets the next channel type in the build channel progression cycle.
    /// </summary>
    /// <param name="channel">The current channel type.</param>
    /// <returns>The next channel type in the cycle (Nightly → Canary → Stable → Nightly).</returns>
    internal static ChannelType NextChannel(ChannelType channel)
    {
        return channel switch
        {
            ChannelType.Nightly => ChannelType.Canary,
            ChannelType.Canary  => ChannelType.Stable,
            _                   => ChannelType.Nightly
        };
    }

    /// <summary>
    /// Gets the next build action in the build action progression cycle.
    /// </summary>
    /// <param name="action">The current build action.</param>
    /// <returns>The next build action in the cycle (Build → Rebuild → Pack → BuildPack → Debug → NuGetTools → Installer → Build).</returns>
    internal static BuildAction NextAction(BuildAction action)
    {
        return action switch
        {
            BuildAction.Build       => BuildAction.Rebuild,
            BuildAction.Rebuild     => BuildAction.Pack,
            BuildAction.Pack        => BuildAction.BuildPack,
            BuildAction.BuildPack   => BuildAction.Debug,
            BuildAction.Debug       => BuildAction.NuGetTools,
            BuildAction.NuGetTools  => BuildAction.Installer,
            BuildAction.Installer   => BuildAction.Build,
            _                       => BuildAction.Build
        };
    }

    /// <summary>
    /// Gets the next pack mode in the pack mode progression cycle.
    /// </summary>
    /// <param name="mode">The current pack mode.</param>
    /// <returns>The next pack mode in the cycle (Pack → PackLite → PackAll → Pack).</returns>
    internal static PackMode NextPackMode(PackMode mode)
    {
        return mode switch
        {
            PackMode.Pack       => PackMode.PackLite,
            PackMode.PackLite   => PackMode.PackAll,
            _                   => PackMode.Pack
        };
    }

    /// <summary>
    /// Gets the default build configuration for the specified channel.
    /// </summary>
    /// <param name="channel">The build channel type.</param>
    /// <returns>The default configuration string for the channel (Nightly → "Nightly", Canary → "Canary", others → "Release").</returns>
    internal static string DefaultConfig(ChannelType channel)
    {
        return channel switch
        {
            ChannelType.Nightly => "Nightly",
            ChannelType.Canary  => "Canary",
            _                   => "Release"  // MUST for any other!
        };
    }

    /// <summary>
    /// Toggles between Debug and Release configuration.
    /// </summary>
    /// <param name="channel">The build channel (unused in current implementation).</param>
    /// <param name="current">The current configuration string.</param>
    /// <returns>"Debug" if current is "Release", otherwise "Release".</returns>
    internal static string NextConfig(ChannelType channel, string current)
    {
        if (string.Equals(current, "Release", StringComparison.OrdinalIgnoreCase))
        {
            return "Debug";
        }
        return "Release";
    }

    /// <summary>
    /// Gets the effective build configuration for the current build state.
    /// Currently returns the state's configuration directly (installer config is not used as an Ops action).
    /// </summary>
    /// <param name="state">The current application state.</param>
    /// <returns>The effective configuration string to use for the build.</returns>
    private static string GetEffectiveConfiguration(AppState state)
    {
        // Installer config is not used as an Ops action anymore
        return state.Configuration;
    }

    /// <summary>
    /// Cycles through different tail buffer sizes for build output display.
    /// Cycles between 200, 500, and 1000 lines.
    /// </summary>
    /// <param name="state">The application state containing the tail buffer and size settings.</param>
    internal static void CycleTailSize(AppState state)
    {
        state.TailLines = state.TailLines == 200 ? 500 : state.TailLines == 500 ? 1000 : 200;
        state.Tail.SetCapacity(state.TailLines);
    }

    /// <summary>
    /// Starts a build operation based on the current application state.
    /// Handles different build actions, NuGet operations, and orchestrates the build workflow.
    /// </summary>
    /// <param name="state">The application state containing build configuration and settings.</param>
    internal static void StartBuild(AppState state)
    {
        if (state.IsRunning)
        {
            return;
        }
        EnsurePaths(state);

        state.Tail.Clear();
        state.SummaryReady = false;
        state.ErrorCount = 0;
        state.WarningCount = 0;
        // Reset any leftover orchestration flags/queues from previous runs
        state.PendingTargets = null;
        state.NuGetRunPushAfterMsBuild = false;
        state.NuGetRunZipAfterMsBuild = false;
        state.LastCompletedTarget = null;
        if (state.Action == BuildAction.NuGetTools)
        {
            StartNuGetTools(state);
            return;
        }

        // NuGet page actions are interpreted here via the standard F5 Run
        if (state.TasksPage == TasksPage.NuGet)
        {
            EnsurePaths(state);
            state.Tail.Clear();
            state.SummaryReady = false;
            state.ErrorCount = 0;
            state.WarningCount = 0;

            var nugetTargets = new List<string>();
            switch (state.NuGetAction)
            {
                case NuGetAction.Tools:
                {
                    StartNuGetTools(state);
                    return;
                }
                case NuGetAction.RebuildPack:
                {
                    nugetTargets.Add("Rebuild");
                    nugetTargets.Add(GetPackTargetForCurrent(state));
                    state.NuGetRunZipAfterMsBuild = state.NuGetCreateZip;
                    break;
                }
                case NuGetAction.Push:
                {
                    StartNuGetPush(state);
                    return;
                }
                case NuGetAction.PackPush:
                {
                    nugetTargets.Add(GetPackTargetForCurrent(state));
                    state.PendingTargets = new Queue<string>(nugetTargets);
                    // After MSBuild completes, run zip (optional) and then push
                    state.NuGetRunPushAfterMsBuild = true;
                    state.NuGetRunZipAfterMsBuild = state.NuGetCreateZip;
                    StartNextBuildStep(state);
                    return;
                }
                case NuGetAction.BuildPackPush:
                {
                    nugetTargets.Add("Rebuild");
                    nugetTargets.Add(GetPackTargetForCurrent(state));
                    state.PendingTargets = new Queue<string>(nugetTargets);
                    // After MSBuild completes, run zip (optional) and then push
                    state.NuGetRunPushAfterMsBuild = true;
                    state.NuGetRunZipAfterMsBuild = state.NuGetCreateZip;
                    StartNextBuildStep(state);
                    return;
                }
            }
            state.PendingTargets = new Queue<string>(nugetTargets);
            StartNextBuildStep(state);
            return;
        }

        if (state.Action == BuildAction.Debug)
        {
            _ = StartCleanAsync(state, onCompleted: () =>
            {
                state.Channel = ChannelType.Nightly;
                EnsurePaths(state);
                state.PendingTargets = new Queue<string>(new[] { "Rebuild" });
                StartNextBuildStep(state);
            });
            return;
        }

        var targets = new List<string>();
        switch (state.Action)
        {
            case BuildAction.Build:
            case BuildAction.Rebuild:
            {
                targets.Add("Rebuild");
                break;
            }
            case BuildAction.Pack:
            {
                targets.Add(GetPackTargetForCurrent(state));
                break;
            }
            case BuildAction.BuildPack:
            {
                targets.Add("Rebuild");
                targets.Add(GetPackTargetForCurrent(state));
                break;
            }
            case BuildAction.Installer:
            {
                targets.Add("Build");
                break;
            }
        }
        state.PendingTargets = new Queue<string>(targets);

        StartNextBuildStep(state);
    }

    /// <summary>
    /// Stops the currently running build process and cleans up resources.
    /// Terminates the MSBuild process and resets the running state.
    /// </summary>
    /// <param name="state">The application state containing the process to stop.</param>
    internal static void StopBuild(AppState state)
    {
        try
        {
            if (state.Process != null && !state.Process.HasExited)
            {
                state.Process.Kill(entireProcessTree: true);
            }
        }
        catch { }
        finally
        {
            state.IsRunning = false;
            state.PendingTargets = null;
        }
    }

    /// <summary>
    /// Ensures that all required paths and directories exist for the build operation.
    /// Creates log directories and sets up project file paths based on the build action and channel.
    /// </summary>
    /// <param name="state">The application state to update with path information.</param>
    internal static void EnsurePaths(AppState state)
    {
        string logs = Path.Combine(state.RootPath, "Logs");
        Directory.CreateDirectory(logs);
        if (state.Action == BuildAction.Installer)
        {
            string prefix = "installer";
            state.ProjectFile = GetInstallerProjFile(state.RootPath);
            state.TextLogPath = Path.Combine(logs, $"{prefix}-build-summary.log");
            state.BinLogPath = Path.Combine(logs, $"{prefix}-build.binlog");
        }
        else
        {
            string prefix = state.Channel.ToString().ToLowerInvariant();
            state.ProjectFile = GetProjFileForChannel(state.RootPath, state.Channel);
            state.TextLogPath = Path.Combine(logs, $"{prefix}-build-summary.log");
            state.BinLogPath = Path.Combine(logs, $"{prefix}-build.binlog");
        }
    }

    /// <summary>
    /// Attempts to load and parse the build summary from the text log file.
    /// Creates a summary with build metadata and recent log output for display.
    /// </summary>
    /// <param name="state">The application state to update with summary information.</param>
    internal static void TryLoadSummary(AppState state)
    {
        try
        {
            if (File.Exists(state.TextLogPath))
            {
                var tail = ReadTailForSummary(state.TextLogPath, 300);
                var meta = BuildSummaryHeader(state);
                var combined = new List<string>(meta.Count + tail.Count + 1);
                combined.AddRange(meta);
                combined.AddRange(tail);
                state.SummaryLines = combined;
                state.SummaryReady = true;
                TryNoteNoWork(state);
            }
            else
            {
                var meta = BuildSummaryHeader(state);
                var missing = new List<string>(meta.Count + 2);
                missing.AddRange(meta);
                missing.Add($"No build log found at: {state.TextLogPath}");
                state.SummaryLines = missing;
                state.SummaryReady = true;
            }
        }
        catch (Exception ex)
        {
            var meta = BuildSummaryHeader(state);
            meta.Add("Failed to load summary:");
            meta.Add(ex.Message);
            state.SummaryLines = meta;
            state.SummaryReady = true;
        }
    }

    private static List<string> BuildSummaryHeader(AppState state)
    {
        string tz = TimeZoneInfo.Local.StandardName;
        string now = DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss zzz");
        string cfg = GetEffectiveConfiguration(state);
        return new List<string>
        {
            $"Build Summary — {now} (Zone: {tz})",
            $"Channel: {state.Channel}   Config: {cfg}"
        };
    }

    /// <summary>
    /// Reads the tail portion of a log file for summary display.
    /// Searches for key build summary markers and returns the relevant lines.
    /// </summary>
    /// <param name="filePath">The path to the log file to read.</param>
    /// <param name="maxLines">The maximum number of lines to return.</param>
    /// <returns>A list of lines from the tail of the file, focusing on build summary information.</returns>
    private static IReadOnlyList<string> ReadTailForSummary(string filePath, int maxLines)
    {
        const int readBytes = 512 * 1024;
        using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        long start = Math.Max(0, fs.Length - readBytes);
        fs.Seek(start, SeekOrigin.Begin);

        using var sr = new StreamReader(fs, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
        string content = sr.ReadToEnd();
        string[] allLines = content.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');

        int idx = LastIndexOf(allLines,
            l => l.Contains("Errors & Warnings", StringComparison.OrdinalIgnoreCase) ||
                l.Contains("Summary", StringComparison.OrdinalIgnoreCase) ||
                l.Contains("Build succeeded.", StringComparison.OrdinalIgnoreCase) ||
                l.Contains("Build FAILED.", StringComparison.OrdinalIgnoreCase) ||
                l.Contains("Time Elapsed", StringComparison.OrdinalIgnoreCase));

        int startLine = idx >= 0 ? idx : Math.Max(0, allLines.Length - maxLines);
        var slice = new List<string>();
        for (int i = startLine; i < allLines.Length; i++)
        {
            string raw = allLines[i];
            if (string.IsNullOrWhiteSpace(raw))
            {
                slice.Add("");
                continue;
            }
            slice.Add(raw);
            if (slice.Count >= maxLines)
            {
                break;
            }
        }
        return slice;
    }

    private static int LastIndexOf(string[] array, Func<string, bool> predicate)
    {
        for (int i = array.Length - 1; i >= 0; i--)
        {
            if (predicate(array[i]))
            {
                return i;
            }
        }
        return -1;
    }

    private static void TryAppendCompletionTime(AppState state, DateTimeOffset when)
    {
        try
        {
            var list = state.SummaryLines as List<string> ?? state.SummaryLines?.ToList() ?? new List<string>();
            list.Add($"Completed at: {when:yyyy-MM-dd HH:mm:ss zzz}");
            state.SummaryLines = list;
            state.SummaryReady = true;
        }
        catch {}
    }

    private static string GetPackTargetForCurrent(AppState state)
    {
        if (state.Channel == ChannelType.Stable)
        {
            return state.PackMode switch
            {
                PackMode.PackLite => "PackLite",
                PackMode.PackAll  => "PackAll",
                _                 => "Pack"
            };
        }
        return "Pack";
    }

    /// <summary>
    /// Starts the next build step in the pending targets queue.
    /// If no more targets are queued, marks the build as complete.
    /// </summary>
    /// <param name="state">The application state containing the pending targets queue.</param>
    private static void StartNextBuildStep(AppState state)
    {
        if (state.PendingTargets == null || state.PendingTargets.Count == 0)
        {
            state.IsRunning = false;
            return;
        }
        string nextTarget = state.PendingTargets.Dequeue();
        StartSingleMsBuild(state, nextTarget);
    }

    /// <summary>
    /// Starts a single MSBuild process for the specified target.
    /// Configures the process with appropriate arguments, logging, and event handlers.
    /// </summary>
    /// <param name="state">The application state containing build configuration.</param>
    /// <param name="target">The MSBuild target to execute (e.g., "Build", "Rebuild", "Pack").</param>
    private static void StartSingleMsBuild(AppState state, string target)
    {
        EnsurePaths(state);
        if (!File.Exists(state.ProjectFile))
        {
            state.OnOutput?.Invoke($"Project file not found: {state.ProjectFile}");
            state.IsRunning = false;
            state.LastExitCode = 1;
            return;
        }
        state.OnOutput?.Invoke($"Project: {state.ProjectFile}");
        string args = new StringBuilder()
            .Append('"').Append(state.ProjectFile).Append('"').Append(' ')
            .Append("-t:").Append(target).Append(' ')
            .Append("/m ")
            .Append("/nr:false ")
            .Append("/p:Configuration=").Append(GetEffectiveConfiguration(state)).Append(' ')
            .Append("/fl ")
            .Append("/flp:logfile=\"").Append(state.TextLogPath).Append("\";verbosity=quiet;summary;encoding=UTF-8 ")
            .Append("/bl:\"").Append(state.BinLogPath).Append("\" ")
            .Append("/clp:Summary;ShowTimestamp ")
            .Append("/v:minimal")
            .ToString();

        state.OnOutput?.Invoke($"Running: \"{state.MsBuildPath}\" {args}");
        state.LastCompletedTarget = null;
        state.Process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = state.MsBuildPath,
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
                WorkingDirectory = state.RootPath
            },
            EnableRaisingEvents = true
        };

        state.Process.OutputDataReceived += (_, e) =>
        {
            if (string.IsNullOrEmpty(e.Data))
            {
                return;
            }
            if (e.Data.IndexOf(" error ", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                state.ErrorCount++;
            }
            if (e.Data.IndexOf(" warning ", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                state.WarningCount++;
            }
            state.OnOutput?.Invoke(e.Data);
        };

        state.Process.ErrorDataReceived += (_, e) =>
        {
            if (string.IsNullOrEmpty(e.Data))
            {
                return;
            }
            if (e.Data.IndexOf(" error ", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                state.ErrorCount++;
            }
            if (e.Data.IndexOf(" warning ", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                state.WarningCount++;
            }
            state.OnOutput?.Invoke(e.Data);
        };

        state.Process.Exited += (_, __) =>
        {
            state.LastExitCode = state.Process?.ExitCode ?? -1;
            state.LastCompletedTarget = target;
            if (state.LastExitCode != 0)
            {
                state.OnOutput?.Invoke($"[red]Exit code: {state.LastExitCode}[/]");
                try
                {
                    IReadOnlyList<string> tail = ReadTailForSummary(state.TextLogPath, 300);
                    foreach (string line in tail)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            state.OnOutput?.Invoke(line);
                        }
                    }
                }
                catch {}
                TryLoadSummary(state);
                state.IsRunning = false;
                state.RequestRenderAll?.Invoke();
                return;
            }

            // Success path
            if (state.PendingTargets != null && state.PendingTargets.Count > 0)
            {
                // Intermediate step (e.g., Rebuild before Pack). Advance only if the last completed
                // target matches what we started for this process; otherwise ignore spurious exits.
                string? justCompleted = state.LastCompletedTarget;
                if (!string.IsNullOrEmpty(justCompleted))
                {
                    StartNextBuildStep(state);
                    return;
                }
            }

            // No more MSBuild steps queued
            bool dispatched = false;
            if (state.TasksPage == TasksPage.NuGet)
            {
                if (state.NuGetRunZipAfterMsBuild)
                {
                    state.NuGetRunZipAfterMsBuild = false;
                    TryCreateNuGetZip(state);
                }
                if (state.NuGetRunPushAfterMsBuild)
                {
                    state.NuGetRunPushAfterMsBuild = false;
                    StartNuGetPush(state);
                    dispatched = true;
                }
            }

            if (!dispatched)
            {
                // Only load summary once at the end of the full chain
                TryLoadSummary(state);
                state.IsRunning = false;
                state.RequestRenderAll?.Invoke();
            }
        };

        if (!state.IsRunning || !state.StartTimeUtc.HasValue)
        {
            state.StartTimeUtc = DateTime.UtcNow;
        }
        state.IsRunning = true;
        state.Process.Start();
        state.Process.BeginOutputReadLine();
        state.Process.BeginErrorReadLine();
    }

    private static void TryNoteNoWork(AppState state)
    {
        try
        {
            IReadOnlyList<string>? lines = state.SummaryLines;
            if (lines == null || lines.Count == 0)
            {
                return;
            }
            bool hasSucceeded = false;
            TimeSpan? elapsed = null;
            foreach (string l in lines)
            {
                if (!hasSucceeded && l.Contains("Build succeeded", StringComparison.OrdinalIgnoreCase))
                {
                    hasSucceeded = true;
                }
                int idx = l.IndexOf("Time Elapsed", StringComparison.OrdinalIgnoreCase);
                if (idx >= 0)
                {
                    string t = l.Substring(idx).Replace("Time Elapsed", string.Empty).Trim().Trim(':').Trim();
                    if (TimeSpan.TryParse(t, out var ts))
                    {
                        elapsed = ts;
                        break;
                    }
                }
            }
            bool nearZero = elapsed.HasValue
                    ? elapsed.Value.TotalSeconds < 2.0
                    : (state.StartTimeUtc.HasValue
                        ? (DateTime.UtcNow - state.StartTimeUtc.Value).TotalSeconds < 2.0
                        : false);
            if (hasSucceeded && nearZero)
            {
                state.OnOutput?.Invoke("[yellow]Note: MSBuild finished with no observable work. Verify Configuration and TargetFrameworks in the .proj so targets are not skipped.[/]");
            }
        }
        catch { }
    }

    /// <summary>
    /// Starts an asynchronous clean operation that removes build artifacts and temporary files.
    /// Deletes Bin directory, object files, and logs to prepare for a fresh build.
    /// </summary>
    /// <param name="state">The application state to update during the clean operation.</param>
    /// <param name="onCompleted">Optional callback to execute when the clean operation completes.</param>
    /// <returns>A Task representing the asynchronous clean operation.</returns>
    internal static Task StartCleanAsync(AppState state, Action? onCompleted = null)
    {
        if (state.IsRunning)
        {
            return Task.CompletedTask;
        }
        state.IsRunning = true;
        state.StartTimeUtc = DateTime.UtcNow;
        state.Tail.Clear();
        state.SummaryReady = false;
        state.ErrorCount = 0;
        state.WarningCount = 0;
        return Task.Run(() =>
        {
            void DelDir(string rel)
            {
                try
                {
                    string full = Path.Combine(state.RootPath, rel);
                    state.OnOutput?.Invoke($"Deleting: {rel}");
                    if (Directory.Exists(full))
                    {
                        Directory.Delete(full, recursive: true);
                    }
                }
                catch (Exception ex)
                {
                    state.OnOutput?.Invoke($"[yellow]Warning deleting {rel}: {ex.Message}[/]");
                    state.WarningCount++;
                }
            }
            DelDir("Bin");
            DelDir(Path.Combine("Source", "Krypton Components", "Krypton.Docking", "obj"));
            DelDir(Path.Combine("Source", "Krypton Components", "Krypton.Navigator", "obj"));
            DelDir(Path.Combine("Source", "Krypton Components", "Krypton.Ribbon", "obj"));
            DelDir(Path.Combine("Source", "Krypton Components", "Krypton.Toolkit", "obj"));
            DelDir(Path.Combine("Source", "Krypton Components", "Krypton.Workspace", "obj"));
            DelDir("Logs");
            state.LastExitCode = 0;
            state.IsRunning = false;
            state.SummaryLines = new[] { "Clean completed." };
            state.SummaryReady = true;
            state.OnOutput?.Invoke("Clean complete.");
            onCompleted?.Invoke();
        });
    }

    /// <summary>
    /// Starts the NuGet tools update process to ensure the latest NuGet.exe is available.
    /// Runs 'nuget.exe update -Self -NonInteractive' in the Scripts directory.
    /// </summary>
    /// <param name="state">The application state to update with process information.</param>
    private static void StartNuGetTools(AppState state)
    {
        if (state.IsRunning)
        {
            return;
        }

        state.Tail.Clear();
        state.SummaryReady = false;
        state.ErrorCount = 0;
        state.WarningCount = 0;
        string scriptsDir = Path.Combine(state.RootPath, "Scripts");
        string fileName = "nuget.exe";
        string arguments = "update -Self -NonInteractive";

        state.OnOutput?.Invoke($"Running: {fileName} {arguments} (cwd: {scriptsDir})");
        state.Process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                WorkingDirectory = scriptsDir,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8
            },
            EnableRaisingEvents = true
        };

        state.Process.OutputDataReceived += (_, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                state.OnOutput?.Invoke(e.Data);
            }
        };

        state.Process.ErrorDataReceived += (_, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                state.OnOutput?.Invoke(e.Data);
            }
        };

        state.Process.Exited += (_, __) =>
        {
            state.LastExitCode = state.Process?.ExitCode ?? -1;
            state.IsRunning = false;
            state.SummaryLines = new[] { $"NuGet tools exited with code {state.LastExitCode}." };
            state.SummaryReady = true;
        };

        state.IsRunning = true;
        state.StartTimeUtc = DateTime.UtcNow;
        state.Process.Start();
        state.Process.BeginOutputReadLine();
        state.Process.BeginErrorReadLine();
    }

    /// <summary>
    /// Starts the NuGet package push process.
    /// Validates credentials, finds packages, and queues them for pushing to the configured NuGet source.
    /// </summary>
    /// <param name="state">The application state containing NuGet configuration and package information.</param>
    private static void StartNuGetPush(AppState state)
    {
        if (state.IsRunning)
        {
            return;
        }

        try
        {
            // Preflight credential/source checks
            if (!ValidateNuGetCredentials(state))
            {
                return;
            }

            var files = new List<string>();
            foreach (string f in GetCandidatePackageFolders(state))
            {
                if (!Directory.Exists(f))
                {
                    continue;
                }

                files.AddRange(Directory.GetFiles(f, "*.nupkg", SearchOption.TopDirectoryOnly));
                if (state.NuGetIncludeSymbols)
                {
                    files.AddRange(Directory.GetFiles(f, "*.snupkg", SearchOption.TopDirectoryOnly));
                }
            }
            files.Sort(StringComparer.OrdinalIgnoreCase);
            if (files.Count == 0)
            {
                state.OnOutput?.Invoke("No packages found to push.");
                state.SummaryLines = new[] { "No packages found to push." };
                state.SummaryReady = true;
                return;
            }

            state.NuGetPushQueue = new Queue<string>(files);
            RunNextNuGetPush(state);
        }
        catch (Exception ex)
        {
            state.OnOutput?.Invoke($"NuGet push error: {ex.Message}");
            state.LastExitCode = 1;
            state.SummaryLines = new[] { "NuGet push error:", ex.Message };
            state.SummaryReady = true;
        }
    }

    /// <summary>
    /// Attempts to create a ZIP archive containing all NuGet packages from the build output.
    /// Creates a timestamped ZIP file in the Bin directory with all .nupkg files.
    /// </summary>
    /// <param name="state">The application state to update with the ZIP file path.</param>
    private static void TryCreateNuGetZip(AppState state)
    {
        try
        {
            string bin = Path.Combine(state.RootPath, "Bin", "Release");
            if (!Directory.Exists(bin))
            {
                state.OnOutput?.Invoke($"ZIP: folder not found: {bin}");
                return;
            }
            string date = DateTime.Now.ToString("yyyyMMdd");
            string name = $"{date}_NuGet_Packages.zip";
            string zipPath = Path.Combine(state.RootPath, "Bin", name);
            if (File.Exists(zipPath))
            {
                try { File.Delete(zipPath); } catch { }
            }
            state.OnOutput?.Invoke($"Creating ZIP: {zipPath}");
            System.IO.Compression.ZipFile.CreateFromDirectory(bin, zipPath, System.IO.Compression.CompressionLevel.Optimal, includeBaseDirectory: false);
            state.NuGetLastZipPath = zipPath;
        }
        catch (Exception ex)
        {
            state.OnOutput?.Invoke($"ZIP error: {ex.Message}");
        }
    }

    /// <summary>
    /// Validates NuGet credentials and source configuration before attempting to push packages.
    /// Checks for API keys, source configuration, and custom source URLs as needed.
    /// </summary>
    /// <param name="state">The application state containing NuGet configuration.</param>
    /// <returns>True if credentials are valid and ready for pushing, false otherwise.</returns>
    private static bool ValidateNuGetCredentials(AppState state)
    {
        try
        {
            switch (state.NuGetSource)
            {
                case NuGetSource.GitHub:
                {
                    bool hasGithub = NuGetSourceExists(state, "nuget.pkg.github.com") || NuGetSourceExists(state, "github");
                    if (!hasGithub)
                    {
                        state.OnOutput?.Invoke("[yellow]GitHub source not found in NuGet sources. Configure it first (nuget sources add ...).[/]");
                        state.SummaryLines = new[] { "GitHub source not configured.", "Add a NuGet source for GitHub Packages before pushing." };
                        state.SummaryReady = true;
                        state.LastExitCode = 1;
                        return false;
                    }
                    break;
                }
                case NuGetSource.Custom:
                {
                    if (string.IsNullOrWhiteSpace(state.NuGetCustomSource))
                    {
                        state.OnOutput?.Invoke("[yellow]Custom source is empty. Set a URL for Custom source before pushing.[/]");
                        state.SummaryLines = new[] { "Custom source is empty.", "Set 'Source' to Custom and provide a URL." };
                        state.SummaryReady = true;
                        state.LastExitCode = 1;
                        return false;
                    }
                    break;
                }
                case NuGetSource.NuGetOrg:
                case NuGetSource.Default:
                default:
                {
                    if (!HasNuGetOrgApiKey(state))
                    {
                        state.OnOutput?.Invoke("[yellow]NuGet.org API key not found. Configure it with 'nuget.exe setapikey <KEY> -Source https://api.nuget.org/v3/index.json'.[/]");
                        state.SummaryLines = new[]
                        {
                            "NuGet.org API key not detected.",
                            "Run: nuget.exe setapikey <KEY> -Source https://api.nuget.org/v3/index.json"
                        };
                        state.SummaryReady = true;
                        state.LastExitCode = 1;
                        return false;
                    }
                    break;
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            state.OnOutput?.Invoke($"[yellow]Credential preflight check failed: {ex.Message}[/]");
            state.SummaryLines = new[] { "Credential preflight check failed.", ex.Message };
            state.SummaryReady = true;
            state.LastExitCode = 1;
            return false;
        }
    }

    private static bool HasNuGetOrgApiKey(AppState state)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "nuget.exe",
            Arguments = "config -list",
            WorkingDirectory = Path.Combine(state.RootPath, "Scripts"),
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8
        };
        using var p = Process.Start(psi)!;
        string output = p.StandardOutput.ReadToEnd();
        p.WaitForExit(5000);
        if (string.IsNullOrEmpty(output))
        {
            return false;
        }
        return output.IndexOf("api.nuget.org", StringComparison.OrdinalIgnoreCase) >= 0
            || output.IndexOf("https://api.nuget.org/v3/index.json", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private static bool NuGetSourceExists(AppState state, string expected)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "nuget.exe",
            Arguments = "sources list -format Detailed",
            WorkingDirectory = Path.Combine(state.RootPath, "Scripts"),
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8
        };
        using var p = Process.Start(psi)!;
        string output = p.StandardOutput.ReadToEnd();
        p.WaitForExit(5000);
        if (string.IsNullOrEmpty(output))
        {
            return false;
        }
        return output.IndexOf(expected, StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private static void RunNextNuGetPush(AppState state)
    {
        if (state.NuGetPushQueue == null || state.NuGetPushQueue.Count == 0)
        {
            state.IsRunning = false;
            state.LastExitCode = 0;
            state.SummaryLines = new[] { "NuGet push completed." };
            state.SummaryReady = true;
            state.RequestRenderAll?.Invoke();
            return;
        }

        string pkg = state.NuGetPushQueue.Dequeue();
        string fileName = "nuget.exe";
        var args = new StringBuilder().Append("push \"").Append(pkg).Append("\" ");
        if (state.NuGetSkipDuplicate)
        {
            args.Append("-SkipDuplicate ");
        }
        args.Append("-NonInteractive ");

        string? resolvedSource = state.NuGetSource switch
        {
            NuGetSource.NuGetOrg => "https://api.nuget.org/v3/index.json",
            NuGetSource.GitHub => "github",
            NuGetSource.Custom => string.IsNullOrWhiteSpace(state.NuGetCustomSource) ? null : state.NuGetCustomSource,
            _ => null
        };
        if (!string.IsNullOrWhiteSpace(resolvedSource))
        {
            args.Append("-Source \"").Append(resolvedSource).Append("\" ");
        }

        state.OnOutput?.Invoke($"Running: {fileName} {args}");
        state.Process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = args.ToString(),
                WorkingDirectory = Path.Combine(state.RootPath, "Scripts"),
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8
            },
            EnableRaisingEvents = true
        };
        state.Process.OutputDataReceived += (_, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                state.OnOutput?.Invoke(e.Data);
            }
        };
        state.Process.ErrorDataReceived += (_, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                state.OnOutput?.Invoke(e.Data);
            }
        };
        state.Process.Exited += (_, __) =>
        {
            int code = state.Process?.ExitCode ?? -1;
            if (code != 0)
            {
                state.OnOutput?.Invoke($"[red]nuget.exe exited with code {code} for {Path.GetFileName(pkg)}[/]");
                state.LastExitCode = code;
                state.IsRunning = false;
                state.SummaryLines = new[] { $"NuGet push failed for {Path.GetFileName(pkg)} (code {code})." };
                state.SummaryReady = true;
                state.RequestRenderAll?.Invoke();
                return;
            }
            RunNextNuGetPush(state);
        };
        if (!state.IsRunning || !state.StartTimeUtc.HasValue)
        {
            state.StartTimeUtc = DateTime.UtcNow;
        }
        state.IsRunning = true;
        state.Process.Start();
        state.Process.BeginOutputReadLine();
        state.Process.BeginErrorReadLine();
    }

    /// <summary>
    /// Generates a preview of NuGet push commands that would be executed based on current settings.
    /// Shows the exact command lines that would be run for each package found.
    /// </summary>
    /// <param name="state">The application state containing NuGet configuration and package information.</param>
    internal static void PreviewNuGetCommands(AppState state)
    {
        try
        {
            var files = new List<string>();
            foreach (string f in GetCandidatePackageFolders(state))
            {
                if (!Directory.Exists(f))
                {
                    continue;
                }

                files.AddRange(Directory.GetFiles(f, "*.nupkg", SearchOption.TopDirectoryOnly));
                if (state.NuGetIncludeSymbols)
                {
                    files.AddRange(Directory.GetFiles(f, "*.snupkg", SearchOption.TopDirectoryOnly));
                }
            }
            files.Sort(StringComparer.OrdinalIgnoreCase);

            string? resolvedSource = state.NuGetSource switch
            {
                NuGetSource.NuGetOrg => "https://api.nuget.org/v3/index.json",
                NuGetSource.GitHub => "github",
                NuGetSource.Custom => string.IsNullOrWhiteSpace(state.NuGetCustomSource) ? null : state.NuGetCustomSource,
                _ => null
            };

            var lines = new List<string>();
            lines.Add("NuGet preview commands:");
            foreach (string pkg in files)
            {
                var cmd = new StringBuilder().Append("nuget.exe push \"").Append(pkg).Append("\" ");
                if (state.NuGetSkipDuplicate)
                {
                    cmd.Append("-SkipDuplicate ");
                }

                cmd.Append("-NonInteractive ");
                if (!string.IsNullOrWhiteSpace(resolvedSource))
                {
                    cmd.Append("-Source \"").Append(resolvedSource).Append("\" ");
                }
                lines.Add(cmd.ToString());
            }
            if (lines.Count == 1)
            {
                lines.Add("(no packages found)");
            }
            state.SummaryLines = lines;
            state.SummaryReady = true;
            state.RequestRenderAll?.Invoke();
        }
        catch (Exception ex)
        {
            state.SummaryLines = new[] { "Failed to preview NuGet commands:", ex.Message };
            state.SummaryReady = true;
            state.RequestRenderAll?.Invoke();
        }
    }

    /// <summary>
    /// Gets the list of candidate directories that may contain NuGet packages.
    /// Currently returns only the Bin/Release directory regardless of channel.
    /// </summary>
    /// <param name="state">The application state containing build configuration.</param>
    /// <returns>A list of directory paths to search for NuGet packages.</returns>
    private static IReadOnlyList<string> GetCandidatePackageFolders(AppState state)
    {
        /*
        // Should packages ever be expected in their own channel output bin folders:
        string bin = Path.Combine(state.RootPath, "Bin");
        var list = new List<string>(5);
        switch (state.Channel)
        {
            case ChannelType.Nightly:
                list.Add(Path.Combine(bin, "Nightly"));
                break;
            case ChannelType.Canary:
                list.Add(Path.Combine(bin, "Canary"));
                break;
            default:
                list.Add(Path.Combine(bin, "Release"));
                list.Add(Path.Combine(bin, "Stable"));
                break;
        }
        // Also consider generic bin outputs without channel segregation
        list.Add(Path.Combine(bin, "Release"));
        list.Add(Path.Combine(bin, "Debug"));
        return list.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        */
        // Packages are always produced into Bin/Release regardless of channel
        string binRelease = Path.Combine(state.RootPath, "Bin", "Release");
        return new[] { binRelease };
    }

    /// <summary>
    /// Gets the path to the installer project file.
    /// Searches in both Scripts and Scripts/Project-Files directories for installer.proj.
    /// </summary>
    /// <param name="rootPath">The root path of the repository.</param>
    /// <returns>The path to the installer project file.</returns>
    private static string GetInstallerProjFile(string rootPath)
    {
        string scripts = Path.Combine(rootPath, "Scripts", "installer.proj");
        if (File.Exists(scripts))
        {
            return scripts;
        }
        string projectFiles = Path.Combine(rootPath, "Scripts", "Project-Files", "installer.proj");
        return projectFiles;
    }
}