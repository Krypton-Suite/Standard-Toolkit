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
/// Storage for taskbar-related values (overlay icon, progress, and jump list).
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class WindowsShellValues : Storage
{
    #region Instance Fields

    private readonly TaskbarOverlayIconValues _overlayIconValues;

    #endregion

    #region Identity

    public WindowsShellValues(NeedPaintHandler needPaint)
    {
        NeedPaint = needPaint;

        _overlayIconValues = new TaskbarOverlayIconValues(needPaint);

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

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)] 
    public override bool IsDefault => !(ShouldSerializeOverlayIconValues());

    #endregion

    #region Implementation

    public void Reset()
    {
        ResetOverlayIconValues();
    }

    #endregion
}