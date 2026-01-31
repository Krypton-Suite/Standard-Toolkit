#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2023 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

[Category(@"code"), ToolboxBitmap(typeof(PictureBox)), Description(@""), ToolboxItem(true)]
public class KryptonPictureBox : PictureBox
{
    #region Instance Fields
    private readonly ToolTipManager _toolTipManager;
    private VisualPopupToolTip? _visualPopupToolTip;
    private readonly ViewLayoutNull _toolTipTarget = new ViewLayoutNull();
    #endregion

    #region Identity

    public KryptonPictureBox()
    {
        BackColor = Color.Transparent;

        // ToolTip support
        ToolTipValues = new ToolTipValues(null, GetDpiFactor);
        _toolTipManager = new ToolTipManager(ToolTipValues);
        _toolTipManager.ShowToolTip += OnShowToolTip;
        _toolTipManager.CancelToolTip += OnCancelToolTip;
    }

    #endregion

    #region Removed Designer Visibility

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Color BackColor { get => base.BackColor; set => base.BackColor = value; }

    #endregion

    #region ToolTip
    /// <summary>
    /// Gets access to the ToolTip values for this control.
    /// </summary>
    [Category(@"ToolTip")]
    [Description(@"Control ToolTip Text")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ToolTipValues ToolTipValues { get; set; }

    private bool ShouldSerializeToolTipValues() => !ToolTipValues.IsDefault;

    /// <summary>
    /// Resets the ToolTipValues property to its default value.
    /// </summary>
    public void ResetToolTipValues() => ToolTipValues.Reset();

    /// <summary>
    /// Gets access to the ToolTipManager used for displaying tool tips.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ToolTipManager ToolTipManager => _toolTipManager;
    #endregion

    #region Overrides
    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            OnCancelToolTip(this, EventArgs.Empty);
        }

        base.Dispose(disposing);
    }

    /// <inheritdoc />
    protected override void OnMouseEnter(EventArgs e)
    {
        _toolTipManager.MouseEnter(_toolTipTarget, this);
        base.OnMouseEnter(e);
    }

    /// <inheritdoc />
    protected override void OnMouseMove(MouseEventArgs e)
    {
        _toolTipManager.MouseMove(_toolTipTarget, this, e.Location);
        base.OnMouseMove(e);
    }

    /// <inheritdoc />
    protected override void OnMouseDown(MouseEventArgs e)
    {
        _toolTipManager.MouseDown(_toolTipTarget, this, e.Location, e.Button);
        base.OnMouseDown(e);
    }

    /// <inheritdoc />
    protected override void OnMouseUp(MouseEventArgs e)
    {
        _toolTipManager.MouseUp(_toolTipTarget, this, e.Location, e.Button);
        base.OnMouseUp(e);
    }

    /// <inheritdoc />
    protected override void OnMouseLeave(EventArgs e)
    {
        _toolTipManager.MouseLeave(null, this, null);
        base.OnMouseLeave(e);
    }
    #endregion

    #region Implementation
    private float GetDpiFactor() => DeviceDpi / 96F;

    private void OnShowToolTip(object? sender, ToolTipEventArgs e)
    {
        if (!IsDisposed
            && ToolTipValues.EnableToolTips
            && !DesignMode)
        {
            // Do not show tooltips when the form we are in does not have focus
            if (FindForm() is { ContainsFocus: false })
            {
                return;
            }

            _visualPopupToolTip?.Dispose();

            var palette = KryptonManager.CurrentGlobalPalette;
            if (palette != null)
            {
                var redirector = new PaletteRedirect(palette);
                var renderer = palette.GetRenderer();

                _visualPopupToolTip = new VisualPopupToolTip(redirector,
                    ToolTipValues,
                    renderer,
                    PaletteBackStyle.ControlToolTip,
                    PaletteBorderStyle.ControlToolTip,
                    CommonHelper.ContentStyleFromLabelStyle(ToolTipValues.ToolTipStyle),
                    ToolTipValues.ToolTipShadow);

                _visualPopupToolTip.Disposed += OnVisualPopupToolTipDisposed;
                _visualPopupToolTip.ShowRelativeTo(e.Target, e.ControlMousePosition);
            }
        }
    }

    private void OnCancelToolTip(object? sender, EventArgs e)
    {
        _visualPopupToolTip?.Dispose();
    }

    private void OnVisualPopupToolTipDisposed(object? sender, EventArgs e)
    {
        if (sender is VisualPopupToolTip popup)
        {
            popup.Disposed -= OnVisualPopupToolTipDisposed;
        }

        _visualPopupToolTip = null;
    }
    #endregion
}