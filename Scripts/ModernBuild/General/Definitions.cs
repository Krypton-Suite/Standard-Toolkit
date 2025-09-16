#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Build;

public enum ChannelType
{
    Nightly,
    Canary,
    Stable,
    LTS
}

public enum BuildAction
{
    Build,
    Rebuild,
    Pack,
    BuildPack,
    Debug,
    NuGetTools,
    Installer,
    CreateArchives,
    Documentation,
}

public enum PackMode
{
    Pack,
    PackLite,
    PackAll
}

public enum TasksPage
{
    Ops,
    NuGet
}

public enum NuGetAction
{
    RebuildPack,
    Push,
    PackPush,
    BuildPackPush,
    Tools
}

public enum NuGetSource
{
    Default,
    NuGetOrg,
    GitHub,
    Custom
}