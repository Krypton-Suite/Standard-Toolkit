#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Stable identifiers for System Information tree nodes (msinfo32-aligned).
/// </summary>
public static class SystemInformationCategoryId
{
    /// <summary>System Summary.</summary>
    public const string SystemSummary = "system-summary";

    /// <summary>Hardware Resources (folder).</summary>
    public const string HardwareResources = "hardware-resources";

    /// <summary>Hardware Resources / Conflicts/Sharing.</summary>
    public const string HardwareConflicts = "hardware-conflicts";

    /// <summary>Hardware Resources / DMA.</summary>
    public const string HardwareDma = "hardware-dma";

    /// <summary>Hardware Resources / Forced Hardware.</summary>
    public const string HardwareForced = "hardware-forced";

    /// <summary>Hardware Resources / I/O.</summary>
    public const string HardwareIo = "hardware-io";

    /// <summary>Hardware Resources / IRQs.</summary>
    public const string HardwareIrq = "hardware-irq";

    /// <summary>Hardware Resources / Memory.</summary>
    public const string HardwareMemory = "hardware-memory";

    /// <summary>Components (folder).</summary>
    public const string Components = "components";

    /// <summary>Components / Multimedia.</summary>
    public const string ComponentsMultimedia = "components-multimedia";

    /// <summary>Components / Display.</summary>
    public const string ComponentsDisplay = "components-display";

    /// <summary>Components / Infrared.</summary>
    public const string ComponentsInfrared = "components-infrared";

    /// <summary>Components / Input.</summary>
    public const string ComponentsInput = "components-input";

    /// <summary>Components / Modem.</summary>
    public const string ComponentsModem = "components-modem";

    /// <summary>Components / Network.</summary>
    public const string ComponentsNetwork = "components-network";

    /// <summary>Components / Network / Adapter configuration (IP, DHCP).</summary>
    public const string ComponentsNetworkConfiguration = "components-network-configuration";

    /// <summary>Components / Ports.</summary>
    public const string ComponentsPorts = "components-ports";

    /// <summary>Components / Storage.</summary>
    public const string ComponentsStorage = "components-storage";

    /// <summary>Components / Printing.</summary>
    public const string ComponentsPrinting = "components-printing";

    /// <summary>Components / Problem Devices.</summary>
    public const string ComponentsProblemDevices = "components-problem-devices";

    /// <summary>Components / USB.</summary>
    public const string ComponentsUsb = "components-usb";

    /// <summary>Software Environment (folder).</summary>
    public const string SoftwareEnvironment = "software-environment";

    /// <summary>Software Environment / System Drivers.</summary>
    public const string SoftwareDrivers = "software-drivers";

    /// <summary>Software Environment / Environment Variables.</summary>
    public const string SoftwareEnvironmentVariables = "software-environment-variables";

    /// <summary>Software Environment / Running Tasks.</summary>
    public const string SoftwareRunningTasks = "software-running-tasks";

    /// <summary>Software Environment / Loaded Modules.</summary>
    public const string SoftwareLoadedModules = "software-loaded-modules";

    /// <summary>Software Environment / Services.</summary>
    public const string SoftwareServices = "software-services";

    /// <summary>Software Environment / Program Groups.</summary>
    public const string SoftwareProgramGroups = "software-program-groups";

    /// <summary>Software Environment / Startup Programs.</summary>
    public const string SoftwareStartup = "software-startup";

    /// <summary>Software Environment / OLE Registration.</summary>
    public const string SoftwareOle = "software-ole";

    /// <summary>Software Environment / Windows Error Reporting.</summary>
    public const string SoftwareWer = "software-wer";
}
