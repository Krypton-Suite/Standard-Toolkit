#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonThemeComboBoxDesigner : ControlDesigner
{
    #region Instance Fields

    private DesignerVerbCollection? _verbCollection;
    private DesignerVerb _resetVerb;
    private KryptonThemeComboBox? _themeComboBox;
    private IComponentChangeService? _changeService;

    #endregion

    #region Public

    public override void Initialize([DisallowNull] IComponent component)
    {
        base.Initialize(component);

        Debug.Assert(component != null);

        _themeComboBox = component as KryptonThemeComboBox;

        _changeService = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    public override DesignerActionListCollection ActionLists
    {
        get;
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

            UpdateVerbStatus();

            return _verbCollection;
        }
    }

    #endregion

    #region Implementation

    private void UpdateVerbStatus()
    {
        if (_verbCollection != null)
        {
            _resetVerb.Enabled = !_themeComboBox!.SelectedIndex.Equals((int)PaletteMode.Microsoft365Blue);
        }
    }

    private void OnComponentChanged(object sender, ComponentChangedEventArgs e) => UpdateVerbStatus();

    private void OnComponentRemoving(object sender, ComponentEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnReset(object? sender, EventArgs e)
    {
        if (_themeComboBox != null)
        {
            DialogResult result = KryptonMessageBox.Show(@"This will reset the current theme back to 'Microsoft 365 - Blue'. Do you want to continue?",
                @"Reset Theme",
                KryptonMessageBoxButtons.YesNo,
                KryptonMessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                _themeComboBox.SelectedIndex = (int)PaletteMode.Microsoft365Blue;

                //_themeComboBox.OnComponentChanged(_manager, null, _manager.GlobalPaletteMode, PaletteMode.Microsoft365Blue);

                UpdateVerbStatus();
            }
        }
    }

    #endregion
}