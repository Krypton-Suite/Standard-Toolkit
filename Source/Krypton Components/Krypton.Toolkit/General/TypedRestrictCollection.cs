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
/// Manage a collection of references that must be one of a restricted set of types.
/// </summary>
public abstract class TypedRestrictCollection<T> : TypedCollection<T> where T : class
{
    #region Restrict
    /// <summary>
    /// Gets an array of types that the collection is restricted to contain.
    /// </summary>
    public abstract Type[] RestrictTypes { get; }
    #endregion

    #region IList
    /// <summary>
    /// Discover if the incoming type is allowed to be in the collection.
    /// </summary>
    /// <param name="value">Instance to test.</param>
    /// <returns>True if allowed; otherwise false.</returns>
    protected bool IsTypeAllowed(object value) =>
        // Check if incoming instance derives from an allowed type

        RestrictTypes.Any(t => t.IsInstanceOfType(value));

    /// <summary>
    /// Append an item to the collection.
    /// </summary>
    /// <param name="value">Object reference.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>The position into which the new item was inserted.</returns>
    public override int Add(object? value) =>
        // We only allow objects that implement a restricted type
        (value != null) && !IsTypeAllowed(value)
            ? throw new ArgumentException("Type to be added is not allowed in this collection.")
            : base.Add(value!);

    /// <summary>
    /// Inserts an item to the collection at the specified index.
    /// </summary>
    /// <param name="index">Insert index.</param>
    /// <param name="value">Object reference.</param>
    /// <exception cref="ArgumentException"></exception>
    public override void Insert(int index, object? value)
    {
        // We only allow objects that implement IQuickAccessToolbarButton
        if ((value != null) && !IsTypeAllowed(value))
        {
            throw new ArgumentException("Type to be added is not allowed in this collection.");
        }

        base.Insert(index, value!);
    }
    #endregion

    #region IList<T>

    /// <summary>
    /// Inserts an item to the collection at the specified index.
    /// </summary>
    /// <param name="index">Insert index.</param>
    /// <param name="item">Item reference.</param>
    /// <exception cref="ArgumentException"></exception>
    public override void Insert(int index, T? item)
    {
        // We only allow objects that implement IQuickAccessToolbarButton
        if ((item != null) && !IsTypeAllowed(item))
        {
            throw new ArgumentException("Type to be added is not allowed in this collection.");
        }

        base.Insert(index, item!);
    }
    #endregion

    #region ICollection<T>

    /// <summary>
    /// Append an item to the collection.
    /// </summary>
    /// <param name="item">Item reference.</param>
    /// <exception cref="ArgumentException"></exception>
    public override void Add(T? item)
    {
        // We only allow objects that implement IQuickAccessToolbarButton
        if ((item != null) && !IsTypeAllowed(item))
        {
            throw new ArgumentException("Type to be added is not allowed in this collection.");
        }

        base.Add(item!);
    }
    #endregion
}