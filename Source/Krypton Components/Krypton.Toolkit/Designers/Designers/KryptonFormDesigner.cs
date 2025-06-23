#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonFormDesigner : ParentControlDesigner
{
    /*
    #region Variables
    private DesignerVerbCollection _verbs;

    // TODO: When is this assigned to ?
    private IComponentChangeService _service;

    // TODO: When is this assigned to ?
    //private KryptonPalette _palette;
    #endregion

    public override DesignerVerbCollection Verbs => _verbs ?? (_verbs = new DesignerVerbCollection { new DesignerVerb(@"Reset to Defaults", OnVerbReset),
                                                               new DesignerVerb(@"Populate from Base", OnVerbPopulate), new DesignerVerb(@"Import from XML file", OnVerbImport),
                                                               new DesignerVerb(@"Export to XML file", OnVerbExport) });

    private void OnVerbReset(object sender, EventArgs e)
    {
        if (_palette != null)
        {
            if (MessageBox.Show("Are you sure you want to reset the palette?",
                                "Palette Reset",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _palette.ResetToDefaults(false);
                _service.OnComponentChanged(_palette, null, null, null);
            }
        }
    }

    private void OnVerbPopulate(object sender, EventArgs e)
    {
        if (_palette != null)
        {
            if (MessageBox.Show("Are you sure you want to populate from the base?",
                                "Populate From Base",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _palette.PopulateFromBase(false);
                _service.OnComponentChanged(_palette, null, null, null);
            }
        }
    }

    private void OnVerbImport(object sender, EventArgs e)
    {
        if (_palette != null)
        {
            _palette.Import();
            _service.OnComponentChanged(_palette, null, null, null);
        }
    }

    private void OnVerbExport(object sender, EventArgs e)
    {
        _palette?.Export();
    }
    */
}