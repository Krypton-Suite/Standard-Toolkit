#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class KryptonTaskDialogElementRichTextBox : KryptonTaskDialogElementBase,
    IKryptonTaskDialogElementText,
    IKryptonTaskDialogElementHeight,
    IKryptonTaskDialogElementRoundedCorners
{
    #region fields
    private bool _disposed;
    private KryptonTaskDialogKryptonRichTextBox _richTextBox;
    private KryptonContextMenu _contextMenu;
    KryptonContextMenuItem _kcmiCopy;
    KryptonContextMenuItem _kcmiCut;
    KryptonContextMenuItem _kcmiPaste;
    #endregion

    #region Identity
    public KryptonTaskDialogElementRichTextBox(KryptonTaskDialogDefaults taskDialogDefaults) : 
        base(taskDialogDefaults)
    {
        _disposed = false;
        _richTextBox = new();
        _contextMenu = new();
        _kcmiCopy = new KryptonContextMenuItem("Cop&y");
        _kcmiCut = new KryptonContextMenuItem("&Cut");
        _kcmiPaste = new KryptonContextMenuItem("&Paste");

        SetupPanel();
        SetupContextMenu();

        EnableContextMenu = true;
        ScrollBars = RichTextBoxScrollBars.Both;
    }
    #endregion

    #region Protected/Internal
    /// <inheritdoc/>
    protected override void OnPalettePaint(object? sender, PaletteLayoutEventArgs e)
    {
        base.OnPalettePaint(sender, e);

        // Flag dirty, and if visible call OnSizeChanged,
        // otherwise leave it deferred for a call from PerformLayout.
        LayoutDirty = true;
        if (Visible)
        {
            OnSizeChanged();
        }
    }

    /// <inheritdoc/>
    protected override void OnSizeChanged(bool performLayout = false)
    {
        // Updates / changes are deferred if the element is not visible or until PerformLayout is called
        if (LayoutDirty && (Visible || performLayout))
        {
            Panel.Refresh();

            base.OnSizeChanged(performLayout);
            LayoutDirty = false;
        }
    }

    /// <inheritdoc/>
    internal override void PerformLayout()
    {
        base.PerformLayout();
        OnSizeChanged(true);
    }
    #endregion

    #region Private
    private void SetupRichTextBox()
    {
        _richTextBox.Dock = DockStyle.Fill;
        _richTextBox.DetectUrls = false;
        _richTextBox.ShortcutsEnabled = false;
        _richTextBox.WordWrap = true;
        _richTextBox.Rtf = "";
        _richTextBox.Text = "";
    }

    private void SetupPanel()
    {
        Panel.Width = Defaults.ClientWidth;
        Panel.Height = 300;
        Panel.Padding = Defaults.PanelPadding1;

        Panel.Controls.Add(_richTextBox);
        SetupRichTextBox();
    }

    private void SetupContextMenu()
    {
        // Images are assigned static for now.
        // The contextmenu grays them when disabled so there's no need for switching images. They also work well among all themes.
        // Can be revisited if these will be integrated in the themes
        _kcmiCopy.Image = ResourceFiles.Toolbars.Office2013ToolbarImageResources.Office2013ToolbarCopyNormal;
        _kcmiCut.Image = ResourceFiles.Toolbars.Office2013ToolbarImageResources.Office2013ToolbarCutNormal;
        _kcmiPaste.Image = ResourceFiles.Toolbars.Office2013ToolbarImageResources.Office2013ToolbarPasteNormal;

        var items = new KryptonContextMenuItems();
        items.Items.Add(_kcmiCopy);
        items.Items.Add(_kcmiCut);
        items.Items.Add(_kcmiPaste);
        _contextMenu.Items.Add(items);
        
        _contextMenu.Opening += OnContextMenuOpening;
        _kcmiCopy.Click += OnContextMenuCopyClick;
        _kcmiCut.Click += OnContextMenuCutClick;
        _kcmiPaste.Click += OnContextMenuPasteClick;
    }

    private void OnContextMenuPasteClick(object? sender, EventArgs e)
    {
        _richTextBox.Paste();
    }

    private void OnContextMenuCutClick(object? sender, EventArgs e)
    {
        _richTextBox.Cut();
    }

    private void OnContextMenuCopyClick(object? sender, EventArgs e)
    {
        _richTextBox.Copy();
    }

    private void OnContextMenuOpening(object? sender, CancelEventArgs e)
    {
        _kcmiPaste.Enabled = Clipboard.ContainsText();

        _kcmiCut.Enabled =
        _kcmiCopy.Enabled = _richTextBox.SelectedText.Length > 0;
    }
    #endregion

    #region Public
    /// <summary>
    /// Get or set the text in the richtextbox.
    /// </summary>
    public string Text
    {
        get => _richTextBox.Text;
        set => _richTextBox.Text = value;
    }

    ///<summary>
    /// Enables the textbox.
    /// </summary>
    public bool Enabled
    {
        get => _richTextBox.Enabled;
        set => _richTextBox.Enabled = value;
    }

    ///<summary>
    /// if the text can be changed or not.
    /// </summary>
    public bool ReadOnly
    {
        get => _richTextBox.ReadOnly;
        set => _richTextBox.ReadOnly = value;
    }

    /// <summary>
    /// Which scrollbars should be enabled.
    /// </summary>
    public RichTextBoxScrollBars ScrollBars
    {
        get => _richTextBox.ScrollBars;
        set => _richTextBox.ScrollBars = value;
    }

    /// <summary>
    /// Get or set the element height.
    /// </summary>
    public int ElementHeight
    {
        get => Panel.Height;

        set
        {
            if (Panel.Height != value)
            {
                Panel.Height = value;
                LayoutDirty = true;
                OnSizeChanged();
            }
        }
    }

    /// <summary>
    /// If the Richtextbox contextmenu should be enabled.
    /// </summary>
    public bool EnableContextMenu
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;

                _richTextBox.KryptonContextMenu = value
                    ? _contextMenu
                    : null;
            }
        }
    }
    /// <summary>
    /// Rounds the RichTextBox corners.
    /// </summary>
    public bool RoundedCorners
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                _richTextBox.StateCommon.Border.Rounding = Defaults.GetCornerRouding(value);
            }
        }
    }
    #endregion

    #region IDispose
    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _contextMenu.Opening -= OnContextMenuOpening;
            _kcmiCopy.Click -= OnContextMenuCopyClick;
            _kcmiCut.Click -= OnContextMenuCutClick;
            _kcmiPaste.Click -= OnContextMenuPasteClick;
            _disposed = true;
        }

        base.Dispose(disposing);
    }
    #endregion
}
