#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Manual repro for Issue #4046: <see cref="KryptonDataGridView"/> should not show a
/// non-functional themed scrollbar when external corner rounding is not enabled.
/// </summary>
public partial class Bug4046DataGridViewScrollbarDemo : KryptonForm
{
    public Bug4046DataGridViewScrollbarDemo()
    {
        InitializeComponent();
    }

    private void Bug4046DataGridViewScrollbarDemo_Load(object? sender, EventArgs e)
    {
        kdgvRounded.StateNormal.Border.Rounding = 8f;
        kdgvRounded.AutoGenerateColumns = true;
        kdgvRounded.DataSource = CreateSampleRows(40);

        kdgvDefault.AutoGenerateColumns = true;
        kdgvDefault.DataSource = CreateSampleRows(3);
    }

    private static List<SampleRow> CreateSampleRows(int count)
    {
        var rows = new List<SampleRow>(count);
        for (int i = 0; i < count; i++)
        {
            rows.Add(new SampleRow(i + 1, $"Row {i + 1}", $"Value {i + 1}"));
        }

        return rows;
    }

    private sealed class SampleRow
    {
        public SampleRow(int id, string name, string value)
        {
            Id = id;
            Name = name;
            Value = value;
        }

        public int Id { get; }

        public string Name { get; }

        public string Value { get; }
    }
}
