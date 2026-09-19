#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Shared designer-verb and smart-tag handlers for <see cref="KryptonManager"/>.
/// </summary>
internal static class KryptonManagerDesignerActions
{
    /// <summary>
    /// Resets <see cref="KryptonManager.GlobalPaletteMode"/> to Microsoft 365 Blue after confirmation.
    /// </summary>
    /// <param name="manager">Manager instance being designed.</param>
    /// <param name="service">Change service used to notify the designer.</param>
    public static void ResetTheme(KryptonManager? manager, IComponentChangeService? service)
    {
        if (manager == null)
        {
            return;
        }

        DialogResult result = KryptonMessageBox.Show(
            @"This will reset the current theme back to 'Microsoft 365 - Blue'. Do you want to continue?",
            @"Reset Theme",
            KryptonMessageBoxButtons.YesNo,
            KryptonMessageBoxIcon.Question);

        if (result != DialogResult.Yes)
        {
            return;
        }

        manager.GlobalPaletteMode = PaletteMode.Microsoft365Blue;
        service?.OnComponentChanged(manager, null, manager.GlobalPaletteMode, PaletteMode.Microsoft365Blue);
    }

    /// <summary>
    /// Opens the designer editor theme settings dialog.
    /// </summary>
    public static void ShowDesignerEditorSettings() => KryptonDesignerEditorTheme.ShowSettingsDialog();

    /// <summary>
    /// Imports toolkit strings from an XML translations file.
    /// </summary>
    /// <param name="manager">Manager instance being designed.</param>
    /// <param name="service">Change service used to notify the designer.</param>
    public static void ImportTranslationsXml(KryptonManager? manager, IComponentChangeService? service) =>
        ImportTranslations(manager, service, xml: true);

    /// <summary>
    /// Exports toolkit strings to an XML translations file.
    /// </summary>
    /// <param name="manager">Manager instance being designed.</param>
    public static void ExportTranslationsXml(KryptonManager? manager) =>
        ExportTranslations(manager, xml: true, includeDefaults: false);

    /// <summary>
    /// Imports toolkit strings from a JSON translations file.
    /// </summary>
    /// <param name="manager">Manager instance being designed.</param>
    /// <param name="service">Change service used to notify the designer.</param>
    public static void ImportTranslationsJson(KryptonManager? manager, IComponentChangeService? service) =>
        ImportTranslations(manager, service, xml: false);

    /// <summary>
    /// Exports toolkit strings to a JSON translations file.
    /// </summary>
    /// <param name="manager">Manager instance being designed.</param>
    public static void ExportTranslationsJson(KryptonManager? manager) =>
        ExportTranslations(manager, xml: false, includeDefaults: false);

    /// <summary>
    /// Writes an XML template that includes every toolkit string default.
    /// </summary>
    /// <param name="manager">Manager instance being designed.</param>
    public static void GenerateTemplateXml(KryptonManager? manager) =>
        ExportTranslations(manager, xml: true, includeDefaults: true, fileName: @"Translations-Template.xml",
            title: @"Generate Translation Template (XML — all strings included)");

    /// <summary>
    /// Writes a JSON template that includes every toolkit string default.
    /// </summary>
    /// <param name="manager">Manager instance being designed.</param>
    public static void GenerateTemplateJson(KryptonManager? manager) =>
        ExportTranslations(manager, xml: false, includeDefaults: true, fileName: @"Translations-Template.json",
            title: @"Generate Translation Template (JSON — all strings included)");

    /// <summary>
    /// Merges missing toolkit strings into an existing translations file.
    /// </summary>
    /// <param name="manager">Manager instance being designed.</param>
    /// <param name="service">Change service used to notify the designer.</param>
    public static void MergeMissingTranslations(KryptonManager? manager, IComponentChangeService? service)
    {
        if (manager == null)
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

            var fileName = ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : string.Empty;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }

            var before = manager.ToolkitStrings.AnalyzeTranslationsFromFile(fileName);
            var after = manager.ToolkitStrings.MergeMissingTranslationsToFile(fileName, includeDefaults: true);
            service?.OnComponentChanged(manager, null, null, null);

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

    /// <summary>
    /// Switches the designer translations culture and loads matching files.
    /// </summary>
    /// <param name="manager">Manager instance being designed.</param>
    /// <param name="service">Change service used to notify the designer.</param>
    public static void SwitchTranslationsCulture(KryptonManager? manager, IComponentChangeService? service)
    {
        if (manager == null)
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

            service?.OnComponentChanged(manager, null, null, null);

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

    private static void ImportTranslations(KryptonManager? manager, IComponentChangeService? service, bool xml)
    {
        if (manager == null)
        {
            return;
        }

        try
        {
            using var ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.FileName = @"Translations";
            ofd.DefaultExt = xml ? @"xml" : @"json";
            ofd.Filter = xml
                ? @"Translations files (*.xml)|*.xml|All files (*.*)|(*.*)"
                : @"JSON Translations files (*.json)|*.json|All files (*.*)|(*.*)";
            ofd.Title = xml ? @"Load Translations" : @"Load Translations (JSON)";

            var fileName = ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : string.Empty;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }

            if (xml)
            {
                manager.ToolkitStrings.ImportFromXmlFile(fileName, resetFirst: true, refreshOpenForms: false);
            }
            else
            {
                manager.ToolkitStrings.ImportFromJsonFile(fileName, resetFirst: true, refreshOpenForms: false);
            }

            service?.OnComponentChanged(manager, null, null, null);
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }

    private static void ExportTranslations(
        KryptonManager? manager,
        bool xml,
        bool includeDefaults,
        string? fileName = null,
        string? title = null)
    {
        if (manager == null)
        {
            return;
        }

        try
        {
            using var sfd = new SaveFileDialog();
            sfd.OverwritePrompt = true;
            sfd.DefaultExt = xml ? @"xml" : @"json";
            sfd.FileName = fileName ?? @"Translations";
            sfd.Filter = xml
                ? @"Translations files (*.xml)|*.xml|All files (*.*)|(*.*)"
                : @"JSON Translations files (*.json)|*.json|All files (*.*)|(*.*)";
            sfd.Title = title
                        ?? (xml
                            ? (includeDefaults ? @"Generate Translation Template (all strings included)" : @"Save Translations")
                            : @"Save Translations (JSON)");

            var path = sfd.ShowDialog() == DialogResult.OK ? sfd.FileName : string.Empty;
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            if (xml)
            {
                manager.ToolkitStrings.ExportToXmlFile(path, includeDefaults);
            }
            else
            {
                manager.ToolkitStrings.ExportToJsonFile(path, includeDefaults);
            }
        }
        catch (Exception exc)
        {
            KryptonExceptionHandler.CaptureException(exc, showStackTrace: SharedStaticConstants.DEFAULT_USE_STACK_TRACE);
        }
    }
}
