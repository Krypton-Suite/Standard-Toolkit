﻿namespace Krypton.Toolkit
{
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
        protected bool IsTypeAllowed(object value)
        {
            var valid = false;

            // Check if incoming instance derives from an allowed type
            foreach (Type t in RestrictTypes)
            {
                if (t.IsInstanceOfType(value))
                {
                    valid = true;
                    break;
                }
            }

            return valid;
        }

        /// <summary>
        /// Append an item to the collection.
        /// </summary>
        /// <param name="value">Object reference.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>The position into which the new item was inserted.</returns>
        public override int Add(object value) =>
            // We only allow objects that implement a restricted type
            (value != null) && !IsTypeAllowed(value)
                ? throw new ArgumentException("Type to be added is not allowed in this collection.")
                : base.Add(value);

        /// <summary>
        /// Inserts an item to the collection at the specified index.
        /// </summary>
        /// <param name="index">Insert index.</param>
        /// <param name="value">Object reference.</param>
        /// <exception cref="ArgumentException"></exception>
        public override void Insert(int index, object value)
        {
            // We only allow objects that implement IQuickAccessToolbarButton
            if ((value != null) && !IsTypeAllowed(value))
            {
                throw new ArgumentException("Type to be added is not allowed in this collection.");
            }

            base.Insert(index, value);
        }
        #endregion

        #region IList<T>

        /// <summary>
        /// Inserts an item to the collection at the specified index.
        /// </summary>
        /// <param name="index">Insert index.</param>
        /// <param name="item">Item reference.</param>
        /// <exception cref="ArgumentException"></exception>
        public override void Insert(int index, T item)
        {
            // We only allow objects that implement IQuickAccessToolbarButton
            if ((item != null) && !IsTypeAllowed(item))
            {
                throw new ArgumentException("Type to be added is not allowed in this collection.");
            }

            base.Insert(index, item);
        }
        #endregion

        #region ICollection<T>

        /// <summary>
        /// Append an item to the collection.
        /// </summary>
        /// <param name="item">Item reference.</param>
        /// <exception cref="ArgumentException"></exception>
        public override void Add(T item)
        {
            // We only allow objects that implement IQuickAccessToolbarButton
            if ((item != null) && !IsTypeAllowed(item))
            {
                throw new ArgumentException("Type to be added is not allowed in this collection.");
            }

            base.Add(item);
        }
        #endregion
    }
}
