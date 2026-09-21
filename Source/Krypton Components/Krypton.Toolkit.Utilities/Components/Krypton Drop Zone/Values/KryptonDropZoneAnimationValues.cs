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
/// Animation settings for <see cref="KryptonDropZone"/> drop-area visual feedback.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonDropZoneAnimationValues : Storage
{
    #region Static Defaults

    private const bool DEFAULT_ENABLED = true;
    private const int DEFAULT_DURATION = 200;
    private const int DEFAULT_FLASH_DURATION = 450;
    private const int DEFAULT_QUOTA_WARNING_THRESHOLD = 80;

    #endregion

    #region Instance Fields

    private KryptonDropZone? _owner;
    private bool _enabled = DEFAULT_ENABLED;
    private int _duration = DEFAULT_DURATION;
    private int _flashDuration = DEFAULT_FLASH_DURATION;
    private int _quotaWarningThresholdPercent = DEFAULT_QUOTA_WARNING_THRESHOLD;
    private Color _idleColor = Color.WhiteSmoke;
    private Color _dragHoverColor = Color.LightBlue;
    private Color _dropSuccessColor = Color.PaleGreen;
    private Color _dropRejectedColor = Color.MistyRose;
    private Color _dropPartialColor = Color.LemonChiffon;
    private Color _quotaWarningColor = Color.Khaki;
    private Color _quotaExceededColor = Color.LightCoral;

    #endregion

    #region Identity

    internal KryptonDropZoneAnimationValues(KryptonDropZone owner) => _owner = owner;

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault =>
        _enabled == DEFAULT_ENABLED &&
        _duration == DEFAULT_DURATION &&
        _flashDuration == DEFAULT_FLASH_DURATION &&
        _quotaWarningThresholdPercent == DEFAULT_QUOTA_WARNING_THRESHOLD &&
        _idleColor == Color.WhiteSmoke &&
        _dragHoverColor == Color.LightBlue &&
        _dropSuccessColor == Color.PaleGreen &&
        _dropRejectedColor == Color.MistyRose &&
        _dropPartialColor == Color.LemonChiffon &&
        _quotaWarningColor == Color.Khaki &&
        _quotaExceededColor == Color.LightCoral;

    #endregion

    #region Behavior

    [Category(@"Behavior")]
    [Description(@"Whether drop-zone color animations are enabled.")]
    [DefaultValue(DEFAULT_ENABLED)]
    public bool Enabled
    {
        get => _enabled;
        set
        {
            if (_enabled == value)
            {
                return;
            }

            _enabled = value;
            _owner?.OnAnimationValuesChanged();
        }
    }

    [Category(@"Behavior")]
    [Description(@"Duration in milliseconds for transitions such as drag hover and returning to idle.")]
    [DefaultValue(DEFAULT_DURATION)]
    public int Duration
    {
        get => _duration;
        set => _duration = Math.Max(0, value);
    }

    [Category(@"Behavior")]
    [Description(@"Total duration in milliseconds for drop result feedback flashes (success, rejected, partial).")]
    [DefaultValue(DEFAULT_FLASH_DURATION)]
    public int FlashDuration
    {
        get => _flashDuration;
        set => _flashDuration = Math.Max(0, value);
    }

    [Category(@"Behavior")]
    [Description(@"When upload quota usage is at or above this percentage, the idle/drag state uses QuotaWarningColor.")]
    [DefaultValue(DEFAULT_QUOTA_WARNING_THRESHOLD)]
    public int QuotaWarningThresholdPercent
    {
        get => _quotaWarningThresholdPercent;
        set => _quotaWarningThresholdPercent = Math.Max(0, Math.Min(100, value));
    }

    #endregion

    #region Colors

    [Category(@"Colors")]
    [Description(@"Background color when the drop zone is idle.")]
    public Color IdleColor
    {
        get => _idleColor;
        set => SetColor(ref _idleColor, value);
    }

    [Category(@"Colors")]
    [Description(@"Background color while a valid file drag hovers over the drop zone.")]
    public Color DragHoverColor
    {
        get => _dragHoverColor;
        set => SetColor(ref _dragHoverColor, value);
    }

    [Category(@"Colors")]
    [Description(@"Flash color when all dropped files in a batch are accepted.")]
    public Color DropSuccessColor
    {
        get => _dropSuccessColor;
        set => SetColor(ref _dropSuccessColor, value);
    }

    [Category(@"Colors")]
    [Description(@"Flash color when all dropped files in a batch are rejected.")]
    public Color DropRejectedColor
    {
        get => _dropRejectedColor;
        set => SetColor(ref _dropRejectedColor, value);
    }

    [Category(@"Colors")]
    [Description(@"Flash color when a batch contains both accepted and rejected files.")]
    public Color DropPartialColor
    {
        get => _dropPartialColor;
        set => SetColor(ref _dropPartialColor, value);
    }

    [Category(@"Colors")]
    [Description(@"Color used when upload quota usage is near the limit.")]
    public Color QuotaWarningColor
    {
        get => _quotaWarningColor;
        set => SetColor(ref _quotaWarningColor, value);
    }

    [Category(@"Colors")]
    [Description(@"Color used when upload quota is exhausted or a drop would exceed it.")]
    public Color QuotaExceededColor
    {
        get => _quotaExceededColor;
        set => SetColor(ref _quotaExceededColor, value);
    }

    private bool ShouldSerializeIdleColor() => _idleColor != Color.WhiteSmoke;
    private void ResetIdleColor() => IdleColor = Color.WhiteSmoke;
    private bool ShouldSerializeDragHoverColor() => _dragHoverColor != Color.LightBlue;
    private void ResetDragHoverColor() => DragHoverColor = Color.LightBlue;
    private bool ShouldSerializeDropSuccessColor() => _dropSuccessColor != Color.PaleGreen;
    private void ResetDropSuccessColor() => DropSuccessColor = Color.PaleGreen;
    private bool ShouldSerializeDropRejectedColor() => _dropRejectedColor != Color.MistyRose;
    private void ResetDropRejectedColor() => DropRejectedColor = Color.MistyRose;
    private bool ShouldSerializeDropPartialColor() => _dropPartialColor != Color.LemonChiffon;
    private void ResetDropPartialColor() => DropPartialColor = Color.LemonChiffon;
    private bool ShouldSerializeQuotaWarningColor() => _quotaWarningColor != Color.Khaki;
    private void ResetQuotaWarningColor() => QuotaWarningColor = Color.Khaki;
    private bool ShouldSerializeQuotaExceededColor() => _quotaExceededColor != Color.LightCoral;
    private void ResetQuotaExceededColor() => QuotaExceededColor = Color.LightCoral;

    #endregion

    #region Implementation

    public void Reset()
    {
        Enabled = DEFAULT_ENABLED;
        Duration = DEFAULT_DURATION;
        FlashDuration = DEFAULT_FLASH_DURATION;
        QuotaWarningThresholdPercent = DEFAULT_QUOTA_WARNING_THRESHOLD;
        IdleColor = Color.WhiteSmoke;
        DragHoverColor = Color.LightBlue;
        DropSuccessColor = Color.PaleGreen;
        DropRejectedColor = Color.MistyRose;
        DropPartialColor = Color.LemonChiffon;
        QuotaWarningColor = Color.Khaki;
        QuotaExceededColor = Color.LightCoral;
    }

    internal void SetOwner(KryptonDropZone owner) => _owner = owner;

    private void SetColor(ref Color field, Color value)
    {
        if (field == value)
        {
            return;
        }

        field = value;
        _owner?.OnAnimationValuesChanged();
    }

    #endregion
}
