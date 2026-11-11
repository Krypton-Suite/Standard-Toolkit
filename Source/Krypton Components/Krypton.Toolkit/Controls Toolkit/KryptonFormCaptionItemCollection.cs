#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Collection of <see cref="KryptonFormCaptionItem"/> entries for <see cref="KryptonCaptionForm.CaptionItems"/>.
/// </summary>
public class KryptonFormCaptionItemCollection : TypedCollection<KryptonFormCaptionItem>
{
    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonFormCaptionItemCollection"/> class.
    /// </summary>
    /// <param name="owner">Form that owns this collection.</param>
    public KryptonFormCaptionItemCollection(KryptonCaptionForm owner) => Owner = owner;

    #endregion

    #region Public

    /// <summary>
    /// Gets the owning caption form.
    /// </summary>
    public KryptonCaptionForm Owner { get; }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() => $"{Count} CaptionItems";

    #endregion

    #region Protected

    /// <summary>
    /// Raises the Inserting event.
    /// </summary>
    /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
    protected override void OnInserting(TypedCollectionEventArgs<KryptonFormCaptionItem> e)
    {
        if (e.Item != null)
        {
            e.Item.Owner = Owner;
        }

        base.OnInserting(e);
    }

    /// <summary>
    /// Raises the Inserted event.
    /// </summary>
    /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
    protected override void OnInserted(TypedCollectionEventArgs<KryptonFormCaptionItem> e)
    {
        base.OnInserted(e);
        Owner.NotifyCaptionItemsChanged();
    }

    /// <summary>
    /// Raises the Removing event.
    /// </summary>
    /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
    protected override void OnRemoving(TypedCollectionEventArgs<KryptonFormCaptionItem> e)
    {
        if (e.Item != null)
        {
            e.Item.Owner = null;
        }

        base.OnRemoving(e);
    }

    /// <summary>
    /// Raises the Removed event.
    /// </summary>
    /// <param name="e">A TypedCollectionEventArgs instance containing event data.</param>
    protected override void OnRemoved(TypedCollectionEventArgs<KryptonFormCaptionItem> e)
    {
        base.OnRemoved(e);
        Owner.NotifyCaptionItemsChanged();
    }

    /// <summary>
    /// Raises the Cleared event.
    /// </summary>
    /// <param name="e">An EventArgs instance containing event data.</param>
    protected override void OnCleared(EventArgs e)
    {
        base.OnCleared(e);
        Owner.NotifyCaptionItemsChanged();
    }

    #endregion
}
