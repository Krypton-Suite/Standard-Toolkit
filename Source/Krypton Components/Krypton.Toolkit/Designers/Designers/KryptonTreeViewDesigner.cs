﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion


namespace Krypton.Toolkit
{
    internal class KryptonTreeViewDesigner : ControlDesigner
    {
        #region Public Overrides
        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The IComponent to associate the designer with.</param>
        public override void Initialize(IComponent component)
        {
            // Let base class do standard stuff
            base.Initialize(component);

            Debug.Assert(component != null);

            // The resizing handles around the control need to change depending on the
            // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
            // do not get the resizing handles, otherwise you do.
            AutoResizeHandles = true;
        }

        /// <summary>
        ///  Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                // Create a collection of action lists
                DesignerActionListCollection actionLists = new()
                {

                    // Add the label specific list
                    new KryptonTreeViewActionList(this)
                };

                return actionLists;
            }
        }
        #endregion
    }

    internal class NoneExcludedImageIndexConverter : ImageIndexConverter
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the NoneExcludedImageIndexConverter class.
        /// </summary>
        public NoneExcludedImageIndexConverter()
        {
        }
        #endregion

        // Properties
        /// <summary>
        /// Indicates if the the None value should be included in standard values for selection.
        /// </summary>
        protected override bool IncludeNoneAsStandardValue => false;
    }
}
