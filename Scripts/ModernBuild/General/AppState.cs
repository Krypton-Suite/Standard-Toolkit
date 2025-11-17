#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Build;

/// <summary>
/// Represents the application state for the Krypton Modern Build tool.
/// This class maintains all the configuration, runtime state, and data necessary for the build process.
/// </summary>
public sealed class AppState
{
    #region Build Configuration
    
    /// <summary>
    /// Gets or sets the build channel type (Nightly, Canary, Stable, LTS).
    /// </summary>
    public ChannelType Channel { get; set; }
    
    /// <summary>
    /// Gets or sets the build action to perform (Build, Rebuild, Pack, etc.).
    /// </summary>
    public BuildAction Action { get; set; }
    
    /// <summary>
    /// Gets or sets the build configuration (Debug, Release, etc.). Defaults to "Release".
    /// </summary>
    public string Configuration { get; set; } = "Release";
    
    /// <summary>
    /// Gets or sets the root path of the repository.
    /// </summary>
    public string RootPath { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the path to the MSBuild executable.
    /// </summary>
    public string MsBuildPath { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the project file path for the build operation.
    /// </summary>
    public string ProjectFile { get; set; } = string.Empty;

    #endregion

    #region Logging Configuration
    
    /// <summary>
    /// Gets or sets the path for text log output.
    /// </summary>
    public string TextLogPath { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the path for binary log output.
    /// </summary>
    public string BinLogPath { get; set; } = string.Empty;

    #endregion

    #region Runtime State
    
    /// <summary>
    /// Gets the tail buffer for capturing and displaying recent build output.
    /// </summary>
    public TailBuffer Tail { get; } = new TailBuffer(200);
    
    /// <summary>
    /// Gets or sets the number of tail lines to display.
    /// </summary>
    public int TailLines { get; set; }
    
    /// <summary>
    /// Gets or sets whether the build process is currently running.
    /// </summary>
    public bool IsRunning { get; set; }
    
    /// <summary>
    /// Gets or sets the last exit code from the build process.
    /// </summary>
    public int LastExitCode { get; set; }
    
    /// <summary>
    /// Gets or sets the UTC start time of the current build operation.
    /// </summary>
    public DateTime? StartTimeUtc { get; set; }
    
    /// <summary>
    /// Gets or sets the count of errors encountered during the build.
    /// </summary>
    public int ErrorCount { get; set; }
    
    /// <summary>
    /// Gets or sets the count of warnings encountered during the build.
    /// </summary>
    public int WarningCount { get; set; }

    #endregion

    #region Summary and Output
    
    /// <summary>
    /// Gets or sets whether the build summary is ready for display.
    /// </summary>
    public bool SummaryReady { get; set; }
    
    /// <summary>
    /// Gets or sets the lines of the build summary.
    /// </summary>
    public IReadOnlyList<string>? SummaryLines { get; set; }
    
    /// <summary>
    /// Gets or sets the offset for summary display positioning.
    /// </summary>
    public int SummaryOffset { get; set; }
    
    /// <summary>
    /// Gets or sets the callback action for handling output messages.
    /// </summary>
    public Action<string>? OnOutput { get; set; }

    #endregion

    #region Process Management
    
    /// <summary>
    /// Gets or sets the current build process instance.
    /// </summary>
    public Process? Process { get; set; }
    
    /// <summary>
    /// Gets or sets the queue of pending build targets.
    /// </summary>
    public Queue<string>? PendingTargets { get; set; }
    
    /// <summary>
    /// Gets or sets the last completed build target.
    /// </summary>
    public string? LastCompletedTarget { get; set; }

    #endregion

    #region Pack Configuration
    
    /// <summary>
    /// Gets or sets the pack mode for NuGet packaging (Pack, PackLite, PackAll).
    /// </summary>
    public PackMode PackMode { get; set; } = PackMode.Pack;

    #endregion

    #region UI State
    
    /// <summary>
    /// Gets or sets the callback action to request a full UI render.
    /// </summary>
    public Action? RequestRenderAll { get; set; }
    
    /// <summary>
    /// Gets or sets whether auto-scroll is enabled in the output display.
    /// </summary>
    public bool AutoScroll { get; set; } = true;

    #endregion

    #region NuGet Configuration
    
    /// <summary>
    /// Gets or sets the queue of packages waiting to be pushed to NuGet.
    /// </summary>
    public Queue<string>? NuGetPushQueue { get; set; }
    
    /// <summary>
    /// Gets or sets the current tasks page being displayed (Ops or NuGet).
    /// </summary>
    public TasksPage TasksPage { get; set; } = TasksPage.Ops;
    
    /// <summary>
    /// Gets or sets the NuGet action to perform (RebuildPack, Push, etc.).
    /// </summary>
    public NuGetAction NuGetAction { get; set; } = NuGetAction.RebuildPack;
    
    /// <summary>
    /// Gets or sets the NuGet source for package operations (Default, NuGetOrg, GitHub, Custom).
    /// </summary>
    public NuGetSource NuGetSource { get; set; } = NuGetSource.Default;
    
    /// <summary>
    /// Gets or sets whether to create a ZIP archive of NuGet packages.
    /// </summary>
    public bool NuGetCreateZip { get; set; }
    
    /// <summary>
    /// Gets or sets the custom NuGet source URL when using Custom source type.
    /// </summary>
    public string NuGetCustomSource { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets whether to include symbols in NuGet packages.
    /// </summary>
    public bool NuGetIncludeSymbols { get; set; }
    
    /// <summary>
    /// Gets or sets the path to the last created NuGet ZIP archive.
    /// </summary>
    public string? NuGetLastZipPath { get; set; }
    
    /// <summary>
    /// Gets or sets whether to run NuGet push after MSBuild completes.
    /// </summary>
    public bool NuGetRunPushAfterMsBuild { get; set; }
    
    /// <summary>
    /// Gets or sets whether to run ZIP creation after MSBuild completes.
    /// </summary>
    public bool NuGetRunZipAfterMsBuild { get; set; }
    
    /// <summary>
    /// Gets or sets whether to skip duplicate packages during NuGet operations. Defaults to true.
    /// </summary>
    public bool NuGetSkipDuplicate { get; set; } = true;

    #endregion
}

/// <summary>
/// A thread-safe circular buffer that maintains a fixed number of recent lines.
/// Used for capturing and displaying the tail of build output in the UI.
/// </summary>
public sealed class TailBuffer
{
    private int capacity;
    private readonly LinkedList<string> lines = new LinkedList<string>();
    private readonly object sync = new object();

    /// <summary>
    /// Initializes a new instance of the <see cref="TailBuffer"/> class with the specified capacity.
    /// </summary>
    /// <param name="capacity">The maximum number of lines to store in the buffer.</param>
    public TailBuffer(int capacity)
    {
        this.capacity = capacity;
    }

    /// <summary>
    /// Sets the capacity of the buffer and trims excess lines if necessary.
    /// </summary>
    /// <param name="newCapacity">The new capacity for the buffer. Must be greater than 0.</param>
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

    /// <summary>
    /// Clears all lines from the buffer.
    /// </summary>
    public void Clear()
    {
        lock (sync)
        {
            lines.Clear();
        }
    }
}