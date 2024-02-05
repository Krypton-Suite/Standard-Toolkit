#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2024. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    internal class KryptonManagerDesigner : ComponentDesigner
    {
        #region Instance Fields

        private DesignerVerbCollection _verbCollection;

        private DesignerVerb _resetVerb;

        private KryptonManager? _manager;

        private IComponentChangeService? _service;

        #endregion

        #region Public Overrides

        public override void Initialize([DisallowNull] IComponent component)
        {
            base.Initialize(component);

            Debug.Assert(component != null);

            _manager = component as KryptonManager;

            _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        }

        /// <summary>
        ///  Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                // Create a collection of action lists
                var actionLists = new DesignerActionListCollection
                {
                    // Add the manager specific list
                    new KryptonManagerActionList(this)
                };

                return actionLists;
            }
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (_verbCollection == null)
                {
                    _verbCollection = [];

                    _resetVerb = new DesignerVerb(@"Reset to Default Theme", OnReset);

                    _verbCollection.AddRange(new DesignerVerb[] { _resetVerb });
                }

                return _verbCollection;
            }
        }

        #endregion

        #region Implementation

        private void OnReset(object sender, EventArgs e)
        {
            DebugTools.NotImplemented("");
        }

        #endregion
    }
}
