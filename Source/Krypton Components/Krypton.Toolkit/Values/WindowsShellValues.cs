#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for taskbar-related values (overlay icon, thumbnail buttons, progress, and jump list).
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class WindowsShellValues : Storage
{
    #region Instance Fields

    private readonly TaskbarOverlayIconValues _overlayIconValues;
    private readonly TaskbarThumbnailButtonValues _thumbnailButtonValues;

    #endregion

    #region Identity

    public WindowsShellValues(NeedPaintHandler needPaint)
    {
        NeedPaint = needPaint;

        _overlayIconValues = new TaskbarOverlayIconValues(needPaint);
        _thumbnailButtonValues = new TaskbarThumbnailButtonValues(needPaint);

        Reset();
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets access to the taskbar overlay icon values.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Taskbar overlay icon to display on the taskbar button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TaskbarOverlayIconValues OverlayIconValues => _overlayIconValues;

    private bool ShouldSerializeOverlayIconValues() => !OverlayIconValues.IsDefault;

    /// <summary>
    /// Resets the OverlayIcon property to its default value.
    /// </summary>
    public void ResetOverlayIconValues() => OverlayIconValues.Reset();

    /// <summary>
    /// Gets access to the taskbar thumbnail toolbar button values (quick actions in the taskbar preview).
    /// </summary>
    [Category(@"Behavior")]
    [Description(@"Buttons shown in the taskbar thumbnail preview (e.g. play, pause). Up to 7 buttons.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public TaskbarThumbnailButtonValues ThumbnailButtonValues => _thumbnailButtonValues;

    private bool ShouldSerializeThumbnailButtonValues() => !ThumbnailButtonValues.IsDefault;

    /// <summary>
    /// Resets the ThumbnailButtonValues property to its default value.
    /// </summary>
    public void ResetThumbnailButtonValues() => ThumbnailButtonValues.Reset();

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault => !ShouldSerializeOverlayIconValues() && !ShouldSerializeThumbnailButtonValues();

    #endregion

    #region Implementation

    public void Reset()
    {
        ResetOverlayIconValues();
        ResetThumbnailButtonValues();
    }

    #endregion
}