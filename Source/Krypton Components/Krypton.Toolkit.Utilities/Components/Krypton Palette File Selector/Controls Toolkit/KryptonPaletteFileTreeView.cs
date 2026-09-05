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
/// Tree that lists <c>.kthemex</c>, <c>.ktheme</c> (including collections), and optional XML palette files
/// from a folder (and its subfolders) and applies the selected theme through a <see cref="KryptonManager"/>.
/// Path-named collection themes reconstruct the original folder tree.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonTreeView), "ToolboxBitmaps.KryptonTreeView.bmp")]
[DefaultEvent(nameof(AfterSelect))]
[DefaultProperty(nameof(PaletteDirectory))]
[Designer(typeof(KryptonStubDesigner))]
[DesignerCategory(@"code")]
[Description(@"Shows palette files and .ktheme collection folders as a tree and applies the selected custom theme.")]
public class KryptonPaletteFileTreeView : KryptonTreeView
{
    private readonly KryptonPaletteFileThemeSelectorController _controller = new KryptonPaletteFileThemeSelectorController();
    private readonly KryptonPaletteFileSelectorStrings _strings = new KryptonPaletteFileSelectorStrings();
    private ImageList? _thumbnailImages;

    /// <summary>Initializes a new instance of the <see cref="KryptonPaletteFileTreeView"/> class.</summary>
    public KryptonPaletteFileTreeView()
    {
        _controller.Strings = _strings;
        _controller.SearchSubdirectories = true;
        HideSelection = false;
        Sorted = true;
        FullRowSelect = true;
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
    [DefaultValue(true)]
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

    /// <summary>Gets or sets whether selecting a theme node applies it as the global custom palette.</summary>
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

    /// <summary>Gets the selected palette file theme, if any. Folder nodes return <see langword="null"/>.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonPaletteFileThemeItem? SelectedPaletteTheme
    {
        get => SelectedNode?.Tag as KryptonPaletteFileThemeItem;
        set
        {
            SelectedNode = FindNode(Nodes, value);
        }
    }

    /// <summary>Rescans <see cref="PaletteDirectory"/> and restores the previous selection when possible.</summary>
    public void Reload()
    {
        var previous = SelectedPaletteTheme;
        _controller.SuppressSelection = true;
        try
        {
            var selected = _controller.ReloadTree(Nodes, previous);
            ApplyThumbnailImages();
            ExpandAll();
            SelectedNode = selected;
        }
        finally
        {
            _controller.SuppressSelection = false;
        }
    }

    /// <summary>Applies the current theme node without requiring <see cref="AutoApply"/>.</summary>
    /// <returns><see langword="true"/> when a theme was applied.</returns>
    public bool ApplySelected() => _controller.Apply(SelectedNode);

    /// <inheritdoc />
    protected override void OnAfterSelect(TreeViewEventArgs e)
    {
        if (!_controller.SuppressSelection && AutoApply && !DesignMode)
        {
            _controller.Apply(e.Node);
        }

        base.OnAfterSelect(e);
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

    /// <summary>Gets the tree nodes.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new TreeNodeCollection Nodes => base.Nodes;

    /// <summary>Gets and sets the selected node.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new TreeNode? SelectedNode
    {
        get => base.SelectedNode;
        set => base.SelectedNode = value;
    }

    private static TreeNode? FindNode(TreeNodeCollection nodes, KryptonPaletteFileThemeItem? match)
    {
        if (match == null)
        {
            return null;
        }

        foreach (TreeNode node in nodes)
        {
            if (node.Tag is KryptonPaletteFileThemeItem item
                && KryptonPaletteFileThemeSelectorController.Matches(item, match))
            {
                return node;
            }

            var child = FindNode(node.Nodes, match);
            if (child != null)
            {
                return child;
            }
        }

        return null;
    }

    private void ApplyThumbnailImages()
    {
        ImageList = null;
        _thumbnailImages?.Dispose();
        _thumbnailImages = null;

        var list = new ImageList
        {
            ColorDepth = ColorDepth.Depth32Bit,
            ImageSize = _controller.ThumbnailSize
        };
        AssignNodeImages(Nodes, list);
        if (list.Images.Count == 0)
        {
            list.Dispose();
            return;
        }

        // Copy into the native ImageList before the TreeView handle is created.
        _ = list.Handle;
        _thumbnailImages = list;
        ImageList = list;
    }

    private static void AssignNodeImages(TreeNodeCollection nodes, ImageList list)
    {
        foreach (TreeNode node in nodes)
        {
            if (node.Tag is KryptonPaletteFileThemeItem item)
            {
                var image = item.GetImage(PaletteState.Normal);
                if (image != null)
                {
                    var index = list.Images.Count;
                    list.Images.Add(image);
                    node.ImageIndex = index;
                    node.SelectedImageIndex = index;
                }
            }

            AssignNodeImages(node.Nodes, list);
        }
    }
}
