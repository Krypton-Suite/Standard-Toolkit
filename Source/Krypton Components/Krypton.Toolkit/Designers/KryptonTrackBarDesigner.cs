// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.500.0.0  
// *****************************************************************************

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Krypton.Toolkit
{
    internal class KryptonTrackBarDesigner : ControlDesigner
    {
        #region Instance Fields
        private KryptonTrackBar _trackBar;
        #endregion

        #region Public Overrides
        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The IComponent to associate the designer with.</param>
        public override void Initialize(IComponent component)
        {
            // ReSharper disable RedundantBaseQualifier
            // Let base class do standard stuff
            base.Initialize(component);
            base.AutoResizeHandles = true;
            // ReSharper restore RedundantBaseQualifier

            // Cast to correct type
            _trackBar = component as KryptonTrackBar;
        }

        /// <summary>
        /// Gets the selection rules that indicate the movement capabilities of a component.
        /// </summary>
        public override SelectionRules SelectionRules
        {
            get
            {
                if (!_trackBar.AutoSize)
                {
                    return SelectionRules.AllSizeable | SelectionRules.Moveable;
                }
                else
                {
                    if (_trackBar.Orientation == Orientation.Horizontal)
                    {
                        return SelectionRules.RightSizeable | SelectionRules.LeftSizeable | SelectionRules.Moveable;
                    }
                    else
                    {
                        return SelectionRules.TopSizeable | SelectionRules.BottomSizeable | SelectionRules.Moveable;
                    }
                }
            }
        }

        /// <summary>
        ///  Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                // Create a collection of action lists
                DesignerActionListCollection actionLists = new DesignerActionListCollection
                {

                    // Add the button specific list
                    new KryptonTrackBarActionList(this)
                };

                return actionLists;
            }
        }
        #endregion
    }
}
