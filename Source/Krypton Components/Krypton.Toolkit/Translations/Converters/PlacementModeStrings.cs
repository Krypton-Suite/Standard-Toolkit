#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable InconsistentNaming
namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="PlacementModeConverter"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class PlacementModeStrings : GlobalId
{
    #region Static Fields

    private const string DEFAULT_PLACEMENT_MODE_ABSOLUTE = @"Placement Mode - Absolute";
    private const string DEFAULT_PLACEMENT_MODE_ABSOLUTE_POINT = @"Placement Mode - Absolute Point";
    internal const string DEFAULT_PLACEMENT_MODE_BOTTOM = @"Placement Mode - Bottom";
    private const string DEFAULT_PLACEMENT_MODE_CENTER = @"Placement Mode - Center";
    private const string DEFAULT_PLACEMENT_MODE_LEFT = @"Placement Mode - Left";
    private const string DEFAULT_PLACEMENT_MODE_MOUSE = @"Placement Mode - Mouse";
    private const string DEFAULT_PLACEMENT_MODE_MOUSE_POINT = @"Placement Mode - Mouse Point";
    private const string DEFAULT_PLACEMENT_MODE_RELATIVE = @"Placement Mode - Relative";
    private const string DEFAULT_PLACEMENT_MODE_RELATIVE_POINT = @"Placement Mode - Relative Point";
    private const string DEFAULT_PLACEMENT_MODE_RIGHT = @"Placement Mode - Right";
    private const string DEFAULT_PLACEMENT_MODE_TOP = @"Placement Mode - Top";

    #endregion

    #region Identity

    public PlacementModeStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    [Browsable(false)]
    public bool IsDefault => Absolute.Equals(DEFAULT_PLACEMENT_MODE_ABSOLUTE) &&
                             AbsolutePoint.Equals(DEFAULT_PLACEMENT_MODE_ABSOLUTE_POINT) &&
                             Bottom.Equals(DEFAULT_PLACEMENT_MODE_BOTTOM) &&
                             Center.Equals(DEFAULT_PLACEMENT_MODE_CENTER) &&
                             Left.Equals(DEFAULT_PLACEMENT_MODE_LEFT) &&
                             Mouse.Equals(DEFAULT_PLACEMENT_MODE_MOUSE) &&
                             MousePoint.Equals(DEFAULT_PLACEMENT_MODE_MOUSE_POINT) &&
                             Relative.Equals(DEFAULT_PLACEMENT_MODE_RELATIVE) &&
                             RelativePoint.Equals(DEFAULT_PLACEMENT_MODE_RELATIVE_POINT) &&
                             Right.Equals(DEFAULT_PLACEMENT_MODE_RIGHT) &&
                             Top.Equals(DEFAULT_PLACEMENT_MODE_TOP);

    public void Reset()
    {
        Absolute = DEFAULT_PLACEMENT_MODE_ABSOLUTE;

        AbsolutePoint = DEFAULT_PLACEMENT_MODE_ABSOLUTE_POINT;

        Bottom = DEFAULT_PLACEMENT_MODE_BOTTOM;

        Center = DEFAULT_PLACEMENT_MODE_CENTER;

        Left = DEFAULT_PLACEMENT_MODE_LEFT;

        Mouse = DEFAULT_PLACEMENT_MODE_MOUSE;

        MousePoint = DEFAULT_PLACEMENT_MODE_MOUSE_POINT;

        Relative = DEFAULT_PLACEMENT_MODE_RELATIVE;

        RelativePoint = DEFAULT_PLACEMENT_MODE_RELATIVE_POINT;

        Right = DEFAULT_PLACEMENT_MODE_RIGHT;

        Top = DEFAULT_PLACEMENT_MODE_TOP;
    }

    /// <summary>Gets or sets the absolute placement mode string.</summary>
    [Category(@"Visuals")]
    [Description(@"The absolute placement mode.")]
    [DefaultValue(DEFAULT_PLACEMENT_MODE_ABSOLUTE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Absolute { get; set; }

    /// <summary>Gets or sets the absolute point placement mode string.</summary>
    [Category(@"Visuals")]
    [Description(@"The absolute placement point mode.")]
    [DefaultValue(DEFAULT_PLACEMENT_MODE_ABSOLUTE_POINT)]
    [RefreshProperties(RefreshProperties.All)]
    public string AbsolutePoint { get; set; }

    /// <summary>Gets or sets the bottom placement mode string.</summary>
    [Category(@"Visuals")]
    [Description(@"The bottom placement mode.")]
    [DefaultValue(DEFAULT_PLACEMENT_MODE_BOTTOM)]
    [RefreshProperties(RefreshProperties.All)]
    public string Bottom { get; set; }

    /// <summary>Gets or sets the center placement mode string.</summary>
    [Category(@"Visuals")]
    [Description(@"The center placement mode.")]
    [DefaultValue(DEFAULT_PLACEMENT_MODE_CENTER)]
    [RefreshProperties(RefreshProperties.All)]
    public string Center { get; set; }

    /// <summary>Gets or sets the left placement mode string.</summary>
    [Category(@"Visuals")]
    [Description(@"The left placement mode.")]
    [DefaultValue(DEFAULT_PLACEMENT_MODE_LEFT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Left { get; set; }

    /// <summary>Gets or sets the mouse placement mode string.</summary>
    [Category(@"Visuals")]
    [Description(@"The mouse placement mode.")]
    [DefaultValue(DEFAULT_PLACEMENT_MODE_MOUSE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Mouse { get; set; }

    /// <summary>Gets or sets the mouse point placement mode string.</summary>
    [Category(@"Visuals")]
    [Description(@"The mouse point placement mode.")]
    [DefaultValue(DEFAULT_PLACEMENT_MODE_MOUSE_POINT)]
    [RefreshProperties(RefreshProperties.All)]
    public string MousePoint { get; set; }

    /// <summary>Gets or sets the relative placement mode string.</summary>
    [Category(@"Visuals")]
    [Description(@"The relative placement mode.")]
    [DefaultValue(DEFAULT_PLACEMENT_MODE_RELATIVE)]
    [RefreshProperties(RefreshProperties.All)]
    public string Relative { get; set; }

    /// <summary>Gets or sets the relative point placement mode string.</summary>
    [Category(@"Visuals")]
    [Description(@"The relative point placement mode.")]
    [DefaultValue(DEFAULT_PLACEMENT_MODE_RELATIVE_POINT)]
    [RefreshProperties(RefreshProperties.All)]
    public string RelativePoint { get; set; }

    /// <summary>Gets or sets the right placement mode string.</summary>
    [Category(@"Visuals")]
    [Description(@"The right placement mode.")]
    [DefaultValue(DEFAULT_PLACEMENT_MODE_RIGHT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Right { get; set; }

    /// <summary>Gets or sets the top placement mode string.</summary>
    [Category(@"Visuals")]
    [Description(@"The top placement mode.")]
    [DefaultValue(DEFAULT_PLACEMENT_MODE_TOP)]
    [RefreshProperties(RefreshProperties.All)]
    public string Top { get; set; }

    #endregion
}