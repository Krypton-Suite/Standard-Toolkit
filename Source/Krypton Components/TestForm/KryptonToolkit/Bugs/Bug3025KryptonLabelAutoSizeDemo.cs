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
/// Comprehensive demo for Issue #3025: KryptonLabel with AutoSize not working in the designer.
/// When drawing a KryptonLabel by click-drag in the WinForms Designer, the label now resizes
/// to fit its text (when AutoSize = true), matching standard Label behavior.
/// This form demonstrates: AutoSize on/off, various LabelStyles, short/long text, and text + image.
/// </summary>
public partial class Bug3025KryptonLabelAutoSizeDemo : KryptonForm
{
    public Bug3025KryptonLabelAutoSizeDemo()
    {
        InitializeComponent();
        Load += Bug3025KryptonLabelAutoSizeDemo_Load;
    }

    private void Bug3025KryptonLabelAutoSizeDemo_Load(object? sender, EventArgs e)
    {
        // Set a small image on the "Text + image" label so AutoSize includes image in preferred size
        const int size = 16;
        using var bmp = new Bitmap(size, size);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Transparent);
            g.DrawIcon(SystemIcons.Information, new Rectangle(0, 0, size, size));
        }
        kryptonLabelWithImage.Values.Image = (Image)bmp.Clone();
    }
}
