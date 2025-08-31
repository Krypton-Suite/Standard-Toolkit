#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    /// <summary>
    /// Collection editor for ThemedSystemMenuItem objects that provides a user-friendly interface in the designer.
    /// </summary>
    public class ThemedSystemMenuItemsEditor : CollectionEditor
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ThemedSystemMenuItemsEditor class.
        /// </summary>
        /// <param name="type">The type of collection to edit.</param>
        public ThemedSystemMenuItemsEditor(Type type) : base(type)
        {
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Creates a new instance of the specified collection item type.
        /// </summary>
        /// <param name="itemType">The type of item to create.</param>
        /// <returns>A new instance of the specified type.</returns>
        protected override object CreateInstance(Type itemType)
        {
            if (itemType == typeof(ThemedSystemMenuItemValues))
            {
                return new ThemedSystemMenuItemValues("New Menu Item");
            }

            return base.CreateInstance(itemType);
        }

        /// <summary>
        /// Gets the display text for the specified item.
        /// </summary>
        /// <param name="value">The item to get the display text for.</param>
        /// <returns>The display text for the item.</returns>
        protected override string GetDisplayText(object? value)
        {
            if (value is ThemedSystemMenuItemValues menuItem)
            {
                return menuItem.ToString();
            }

            return base.GetDisplayText(value);
        }
        #endregion
    }
}