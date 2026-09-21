#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>Localisable strings for <see cref="KryptonLogViewer"/>.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonLogViewerStrings : GlobalId
{
    private const string DefaultWindowTitle = @"Application Log";
    private const string DefaultLevel = @"Level:";
    private const string DefaultCategory = @"Category:";
    private const string DefaultSearch = @"Search:";
    private const string DefaultLiveTail = @"Live tail";
    private const string DefaultExport = @"Export…";
    private const string DefaultClose = @"Close";
    private const string DefaultColumnTime = @"Time";
    private const string DefaultColumnLevel = @"Level";
    private const string DefaultColumnCategory = @"Category";
    private const string DefaultColumnMessage = @"Message";
    private const string DefaultViewLog = @"View Log";
    private const string DefaultIncludeLog = @"Include application log";
    private const string DefaultRecentLogHeader = @"Recent log:";
    private const string DefaultNoMemorySink = @"No memory sink is configured. Call WriteTo.Memory() in KryptonLog.Configure.";
    private const string DefaultExportFilter = @"Log files (*.log)|*.log|Text files (*.txt)|*.txt|All files (*.*)|*.*";
    private const string DefaultExportTitle = @"Export log";
    private const string DefaultAllLevels = @"All";

    /// <summary>Initializes a new instance of the <see cref="KryptonLogViewerStrings"/> class.</summary>
    public KryptonLogViewerStrings() => Reset();

    /// <inheritdoc />
    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    /// <summary>Gets a value indicating whether all values are default.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDefault =>
        WindowTitle == DefaultWindowTitle
        && Level == DefaultLevel
        && Category == DefaultCategory
        && Search == DefaultSearch
        && LiveTail == DefaultLiveTail
        && Export == DefaultExport
        && Close == DefaultClose
        && ColumnTime == DefaultColumnTime
        && ColumnLevel == DefaultColumnLevel
        && ColumnCategory == DefaultColumnCategory
        && ColumnMessage == DefaultColumnMessage
        && ViewLog == DefaultViewLog
        && IncludeLog == DefaultIncludeLog
        && RecentLogHeader == DefaultRecentLogHeader
        && NoMemorySink == DefaultNoMemorySink
        && ExportFilter == DefaultExportFilter
        && ExportTitle == DefaultExportTitle
        && AllLevels == DefaultAllLevels;

    /// <summary>Resets all strings to defaults.</summary>
    public void Reset()
    {
        WindowTitle = DefaultWindowTitle;
        Level = DefaultLevel;
        Category = DefaultCategory;
        Search = DefaultSearch;
        LiveTail = DefaultLiveTail;
        Export = DefaultExport;
        Close = DefaultClose;
        ColumnTime = DefaultColumnTime;
        ColumnLevel = DefaultColumnLevel;
        ColumnCategory = DefaultColumnCategory;
        ColumnMessage = DefaultColumnMessage;
        ViewLog = DefaultViewLog;
        IncludeLog = DefaultIncludeLog;
        RecentLogHeader = DefaultRecentLogHeader;
        NoMemorySink = DefaultNoMemorySink;
        ExportFilter = DefaultExportFilter;
        ExportTitle = DefaultExportTitle;
        AllLevels = DefaultAllLevels;
    }

    /// <summary>Gets or sets the viewer window title.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultWindowTitle)]
    public string WindowTitle { get; set; }

    /// <summary>Gets or sets the level filter label.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultLevel)]
    public string Level { get; set; }

    /// <summary>Gets or sets the category filter label.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultCategory)]
    public string Category { get; set; }

    /// <summary>Gets or sets the search label.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultSearch)]
    public string Search { get; set; }

    /// <summary>Gets or sets the live-tail checkbox text.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultLiveTail)]
    public string LiveTail { get; set; }

    /// <summary>Gets or sets the export button text.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultExport)]
    public string Export { get; set; }

    /// <summary>Gets or sets the close button text.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultClose)]
    public string Close { get; set; }

    /// <summary>Gets or sets the time column header.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultColumnTime)]
    public string ColumnTime { get; set; }

    /// <summary>Gets or sets the level column header.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultColumnLevel)]
    public string ColumnLevel { get; set; }

    /// <summary>Gets or sets the category column header.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultColumnCategory)]
    public string ColumnCategory { get; set; }

    /// <summary>Gets or sets the message column header.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultColumnMessage)]
    public string ColumnMessage { get; set; }

    /// <summary>Gets or sets the exception-dialog View Log button text.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultViewLog)]
    public string ViewLog { get; set; }

    /// <summary>Gets or sets the bug-report include-log checkbox text.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultIncludeLog)]
    public string IncludeLog { get; set; }

    /// <summary>Gets or sets the header prepended when copying recent log lines.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultRecentLogHeader)]
    public string RecentLogHeader { get; set; }

    /// <summary>Gets or sets the message shown when no memory sink is configured.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultNoMemorySink)]
    public string NoMemorySink { get; set; }

    /// <summary>Gets or sets the export file dialog filter.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultExportFilter)]
    public string ExportFilter { get; set; }

    /// <summary>Gets or sets the export file dialog title.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultExportTitle)]
    public string ExportTitle { get; set; }

    /// <summary>Gets or sets the "all levels" combo item.</summary>
    [Localizable(true)]
    [DefaultValue(DefaultAllLevels)]
    public string AllLevels { get; set; }
}
