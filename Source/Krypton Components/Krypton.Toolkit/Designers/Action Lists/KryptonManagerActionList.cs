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

    private void OnReset(object? sender, EventArgs e) =>
        KryptonManagerDesignerActions.ResetTheme(_manager, _service);

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
        KryptonManagerDesignerActions.ShowDesignerEditorSettings();

    private void OnImportTranslationsXml(object? sender, EventArgs e) =>
        KryptonManagerDesignerActions.ImportTranslationsXml(_manager, _service);

    private void OnExportTranslationsXml(object? sender, EventArgs e) =>
        KryptonManagerDesignerActions.ExportTranslationsXml(_manager);

    private void OnImportTranslationsJson(object? sender, EventArgs e) =>
        KryptonManagerDesignerActions.ImportTranslationsJson(_manager, _service);

    private void OnExportTranslationsJson(object? sender, EventArgs e) =>
        KryptonManagerDesignerActions.ExportTranslationsJson(_manager);

    private void OnGenerateTemplateXml(object? sender, EventArgs e) =>
        KryptonManagerDesignerActions.GenerateTemplateXml(_manager);

    private void OnGenerateTemplateJson(object? sender, EventArgs e) =>
        KryptonManagerDesignerActions.GenerateTemplateJson(_manager);

    private void OnMergeMissingTranslations(object? sender, EventArgs e) =>
        KryptonManagerDesignerActions.MergeMissingTranslations(_manager, _service);

    private void OnSwitchTranslationsCulture(object? sender, EventArgs e) =>
        KryptonManagerDesignerActions.SwitchTranslationsCulture(_manager, _service);
    #endregion
}
