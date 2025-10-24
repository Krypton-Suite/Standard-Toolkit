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
/// Storage for metrics that can be overriden by the developer.
/// </summary>
public class PaletteMetrics : Storage
{
    #region Instance Fields
    private readonly KryptonNavigator? _navigator;
    private int _pageButtonSpecInset;
    private Padding _pageButtonSpecPadding;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteMetrics class.
    /// </summary>
    /// <param name="navigator">Reference to owning navigator.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteMetrics(KryptonNavigator? navigator,
        NeedPaintHandler needPaint)
    {
        _navigator = navigator;

        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        // Default values
        _pageButtonSpecInset = -1;
        _pageButtonSpecPadding = CommonHelper.InheritPadding;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => ((PageButtonSpecInset == -1) && 
                                       PageButtonSpecPadding.Equals(CommonHelper.InheritPadding));

    #endregion

    #region PageButtonSpecInset
    /// <summary>
    /// Gets and sets the pixel inset of button specs from the edge of the page header.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Pixel inset of button specs from the edge of the page header.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(-1)]
    public int PageButtonSpecInset
    {
        get => _pageButtonSpecInset;

        set
        {
            if (_pageButtonSpecInset != value)
            {
                _pageButtonSpecInset = value;

                _navigator?.OnViewBuilderPropertyChanged(nameof(PageButtonSpecInset));
            }
        }
    }

    /// <summary>
    /// Resets the PageButtonSpecInset property to its default value.
    /// </summary>
    public void ResetPageButtonSpecInset() => PageButtonSpecInset = -1;
    #endregion

    #region PageButtonSpecPadding
    /// <summary>
    /// Gets and sets the pixel padding around the button specs on a page header.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"Pixel padding around the button specs on a page header.")]
    [RefreshProperties(RefreshProperties.All)]
    public Padding PageButtonSpecPadding
    {
        get => _pageButtonSpecPadding;

        set
        {
            if (_pageButtonSpecPadding != value)
            {
                _pageButtonSpecPadding = value;

                _navigator?.OnViewBuilderPropertyChanged(nameof(PageButtonSpecPadding));
            }
        }
    }

    /// <summary>
    /// Resets the PageButtonSpecPadding property to its default value.
    /// </summary>
    public void ResetPageButtonSpecPadding() => PageButtonSpecPadding = CommonHelper.InheritPadding;

    private bool ShouldSerializePageButtonSpecPadding() => !PageButtonSpecPadding.Equals(CommonHelper.InheritPadding);

    #endregion
}