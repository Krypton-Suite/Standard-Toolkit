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
/// Storage for popup page related properties.
/// </summary>
public class NavigatorPopupPages : Storage
{
    #region Static Fields

    private const int DEFAULT_BORDER = 3;
    private const int DEFAULT_GAP = 3;
    private const PopupPageElement DEFAULT_ELEMENT = PopupPageElement.Item;
    private const PopupPagePosition DEFAULT_POSITION = PopupPagePosition.ModeAppropriate;

    #endregion

    #region Instance Fields
    private readonly KryptonNavigator _navigator;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the NavigatorPopupPage class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public NavigatorPopupPages([DisallowNull] KryptonNavigator navigator,
        [DisallowNull] NeedPaintHandler needPaint)
    {
        Debug.Assert(navigator is not null);
        Debug.Assert(needPaint is not null);

        // Remember back reference
        _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Default values
        AllowPopupPages = PopupPageAllow.OnlyOutlookMiniMode;
        Gap = DEFAULT_GAP;
        Border = DEFAULT_GAP;
        Element = DEFAULT_ELEMENT;
        Position = DEFAULT_POSITION;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => ((AllowPopupPages == PopupPageAllow.OnlyOutlookMiniMode) &&
                                       (Border == DEFAULT_BORDER) &&
                                       (Element == DEFAULT_ELEMENT) &&
                                       (Gap == DEFAULT_GAP) &&
                                       (Position == DEFAULT_POSITION));

    #endregion

    #region AllowPopupPages
    /// <summary>
    /// Gets and sets if popup pages are Displayed.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Determines if popup pages are Displayed.")]
    //[DefaultValue(typeof(PopupPageAllow), "Only Outlook Mini Mode")]
    public PopupPageAllow AllowPopupPages { get; set; }

    #endregion

    #region Border
    /// <summary>
    /// Gets and sets the border pixel width around the popup page.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Pixel border width around the popup page.")]
    [DefaultValue(3)]
    public int Border { get; set; }

    #endregion

    #region Element
    /// <summary>
    /// Gets and sets the relative element to use when calculating size and position of the popup page.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"The relative element to use when calculating size and position of the popup page.")]
    //[DefaultValue(typeof(PopupPageElement), "Item")]
    public PopupPageElement Element { get; set; }

    #endregion

    #region Gap
    /// <summary>
    /// Gets and sets the pixel gap between the source element and the popup page.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Pixel gap between the source element and the popup page.")]
    [DefaultValue(3)]
    public int Gap { get; set; }

    #endregion

    #region Position
    /// <summary>
    /// Gets and sets how to calculate the size and position of the popup page relative to element.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"How to calculate the size and position of the popup page.")]
    //[DefaultValue(typeof(PopupPagePosition), "ModeAppropriate")]
    public PopupPagePosition Position { get; set; }

    #endregion
}