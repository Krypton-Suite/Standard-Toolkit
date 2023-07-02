﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon
{
    internal class KryptonRibbonActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonRibbon? _ribbon;
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

        /// <summary>Gets or sets a value indicating whether [allow form integrate].</summary>
        /// <value><c>true</c> if [allow form integrate]; otherwise, <c>false</c>.</value>
        public bool AllowFormIntegrate
        {
            get => _ribbon.AllowFormIntegrate;
            set => _ribbon.AllowFormIntegrate = value;
        }

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
            var actions = new DesignerActionItemCollection();

            // This can be null when deleting a control instance at design time
            if (_ribbon != null)
            {
                // Add the list of button specific actions
                actions.Add(new DesignerActionHeaderItem("Design"));
                actions.Add(new DesignerActionPropertyItem(nameof(InDesignHelperMode), "Design Helpers", "Design", "Show design time helpers for creating items."));
                actions.Add(new DesignerActionHeaderItem("Visuals"));
                actions.Add(new DesignerActionPropertyItem(nameof(AllowFormIntegrate), "Allow Form Integration", "Visuals", "Integrate with operating system chrome instead of Krypton Palette."));
                actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals", "Palette applied to drawing"));
            }

            return actions;
        }
        #endregion
    }
}
