#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
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
    private DesignerVerb _upgradeXmlVerb;
    private DesignerVerb _upgradeXmlFolderVerb;
    private DesignerVerb _convertVerb;

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

                _importVerb = new DesignerVerb(@"Import palette...", OnImport);

                _exportVerb = new DesignerVerb(@"Export palette...", OnExport);

                _upgradeVerb = new DesignerVerb(@"Upgrade Palette", OnUpgrade);

                _upgradeXmlVerb = new DesignerVerb(@"Upgrade .xml to .kpalx...", OnUpgradeXml);

                _upgradeXmlFolderVerb = new DesignerVerb(@"Upgrade folder .xml to .kpalx...", OnUpgradeXmlFolder);

                _convertVerb = new DesignerVerb(@"Convert palette file...", OnConvert);

                _verbCollection.AddRange(new DesignerVerb[] { _resetVerb, _populateVerb, _importVerb, _exportVerb, _upgradeVerb, _upgradeXmlVerb, _upgradeXmlFolderVerb, _convertVerb });
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
            ofd.DefaultExt = KryptonPaletteFile.Extension;
            ofd.Filter = KryptonPaletteFile.DialogFilter;
            ofd.Title = @"Load Custom Palette";

            KryptonPaletteFile.EnsureShellAssociations();

            var paletteFileName = (ofd.ShowDialog() == DialogResult.OK)
                ? ofd.FileName
                : string.Empty;

            if (string.IsNullOrWhiteSpace(paletteFileName))
            {
                return;
            }

            _palette?.ImportWithUpgrade(paletteFileName);
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    private void OnUpgradeXml(object? sender, EventArgs e)
    {
        try
        {
            var destination = _palette?.ActionListUpgradeXml();
            if (!string.IsNullOrWhiteSpace(destination) && _palette != null)
            {
                _service?.OnComponentChanged(_palette, null, null, null);
            }
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    private void OnUpgradeXmlFolder(object? sender, EventArgs e)
    {
        try
        {
            _palette?.ActionListUpgradeXmlDirectory();
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    private void OnConvert(object? sender, EventArgs e)
    {
        try
        {
            var destination = _palette?.ActionListConvert();
            if (!string.IsNullOrWhiteSpace(destination) && _palette != null)
            {
                _service?.OnComponentChanged(_palette, null, null, null);
            }
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
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