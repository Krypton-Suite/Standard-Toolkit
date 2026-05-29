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
/// Storage for stack related properties.
/// </summary>
public class NavigatorStack : Storage
{
    #region Instance Fields
    private readonly KryptonNavigator _navigator;
    private ButtonStyle _checkButtonStyle;
    private PaletteBorderStyle _borderEdgeStyle;
    private bool _stackAnimation;
    private Orientation _stackOrientation;
    private RelativePositionAlign _stackAlignment;
    private ButtonOrientation _itemOrientation;
    private MapKryptonPageText _stackMapText;
    private MapKryptonPageText _stackMapExtraText;
    private MapKryptonPageImage _stackMapImage;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the NavigatorStack class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public NavigatorStack([DisallowNull] KryptonNavigator navigator,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(navigator is not null);

        // Remember back reference
        _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Default values
        _checkButtonStyle = ButtonStyle.NavigatorStack;
        _borderEdgeStyle = PaletteBorderStyle.ControlClient;
        _stackAnimation = true;
        _stackOrientation = Orientation.Vertical;
        _stackAlignment = RelativePositionAlign.Center;
        _itemOrientation = ButtonOrientation.Auto;
        _stackMapImage = MapKryptonPageImage.Small;
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
    public override bool IsDefault => ((CheckButtonStyle == ButtonStyle.NavigatorStack) &&
                                       (BorderEdgeStyle == PaletteBorderStyle.ControlClient) &&
                                       StackAnimation &&
                                       (StackOrientation == Orientation.Vertical) &&
                                       (StackAlignment == RelativePositionAlign.Center) &&
                                       (ItemOrientation == ButtonOrientation.Auto) &&
                                       (StackMapImage == MapKryptonPageImage.Small) &&
                                       (StackMapText == MapKryptonPageText.TextTitle) &&
                                       (StackMapExtraText == MapKryptonPageText.None));

    #endregion

    #region CheckButtonStyle
    /// <summary>
    /// Gets and sets the check button style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Check button style.")]
    //[DefaultValue(typeof(ButtonStyle), "NavigatorStack")]
    public ButtonStyle CheckButtonStyle
    {
        get => _checkButtonStyle;

        set
        {
            if (_checkButtonStyle != value)
            {
                _checkButtonStyle = value;
                _navigator.OnViewBuilderPropertyChanged("CheckButtonStyleStack");
            }
        }
    }
    #endregion

    #region BorderEdgeStyle
    /// <summary>
    /// Gets and sets the border edge style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Check button style.")]
    //[DefaultValue(typeof(PaletteBorderStyle), "ControlClient")]
    public PaletteBorderStyle BorderEdgeStyle
    {
        get => _borderEdgeStyle;

        set
        {
            if (_borderEdgeStyle != value)
            {
                _borderEdgeStyle = value;
                _navigator.OnViewBuilderPropertyChanged("BorderEdgeStyleStack");
            }
        }
    }
    #endregion

    #region StackAnimation
    /// <summary>
    /// Gets and sets if animation should be used on the stack.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Should animation effects be used on the stack.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(true)]
    public bool StackAnimation
    {
        get => _stackAnimation;

        set
        {
            if (_stackAnimation != value)
            {
                _stackAnimation = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(StackAnimation));
            }
        }
    }

    /// <summary>
    /// Resets the StackAnimation property to its default value.
    /// </summary>
    public void ResetStackAnimation() => StackAnimation = true;
    #endregion

    #region StackOrientation
    /// <summary>
    /// Gets and sets the orientation for positioning stack items.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Orientation for positioning stack items.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(Orientation), "Vertical")]
    public Orientation StackOrientation
    {
        get => _stackOrientation;

        set
        {
            if (_stackOrientation != value)
            {
                _stackOrientation = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(StackOrientation));
            }
        }
    }

    /// <summary>
    /// Resets the StackOrientation property to its default value.
    /// </summary>
    public void ResetStackOrientation() => StackOrientation = Orientation.Vertical;
    #endregion

    #region StackAlignment
    /// <summary>
    /// Gets and sets the alignment of the stack relative to the Displayed page.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Alignment of the stack relative to the Displayed page.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(RelativePositionAlign), "Center")]
    public RelativePositionAlign StackAlignment
    {
        get => _stackAlignment;

        set
        {
            if (_stackAlignment != value)
            {
                _stackAlignment = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(StackAlignment));
            }
        }
    }

    /// <summary>
    /// Resets the StackAlignment property to its default value.
    /// </summary>
    public void ResetStackAlignment() => StackAlignment = RelativePositionAlign.Center;
    #endregion

    #region ItemOrientation
    /// <summary>
    /// Gets and sets the orientation for positioning items in the stack.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Orientation for positioning items in the stack.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(ButtonOrientation), "Auto")]
    public ButtonOrientation ItemOrientation
    {
        get => _itemOrientation;

        set
        {
            if (_itemOrientation != value)
            {
                _itemOrientation = value;
                _navigator.OnViewBuilderPropertyChanged("ItemOrientationStack");
            }
        }
    }

    /// <summary>
    /// Resets the ItemOrientation property to its default value.
    /// </summary>
    public void ResetItemOrientation() => ItemOrientation = ButtonOrientation.Auto;
    #endregion

    #region StackMapImage
    /// <summary>
    /// Gets and sets the mapping used for the stack item image.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Mapping used for the stack item image.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(MapKryptonPageImage), "Small")]
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
    public void ResetStackMapImage() => StackMapImage = MapKryptonPageImage.Small;
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

    #region MapExtraText
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