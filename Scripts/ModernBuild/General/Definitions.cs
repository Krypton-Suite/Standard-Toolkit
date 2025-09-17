#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Build;

/// <summary>
/// Defines the different build channels available in the Krypton build system.
/// Each channel represents a different release tier with varying stability and feature sets.
/// </summary>
public enum ChannelType
{
    /// <summary>
    /// Nightly build channel - Contains the latest development changes and features.
    /// Built automatically every night with the most recent code changes.
    /// May be unstable and is primarily for testing and development purposes.
    /// </summary>
    Nightly,
    
    /// <summary>
    /// Canary build channel - Pre-release builds with new features and improvements.
    /// More stable than Nightly but still contains experimental features.
    /// Used for early testing and validation before Stable release.
    /// </summary>
    Canary,
    
    /// <summary>
    /// Stable build channel - Production-ready releases with tested and stable features.
    /// Recommended for general use and production environments.
    /// Contains well-tested features with comprehensive quality assurance.
    /// </summary>
    Stable,
    
    /// <summary>
    /// Long Term Support (LTS) channel - Extended support releases with long-term maintenance.
    /// Provides stability and security updates for extended periods.
    /// Suitable for enterprise environments requiring long-term support.
    /// </summary>
    LTS
}

/// <summary>
/// Defines the different build actions that can be performed by the build system.
/// Each action represents a specific build operation or combination of operations.
/// </summary>
public enum BuildAction
{
    /// <summary>
    /// Performs an incremental build operation.
    /// Only builds projects that have changed since the last build.
    /// Fastest build option for development scenarios.
    /// </summary>
    Build,
    
    /// <summary>
    /// Performs a clean rebuild operation.
    /// Removes all build artifacts and rebuilds everything from scratch.
    /// Ensures a completely fresh build state.
    /// </summary>
    Rebuild,
    
    /// <summary>
    /// Creates NuGet packages from the built assemblies.
    /// Packages the compiled libraries into .nupkg files for distribution.
    /// </summary>
    Pack,
    
    /// <summary>
    /// Combines build and pack operations.
    /// First performs a rebuild, then creates NuGet packages from the results.
    /// </summary>
    BuildPack,
    
    /// <summary>
    /// Performs a debug build with debug symbols and optimizations disabled.
    /// Suitable for development and debugging scenarios.
    /// </summary>
    Debug,
    
    /// <summary>
    /// Updates NuGet.exe to the latest version.
    /// Ensures the NuGet tooling is current before performing package operations.
    /// </summary>
    NuGetTools,
    
    /// <summary>
    /// Creates installer packages for the application.
    /// Generates installation packages for distribution and deployment.
    /// </summary>
    Installer,
    
    /// <summary>
    /// Creates archive files (ZIP, etc.) of build outputs.
    /// Packages build artifacts into compressed archives for distribution.
    /// </summary>
    CreateArchives,
    
    /// <summary>
    /// Generates documentation from source code.
    /// Creates API documentation and other documentation artifacts.
    /// </summary>
    Documentation,
}

/// <summary>
/// Defines the different modes for NuGet package creation.
/// Each mode determines which packages are created during the pack operation.
/// </summary>
public enum PackMode
{
    /// <summary>
    /// Standard pack mode - Creates standard NuGet packages.
    /// Generates the core packages for normal distribution.
    /// </summary>
    Pack,
    
    /// <summary>
    /// Lite pack mode - Creates minimal NuGet packages.
    /// Generates smaller packages with reduced content for lightweight distribution.
    /// </summary>
    PackLite,
    
    /// <summary>
    /// Complete pack mode - Creates all available NuGet packages.
    /// Generates comprehensive packages including all variants and dependencies.
    /// </summary>
    PackAll
}

/// <summary>
/// Defines the different task pages available in the Modern Build UI.
/// Each page provides access to different sets of build operations and controls.
/// </summary>
public enum TasksPage
{
    /// <summary>
    /// Operations page - Provides access to general build operations.
    /// Contains controls for building, rebuilding, cleaning, and general build tasks.
    /// </summary>
    Ops,
    
    /// <summary>
    /// NuGet page - Provides access to NuGet-specific operations.
    /// Contains controls for package creation, pushing, and NuGet tool management.
    /// </summary>
    NuGet
}

/// <summary>
/// Defines the different NuGet-specific actions available in the NuGet page.
/// Each action represents a specific NuGet operation or combination of operations.
/// </summary>
public enum NuGetAction
{
    /// <summary>
    /// Performs a rebuild followed by package creation.
    /// Combines clean rebuild with NuGet package generation.
    /// </summary>
    RebuildPack,
    
    /// <summary>
    /// Pushes existing packages to the configured NuGet source.
    /// Uploads previously created packages to the package repository.
    /// </summary>
    Push,
    
    /// <summary>
    /// Creates packages and then pushes them to the NuGet source.
    /// Combines package creation with immediate publishing.
    /// </summary>
    PackPush,
    
    /// <summary>
    /// Performs a complete rebuild, package creation, and push sequence.
    /// Full workflow from clean build to published packages.
    /// </summary>
    BuildPackPush,
    
    /// <summary>
    /// Updates NuGet.exe to the latest version.
    /// Ensures the NuGet tooling is current before performing other operations.
    /// </summary>
    Tools
}

/// <summary>
/// Defines the different NuGet package sources available for pushing packages.
/// Each source represents a different package repository or feed.
/// </summary>
public enum NuGetSource
{
    /// <summary>
    /// Uses the default NuGet source configuration.
    /// Relies on the system's default NuGet source settings.
    /// </summary>
    Default,
    
    /// <summary>
    /// Uses the official NuGet.org package repository.
    /// Publishes packages to the public NuGet.org feed.
    /// </summary>
    NuGetOrg,
    
    /// <summary>
    /// Uses GitHub Packages as the NuGet source.
    /// Publishes packages to GitHub's package registry.
    /// </summary>
    GitHub,
    
    /// <summary>
    /// Uses a custom NuGet source URL.
    /// Allows publishing to private or custom package repositories.
    /// </summary>
    Custom
}