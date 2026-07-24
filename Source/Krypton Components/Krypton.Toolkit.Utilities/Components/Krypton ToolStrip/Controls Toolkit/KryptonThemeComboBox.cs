#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// ToolStripComboBox that lists built-in Krypton themes and applies the selection via <see cref="ThemeManager"/>.
/// </summary>
[Description("Lists Krypton themes and applies the selected theme through a KryptonManager.")]
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip | ToolStripItemDesignerAvailability.MenuStrip)]
public class KryptonThemeToolStripComboBox : ToolStripComboBox
{
    #region Instances

    private KryptonManager _manager = new KryptonManager();

    #endregion

    #region Properties

    /// <summary>Gets or sets the <see cref="KryptonManager"/> used when applying a theme.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonManager KryptonManager
    {
        get => _manager;
        set => _manager = value ?? new KryptonManager();
    }

    #endregion

    #region Identity

    public KryptonThemeToolStripComboBox()
    {
        Size = new Size(200, 23);
        DropDownWidth = 200;
        DropDownStyle = ComboBoxStyle.DropDownList;
        AutoCompleteSource = AutoCompleteSource.ListItems;
        AutoCompleteMode = AutoCompleteMode.SuggestAppend;

        PopulateThemes();
    }

    #endregion

    #region Implementation

    /// <summary>Clears and repopulates the combo with supported internal theme names.</summary>
    public void PopulateThemes()
    {
        Items.Clear();

        foreach (string themeName in ThemeManager.SupportedInternalThemeNames)
        {
            Items.Add(themeName);
        }

        string current = ThemeManager.ReturnPaletteModeAsString(_manager.GlobalPaletteMode);
        int index = Items.IndexOf(current);
        if (index >= 0)
        {
            SelectedIndex = index;
        }
        else if (Items.Count > 0)
        {
            SelectedIndex = 0;
        }
    }

    #endregion

    #region Protected

    protected override void OnSelectedIndexChanged(EventArgs e)
    {
        if (!string.IsNullOrEmpty(Text))
        {
            ThemeManager.ApplyTheme(Text, KryptonManager);
        }

        base.OnSelectedIndexChanged(e);
    }

    #endregion
}
