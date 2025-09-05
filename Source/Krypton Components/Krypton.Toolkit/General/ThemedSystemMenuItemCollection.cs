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
    /// Collection of ThemedSystemMenuItem objects that supports designer serialization and change notifications.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ThemedSystemMenuItemCollection : ObservableCollection<ThemedSystemMenuItemValues>
    {


        #region Identity
        /// <summary>
        /// Initialize a new instance of the ThemedSystemMenuItemCollection class.
        /// </summary>
        public ThemedSystemMenuItemCollection()
        {
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Raises the CollectionChanged event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a new menu item with the specified text.
        /// </summary>
        /// <param name="text">The text for the menu item.</param>
        /// <returns>The newly added menu item.</returns>
        public ThemedSystemMenuItemValues Add(string text)
        {
            var item = new ThemedSystemMenuItemValues(text);
            Add(item);
            return item;
        }

        /// <summary>
        /// Adds a new menu item with the specified text and shortcut.
        /// </summary>
        /// <param name="text">The text for the menu item.</param>
        /// <param name="shortcut">The keyboard shortcut text.</param>
        /// <returns>The newly added menu item.</returns>
        public ThemedSystemMenuItemValues Add(string text, string shortcut)
        {
            var item = new ThemedSystemMenuItemValues(text, shortcut);
            Add(item);
            return item;
        }

        /// <summary>
        /// Returns a string representation of the collection.
        /// </summary>
        /// <returns>A string showing the number of items in the collection.</returns>
        public override string ToString()
        {
            return Count == 0 ? "No custom menu items" : $"{Count} custom menu item(s)";
        }
        #endregion
    }
}