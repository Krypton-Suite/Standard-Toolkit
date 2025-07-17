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

namespace Krypton.Navigator;

/// <summary>
/// Storage for tooltip related properties.
/// </summary>
public class NavigatorToolTips : Storage
{
    #region Instance Fields
    private readonly KryptonNavigator _navigator;
    private MapKryptonPageImage _mapImage;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the NavigatorPopupPage class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public NavigatorToolTips([DisallowNull] KryptonNavigator navigator,
        [DisallowNull] NeedPaintHandler needPaint)
    {
        Debug.Assert(navigator is not null);
        Debug.Assert(needPaint is not null);

        // Remember back reference
        _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Default values
        AllowPageToolTips = false;
        AllowButtonSpecToolTips = false;
        AllowButtonSpecToolTipPriority = false;
        _mapImage = MapKryptonPageImage.ToolTip;
        MapText = MapKryptonPageText.ToolTipTitle;
        MapExtraText = MapKryptonPageText.ToolTipBody;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (!AllowPageToolTips &&
                                       !AllowButtonSpecToolTips &&
                                       !AllowButtonSpecToolTipPriority &&
                                       (MapImage == MapKryptonPageImage.ToolTip) &&
                                       (MapText == MapKryptonPageText.ToolTipTitle) &&
                                       (MapExtraText == MapKryptonPageText.ToolTipBody));

    #endregion

    #region AllowPageToolTips
    /// <summary>
    /// Gets and sets a value indicating if tooltips should be Displayed for page headers.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should tooltips be Displayed for page headers.")]
    [DefaultValue(false)]
    public bool AllowPageToolTips { get; set; }

    #endregion

    #region AllowButtonSpecToolTips
    /// <summary>
    /// Gets and sets a value indicating if tooltips should be Displayed for button specs.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should tooltips be Displayed for button specs.")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTips { get; set; }

    /// <summary>
    /// Gets and sets a value indicating if button spec tooltips should remove the parent tooltip.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should button spec tooltips should remove the parent tooltip")]
    [DefaultValue(false)]
    public bool AllowButtonSpecToolTipPriority { get; set; }
    #endregion

    #region MapImage
    /// <summary>
    /// Gets and sets the mapping used for the tooltip image.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Mapping used for the tooltip image.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(MapKryptonPageImage), "ToolTip")]
    public virtual MapKryptonPageImage MapImage
    {
        get => _mapImage;
        set => _mapImage = value;
    }

    /// <summary>
    /// Resets the MapImage property to its default value.
    /// </summary>
    public void ResetMapImage() => MapImage = MapKryptonPageImage.ToolTip;
    #endregion

    #region MapText
    /// <summary>
    /// Gets and sets the mapping used for the tooltip text.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Mapping used for the tooltip text.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(MapKryptonPageText), "ToolTipTitle")]
    public MapKryptonPageText MapText { get; set; }

    /// <summary>
    /// Resets the MapText property to its default value.
    /// </summary>
    public void ResetMapText() => MapText = MapKryptonPageText.ToolTipTitle;
    #endregion

    #region MapExtraText
    /// <summary>
    /// Gets and sets the mapping used for the tooltip description.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Mapping used for the tooltip description.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(MapKryptonPageText), "ToolTipBody")]
    public MapKryptonPageText MapExtraText { get; set; }

    /// <summary>
    /// Resets the MapExtraText property to its default value.
    /// </summary>
    public void ResetMapExtraText() => MapExtraText = MapKryptonPageText.ToolTipBody;
    #endregion
}