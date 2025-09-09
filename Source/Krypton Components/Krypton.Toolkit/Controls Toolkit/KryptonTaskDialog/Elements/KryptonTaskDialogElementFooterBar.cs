#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 * © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

using System.Windows.Forms;

namespace Krypton.Toolkit;

public class KryptonTaskDialogElementFooterBar : KryptonTaskDialogElementBase,
    IKryptonTaskDialogElementFooterBar,
    IKryptonTaskDialogElementForeColor,
    IKryptonTaskDialogElementIconType
{
    #region Fields
    // incoming expander instance
    private KryptonTaskDialogElementContent _expander;
    // Panel to layout the controls
    private TableLayoutPanel _tlp;
    // Expander button
    private KryptonButton _expanderButton;
    // Expander text
    private KryptonLabel _expanderText;
    // Image / Icon controller
    private KryptonTaskDialogIconController _iconController;
    // Footnote picturebox,
    private PictureBox _footNotePictureBox;
    // Footnote label
    private KryptonLabel _footNoteText;
    // Disposal
    protected bool _disposed;
    // Margin on the right to evenly space controls
    private Padding _spacerPadding;
    #endregion

    #region Identity
    public KryptonTaskDialogElementFooterBar(KryptonTaskDialogDefaults taskDialogDefaults, KryptonTaskDialogElementContent expander) 
        : base(taskDialogDefaults)
    {
        _spacerPadding = new Padding(0, 0, Defaults.ComponentSpace, 0);

        _iconController = new();
        _expander = expander;
        _expanderButton = new();
        _expanderText = new();
        _footNotePictureBox = new();
        _footNoteText = new();
        _disposed = false;

        // default values
        ExpanderExpandedText = "Expand";
        ExpanderCollapsedText = "Collapse";

        // FootNote default IconType
        IconType = KryptonTaskDialogIconType.None;

        // init 
        SetupPanel();

        // Set the state
        UpdateExpanderEnabledState();
        UpdateExpanderImage();
        UpdateFootNoteIcon();
        UpdateForeColor();

        // Update at the next request
        LayoutDirty = true;
    }
    #endregion

    #region Public
    /// <inheritdoc/>
    public string FootNoteText 
    { 
        get => _footNoteText.Text;

        set
        {
            if (_footNoteText.Text != value)
            {
                _footNoteText.Text = value;
                _footNoteText.Invalidate();
                LayoutDirty = true;
            }
        }
    }

    /// <inheritdoc/>
    public string ExpanderExpandedText 
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                LayoutDirty = true;
                UpdateExpanderText();
            }
        }
    }

    /// <inheritdoc/>
    public string ExpanderCollapsedText 
    { 
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                LayoutDirty = true;
                UpdateExpanderText();
            }
        }
    }

    /// <summary>
    /// Icon used to decorate the footnote.
    /// </summary>
    public KryptonTaskDialogIconType IconType 
    { 
        get => field; 

        set
        {
            if ( field != value)
            {
                field = value;
                LayoutDirty = true;
                UpdateFootNoteIcon();
            }
        }
    }

    /// <inheritdoc/>
    public virtual Color ForeColor 
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                UpdateForeColor();
            }
        }
    }

    /// <inheritdoc/>
    public bool EnableExpanderControls 
    {
        get => field;

        set
        {
            if (field != value)
            {
                field = value;
                LayoutDirty = true;
                UpdateExpanderEnabledState();
                OnSizeChanged();
            }
        }
    }
    #endregion

    #region Overrides
    /// <inheritdoc/>
    internal override void PerformLayout()
    {
        base.PerformLayout();
        OnSizeChanged(true);
    }

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
    public override Color BackColor1 
    {
        get => base.BackColor1;
        set
        {
            base.BackColor1 = value;

            // labels need a little help
            _expanderText.Invalidate();
            _footNoteText.Invalidate();
        }
    }

    /// <inheritdoc/>
    public override Color BackColor2 
    {
        get => base.BackColor2;
        set
        {
            base.BackColor2 = value;

            // labels need a little help
            _expanderText.Invalidate();
            _footNoteText.Invalidate();
        }
    }
    #endregion

    #region Private
    private void OnSizeChanged(bool performLayout = false)
    {
        // Updates / changes are deferred if the element is not visible or until PerformLayout is called
        if (LayoutDirty && (Visible || performLayout))
        {
            // First set the size to zero so the images don't interfere with the tlp height
            _footNotePictureBox.Width  = 0;
            _footNotePictureBox.Height = 0;
            _expanderButton.Width      = 0;
            _expanderButton.Height     = 0;

            // update the images first to make them of the same size.
            UpdateFootNoteIcon();
            UpdateExpanderImage();

            // and make'm visible
            _footNotePictureBox.Width  = _tlp.Height;
            _footNotePictureBox.Height = _tlp.Height;
            _expanderButton.Width      = _tlp.Height;
            _expanderButton.Height     = _tlp.Height;

            // Size the panel. Add an extra PanelBottom to gie it some more space at the border
            Panel.Height = _tlp.Height + Defaults.PanelTop + Defaults.PanelBottom + Defaults.PanelBottom;

            // Tell everybody about it when visible.
            if (!performLayout)
            {
                base.OnSizeChanged();
            }

            // Done
            LayoutDirty = false;
        }
    }

    private void UpdateFootNoteIcon()
    {
        if (IconType != KryptonTaskDialogIconType.None)
        {
            _footNotePictureBox.Image = _iconController.GetImage(IconType, _tlp.Height);
            _footNotePictureBox.Visible = true;
        }
        else
        {
            _footNotePictureBox.Visible = false;
            _footNotePictureBox.Image = null;
        }
    }

    private void UpdateForeColor()
    {
        _expanderText.StateCommon.ShortText.Color1 = ForeColor;
        _footNoteText.StateCommon.ShortText.Color1 = ForeColor;

        _expanderText.Invalidate();
        _footNoteText.Invalidate();
    }

    private void UpdateExpanderImage()
    {
        if (EnableExpanderControls)
        {
            _expanderButton.StateCommon.Back.Image = _expander.Visible
                    ? _iconController.GetImage(KryptonTaskDialogIconType.ArrowGrayUp, _tlp.Height)
                    : _iconController.GetImage(KryptonTaskDialogIconType.ArrowGrayDown, _tlp.Height);

            _expanderButton.Invalidate();
        }
    }

    private void UpdateExpanderText()
    {
        if (EnableExpanderControls)
        {
            _expanderText.Text = _expander.Visible
                ? ExpanderCollapsedText
                : ExpanderExpandedText;

            _expanderText.Invalidate();
        }
    }

    private void UpdateExpanderEnabledState()
    {
        _expanderButton.Visible = EnableExpanderControls;
        _expanderText.Visible = EnableExpanderControls;

        if (EnableExpanderControls)
        {
            UpdateExpanderText();
            UpdateExpanderImage();
        }
    }

    private void OnExpanderButtonClick(object? sender, EventArgs e)
    {
        // briefly detach from the event
        WireExpanderVisibleChanged(false);

        // change mode
        _expander.Visible = !_expander.Visible;

        // change the button icon & text
        UpdateExpanderImage();
        UpdateExpanderText();

        // attach the event again
        WireExpanderVisibleChanged(true);
    }

    private void OnExpanderVisibleChanged()
    {
        UpdateExpanderEnabledState();
    }

    private void WireExpanderVisibleChanged(bool wireEvent)
    {
        if (wireEvent)
        {
            _expander.VisibleChanged += OnExpanderVisibleChanged;
        }
        else
        {
            _expander.VisibleChanged -= OnExpanderVisibleChanged;
        }
    }

    private void OnExpanderVisibleChanged()
    {
        UpdateExpanderEnabledState();
    }

    private void WireExpanderVisibleChanged(bool wireEvent)
    {
        if (wireEvent)
        {
            _expander.VisibleChanged += OnExpanderVisibleChanged;
        }
        else
        {
            _expander.VisibleChanged -= OnExpanderVisibleChanged;
        }
    }

    private void SetupTableLayoutPanel()
    {
        _tlp = new()
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Width = Defaults.TLP.StdMaxSize.Width,
            Padding = Defaults.NullPadding,
            Margin = Defaults.NullMargin,
            BackColor = Color.Transparent,
            CellBorderStyle = TableLayoutPanelCellBorderStyle.None
        };

        // 1 row
        _tlp.RowCount = 1;
        _tlp.ColumnStyles.Clear();
        _tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        // 4 columns 
        _tlp.ColumnCount = 4;
        _tlp.RowStyles.Clear();
        // expander button
        _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        // expander text
        _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        // footnote icon
        _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        // footnote text
        _tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

        // Add the controls to the layout
        _tlp.Controls.Add(_expanderButton, 0, 0);
        _tlp.Controls.Add(_expanderText, 1, 0);
        _tlp.Controls.Add(_footNotePictureBox, 2, 0);
        _tlp.Controls.Add(_footNoteText, 3, 0);
    }

    private void SetupControls()
    {
        // configure controls
        _expanderButton.AutoSize = false;
        _expanderButton.Margin = _spacerPadding;
        _expanderButton.StateCommon.Back.ImageStyle = PaletteImageStyle.CenterMiddle;
        _expanderButton.StateCommon.Back.Color1 = Color.WhiteSmoke;
        _expanderButton.Text = string.Empty;
        _expanderButton.Click += OnExpanderButtonClick;

        _expanderText.AutoSize = true;
        _expanderText.Margin = _spacerPadding;
        _expanderText.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;

        _footNotePictureBox.Margin = _spacerPadding;
        _footNotePictureBox.Padding = Defaults.NullPadding;
        _footNotePictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

        _footNoteText.AutoSize = true;
        _footNoteText.Margin = Defaults.NullPadding;
        _footNoteText.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;

        // If the expander element changes visibility we need to react to that.
        WireExpanderVisibleChanged(true);
    }

    private void SetupPanel()
    {
        // Panel height, and padding
        Panel.Width = Defaults.ClientWidth;
        Panel.Padding = Defaults.PanelPadding1;
        
        SetupTableLayoutPanel();
        SetupControls();
        Panel.Controls.Add(_tlp);

        // If the expander element changes visibility we need to react to that.
        WireExpanderVisibleChanged(true);
    }
    #endregion

    #region IDispose
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _expanderButton.Click -= OnExpanderButtonClick;
            WireExpanderVisibleChanged(false);

            _iconController.Dispose();

            _disposed = true;
        }

        base.Dispose(disposing);
    }
    #endregion
}
