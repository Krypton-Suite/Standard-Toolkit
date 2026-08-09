#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Shows <see cref="VisualPopupToolTip"/> for hovered radial menu items using <see cref="ToolTipValues"/>.
/// </summary>
internal sealed class RadialMenuToolTipHost : IDisposable
{
    #region Instance Fields

    private readonly Control _placementControl;
    private readonly Func<PaletteBase> _resolvePalette;
    private readonly System.Windows.Forms.Timer _showTimer;
    private VisualPopupToolTip? _popup;
    private PaletteRedirect? _redirector;
    private KryptonRadialMenuItemBase? _pendingItem;
    private KryptonRadialMenuItemBase? _activeItem;
    private bool _disposed;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="RadialMenuToolTipHost"/> class.
    /// </summary>
    /// <param name="placementControl">Control used for tooltip placement.</param>
    /// <param name="resolvePalette">Delegate that returns the active palette.</param>
    public RadialMenuToolTipHost(Control placementControl, Func<PaletteBase> resolvePalette)
    {
        _placementControl = placementControl ?? throw new ArgumentNullException(nameof(placementControl));
        _resolvePalette = resolvePalette ?? throw new ArgumentNullException(nameof(resolvePalette));
        _showTimer = new System.Windows.Forms.Timer();
        _showTimer.Tick += OnShowTimerTick;
    }

    #endregion

    #region Public

    /// <summary>
    /// Updates the hovered item; shows or cancels tooltips as needed.
    /// </summary>
    /// <param name="item">Hovered item, or null when nothing is hovered.</param>
    public void UpdateHover(KryptonRadialMenuItemBase? item)
    {
        if (_disposed)
        {
            return;
        }

        if (ReferenceEquals(_activeItem, item) || ReferenceEquals(_pendingItem, item))
        {
            return;
        }

        Cancel();

        if (item?.ToolTipValues.EnableToolTips != true)
        {
            return;
        }

        _pendingItem = item;
        _showTimer.Interval = Math.Max(1, item.ToolTipValues.ShowIntervalDelay);
        _showTimer.Start();
    }

    /// <summary>
    /// Hides any pending or visible tooltip.
    /// </summary>
    public void Cancel()
    {
        _showTimer.Stop();
        _pendingItem = null;
        _activeItem = null;
        DisposePopup();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        Cancel();
        _showTimer.Tick -= OnShowTimerTick;
        _showTimer.Dispose();
        _redirector?.Dispose();
        _redirector = null;
    }

    #endregion

    #region Implementation

    private void OnShowTimerTick(object? sender, EventArgs e)
    {
        _showTimer.Stop();
        var item = _pendingItem;
        _pendingItem = null;
        if (item == null || _disposed || !_placementControl.IsHandleCreated)
        {
            return;
        }

        ShowToolTip(item);
    }

    private void ShowToolTip(KryptonRadialMenuItemBase item)
    {
        var values = item.ToolTipValues;
        var args = new ToolTipNeededEventArgs(0, item)
        {
            Heading = values.Heading == @"Heading" ? string.Empty : values.Heading,
            Description = values.Description == @"Description" ? string.Empty : values.Description,
            Icon = values.Image
        };
        item.OnToolTipNeeded(args);
        if (args.IsEmpty)
        {
            return;
        }

        values.Heading = args.Heading;
        values.Description = args.Description;
        values.Image = args.Icon;

        DisposePopup();

        var palette = _resolvePalette();
        _redirector?.Dispose();
        _redirector = new PaletteRedirect(palette);
        var renderer = palette.GetRenderer();
        _popup = new VisualPopupToolTip(
            _redirector,
            values,
            renderer,
            PaletteBackStyle.ControlToolTip,
            PaletteBorderStyle.ControlToolTip,
            CommonHelper.ContentStyleFromLabelStyle(values.ToolTipStyle),
            values.ToolTipShadow);
        _popup.Disposed += OnPopupDisposed;
        _popup.ShowRelativeTo(_placementControl, Control.MousePosition, values.ToolTipPosition);
        _activeItem = item;
    }

    private void OnPopupDisposed(object? sender, EventArgs e)
    {
        if (sender is VisualPopupToolTip popup)
        {
            popup.Disposed -= OnPopupDisposed;
        }

        if (ReferenceEquals(_popup, sender))
        {
            _popup = null;
            _activeItem = null;
        }
    }

    private void DisposePopup()
    {
        if (_popup == null)
        {
            return;
        }

        var popup = _popup;
        _popup = null;
        popup.Disposed -= OnPopupDisposed;
        popup.Dispose();
    }

    #endregion
}
