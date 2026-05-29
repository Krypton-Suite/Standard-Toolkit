#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
///Be More WPF like...
/// https://docs.microsoft.com/en-us/dotnet/framework/wpf/controls/popup-placement-behavior?view=netframework-4.7.2
/// https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.tooltip.placement?view=netframework-4.7.2
/// 
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public class PopupPositionValues : Storage
{
    #region Identity
    /// <summary>
    /// 
    /// </summary>
    public PopupPositionValues()
    {
        Reset();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Reset()
    {
        ResetPlacementMode();
        ResetPlacementTarget();
        ResetPlacementRectangle();
    }

    #endregion Identity

    private PlacementMode _placementMode = PlacementMode.Bottom;
    private ViewBase? _placementTarget;

    /// <summary>
    /// 
    /// </summary>
    [Description(@"Describes the placement of where a Popup control appears on the screen.")]
    [DefaultValue(PlacementMode.Bottom)]
    public PlacementMode PlacementMode
    {
        get => _placementMode;
        set => _placementMode = value;
    }

    private bool ShouldSerializePlacementMode() => _placementMode != PlacementMode.Bottom;

    /// <summary>
    /// Resets the PlacementMode property to its default value.
    /// </summary>
    private void ResetPlacementMode() => _placementMode = PlacementMode.Bottom;

    /// <summary>
    /// 
    /// </summary>
    [Description(@"The element relative to which the Popup is positioned when it opens.")]
    [DefaultValue(null)]
    [AllowNull]
    public ViewBase? PlacementTarget
    {
        get => _placementTarget;
        set => _placementTarget = value;
    }

    private bool ShouldSerializePlacementTarget() => PlacementTarget != null;

    /// <summary>
    /// Resets the PlacementTarget property to its default value.
    /// </summary>
    private void ResetPlacementTarget() => PlacementTarget = null;

    /// <summary>
    /// 
    /// </summary>
    [Description(@"The rectangle relative to which the Popup control is positioned when it opens.")]
    public Rectangle PlacementRectangle { get; set; }

    private bool ShouldSerializePlacementRectangle() => !PlacementRectangle.IsEmpty;

    /// <summary>
    /// Resets the ToolTipStyle property to its default value.
    /// </summary>
    private void ResetPlacementRectangle() => PlacementRectangle = new Rectangle();

    #region Default Values
    /// <summary>
    /// 
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !ShouldSerializePlacementMode()
                                      && !ShouldSerializePlacementTarget()
                                      && !ShouldSerializePlacementRectangle()
    ;
    #endregion Default Values
}