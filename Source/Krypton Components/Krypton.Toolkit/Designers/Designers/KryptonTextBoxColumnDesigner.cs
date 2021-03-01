﻿// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Krypton.Toolkit
{
    internal class KryptonTextBoxColumnDesigner : ComponentDesigner
    {
        #region Instance Fields
        private KryptonDataGridViewTextBoxColumn _textBox;
        private IComponentChangeService _changeService;
        #endregion

        #region Public Overrides
        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The IComponent to associate the designer with.</param>
        public override void Initialize(IComponent component)
        {
            // Let base class do standard stuff
            base.Initialize(component);

            // Cast to correct type
            _textBox = component as KryptonDataGridViewTextBoxColumn;

            // Get access to the design services
            _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }

        /// <summary>
        /// Gets the collection of components associated with the component managed by the designer.
        /// </summary>
        public override ICollection AssociatedComponents =>
            _textBox != null ? _textBox.ButtonSpecs : base.AssociatedComponents;

        #endregion

        #region Private
        private void OnComponentRemoving(object sender, ComponentEventArgs e)
        {
            // If our control is being removed
            if ((_textBox != null) && (e.Component == _textBox))
            {
                // Need access to host in order to delete a component
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                // We need to remove all the button spec instances
                for (int i = _textBox.ButtonSpecs.Count - 1; i >= 0; i--)
                {
                    // Get access to the indexed button spec
                    ButtonSpec spec = _textBox.ButtonSpecs[i];

                    // Must wrap button spec removal in change notifications
                    _changeService.OnComponentChanging(_textBox, null);

                    // Perform actual removal of button spec from textbox
                    _textBox.ButtonSpecs.Remove(spec);

                    // Get host to remove it from design time
                    host.DestroyComponent(spec);

                    // Must wrap button spec removal in change notifications
                    _changeService.OnComponentChanged(_textBox, null, null, null);
                }
            }
        }
        #endregion
    }
}
