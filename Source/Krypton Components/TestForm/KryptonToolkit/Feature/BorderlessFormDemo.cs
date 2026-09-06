#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Demo for Issue #2922: Borderless KryptonForm startup without system title bar flicker.
/// </summary>
public partial class BorderlessFormDemo : KryptonForm
{
    public BorderlessFormDemo()
    {
        InitializeComponent();
    }

    private void kbtnOpenAnother_Click(object? sender, EventArgs e)
    {
        var another = new BorderlessFormDemo
        {
            Text = $"Borderless Form Demo #{DateTime.Now:HHmmss}",
            StartPosition = FormStartPosition.Manual,
            Location = new Point(Location.X + 40, Location.Y + 40),
        };
        another.Show();
    }

    private void kbtnClose_Click(object? sender, EventArgs e)
    {
        Close();
    }
}