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

internal class KryptonManagerActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonManager? _manager;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonManagerActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonManagerActionList(KryptonManagerDesigner owner)
        : base(owner.Component)
    {
        // Remember the panel instance
        _manager = (owner.Component as KryptonManager)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the global palette mode.
    /// </summary>
    public PaletteMode GlobalPaletteMode
    {
        get => _manager!.GlobalPaletteMode;

        set
        {
            if (_manager != null && _manager.GlobalPaletteMode != value)
            {
                _service?.OnComponentChanged(_manager, null, _manager.GlobalPaletteMode, value);
                _manager.GlobalPaletteMode = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the designer UI culture used for toolkit translations preview.
    /// </summary>
    [TypeConverter(typeof(KryptonTranslationsCultureNameConverter))]
    public string TranslationsCulture
    {
        get => KryptonManager.ActiveTranslationsCulture?.Name
               ?? CultureInfo.CurrentUICulture.Name;

        set
        {
            if (_manager == null || string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            if (string.Equals(TranslationsCulture, value, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            KryptonManager.TrySwitchTranslationsCulture(value, refreshOpenForms: false);
            _service?.OnComponentChanged(_manager, null, null, null);
        }
    }

    /// <summary>
    /// Gets and sets whether matching toolkit strings prefer text from the installed Windows language pack.
    /// </summary>
    [Category(@"Visuals")]
    [Description(@"When true, matching dialog and Explorer-style strings use the installed Windows language pack.")]
    [DefaultValue(false)]
    public bool UseWindowsLanguagePackStrings
    {
        get => _manager?.ToolkitStrings.UseWindowsLanguagePackStrings ?? false;

        set
        {
            if (_manager == null || _manager.ToolkitStrings.UseWindowsLanguagePackStrings == value)
            {
                return;
            }

            _service?.OnComponentChanged(_manager, null, _manager.ToolkitStrings.UseWindowsLanguagePackStrings, value);
            _manager.ToolkitStrings.UseWindowsLanguagePackStrings = value;
        }
    }

    #endregion

    #region Implementation

    private void OnReset(object? sender, EventArgs e)
    {
        if (_manager != null)
        {
            DialogResult result = KryptonMessageBox.Show(
                @"This will reset the current theme back to 'Microsoft 365 - Blue'. Do you want to continue?",
                @"Reset Theme",
                KryptonMessageBoxButtons.YesNo,
                KryptonMessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                _manager.GlobalPaletteMode = PaletteMode.Microsoft365Blue;

                _service?.OnComponentChanged(_manager, null, _manager.GlobalPaletteMode, PaletteMode.Microsoft365Blue);

                //UpdateVerbStatus();
            }
        }
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
        if (_manager != null)
        {
            // Add the list of panel specific actions
            actions.Add(new DesignerActionHeaderItem(@"Actions"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Reset to Default Theme", OnReset), @"Actions"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Designer Editor Settings...", OnDesignerEditorSettings), @"Actions"));
            
            /*actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Add language manager", OnAddLanguageManager), "Actions"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Remove language manager", OnRemoveLanguageManager), "Actions"));
            actions.Add(new DesignerActionHeaderItem(@"Data"));*/
            actions.Add(new DesignerActionHeaderItem(@"Translations"));
            actions.Add(new DesignerActionPropertyItem(nameof(TranslationsCulture), @"UI Culture", @"Translations",
                @"Switch the designer UI culture and load matching Translations.{culture}.* files with graceful fallback."));
            actions.Add(new DesignerActionPropertyItem(nameof(UseWindowsLanguagePackStrings), @"Use Windows Language Pack", @"Translations",
                @"When enabled, matching dialog buttons and Explorer column headers use strings from the installed Windows language pack."));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Import Translations from Xml file...", OnImportTranslationsXml), @"Translations"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Export Translations to Xml file...", OnExportTranslationsXml), @"Translations"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Import Translations from Json file...", OnImportTranslationsJson), @"Translations"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Export Translations to Json file...", OnExportTranslationsJson), @"Translations"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Generate Translation Template (XML)...", OnGenerateTemplateXml), @"Translations"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Generate Translation Template (JSON)...", OnGenerateTemplateJson), @"Translations"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Merge Missing Translations...", OnMergeMissingTranslations), @"Translations"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Switch Translations Culture...", OnSwitchTranslationsCulture), @"Translations"));
            actions.Add(new DesignerActionHeaderItem(@"Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(GlobalPaletteMode), @"Global Palette", @"Visuals", @"Global palette setting"));
        }

        return actions;
    }

    private void OnDesignerEditorSettings(object? sender, EventArgs e) =>
        KryptonDesignerEditorTheme.ShowSettingsDialog();

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

    private void OnGenerateTemplateXml(object? sender, EventArgs e)
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
            sfd.Title = @"Generate Translation Template (XML — all strings included)";

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

    private void OnGenerateTemplateJson(object? sender, EventArgs e)
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
            sfd.FileName = @"Translations-Template.json";
            sfd.Filter = @"JSON Translations files (*.json)|*.json|All files (*.*)|(*.*)";
            sfd.Title = @"Generate Translation Template (JSON — all strings included)";

            var fileName = (sfd.ShowDialog() == DialogResult.OK) ? sfd.FileName : string.Empty;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }

            _manager.ToolkitStrings.ExportToJsonFile(fileName, includeDefaults: true);
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    private void OnMergeMissingTranslations(object? sender, EventArgs e)
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
            ofd.Filter = @"Translations files (*.xml;*.json)|*.xml;*.json|XML (*.xml)|*.xml|JSON (*.json)|*.json|All files (*.*)|(*.*)";
            ofd.Title = @"Merge Missing Translations into File";

            var fileName = (ofd.ShowDialog() == DialogResult.OK) ? ofd.FileName : string.Empty;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }

            var before = _manager.ToolkitStrings.AnalyzeTranslationsFromFile(fileName);
            var after = _manager.ToolkitStrings.MergeMissingTranslationsToFile(fileName, includeDefaults: true);
            _service?.OnComponentChanged(_manager, null, null, null);

            KryptonMessageBox.Show(
                $@"Merged '{fileName}'.{Environment.NewLine}" +
                $@"Previously missing: {before.MissingInFile.Count}{Environment.NewLine}" +
                $@"Extra (ignored): {before.ExtraInFile.Count}{Environment.NewLine}" +
                $@"After merge missing: {after.MissingInFile.Count}",
                @"Merge Missing Translations",
                KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Information);
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    private void OnSwitchTranslationsCulture(object? sender, EventArgs e)
    {
        if (_manager == null)
        {
            return;
        }

        try
        {
            using var dialog = new VisualSwitchTranslationsCultureForm();
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(dialog.SelectedCultureName))
            {
                return;
            }

            var loaded = KryptonManager.TrySwitchTranslationsCulture(
                dialog.SelectedCultureName,
                dialog.SelectedDirectory,
                refreshOpenForms: false);

            _service?.OnComponentChanged(_manager, null, null, null);

            KryptonMessageBox.Show(
                loaded
                    ? $@"Switched designer culture to '{dialog.SelectedCultureName}' and loaded matching translations."
                    : $@"Switched designer culture to '{dialog.SelectedCultureName}'. No matching translations file was found; built-in defaults were restored.",
                @"Switch Translations Culture",
                KryptonMessageBoxButtons.OK,
                loaded ? KryptonMessageBoxIcon.Information : KryptonMessageBoxIcon.Warning);
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }
    #endregion
}
