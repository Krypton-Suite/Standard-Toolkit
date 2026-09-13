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
        if (_service != null)
        {
            _service.ComponentChanged += OnComponentChanged;
        }
    }

    /// <summary>
    ///  Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionLists = new DesignerActionListCollection
            {
                new KryptonManagerActionList(this)
            };

            return actionLists;
        }
    }

    /// <summary>
    /// Gets the design-time verbs shown on the component context menu.
    /// </summary>
    public override DesignerVerbCollection Verbs
    {
        get
        {
            if (_verbCollection == null)
            {
                _resetVerb = new DesignerVerb(@"Reset to Default Theme", OnReset);
                _verbCollection = new DesignerVerbCollection
                {
                    _resetVerb,
                    new DesignerVerb(@"Designer Editor Settings...", OnDesignerEditorSettings),
                    new DesignerVerb(@"Import Translations from Xml file...", OnImportTranslationsXml),
                    new DesignerVerb(@"Export Translations to Xml file...", OnExportTranslationsXml),
                    new DesignerVerb(@"Import Translations from Json file...", OnImportTranslationsJson),
                    new DesignerVerb(@"Export Translations to Json file...", OnExportTranslationsJson),
                    new DesignerVerb(@"Generate Translation Template (XML)...", OnGenerateTemplateXml),
                    new DesignerVerb(@"Generate Translation Template (JSON)...", OnGenerateTemplateJson),
                    new DesignerVerb(@"Merge Missing Translations...", OnMergeMissingTranslations),
                    new DesignerVerb(@"Switch Translations Culture...", OnSwitchTranslationsCulture)
                };
            }

            UpdateVerbStatus();
            return _verbCollection;
        }
    }

    #endregion

    #region Protected

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        try
        {
            if (disposing && _service != null)
            {
                _service.ComponentChanged -= OnComponentChanged;
            }
        }
        finally
        {
            base.Dispose(disposing);
        }
    }

    #endregion

    #region Implementation

    private void UpdateVerbStatus() =>
        _resetVerb?.Enabled = _manager != null && !_manager.GlobalPaletteMode.Equals(PaletteMode.Microsoft365Blue);

    private void OnComponentChanged(object? sender, ComponentChangedEventArgs e) => UpdateVerbStatus();

    private void OnReset(object? sender, EventArgs e) =>
        KryptonManagerDesignerActions.ResetTheme(_manager, _service);

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
