﻿#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    internal class KryptonLinkWrapLabelActionList : DesignerActionList
    {
        #region Instance Fields
        private readonly KryptonLinkWrapLabel _linkWrapLabel;
        private readonly IComponentChangeService _service;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonLinkWrapLabelActionList class.
        /// </summary>
        /// <param name="owner">Designer that owns this action list instance.</param>
        public KryptonLinkWrapLabelActionList(KryptonLinkWrapLabelDesigner owner)
            : base(owner.Component)
        {
            // Remember the label instance
            _linkWrapLabel = owner.Component as KryptonLinkWrapLabel;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the label style.
        /// </summary>
        public LabelStyle LabelStyle
        {
            get => _linkWrapLabel.LabelStyle;

            set
            {
                if (_linkWrapLabel.LabelStyle != value)
                {
                    _service.OnComponentChanged(_linkWrapLabel, null, _linkWrapLabel.LabelStyle, value);
                    _linkWrapLabel.LabelStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets and sets the palette mode.
        /// </summary>
        public PaletteMode PaletteMode
        {
            get => _linkWrapLabel.PaletteMode;

            set
            {
                if (_linkWrapLabel.PaletteMode != value)
                {
                    _service.OnComponentChanged(_linkWrapLabel, null, _linkWrapLabel.PaletteMode, value);
                    _linkWrapLabel.PaletteMode = value;
                }
            }
        }

        /// <summary>Gets or sets the font.</summary>
        /// <value>The font.</value>
        public Font Font
        {
            get => _linkWrapLabel.StateCommon.Font;

            set
            {
                if (_linkWrapLabel.StateCommon.Font != value)
                {
                    _service.OnComponentChanged(_linkWrapLabel, null, _linkWrapLabel.StateCommon.Font, value);

                    _linkWrapLabel.StateCommon.Font = value;
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
            DesignerActionItemCollection actions = new();

            // This can be null when deleting a control instance at design time
            if (_linkWrapLabel != null)
            {
                actions.Add(new DesignerActionHeaderItem(@"Appearance"));
                actions.Add(new DesignerActionPropertyItem(@"LabelStyle", @"Style", @"Appearance", @"Label style"));
                actions.Add(new DesignerActionPropertyItem(@"Font", @"Font", @"Appearance", @"The wrap label font."));
                actions.Add(new DesignerActionHeaderItem(@"Visuals"));
                actions.Add(new DesignerActionPropertyItem(@"PaletteMode", @"Palette", @"Visuals", @"Palette applied to drawing"));
            }

            return actions;
        }
        #endregion
    }
}