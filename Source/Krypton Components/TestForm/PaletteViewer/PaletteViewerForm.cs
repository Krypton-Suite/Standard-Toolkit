#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

using System.IO;

namespace TestForm;

public partial class PaletteViewerForm : KryptonForm
{
    private readonly List<PaletteBase> _palettes = new List<PaletteBase>();
    private List<System.Reflection.MethodInfo>? _apiCalls;
    private Dictionary<System.Reflection.MemberInfo, string>? _methodEnumMapping;
    private SchemeBaseColors[]? _enumValues;
    private Color[]? _baselineColors;
    private string? _baselinePaletteName;
    private string? _sourcePath;
    private bool _isSourceValid;
    private System.Threading.CancellationTokenSource? _cancellationToken;
    private int _highlightRowIndex = -1;
    private bool _bulkUpdating;
    private const int MinColumnWidth = 120;
    private readonly WindowStateStore _winStore;
    private readonly WindowStateInfo? _winInfo;
    private static readonly IReadOnlyDictionary<string, PaletteMode> DisplayToEnum = PaletteModeStrings.SupportedThemesMap;
    private static readonly Dictionary<PaletteMode, string> EnumToDisplay = new Dictionary<PaletteMode, string>(Enumerable.ToDictionary(DisplayToEnum, kv => kv.Value, kv => kv.Key));
    private readonly HashSet<Type> _processedBases = new HashSet<Type>();
    private KryptonManager? _kryptonManager1;

    // Add undo stack for colour edits
    private readonly Stack<UndoItem> _undoStack = new Stack<UndoItem>();

    // Active filter state
    private Color? _activeColourFilter;
    private string? _activeNameFilter;
    private string? _lastColorFilterInput;

    private ContextMenuStrip? _contextMenu;

    private readonly struct UndoItem
    {
        public PaletteBase Palette { get; }
        public SchemeBaseColors ColorEnum { get; }
        public Color OldColor { get; }
        public int RowIndex { get; }
        public int ColumnIndex { get; }

        public UndoItem(PaletteBase palette, SchemeBaseColors colorEnum, Color oldColor, int rowIndex, int columnIndex)
        {
            Palette = palette;
            ColorEnum = colorEnum;
            OldColor = oldColor;
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }
    }

    private static readonly PaletteMode[] LegacyProfessionalModes = new[]
    {
        PaletteMode.ProfessionalSystem,
        PaletteMode.ProfessionalOffice2003
    };

    private static bool IsSparkleDisplay(string display) => display.StartsWith("Sparkle", StringComparison.OrdinalIgnoreCase);
    private static bool IsSparkleMode(PaletteMode mode) => mode.ToString().StartsWith("Sparkle", StringComparison.OrdinalIgnoreCase);
    private static bool IsSparkleType(Type t) => t.Name.StartsWith("PaletteSparkle", StringComparison.OrdinalIgnoreCase);

    private static bool IsLegacyProfessionalMode(PaletteMode mode) => Array.IndexOf(LegacyProfessionalModes, mode) >= 0;
    private static bool IsLegacyProfessionalDisplay(string display) => DisplayToEnum.TryGetValue(display, out var m) && IsLegacyProfessionalMode(m);
    private static bool IsLegacyProfessionalType(Type t) => t.Name.StartsWith("PaletteProfessional", StringComparison.OrdinalIgnoreCase);

    private static bool IsProblematicBaseMethod(System.Reflection.MethodInfo m)
    {
        if (m.DeclaringType == null)
        {
            return false;
        }

        string typeName = m.DeclaringType.Name;

        // Exclude any GetRibbonBackColor* method declared on a *Base palette class,
        // as many of these throw or show dialogs for unsupported styles.
        if (typeName.EndsWith("Base", StringComparison.Ordinal) &&
            m.Name.StartsWith("GetRibbonBackColor", StringComparison.Ordinal))
        {
            return true;
        }

        // Add other known problematic patterns here if discovered

        return false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaletteViewerForm"/> class.
    /// </summary>
    public PaletteViewerForm()
    {
        InitializeComponent();

        SetupContextMenu();

        // load persisted window state
        _winStore = new WindowStateStore();
        _winInfo = _winStore.Load() ?? new WindowStateInfo();

        PopulateThemeCombo(_winInfo.LastTheme);

        _sourcePath = _winInfo.SourcePath;
        _isSourceValid = IsValidSourcePath(_sourcePath);
        if (textSourcePath != null)
        {
            textSourcePath.Text = _sourcePath ?? string.Empty;
        }

        Rectangle workArea = Screen.PrimaryScreen!.WorkingArea;

        if (_winInfo != null)
        {
            StartPosition = FormStartPosition.Manual;
            Left = _winInfo.Left;
            Top = _winInfo.Top;

            var screen = Screen.FromPoint(new Point(_winInfo.Left, _winInfo.Top));
            var wa = screen.WorkingArea;
            Width = Math.Min(_winInfo.Width, wa.Width);
            Height = Math.Min(_winInfo.Height, wa.Height);

            var st = _winInfo.State == FormWindowState.Minimized ? FormWindowState.Normal : _winInfo.State;
            WindowState = st;
        }
        else
        {
            Size = new Size(workArea.Width / 2, workArea.Height / 2);
            Location = new Point((workArea.Width - Width) / 2, (workArea.Height - Height) / 2);
        }

        FormClosing += MainForm_FormClosing;

        UpdateStatus("Ready");

        UpdateUIState();

        AttachKryptonManager(new KryptonManager());
    }

    /// <summary>
    /// Determines whether the provided toolkit root path is structurally valid.
    /// </summary>
    private static bool IsValidSourcePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
        {
            return false;
        }

        // Must contain top-level "Source" folder.
        var sourceDir = Path.Combine(path, "Source");
        if (!Directory.Exists(sourceDir))
        {
            return false;
        }

        // Required Enumerations folder and Microsoft 365 folder.
        var enumDir = Path.Combine(sourceDir, "Krypton Components", "Krypton.Toolkit", "Palette Builtin", "Enumerations");
        var m365Dir = Path.Combine(sourceDir, "Krypton Components", "Krypton.Toolkit", "Palette Builtin", "Microsoft 365");
        if (!Directory.Exists(enumDir) || !Directory.Exists(m365Dir))
        {
            return false;
        }

        // Required file PaletteEnumerations.cs inside Enumerations folder.
        var enumFile = Path.Combine(enumDir, "PaletteEnumerations.cs");
        if (!File.Exists(enumFile))
        {
            return false;
        }

        return true;
    }

    public void AttachKryptonManager(KryptonManager manager)
    {
        _kryptonManager1 = manager;
        UpdateThemeSwitcher();
    }

