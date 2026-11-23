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
/// Localisable strings for <see cref="KryptonSystemInformation"/>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonSystemInformationStrings : GlobalId
{
    private const string DEFAULT_WINDOW_TITLE = @"System Information";
    private const string DEFAULT_FIND = @"Find:";
    private const string DEFAULT_COPY = @"Copy";
    private const string DEFAULT_SAVE = @"Save";
    private const string DEFAULT_SAVE_SELECTED = @"Save selected category";
    private const string DEFAULT_SAVE_ALL = @"Save all categories";
    private const string DEFAULT_REFRESH = @"Refresh";
    private const string DEFAULT_WINDOWS_MSINFO = @"Windows System Information...";
    private const string DEFAULT_CLOSE = @"Close";
    private const string DEFAULT_COLLECTING = @"Collecting information...";
    private const string DEFAULT_READY = @"Ready";
    private const string DEFAULT_ACCESS_DENIED = @"Access denied";
    private const string DEFAULT_UNAVAILABLE = @"Unavailable";
    private const string DEFAULT_TIMEOUT = @"The request timed out.";
    private const string DEFAULT_SELECT_CATEGORY = @"Select a category in the tree.";
    private const string DEFAULT_NO_ITEMS = @"(No items)";
    private const string DEFAULT_COLUMN_ITEM = @"Item";
    private const string DEFAULT_COLUMN_VALUE = @"Value";
    private const string DEFAULT_SAVE_FILTER = @"Text files (*.txt)|*.txt|All files (*.*)|*.*";
    private const string DEFAULT_ROW_LIMIT_NOTE = @"Output truncated.";
    private const string DEFAULT_PRINT = @"Print";
    private const string DEFAULT_FIND_NEXT = @"Find next";
    private const string DEFAULT_ALL_MODULES = @"All processes";
    private const string DEFAULT_SAVING = @"Saving...";
    private const string DEFAULT_ITEMS_FORMAT = @"{0} items";
    private const string DEFAULT_CAT_NETWORK_CONFIG = @"Network Configuration";

    private const string DEFAULT_CAT_SYSTEM_SUMMARY = @"System Summary";
    private const string DEFAULT_CAT_HARDWARE = @"Hardware Resources";
    private const string DEFAULT_CAT_CONFLICTS = @"Conflicts/Sharing";
    private const string DEFAULT_CAT_DMA = @"DMA";
    private const string DEFAULT_CAT_FORCED = @"Forced Hardware";
    private const string DEFAULT_CAT_IO = @"I/O";
    private const string DEFAULT_CAT_IRQ = @"IRQs";
    private const string DEFAULT_CAT_MEMORY = @"Memory";
    private const string DEFAULT_CAT_COMPONENTS = @"Components";
    private const string DEFAULT_CAT_MULTIMEDIA = @"Multimedia";
    private const string DEFAULT_CAT_DISPLAY = @"Display";
    private const string DEFAULT_CAT_INFRARED = @"Infrared";
    private const string DEFAULT_CAT_INPUT = @"Input";
    private const string DEFAULT_CAT_MODEM = @"Modem";
    private const string DEFAULT_CAT_NETWORK = @"Network";
    private const string DEFAULT_CAT_PORTS = @"Ports";
    private const string DEFAULT_CAT_STORAGE = @"Storage";
    private const string DEFAULT_CAT_PRINTING = @"Printing";
    private const string DEFAULT_CAT_PROBLEM = @"Problem Devices";
    private const string DEFAULT_CAT_USB = @"USB";
    private const string DEFAULT_CAT_SOFTWARE = @"Software Environment";
    private const string DEFAULT_CAT_DRIVERS = @"System Drivers";
    private const string DEFAULT_CAT_ENV = @"Environment Variables";
    private const string DEFAULT_CAT_TASKS = @"Running Tasks";
    private const string DEFAULT_CAT_MODULES = @"Loaded Modules";
    private const string DEFAULT_CAT_SERVICES = @"Services";
    private const string DEFAULT_CAT_GROUPS = @"Program Groups";
    private const string DEFAULT_CAT_STARTUP = @"Startup Programs";
    private const string DEFAULT_CAT_OLE = @"OLE Registration";
    private const string DEFAULT_CAT_WER = @"Windows Error Reporting";

    private static KryptonSystemInformationStrings? _current;

    /// <summary>Gets the strings used by the System Information UI.</summary>
    public static KryptonSystemInformationStrings Current => _current ??= new KryptonSystemInformationStrings();

    /// <summary>Initializes a new instance of the <see cref="KryptonSystemInformationStrings"/> class.</summary>
    public KryptonSystemInformationStrings() => Reset();

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    /// <summary>Gets a value indicating if all values are default.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDefault =>
        WindowTitle == DEFAULT_WINDOW_TITLE &&
        Find == DEFAULT_FIND &&
        Copy == DEFAULT_COPY &&
        Save == DEFAULT_SAVE;

    /// <summary>Resets all strings to defaults.</summary>
    public void Reset()
    {
        WindowTitle = DEFAULT_WINDOW_TITLE;
        Find = DEFAULT_FIND;
        Copy = DEFAULT_COPY;
        Save = DEFAULT_SAVE;
        SaveSelected = DEFAULT_SAVE_SELECTED;
        SaveAll = DEFAULT_SAVE_ALL;
        Refresh = DEFAULT_REFRESH;
        WindowsSystemInformation = DEFAULT_WINDOWS_MSINFO;
        Close = DEFAULT_CLOSE;
        Collecting = DEFAULT_COLLECTING;
        Ready = DEFAULT_READY;
        AccessDenied = DEFAULT_ACCESS_DENIED;
        Unavailable = DEFAULT_UNAVAILABLE;
        Timeout = DEFAULT_TIMEOUT;
        SelectCategory = DEFAULT_SELECT_CATEGORY;
        NoItems = DEFAULT_NO_ITEMS;
        ColumnItem = DEFAULT_COLUMN_ITEM;
        ColumnValue = DEFAULT_COLUMN_VALUE;
        SaveFilter = DEFAULT_SAVE_FILTER;
        RowLimitNote = DEFAULT_ROW_LIMIT_NOTE;
        Print = DEFAULT_PRINT;
        FindNext = DEFAULT_FIND_NEXT;
        AllProcessModules = DEFAULT_ALL_MODULES;
        Saving = DEFAULT_SAVING;
        ItemsFormat = DEFAULT_ITEMS_FORMAT;
        CategoryNetworkConfiguration = DEFAULT_CAT_NETWORK_CONFIG;
        CategorySystemSummary = DEFAULT_CAT_SYSTEM_SUMMARY;
        CategoryHardwareResources = DEFAULT_CAT_HARDWARE;
        CategoryConflicts = DEFAULT_CAT_CONFLICTS;
        CategoryDma = DEFAULT_CAT_DMA;
        CategoryForcedHardware = DEFAULT_CAT_FORCED;
        CategoryIo = DEFAULT_CAT_IO;
        CategoryIrq = DEFAULT_CAT_IRQ;
        CategoryMemory = DEFAULT_CAT_MEMORY;
        CategoryComponents = DEFAULT_CAT_COMPONENTS;
        CategoryMultimedia = DEFAULT_CAT_MULTIMEDIA;
        CategoryDisplay = DEFAULT_CAT_DISPLAY;
        CategoryInfrared = DEFAULT_CAT_INFRARED;
        CategoryInput = DEFAULT_CAT_INPUT;
        CategoryModem = DEFAULT_CAT_MODEM;
        CategoryNetwork = DEFAULT_CAT_NETWORK;
        CategoryPorts = DEFAULT_CAT_PORTS;
        CategoryStorage = DEFAULT_CAT_STORAGE;
        CategoryPrinting = DEFAULT_CAT_PRINTING;
        CategoryProblemDevices = DEFAULT_CAT_PROBLEM;
        CategoryUsb = DEFAULT_CAT_USB;
        CategorySoftwareEnvironment = DEFAULT_CAT_SOFTWARE;
        CategoryDrivers = DEFAULT_CAT_DRIVERS;
        CategoryEnvironmentVariables = DEFAULT_CAT_ENV;
        CategoryRunningTasks = DEFAULT_CAT_TASKS;
        CategoryLoadedModules = DEFAULT_CAT_MODULES;
        CategoryServices = DEFAULT_CAT_SERVICES;
        CategoryProgramGroups = DEFAULT_CAT_GROUPS;
        CategoryStartupPrograms = DEFAULT_CAT_STARTUP;
        CategoryOleRegistration = DEFAULT_CAT_OLE;
        CategoryWindowsErrorReporting = DEFAULT_CAT_WER;
    }

    /// <summary>Window title.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_WINDOW_TITLE)]
    public string WindowTitle { get; set; } = DEFAULT_WINDOW_TITLE;

    /// <summary>Find label.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_FIND)]
    public string Find { get; set; } = DEFAULT_FIND;

    /// <summary>Copy button.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_COPY)]
    public string Copy { get; set; } = DEFAULT_COPY;

    /// <summary>Save button.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_SAVE)]
    public string Save { get; set; } = DEFAULT_SAVE;

    /// <summary>Save selected category.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_SAVE_SELECTED)]
    public string SaveSelected { get; set; } = DEFAULT_SAVE_SELECTED;

    /// <summary>Save all categories.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_SAVE_ALL)]
    public string SaveAll { get; set; } = DEFAULT_SAVE_ALL;

    /// <summary>Refresh button.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_REFRESH)]
    public string Refresh { get; set; } = DEFAULT_REFRESH;

    /// <summary>Launch native MSInfo32.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_WINDOWS_MSINFO)]
    public string WindowsSystemInformation { get; set; } = DEFAULT_WINDOWS_MSINFO;

    /// <summary>Close button.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CLOSE)]
    public string Close { get; set; } = DEFAULT_CLOSE;

    /// <summary>Status while collecting.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_COLLECTING)]
    public string Collecting { get; set; } = DEFAULT_COLLECTING;

    /// <summary>Ready status.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_READY)]
    public string Ready { get; set; } = DEFAULT_READY;

    /// <summary>Access denied placeholder.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_ACCESS_DENIED)]
    public string AccessDenied { get; set; } = DEFAULT_ACCESS_DENIED;

    /// <summary>Unavailable placeholder.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_UNAVAILABLE)]
    public string Unavailable { get; set; } = DEFAULT_UNAVAILABLE;

    /// <summary>Timeout message.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_TIMEOUT)]
    public string Timeout { get; set; } = DEFAULT_TIMEOUT;

    /// <summary>Prompt to select a category.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_SELECT_CATEGORY)]
    public string SelectCategory { get; set; } = DEFAULT_SELECT_CATEGORY;

    /// <summary>Empty category.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_NO_ITEMS)]
    public string NoItems { get; set; } = DEFAULT_NO_ITEMS;

    /// <summary>Item column header.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_COLUMN_ITEM)]
    public string ColumnItem { get; set; } = DEFAULT_COLUMN_ITEM;

    /// <summary>Value column header.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_COLUMN_VALUE)]
    public string ColumnValue { get; set; } = DEFAULT_COLUMN_VALUE;

    /// <summary>Save file dialog filter.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_SAVE_FILTER)]
    public string SaveFilter { get; set; } = DEFAULT_SAVE_FILTER;

    /// <summary>Note when a list is truncated.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_ROW_LIMIT_NOTE)]
    public string RowLimitNote { get; set; } = DEFAULT_ROW_LIMIT_NOTE;

    /// <summary>Print button.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_PRINT)]
    public string Print { get; set; } = DEFAULT_PRINT;

    /// <summary>Find next category in the tree.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_FIND_NEXT)]
    public string FindNext { get; set; } = DEFAULT_FIND_NEXT;

    /// <summary>Enumerate modules from all processes.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_ALL_MODULES)]
    public string AllProcessModules { get; set; } = DEFAULT_ALL_MODULES;

    /// <summary>Status while saving.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_SAVING)]
    public string Saving { get; set; } = DEFAULT_SAVING;

    /// <summary>Status item count format. {0} is the row count.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_ITEMS_FORMAT)]
    public string ItemsFormat { get; set; } = DEFAULT_ITEMS_FORMAT;

    /// <summary>System Summary node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_SYSTEM_SUMMARY)]
    public string CategorySystemSummary { get; set; } = DEFAULT_CAT_SYSTEM_SUMMARY;

    /// <summary>Hardware Resources node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_HARDWARE)]
    public string CategoryHardwareResources { get; set; } = DEFAULT_CAT_HARDWARE;

    /// <summary>Conflicts node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_CONFLICTS)]
    public string CategoryConflicts { get; set; } = DEFAULT_CAT_CONFLICTS;

    /// <summary>DMA node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_DMA)]
    public string CategoryDma { get; set; } = DEFAULT_CAT_DMA;

    /// <summary>Forced Hardware node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_FORCED)]
    public string CategoryForcedHardware { get; set; } = DEFAULT_CAT_FORCED;

    /// <summary>I/O node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_IO)]
    public string CategoryIo { get; set; } = DEFAULT_CAT_IO;

    /// <summary>IRQs node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_IRQ)]
    public string CategoryIrq { get; set; } = DEFAULT_CAT_IRQ;

    /// <summary>Memory node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_MEMORY)]
    public string CategoryMemory { get; set; } = DEFAULT_CAT_MEMORY;

    /// <summary>Components node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_COMPONENTS)]
    public string CategoryComponents { get; set; } = DEFAULT_CAT_COMPONENTS;

    /// <summary>Multimedia node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_MULTIMEDIA)]
    public string CategoryMultimedia { get; set; } = DEFAULT_CAT_MULTIMEDIA;

    /// <summary>Display node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_DISPLAY)]
    public string CategoryDisplay { get; set; } = DEFAULT_CAT_DISPLAY;

    /// <summary>Infrared node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_INFRARED)]
    public string CategoryInfrared { get; set; } = DEFAULT_CAT_INFRARED;

    /// <summary>Input node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_INPUT)]
    public string CategoryInput { get; set; } = DEFAULT_CAT_INPUT;

    /// <summary>Modem node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_MODEM)]
    public string CategoryModem { get; set; } = DEFAULT_CAT_MODEM;

    /// <summary>Network node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_NETWORK)]
    public string CategoryNetwork { get; set; } = DEFAULT_CAT_NETWORK;

    /// <summary>Network configuration node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_NETWORK_CONFIG)]
    public string CategoryNetworkConfiguration { get; set; } = DEFAULT_CAT_NETWORK_CONFIG;

    /// <summary>Ports node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_PORTS)]
    public string CategoryPorts { get; set; } = DEFAULT_CAT_PORTS;

    /// <summary>Storage node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_STORAGE)]
    public string CategoryStorage { get; set; } = DEFAULT_CAT_STORAGE;

    /// <summary>Printing node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_PRINTING)]
    public string CategoryPrinting { get; set; } = DEFAULT_CAT_PRINTING;

    /// <summary>Problem Devices node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_PROBLEM)]
    public string CategoryProblemDevices { get; set; } = DEFAULT_CAT_PROBLEM;

    /// <summary>USB node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_USB)]
    public string CategoryUsb { get; set; } = DEFAULT_CAT_USB;

    /// <summary>Software Environment node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_SOFTWARE)]
    public string CategorySoftwareEnvironment { get; set; } = DEFAULT_CAT_SOFTWARE;

    /// <summary>Drivers node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_DRIVERS)]
    public string CategoryDrivers { get; set; } = DEFAULT_CAT_DRIVERS;

    /// <summary>Environment Variables node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_ENV)]
    public string CategoryEnvironmentVariables { get; set; } = DEFAULT_CAT_ENV;

    /// <summary>Running Tasks node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_TASKS)]
    public string CategoryRunningTasks { get; set; } = DEFAULT_CAT_TASKS;

    /// <summary>Loaded Modules node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_MODULES)]
    public string CategoryLoadedModules { get; set; } = DEFAULT_CAT_MODULES;

    /// <summary>Services node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_SERVICES)]
    public string CategoryServices { get; set; } = DEFAULT_CAT_SERVICES;

    /// <summary>Program Groups node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_GROUPS)]
    public string CategoryProgramGroups { get; set; } = DEFAULT_CAT_GROUPS;

    /// <summary>Startup Programs node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_STARTUP)]
    public string CategoryStartupPrograms { get; set; } = DEFAULT_CAT_STARTUP;

    /// <summary>OLE Registration node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_OLE)]
    public string CategoryOleRegistration { get; set; } = DEFAULT_CAT_OLE;

    /// <summary>Windows Error Reporting node.</summary>
    [Localizable(true)]
    [DefaultValue(DEFAULT_CAT_WER)]
    public string CategoryWindowsErrorReporting { get; set; } = DEFAULT_CAT_WER;
}
