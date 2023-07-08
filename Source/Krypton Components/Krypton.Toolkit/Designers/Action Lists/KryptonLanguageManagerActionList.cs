#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    internal class KryptonLanguageManagerActionList : DesignerActionList
    {
        #region Instance Fields

        private readonly KryptonLanguageManager? _languageManager;

        private readonly IComponentChangeService _service;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="KryptonLanguageManagerActionList" /> class.</summary>
        /// <param name="manager">The manager.</param>
        public KryptonLanguageManagerActionList(KryptonLanguageManagerDesigner manager) : base(manager.Component)
        {
            _languageManager = manager.Component as KryptonLanguageManager;

            // Cache service used to notify when a property has changed
            _service = (IComponentChangeService)GetService(typeof(IComponentChangeService));
        }

        #endregion

        #region Public Overrides

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            var actions = new DesignerActionItemCollection();

            if (_languageManager != null)
            {
                actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Reset strings to factory defaults", OnResetStrings), "Actions"));
            }

            return actions;
        }

        #endregion

        #region Implementation

        private void OnResetStrings(object sender, EventArgs e)
        {
            if (_languageManager != null)
            {
                DialogResult result =
                    MessageBox.Show(@"Are you sure that you want to reset all strings back to default?",
                        @"Reset Strings", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    _languageManager.Reset();

                    _service.OnComponentChanged(_languageManager, null, null, null);
                }
            }
        }

        #endregion
    }
}