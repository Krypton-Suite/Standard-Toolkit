#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Theme selector list view that shows stored or generated previews in icon, tile, and details views (issue #3870).
/// Custom palettes with a <see cref="KryptonCustomPaletteBase.Thumbnail"/> use that image plus the Stable Kr overlay.
/// Missing images fall back to the Kr tile. Builtin themes get a generated window mock-up when
/// <see cref="ShowThemePreviews"/> is on. Hovering a row applies that theme as a live preview until the pointer
/// leaves; a click still commits. See <see cref="LivePreviewOnHover"/>.
/// </summary>
[Designer(typeof(KryptonStubDesigner))]
[Description(@"Lists Krypton themes with preview images and applies the selected theme.")]
public class KryptonThemeListView : KryptonListView, IKryptonThemeSelectorBase
{
    private const int LargePreviewSize = 48;
    private const int SmallPreviewSize = 16;

    /// <summary> When we change the palette, Krypton Manager will notify us that there was a change. Since we are changing it that notification can be skipped.</summary>
    private bool _isLocalUpdate;

    /// <summary> Suppress code execution in the SelectedIndexChanged event handler, when a theme change via the KManager has been performed.</summary>
    private bool _isExternalUpdate;

    /// <summary> Backing var for the DefaultPalette property.</summary>
    private PaletteMode _defaultPalette = PaletteMode.Global;

    /// <summary> Whether extra catalogued palettes appear in the list.</summary>
    private bool _showExtraThemes = true;

    /// <summary> Whether preview images are composed for each row.</summary>
    private bool _showThemePreviews = true;

    /// <summary> Whether hovering a row temporarily applies that theme.</summary>
    private bool _livePreviewOnHover = true;

    /// <summary> Local Krypton Manager instance.</summary>
    private readonly KryptonManager _manager;

    /// <summary> User defined palette.</summary>
    private KryptonCustomPaletteBase? _kryptonCustomPalette;

    private ImageList? _largeThemeImages;
    private ImageList? _smallThemeImages;
    private bool _applyPosted;
    private string? _appliedHoverName;
    private bool _livePreviewing;
    private PaletteMode _restoreMode = PaletteMode.Global;
    private KryptonCustomPaletteBase? _restoreCustom;
    private string? _restoreName;

    /// <summary>Initializes a new instance of the <see cref="KryptonThemeListView"/> class.</summary>
    public KryptonThemeListView()
    {
        _manager = new KryptonManager();
        this.View = System.Windows.Forms.View.LargeIcon;
        MultiSelect = false;
        FullRowSelect = true;
        HeaderStyle = ColumnHeaderStyle.Nonclickable;
        ShowItemToolTips = true;
        HideSelection = false;
        SelectedIndexChanged += OnThemeSelectedIndexChanged;
        ListView.MouseMove += OnLivePreviewMouseMove;
        ListView.MouseLeave += OnLivePreviewMouseLeave;
        PopulateThemes();
    }

