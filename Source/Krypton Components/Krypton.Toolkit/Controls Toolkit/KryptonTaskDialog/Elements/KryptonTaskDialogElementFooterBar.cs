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
    private const int _tableLayoutPanelHeight = 24;
    // incoming expander instande
    private KryptonTaskDialogElementContent _expander;
    // Panel to layout the controls
    private TableLayoutPanel _tableLayoutPanel;
    // Expander button
    private KryptonButton _expanderButton;
    // Expander text
    private KryptonLabel _expanderText;
    // Expander expanded image
    private Image? _expanderExpandedImage;
    // Expander collapsed image
    private Image? _expanderCollapsedImage;
    // Image / Icon controller
    private KryptonTaskDialogIconController _iconController;
    // Footnote picturebox,
    private PictureBox _footNotePictureBox;
    // Footnote label
    private KryptonLabel _footNoteText;
    // Disposal
    protected bool _disposed;
    // null padding preset
    private Padding _nullPadding;
    // Margin on the right to evenly space controls
    private Padding _spacerPadding;
    // used for control min and max sizes
    private Size _size_0_tableLayoutPanelHeight;
    // fixed control size
    private Size _size_tableLayoutPanelHeight_tableLayoutPanelHeight;
    #endregion
    
    #region Identity
    public KryptonTaskDialogElementFooterBar(KryptonTaskDialogElementContent expander)
    {
        _nullPadding = new(0);
        _spacerPadding = new Padding(0, 0, KryptonTaskDialog.Defaults.ComponentSpace, 0);
        _size_0_tableLayoutPanelHeight = new Size(0, _tableLayoutPanelHeight);
        _size_tableLayoutPanelHeight_tableLayoutPanelHeight = new Size(_tableLayoutPanelHeight, _tableLayoutPanelHeight);

        _iconController = new();
        _expander = expander;
        _tableLayoutPanel = new();
        _expanderButton = new();
        _expanderText = new();
        _footNotePictureBox = new();
        _footNoteText = new();
        _disposed = false;

        // default values
        ExpanderExpandedText = "Expanded";
        ExpanderCollapsedText = "Collapsed";

        // Expander button images
        _expanderExpandedImage = ResourceFiles.TaskDialog.TaskDialogImageResources.DoubleChevronSmallUp_20x20;
        _expanderCollapsedImage = ResourceFiles.TaskDialog.TaskDialogImageResources.DoubleChevronSmallDown_20x20;

        // FootNote default IconType
        IconType = KryptonTaskDialogIconType.None;

        // init 
        SetupPanel();

        // refresh the state
        UpdateExpanderEnabledState();
    }
    #endregion

    #region Public
    /// <inheritdoc/>
    public string FootNoteText 
    { 
        get => _footNoteText.Text;
        set
        {
            _footNoteText.Text = value;
            _footNoteText.Invalidate();
        }

    }

    /// <inheritdoc/>
    public string ExpanderExpandedText 
    {
        get => field;
        set
        {
            field = value;
            UpdateExpanderText();
        }
    }

    /// <inheritdoc/>
    public string ExpanderCollapsedText 
    { 
        get => field;
        set
        {
            field = value;
            UpdateExpanderText();
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
                UpdateFootNoteIcon();
            }
        }
    }

    /// <inheritdoc/>
    public virtual Color ForeColor {
        get => field;

        set
        {
            field = value;
            UpdateForeColor();
        }
    }

    /// <inheritdoc/>
    public bool EnableExpanderControls {
        get => field;

        set
        {
            field = value;
            UpdateExpanderEnabledState();
        }
    }
    #endregion

    #region Overrides
    /// <summary>
    /// Not implemented
    /// </summary>
    /// <returns>String.Empty</returns>
    public override string ToString()
    {
        return string.Empty;
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
    private void UpdateFootNoteIcon()
    {
        _footNotePictureBox.Image = IconType != KryptonTaskDialogIconType.None
            ? _iconController.GetImage(IconType, _footNotePictureBox.Size.Height)
            : null;
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
            _expanderButton.StateCommon.Back.Image = null;
            _expanderButton.StateCommon.Back.Image = _expander.Visible
                ? _expanderExpandedImage
                : _expanderCollapsedImage;

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

        UpdateExpanderText();
        UpdateExpanderImage();
    }

    private void OnExpanderButtonClick(object? sender, EventArgs e)
    {
        // change mode
        _expander.Visible = !_expander.Visible;

        // change the button icon
        UpdateExpanderImage();
        UpdateExpanderText();
    }

    private void SetupPanel()
    {
        // Panel height, and padding
        Panel.Height = _tableLayoutPanelHeight + KryptonTaskDialog.Defaults.PanelTop + KryptonTaskDialog.Defaults.PanelBottom + 10;
        Panel.Padding = new Padding(KryptonTaskDialog.Defaults.PanelLeft, KryptonTaskDialog.Defaults.PanelTop, KryptonTaskDialog.Defaults.PanelRight, KryptonTaskDialog.Defaults.PanelBottom);

        // Layout panel setup
        _tableLayoutPanel.Dock = DockStyle.Fill;
        _tableLayoutPanel.Height = _tableLayoutPanelHeight;
        _tableLayoutPanel.Margin = new Padding(0);
        _tableLayoutPanel.Padding = new Padding(0);

        // 1 row
        _tableLayoutPanel.RowCount = 1;
        _tableLayoutPanel.ColumnStyles.Clear();
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, _tableLayoutPanelHeight));
        // 4 columns 
        _tableLayoutPanel.ColumnCount = 4;
        _tableLayoutPanel.RowStyles.Clear();
        // expander button
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        // expander text
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        // footnote icon
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        // footnote text
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

        // Add the controls to the layout
        _tableLayoutPanel.Controls.Add(_expanderButton, 0, 0);
        _tableLayoutPanel.Controls.Add(_expanderText, 1, 0);
        _tableLayoutPanel.Controls.Add(_footNotePictureBox, 2, 0);
        _tableLayoutPanel.Controls.Add(_footNoteText, 3, 0);
        Panel.Controls.Add(_tableLayoutPanel);

        // configure controls
        _expanderButton.AutoSize = false;
        _expanderButton.Margin = _spacerPadding;
        _expanderButton.MaximumSize = _size_tableLayoutPanelHeight_tableLayoutPanelHeight;
        _expanderButton.MinimumSize = _size_tableLayoutPanelHeight_tableLayoutPanelHeight;
        _expanderButton.Width = _tableLayoutPanelHeight;
        _expanderButton.Height = _tableLayoutPanelHeight;
        _expanderButton.StateCommon.Back.ImageStyle = PaletteImageStyle.CenterMiddle;
        _expanderButton.StateCommon.Back.Color1 = Color.WhiteSmoke;
        _expanderButton.Text = string.Empty;
        _expanderButton.Click += OnExpanderButtonClick;

        _expanderText.AutoSize = true;
        _expanderText.Margin = _spacerPadding;
        _expanderText.MaximumSize = _size_0_tableLayoutPanelHeight;
        _expanderText.MinimumSize = _size_0_tableLayoutPanelHeight;
        _expanderText.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;

        _footNotePictureBox.Size = _size_tableLayoutPanelHeight_tableLayoutPanelHeight;
        _footNotePictureBox.Margin = _spacerPadding;
        _footNotePictureBox.Padding = _nullPadding;
        _footNotePictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

        _footNoteText.AutoSize = true;
        _footNoteText.Margin = _nullPadding;
        _footNoteText.MaximumSize = _size_0_tableLayoutPanelHeight;
        _footNoteText.MinimumSize = _size_0_tableLayoutPanelHeight;
        _footNoteText.StateCommon.ShortText.TextV = PaletteRelativeAlign.Center;
    }
    #endregion

    #region IDispose
    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _expanderButton.Click -= OnExpanderButtonClick;
            _iconController.Dispose();

            _disposed = true;
        }

        base.Dispose(disposing);
    }
    #endregion
}
