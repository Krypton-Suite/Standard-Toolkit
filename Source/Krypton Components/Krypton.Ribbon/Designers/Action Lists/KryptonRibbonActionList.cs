#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

namespace Krypton.Ribbon;

internal class KryptonRibbonActionList : DesignerActionList
{
    #region Instance Fields
    private readonly KryptonRibbon _ribbon;
    private readonly IComponentChangeService? _service;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonRibbonActionList class.
    /// </summary>
    /// <param name="owner">Designer that owns this action list instance.</param>
    public KryptonRibbonActionList(KryptonRibbonDesigner owner)
        : base(owner.Component)
    {
        // Remember the ribbon instance
        _ribbon = (owner.Component as KryptonRibbon)!;

        // Cache service used to notify when a property has changed
        _service = GetService(typeof(IComponentChangeService)) as IComponentChangeService;
    }
    #endregion

    #region Public

    /// <summary>
    /// Gets and sets use of design time helpers.
    /// </summary>
    public bool InDesignHelperMode
    {
        get => _ribbon.InDesignHelperMode;
        set => _ribbon.InDesignHelperMode = value;
    }

    /// <summary>
    /// Gets and sets whether ribbon tab headers are visible.
    /// </summary>
    public bool ShowTabHeaders
    {
        get => _ribbon.ShowTabHeaders;

        set
        {
            if (_ribbon.ShowTabHeaders != value)
            {
                _service?.OnComponentChanged(_ribbon, null, _ribbon.ShowTabHeaders, value);
                _ribbon.ShowTabHeaders = value;
            }
        }
    }

