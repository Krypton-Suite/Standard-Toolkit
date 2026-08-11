#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Designer dialog used to pick a UI culture and optional translations directory.
/// </summary>
internal sealed partial class VisualSwitchTranslationsCultureForm : KryptonForm
{
    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="VisualSwitchTranslationsCultureForm"/> class.
    /// </summary>
    public VisualSwitchTranslationsCultureForm()
    {
        SetInheritedControlOverride();
        InitializeComponent();
        ApplyLocalizedText();
        PopulateCultures();
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the culture name entered or selected by the user.
    /// </summary>
    public string SelectedCultureName => kcmbCulture.Text.Trim();

    /// <summary>
    /// Gets the translations directory entered by the user, or <c>null</c> when blank.
    /// </summary>
    public string? SelectedDirectory
    {
        get
        {
            var directory = ktxtDirectory.Text.Trim();
            return string.IsNullOrWhiteSpace(directory) ? null : directory;
        }
    }

    #endregion

    #region Implementation

    private void ApplyLocalizedText()
    {
        var general = KryptonManager.Strings.GeneralStrings;

        Text = @"Switch Translations Culture";
        klblCulture.Values.Text = @"Culture:";
        klblDirectory.Values.Text = @"Directory:";
        kbtnBrowse.Values.Text = @"Browse...";
        kbtnOk.Values.Text = general.OK;
        kbtnCancel.Values.Text = general.Cancel;
    }

    private void PopulateCultures()
    {
        kcmbCulture.Items.AddRange(new object[]
        {
            @"en-US", @"en-GB", @"de-DE", @"fr-FR", @"es-ES",
            @"it-IT", @"pt-BR", @"ja-JP", @"zh-CN", @"ko-KR"
        });

        kcmbCulture.Text = KryptonManager.ActiveTranslationsCulture?.Name
                           ?? CultureInfo.CurrentUICulture.Name;

        ktxtDirectory.Text = AppDomain.CurrentDomain.BaseDirectory ?? string.Empty;
    }

    private void kbtnBrowse_Click(object? sender, EventArgs e)
    {
        using var folderDialog = new KryptonFolderBrowserDialog
        {
            Title = @"Select the folder containing Translations.{culture}.* files",
            SelectedPath = ktxtDirectory.Text
        };

        if (folderDialog.ShowDialog(this) == DialogResult.OK)
        {
            ktxtDirectory.Text = folderDialog.SelectedPath;
        }
    }

    #endregion
}
