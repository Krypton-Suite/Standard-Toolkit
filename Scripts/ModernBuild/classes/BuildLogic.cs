#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace krypton.build;

internal static class BuildLogic
{
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
            @"C:\\Program Files\\Microsoft Visual Studio\\2022\\Enterprise\\MSBuild\\Current\\Bin\\MSBuild.exe",
            @"C:\\Program Files\\Microsoft Visual Studio\\2022\\Professional\\MSBuild\\Current\\Bin\\MSBuild.exe",
            @"C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\MSBuild\\Current\\Bin\\MSBuild.exe"
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

    internal static ChannelType NextChannel(ChannelType channel)
    {
        return channel switch
        {
            ChannelType.Nightly => ChannelType.Canary,
            ChannelType.Canary  => ChannelType.Stable,
            _                   => ChannelType.Nightly
        };
    }

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

    internal static PackMode NextPackMode(PackMode mode)
    {
        return mode switch
        {
            PackMode.Pack       => PackMode.PackLite,
            PackMode.PackLite   => PackMode.PackAll,
            _                   => PackMode.Pack
        };
    }

    internal static string DefaultConfig(ChannelType channel)
    {
        return channel switch
        {
            ChannelType.Nightly => "Nightly",
            ChannelType.Canary  => "Canary",
            _                   => "Release"  // MUST for any other!
        };
    }

    internal static string NextConfig(ChannelType channel, string current)
    {
        if (string.Equals(current, "Release", StringComparison.OrdinalIgnoreCase))
        {
            return "Debug";
        }
        return "Release";
    }

    private static string GetEffectiveConfiguration(AppState state)
    {
        if (state.Action == BuildAction.Installer)
        {
            return "Installer";
        }
        return state.Configuration;
    }

    internal static void CycleTailSize(AppState state)
    {
        state.TailLines = state.TailLines == 200 ? 500 : state.TailLines == 500 ? 1000 : 200;
        state.Tail.SetCapacity(state.TailLines);
    }

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
                    state.Process = null;
                    StartNextBuildStep(state);
                    state.PendingTargets = new Queue<string>(new string[0]);
                    StartNuGetPush(state);
                    return;
                }
                case NuGetAction.BuildPackPush:
                {
                    nugetTargets.Add("Rebuild");
                    nugetTargets.Add(GetPackTargetForCurrent(state));
                    state.PendingTargets = new Queue<string>(nugetTargets);
                    state.Process = null;
                    StartNextBuildStep(state);
                    state.PendingTargets = new Queue<string>(new string[0]);
                    StartNuGetPush(state);
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
                catch
                {
                }
            }
            TryLoadSummary(state);
            if (state.LastExitCode == 0 && state.PendingTargets != null && state.PendingTargets.Count > 0)
            {
                StartNextBuildStep(state);
            }
            else
            {
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

            string channelFolder = state.Channel switch
            {
                ChannelType.Nightly => Path.Combine(state.RootPath, "Bin", "Nightly"),
                ChannelType.Canary => Path.Combine(state.RootPath, "Bin", "Canary"),
                _ => Path.Combine(state.RootPath, "Bin", "Release")
            };
            if (!Directory.Exists(channelFolder))
            {
                state.OnOutput?.Invoke($"Packages folder not found: {channelFolder}");
                state.LastExitCode = 1;
                state.SummaryLines = new[] { $"NuGet push failed. Folder not found: {channelFolder}" };
                state.SummaryReady = true;
                return;
            }

            var files = new List<string>();
            files.AddRange(Directory.GetFiles(channelFolder, "*.nupkg", SearchOption.TopDirectoryOnly));
            if (state.NuGetIncludeSymbols)
            {
                files.AddRange(Directory.GetFiles(channelFolder, "*.snupkg", SearchOption.TopDirectoryOnly));
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
            if (!string.IsNullOrEmpty(e.Data)) state.OnOutput?.Invoke(e.Data);
        };
        state.Process.ErrorDataReceived += (_, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data)) state.OnOutput?.Invoke(e.Data);
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