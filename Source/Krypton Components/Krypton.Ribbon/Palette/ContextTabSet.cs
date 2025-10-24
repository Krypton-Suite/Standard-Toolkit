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
/// Stores information needed to draw the display text for a context set
/// </summary>
internal class ContextTabSet
{
    #region Instance Fields

    private ViewDrawRibbonTab _lastTab;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ContextTabSet class.
    /// </summary>
    /// <param name="tab">Reference to first tab of the set.</param>
    /// <param name="context">Reference to owning context details.</param>
    public ContextTabSet([DisallowNull] ViewDrawRibbonTab? tab,
        [DisallowNull] KryptonRibbonContext? context)
    {
        Debug.Assert(tab is not null);
        Debug.Assert(context is not null);

        FirstTab = tab ?? throw new ArgumentNullException(nameof(tab));
        _lastTab = tab;

        Context = context ?? throw new ArgumentNullException(nameof(context));
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets access to the first tab.
    /// </summary>
    public ViewDrawRibbonTab FirstTab { get; }

    /// <summary>
    /// Gets a value indicating if the tab is the first in set.
    /// </summary>
    /// <param name="tab">Tab to test.</param>
    /// <returns>True if first; otherwise false.</returns>
    public bool IsFirstTab(ViewDrawRibbonTab tab) => tab == FirstTab;

    /// <summary>
    /// Gets a value indicating if the tab is the last in set.
    /// </summary>
    /// <param name="tab">Tab to test.</param>
    /// <returns>True if last; otherwise false.</returns>
    public bool IsLastTab(ViewDrawRibbonTab tab) => tab == _lastTab;

    /// <summary>
    /// Gets a value indicating if the tab is the first or last in set.
    /// </summary>
    /// <param name="tab">Tab to test.</param>
    /// <returns>True if first or last; otherwise false.</returns>
    public bool IsFirstOrLastTab(ViewDrawRibbonTab tab) => (tab == FirstTab) || (tab == _lastTab);

    /// <summary>
    /// Update the last tab in the set with new refernece.
    /// </summary>
    /// <param name="tab">Reference to new last tab.</param>
    public void UpdateLastTab([DisallowNull] ViewDrawRibbonTab tab)
    {
        Debug.Assert(tab is not null);
        _lastTab = tab ?? throw new ArgumentNullException(nameof(tab));
    }

    /// <summary>
    /// Gets the left position needed to show the context tab in screen coordinates.
    /// </summary>
    /// <returns>Screen position.</returns>
    public Point GetLeftScreenPosition()
    {
        var ret = FirstTab.ClientLocation with { X = FirstTab.ClientLocation.X - 1 };

        if (FirstTab.OwningControl != null)
        {
            ret = FirstTab.OwningControl.PointToScreen(ret);
        }

        return ret;
    }

    /// <summary>
    /// Gets the right position needed to show the context tab in screen coordinates.
    /// </summary>
    /// <returns>Screen position.</returns>
    public Point GetRightScreenPosition()
    {
        var ret = _lastTab.ClientLocation with { X = _lastTab.ClientRectangle.Right + 1 };

        if (_lastTab.OwningControl != null)
        {
            ret = _lastTab.OwningControl.PointToScreen(ret);
        }

        return ret;
    }

    /// <summary>
    /// Gets the context component.
    /// </summary>
    public KryptonRibbonContext Context { get; }

    /// <summary>
    /// Gets the name of the context.
    /// </summary>
    public string ContextName => Context.ContextName;

    /// <summary>
    /// Gets the name of the context.
    /// </summary>
    public Color ContextColor => Context.ContextColor;

    /// <summary>
    /// Gets the title of the context.
    /// </summary>
    public string ContextTitle => Context.ContextTitle;

    #endregion
}

/// <summary>
/// Specialise the generic collection with type specific rules for item accessor.
/// </summary>
internal class ContextTabSetCollection : TypedCollection<ContextTabSet>
{
    #region Public
    /// <summary>
    /// Gets the item with the provided unique name.
    /// </summary>
    /// <param name="name">Name of the ribbon context instance.</param>
    /// <returns>Item at specified index.</returns>
    public override ContextTabSet? this[string name]
    {
        get
        {
            // Search for a context with the same name as that requested.
            foreach (ContextTabSet context in this.Where(context => context.ContextName == name))
            {
                return context;
            }

            // Let base class perform standard processing
            return base[name];
        }
    }
    #endregion
}