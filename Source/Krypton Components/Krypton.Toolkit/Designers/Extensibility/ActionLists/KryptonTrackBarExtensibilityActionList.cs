#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Action list for the KryptonTrackBar control using the WinForms Designer Extensibility SDK.
/// </summary>
internal class KryptonTrackBarExtensibilityActionList : KryptonExtensibilityActionListBase
{
    #region Instance Fields
    private readonly KryptonTrackBar? _trackBar;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonTrackBarExtensibilityActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonTrackBarExtensibilityActionList(ControlDesigner owner)
        : base(owner)
    {
        _trackBar = (KryptonTrackBar?)owner.Component;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets and sets the current position of the track bar.
    /// </summary>
    public int Value
    {
        get => _trackBar?.Value ?? 0;
        set => SetPropertyValue(_trackBar!, nameof(Value), Value, value, v => _trackBar!.Value = v);
    }

    /// <summary>
    /// Gets and sets the minimum value for the track bar.
    /// </summary>
    public int Minimum
    {
        get => _trackBar?.Minimum ?? 0;
        set => SetPropertyValue(_trackBar!, nameof(Minimum), Minimum, value, v => _trackBar!.Minimum = v);
    }

    /// <summary>
    /// Gets and sets the maximum value for the track bar.
    /// </summary>
    public int Maximum
    {
        get => _trackBar?.Maximum ?? 100;
        set => SetPropertyValue(_trackBar!, nameof(Maximum), Maximum, value, v => _trackBar!.Maximum = v);
    }

    /// <summary>
    /// Gets and sets the orientation of the track bar.
    /// </summary>
    public Orientation Orientation
    {
        get => _trackBar?.Orientation ?? Orientation.Horizontal;
        set => SetPropertyValue(_trackBar!, nameof(Orientation), Orientation, value, v => _trackBar!.Orientation = v);
    }

    /// <summary>
    /// Gets and sets the palette to be applied.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _trackBar?.PaletteMode ?? PaletteMode.Global;
        set => SetPropertyValue(_trackBar!, nameof(PaletteMode), PaletteMode, value, v => _trackBar!.PaletteMode = v);
    }

    /// <summary>
    /// Gets and sets the size of the track bar.
    /// </summary>
    public PaletteTrackBarSize TrackBarSize
    {
        get => _trackBar?.TrackBarSize ?? PaletteTrackBarSize.Medium;
        set => SetPropertyValue(_trackBar!, nameof(TrackBarSize), TrackBarSize, value, v => _trackBar!.TrackBarSize = v);
    }

    /// <summary>
    /// Gets and sets the number of ticks along the track bar.
    /// </summary>
    public int TickFrequency
    {
        get => _trackBar?.TickFrequency ?? 1;
        set => SetPropertyValue(_trackBar!, nameof(TickFrequency), TickFrequency, value, v => _trackBar!.TickFrequency = v);
    }

    /// <summary>
    /// Gets and sets the tick mark appearance.
    /// </summary>
    public TickStyle TickStyle
    {
        get => _trackBar?.TickStyle ?? TickStyle.BottomRight;
        set => SetPropertyValue(_trackBar!, nameof(TickStyle), TickStyle, value, v => _trackBar!.TickStyle = v);
    }
    #endregion

    #region Public Override
    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a control instance at design time
        if (_trackBar != null)
        {
            // Add the list of TrackBar specific actions
            actions.Add(new DesignerActionHeaderItem(@"Values"));
            actions.Add(new DesignerActionPropertyItem(nameof(Value), @"Value", @"Values", @"Current value"));
            actions.Add(new DesignerActionPropertyItem(nameof(Minimum), @"Minimum", @"Values", @"Minimum value"));
            actions.Add(new DesignerActionPropertyItem(nameof(Maximum), @"Maximum", @"Values", @"Maximum value"));
            actions.Add(new DesignerActionHeaderItem(@"Appearance"));
            actions.Add(new DesignerActionPropertyItem(nameof(Orientation), @"Orientation", @"Appearance", @"Track bar orientation"));
            actions.Add(new DesignerActionPropertyItem(nameof(TrackBarSize), @"Size", @"Appearance", @"Track bar size"));
            actions.Add(new DesignerActionPropertyItem(nameof(TickStyle), @"Tick Style", @"Appearance", @"Tick mark style"));
            actions.Add(new DesignerActionPropertyItem(nameof(TickFrequency), @"Tick Frequency", @"Appearance", @"Tick mark frequency"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), @"Palette", @"Visuals", @"Palette applied to drawing"));
        }

        return actions;
    }
    #endregion
}