    private void UpdateThemeSwitcher()
    {
        if (_kryptonManager1 is not null
            && kryptonThemeComboBox is not null
            && _kryptonManager1.GlobalPaletteMode != PaletteMode.Custom
            && EnumToDisplay.TryGetValue(_kryptonManager1.GlobalPaletteMode, out string? disp)
            && disp is not null)
        {
            kryptonThemeComboBox.SelectedItem = disp;
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        UpdateThemeSwitcher();
    }

    private void LoadApiCalls()
    {
        if (_apiCalls != null)
        {
            return;
        }

        var baseType = typeof(PaletteMicrosoft365Base);
        var methods = baseType.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

        var list = new List<System.Reflection.MethodInfo>();

        foreach (var m in methods)
        {
            if (m.IsSpecialName
                || IsProblematicBaseMethod(m)
                || m.ReturnType != typeof(Color))
            {
                continue;
            }

            // Skip methods with ref/out parameters or open generics
            bool skip = false;
            foreach (var p in m.GetParameters())
            {
                if (p.IsOut || p.ParameterType.IsByRef)
                {
                    skip = true;
                    break;
                }
            }

            if (skip)
            {
                continue;
            }

            list.Add(m);
        }

        _apiCalls = list;

        // prepare enum values array
        _enumValues = (SchemeBaseColors[])Enum.GetValues(typeof(SchemeBaseColors));

        BuildBaseRows();
    }

    private void BuildBaseRows()
    {
        dataGridViewPalette.Columns.Clear();
        dataGridViewPalette.Rows.Clear();

        // Column 0: Enum Index
        var colIndex = new DataGridViewTextBoxColumn();
        colIndex.HeaderText = @"#";
        colIndex.Name = "colIndex";
        colIndex.ReadOnly = true;
        colIndex.Frozen = true;
        colIndex.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        colIndex.MinimumWidth = 50;
        colIndex.SortMode = DataGridViewColumnSortMode.NotSortable;
        dataGridViewPalette.Columns.Add(colIndex);

        // Column 1: Enum Name
        var colEnum = new DataGridViewTextBoxColumn();
        colEnum.HeaderText = @"SchemeBaseColors";
        colEnum.Name = "colEnum";
        colEnum.ReadOnly = true;
        colEnum.Frozen = true;
        colEnum.SortMode = DataGridViewColumnSortMode.NotSortable;
        dataGridViewPalette.Columns.Add(colEnum);

        // Column 2: API Call(s)
        var colApi = new DataGridViewTextBoxColumn();
        colApi.HeaderText = @"API Call(s)";
        colApi.Name = "colApi";
        colApi.ReadOnly = true;
        colApi.MinimumWidth = 150;
        colApi.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        colApi.SortMode = DataGridViewColumnSortMode.NotSortable;
        colApi.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        colApi.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
        colApi.Frozen = true;
        dataGridViewPalette.Columns.Add(colApi);

        if (_enumValues == null)
        {
            return;
        }

        for (int idx = 0; idx < _enumValues.Length; idx++)
        {
            int rowIndex = dataGridViewPalette.Rows.Add();
            var row = dataGridViewPalette.Rows[rowIndex];
            row.Cells[0].Value = idx; // index column
            row.Cells[1].Value = _enumValues[idx].ToString();
            row.Cells[2].Value = string.Empty; // API mapping
        }

        // Auto-resize columns once, then allow manual resize
        dataGridViewPalette.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        foreach (DataGridViewColumn col in dataGridViewPalette.Columns)
        {
            if (col.Name != "colApi")
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
        }

        UpdateUIState();
    }

    private void AdjustRowHeights()
    {
        const int padding = 4;

        int singleLineHeight = 22;

        foreach (DataGridViewRow row in dataGridViewPalette.Rows)
        {
            if (row.IsNewRow)
            {
                continue;
            }

            var cell = row.Cells.Count > 2 ? row.Cells[2] : null; // API column
            int lineCount = 1;
            if (cell?.Value is string { Length: > 0 } txt)
            {
                lineCount = txt.Split('\n').Length;
            }

            row.Height = (singleLineHeight * lineCount) + padding;
        }
    }

    private void BuildMethodEnumMapping(PaletteBase? palette)
    {
        if (palette == null)
        {
            return;
        }

        var mapping = new Dictionary<System.Reflection.MemberInfo, string>();

        Color[]? colorArray = TryGetPaletteColors(palette) ?? [];

        if (colorArray == null || colorArray.Length == 0)
        {
            _methodEnumMapping = mapping;
            return;
        }

        // 1. Try to map public color-returning methods (including overrides)
        var paletteMethods = palette.GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
        foreach (var m in paletteMethods)
        {
            if (m.IsSpecialName || m.ReturnType != typeof(Color) || (IsProblematicBaseMethod(m)))
            {
                continue;
            }

            // Skip methods with ref/out parameters
            bool skip = false;
            foreach (var p in m.GetParameters())
            {
                if (p.IsOut || p.ParameterType.IsByRef)
                {
                    skip = true;
                    break;
                }
            }
            if (skip)
            {
                continue;
            }

            Color resultColor;
            try
            {
                var parameters = m.GetParameters();
                object?[] args = new object[parameters.Length];
                // Use the first enum value or default struct instance as placeholder argument
                for (int idx = 0; idx < parameters.Length; idx++)
                {
                    var pt = parameters[idx].ParameterType;
                    if (pt.IsEnum)
                    {
                        args[idx] = Enum.GetValues(pt).GetValue(0)!;
                    }
                    else if (pt.IsValueType)
                    {
                        args[idx] = Activator.CreateInstance(pt)!;
                    }
                    else
                    {
                        args[idx] = null;
                    }
                }

                resultColor = (Color)m.Invoke(palette, args)!;
            }
            catch (System.Reflection.TargetInvocationException tie)
            {
                // Ignore known palette issues that manifest as exceptions
                if (tie.InnerException is NotImplementedException ||
                    tie.InnerException is ArgumentOutOfRangeException ||
                    tie.InnerException is IndexOutOfRangeException)
                {
                    continue;
                }

                continue;
            }

            // Match against array to find enum slot
            for (int i = 0; i < colorArray.Length; i++)
            {
                if (colorArray[i].ToArgb() == resultColor.ToArgb())
                {
                    if (Enum.IsDefined(typeof(SchemeBaseColors), i))
                    {
                        var enumName = ((SchemeBaseColors)i).ToString();
                        mapping[m] = enumName;
                    }
                    break;
                }
            }
        }

        // Map ColorTable properties as well
        if (palette.ColorTable != null)
        {
            var ctProps = palette.ColorTable.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (var prop in ctProps)
            {
                if (prop.PropertyType != typeof(Color) || prop.GetIndexParameters().Length > 0)
                {
                    continue;
                }

                Color c;
                try
                {
                    c = (Color)prop.GetValue(palette.ColorTable)!;
                }
                catch
                {
                    continue;
                }

                for (int i = 0; i < colorArray.Length; i++)
                {
                    if (colorArray[i].ToArgb() == c.ToArgb())
                    {
                        if (Enum.IsDefined(typeof(SchemeBaseColors), i))
                        {
                            var enumName = ((SchemeBaseColors)i).ToString();
                            mapping[prop] = enumName;
                        }
                        break;
                    }
                }
            }
        }

        MergeEnumMethodMapping(mapping);

        // Update grid mapping column
        if (_enumValues != null)
        {
            // create reverse map enumName -> list of methods
            var enumMethodMap = new Dictionary<string, List<string>>();
            foreach (var kvp in mapping)
            {
                var enumName = kvp.Value;
                if (!enumMethodMap.TryGetValue(enumName, out var list))
                {
                    list = new List<string>();
                    enumMethodMap.Add(enumName, list);
                }
                list.Add(kvp.Key.Name);
            }

            for (int i = 0; i < _enumValues.Length; i++)
            {
                var enName = _enumValues[i].ToString();
                if (enumMethodMap.TryGetValue(enName, out var mlist))
                {
                    var joined = string.Join("\n", mlist);
                    var apiCell = dataGridViewPalette.Rows[i].Cells[2];
                    apiCell.Value = joined;
                    apiCell.Tag = mlist.Count > 1;
                }
            }
        }

        UpdateUIState();
    }

    private void AddPalette(PaletteBase palette)
    {
        if (palette == null)
        {
            return;
        }

        var modeForPalette = KryptonManager.GetModeForPalette(palette);
        if (IsSparkleMode(modeForPalette) || IsSparkleType(palette.GetType()) || IsLegacyProfessionalMode(modeForPalette) || IsLegacyProfessionalType(palette.GetType()))
        {
            return; // skip unsupported palette types
        }

        _palettes.Add(palette);

        // Build/merge mapping for each unique palette base (class that owns the _ribbonColors field)
        var baseOwner = GetRibbonColorsOwner(palette.GetType());
        if (baseOwner != null && !_processedBases.Contains(baseOwner))
        {
            BuildMethodEnumMapping(palette);
            _processedBases.Add(baseOwner);
        }

        // Add column
        var rawHeader = modeForPalette != PaletteMode.Custom ? GetDisplayName(modeForPalette) : palette.GetType().Name;
        var baseHeader = BreakHeader(rawHeader);

        var col = new DataGridViewTextBoxColumn();
        col.HeaderText = baseHeader;
        // Use full type name for unique identification across namespaces
        col.Name = palette.GetType().FullName;
        col.ReadOnly = false;
        col.SortMode = DataGridViewColumnSortMode.NotSortable;
        col.HeaderCell.Style.WrapMode = DataGridViewTriState.True;
        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        dataGridViewPalette.Columns.Add(col);

        int paletteColumnIndex = dataGridViewPalette.Columns.Count - 1;

        // populate enum rows with color values

        // fetch palette color array
        Color[] paletteColors = TryGetPaletteColors(palette) ?? [];

        // Assign baseline if not yet set
        if (_baselineColors == null && paletteColors != null)
        {
            _baselineColors = paletteColors;
            _baselinePaletteName = palette.GetType().Name;
        }

        int missingCount = 0;
        bool sparsePalette = paletteColors == null || paletteColors.Length < _enumValues!.Length / 3; // heuristics for incompatible palettes

        string headerSuffix = string.Empty;

        if (_enumValues != null)
        {
            for (int i = 0; i < _enumValues.Length; i++)
            {
                var row = dataGridViewPalette.Rows[i];
                bool indexPresent = paletteColors is { Length: > 0 }
                                    && i >= 0
                                    && i < paletteColors.Length;

                Color color = indexPresent ? paletteColors![i] : Color.Transparent;
                if (!indexPresent)
                {
                    // count missing only if palette is expected to support full scheme
                    if (!sparsePalette)
                    {
                        missingCount++;
                        if (string.IsNullOrEmpty(row.Cells[paletteColumnIndex].ErrorText))
                        {
                            row.Cells[paletteColumnIndex].ErrorText = "Missing index";
                        }
                    }
                }
                else if (color.A == 0)
                {
                    // Intentionally empty – mark as warning (italic text) but no error icon
                    string label = color == Color.Transparent ? "Transparent" : "EMPTY";
                    row.Cells[paletteColumnIndex].Value = label;
                    row.Cells[paletteColumnIndex].Style.Font = new Font(Font, FontStyle.Italic);
                }

                var cell = row.Cells[paletteColumnIndex];
                var textColor = ContrastColor(color);
                cell.Style.BackColor = color;
                cell.Style.ForeColor = textColor;
                cell.Style.SelectionBackColor = AdjustSelectionBack(color);
                cell.Style.SelectionForeColor = textColor;
                cell.Value = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");

                cell.ToolTipText = palette.GetType().Name + " - " + _enumValues[i];

                // Legacy fallback removed – handled below for missing or intentional transparent values
            }
        }

        // Static source check for array vs enum
        if (!string.IsNullOrWhiteSpace(_sourcePath))
        {
            var issues = Classes.ThemeArrayInspector.GetIssues(palette.GetType(), _sourcePath ?? string.Empty);
            if (issues is { IsClean: false })
            {
                foreach (int idx in issues.MissingIndices)
                {
                    if (idx >= 0 && idx < dataGridViewPalette.Rows.Count)
                    {
                        var c = dataGridViewPalette.Rows[idx].Cells[paletteColumnIndex];
                        if (string.IsNullOrEmpty(c.ErrorText))
                        {
                            c.ErrorText = "Missing value";
                        }

                        if (c.ToolTipText == null || !c.ToolTipText.Contains(c.ErrorText))
                        {
                            c.ToolTipText += " : " + c.ErrorText;
                        }
                    }
                }

                foreach (int idx in issues.UnlabelledIndices)
                {
                    if (idx >= 0 && idx < dataGridViewPalette.Rows.Count)
                    {
                        var c = dataGridViewPalette.Rows[idx].Cells[paletteColumnIndex];
                        if (string.IsNullOrEmpty(c.ErrorText))
                        {
                            c.ErrorText = "Unmarked value";
                        }

                        if (c.ToolTipText == null || !c.ToolTipText.Contains(c.ErrorText))
                        {
                            c.ToolTipText += " : " + c.ErrorText;
                        }
                    }
                }

                foreach (int idx in issues.OutOfOrderIndices)
                {
                    if (idx >= 0 && idx < dataGridViewPalette.Rows.Count)
                    {
                        var c = dataGridViewPalette.Rows[idx].Cells[paletteColumnIndex];
                        if (string.IsNullOrEmpty(c.ErrorText))
                        {
                            c.ErrorText = "Out-of-order";
                        }

                        if (c.ToolTipText == null || !c.ToolTipText.Contains(c.ErrorText))
                        {
                            c.ToolTipText += " : " + c.ErrorText;
                        }
                    }
                }

                foreach (int idx in issues.ExtraIndices)
                {
                    if (idx >= 0 && idx < dataGridViewPalette.Rows.Count)
                    {
                        var c = dataGridViewPalette.Rows[idx].Cells[paletteColumnIndex];
                        if (string.IsNullOrEmpty(c.ErrorText))
                        {
                            c.ErrorText = "Extra entry";
                        }

                        if (c.ToolTipText == null || !c.ToolTipText.Contains(c.ErrorText))
                        {
                            c.ToolTipText += " : " + c.ErrorText;
                        }
                    }
                }

                headerSuffix = $"\nΔ {issues.MissingCount}/{issues.UnlabelledCount}/{issues.OutOfOrderCount}/{issues.ExtraCount}";
            }
        }

        // Update column header with summary
        var colHeader = baseHeader;

        if (sparsePalette)
        {
            colHeader += "\n(incompatible)";
        }
        else if (missingCount > 0)
        {
            colHeader += $"\n⚠{missingCount} missing";
        }

        colHeader += headerSuffix;

        dataGridViewPalette.Columns[paletteColumnIndex].HeaderText = colHeader;
        dataGridViewPalette.Columns[paletteColumnIndex].HeaderCell.Style.WrapMode = DataGridViewTriState.True;
        dataGridViewPalette.Columns[paletteColumnIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        // Recalculate header height to accommodate up to 3 lines
        dataGridViewPalette.AutoResizeColumnHeadersHeight();

        // Auto-size new column to contents once, then freeze
        int preferred = col.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
        preferred = Math.Max(MinColumnWidth, Math.Min(preferred, 300));
        col.Width = preferred;
        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

        // ensure new column is not sortable
        col.SortMode = DataGridViewColumnSortMode.NotSortable;

        // After populating all rows for this palette, adjust row heights only if not in bulk update
        if (!_bulkUpdating)
        {
            AdjustRowHeights();
        }

        UpdateUIState();
    }

    private static Color ContrastColor(Color c)
    {
        // If fully transparent, fallback to default grid text colour (black)
        if (c.A == 0)
        {
            return Color.Black;
        }

        // Convert sRGB to linear RGB
        double R = c.R / 255.0;
        double G = c.G / 255.0;
        double B = c.B / 255.0;

        R = R <= 0.03928 ? R / 12.92 : Math.Pow((R + 0.055) / 1.055, 2.4);
        G = G <= 0.03928 ? G / 12.92 : Math.Pow((G + 0.055) / 1.055, 2.4);
        B = B <= 0.03928 ? B / 12.92 : Math.Pow((B + 0.055) / 1.055, 2.4);

        // Calculate relative luminance
        double L = 0.2126 * R + 0.7152 * G + 0.0722 * B;

        // Choose foreground that meets WCAG 2.0 contrast ratio of at least 4.5:1
        // For white text contrast ratio is (1.0 + 0.05) / (L + 0.05)
        // For black text contrast ratio is (L + 0.05) / 0.05
        // White is better if background luminance <= 0.179
        return L <= 0.179 ? Color.White : Color.Black;
    }

    private static Color AdjustSelectionBack(Color c)
    {
        // If color is dark, lighten; if light, darken.
        double factor = 0.3; // 30% difference
        bool isLight = ((c.R * 299 + c.G * 587 + c.B * 114) / 1000) > 128;
        int r = isLight ? (int)(c.R * (1 - factor)) : (int)(c.R + (255 - c.R) * factor);
        int g = isLight ? (int)(c.G * (1 - factor)) : (int)(c.G + (255 - c.G) * factor);
        int b = isLight ? (int)(c.B * (1 - factor)) : (int)(c.B + (255 - c.B) * factor);
        return Color.FromArgb(c.A, r, g, b);
    }

    private void BtnAddPalette_Click(object sender, EventArgs e)
    {
        LoadApiCalls();

        buttonAddPalette.Enabled = false;
        buttonRemovePalette.Enabled = false;
        Cursor = Cursors.WaitCursor;
        UpdateStatus("Adding palette...");
        Application.DoEvents();

        try
        {
            if (comboTheme.SelectedItem is string display && DisplayToEnum.TryGetValue(display, out PaletteMode mode))
            {
                var palette = KryptonManager.GetPaletteForMode(mode);

                if (!_palettes.Contains(palette))
                {
                    AddPalette(palette);
                }
            }
        }
        finally
        {
            UpdateStatus("Ready");
            Cursor = Cursors.Default;
            // State recalculated based on current validity
            UpdateUIState();
        }
    }

    private void BtnRemovePalette_Click(object sender, EventArgs e)
    {
        if (!(comboTheme.SelectedItem is string display) || !DisplayToEnum.TryGetValue(display, out PaletteMode mode))
        {
            return; // nothing selected
        }

        var palette = _palettes.Find(p => KryptonManager.GetModeForPalette(p) == mode);
        if (palette == null)
        {
            return; // selected palette not loaded
        }

        // remove from list
        _palettes.Remove(palette);

        // find column
        var column = dataGridViewPalette.Columns.GetColumnCount(DataGridViewElementStates.Visible) > 0 ? dataGridViewPalette.Columns[palette.GetType().FullName!] : null;
        if (column != null)
        {
            dataGridViewPalette.Columns.Remove(column);
        }

        UpdateUIState();
    }

    private void PopulateThemeCombo(string preferredDisplay = "")
    {
        foreach (var kvp in DisplayToEnum)
        {
            if (kvp.Value == PaletteMode.Custom)
            {
                continue;
            }

            if (IsSparkleDisplay(kvp.Key) || IsSparkleMode(kvp.Value))
            {
                continue; // skip sparkle
            }

            if (IsLegacyProfessionalDisplay(kvp.Key) || IsLegacyProfessionalMode(kvp.Value))
            {
                continue; // skip legacy professional themes
            }

            comboTheme.Items.Add(kvp.Key);
        }

        if (!string.IsNullOrEmpty(preferredDisplay) && comboTheme.Items.Contains(preferredDisplay))
        {
            comboTheme.SelectedItem = preferredDisplay;
        }
    }

    private void ComboTheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateUIState();
    }

    private async void BtnAddAll_Click(object sender, EventArgs e)
    {
        // Ensure base rows and enum array are ready
        LoadApiCalls();

        if (_cancellationToken != null)
        {
            return; // already running
        }

        _cancellationToken = new System.Threading.CancellationTokenSource();
        var token = _cancellationToken.Token;

        buttonAddAll.Enabled = false;
        buttonAddPalette.Enabled = false;
        buttonRemovePalette.Enabled = false;
        comboTheme.Enabled = false;
        buttonCancel.Visible = true;

        UpdateStatus("Adding all palettes...");

        // Force a gui refresh before the adding starts
        Refresh();

        // Begin bulk update
        _bulkUpdating = true;
        dataGridViewPalette.SuspendLayout();

        try
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                var modes = (PaletteMode[])Enum.GetValues(typeof(PaletteMode));
                foreach (var mode in modes)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }

                    if (mode == PaletteMode.Custom || IsSparkleMode(mode) || IsLegacyProfessionalMode(mode))
                    {
                        continue; // skip unsupported modes
                    }

                    var palette = KryptonManager.GetPaletteForMode(mode);

                    Invoke((Action)(() =>
                    {
                        if (!_palettes.Contains(palette))
                        {
                            AddPalette(palette);
                            UpdateStatus($"Added {mode}...");
                        }
                    }));
                }
            }, token);
        }
        catch (OperationCanceledException)
        {
            UpdateStatus("Add all cancelled.");
        }
        finally
        {
            // End bulk update
            _bulkUpdating = false;
            dataGridViewPalette.ResumeLayout();

            // Perform final row-height adjustment once after bulk add completes
            AdjustRowHeights();

            buttonAddAll.Enabled = true;
            buttonAddPalette.Enabled = true;
            buttonRemovePalette.Enabled = true;
            comboTheme.Enabled = true;
            buttonCancel.Visible = false;

            _cancellationToken.Dispose();
            _cancellationToken = null;

            UpdateStatus("Ready");

            UpdateUIState();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        _cancellationToken?.Cancel();
    }

    private void UpdateStatus(string message)
    {
        if (InvokeRequired)
        {
            BeginInvoke((Action)(() => UpdateStatus(message)));
            return;
        }

        statusLabel.Text = message;
    }

    private void DataGridViewPalette_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
    {
        // auto-size once then freeze
        int preferred = e.Column.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
        preferred = Math.Max(MinColumnWidth, Math.Min(preferred, 300));
        e.Column.Width = preferred;
        if (e.Column.Index != 2)
        {
            e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }
        else
        {
            e.Column.Frozen = true;
        }
        e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;

        // Add tooltip for palette/theme columns to indicate activation behaviour
        if (e.Column.Index >= 3)
        {
            e.Column.HeaderCell.ToolTipText = "Click to activate";
        }
    }

    private void DataGridViewPalette_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.ColumnIndex < 3)
        {
            return;
        }

        var column = dataGridViewPalette.Columns[e.ColumnIndex];
        var palette = _palettes.Find(p => p.GetType().FullName == column.Name);

        // New guard: do not activate palettes that still have missing enum values
        if (palette != null && !string.IsNullOrWhiteSpace(_sourcePath))
        {
            var issues = Classes.ThemeArrayInspector.GetIssues(palette.GetType(), _sourcePath ?? string.Empty);
            if (issues is { MissingCount: > 0 })
            {
                UpdateStatus($"Cannot activate {palette.GetType().Name}: {issues.MissingCount} missing enum colours");
                return;
            }
        }

        if (palette != null)
        {
            var mode = KryptonManager.GetModeForPalette(palette);
            if (mode != PaletteMode.Custom)
            {
                _kryptonManager1?.GlobalPaletteMode = mode;
            }
            else if (palette is KryptonCustomPaletteBase cp)
            {
                _kryptonManager1?.GlobalPaletteMode = PaletteMode.Custom;
                _kryptonManager1?.GlobalCustomPalette = cp;
            }

            string headerFlat = column.HeaderText.Replace("\n", " ").Replace("\r", " ");
            UpdateStatus($"Activated {headerFlat}");
        }
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        _palettes.Clear();
        _baselineColors = null;
        _baselinePaletteName = null;
        _processedBases.Clear();
        _methodEnumMapping = null;

        BuildBaseRows();

        UpdateStatus("Cleared palettes");

        UpdateUIState();
    }

    private static string BreakHeader(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            char ch = text[i];
            char prev = text[i - 1];
            if (char.IsUpper(ch) && !char.IsUpper(prev))
            {
                sb.Append('\n');
            }
            sb.Append(ch);
        }
        return sb.ToString();
    }

    private string GetDisplayName(PaletteMode mode)
    {
            return EnumToDisplay.TryGetValue(mode, out string? display)
            ? display
            : mode.ToString();
    }

    private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        // determine bounds depending on state
        var stateForSave = WindowState;
        var bounds = stateForSave == FormWindowState.Normal ? Bounds : RestoreBounds;

        var ws = new WindowStateInfo
        {
            Left = bounds.Left,
            Top = bounds.Top,
            Width = bounds.Width,
            Height = bounds.Height,
            State = stateForSave,
            LastTheme = (comboTheme.SelectedItem as string)!,
            SourcePath = _sourcePath ?? string.Empty
        };

        _winStore.Save(ws);
    }

    private void DataGridViewPalette_CurrentCellChanged(object sender, EventArgs e)
    {
        if (_bulkUpdating || dataGridViewPalette.CurrentCell == null)
        {
            return;
        }

        int newIndex = dataGridViewPalette.CurrentCell.RowIndex;
        if (newIndex == _highlightRowIndex)
        {
            return;
        }

        int old = _highlightRowIndex;
        _highlightRowIndex = newIndex;

        if (old >= 0 && old < dataGridViewPalette.Rows.Count)
        {
            dataGridViewPalette.InvalidateRow(old);
        }
        if (newIndex >= 0 && newIndex < dataGridViewPalette.Rows.Count)
        {
            dataGridViewPalette.InvalidateRow(newIndex);
        }
    }

    private void DataGridViewPalette_Paint(object sender, PaintEventArgs e)
    {
        if (_bulkUpdating
            || _highlightRowIndex < 0
            || _highlightRowIndex >= dataGridViewPalette.RowCount)
        {
            return;
        }

        var rowRect = dataGridViewPalette.GetRowDisplayRectangle(_highlightRowIndex, true);
        if (rowRect.Width <= 0 || rowRect.Height <= 0)
        {
            return; // row not visible
        }

        using (var pen = new Pen(Color.DarkOrange, 1))
        {
            rowRect.Width -= 1;
            rowRect.Height -= 1;
            e.Graphics.DrawRectangle(pen, rowRect);
        }
    }

    private void SaveCsv_Click(object sender, EventArgs e) => SaveGrid("csv");
    private void SaveXml_Click(object sender, EventArgs e) => SaveGrid("xml");

    private void SaveGrid(string format)
    {
        using (var sfd = new SaveFileDialog())
        {
            sfd.Filter = $@"{format.ToUpper()} files|*.{format}|All files|*.*";
            sfd.DefaultExt = format;
            if (sfd.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                switch (format)
                {
                    case "csv":
                        File.WriteAllText(sfd.FileName, ExportCsv());
                        break;
                    case "xml":
                        ExportXml(sfd.FileName);
                        break;
                }
                UpdateStatus($"Saved {Path.GetFileName(sfd.FileName)}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, @"Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private string ExportCsv() => Classes.Exporter.ToCsv(dataGridViewPalette);

    private void ExportXml(string fileName) => Classes.Exporter.ToXml(dataGridViewPalette, fileName);

    private void UpdateUIState()
    {
        bool validSource = _isSourceValid;

        bool hasData = dataGridViewPalette.Columns.Count > 3 && dataGridViewPalette.Rows.Count > 0;
        buttonSave.Enabled = hasData;

        bool hasPalettes = _palettes.Count > 0;

        bool canRemove = false;
        bool canAdd = false;

        if (comboTheme.SelectedItem is string selDisplay && DisplayToEnum.TryGetValue(selDisplay, out PaletteMode selMode))
        {
            canRemove = _palettes.Exists(p => KryptonManager.GetModeForPalette(p) == selMode);
            canAdd = !canRemove;
        }

        buttonRemovePalette.Enabled = canRemove;
        buttonAddPalette.Enabled = canAdd && validSource;
        buttonClear.Enabled = hasPalettes;

        // AddAll button if exists
        if (buttonAddAll != null)
        {
            bool anyRemaining = false;
            foreach (var kv in DisplayToEnum)
            {
                if (IsSparkleDisplay(kv.Key) || IsSparkleMode(kv.Value) || IsLegacyProfessionalDisplay(kv.Key) || IsLegacyProfessionalMode(kv.Value))
                {
                    continue; // skip
                }

                if (!_palettes.Exists(p => KryptonManager.GetModeForPalette(p) == kv.Value))
                {
                    anyRemaining = true;
                    break;
                }
            }
            buttonAddAll.Enabled = validSource && anyRemaining;
        }

        if (buttonClearSource != null)
        {
            buttonClearSource.Enabled = !string.IsNullOrWhiteSpace(_sourcePath);
        }

        // Update source-required hint visibility
        if (labelSourceRequired != null)
        {
            labelSourceRequired.Visible = !validSource;
        }
    }

    private void DataGridViewPalette_SelectionChanged(object sender, EventArgs e)
    {
        UpdateUIState();
    }

    private void ButtonSave_Click(object sender, EventArgs e)
    {
        string format = "csv";
        if (comboSaveFormat.SelectedItem is string sel)
        {
            format = sel.ToLower();
        }

        SaveGrid(format);
    }

    private void DataGridViewPalette_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        if (e.Control is TextBoxBase tb)
        {
            tb.ReadOnly = true; // allow selection but no edits
            tb.BorderStyle = BorderStyle.None;
            tb.BackColor = SystemColors.Window;
            // No need to capture or reset text – leaving it untouched avoids unwanted changes

            tb.KeyDown -= EditingTextBox_KeyDown;
            tb.KeyDown += EditingTextBox_KeyDown;
        }
    }

    private void EditingTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F6)
        {
            e.Handled = true;
            EditCurrentCellColor();
        }
    }

    private void DataGridViewPalette_Scroll(object sender, ScrollEventArgs e)
    {
        if (!_bulkUpdating && e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
        {
            dataGridViewPalette.Refresh();
        }
    }

    private static void TryCopy(string text)
    {
        const int retries = 5;
        const int delay = 100;
        for (int i = 0; i < retries; i++)
        {
            try
            {
                Clipboard.SetDataObject(text, true);
                return;
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                System.Threading.Thread.Sleep(delay);
            }
        }
    }

    private static Type? GetRibbonColorsOwner(Type t)
    {
        return t?.BaseType;
    }

    /// <summary>
    /// Extracts the colour array from a palette via <c>ColorTable.Colors</c> (or <c>Colours</c>).
    /// </summary>
    private static Color[]? TryGetPaletteColors(PaletteBase palette)
    {
        if (palette == null)
        {
            return null;
        }

        try
        {
            var ct = palette.ColorTable;
            if (ct != null)
            {
                var prop = ct.GetType().GetProperty("Colors", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                            ?? ct.GetType().GetProperty("Colours", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                if (prop != null && prop.PropertyType == typeof(Color[]))
                {
                    return (prop.GetValue(ct) as Color[])!;
                }
            }
        }
        catch (IndexOutOfRangeException)
        {
            // Ignore faulty palettes; caller treats palette as sparse/incompatible.
        }

        return null;
    }

    private void MergeEnumMethodMapping(Dictionary<System.Reflection.MemberInfo, string> newMap)
    {
        if (_methodEnumMapping == null)
        {
            _methodEnumMapping = newMap;
            return;
        }

        foreach (var kv in newMap)
        {
            if (!_methodEnumMapping.ContainsKey(kv.Key))
            {
                _methodEnumMapping[kv.Key] = kv.Value;
            }
        }
    }

    private void BtnBrowseSource_Click(object sender, EventArgs e)
    {
        using (var dlg = new KryptonFolderBrowserDialog())
        {
            if (!string.IsNullOrEmpty(_sourcePath) && Directory.Exists(_sourcePath))
            {
                dlg.SelectedPath = _sourcePath;
            }

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                SetSourcePath(dlg.SelectedPath);
            }
        }
    }

    private void SetSourcePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }

        if (string.Equals(_sourcePath, path, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        _sourcePath = path;
        _isSourceValid = IsValidSourcePath(_sourcePath);
        if (textSourcePath != null)
        {
            textSourcePath.Text = path;
        }

        // persist immediately
        var ws = _winInfo ?? new WindowStateInfo();
        ws.SourcePath = path;
        ws.Left = Left;
        ws.Top = Top;
        ws.Width = Width;
        ws.Height = Height;
        ws.State = WindowState;
        ws.LastTheme = comboTheme.SelectedItem as string ?? string.Empty;
        _winStore.Save(ws);

        RecheckAllPalettes();

        UpdateUIState();
    }

    private void RecheckAllPalettes()
    {
        if (string.IsNullOrWhiteSpace(_sourcePath))
        {
            return;
        }

        for (int p = 0; p < _palettes.Count; p++)
        {
            var palette = _palettes[p];
            var col = dataGridViewPalette.Columns[palette.GetType().FullName!];
            if (col == null)
            {
                continue;
            }
            int colIndex = col.Index;

            var issues = Classes.ThemeArrayInspector.GetIssues(palette.GetType(), _sourcePath!);
            if (issues == null || issues.IsClean)
            {
                continue;
            }

            foreach (int idx in issues.MissingIndices)
            {
                if (idx >= 0 && idx < dataGridViewPalette.Rows.Count)
                {
                    var c = dataGridViewPalette.Rows[idx].Cells[colIndex];
                    if (string.IsNullOrEmpty(c.ErrorText))
                    {
                        c.ErrorText = "Missing value";
                    }

                    if (c.ToolTipText == null || !c.ToolTipText.Contains(c.ErrorText))
                    {
                        c.ToolTipText += " : " + c.ErrorText;
                    }
                }
            }
            foreach (int idx in issues.UnlabelledIndices)
            {
                if (idx >= 0 && idx < dataGridViewPalette.Rows.Count)
                {
                    var c = dataGridViewPalette.Rows[idx].Cells[colIndex];
                    if (string.IsNullOrEmpty(c.ErrorText))
                    {
                        c.ErrorText = "Unmarked value";
                    }

                    if (c.ToolTipText == null || !c.ToolTipText.Contains(c.ErrorText))
                    {
                        c.ToolTipText += " : " + c.ErrorText;
                    }
                }
            }
            foreach (int idx in issues.OutOfOrderIndices)
            {
                if (idx >= 0 && idx < dataGridViewPalette.Rows.Count)
                {
                    var c = dataGridViewPalette.Rows[idx].Cells[colIndex];
                    if (string.IsNullOrEmpty(c.ErrorText))
                    {
                        c.ErrorText = "Out-of-order";
                    }

                    if (c.ToolTipText == null || !c.ToolTipText.Contains(c.ErrorText))
                    {
                        c.ToolTipText += " : " + c.ErrorText;
                    }
                }
            }
            foreach (int idx in issues.ExtraIndices)
            {
                if (idx >= 0 && idx < dataGridViewPalette.Rows.Count)
                {
                    var c = dataGridViewPalette.Rows[idx].Cells[colIndex];
                    if (string.IsNullOrEmpty(c.ErrorText))
                    {
                        c.ErrorText = "Extra entry";
                    }

                    if (c.ToolTipText == null || !c.ToolTipText.Contains(c.ErrorText))
                    {
                        c.ToolTipText += " : " + c.ErrorText;
                    }
                }
            }
        }

        dataGridViewPalette.Refresh();
    }

    private void BtnClearSource_Click(object sender, EventArgs e)
    {
        ClearSourcePath();
    }

    private void ClearSourcePath()
    {
        _sourcePath = null;
        _isSourceValid = false;

        if (textSourcePath != null)
        {
            textSourcePath.Text = string.Empty;
        }

        // Persist cleared setting
        var ws = _winInfo ?? new WindowStateInfo();
        ws.SourcePath = string.Empty;
        ws.Left = Left;
        ws.Top = Top;
        ws.Width = Width;
        ws.Height = Height;
        ws.State = WindowState;
        ws.LastTheme = (comboTheme.SelectedItem as string)!;
        _winStore.Save(ws);

        UpdateUIState();
    }

    private void EditCurrentCellColor()
    {
        if (dataGridViewPalette.CurrentCell == null)
        {
            return;
        }

        int colIndex = dataGridViewPalette.CurrentCell.ColumnIndex;
        int rowIndex = dataGridViewPalette.CurrentCell.RowIndex;

        if (_enumValues != null && (colIndex <= 2 || rowIndex < 0 || rowIndex >= _enumValues.Length))
        {
            return;
        }

        DataGridViewColumn col = dataGridViewPalette.Columns[colIndex];

        // Column Name already stores the full type name, use it directly
        string expectedName = col.Name;
        PaletteBase? palette = _palettes
            .Find(p => string.Equals(p.GetType().FullName, expectedName, StringComparison.Ordinal));

        if (palette is null)
        {
            return;
        }

        if (_enumValues != null)
        {
            SchemeBaseColors colorEnum = _enumValues[rowIndex];

            Color currentColor = palette.GetSchemeColor(colorEnum);

            using (var dialog = new LiveColorPickerDialog())
            {
                dialog.Color = currentColor;
                dialog.Text = $@"A:{currentColor.A} R:{currentColor.R} G:{currentColor.G} B:{currentColor.B}";

                // live-update handler
                dialog.LiveColorChanged += (_, eArgs) =>
                {
                    ApplySelectedColor(eArgs.Color, palette, colorEnum, rowIndex, colIndex, pushUndo: false);
                };

                var result = dialog.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    ApplySelectedColor(dialog.Color, palette, colorEnum, rowIndex, colIndex, pushUndo: true);
                }
                else
                {
                    // Revert any live preview changes if the user cancels the dialog
                    ApplySelectedColor(currentColor, palette, colorEnum, rowIndex, colIndex, pushUndo: false);
                }
            }
        }
    }

    private void ApplySelectedColor(Color newColor, PaletteBase palette, SchemeBaseColors colorEnum, int rowIndex, int colIndex, bool pushUndo)
    {
        if (pushUndo)
        {
            _undoStack.Push(new UndoItem(palette, colorEnum, palette.GetSchemeColor(colorEnum), rowIndex, colIndex));
        }

        palette.SetSchemeColor(colorEnum, newColor);

        // Commit any pending edit before updating
        dataGridViewPalette.EndEdit();

        // Update cell visuals
        DataGridViewCell cell = dataGridViewPalette.Rows[rowIndex].Cells[colIndex];
        cell.Value = "#" + newColor.R.ToString("X2") + newColor.G.ToString("X2") + newColor.B.ToString("X2");
        cell.Style.BackColor = newColor;
        cell.Style.ForeColor = ContrastColor(newColor);
        cell.Style.SelectionBackColor = AdjustSelectionBack(newColor);
        cell.Style.SelectionForeColor = cell.Style.ForeColor;

        // Ensure refresh
        dataGridViewPalette.CurrentCell = null;
        dataGridViewPalette.CurrentCell = cell;
        dataGridViewPalette.Refresh();

        if (KryptonManager.CurrentGlobalPalette == palette)
        {
            Invalidate(true);
        }
    }

    private void UndoLastColorChange()
    {
        if (_undoStack.Count == 0)
        {
            return;
        }

        var action = _undoStack.Pop();

        action.Palette.SetSchemeColor(action.ColorEnum, action.OldColor);

        if (action.RowIndex >= 0 && action.RowIndex < dataGridViewPalette.Rows.Count &&
            action.ColumnIndex >= 0 && action.ColumnIndex < dataGridViewPalette.Columns.Count)
        {
            DataGridViewCell cell = dataGridViewPalette.Rows[action.RowIndex].Cells[action.ColumnIndex];
            cell.Value = "#" + action.OldColor.R.ToString("X2") + action.OldColor.G.ToString("X2") + action.OldColor.B.ToString("X2");
            cell.Style.BackColor = action.OldColor;
            cell.Style.ForeColor = ContrastColor(action.OldColor);
            cell.Style.SelectionBackColor = AdjustSelectionBack(action.OldColor);
            cell.Style.SelectionForeColor = cell.Style.ForeColor;

            dataGridViewPalette.CurrentCell = null;
            dataGridViewPalette.CurrentCell = cell;
            dataGridViewPalette.Refresh();
        }

        if (KryptonManager.CurrentGlobalPalette == action.Palette)
        {
            Invalidate(true);
        }
    }

    // ------------------------- Search / Filter helpers ---------------------------

    private static bool TryParseColorString(string input, out Color color)
    {
        color = Color.Empty;
        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        input = input.Trim();

        // Hex with #
        if (input.StartsWith("#", StringComparison.Ordinal))
        {
            try
            {
                color = ColorTranslator.FromHtml(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Hex without #
        if (input.Length == 6 || input.Length == 8)
        {
            try
            {
                color = ColorTranslator.FromHtml("#" + input);
                return true;
            }
            catch
            {
                // ignored
            }
        }

        // RGB triplet "R,G,B"
        var parts = input.Split(',');
        if (parts.Length == 3 &&
            byte.TryParse(parts[0].Trim(), out byte r) &&
            byte.TryParse(parts[1].Trim(), out byte g) &&
            byte.TryParse(parts[2].Trim(), out byte b))
        {
            color = Color.FromArgb(r, g, b);
            return true;
        }

        // Named colour
        var named = Color.FromName(input);
        if (named.IsKnownColor || named.IsNamedColor)
        {
            color = named;
            return true;
        }

        return false;
    }

    private void SearchForColor()
    {
        string? input = KryptonInputBox.Show(new KryptonInputBoxData
        {
            Prompt = "Enter colour to search (e.g. #FF0000 or 255,0,0):",
            Caption = "Search Colour",
            DefaultResponse = "#"
        });

        if (!TryParseColorString(input, out var target))
        {
            UpdateStatus("Invalid colour value.");
            return;
        }

        // starting position after current cell
        int startRow = dataGridViewPalette.CurrentCell?.RowIndex ?? 0;
        int startCol = dataGridViewPalette.CurrentCell?.ColumnIndex ?? 3;

        // Search forward from next cell
        bool found = false;
        int rowCount = dataGridViewPalette.Rows.Count;
        int colCount = dataGridViewPalette.Columns.Count;

        int r = startRow;
        int c = startCol + 1;

        for (int iter = 0; iter < rowCount * (colCount - 3); iter++)
        {
            if (c >= colCount)
            {
                r = (r + 1) % rowCount;
                c = 3;
            }

            var cell = dataGridViewPalette.Rows[r].Cells[c];
            if (cell.Style.BackColor.ToArgb() == target.ToArgb())
            {
                dataGridViewPalette.CurrentCell = cell;
                found = true;
                break;
            }

            c++;
        }

        UpdateStatus(found ? "Colour found." : "Colour not found.");
    }

    private void ApplyRowFilter(Func<DataGridViewRow, bool> predicate)
    {
        foreach (DataGridViewRow row in dataGridViewPalette.Rows)
        {
            if (row.IsNewRow)
            {
                continue;
            }

            row.Visible = predicate(row);
        }
    }

    private void FilterRowsByColour()
    {
        string? input = KryptonInputBox.Show(new KryptonInputBoxData
        {
            Prompt = "Enter colour to filter by (e.g. #FF0000 or 255,0,0):",
            Caption = "Filter Rows – Colour",
            DefaultResponse = _lastColorFilterInput ?? "#"
        });

        if (!TryParseColorString(input, out var target))
        {
            UpdateStatus("Invalid colour value.");
            return;
        }

        _activeColourFilter = target;
        _lastColorFilterInput = input;
        UpdateRowVisibility();
        UpdateStatus("Applied colour filter.");
    }

    private void FilterRowsByEnumSubstring()
    {
        string? keyword = KryptonInputBox.Show(new KryptonInputBoxData
        {
            Prompt = "Enter text to filter SchemeBaseColors (contains, case-insensitive):",
            Caption = "Filter Rows – SchemeBaseColors",
            DefaultResponse = string.Empty
        });

        if (string.IsNullOrWhiteSpace(keyword))
        {
            UpdateStatus("Filter cleared.");
            ClearRowFilter();
            return;
        }

        _activeNameFilter = keyword.Trim();
        UpdateRowVisibility();
        UpdateStatus("Applied name filter.");
    }

    private void ClearRowFilter()
    {
        _activeColourFilter = null;
        _activeNameFilter = null;
        UpdateRowVisibility();
        UpdateStatus("Filters cleared.");
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == (Keys.Control | Keys.Z))
        {
            UndoLastColorChange();
            return true;
        }

        // Ctrl+F – search next colour
        if (keyData == (Keys.Control | Keys.F))
        {
            SearchForColor();
            return true;
        }

        // Ctrl+Shift+C – filter by colour
        if (keyData == (Keys.Control | Keys.Shift | Keys.C))
        {
            FilterRowsByColour();
            return true;
        }

        // Ctrl+Shift+F – filter by enum substring
        if (keyData == (Keys.Control | Keys.Shift | Keys.F))
        {
            FilterRowsByEnumSubstring();
            return true;
        }

        // Ctrl+Shift+R – clear filters
        if (keyData == (Keys.Control | Keys.Shift | Keys.R))
        {
            ClearRowFilter();
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    private void SetupContextMenu()
    {
        _contextMenu = new ContextMenuStrip();
        _contextMenu.Items.Add("Search Colour...\tCtrl+F", null, (_, __) => SearchForColor());
        _contextMenu.Items.Add("Filter by Colour...\tCtrl+Shift+C", null, (_, __) => FilterRowsByColour());
        _contextMenu.Items.Add("Filter by Name...\tCtrl+Shift+F", null, (_, __) => FilterRowsByEnumSubstring());
        _contextMenu.Items.Add(new ToolStripSeparator());
        _contextMenu.Items.Add("Reset Filters\tCtrl+Shift+R", null, (_, __) => ClearRowFilter());

        dataGridViewPalette.ContextMenuStrip = _contextMenu;
    }

    //-------------- Filter evaluation -------------------
    private bool RowPassesFilters(DataGridViewRow row)
    {
        if (row.IsNewRow)
        {
            return false;
        }

        bool passesColour = true;
        if (_activeColourFilter.HasValue)
        {
            passesColour = false;
            for (int col = 3; col < row.Cells.Count; col++)
            {
                if (row.Cells[col].Style.BackColor.ToArgb() == _activeColourFilter.Value.ToArgb())
                {
                    passesColour = true;
                    break;
                }
            }
        }

        bool passesName = true;
        if (!string.IsNullOrWhiteSpace(_activeNameFilter))
        {
            string? enumName = row.Cells.Count > 1 ? row.Cells[1].Value?.ToString() : null;
            if (_activeNameFilter != null)
            {
                passesName = enumName != null &&
                             enumName.IndexOf(_activeNameFilter, StringComparison.OrdinalIgnoreCase) >= 0;
            }
        }

        return passesColour && passesName;
    }

    private void UpdateRowVisibility()
    {
        foreach (DataGridViewRow row in dataGridViewPalette.Rows)
        {
            if (row.IsNewRow)
            {
                continue;
            }

            row.Visible = RowPassesFilters(row);
        }

        dataGridViewPalette.Refresh();
    }
}