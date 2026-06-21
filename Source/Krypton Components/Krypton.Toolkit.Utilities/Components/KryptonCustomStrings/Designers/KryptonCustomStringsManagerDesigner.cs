#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal class KryptonCustomStringsManagerDesigner : ComponentDesigner
{
    #region Instance Fields

    private DesignerVerbCollection? _verbCollection;
    private DesignerVerb? _resetVerb;
    private KryptonCustomStringsManager? _manager;
    private IComponentChangeService? _service;

    #endregion

    #region Public Overrides

    /// <inheritdoc />
    public override void Initialize(IComponent component)
    {
        base.Initialize(component);

        _manager = component as KryptonCustomStringsManager;
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    /// <inheritdoc />
    public override DesignerVerbCollection Verbs
    {
        get
        {
            if (_verbCollection == null)
            {
                _verbCollection = new DesignerVerbCollection();
                _resetVerb = new DesignerVerb(@"Reset Custom Strings", OnResetCustomStrings);
                _verbCollection.Add(_resetVerb);
            }

            UpdateVerbStatus();
            return _verbCollection;
        }
    }

    #endregion

    #region Implementation

    private void UpdateVerbStatus()
    {
        if (_resetVerb != null)
        {
            _resetVerb.Enabled = _manager != null && !KryptonCustomStrings.Values.IsDefault;
        }
    }

    private void OnResetCustomStrings(object? sender, EventArgs e)
    {
        if (_manager == null)
        {
            return;
        }

        DialogResult result = KryptonMessageBox.Show(
            @"This will reset all custom string values to their defaults. Do you want to continue?",
            @"Reset Custom Strings",
            KryptonMessageBoxButtons.YesNo,
            KryptonMessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            KryptonCustomStrings.ResetValues();
            _service?.OnComponentChanged(_manager, null, _manager.CustomStrings, _manager.CustomStrings);
            UpdateVerbStatus();
        }
    }

    #endregion
}
