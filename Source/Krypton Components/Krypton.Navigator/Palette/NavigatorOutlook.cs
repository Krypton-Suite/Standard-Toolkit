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
/// Storage for outlook mode related properties.
/// </summary>
public class NavigatorOutlook : Storage
{
    #region Static Fields

    private const string DEFAULT_MORE_BUTTONS = "Show &More Buttons";
    private const string DEFAULT_FEWER_BUTTONS = "Show Fe&wer Buttons";
    private const string DEFAULT_ADD_REMOVE_BUTTONS = "&Add or Remove Buttons";

    #endregion

    #region Instance Fields
    private readonly KryptonNavigator _navigator;
    private ButtonStyle _checkButtonStyle;
    private ButtonStyle _overflowButtonStyle;
    private PaletteBorderStyle _borderEdgeStyle;
    private ButtonOrientation _itemOrientation;
    private Orientation _orientation;
    private InheritBool _headerSecondaryVisible;
    private bool _showDropDownButton;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the NavigatorOutlook class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public NavigatorOutlook([DisallowNull] KryptonNavigator navigator,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(navigator is not null);

        // Remember back reference
        _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Create compound objects
        Full = new NavigatorOutlookFull(navigator, needPaint);
        Mini = new NavigatorOutlookMini(navigator, needPaint);

        // Default values
        _checkButtonStyle = ButtonStyle.NavigatorStack;
        _overflowButtonStyle = ButtonStyle.NavigatorOverflow;
        _borderEdgeStyle = PaletteBorderStyle.ControlClient;
        _orientation = Orientation.Vertical;
        _itemOrientation = ButtonOrientation.Auto;
        _headerSecondaryVisible = InheritBool.False;
        TextMoreButtons = DEFAULT_MORE_BUTTONS;
        TextFewerButtons = DEFAULT_FEWER_BUTTONS;
        TextAddRemoveButtons = DEFAULT_ADD_REMOVE_BUTTONS;
        _showDropDownButton = true;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (Full.IsDefault &&
                                       Mini.IsDefault &&
                                       (CheckButtonStyle == ButtonStyle.NavigatorStack) &&
                                       (OverflowButtonStyle == ButtonStyle.NavigatorOverflow) &&
                                       (BorderEdgeStyle == PaletteBorderStyle.ControlClient) &&
                                       (Orientation == Orientation.Vertical) &&
                                       (ItemOrientation == ButtonOrientation.Auto) &&
                                       (HeaderSecondaryVisible == InheritBool.False) &&
                                       (TextMoreButtons.Equals(DEFAULT_MORE_BUTTONS)) &&
                                       (TextFewerButtons.Equals(DEFAULT_FEWER_BUTTONS)) &&
                                       (TextAddRemoveButtons.Equals(DEFAULT_ADD_REMOVE_BUTTONS)) &&
                                       ShowDropDownButton);

    #endregion

    #region Full
    /// <summary>
    /// Gets and sets settings appropriate for the Outlook - Full mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Settings for the Outlook - Full mode.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public NavigatorOutlookFull Full { get; }

    private bool ShouldSerializeFull() => !Full.IsDefault;

    #endregion

    #region Mini
    /// <summary>
    /// Gets and sets settings appropriate for the Outlook - Mini mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Settings for the Outlook - Mini mode.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public NavigatorOutlookMini Mini { get; }

    private bool ShouldSerializeMini() => !Mini.IsDefault;

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
                _navigator.OnViewBuilderPropertyChanged("CheckButtonStyleOutlook");
            }
        }
    }
    #endregion

    #region OverflowButtonStyle
    /// <summary>
    /// Gets and sets the outlook overflow button style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Outlook overflow button style.")]
    //[DefaultValue(typeof(ButtonStyle), "NavigatorOverflow")]
    public ButtonStyle OverflowButtonStyle
    {
        get => _overflowButtonStyle;

        set
        {
            if (_overflowButtonStyle != value)
            {
                _overflowButtonStyle = value;
                _navigator.OnViewBuilderPropertyChanged("OverflowButtonStyleOutlook");
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
                _navigator.OnViewBuilderPropertyChanged("BorderEdgeStyleOutlook");
            }
        }
    }
    #endregion

    #region Orientation
    /// <summary>
    /// Gets and sets the orientation for positioning stack and overflow items.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Orientation for positioning stack and overflow items.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(Orientation), "Vertical")]
    public Orientation Orientation
    {
        get => _orientation;

        set
        {
            if (_orientation != value)
            {
                _orientation = value;
                _navigator.OnViewBuilderPropertyChanged("OrientationOutlook");
            }
        }
    }

    /// <summary>
    /// Resets the Orientation property to its default value.
    /// </summary>
    public void ResetOrientation() => Orientation = Orientation.Vertical;
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
                _navigator.OnViewBuilderPropertyChanged("ItemOrientationOutlook");
            }
        }
    }

    /// <summary>
    /// Resets the ItemOrientation property to its default value.
    /// </summary>
    public void ResetItemOrientation() => ItemOrientation = ButtonOrientation.Auto;
    #endregion

    #region HeaderSecondaryVisible
    /// <summary>
    /// Gets and sets the secondary header visiblity when in Outlook mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Secondary header visiblity when in Outlook mode.")]
    [RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(typeof(InheritBool), "False")]
    public InheritBool HeaderSecondaryVisible
    {
        get => _headerSecondaryVisible;

        set
        {
            if (_headerSecondaryVisible != value)
            {
                _headerSecondaryVisible = value;
                _navigator.OnViewBuilderPropertyChanged("HeaderSecondaryVisibleOutlook");
            }
        }
    }

    /// <summary>
    /// Resets the HeaderSecondaryVisible property to its default value.
    /// </summary>
    public void ResetHeaderSecondaryVisible() => HeaderSecondaryVisible = InheritBool.False;
    #endregion

    #region TextMoreButtons
    /// <summary>
    /// Gets and sets the text to use when asking if more buttons should be shown in Outlook mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Text to use when asking if more buttons should be shown in Outlook mode.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Show &More Buttons")]
    [Localizable(true)]
    public string TextMoreButtons { get; set; }

    /// <summary>
    /// Resets the TextMoreButtons property to its default value.
    /// </summary>
    public void ResetTextMoreButtons() => TextMoreButtons = DEFAULT_MORE_BUTTONS;
    #endregion

    #region TextFewerButtons
    /// <summary>
    /// Gets and sets the text to use when asking if fewer buttons should be shown in Outlook mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Text to use when asking if fewer buttons should be shown in Outlook mode.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("Show Fe&wer Buttons")]
    [Localizable(true)]
    public string TextFewerButtons { get; set; }

    /// <summary>
    /// Resets the TextFewerButtons property to its default value.
    /// </summary>
    public void ResetTextFewerButtons() => TextFewerButtons = DEFAULT_FEWER_BUTTONS;
    #endregion

    #region TextAddRemoveButtons
    /// <summary>
    /// Gets and sets the text to use when asking if buttons should be shown/hidden in Outlook mode.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Text to use when asking if buttons should be shown/hidden in Outlook mode.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue("&Add or Remove Buttons")]
    [Localizable(true)]
    public string TextAddRemoveButtons { get; set; }

    /// <summary>
    /// Resets the TextAddRemoveButtons property to its default value.
    /// </summary>
    public void ResetTextAddRemoveButtons() => TextAddRemoveButtons = DEFAULT_ADD_REMOVE_BUTTONS;
    #endregion

    #region ShowDropDownButton
    /// <summary>
    /// Gets and sets the visibility of the drop-down button on the Outlook overflow bar.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Visibility of the drop-down button on the Outlook overflow bar.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(true)]
    public bool ShowDropDownButton
    {
        get => _showDropDownButton;

        set
        {
            if (_showDropDownButton != value)
            {
                _showDropDownButton = value;
                _navigator.OnViewBuilderPropertyChanged("ShowDropDownButtonOutlook");
            }
        }
    }

    /// <summary>
    /// Resets the ShowDropDownButton property to its default value.
    /// </summary>
    public void ResetShowDropDownButton() => ShowDropDownButton = true;
    #endregion
}