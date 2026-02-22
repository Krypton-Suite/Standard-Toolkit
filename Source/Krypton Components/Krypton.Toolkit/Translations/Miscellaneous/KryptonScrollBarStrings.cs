#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>Exposes the set of <see cref="KryptonScrollBar"/> strings used within Krypton and that are localizable.</summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonScrollBarStrings : GlobalId
{
    #region Static Strings

    private const string DEFAULT_SCROLL_BAR_PAGE_DOWN = @"Page Down";
    private const string DEFAULT_SCROLL_BAR_PAGE_UP = @"Page Up";
    private const string DEFAULT_SCROLL_BAR_PAGE_LEFT = @"Page Left";
    private const string DEFAULT_SCROLL_BAR_PAGE_RIGHT = @"Page Right";
    private const string DEFAULT_SCROLL_BAR_SCROLL_DOWN = @"Scroll Down";
    private const string DEFAULT_SCROLL_BAR_SCROLL_HERE = @"Scroll Here";
    private const string DEFAULT_SCROLL_BAR_SCROLL_UP = @"Scroll Up";
    private const string DEFAULT_SCROLL_BAR_TOP = @"Top";
    private const string DEFAULT_SCROLL_BAR_BOTTOM = @"Bottom";
    private const string DEFAULT_SCROLL_BAR_LEFT = @"Left";
    private const string DEFAULT_SCROLL_BAR_RIGHT = @"Right";
    private const string DEFAULT_SCROLL_BAR_SCROLL_RIGHT = @"Scroll Right";
    private const string DEFAULT_SCROLL_BAR_SCROLL_LEFT = @"Scroll Left";

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="KryptonScrollBarStrings" /> class.</summary>
    public KryptonScrollBarStrings()
    {
        Reset();
    }

    public override string ToString() => !IsDefault ? "Modified" : string.Empty;

    #endregion

    #region Public

    /// <summary>Gets a value indicating whether this instance is default.</summary>
    /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    public bool IsDefault => PageDown.Equals(DEFAULT_SCROLL_BAR_PAGE_DOWN) &&
                             PageUp.Equals(DEFAULT_SCROLL_BAR_PAGE_UP) &&
                             PageRight.Equals(DEFAULT_SCROLL_BAR_PAGE_RIGHT) &&
                             PageLeft.Equals(DEFAULT_SCROLL_BAR_PAGE_LEFT) &&
                             ScrollDown.Equals(DEFAULT_SCROLL_BAR_SCROLL_DOWN) &&
                             ScrollHere.Equals(DEFAULT_SCROLL_BAR_SCROLL_HERE) &&
                             ScrollUp.Equals(DEFAULT_SCROLL_BAR_SCROLL_UP) &&
                             ScrollLeft.Equals(DEFAULT_SCROLL_BAR_SCROLL_LEFT) &&
                             ScrollRight.Equals(DEFAULT_SCROLL_BAR_SCROLL_RIGHT) &&
                             Top.Equals(DEFAULT_SCROLL_BAR_TOP) &&
                             Bottom.Equals(DEFAULT_SCROLL_BAR_BOTTOM) &&
                             Left.Equals(DEFAULT_SCROLL_BAR_LEFT) &&
                             Right.Equals(DEFAULT_SCROLL_BAR_RIGHT);

    /// <summary>Resets this instance.</summary>
    public void Reset()
    {
        PageDown = DEFAULT_SCROLL_BAR_PAGE_DOWN;

        PageUp = DEFAULT_SCROLL_BAR_PAGE_UP;

        PageLeft = DEFAULT_SCROLL_BAR_PAGE_LEFT;

        PageRight = DEFAULT_SCROLL_BAR_PAGE_RIGHT;

        ScrollDown = DEFAULT_SCROLL_BAR_SCROLL_DOWN;

        ScrollHere = DEFAULT_SCROLL_BAR_SCROLL_HERE;

        ScrollUp = DEFAULT_SCROLL_BAR_SCROLL_UP;

        ScrollLeft = DEFAULT_SCROLL_BAR_SCROLL_LEFT;

        ScrollRight = DEFAULT_SCROLL_BAR_SCROLL_RIGHT;

        Top = DEFAULT_SCROLL_BAR_TOP;

        Bottom = DEFAULT_SCROLL_BAR_BOTTOM;

        Right = DEFAULT_SCROLL_BAR_RIGHT;

        Left = DEFAULT_SCROLL_BAR_LEFT;
    }

    /// <summary>Gets or sets the scrollbar page down string.</summary>
    [Category(@"Visuals")]
    [Description(@"The scrollbar page down string.")]
    [DefaultValue(DEFAULT_SCROLL_BAR_PAGE_DOWN)]
    [RefreshProperties(RefreshProperties.All)]
    public string PageDown { get; set; }

    /// <summary>Gets or sets the scrollbar page up string.</summary>
    [Category(@"Visuals")]
    [Description(@"The scrollbar page up string.")]
    [DefaultValue(DEFAULT_SCROLL_BAR_PAGE_UP)]
    [RefreshProperties(RefreshProperties.All)]
    public string PageUp { get; set; }

    /// <summary>Gets or sets the scrollbar page right string.</summary>
    [Category(@"Visuals")]
    [Description(@"The scrollbar page right string.")]
    [DefaultValue(DEFAULT_SCROLL_BAR_PAGE_RIGHT)]
    [RefreshProperties(RefreshProperties.All)]
    public string PageRight { get; set; }

    /// <summary>Gets or sets the scrollbar page left string.</summary>
    [Category(@"Visuals")]
    [Description(@"The scrollbar page left string.")]
    [DefaultValue(DEFAULT_SCROLL_BAR_PAGE_LEFT)]
    [RefreshProperties(RefreshProperties.All)]
    public string PageLeft { get; set; }

    /// <summary>Gets or sets the scrollbar scroll down string.</summary>
    [Category(@"Visuals")]
    [Description(@"The scrollbar scroll down string.")]
    [DefaultValue(DEFAULT_SCROLL_BAR_SCROLL_DOWN)]
    [RefreshProperties(RefreshProperties.All)]
    public string ScrollDown { get; set; }

    /// <summary>Gets or sets the scrollbar scroll here string.</summary>
    [Category(@"Visuals")]
    [Description(@"The scrollbar scroll here string.")]
    [DefaultValue(DEFAULT_SCROLL_BAR_SCROLL_HERE)]
    [RefreshProperties(RefreshProperties.All)]
    public string ScrollHere { get; set; }

    /// <summary>Gets or sets the scrollbar scroll up string.</summary>
    [Category(@"Visuals")]
    [Description(@"The scrollbar scroll up string.")]
    [DefaultValue(DEFAULT_SCROLL_BAR_SCROLL_UP)]
    [RefreshProperties(RefreshProperties.All)]
    public string ScrollUp { get; set; }

    /// <summary>Gets or sets the scrollbar scroll right string.</summary>
    [Category(@"Visuals")]
    [Description(@"The scrollbar scroll right string.")]
    [DefaultValue(DEFAULT_SCROLL_BAR_SCROLL_RIGHT)]
    [RefreshProperties(RefreshProperties.All)]
    public string ScrollRight { get; set; }

    /// <summary>Gets or sets the scrollbar scroll left string.</summary>
    [Category(@"Visuals")]
    [Description(@"The scrollbar scroll left string.")]
    [DefaultValue(DEFAULT_SCROLL_BAR_SCROLL_LEFT)]
    [RefreshProperties(RefreshProperties.All)]
    public string ScrollLeft { get; set; }

    [Category(@"Visuals")]
    [Description(@"The scrollbar top string.")]
    [DefaultValue(DEFAULT_SCROLL_BAR_TOP)]
    [RefreshProperties(RefreshProperties.All)]
    public string Top { get; set; }

    [Category(@"Visuals")]
    [Description(@"The scrollbar bottom string.")]
    [DefaultValue(DEFAULT_SCROLL_BAR_BOTTOM)]
    [RefreshProperties(RefreshProperties.All)]
    public string Bottom { get; set; }

    [Category(@"Visuals")]
    [Description(@"The scrollbar left string.")]
    [DefaultValue(DEFAULT_SCROLL_BAR_LEFT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Left { get; set; }

    [Category(@"Visuals")]
    [Description(@"The scrollbar right string.")]
    [DefaultValue(DEFAULT_SCROLL_BAR_RIGHT)]
    [RefreshProperties(RefreshProperties.All)]
    public string Right { get; set; }

    #endregion
}