    /// <inheritdoc/>
    [Category(@"Visuals")]
    [Description(@"The default palette mode.")]
    [DefaultValue(PaletteMode.Global)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public PaletteMode DefaultPalette
    {
        get => _defaultPalette;
        set => SelectedIndex = CommonHelperThemeSelectors.DefaultPaletteSetter(ref _defaultPalette, value, ThemeNames, SelectedIndex);
    }

    private void ResetDefaultPalette() => DefaultPalette = PaletteMode.Global;

    private bool ShouldSerializeDefaultPalette() => _defaultPalette != PaletteMode.Global;

    /// <summary>
    /// Gets or sets whether extra (non-core) catalogued palettes appear in the list.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"When false, only core Toolkit palettes are listed.")]
    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ShowExtraThemes
    {
        get => _showExtraThemes;
        set
        {
            if (_showExtraThemes == value)
            {
                return;
            }

            _showExtraThemes = value;
            ReloadThemeItems();
        }
    }

    /// <summary>
    /// Gets or sets whether Large Icon, Tile, Small Icon, and Details views show theme preview images.
    /// When off, every row uses the Stable Kr tile.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Show stored or generated theme preview images. Off uses the Stable Kr tile for every theme.")]
    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ShowThemePreviews
    {
        get => _showThemePreviews;
        set
        {
            if (_showThemePreviews == value)
            {
                return;
            }

            _showThemePreviews = value;
            ReloadThemeItems();
        }
    }

    /// <summary>
    /// Gets or sets whether hovering a theme row temporarily applies that theme to the application.
    /// The committed (clicked) selection is restored when the pointer leaves the list.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Temporarily apply the hovered theme as a live preview. The last clicked theme is restored when the pointer leaves.")]
    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool LivePreviewOnHover
    {
        get => _livePreviewOnHover;
        set
        {
            if (_livePreviewOnHover == value)
            {
                return;
            }

            _livePreviewOnHover = value;
            if (!value)
            {
                CancelLivePreview(restore: true);
            }
        }
    }

    /// <summary>
    /// Gets or sets the selected theme index.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectedIndex
    {
        get => SelectedIndices.Count > 0 ? SelectedIndices[0] : -1;
        set
        {
            BeginUpdate();
            try
            {
                foreach (ListViewItem selected in SelectedItems)
                {
                    selected.Selected = false;
                }

                if (value >= 0 && value < Items.Count)
                {
                    Items[value].Selected = true;
                    Items[value].Focused = true;
                    Items[value].EnsureVisible();
                }
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    /// <inheritdoc />
    protected override void OnHandleCreated(EventArgs e)
    {
        KryptonManager.GlobalPaletteChanged += KryptonManagerGlobalPaletteChanged;
        ThemeManager.RegisteredThemesChanged += ThemeManagerRegisteredThemesChanged;
        base.OnHandleCreated(e);
        EnsureDetailsColumn();
    }

    /// <inheritdoc />
    protected override void OnHandleDestroyed(EventArgs e)
    {
        KryptonManager.GlobalPaletteChanged -= KryptonManagerGlobalPaletteChanged;
        ThemeManager.RegisteredThemesChanged -= ThemeManagerRegisteredThemesChanged;
        base.OnHandleDestroyed(e);
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            ListView.MouseMove -= OnLivePreviewMouseMove;
            ListView.MouseLeave -= OnLivePreviewMouseLeave;
            CancelLivePreview(restore: false);
            DisposeThemeImages();
        }

        base.Dispose(disposing);
    }

    private void ThemeManagerRegisteredThemesChanged(object? sender, EventArgs e)
    {
        if (IsDisposed)
        {
            return;
        }

        ReloadThemeItems();
    }

    private string GetSelectedThemeName()
    {
        if (SelectedItems.Count > 0)
        {
            var text = SelectedItems[0].Text;
            if (!string.IsNullOrEmpty(text))
            {
                return text;
            }
        }

        return string.Empty;
    }

    private List<string> ThemeNames
    {
        get
        {
            var names = new List<string>(Items.Count);
            foreach (ListViewItem item in Items)
            {
                names.Add(item.Text);
            }

            return names;
        }
    }

    private void PopulateThemes()
    {
        _isExternalUpdate = true;
        try
        {
            RebuildItems(null, KryptonManager.CurrentGlobalPaletteMode);
            SelectedIndex = CommonHelperThemeSelectors.GetInitialSelectedIndex(DefaultPalette, _manager, ThemeNames);
        }
        finally
        {
            _isExternalUpdate = false;
        }
    }

    private void ReloadThemeItems()
    {
        _isExternalUpdate = true;
        try
        {
            var previous = GetSelectedThemeName();
            var fallback = KryptonManager.CurrentGlobalPaletteMode;
            RebuildItems(previous, fallback);
        }
        finally
        {
            _isExternalUpdate = false;
        }
    }

    private void RebuildItems(string? previousName, PaletteMode fallbackMode)
    {
        BeginUpdate();
        try
        {
            LargeImageList = null;
            SmallImageList = null;
            DisposeThemeImages();
            Items.Clear();

            _largeThemeImages = CreateImageList(LargePreviewSize);
            _smallThemeImages = CreateImageList(SmallPreviewSize);
            var names = CommonHelperThemeSelectors.GetThemesArray(_showExtraThemes);
            var badgeIndex = -1;
            for (var i = 0; i < names.Length; i++)
            {
                var imageIndex = AddThemePreview(names[i], ref badgeIndex);
                var item = new ListViewItem(names[i])
                {
                    ImageIndex = imageIndex,
                    ToolTipText = names[i]
                };
                Items.Add(item);
            }

            _ = _largeThemeImages.Handle;
            _ = _smallThemeImages.Handle;
            LargeImageList = _largeThemeImages;
            SmallImageList = _smallThemeImages;
            EnsureDetailsColumn();

            var namesList = ThemeNames;
            var index = -1;
            if (!string.IsNullOrEmpty(previousName))
            {
                index = namesList.IndexOf(previousName!);
            }

            if (index < 0)
            {
                index = CommonHelperThemeSelectors.GetPaletteIndex(namesList, fallbackMode);
            }

            if (index >= 0)
            {
                SelectedIndex = index;
            }
        }
        finally
        {
            EndUpdate();
        }
    }

    private int AddThemePreview(string themeName, ref int badgeIndex)
    {
        Image? preview = null;
        if (_showThemePreviews)
        {
            preview = KryptonThemePreview.Resolve(themeName, _kryptonCustomPalette, generateWhenMissing: true);
        }

        if (preview == null)
        {
            if (badgeIndex < 0)
            {
                badgeIndex = AddIconPair(null);
            }

            return badgeIndex;
        }

        try
        {
            return AddIconPair(preview);
        }
        finally
        {
            preview.Dispose();
        }
    }

    private int AddIconPair(Image? preview)
    {
        var index = _largeThemeImages!.Images.Count;
        using (var large = KryptonPaletteFile.CreateThemeIcon(preview, LargePreviewSize))
        using (var small = KryptonPaletteFile.CreateThemeIcon(preview, SmallPreviewSize))
        {
            _largeThemeImages.Images.Add(large);
            _smallThemeImages!.Images.Add(small);
            _ = _largeThemeImages.Handle;
            _ = _smallThemeImages.Handle;
        }

        return index;
    }

    private static ImageList CreateImageList(int size) =>
        new ImageList
        {
            ColorDepth = ColorDepth.Depth32Bit,
            ImageSize = new Size(size, size)
        };

    private void EnsureDetailsColumn()
    {
        if (Columns.Count == 0)
        {
            Columns.Add(@"Theme", -2);
        }
        else
        {
            Columns[0].Text = @"Theme";
        }
    }

    private void DisposeThemeImages()
    {
        LargeImageList = null;
        SmallImageList = null;
        _largeThemeImages?.Dispose();
        _smallThemeImages?.Dispose();
        _largeThemeImages = null;
        _smallThemeImages = null;
    }

    private void KryptonManagerGlobalPaletteChanged(object? sender, EventArgs e)
    {
        if (_isLocalUpdate)
        {
            return;
        }

        // An external theme change is the new committed state; drop any hover preview.
        if (_livePreviewing)
        {
            CancelLivePreview(restore: false);
        }

        var mode = KryptonManager.CurrentGlobalPaletteMode;
        if (mode == PaletteMode.Global)
        {
            return;
        }

        _isExternalUpdate = true;
        var deferCommit = false;
        var idx = -1;
        try
        {
            var names = CommonHelperThemeSelectors.GetThemesArray(_showExtraThemes);
            if (!ThemeNamesMatch(names))
            {
                var previous = GetSelectedThemeName();
                RebuildItems(mode == PaletteMode.Custom ? null : previous, mode);
            }

            idx = CommonHelperThemeSelectors.GetPaletteIndex(ThemeNames, mode);
            if (idx < 0 || idx == SelectedIndex)
            {
                return;
            }

            deferCommit = ThemeChangeCoordinator.InProgress && !IsDisposed && IsHandleCreated;
            if (deferCommit)
            {
                BeginInvoke((System.Windows.Forms.MethodInvoker)(() => CommitThemeSelection(idx)));
            }
            else
            {
                SelectedIndex = idx;
            }
        }
        finally
        {
            if (!deferCommit)
            {
                _isExternalUpdate = false;
            }
        }
    }

    private bool ThemeNamesMatch(string[] names)
    {
        if (names.Length != Items.Count)
        {
            return false;
        }

        for (var i = 0; i < names.Length; i++)
        {
            if (!string.Equals(Items[i].Text, names[i], StringComparison.Ordinal))
            {
                return false;
            }
        }

        return true;
    }

    private void CommitThemeSelection(int idx)
    {
        try
        {
            if (IsDisposed || !IsHandleCreated)
            {
                return;
            }

            _isExternalUpdate = true;
            SelectedIndex = idx;
        }
        finally
        {
            _isExternalUpdate = false;
        }
    }

    private void OnThemeSelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_isExternalUpdate || _isLocalUpdate || _applyPosted)
        {
            return;
        }

        // RecreateHandle after a hover apply re-raises item-changed for the committed row.
        if (_livePreviewing && string.Equals(GetSelectedThemeName(), _restoreName, StringComparison.Ordinal))
        {
            return;
        }

        if (!IsHandleCreated)
        {
            ApplySelectedTheme();
            return;
        }

        _applyPosted = true;
        BeginInvoke((System.Windows.Forms.MethodInvoker)(() =>
        {
            _applyPosted = false;
            ApplySelectedTheme();
        }));
    }

    private void ApplySelectedTheme()
    {
        if (IsDisposed)
        {
            return;
        }

        CancelLivePreview(restore: false);
        var themeName = GetSelectedThemeName();
        if (!CommonHelperThemeSelectors.OnSelectedIndexChanged(ref _isLocalUpdate, _isExternalUpdate, ref _defaultPalette,
                themeName, _manager, _kryptonCustomPalette))
        {
            SelectedIndex = CommonHelperThemeSelectors.GetPaletteIndex(ThemeNames, _manager.GlobalPaletteMode);
        }

        ResumeHoverUnderPointer();
    }

    private void OnLivePreviewMouseMove(object? sender, MouseEventArgs e)
    {
        if (!_livePreviewOnHover || DesignMode || IsDisposed)
        {
            return;
        }

        var item = ListView.GetItemAt(e.X, e.Y);
        if (item == null)
        {
            return;
        }

        ApplyLivePreview(item.Text);
    }

    private void OnLivePreviewMouseLeave(object? sender, EventArgs e)
    {
        if (!_livePreviewOnHover || DesignMode || IsDisposed)
        {
            return;
        }

        // RecreateHandle after a palette change raises MouseLeave while the pointer is still over the list.
        if (ListView.RecreatingHandle || IsPointerOverSelector())
        {
            return;
        }

        ApplyLivePreview(null);
    }

    private void ApplyLivePreview(string? themeName)
    {
        if (IsDisposed || !_livePreviewOnHover || DesignMode)
        {
            CancelLivePreview(restore: _livePreviewing);
            return;
        }

        if (string.IsNullOrEmpty(themeName))
        {
            RestoreCommittedTheme();
            return;
        }

        var committedName = GetSelectedThemeName();
        if (string.Equals(themeName, committedName, StringComparison.Ordinal))
        {
            if (_livePreviewing)
            {
                RestoreCommittedTheme();
            }

            _appliedHoverName = themeName;
            return;
        }

        if (string.Equals(themeName, _appliedHoverName, StringComparison.Ordinal))
        {
            return;
        }

        CaptureCommittedTheme();
        _isLocalUpdate = true;
        try
        {
            if (ApplyThemeNameCore(themeName!))
            {
                _appliedHoverName = themeName;
            }
        }
        finally
        {
            _isLocalUpdate = false;
        }
    }

    private void CaptureCommittedTheme()
    {
        if (_livePreviewing)
        {
            return;
        }

        _restoreMode = KryptonManager.CurrentGlobalPaletteMode;
        _restoreCustom = KryptonManager.CurrentGlobalPalette as KryptonCustomPaletteBase;
        _restoreName = GetSelectedThemeName();
        _livePreviewing = true;
    }

    private void RestoreCommittedTheme()
    {
        if (!_livePreviewing)
        {
            _appliedHoverName = null;
            return;
        }

        _isLocalUpdate = true;
        try
        {
            if (_restoreMode == PaletteMode.Custom && _restoreCustom != null)
            {
                ThemeManager.ApplyTheme(_restoreCustom, _manager);
            }
            else if (_restoreMode != PaletteMode.Global && _restoreMode != PaletteMode.Custom)
            {
                ThemeManager.ApplyGlobalTheme(_manager, _restoreMode);
            }
            else if (!string.IsNullOrEmpty(_restoreName))
            {
                ApplyThemeNameCore(_restoreName!);
            }
        }
        finally
        {
            _isLocalUpdate = false;
            _livePreviewing = false;
            _appliedHoverName = null;
            _restoreCustom = null;
            _restoreName = null;
            _restoreMode = PaletteMode.Global;
        }
    }

    private void CancelLivePreview(bool restore)
    {
        if (restore)
        {
            RestoreCommittedTheme();
            return;
        }

        _livePreviewing = false;
        _appliedHoverName = null;
        _restoreCustom = null;
        _restoreName = null;
        _restoreMode = PaletteMode.Global;
    }

    private void ResumeHoverUnderPointer()
    {
        if (!_livePreviewOnHover || DesignMode || IsDisposed || !IsHandleCreated)
        {
            return;
        }

        var client = ListView.PointToClient(Control.MousePosition);
        var item = ListView.GetItemAt(client.X, client.Y);
        if (item == null || string.Equals(item.Text, GetSelectedThemeName(), StringComparison.Ordinal))
        {
            return;
        }

        ApplyLivePreview(item.Text);
    }

    private bool IsPointerOverSelector()
    {
        if (!IsHandleCreated)
        {
            return false;
        }

        return ClientRectangle.Contains(PointToClient(Control.MousePosition));
    }

    private bool ApplyThemeNameCore(string themeName)
    {
        if (ThemeManager.TryApplyRegisteredTheme(themeName, _manager))
        {
            return true;
        }

        var mode = ThemeManager.GetThemeManagerMode(themeName);
        if (mode == PaletteMode.Custom)
        {
            if (_kryptonCustomPalette != null)
            {
                _manager.GlobalCustomPalette = _kryptonCustomPalette;
                return true;
            }

            return false;
        }

        if (mode == PaletteMode.Global)
        {
            return false;
        }

        ThemeManager.ApplyTheme(themeName, _manager);
        return true;
    }

    /// <summary>Gets the items of the list view.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ListView.ListViewItemCollection Items => base.Items;

    /// <summary>Gets or sets the large-icon image list. Managed by this control.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ImageList? LargeImageList
    {
        get => base.LargeImageList;
        set => base.LargeImageList = value;
    }

    /// <summary>Gets or sets the small-icon image list. Managed by this control.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ImageList? SmallImageList
    {
        get => base.SmallImageList;
        set => base.SmallImageList = value;
    }
}
