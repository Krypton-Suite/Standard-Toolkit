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
/// Lists <c>.kpalx</c>, <c>.kpal</c> (including packs), and optional XML palette files from a folder
/// (optionally including subfolders) and applies the selected theme through a <see cref="KryptonManager"/>.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonListBox), "ToolboxBitmaps.KryptonListBox.bmp")]
[DefaultEvent(nameof(SelectedIndexChanged))]
[DefaultProperty(nameof(PaletteDirectory))]
[Designer(typeof(KryptonStubDesigner))]
[DesignerCategory(@"code")]
[Description(@"Lists palette files (.kpalx / .kpal packs / .xml) and applies the selected custom theme.")]
public class KryptonPaletteFileListBox : KryptonListBox
{
    private readonly KryptonPaletteFileThemeSelectorController _controller = new();

    /// <summary>
    /// Gets or sets the folder that is scanned for palette files.
    /// </summary>
    [Category(@"Data")]
    [Description(@"Folder that is scanned for .kpalx, .kpal, and optional .xml palette files.")]
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

    /// <summary>Gets or sets whether <c>*.kpalx</c> files are listed.</summary>
    [Category(@"Data")]
    [Description(@"When true, *.kpalx XML palette files are listed.")]
    [DefaultValue(true)]
    public bool IncludeKpalx
    {
        get => _controller.IncludeKpalx;
        set
        {
            if (_controller.IncludeKpalx == value)
            {
                return;
            }

            _controller.IncludeKpalx = value;
            Reload();
        }
    }

    /// <summary>Gets or sets whether <c>*.kpal</c> files (including packs) are listed.</summary>
    [Category(@"Data")]
    [Description(@"When true, *.kpal files (including multi-theme packs) are listed.")]
    [DefaultValue(true)]
    public bool IncludeKpal
    {
        get => _controller.IncludeKpal;
        set
        {
            if (_controller.IncludeKpal == value)
            {
                return;
            }

            _controller.IncludeKpal = value;
            Reload();
        }
    }

    /// <summary>Gets or sets whether legacy <c>*.xml</c> palette files are listed.</summary>
    [Category(@"Data")]
    [Description(@"When true, legacy *.xml palette files are listed.")]
    [DefaultValue(true)]
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
    /// <see cref="KryptonCustomPaletteBase.Thumbnail"/> (or a pack thumbnail catalog).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"When true, optional palette thumbnails are loaded and shown. Leave off until palettes provide previews.")]
    [DefaultValue(false)]
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
