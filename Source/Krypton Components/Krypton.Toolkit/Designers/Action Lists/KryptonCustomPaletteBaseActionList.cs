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

internal class KryptonCustomPaletteBaseActionList : DesignerActionList
{
    #region Instance Fields

    private readonly KryptonCustomPaletteBase _palette;
    private readonly IComponentChangeService? _service;

    #endregion

    #region Identity

    /// <summary>
    /// Initialize a new instance of the KryptonCustomPaletteBaseActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonCustomPaletteBaseActionList(KryptonCustomPaletteBaseDesigner owner)
        : base(owner.Component)
    {
        // Remember the panel instance
        _palette = (owner.Component as KryptonCustomPaletteBase)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }

    #endregion

    #region Public Override

    /// <summary>
    /// Returns the collection of DesignerActionItem objects contained in the list.
    /// </summary>
    /// <returns>A DesignerActionItem array that contains the items in this list.</returns>
    public override DesignerActionItemCollection GetSortedActionItems()
    {
        // Create a new collection for holding the single item we want to create
        var actions = new DesignerActionItemCollection();

        // This can be null when deleting a component instance at design time
        if (_palette != null)
        {
            // Add the list of panel specific actions
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Reset to Defaults", OnResetClick), "Actions"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Populate from Base", OnPopulateClick), "Actions"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Import from Xml file...", OnImportClick), "Actions"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Export to Xml file...", OnExportClick), "Actions"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Upgrade Palette", OnUpgradePalette), "Actions"));
            // TODO: Uncomment when binary serialisation is implemented
            //actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Export theme to binary", OnExportToBinaryClick), "Actions"));
        }

        return actions;
    }

    #endregion

    #region Implementation

    private void OnResetClick(object? sender, EventArgs e)
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

    private void OnPopulateClick(object? sender, EventArgs e)
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

    private void OnImportClick(object? sender, EventArgs e)
    {
        if (_palette != null)
        {
            _palette.ActionListImport();
            _service?.OnComponentChanged(_palette, null, null, null);
        }
    }

    private void OnExportClick(object? sender, EventArgs e) => _palette?.ActionListExport();

    private void OnUpgradePalette(object? sender, EventArgs e)
    {
        try
        {
            using var ofd = new OpenFileDialog(); /*KryptonOpenFileDialog*/
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.DefaultExt = @"xml";
            ofd.Filter = @"Palette files (*.xml)|*.xml|All files (*.*)|(*.*)";
            ofd.Title = @"Load Custom Palette";

            string paletteFileName = (ofd.ShowDialog() == DialogResult.OK)
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
    #endregion
}