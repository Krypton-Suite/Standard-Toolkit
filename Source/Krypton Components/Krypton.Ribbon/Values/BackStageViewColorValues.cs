#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Storage for backstage view color information.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class BackStageViewColorValues : Storage
{
    #region Instance Fields

    private Color? _navigationBackgroundColor;
    private Color? _contentBackgroundColor;
    private Color? _selectedItemHighlightColor;
   
    #endregion

    #region Identity
    
    /// <summary>
    /// Initialize a new instance of the <see cref="BackStageViewColorValues"/> class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public BackStageViewColorValues(NeedPaintHandler needPaint)
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;
    }
    
    #endregion

    #region IsDefault
    
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => !_navigationBackgroundColor.HasValue &&
                                      !_contentBackgroundColor.HasValue &&
                                      !_selectedItemHighlightColor.HasValue;
    
    #endregion

    #region NavigationBackgroundColor
    
    /// <summary>
    /// Gets and sets the custom background color for the navigation panel. If null, uses PanelAlternate palette color.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Custom background color for the navigation panel. If null, uses PanelAlternate palette color.")]
    [DefaultValue(null)]
    public Color? NavigationBackgroundColor
    {
        get => _navigationBackgroundColor;
        set
        {
            if (_navigationBackgroundColor != value)
            {
                _navigationBackgroundColor = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeNavigationBackgroundColor() => _navigationBackgroundColor.HasValue;

    /// <summary>
    /// Resets the NavigationBackgroundColor property to its default value.
    /// </summary>
    public void ResetNavigationBackgroundColor() => NavigationBackgroundColor = null;
    
    #endregion

    #region ContentBackgroundColor
    
    /// <summary>
    /// Gets and sets the custom background color for the content area. If null, uses PanelClient palette color.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Custom background color for the content area. If null, uses PanelClient palette color.")]
    [DefaultValue(null)]
    public Color? ContentBackgroundColor
    {
        get => _contentBackgroundColor;
        set
        {
            if (_contentBackgroundColor != value)
            {
                _contentBackgroundColor = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeContentBackgroundColor() => _contentBackgroundColor.HasValue;

    /// <summary>
    /// Resets the ContentBackgroundColor property to its default value.
    /// </summary>
    public void ResetContentBackgroundColor() => ContentBackgroundColor = null;
    
    #endregion

    #region SelectedItemHighlightColor
    
    /// <summary>
    /// Gets and sets the custom highlight color for selected navigation items. If null, uses theme default.
    /// </summary>
    [Category(@"Backstage")]
    [Description(@"Custom highlight color for selected navigation items. If null, uses theme default.")]
    [DefaultValue(null)]
    public Color? SelectedItemHighlightColor
    {
        get => _selectedItemHighlightColor;
        set
        {
            if (_selectedItemHighlightColor != value)
            {
                _selectedItemHighlightColor = value;
                PerformNeedPaint(true);
            }
        }
    }

    private bool ShouldSerializeSelectedItemHighlightColor() => _selectedItemHighlightColor.HasValue;

    /// <summary>
    /// Resets the SelectedItemHighlightColor property to its default value.
    /// </summary>
    public void ResetSelectedItemHighlightColor() => SelectedItemHighlightColor = null;
    
    #endregion
}

