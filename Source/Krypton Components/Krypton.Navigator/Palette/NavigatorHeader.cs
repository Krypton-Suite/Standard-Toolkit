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
/// Storage for header related properties.
/// </summary>
public class NavigatorHeader : Storage
{
    #region Instance Fields
    private readonly KryptonNavigator _navigator;
    private bool _headerVisiblePrimary;
    private bool _headerVisibleSecondary;
    private bool _headerVisibleBar;
    private HeaderStyle _headerStylePrimary;
    private HeaderStyle _headerStyleSecondary;
    private HeaderStyle _headerStyleBar;
    private VisualOrientation _headerPositionPrimary;
    private VisualOrientation _headerPositionSecondary;
    private VisualOrientation _headerPositionBar;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the NavigatorHeader class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator instance.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public NavigatorHeader([DisallowNull] KryptonNavigator navigator,
        NeedPaintHandler needPaint)
    {
        Debug.Assert(navigator is not null);

        // Remember back reference
        _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Default values
        _headerStylePrimary = HeaderStyle.Primary;
        _headerStyleSecondary = HeaderStyle.Secondary;
        _headerStyleBar = HeaderStyle.Secondary;
        _headerPositionPrimary = VisualOrientation.Top;
        _headerPositionSecondary = VisualOrientation.Bottom;
        _headerPositionBar = VisualOrientation.Top;
        _headerVisiblePrimary = true;
        _headerVisibleSecondary = true;
        _headerVisibleBar = true;
        HeaderValuesPrimary = new HeaderGroupMappingPrimary(_navigator, needPaint, GetDpiFactor);
        HeaderValuesSecondary = new HeaderGroupMappingSecondary(_navigator, needPaint, GetDpiFactor);
    }

    private float GetDpiFactor() => _navigator.DeviceDpi / 96F;

    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => ((HeaderStylePrimary == HeaderStyle.Primary) &&
                                       (HeaderStyleSecondary == HeaderStyle.Secondary) &&
                                       (HeaderStyleBar == HeaderStyle.Secondary) &&
                                       (HeaderPositionPrimary == VisualOrientation.Top) &&
                                       (HeaderPositionSecondary == VisualOrientation.Bottom) &&
                                       (HeaderPositionBar == VisualOrientation.Top) &&
                                       HeaderVisiblePrimary &&
                                       HeaderVisibleSecondary &&
                                       HeaderVisibleBar &&
                                       HeaderValuesPrimary.IsDefault &&
                                       HeaderValuesSecondary.IsDefault);

    #endregion

    #region HeaderStylePrimary
    /// <summary>
    /// Gets and sets the primary header style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Primary header style.")]
    //[DefaultValue(typeof(HeaderStyle), "Primary")]
    public HeaderStyle HeaderStylePrimary
    {
        get => _headerStylePrimary;

        set
        {
            if (_headerStylePrimary != value)
            {
                _headerStylePrimary = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(HeaderStylePrimary));
            }
        }
    }
    #endregion

    #region HeaderStyleSecondary
    /// <summary>
    /// Gets and sets the secondary header style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Secondary header style.")]
    //[DefaultValue(typeof(HeaderStyle), "Secondary")]
    public HeaderStyle HeaderStyleSecondary
    {
        get => _headerStyleSecondary;

        set
        {
            if (_headerStyleSecondary != value)
            {
                _headerStyleSecondary = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(HeaderStyleSecondary));
            }
        }
    }
    #endregion

    #region HeaderStyleBar
    /// <summary>
    /// Gets and sets the bar header style.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Bar header style.")]
    //[DefaultValue(typeof(HeaderStyle), "Secondary")]
    public HeaderStyle HeaderStyleBar
    {
        get => _headerStyleBar;

        set
        {
            if (_headerStyleBar != value)
            {
                _headerStyleBar = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(HeaderStyleBar));
            }
        }
    }
    #endregion

    #region HeaderPositionPrimary
    /// <summary>
    /// Gets and sets the position of the primary header.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Edge position of the primary header.")]
    //[DefaultValue(typeof(VisualOrientation), "Top")]
    public VisualOrientation HeaderPositionPrimary
    {
        get => _headerPositionPrimary;

        set
        {
            if (_headerPositionPrimary != value)
            {
                _headerPositionPrimary = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(HeaderPositionPrimary));
            }
        }
    }
    #endregion

    #region HeaderPositionSecondary
    /// <summary>
    /// Gets and sets the position of the secondary header.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Edge position of the secondary header.")]
    //[DefaultValue(typeof(VisualOrientation), "Bottom")]
    public VisualOrientation HeaderPositionSecondary
    {
        get => _headerPositionSecondary;

        set
        {
            if (_headerPositionSecondary != value)
            {
                _headerPositionSecondary = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(HeaderPositionSecondary));
            }
        }
    }
    #endregion

    #region HeaderPositionBar
    /// <summary>
    /// Gets and sets the position of the bar header.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Edge position of the bar header.")]
    //[DefaultValue(typeof(VisualOrientation), "Top")]
    public VisualOrientation HeaderPositionBar
    {
        get => _headerPositionBar;

        set
        {
            if (_headerPositionBar != value)
            {
                _headerPositionBar = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(HeaderPositionBar));
            }
        }
    }
    #endregion

    #region HeaderVisiblePrimary
    /// <summary>
    /// Gets and sets the primary header visibility.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Primary header visibility.")]
    [DefaultValue(true)]
    public bool HeaderVisiblePrimary
    {
        get => _headerVisiblePrimary;

        set
        {
            if (_headerVisiblePrimary != value)
            {
                _headerVisiblePrimary = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(HeaderVisiblePrimary));
            }
        }
    }
    #endregion

    #region HeaderVisibleSecondary
    /// <summary>
    /// Gets and sets the secondary header visibility.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Secondary header visibility.")]
    [DefaultValue(true)]
    public bool HeaderVisibleSecondary
    {
        get => _headerVisibleSecondary;

        set
        {
            if (_headerVisibleSecondary != value)
            {
                _headerVisibleSecondary = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(HeaderVisibleSecondary));
            }
        }
    }
    #endregion

    #region HeaderVisibleBar
    /// <summary>
    /// Gets and sets the bar header visibility.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Bar header visibility.")]
    [DefaultValue(true)]
    public bool HeaderVisibleBar
    {
        get => _headerVisibleBar;

        set
        {
            if (_headerVisibleBar != value)
            {
                _headerVisibleBar = value;
                _navigator.OnViewBuilderPropertyChanged(nameof(HeaderVisibleBar));
            }
        }
    }
    #endregion

    #region HeaderValuesPrimary
    /// <summary>
    /// Gets access to the primary header content.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Primary header values")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public HeaderGroupMappingPrimary HeaderValuesPrimary { get; }

    private bool ShouldSerializeHeaderValuesPrimary() => !HeaderValuesPrimary.IsDefault;

    #endregion

    #region HeaderValuesSecondary
    /// <summary>
    /// Gets access to the secondary header content.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Secondary header values")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public HeaderGroupMappingSecondary HeaderValuesSecondary { get; }

    private bool ShouldSerializeHeaderValuesSecondary() => !HeaderValuesSecondary.IsDefault;

    #endregion
}