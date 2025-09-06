#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Build;

public sealed class AppState
{
    public ChannelType Channel { get; set; }
    public BuildAction Action { get; set; }
    public string Configuration { get; set; } = "Release";
    public string RootPath { get; set; } = string.Empty;
    public string MsBuildPath { get; set; } = string.Empty;

    public string ProjectFile { get; set; } = string.Empty;
    public string TextLogPath { get; set; } = string.Empty;
    public string BinLogPath { get; set; } = string.Empty;

    public TailBuffer Tail { get; } = new TailBuffer(200);
    public int TailLines { get; set; }
    public bool IsRunning { get; set; }
    public int LastExitCode { get; set; }
    public DateTime? StartTimeUtc { get; set; }
    public int ErrorCount { get; set; }
    public int WarningCount { get; set; }

    public bool SummaryReady { get; set; }
    public IReadOnlyList<string>? SummaryLines { get; set; }
    public int SummaryOffset { get; set; }

    public Action<string>? OnOutput { get; set; }
    public Process? Process { get; set; }
    public Queue<string>? PendingTargets { get; set; }
    public PackMode PackMode { get; set; } = PackMode.Pack;
    public Action? RequestRenderAll { get; set; }
    public bool AutoScroll { get; set; } = true;

    public Queue<string>? NuGetPushQueue { get; set; }
    public TasksPage TasksPage { get; set; } = TasksPage.Ops;
    public NuGetAction NuGetAction { get; set; } = NuGetAction.RebuildPack;
    public NuGetSource NuGetSource { get; set; } = NuGetSource.Default;
    public bool NuGetCreateZip { get; set; }
    public string NuGetCustomSource { get; set; } = string.Empty;
    public bool NuGetIncludeSymbols { get; set; }
    public string? NuGetLastZipPath { get; set; }
    public bool NuGetRunPushAfterMsBuild { get; set; }
    public bool NuGetRunZipAfterMsBuild { get; set; }
    public bool NuGetSkipDuplicate { get; set; } = true;
    public string? LastCompletedTarget { get; set; }
}

public sealed class TailBuffer
{
    private int capacity;
    private readonly LinkedList<string> lines = new LinkedList<string>();
    private readonly object sync = new object();

    public TailBuffer(int capacity)
    {
        this.capacity = capacity;
    }

    public void SetCapacity(int newCapacity)
    {
        if (newCapacity <= 0)
        {
            return;
        }
        lock (sync)
        {
            capacity = newCapacity;
            while (lines.Count > capacity)
            {
                lines.RemoveFirst();
            }
        }
    }

    public void Clear()
    {
        lock (sync)
        {
            lines.Clear();
        }
    }
}