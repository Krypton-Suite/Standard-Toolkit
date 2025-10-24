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

namespace Krypton.Toolkit;

/// <summary>
/// Extends the base class by implementing an end node view.
/// </summary>
public abstract class ViewLeaf : ViewBase
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewLeaf class.
    /// </summary>
    protected ViewLeaf()
    {
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString() =>
        // Return the class name and instance identifier
        $@"ViewLeaf:{Id}";

    #endregion

    #region Eval
    /// <summary>
    /// Evaluate the need for drawing transparent areas.
    /// </summary>
    /// <param name="context">Evaluation context.</param>
    /// <returns>True if transparent areas exist; otherwise false.</returns>
    public override bool EvalTransparentPaint([DisallowNull] ViewContext context)
    {
        Debug.Assert(context != null);
        return false;
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform a render of the elements.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void Render([DisallowNull] RenderContext context)
    {
        Debug.Assert(context != null);

        // Only render visible elements
        if (Visible)
        {
            // We have no children so perform all rendering now
            RenderBefore(context!);
            RenderAfter(context!);
        }
    }
    #endregion

    #region Collection

    /// <summary>
    /// Append a view to the collection.
    /// </summary>
    /// <param name="item">ViewBase reference.</param>
    /// <exception cref="NotSupportedException"></exception>
    public override void Add(ViewBase item) => throw
        // Can never add a view to a leaf view
        new NotSupportedException(@"Cannot add to a leaf view.");

    /// <summary>
    /// Remove all views from the collection.
    /// </summary>
    public override void Clear()
    {
        // Nothing to do
    }

    /// <summary>
    /// Determines whether the collection contains the view.
    /// </summary>
    /// <param name="item">ViewBase reference.</param>
    /// <returns>True if view found; otherwise false.</returns>
    public override bool Contains(ViewBase item) =>
        // Leaf never contains view
        false;

    /// <summary>
    /// Determines whether any part of the view hierarchy is the specified view.
    /// </summary>
    /// <param name="item">ViewBase reference.</param>
    /// <returns>True if view found; otherwise false.</returns>
    public override bool ContainsRecurse(ViewBase? item) =>
        // Only need to check against ourself
        this == item;

    /// <summary>
    /// Copies views to specified array starting at particular index.
    /// </summary>
    /// <param name="array">Target array.</param>
    /// <param name="arrayIndex">Starting array index.</param>
    public override void CopyTo(ViewBase[] array, int arrayIndex)
    {
        // Nothing to copy
    }

    /// <summary>
    /// Removes first occurrence of specified view.
    /// </summary>
    /// <param name="item">ViewBase reference.</param>
    /// <returns>True if removed; otherwise false.</returns>
    public override bool Remove(ViewBase item) =>
        // Can never remove with success
        false;

    /// <summary>
    /// Gets the number of views in collection.
    /// </summary>
    public override int Count => 0;

    /// <summary>
    /// Determines the index of the specified view in the collection.
    /// </summary>
    /// <param name="item">ViewBase reference.</param>
    /// <returns>-1 if not found; otherwise index position.</returns>
    public override int IndexOf(ViewBase item) =>
        // Can never find the item
        -1;

    /// <summary>
    /// Inserts a view to the collection at the specified index.
    /// </summary>
    /// <param name="index">Insert index.</param>
    /// <param name="item">ViewBase reference.</param>
    /// <exception cref="NotSupportedException"></exception>
    public override void Insert(int index, ViewBase item) => throw
        // Can never insert a view to a leaf view
        new NotSupportedException("@Cannot insert to a leaf view.");

    /// <summary>
    /// Removes the view at the specified index.
    /// </summary>
    /// <param name="index">Remove index.</param>
    /// <exception cref="NotSupportedException"></exception>
    public override void RemoveAt(int index) => throw
        // Can never remove a view from a leaf view
        new NotSupportedException(@"Cannot remove a view from a leaf view.");

    /// <summary>
    /// Gets or sets the view at the specified index.
    /// </summary>
    /// <param name="index">ViewBase index.</param>
    /// <returns>ViewBase at specified index.</returns>
    public override ViewBase this[int index]
    {
        get => throw new ArgumentOutOfRangeException(nameof(index));

        set => throw new ArgumentOutOfRangeException(nameof(index));
    }

    /// <summary>
    /// Shallow enumerate forward over children of the element.
    /// </summary>
    /// <returns>Enumerator instance.</returns>
    public override IEnumerator<ViewBase> GetEnumerator()
    {
        // A leaf has no child views
        yield break;
    }

    /// <summary>
    /// Deep enumerate forward over children of the element.
    /// </summary>
    /// <returns>Enumerator instance.</returns>
    public override IEnumerable<ViewBase> Recurse()
    {
        // A leaf has no child views
        yield break;
    }

    /// <summary>
    /// Shallow enumerate backwards over children of the element.
    /// </summary>
    /// <returns>Enumerator instance.</returns>
    public override IEnumerable<ViewBase> Reverse()
    {
        // A leaf has no child views
        yield break;
    }

    /// <summary>
    /// Deep enumerate backwards over children of the element.
    /// </summary>
    /// <returns>Enumerator instance.</returns>
    public override IEnumerable<ViewBase> ReverseRecurse()
    {
        // A leaf has no child views
        yield break;
    }
    #endregion

    #region ViewFromPoint
    /// <summary>
    /// Find the view that contains the specified point.
    /// </summary>
    /// <param name="pt">Point in view coordinates.</param>
    /// <returns>ViewBase if a match is found; otherwise false.</returns>
    public override ViewBase? ViewFromPoint(Point pt) => ClientRectangle.Contains(pt) ? this : null;

    #endregion
}