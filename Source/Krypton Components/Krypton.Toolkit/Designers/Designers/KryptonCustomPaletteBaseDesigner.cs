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

internal class KryptonCustomPaletteBaseDesigner : ComponentDesigner
{
    #region Instance Fields

    private DesignerVerbCollection _verbCollection;
    private DesignerVerb _resetVerb;
    private DesignerVerb _populateVerb;
    private DesignerVerb _importVerb;
    private DesignerVerb _exportVerb;
    private DesignerVerb _upgradeVerb;

    private KryptonCustomPaletteBase? _palette;

    private IComponentChangeService? _service;

    #endregion

    #region Public Overrides

    public override void Initialize([DisallowNull] IComponent component)
    {
        base.Initialize(component);

        Debug.Assert(component != null);

        _palette = component as KryptonCustomPaletteBase;

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
                // Add the palette specific list
                new KryptonCustomPaletteBaseActionList(this)
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

                _resetVerb = new DesignerVerb(@"Reset to Defaults", OnReset);

                _populateVerb = new DesignerVerb(@"Populate from Base", OnPopulate);

                _importVerb = new DesignerVerb(@"Import from XML File...", OnImport);

                _exportVerb = new DesignerVerb(@"Export to XML File...", OnExport);

                _upgradeVerb = new DesignerVerb(@"Upgrade Palette", OnUpgrade);

                _verbCollection.AddRange(new DesignerVerb[] { _resetVerb, _populateVerb, _importVerb, _exportVerb, _upgradeVerb });
            }

            return _verbCollection;
        }
    }

    #endregion

    #region Implementation

    private void OnUpgrade(object? sender, EventArgs e)
    {
        try
        {
            using var ofd = new OpenFileDialog(); /*KryptonOpenFileDialog*/
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.DefaultExt = @"xml";
            ofd.Filter = @"Palette files (*.xml)|*.xml|All files (*.*)|(*.*)";
            ofd.Title = @"Load Custom Palette";

            var paletteFileName = (ofd.ShowDialog() == DialogResult.OK)
                ? ofd.FileName
                : string.Empty;

            if (string.IsNullOrWhiteSpace(paletteFileName))
            {
                return;
            }

            _palette?.ImportWithUpgrade(File.OpenRead(paletteFileName));
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: GlobalStaticValues.DEFAULT_USE_STACK_TRACE);
        }
    }

    private void OnExport(object? sender, EventArgs e) => _palette?.ActionListExport();

    private void OnImport(object? sender, EventArgs e)
    {
        if (_palette != null)
        {
            _palette.ActionListImport();
            _service?.OnComponentChanged(_palette, null, null, null);
        }
    }

    private void OnPopulate(object? sender, EventArgs e)
    {
        if (_palette != null)
        {
            if (KryptonMessageBox.Show(@"Are you sure you want to populate from the base?",
                    @"Populate From Base",
                    KryptonMessageBoxButtons.YesNo,
                    KryptonMessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _palette.PopulateFromBase(false);
                _service?.OnComponentChanged(_palette, null, null, null);
            }
        }
    }

    private void OnReset(object? sender, EventArgs e)
    {
        if (_palette != null)
        {
            if (KryptonMessageBox.Show(@"Are you sure you want to reset the palette?",
                    @"Palette Reset",
                    KryptonMessageBoxButtons.YesNo,
                    KryptonMessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _palette.ResetToDefaults(false);
                _service?.OnComponentChanged(_palette, null, null, null);
            }
        }
    }

    #endregion
}