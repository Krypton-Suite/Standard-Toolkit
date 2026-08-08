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

internal class KryptonManagerDesigner : ComponentDesigner
{
    #region Instance Fields

    private DesignerVerbCollection? _verbCollection;

    private DesignerVerb? _resetVerb;

    private DesignerVerb? _importTranslationsXmlFileVerb;

    private DesignerVerb? _exportTranslationsXmlFileVerb;

    private DesignerVerb? _importTranslationsJsonFileVerb;

    private DesignerVerb? _exportTranslationsJsonFileVerb;

    private DesignerVerb? _generateTranslationTemplateVerb;

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

        //_service.ComponentRemoving += OnComponentRemoving;

        _service!.ComponentChanged += OnComponentChanged;
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

                _importTranslationsXmlFileVerb =
                    new DesignerVerb(@"Import Translations from Xml file...", OnImportTranslationsXml);

                _exportTranslationsXmlFileVerb =
                    new DesignerVerb(@"Export Translations to Xml file...", OnExportTranslationsXml);

                _importTranslationsJsonFileVerb =
                    new DesignerVerb(@"Import Translations from Json file...", OnImportTranslationsJson);

                _exportTranslationsJsonFileVerb =
                    new DesignerVerb(@"Export Translations to Json file...", OnExportTranslationsJson);

                _generateTranslationTemplateVerb =
                    new DesignerVerb(@"Generate Translation Template...", OnGenerateTemplate);

                _verbCollection.AddRange([
                    _resetVerb, _importTranslationsXmlFileVerb, _exportTranslationsJsonFileVerb,
                    _importTranslationsXmlFileVerb, _exportTranslationsJsonFileVerb, _generateTranslationTemplateVerb
                ]);
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
            _resetVerb?.Enabled = !_manager!.GlobalPaletteMode.Equals(PaletteMode.Microsoft365Blue);
        }
    }

    private void OnComponentChanged(object? sender, ComponentChangedEventArgs e) => UpdateVerbStatus();

    private void OnComponentRemoving(object sender, ComponentEventArgs e)
    {
        ThrowHelper.ThrowNotImplementedException();
    }

    private void OnReset(object? sender, EventArgs e)
    {
        if (_manager != null)
        {
            DialogResult result = KryptonMessageBox.Show(@"This will reset the current theme back to 'Microsoft 365 - Blue'. Do you want to continue?",
                @"Reset Theme",
                KryptonMessageBoxButtons.YesNo,
                KryptonMessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                _manager.GlobalPaletteMode = PaletteMode.Microsoft365Blue;

                _service?.OnComponentChanged(_manager, null, _manager.GlobalPaletteMode, PaletteMode.Microsoft365Blue);

                UpdateVerbStatus();
            }
        }
    }

    private void OnImportTranslationsXml(object? sender, EventArgs e)
    {
        if (_manager == null)
        {
            return;
        }

        try
        {
            using var ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.FileName = @"Translations";
            ofd.DefaultExt = @"xml";
            ofd.Filter = @"Translations files (*.xml)|*.xml|All files (*.*)|(*.*)";
            ofd.Title = @"Load Translations";

            var fileName = (ofd.ShowDialog() == DialogResult.OK) ? ofd.FileName : string.Empty;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }

            _manager.ToolkitStrings.ImportFromXmlFile(fileName, resetFirst: true, refreshOpenForms: false);
            _service?.OnComponentChanged(_manager, null, null, null);
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    private void OnExportTranslationsXml(object? sender, EventArgs e)
    {
        if (_manager == null)
        {
            return;
        }

        try
        {
            using var sfd = new SaveFileDialog();
            sfd.OverwritePrompt = true;
            sfd.DefaultExt = @"xml";
            sfd.FileName = @"Translations";
            sfd.Filter = @"Translations files (*.xml)|*.xml|All files (*.*)|(*.*)";
            sfd.Title = @"Save Translations";

            var fileName = (sfd.ShowDialog() == DialogResult.OK) ? sfd.FileName : string.Empty;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }

            _manager.ToolkitStrings.ExportToXmlFile(fileName, includeDefaults: false);
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    private void OnImportTranslationsJson(object? sender, EventArgs e)
    {
        if (_manager == null)
        {
            return;
        }

        try
        {
            using var ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.FileName = @"Translations";
            ofd.DefaultExt = @"json";
            ofd.Filter = @"JSON Translations files (*.json)|*.json|All files (*.*)|(*.*)";
            ofd.Title = @"Load Translations (JSON)";

            var fileName = (ofd.ShowDialog() == DialogResult.OK) ? ofd.FileName : string.Empty;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }

            _manager.ToolkitStrings.ImportFromJsonFile(fileName, resetFirst: true, refreshOpenForms: false);
            _service?.OnComponentChanged(_manager, null, null, null);
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    private void OnExportTranslationsJson(object? sender, EventArgs e)
    {
        if (_manager == null)
        {
            return;
        }

        try
        {
            using var sfd = new SaveFileDialog();
            sfd.OverwritePrompt = true;
            sfd.DefaultExt = @"json";
            sfd.FileName = @"Translations";
            sfd.Filter = @"JSON Translations files (*.json)|*.json|All files (*.*)|(*.*)";
            sfd.Title = @"Save Translations (JSON)";

            var fileName = (sfd.ShowDialog() == DialogResult.OK) ? sfd.FileName : string.Empty;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }

            _manager.ToolkitStrings.ExportToJsonFile(fileName, includeDefaults: false);
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    private void OnGenerateTemplate(object? sender, EventArgs e)
    {
        if (_manager == null)
        {
            return;
        }

        try
        {
            using var sfd = new SaveFileDialog();
            sfd.OverwritePrompt = true;
            sfd.DefaultExt = @"xml";
            sfd.FileName = @"Translations-Template.xml";
            sfd.Filter = @"Translations files (*.xml)|*.xml|All files (*.*)|(*.*)";
            sfd.Title = @"Generate Translation Template (all strings included)";

            var fileName = (sfd.ShowDialog() == DialogResult.OK) ? sfd.FileName : string.Empty;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }

            _manager.ToolkitStrings.ExportToXmlFile(fileName, includeDefaults: true);
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    #endregion
}