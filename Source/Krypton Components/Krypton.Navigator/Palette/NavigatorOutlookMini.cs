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
/// Storage for outlook mini mode related properties.
/// </summary>
public class NavigatorOutlookMini : Storage
{
    #region Instance Fields
    private readonly KryptonNavigator _navigator;
    private ButtonStyle _miniButtonStyle;
    private MapKryptonPageText _miniMapText;
    private MapKryptonPageText _miniMapExtraText;
    private MapKryptonPageImage _miniMapImage;
    private MapKryptonPageText _stackMapText;
    private MapKryptonPageText _stackMapExtraText;
    private MapKryptonPageImage _stackMapImage;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the NavigatorOutlookMini class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public NavigatorOutlookMini([DisallowNull] KryptonNavigator navigator,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(navigator is not null);

        // Remember back reference
        _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Default values
        _miniButtonStyle = ButtonStyle.NavigatorMini;
        _miniMapImage = MapKryptonPageImage.None;
        _miniMapText = MapKryptonPageText.TextTitle;
        _miniMapExtraText = MapKryptonPageText.None;
        _stackMapImage = MapKryptonPageImage.MediumSmall;
        _stackMapText = MapKryptonPageText.None;
        _stackMapExtraText = MapKryptonPageText.None;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => ((MiniButtonStyle == ButtonStyle.NavigatorMini) &&
                                       (MiniMapImage == MapKryptonPageImage.None) &&
                                       (MiniMapText == MapKryptonPageText.TextTitle) &&
                                       (MiniMapExtraText == MapKryptonPageText.None) &&
                                       (StackMapImage == MapKryptonPageImage.MediumSmall) &&
                                       (StackMapText == MapKryptonPageText.None) &&
                                       (StackMapExtraText == MapKryptonPageText.None));

    #endregion

    #region MiniButtonStyle
    /// <summary>
    /// Gets and sets the mini button style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Mini button style.")]
    //[DefaultValue(typeof(ButtonStyle), "NavigatorMini")]
    public ButtonStyle MiniButtonStyle
    {
        get => _miniButtonStyle;

        set
        {
            if (_miniButtonStyle != value)
            {
                _miniButtonStyle = value;
                _navigator.OnViewBuilderPropertyChanged("MiniButtonStyleOutlook");
            }
        }
    }
    #endregion

    #region MiniMapImage
    /// <summary>
    /// Gets and sets the mapping used for the mini button item image.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Mapping used for the mini button item image.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(MapKryptonPageImage), "None (Null image)")]
    public virtual MapKryptonPageImage MiniMapImage
    {
        get => _miniMapImage;

        set
        {
            if (_miniMapImage != value)
            {
                _miniMapImage = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the MiniMapImage property to its default value.
    /// </summary>
    public void ResetMiniMapImage() => MiniMapImage = MapKryptonPageImage.None;
    #endregion

    #region MiniMapText
    /// <summary>
    /// Gets and sets the mapping used for the mini button item text.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Mapping used for the mini button item text.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(MapKryptonPageText), "Text - Title")]
    public MapKryptonPageText MiniMapText
    {
        get => _miniMapText;

        set
        {
            if (_miniMapText != value)
            {
                _miniMapText = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the MiniMapText property to its default value.
    /// </summary>
    public void ResetMiniMapText() => MiniMapText = MapKryptonPageText.TextTitle;
    #endregion

    #region MiniMapExtraText
    /// <summary>
    /// Gets and sets the mapping used for the mini button item description.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Mapping used for the mini button item description.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(MapKryptonPageText), "None (Empty string)")]
    public MapKryptonPageText MiniMapExtraText
    {
        get => _miniMapExtraText;

        set
        {
            if (_miniMapExtraText != value)
            {
                _miniMapExtraText = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Resets the MiniMapExtraText property to its default value.
    /// </summary>
    public void ResetMiniMapExtraText() => MiniMapExtraText = MapKryptonPageText.None;
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
    //[DefaultValue(typeof(MapKryptonPageText), "None (Empty string)")]
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
    public void ResetStackMapText() => StackMapText = MapKryptonPageText.None;
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