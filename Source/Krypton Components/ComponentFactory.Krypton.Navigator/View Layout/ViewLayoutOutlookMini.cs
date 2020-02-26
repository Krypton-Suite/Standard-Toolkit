// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2020. All rights reserved. (https://github.com/Wagnerp/Krypton-Toolkit-Suite-NET-Core)
//  Version 5.500.0.0  www.ComponentFactory.com
// *****************************************************************************

using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// View element that knows how to enforce the visible state of the stacked items.
    /// </summary>
    internal class ViewLayoutOutlookMini : ViewLayoutDocker
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewLayoutOutlookMini class.
        /// </summary>
        /// <param name="viewBuilder">View builder reference.</param>
        public ViewLayoutOutlookMini(ViewBuilderOutlookBase viewBuilder)
        {
            Debug.Assert(viewBuilder != null);
            ViewBuilder = viewBuilder;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewLayoutOutlookMini:" + Id;
        }
        #endregion

        #region ViewBuilder
        /// <summary>
        /// Gets access to the associated view builder.
        /// </summary>
        public ViewBuilderOutlookBase ViewBuilder
        {
            [DebuggerStepThrough]
            get;
        }

        #endregion

        #region Layout
        /// <summary>
        /// Perform a layout of the elements.
        /// </summary>
        /// <param name="context">Layout context.</param>
        public override void Layout(ViewLayoutContext context)
        {
            // Make all stacking items that should be visible are visible
            ViewBuilder.UnshrinkAppropriatePages();

            // Let base class continue with standard layout
            base.Layout(context);
        }
        #endregion
    }
}
