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


namespace Krypton.Ribbon
{
    /// <summary>
    /// Layout area for the application tab.
    /// </summary>
    internal class ViewLayoutRibbonAppTab : ViewLayoutDocker
    {
        #region Instance Fields
        private readonly KryptonRibbon _ribbon;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutRibbonAppTab class.
        /// </summary>
        /// <param name="ribbon">Owning control instance.</param>
        public ViewLayoutRibbonAppTab(KryptonRibbon ribbon)
        {
            Debug.Assert(ribbon != null);
            _ribbon = ribbon;

            AppTab = new ViewDrawRibbonAppTab(ribbon);

            // Dock it against the appropriate edge
            Add(AppTab, ViewDockStyle.Bottom);
            Add(new ViewLayoutSeparator(1), ViewDockStyle.Left);
        }

        /// <summary>
        /// Obtains t+he String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString() =>
            // Return the class name and instance identifier
            "ViewLayoutRibbonAppTab:" + Id;

        #endregion

        #region AppTab
        /// <summary>
        /// Gets the view element that represents the button.
        /// </summary>
        public ViewDrawRibbonAppTab AppTab { get; }

        #endregion
    }
}
