#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable MemberCanBeInternal
// ReSharper disable UnusedMember.Global

namespace Krypton.Toolkit
{
    /// <summary>
    /// Event arguments for the ToolTipNeeded event raised by a KryptonComboBox when it
    /// needs to render a tooltip.
    /// </summary>
    public class ToolTipNeededEventArgs : EventArgs
    {
        /// <summary>
        /// The title of the tooltip.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The body of the tooltip.
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// The icon of the tooltip.
        /// </summary>
        public Image Icon { get; set; }

        /// <summary>
        /// Gets whether the instance is empty.
        /// </summary>
        public bool IsEmpty => string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(Body) && Icon == null;

        /// <summary>
        /// Initializes a new instance of the ToolTipNeededEventArgs class.
        /// </summary>
        /// <param name="index">
        /// The index of the item for which a tooltip is being requested.
        /// </param>
        /// <param name="item">
        /// The item for which a tooltip is being requested.
        /// </param>
        public ToolTipNeededEventArgs(int index, object item)
        {
            Index = index;
            Item = item;
        }

        /// <summary>
        /// The index of the item of the <see cref="KryptonComboBox"/> for which a tooltip is
        /// being requested.
        /// </summary>
        public int Index
        {
            get;
            private set;
        }

        /// <summary>
        /// The item of the <see cref="KryptonComboBox"/> for which a tooltip is being
        /// requested.
        /// </summary>
        public object Item
        {
            get;
            private set;
        }
    }
}