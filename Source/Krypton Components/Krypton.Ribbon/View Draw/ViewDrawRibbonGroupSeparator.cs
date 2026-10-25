#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Draws a long vertical group separator.
/// </summary>
internal class ViewDrawRibbonGroupSeparator : ViewLeaf,
    IRibbonViewGroupContainerView
{
    #region Instance Fields
    private readonly Size _preferredSize2007; // = new(4, 4);
    private readonly Size _preferredSize2010; // = new(7, 4);
    private readonly KryptonRibbon _ribbon;
    private KryptonRibbonGroupSeparator? _ribbonSeparator;
    private readonly NeedPaintHandler _needPaint;
    private Size _preferredSize;
    private PaletteRibbonShape _lastShape;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawRibbonGroupSeparator class.
    /// </summary>
    /// <param name="ribbon">Reference to owning ribbon control.</param>
    /// <param name="ribbonSeparator">Reference to group separator definition.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public ViewDrawRibbonGroupSeparator([DisallowNull] KryptonRibbon? ribbon,
        [DisallowNull] KryptonRibbonGroupSeparator? ribbonSeparator,
        [DisallowNull] NeedPaintHandler? needPaint)
    {
        Debug.Assert(ribbon is not null);
        Debug.Assert(ribbonSeparator is not null);
        Debug.Assert(needPaint is not null);

        _ribbon = ribbon ?? throw new ArgumentNullException(nameof(ribbon));
        _ribbonSeparator = ribbonSeparator ?? throw new ArgumentNullException(nameof(ribbonSeparator));
        _needPaint = needPaint ?? throw new ArgumentNullException(nameof(needPaint));

        // Associate this view with the source component (required for design time selection)
        Component = _ribbonSeparator;

        if (_ribbon.InDesignMode)
        {
            // At design time we need to know when the user right clicks the label
            var controller = new ContextClickController();
            controller.ContextClick += OnContextClick;
            MouseController = controller;
        }

        // Define back reference to view for the separator definition
        _ribbonSeparator.SeparatorView = this;

        // Hook into changes in the ribbon separator definition
        _ribbonSeparator.PropertyChanged += OnSeparatorPropertyChanged;

        _preferredSize2007 = new Size((int)(4 * FactorDpiX), (int)(4 * FactorDpiY));
        _preferredSize2010 = new Size((int)(7 * FactorDpiX), (int)(4 * FactorDpiY));
            
        // Default the preferred size
        _lastShape = PaletteRibbonShape.Office2007;
        _preferredSize = _preferredSize2007;
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewDrawRibbonGroupSeparator:{Id}";

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Must unhook to prevent memory leaks
            _ribbonSeparator!.PropertyChanged -= OnSeparatorPropertyChanged;

            // Remove association with definition
            _ribbonSeparator.SeparatorView = null;
            _ribbonSeparator = null;
        }

        base.Dispose(disposing);
    }
    #endregion

    #region GetFirstFocusItem
    /// <summary>
    /// Gets the first focus item from the container.
    /// </summary>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetFirstFocusItem() =>
        // We never have any child items that can take focus
        null!;

    #endregion

    #region GetLastFocusItem
    /// <summary>
    /// Gets the last focus item from the item.
    /// </summary>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetLastFocusItem() =>
        // We never have any child items that can take focus
        null!;

    #endregion

    #region GetNextFocusItem
    /// <summary>
    /// Gets the next focus item based on the current item as provided.
    /// </summary>
    /// <param name="current">The view that is currently focused.</param>
    /// <param name="matched">Has the current focus item been matched yet.</param>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetNextFocusItem(ViewBase current, ref bool matched) =>
        // We never have any child items that can take focus
        null!;

    #endregion

    #region GetPreviousFocusItem
    /// <summary>
    /// Gets the previous focus item based on the current item as provided.
    /// </summary>
    /// <param name="current">The view that is currently focused.</param>
    /// <param name="matched">Has the current focus item been matched yet.</param>
    /// <returns>ViewBase of item; otherwise false.</returns>
    public ViewBase GetPreviousFocusItem(ViewBase current, ref bool matched) =>
        // We never have any child items that can take focus
        null!;

    #endregion

    #region GetGroupKeyTips
    /// <summary>
    /// Gets the array of group level key tips.
    /// </summary>
    /// <param name="keyTipList">List to add new entries into.</param>
    public void GetGroupKeyTips(KeyTipInfoList keyTipList)
    {
        // Separator never has key tips
    }
    #endregion

    #region Layout
    /// <summary>
    /// Gets an array of the allowed possible sizes of the container.
    /// </summary>
    /// <param name="context">Context used to calculate the sizes.</param>
    /// <returns>Array of size values.</returns>
    public ItemSizeWidth[] GetPossibleSizes(ViewLayoutContext context)
    {
        if (_lastShape != _ribbon.RibbonShape)
        {
            switch (_ribbon.RibbonShape)
            {
                default:
                case PaletteRibbonShape.Office2007:
                    _lastShape = PaletteRibbonShape.Office2007;
                    _preferredSize = _preferredSize2007;
                    break;
                case PaletteRibbonShape.Office2010:
                    _lastShape = PaletteRibbonShape.Office2010;
                    _preferredSize = _preferredSize2010;
                    break;
            }
        }

        // Return the one possible size allowed
        return [new ItemSizeWidth(GroupItemSize.Large, _preferredSize.Width)];
    }

    /// <summary>
    /// Update the group with the provided sizing solution.
    /// </summary>
    /// <param name="size">Value for the container.</param>
    public void SetSolutionSize(ItemSizeWidth size) =>
        // Solution should always be the large, the only size we can be
        Debug.Assert(size.GroupItemSize == GroupItemSize.Large);

    /// <summary>
    /// Reset the container back to its requested size.
    /// </summary>
    public void ResetSolutionSize()
    {
    }

    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize(ViewLayoutContext context) => _preferredSize;

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // We take on all the available display area
        ClientRectangle = context!.DisplayRectangle;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform rendering before child elements are rendered.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void RenderBefore(RenderContext context)
    {
        if (context.Renderer is null)
        {
            throw new ArgumentNullException(nameof(context.Renderer));
        }

        context.Renderer.RenderGlyph.DrawRibbonGroupSeparator(_ribbon.RibbonShape,
            context,
            ClientRectangle,
            _ribbon.StateCommon.RibbonGeneral,
            State);
    }
    #endregion

    #region Protected
    /// <summary>
    /// Raises the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    protected virtual void OnNeedPaint(bool needLayout)
    {
        if (_needPaint != null)
        {
            _needPaint(this, new NeedLayoutEventArgs(needLayout));

            if (needLayout)
            {
                _ribbon.PerformLayout();
            }
        }
    }
    #endregion

    #region Implementation
    private void OnContextClick(object? sender, MouseEventArgs e) => _ribbonSeparator!.OnDesignTimeContextMenu(e);

    private void OnSeparatorPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Visible):
                // If we are on the currently selected tab then...
                if ((_ribbonSeparator!.RibbonTab != null) &&
                    (_ribbon.SelectedTab == _ribbonSeparator.RibbonTab))
                {
                    // ...layout so the visible change is made
                    OnNeedPaint(true);
                }
                break;
        }
    }
    #endregion
}