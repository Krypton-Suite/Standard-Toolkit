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

// ReSharper disable VirtualMemberCallInConstructor
namespace Krypton.Navigator;

/// <summary>
/// Storage for outlook full mode related properties.
/// </summary>
public class NavigatorOutlookFull : Storage
{
    #region Instance Fields
    private readonly KryptonNavigator _navigator;
    private MapKryptonPageText _overflowMapText;
    private MapKryptonPageText _overflowMapExtraText;
    private MapKryptonPageImage _overflowMapImage;
    private MapKryptonPageText _stackMapText;
    private MapKryptonPageText _stackMapExtraText;
    private MapKryptonPageImage _stackMapImage;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the NavigatorOutlookFull class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public NavigatorOutlookFull([DisallowNull] KryptonNavigator navigator,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(navigator is not null);

        // Remember back reference
        _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Default values
        _overflowMapImage = MapKryptonPageImage.Small;
        _overflowMapText = MapKryptonPageText.None;
        _overflowMapExtraText = MapKryptonPageText.None;
        _stackMapImage = MapKryptonPageImage.MediumSmall;
        _stackMapText = MapKryptonPageText.TextTitle;
        _stackMapExtraText = MapKryptonPageText.None;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => ((OverflowMapImage == MapKryptonPageImage.Small) &&
                                       (OverflowMapText == MapKryptonPageText.None) &&
                                       (OverflowMapExtraText == MapKryptonPageText.None) &&
                                       (StackMapImage == MapKryptonPageImage.MediumSmall) &&
                                       (StackMapText == MapKryptonPageText.TextTitle) &&
                                       (StackMapExtraText == MapKryptonPageText.None));

    #endregion

    #region OverflowMapImage
    /// <summary>
    /// Gets and sets the mapping used for the overflow item image.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Mapping used for the overflow item image.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(MapKryptonPageImage), "Small")]
    public virtual MapKryptonPageImage OverflowMapImage
    {
        get => _overflowMapImage;

        set
        {
            if (_overflowMapImage != value)
            {
                _overflowMapImage = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the OverflowMapImage property to its default value.
    /// </summary>
    public void ResetOverflowMapImage() => OverflowMapImage = MapKryptonPageImage.Small;
    #endregion

    #region OverflowMapText
    /// <summary>
    /// Gets and sets the mapping used for the overflow item text.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Mapping used for the overflow item text.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(MapKryptonPageText), "None (Empty string)")]
    public MapKryptonPageText OverflowMapText
    {
        get => _overflowMapText;

        set
        {
            if (_overflowMapText != value)
            {
                _overflowMapText = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the OverflowMapText property to its default value.
    /// </summary>
    public void ResetOverflowMapText() => OverflowMapText = MapKryptonPageText.None;
    #endregion

    #region OverflowMapExtraText
    /// <summary>
    /// Gets and sets the mapping used for the overflow item description.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Mapping used for the overflow item description.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(MapKryptonPageText), "None (Empty string)")]
    public MapKryptonPageText OverflowMapExtraText
    {
        get => _overflowMapExtraText;

        set
        {
            if (_overflowMapExtraText != value)
            {
                _overflowMapExtraText = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the OverflowMapExtraText property to its default value.
    /// </summary>
    public void ResetOverflowMapExtraText() => OverflowMapExtraText = MapKryptonPageText.None;
    #endregion

    #region StackMapImage
    /// <summary>
    /// Gets and sets the mapping used for the stack item image.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Mapping used for the stack item image.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(MapKryptonPageImage), "MediumSmall")]
    public virtual MapKryptonPageImage StackMapImage
    {
        get => _stackMapImage;

        set
        {
            if (_stackMapImage != value)
            {
                _stackMapImage = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the StackMapImage property to its default value.
    /// </summary>
    public void ResetStackMapImage() => StackMapImage = MapKryptonPageImage.MediumSmall;
    #endregion

    #region StackMapText
    /// <summary>
    /// Gets and sets the mapping used for the stack item text.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Mapping used for the stack item text.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(MapKryptonPageText), "Text - Title")]
    public MapKryptonPageText StackMapText
    {
        get => _stackMapText;

        set
        {
            if (_stackMapText != value)
            {
                _stackMapText = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the StackMapText property to its default value.
    /// </summary>
    public void ResetStackMapText() => StackMapText = MapKryptonPageText.TextTitle;
    #endregion

    #region StackMapExtraText
    /// <summary>
    /// Gets and sets the mapping used for the stack item description.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Mapping used for the stack item description.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(MapKryptonPageText), "None (Empty string)")]
    public MapKryptonPageText StackMapExtraText
    {
        get => _stackMapExtraText;

        set
        {
            if (_stackMapExtraText != value)
            {
                _stackMapExtraText = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the StackMapExtraText property to its default value.
    /// </summary>
    public void ResetStackMapExtraText() => StackMapExtraText = MapKryptonPageText.None;
    #endregion
}