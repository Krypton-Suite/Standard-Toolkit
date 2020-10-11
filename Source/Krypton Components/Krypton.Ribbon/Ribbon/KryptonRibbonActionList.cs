// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2020. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 5.550.0  
// *****************************************************************************

using System.ComponentModel.Design;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    internal class KryptonRibbonActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonRibbon _ribbon;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonRibbonActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonRibbonActionList(KryptonRibbonDesigner owner) 
            : base(owner.Component)
        {
            // Remember the ribbon instance
            _ribbon = (KryptonRibbon)owner.Component;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion
        
        #region Public
        /// <summary>
        /// Gets and sets use of design time helpers.
        /// </summary>
        public bool InDesignHelperMode
        {
            get => _ribbon.InDesignHelperMode;
            set => _ribbon.InDesignHelperMode = value;
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _ribbon.PaletteMode;

            set
            {
                if (_ribbon.PaletteMode != value)
                {
                    _service.OnComponentChanged(_ribbon, null, _ribbon.PaletteMode, value);
                    _ribbon.PaletteMode = value;
                }
            }
        }
        #endregion

        #region Public Override
        /// <summary>
        /// Returns the collection of DesignerActionItem objects contained in the list.
        /// </summary>
        /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            // Create a new collection for holding the single item we want to create
            DesignerActionItemCollection actions = new DesignerActionItemCollection();

            // This can be null when deleting a control instance at design time
            if (_ribbon != null)
            {
                // Add the list of button specific actions
                actions.Add(new DesignerActionHeaderItem("Design"));
                actions.Add(new DesignerActionPropertyItem("InDesignHelperMode", "Design Helpers", "Design", "Show design time helpers for creating items."));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem("PaletteMode", "Palette", "Visuals", "Palette applied to drawing"));
            }
            
            return actions;
        }
        #endregion
    }
}
