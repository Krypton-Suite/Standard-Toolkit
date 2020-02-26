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

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Workspace
{
    internal class KryptonWorkspaceSequenceDesigner : ComponentDesigner
    {
        #region Instance Fields
        private KryptonWorkspaceSequence _sequence;
        private IComponentChangeService _changeService;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonWorkspaceSequenceDesigner class.
        /// </summary>
        public KryptonWorkspaceSequenceDesigner()
        {
        }            
        #endregion

        #region Public
        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The IComponent to associate the designer with.</param>
        public override void Initialize(IComponent component)
        {
            Debug.Assert(component != null);

            // Validate the parameter reference
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            // Let base class do standard stuff
            base.Initialize(component);

            // Cast to correct type
            _sequence = (KryptonWorkspaceSequence)component;

            // Get access to the services
            _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));

            // We need to know when we are being removed/changed
            _changeService.ComponentRemoving += OnComponentRemoving;
        }

        /// <summary>
        /// Gets the collection of components associated with the component managed by the designer.
        /// </summary>
        public override ICollection AssociatedComponents
        {
            get
            {
                // Create a new compound array
                ArrayList compound = new ArrayList();

                // Add the list of collection items
                compound.AddRange(_sequence.Children);

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
            // If our sequence is being removed
            if (e.Component == _sequence)
            {
                // Need access to host in order to delete a component
                IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));

                // Climb the workspace item tree to get the top most sequence
                KryptonWorkspace workspace = null;
                IWorkspaceItem workspaceItem = _sequence;
                while (workspaceItem.WorkspaceParent != null)
                {
                    workspaceItem = workspaceItem.WorkspaceParent;
                }

                // Grab the workspace control that contains the top most sequence
                if (workspaceItem is KryptonWorkspaceSequence sequence)
                {
                    workspace = sequence.WorkspaceControl;
                }

                // We need to remove all children from the sequence
                for (int j = _sequence.Children.Count - 1; j >= 0; j--)
                {
                    Component comp = _sequence.Children[j] as Component;

                    // If the component is a control...
                    if ((comp is Control) && (workspace != null))
                    {
                        // We need to manually remove it from the workspace controls collection
                        KryptonReadOnlyControls readOnlyControls = (KryptonReadOnlyControls)workspace.Controls;
                        readOnlyControls.RemoveInternal(comp as Control);
                    }

                    host.DestroyComponent(comp);

                    // Must remove the child after it has been destroyed otherwise the component destroy method 
                    // will not be able to climb the sequence chain to find the parent workspace instance
                    _sequence.Children.Remove(comp);
                }
            }
        }
        #endregion
    }
}
