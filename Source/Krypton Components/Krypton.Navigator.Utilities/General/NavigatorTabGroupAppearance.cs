#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

/// <summary>
/// Appearance options for browser-style caption tab groups (wash, accent bar, member underline/border).
/// </summary>
/// <remarks>
/// Owned by <see cref="KryptonNavigatorFormIntegrator.TabGroupAppearance"/> and applied to every
/// group in that integrator. Changing a property raises <see cref="PropertyChanged"/> so chrome
/// can rebuild. Designed for the Visual Studio Property Grid
/// (<see cref="ExpandableObjectConverter"/> + <see cref="NotifyParentPropertyAttribute"/>).
/// </remarks>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class NavigatorTabGroupAppearance : INotifyPropertyChanged
{
    #region Constants

    private const int DEFAULT_HEADER_WASH_ALPHA = 80;
    private const int DEFAULT_COLLAPSED_HEADER_WASH_ALPHA = 110;
    private const int DEFAULT_HEADER_ACCENT_HEIGHT = 3;
    private const int DEFAULT_MEMBER_UNDERLINE_HEIGHT = 3;
    private const int DEFAULT_MEMBER_BORDER_WIDTH = 2;

    #endregion

    #region Instance Fields

    private int _headerWashAlpha = DEFAULT_HEADER_WASH_ALPHA;
    private int _collapsedHeaderWashAlpha = DEFAULT_COLLAPSED_HEADER_WASH_ALPHA;
    private bool _showHeaderAccent = true;
    private int _headerAccentHeight = DEFAULT_HEADER_ACCENT_HEIGHT;
    private bool _showMemberUnderline = true;
    private int _memberUnderlineHeight = DEFAULT_MEMBER_UNDERLINE_HEIGHT;
    private bool _showMemberBorder = true;
    private int _memberBorderWidth = DEFAULT_MEMBER_BORDER_WIDTH;

    #endregion

    #region Events

    /// <summary>
    /// Occurs when an appearance property that affects chrome has changed.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    #region IsDefault

    /// <summary>
    /// Gets a value indicating whether all properties are at their default values.
    /// </summary>
    [Browsable(false)]
    public bool IsDefault =>
        HeaderWashAlpha == DEFAULT_HEADER_WASH_ALPHA &&
        CollapsedHeaderWashAlpha == DEFAULT_COLLAPSED_HEADER_WASH_ALPHA &&
        ShowHeaderAccent &&
        HeaderAccentHeight == DEFAULT_HEADER_ACCENT_HEIGHT &&
        ShowMemberUnderline &&
        MemberUnderlineHeight == DEFAULT_MEMBER_UNDERLINE_HEIGHT &&
        ShowMemberBorder &&
        MemberBorderWidth == DEFAULT_MEMBER_BORDER_WIDTH;

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the alpha (0–255) used to wash the expanded group header with the group color.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Alpha (0-255) for the soft color wash on expanded group headers.")]
    [DefaultValue(DEFAULT_HEADER_WASH_ALPHA)]
    [NotifyParentProperty(true)]
    [RefreshProperties(RefreshProperties.All)]
    public int HeaderWashAlpha
    {
        get => _headerWashAlpha;
        set => SetClamped(ref _headerWashAlpha, value, 0, 255, nameof(HeaderWashAlpha));
    }

    /// <summary>
    /// Gets or sets the alpha (0–255) used to wash a collapsed group header (typically stronger).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Alpha (0-255) for the soft color wash on collapsed group headers.")]
    [DefaultValue(DEFAULT_COLLAPSED_HEADER_WASH_ALPHA)]
    [NotifyParentProperty(true)]
    [RefreshProperties(RefreshProperties.All)]
    public int CollapsedHeaderWashAlpha
    {
        get => _collapsedHeaderWashAlpha;
        set => SetClamped(ref _collapsedHeaderWashAlpha, value, 0, 255, nameof(CollapsedHeaderWashAlpha));
    }

    /// <summary>
    /// Gets or sets whether the solid accent bar is drawn under the group header.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"When true, a solid group-color accent bar is drawn under the group header.")]
    [DefaultValue(true)]
    [NotifyParentProperty(true)]
    [RefreshProperties(RefreshProperties.All)]
    public bool ShowHeaderAccent
    {
        get => _showHeaderAccent;
        set
        {
            if (_showHeaderAccent == value)
            {
                return;
            }

            _showHeaderAccent = value;
            OnPropertyChanged(nameof(ShowHeaderAccent));
        }
    }

    /// <summary>
    /// Gets or sets the height in pixels of the header accent bar (0 hides it).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Height in pixels of the solid accent bar under the group header.")]
    [DefaultValue(DEFAULT_HEADER_ACCENT_HEIGHT)]
    [NotifyParentProperty(true)]
    [RefreshProperties(RefreshProperties.All)]
    public int HeaderAccentHeight
    {
        get => _headerAccentHeight;
        set => SetClamped(ref _headerAccentHeight, value, 0, 8, nameof(HeaderAccentHeight));
    }

    /// <summary>
    /// Gets or sets whether member tabs draw a solid group-color underline.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"When true, each grouped member tab draws a solid group-color underline.")]
    [DefaultValue(true)]
    [NotifyParentProperty(true)]
    [RefreshProperties(RefreshProperties.All)]
    public bool ShowMemberUnderline
    {
        get => _showMemberUnderline;
        set
        {
            if (_showMemberUnderline == value)
            {
                return;
            }

            _showMemberUnderline = value;
            OnPropertyChanged(nameof(ShowMemberUnderline));
        }
    }

    /// <summary>
    /// Gets or sets the height in pixels of the member tab underline (0 hides it).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Height in pixels of the solid underline under grouped member tabs.")]
    [DefaultValue(DEFAULT_MEMBER_UNDERLINE_HEIGHT)]
    [NotifyParentProperty(true)]
    [RefreshProperties(RefreshProperties.All)]
    public int MemberUnderlineHeight
    {
        get => _memberUnderlineHeight;
        set => SetClamped(ref _memberUnderlineHeight, value, 0, 8, nameof(MemberUnderlineHeight));
    }

    /// <summary>
    /// Gets or sets whether member tabs receive a tinted border from the group color.
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"When true, grouped member tabs use a tinted border matching the group color.")]
    [DefaultValue(true)]
    [NotifyParentProperty(true)]
    [RefreshProperties(RefreshProperties.All)]
    public bool ShowMemberBorder
    {
        get => _showMemberBorder;
        set
        {
            if (_showMemberBorder == value)
            {
                return;
            }

            _showMemberBorder = value;
            OnPropertyChanged(nameof(ShowMemberBorder));
        }
    }

    /// <summary>
    /// Gets or sets the border width in pixels applied to grouped member tabs (0 clears the override).
    /// </summary>
    [Category(@"Appearance")]
    [Description(@"Border width in pixels for the group-color tint on member tabs.")]
    [DefaultValue(DEFAULT_MEMBER_BORDER_WIDTH)]
    [NotifyParentProperty(true)]
    [RefreshProperties(RefreshProperties.All)]
    public int MemberBorderWidth
    {
        get => _memberBorderWidth;
        set => SetClamped(ref _memberBorderWidth, value, 0, 6, nameof(MemberBorderWidth));
    }

    /// <summary>
    /// Resets all properties to their default values.
    /// </summary>
    public void Reset()
    {
        HeaderWashAlpha = DEFAULT_HEADER_WASH_ALPHA;
        CollapsedHeaderWashAlpha = DEFAULT_COLLAPSED_HEADER_WASH_ALPHA;
        ShowHeaderAccent = true;
        HeaderAccentHeight = DEFAULT_HEADER_ACCENT_HEIGHT;
        ShowMemberUnderline = true;
        MemberUnderlineHeight = DEFAULT_MEMBER_UNDERLINE_HEIGHT;
        ShowMemberBorder = true;
        MemberBorderWidth = DEFAULT_MEMBER_BORDER_WIDTH;
    }

    /// <inheritdoc />
    public override string ToString() => IsDefault ? string.Empty : @"Modified";

    #endregion

    #region Implementation

    private void SetClamped(ref int field, int value, int min, int max, string propertyName)
    {
        var clamped = Math.Max(min, Math.Min(max, value));
        if (field == clamped)
        {
            return;
        }

        field = clamped;
        OnPropertyChanged(propertyName);
    }

    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}
