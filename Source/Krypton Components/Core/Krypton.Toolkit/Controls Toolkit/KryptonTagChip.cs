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
/// Compact themed chip used by <see cref="KryptonTagInput"/> to display one tag.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
internal sealed class KryptonTagChip : VisualSimpleBase
{
    #region Instance Fields

    private readonly KryptonTagInput _owner;
    private readonly ButtonSpecAny _closeSpec;
    private readonly ButtonSpecCollection<ButtonSpecAny> _buttonSpecs;
    private readonly ButtonSpecManagerDraw _buttonManager;
    private readonly ViewDrawDocker _drawDocker;
    private readonly ViewDrawContent _drawContent;
    private readonly FixedContentValue _contentValues;
    private readonly PaletteMetricRedirect _metrics;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonTagChip"/> class.
    /// </summary>
    /// <param name="owner">Owning tag input control.</param>
    /// <param name="tag">Tag text shown on the chip.</param>
    /// <param name="chipId">Stable identity used when duplicate tag text is allowed.</param>
    public KryptonTagChip(KryptonTagInput owner, string tag, int chipId)
    {
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
        ChipId = chipId;

        SetStyle(ControlStyles.Selectable, false);
        TabStop = false;

        _contentValues = new FixedContentValue(tag, string.Empty, null, Color.Empty);

        StateCommon = new PaletteTripleRedirect(Redirector, PaletteBackStyle.ButtonStandalone,
            PaletteBorderStyle.ButtonStandalone, PaletteContentStyle.ButtonStandalone, NeedPaintDelegate);
        StateDisabled = new PaletteTriple(StateCommon, NeedPaintDelegate);
        StateNormal = new PaletteTriple(StateCommon, NeedPaintDelegate);

        StateCommon.Content.Padding = new Padding(8, 1, 2, 1);

        _drawDocker = new ViewDrawDocker(StateNormal.Back, StateNormal.Border, null);
        _drawContent = new ViewDrawContent(StateNormal.Content, _contentValues, VisualOrientation.Top);
        _drawDocker.Add(_drawContent, ViewDockStyle.Fill);
        ViewManager = new ViewManager(this, _drawDocker);

        _buttonSpecs = new ButtonSpecCollection<ButtonSpecAny>(this);
        _closeSpec = new ButtonSpecAny
        {
            Type = PaletteButtonSpecStyle.Close,
            ToolTipTitle = @"Remove"
        };
        _closeSpec.Click += OnCloseClick;
        _buttonSpecs.Add(_closeSpec);

        _metrics = new PaletteMetricRedirect(Redirector);
        _buttonManager = new ButtonSpecManagerDraw(this, Redirector, _buttonSpecs, null,
            [_drawDocker],
            [_metrics],
            [PaletteMetricInt.HeaderButtonEdgeInsetInputControl],
            [PaletteMetricPadding.HeaderButtonPaddingInputControl],
            CreateToolStripRenderer,
            NeedPaintDelegate);

        AccessibleName = tag;
        AccessibleRole = AccessibleRole.PushButton;

        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Margin = new Padding(2);
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _closeSpec.Click -= OnCloseClick;
            _buttonManager.Destruct();
        }

        base.Dispose(disposing);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the stable identity of this chip.
    /// </summary>
    public int ChipId { get; }

    /// <summary>
    /// Gets the tag text this chip represents.
    /// </summary>
    public string TagText => _contentValues.ShortText ?? string.Empty;

    /// <summary>
    /// Gets access to the common chip appearance.
    /// </summary>
    public PaletteTripleRedirect StateCommon { get; }

    /// <summary>
    /// Gets access to the disabled chip appearance.
    /// </summary>
    public PaletteTriple StateDisabled { get; }

    /// <summary>
    /// Gets access to the normal chip appearance.
    /// </summary>
    public PaletteTriple StateNormal { get; }

    /// <summary>
    /// Updates the displayed tag text.
    /// </summary>
    /// <param name="tag">New tag text.</param>
    public void SetTagText(string tag)
    {
        _contentValues.ShortText = tag ?? string.Empty;
        AccessibleName = _contentValues.ShortText;
        PerformNeedPaint(true);
    }

    /// <summary>
    /// Applies rounding, optional category colour, and close-button visibility.
    /// </summary>
    /// <param name="categoryColor">Override fill colour, or empty to inherit the button palette.</param>
    /// <param name="rounding">Corner rounding for the chip border.</param>
    /// <param name="showRemove">Whether the close button is visible.</param>
    /// <param name="enabled">Whether the close button can be clicked.</param>
    public void ApplyAppearance(Color categoryColor, float rounding, bool showRemove, bool enabled)
    {
        StateCommon.Border.Rounding = rounding;
        StateCommon.Border.GraphicsHint = PaletteGraphicsHint.AntiAlias;

        if (!categoryColor.IsEmpty)
        {
            StateCommon.Back.Color1 = categoryColor;
            StateCommon.Back.ColorStyle = PaletteColorStyle.Solid;
            StateCommon.Content.ShortText.Color1 = IsDark(categoryColor) ? Color.White : Color.Black;
        }
        else
        {
            StateCommon.Back.Color1 = Color.Empty;
            StateCommon.Back.ColorStyle = PaletteColorStyle.Inherit;
            StateCommon.Content.ShortText.Color1 = Color.Empty;
        }

        _closeSpec.Visible = showRemove;
        _closeSpec.Enabled = enabled ? ButtonEnabled.True : ButtonEnabled.False;
        _buttonManager.RefreshButtons();
        PerformNeedPaint(true);
    }

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override Size DefaultSize => new Size(72, 22);

    /// <inheritdoc />
    protected override void OnEnabledChanged(EventArgs e)
    {
        if (Enabled)
        {
            _drawDocker.SetPalettes(StateNormal.Back, StateNormal.Border);
            _drawContent.SetPalette(StateNormal.Content);
        }
        else
        {
            _drawDocker.SetPalettes(StateDisabled.Back, StateDisabled.Border);
            _drawContent.SetPalette(StateDisabled.Content);
        }

        _drawDocker.Enabled = Enabled;
        _drawContent.Enabled = Enabled;
        _buttonManager.RefreshButtons();
        PerformNeedPaint(true);
        base.OnEnabledChanged(e);
    }

    /// <inheritdoc />
    protected override void OnButtonSpecChanged(object? sender, EventArgs e)
    {
        _buttonManager.RecreateButtons();
        base.OnButtonSpecChanged(sender, e);
    }

    #endregion

    #region Implementation

    private void OnCloseClick(object? sender, EventArgs e)
    {
        if (_closeSpec.Enabled == ButtonEnabled.True)
        {
            _owner.RemoveChipById(ChipId);
        }
    }

    private static bool IsDark(Color color) =>
        ((color.R * 0.299) + (color.G * 0.587) + (color.B * 0.114)) < 128.0;

    #endregion
}
