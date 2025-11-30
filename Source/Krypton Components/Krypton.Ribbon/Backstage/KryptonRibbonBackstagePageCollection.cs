#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro & KamaniAR et al. 2025 - 2025. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Specialise the generic collection with type specific rules for backstage page accessor.
/// </summary>
public class KryptonRibbonBackstagePageCollection : TypedCollection<KryptonRibbonBackstagePage>
{
    #region Instance Fields
    private readonly KryptonRibbon _owner;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonBackstagePageCollection class.
    /// </summary>
    /// <param name="owner">Reference to owning ribbon control.</param>
    public KryptonRibbonBackstagePageCollection(KryptonRibbon owner)
    {
        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the item with the provided unique name.
    /// </summary>
    /// <param name="name">Name of the ribbon backstage page instance.</param>
    /// <returns>Item at specified index.</returns>
    public override KryptonRibbonBackstagePage? this[string name]
    {
        get
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            foreach (KryptonRibbonBackstagePage page in this.Where(page => page.Text == name))
            {
                return page;
            }

            return base[name];
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Add a page with a UserControl to the collection.
    /// </summary>
    /// <param name="text">Text for the navigation button.</param>
    /// <param name="userControl">UserControl to display as page content.</param>
    /// <returns>Reference to the newly created page.</returns>
    public KryptonRibbonBackstagePage Add(string text, UserControl userControl)
    {
        var page = new KryptonRibbonBackstagePage(text, userControl);
        Add(page);
        return page;
    }

    /// <summary>
    /// Add a page with any Control to the collection.
    /// </summary>
    /// <param name="text">Text for the navigation button.</param>
    /// <param name="control">Control to display as page content.</param>
    /// <returns>Reference to the newly created page.</returns>
    public KryptonRibbonBackstagePage Add(string text, Control control)
    {
        var page = new KryptonRibbonBackstagePage(text, control);
        Add(page);
        return page;
    }

    /// <summary>
    /// Add a page with title and description to the collection.
    /// </summary>
    /// <param name="text">Text for the navigation button.</param>
    /// <param name="title">Title to display in content area.</param>
    /// <param name="description">Description to display in content area.</param>
    /// <returns>Reference to the newly created page.</returns>
    public KryptonRibbonBackstagePage Add(string text, string title, string description)
    {
        var page = new KryptonRibbonBackstagePage(text, title, description);
        Add(page);
        return page;
    }

    /// <summary>
    /// Add a page to the collection.
    /// </summary>
    /// <param name="text">Text for the page.</param>
    /// <returns>Reference to the newly created page.</returns>
    public KryptonRibbonBackstagePage Add(string text)
    {
        var page = new KryptonRibbonBackstagePage(text);
        Add(page);
        return page;
    }

    /// <summary>
    /// Insert a page to the collection at the specified index.
    /// </summary>
    /// <param name="index">Insert index.</param>
    /// <param name="text">Text for the page.</param>
    /// <returns>Reference to the newly created page.</returns>
    public KryptonRibbonBackstagePage Insert(int index, string text)
    {
        var page = new KryptonRibbonBackstagePage(text);
        Insert(index, page);
        return page;
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Raises the Inserting event.
    /// </summary>
    /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
    protected override void OnInserting(TypedCollectionEventArgs<KryptonRibbonBackstagePage> e)
    {
        // Let base class perform validation
        base.OnInserting(e);

        // Notify the ribbon of the change
        _owner.OnBackstagePageInserting(e);
    }

    /// <summary>
    /// Raises the Inserted event.
    /// </summary>
    /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
    protected override void OnInserted(TypedCollectionEventArgs<KryptonRibbonBackstagePage> e)
    {
        // Let base class perform default processing
        base.OnInserted(e);

        // Notify the ribbon of the change
        _owner.OnBackstagePageInserted(e);
    }

    /// <summary>
    /// Raises the Removing event.
    /// </summary>
    /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
    protected override void OnRemoving(TypedCollectionEventArgs<KryptonRibbonBackstagePage> e)
    {
        // Let base class perform validation
        base.OnRemoving(e);

        // Notify the ribbon of the change
        _owner.OnBackstagePageRemoving(e);
    }

    /// <summary>
    /// Raises the Removed event.
    /// </summary>
    /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
    protected override void OnRemoved(TypedCollectionEventArgs<KryptonRibbonBackstagePage> e)
    {
        // Let base class perform default processing
        base.OnRemoved(e);

        // Notify the ribbon of the change
        _owner.OnBackstagePageRemoved(e);
    }

    /// <summary>
    /// Raises the Clearing event.
    /// </summary>
    /// <param name="e">An EventArgs instance containing event data.</param>
    protected override void OnClearing(EventArgs e)
    {
        // Let base class perform validation
        base.OnClearing(e);

        // Notify the ribbon of the change
        _owner.OnBackstagePageClearing(e);
    }

    /// <summary>
    /// Raises the Cleared event.
    /// </summary>
    /// <param name="e">An EventArgs instance containing event data.</param>
    protected override void OnCleared(EventArgs e)
    {
        // Let base class perform default processing
        base.OnCleared(e);

        // Notify the ribbon of the change
        _owner.OnBackstagePageCleared(e);
    }
    #endregion
}
