#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace krypton.build
{
    public enum ChannelType
    {
        Nightly,
        Canary,
        Stable }

    public enum BuildAction
    {
        Build,
        Rebuild,
        Pack,
        BuildPack,
        Debug,
        NuGetTools,
        Installer
    }

    public enum PackMode
    {
        Pack,
        PackLite,
        PackAll
    }
}