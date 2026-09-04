#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Lists <c>.kthemex</c>, <c>.ktheme</c> (including collections), and optional XML palette files from a folder
/// (optionally including subfolders) and applies the selected theme through a <see cref="KryptonManager"/>.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonListBox), "ToolboxBitmaps.KryptonListBox.bmp")]
[DefaultEvent(nameof(SelectedIndexChanged))]
[DefaultProperty(nameof(PaletteDirectory))]
[Designer(typeof(KryptonStubDesigner))]
[DesignerCategory(@"code")]
[Description(@"Lists palette files (.kthemex / .ktheme collections / .xml) and applies the selected custom theme.")]
public class KryptonPaletteFileListBox : KryptonListBox
{
    private readonly KryptonPaletteFileThemeSelectorController _controller = new KryptonPaletteFileThemeSelectorController();
    private readonly KryptonPaletteFileSelectorStrings _strings = new KryptonPaletteFileSelectorStrings();

    /// <summary>Initializes a new instance of the <see cref="KryptonPaletteFileListBox"/> class.</summary>
    public KryptonPaletteFileListBox()
    {
        _controller.Strings = _strings;
    }

    /// <summary>
    /// Gets or sets the folder that is scanned for palette files.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Folder that is scanned for .kthemex, .ktheme, and optional .xml palette files.")]
    [DefaultValue(@"")]
    [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
    public string PaletteDirectory
    {
        get => _controller.PaletteDirectory;
        set
        {
            var directory = value ?? string.Empty;
            if (string.Equals(_controller.PaletteDirectory, directory, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            _controller.PaletteDirectory = directory;
            Reload();
        }
    }

    /// <summary>Gets or sets whether nested folders are scanned.</summary>
    [Category(@"Data")]
    [Description(@"When true, palette files in subfolders are included.")]
    [DefaultValue(false)]
    public bool SearchSubdirectories
    {
        get => _controller.SearchSubdirectories;
        set
        {
            if (_controller.SearchSubdirectories == value)
            {
                return;
            }

            _controller.SearchSubdirectories = value;
            Reload();
        }
    }

    /// <summary>Gets or sets whether <c>*.kthemex</c> files are listed.</summary>
    [Category(@"Data")]
    [Description(@"When true, *.kthemex XML palette files are listed.")]
    [DefaultValue(true)]
    public bool IncludeKthemex
    {
        get => _controller.IncludeKthemex;
        set
        {
            if (_controller.IncludeKthemex == value)
            {
                return;
            }

            _controller.IncludeKthemex = value;
            Reload();
        }
    }

    /// <summary>Gets or sets whether <c>*.ktheme</c> files (including collections) are listed.</summary>
    [Category(@"Data")]
    [Description(@"When true, *.ktheme files (including multi-theme collections) are listed.")]
    [DefaultValue(true)]
    public bool IncludeKtheme
    {
        get => _controller.IncludeKtheme;
        set
        {
            if (_controller.IncludeKtheme == value)
            {
                return;
            }

            _controller.IncludeKtheme = value;
            Reload();
        }
    }

    /// <summary>Gets or sets whether legacy <c>*.xml</c> palette files are listed.</summary>
    [Category(@"Data")]
    [Description(@"When true, legacy *.xml palette files are listed.")]
    [DefaultValue(true)]
    // ToDo V120 LTS: Remove IncludeXml. Prefer UpgradeXmlToKthemex, then list .kthemex / .ktheme only.
    public bool IncludeXml
    {
        get => _controller.IncludeXml;
        set
        {
            if (_controller.IncludeXml == value)
            {
                return;
            }

            _controller.IncludeXml = value;
            Reload();
        }
    }

    /// <summary>Gets or sets whether selecting an item applies it as the global custom palette.</summary>
    [Category(@"Behavior")]
    [Description(@"When true, the selected palette file theme is applied immediately.")]
    [DefaultValue(true)]
    public bool AutoApply
    {
        get => _controller.AutoApply;
        set => _controller.AutoApply = value;
    }

    /// <summary>
    /// Gets or sets whether preview images are loaded and shown when a palette defines
    /// <see cref="KryptonCustomPaletteBase.Thumbnail"/> (or a collection thumbnail catalog).
    /// Previews are drawn with the Stable Kr tile as a corner overlay; files without a preview
    /// use the Kr tile alone.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"When true, palette thumbnails are loaded and shown with the Krypton Stable overlay.")]
    [DefaultValue(true)]
    public bool ShowThumbnails
    {
        get => _controller.LoadThumbnails;
        set
        {
            if (_controller.LoadThumbnails == value)
            {
                return;
            }

            _controller.LoadThumbnails = value;
            Reload();
        }
    }

    /// <summary>Gets or sets the display size for loaded thumbnails.</summary>
    [Category(@"Appearance")]
    [Description(@"Display size for palette thumbnails when ShowThumbnails is true.")]
    [DefaultValue(typeof(Size), "32, 32")]
    public Size ThumbnailSize
    {
        get => _controller.ThumbnailSize;
        set
        {
            var size = value.Width < 1 || value.Height < 1 ? new Size(32, 32) : value;
            if (_controller.ThumbnailSize == size)
            {
                return;
            }

            _controller.ThumbnailSize = size;
            if (_controller.LoadThumbnails)
            {
                Reload();
            }
        }
    }

    /// <summary>Gets or sets the manager used when applying a theme.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonManager KryptonManager
    {
        get => _controller.Manager;
        set => _controller.Manager = value ?? new KryptonManager();
    }

    /// <summary>Gets the selected palette file theme, if any.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonPaletteFileThemeItem? SelectedPaletteTheme
    {
        get => SelectedItem as KryptonPaletteFileThemeItem;
        set
        {
            var index = _controller.IndexOf(Items, value);
            SelectedIndex = index;
        }
    }

    /// <summary>Rescans <see cref="PaletteDirectory"/> and restores the previous selection when possible.</summary>
    public void Reload()
    {
        var previous = SelectedPaletteTheme;
        _controller.SuppressSelection = true;
        try
        {
            var index = _controller.Reload(Items, previous);
            SelectedIndex = index;
        }
        finally
        {
            _controller.SuppressSelection = false;
        }
    }

    /// <summary>Applies the current selection without requiring <see cref="AutoApply"/>.</summary>
    /// <returns><see langword="true"/> when a theme was applied.</returns>
    public bool ApplySelected() => _controller.Apply(SelectedItem);

    /// <inheritdoc />
    protected override void OnSelectedIndexChanged(EventArgs e)
    {
        if (!_controller.SuppressSelection && AutoApply && !DesignMode)
        {
            _controller.Apply(SelectedItem);
        }

        base.OnSelectedIndexChanged(e);
    }

    /// <summary>
    /// Gets the localisable strings used when listing palette files.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Localizable strings used when listing palette files.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public KryptonPaletteFileSelectorStrings Strings => _strings;

    private bool ShouldSerializeStrings() => !Strings.IsDefault;

    private void ResetStrings() => Strings.Reset();

    /// <summary>Gets the items of the list box.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ListBox.ObjectCollection Items => base.Items;

    /// <summary>Gets and sets the selected index.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new int SelectedIndex
    {
        get => base.SelectedIndex;
        set => base.SelectedIndex = value;
    }
}
