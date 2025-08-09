#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

using krypton.build;

internal static class Program
{
    private static int Main(string[] args)
    {
        Application.Init();

        var state = new AppState
        {
            Channel = ChannelType.Nightly,
            Action = BuildAction.Rebuild,
            Configuration = BuildLogic.DefaultConfig(ChannelType.Nightly),
            RootPath = BuildLogic.FindRepoRoot(),
            MsBuildPath = BuildLogic.LocateMSBuildExecutable(),
            TailLines = 200
        };

        var top = BuildUI.Create(state);
        Application.Run(top);
        Application.Shutdown();
        return state.LastExitCode;
    }
}