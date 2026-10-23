#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Manual test for Issue #3283: <see cref="Krypton.Toolkit.KryptonThemeComboBox"/> must apply the global
/// palette when <see cref="System.Windows.Forms.ComboBox.SelectedIndex"/> is set in code, not only when
/// the user picks from the drop-down.
/// </summary>
public partial class Bug3283ThemeComboBoxProgrammaticTest : KryptonForm
{
    public Bug3283ThemeComboBoxProgrammaticTest()
    {
        InitializeComponent();
        kryptonThemeComboBox1.SelectedIndexChanged += (_, _) => RefreshStatusLabels();
        kryptonButtonCycle.Click += (_, _) => CycleSelectedIndex();
        kryptonButtonJumpLow.Click += (_, _) => JumpToRelativeIndex(0.25);
        kryptonButtonJumpHigh.Click += (_, _) => JumpToRelativeIndex(0.75);
        kryptonButtonPreHandle.Click += (_, _) => AddComboWithProgrammaticIndexBeforeHost();
        Load += (_, _) => RefreshStatusLabels();
    }

    private void RefreshStatusLabels()
    {
        object? item = kryptonThemeComboBox1.SelectedItem;
        kryptonLabelSelection.Values.Text = $"SelectedIndex = {kryptonThemeComboBox1.SelectedIndex}, SelectedItem = {item?.ToString() ?? "(null)"}";
        kryptonLabelGlobalMode.Values.Text = $"KryptonManager.CurrentGlobalPaletteMode = {KryptonManager.CurrentGlobalPaletteMode}";
    }

    private void CycleSelectedIndex()
    {
        int count = kryptonThemeComboBox1.Items.Count;
        if (count == 0)
        {
            return;
        }

        int i = kryptonThemeComboBox1.SelectedIndex;
        i = i < 0 ? 0 : (i + 1) % count;
        kryptonThemeComboBox1.SelectedIndex = i;
    }

    private void JumpToRelativeIndex(double fraction)
    {
        int count = kryptonThemeComboBox1.Items.Count;
        if (count == 0)
        {
            return;
        }

        int idx = (int)(fraction * (count - 1));
        idx = Math.Max(0, Math.Min(count - 1, idx));
        kryptonThemeComboBox1.SelectedIndex = idx;
    }

    private void AddComboWithProgrammaticIndexBeforeHost()
    {
        kryptonPanelHost.Controls.Clear();

        var combo = new Krypton.Toolkit.KryptonThemeComboBox();
        int count = combo.Items.Count;
        if (count == 0)
        {
            return;
        }

        int idx = Math.Min(4, count - 1);
        combo.SelectedIndex = idx;
        combo.Dock = DockStyle.Fill;
        kryptonPanelHost.Controls.Add(combo);
        kryptonLabelPreHandleResult.Values.Text =
            $"Added a new {nameof(Krypton.Toolkit.KryptonThemeComboBox)} with SelectedIndex = {idx} before its handle existed; the app theme should match that entry.";
        RefreshStatusLabels();
    }
}