    /// <summary>
    /// Gets and sets the palette mode.
    /// </summary>
    public PaletteMode PaletteMode
    {
        get => _ribbon.PaletteMode;

        set
        {
            if (_ribbon.PaletteMode != value)
            {
                _service?.OnComponentChanged(_ribbon, null, _ribbon.PaletteMode, value);
                _ribbon.PaletteMode = value;
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

        // This can be null when deleting a control instance at design time
        if (_ribbon != null)
        {
            // Add the list of button specific actions
            actions.Add(new DesignerActionHeaderItem("Design"));
            actions.Add(new DesignerActionPropertyItem(nameof(InDesignHelperMode), "Design Helpers", "Design", "Show design time helpers for creating items."));
            actions.Add(new DesignerActionHeaderItem("Translations"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Import Ribbon Translations from Xml file...", OnImportTranslationsXml), @"Translations"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Export Ribbon Translations to Xml file...", OnExportTranslationsXml), @"Translations"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Import Ribbon Translations from Json file...", OnImportTranslationsJson), @"Translations"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Export Ribbon Translations to Json file...", OnExportTranslationsJson), @"Translations"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Generate Ribbon Translation Template (XML)...", OnGenerateTemplateXml), @"Translations"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Generate Ribbon Translation Template (JSON)...", OnGenerateTemplateJson), @"Translations"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Merge Missing Ribbon Translations...", OnMergeMissingTranslations), @"Translations"));
            actions.Add(new KryptonDesignerActionItem(new DesignerVerb(@"Switch Ribbon Translations Culture...", OnSwitchTranslationsCulture), @"Translations"));
            actions.Add(new DesignerActionHeaderItem("Visuals"));
            actions.Add(new DesignerActionPropertyItem(nameof(ShowTabHeaders), "Show Tab Headers", "Visuals", "Shows or hides the ribbon tab headers (toolbar mode when false)."));
            actions.Add(new DesignerActionPropertyItem(nameof(PaletteMode), "Palette", "Visuals", "Palette applied to drawing"));
        }

        return actions;
    }
    #endregion

    #region Implementation
    private RibbonTranslationOptions CreateDesignerOptions(bool includeDefaults, bool resetFirst) =>
        new RibbonTranslationOptions
        {
            IncludeDefaults = includeDefaults,
            IncludeChrome = false,
            ResetFirst = resetFirst,
            ChangeService = _service
        };

    private void RunDesignerTransaction(string description, Action action)
    {
        var host = GetService(typeof(IDesignerHost)) as IDesignerHost;
        DesignerTransaction? transaction = null;
        try
        {
            transaction = host?.CreateTransaction(description);
            action();
            _service?.OnComponentChanged(_ribbon, null, null, null);
            transaction?.Commit();
            transaction = null;
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
        finally
        {
            transaction?.Cancel();
        }
    }

    private void OnImportTranslationsXml(object? sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog();
        ofd.CheckFileExists = true;
        ofd.CheckPathExists = true;
        ofd.FileName = @"RibbonTranslations";
        ofd.DefaultExt = @"xml";
        ofd.Filter = @"Ribbon translations (*.xml)|*.xml|All files (*.*)|(*.*)";
        ofd.Title = @"Load Ribbon Translations";
        if (ofd.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        RunDesignerTransaction(@"Import Ribbon Translations (XML)", () =>
            _ribbon.ImportTranslationsFromXmlFile(ofd.FileName, CreateDesignerOptions(includeDefaults: false, resetFirst: true)));
    }

    private void OnExportTranslationsXml(object? sender, EventArgs e)
    {
        try
        {
            using var sfd = new SaveFileDialog();
            sfd.OverwritePrompt = true;
            sfd.DefaultExt = @"xml";
            sfd.FileName = @"RibbonTranslations";
            sfd.Filter = @"Ribbon translations (*.xml)|*.xml|All files (*.*)|(*.*)";
            sfd.Title = @"Save Ribbon Translations";
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _ribbon.ExportTranslationsToXmlFile(sfd.FileName, CreateDesignerOptions(includeDefaults: false, resetFirst: false));
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    private void OnImportTranslationsJson(object? sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog();
        ofd.CheckFileExists = true;
        ofd.CheckPathExists = true;
        ofd.FileName = @"RibbonTranslations";
        ofd.DefaultExt = @"json";
        ofd.Filter = @"JSON ribbon translations (*.json)|*.json|All files (*.*)|(*.*)";
        ofd.Title = @"Load Ribbon Translations (JSON)";
        if (ofd.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        RunDesignerTransaction(@"Import Ribbon Translations (JSON)", () =>
            _ribbon.ImportTranslationsFromJsonFile(ofd.FileName, CreateDesignerOptions(includeDefaults: false, resetFirst: true)));
    }

    private void OnExportTranslationsJson(object? sender, EventArgs e)
    {
        try
        {
            using var sfd = new SaveFileDialog();
            sfd.OverwritePrompt = true;
            sfd.DefaultExt = @"json";
            sfd.FileName = @"RibbonTranslations";
            sfd.Filter = @"JSON ribbon translations (*.json)|*.json|All files (*.*)|(*.*)";
            sfd.Title = @"Save Ribbon Translations (JSON)";
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _ribbon.ExportTranslationsToJsonFile(sfd.FileName, CreateDesignerOptions(includeDefaults: false, resetFirst: false));
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    private void OnGenerateTemplateXml(object? sender, EventArgs e)
    {
        try
        {
            using var sfd = new SaveFileDialog();
            sfd.OverwritePrompt = true;
            sfd.DefaultExt = @"xml";
            sfd.FileName = @"RibbonTranslations-Template.xml";
            sfd.Filter = @"Ribbon translations (*.xml)|*.xml|All files (*.*)|(*.*)";
            sfd.Title = @"Generate Ribbon Translation Template (XML)";
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _ribbon.ExportTranslationsToXmlFile(sfd.FileName, CreateDesignerOptions(includeDefaults: true, resetFirst: false));
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    private void OnGenerateTemplateJson(object? sender, EventArgs e)
    {
        try
        {
            using var sfd = new SaveFileDialog();
            sfd.OverwritePrompt = true;
            sfd.DefaultExt = @"json";
            sfd.FileName = @"RibbonTranslations-Template.json";
            sfd.Filter = @"JSON ribbon translations (*.json)|*.json|All files (*.*)|(*.*)";
            sfd.Title = @"Generate Ribbon Translation Template (JSON)";
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _ribbon.ExportTranslationsToJsonFile(sfd.FileName, CreateDesignerOptions(includeDefaults: true, resetFirst: false));
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    private void OnMergeMissingTranslations(object? sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog();
        ofd.CheckFileExists = true;
        ofd.CheckPathExists = true;
        ofd.FileName = @"RibbonTranslations";
        ofd.Filter = @"Ribbon translations (*.xml;*.json)|*.xml;*.json|XML (*.xml)|*.xml|JSON (*.json)|*.json|All files (*.*)|(*.*)";
        ofd.Title = @"Merge Missing Ribbon Translations";
        if (ofd.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        RunDesignerTransaction(@"Merge Missing Ribbon Translations", () =>
            _ribbon.MergeMissingTranslationsToFile(ofd.FileName, CreateDesignerOptions(includeDefaults: true, resetFirst: true)));
    }

    private void OnSwitchTranslationsCulture(object? sender, EventArgs e)
    {
        try
        {
            using var dialog = new VisualSwitchTranslationsCultureForm();
            if (dialog.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.SelectedCultureName))
            {
                return;
            }

            var loaded = false;
            RunDesignerTransaction(@"Switch Ribbon Translations Culture", () =>
            {
                loaded = _ribbon.TrySwitchTranslationsCulture(dialog.SelectedCultureName, dialog.SelectedDirectory);
            });

            KryptonMessageBox.Show(
                loaded
                    ? $@"Switched designer culture to '{dialog.SelectedCultureName}' and loaded matching ribbon translations."
                    : $@"Switched designer culture to '{dialog.SelectedCultureName}'. No matching RibbonTranslations file was found; instance captions were left unchanged.",
                @"Switch Ribbon Translations Culture",
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