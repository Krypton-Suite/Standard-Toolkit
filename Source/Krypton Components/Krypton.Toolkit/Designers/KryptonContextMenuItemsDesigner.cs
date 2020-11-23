// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. (https://github.com/Krypton-Suite/Standard-Toolkit)
//  Version 6.0.0  
// *****************************************************************************

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Krypton.Toolkit
{
    internal class KryptonContextMenuItemsDesigner : ComponentDesigner
    {
        #region Instance Fields
        private KryptonContextMenuItems _contextMenuItems;
        private IComponentChangeService _changeService;
        #endregion

        #region Public Overrides
        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The IComponent to associate the designer with.</param>
        public override void Initialize(IComponent component)
        {
            // Validate the parameter reference
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            // Let base class do standard stuff
            base.Initialize(component);

            // Cast to correct type
            _contextMenuItems = component as KryptonContextMenuItems;

            // Get access to the services
            _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));

            // We need to know when we are being removed
            _changeService.ComponentRemoving += OnComponentRemoving;
        }

        /// <summary>
        /// Gets the collection of components associated with the component managed by the designer.
        /// </summary>
        public override ICollection AssociatedComponents
        {
            get
            {
                ArrayList compound = new ArrayList(base.AssociatedComponents);
                
                if (_contextMenuItems != null)
                {
                    compound.AddRange(_contextMenuItems.Items);
                }

                return compound;
            }
        }
        #endregion

        #region Protected
        /// <summary>
        /// Releases all resources used by the component. 
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    // Unhook from events
                    _changeService.ComponentRemoving -= OnComponentRemoving;
                }
            }
            finally
            {
                // Must let base class do standard stuff
                base.Dispose(disposing);
            }
        }
        #endregion

        #region Implementation
        private void OnComponentRemoving(object sender, ComponentEventArgs e)
        {
            // If our item collection is being removed
            if ((_contextMenuItems != null) && (e.Component == _contextMenuItems))
            {
                // Need access to host in order to delete a component
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                // We need to remove all items from the item collection
                for (int j = _contextMenuItems.Items.Count - 1; j >= 0; j--)
                {
                    Component item = _contextMenuItems.Items[j] as Component;
                    _contextMenuItems.Items.Remove(item);
                    host.DestroyComponent(item);
                }
            }
        }
        #endregion
    }
}
