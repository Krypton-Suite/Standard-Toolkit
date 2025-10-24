#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

using Krypton.Build;

/// <summary>
/// The main entry point for the Krypton Modern Build tool.
/// This application provides a Terminal.Gui-based interface for building, packaging,
/// and managing the Krypton Standard Toolkit project.
/// </summary>
internal static class Program
{
    /// <summary>
    /// The main entry point for the Modern Build application.
    /// Initializes the Terminal.Gui framework, sets up the application state with default values,
    /// creates the UI, runs the application, and returns the exit code from the last build operation.
    /// </summary>
    /// <param name="args">Command line arguments (currently unused).</param>
    /// <returns>The exit code from the last build operation (0 for success, non-zero for failure).</returns>
    private static int Main(string[] args)
    {
        // Initialize the Terminal.Gui framework
        Application.Init();

        // Create and initialize the application state with default values
        var state = new AppState
        {
            // Set default channel to Nightly for development builds
            Channel = ChannelType.Nightly,
            // Set default action to Rebuild for clean builds
            Action = BuildAction.Rebuild,
            // Get the default configuration for the Nightly channel
            Configuration = BuildLogic.DefaultConfig(ChannelType.Nightly),
            // Discover the repository root directory
            RootPath = BuildLogic.FindRepoRoot(),
            // Locate the MSBuild executable on the system
            MsBuildPath = BuildLogic.LocateMSBuildExecutable(),
            // Set initial tail buffer size for output display
            TailLines = 200
        };

        // Create the main UI and run the application
        var top = BuildUI.Create(state);
        Application.Run(top);
        
        // Clean up Terminal.Gui resources
        Application.Shutdown();
        
        // Return the exit code from the last build operation
        return state.LastExitCode;
    }
}