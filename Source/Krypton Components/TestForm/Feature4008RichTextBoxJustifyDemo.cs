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
/// Demo for Issue #4008: KryptonRichTextBox.SelectionParagraphAlignment including full justify.
/// Native WinForms RichTextBox.SelectionAlignment has no Justify value; Krypton exposes it via RichEdit.
/// </summary>
public partial class Feature4008RichTextBoxJustifyDemo : KryptonForm
{
    private const string SampleParagraph =
        "The quick brown fox jumps over the lazy dog. Justified text stretches soft spaces so each " +
        "full line meets both margins, similar to a word processor. Short final lines stay left-aligned.\r\n\r\n" +
        "Select a paragraph or the whole document, then use the alignment buttons. With Justify, word " +
        "spacing should redistribute so lines fill the control width. Left, Center, and Right should match " +
        "the usual RichTextBox behaviour.";

    public Feature4008RichTextBoxJustifyDemo()
    {
        InitializeComponent();
    }

    private void Feature4008RichTextBoxJustifyDemo_Load(object? sender, EventArgs e)
    {
        krtbDemo.Text = SampleParagraph;
        rtbNative.Text = SampleParagraph;
        UpdateStatus();
    }

    private void kbtnLeft_Click(object? sender, EventArgs e) =>
        ApplyKryptonAlignment(RichTextParagraphAlignment.Left);

    private void kbtnCenter_Click(object? sender, EventArgs e) =>
        ApplyKryptonAlignment(RichTextParagraphAlignment.Center);

    private void kbtnRight_Click(object? sender, EventArgs e) =>
        ApplyKryptonAlignment(RichTextParagraphAlignment.Right);

    private void kbtnJustify_Click(object? sender, EventArgs e) =>
        ApplyKryptonAlignment(RichTextParagraphAlignment.Justify);

    private void kbtnSelectAll_Click(object? sender, EventArgs e)
    {
        krtbDemo.SelectAll();
        krtbDemo.Focus();
        UpdateStatus();
    }

    private void kbtnNativeLeft_Click(object? sender, EventArgs e) =>
        ApplyNativeAlignment(HorizontalAlignment.Left);

    private void kbtnNativeCenter_Click(object? sender, EventArgs e) =>
        ApplyNativeAlignment(HorizontalAlignment.Center);

    private void kbtnNativeRight_Click(object? sender, EventArgs e) =>
        ApplyNativeAlignment(HorizontalAlignment.Right);

    private void krtbDemo_SelectionChanged(object? sender, EventArgs e) => UpdateStatus();

    private void rtbNative_SelectionChanged(object? sender, EventArgs e) => UpdateStatus();

    private void ApplyKryptonAlignment(RichTextParagraphAlignment alignment)
    {
        if (krtbDemo.SelectionLength == 0)
        {
            krtbDemo.SelectAll();
        }

        krtbDemo.SelectionParagraphAlignment = alignment;
        krtbDemo.Focus();
        UpdateStatus();
    }

    private void ApplyNativeAlignment(HorizontalAlignment alignment)
    {
        if (rtbNative.SelectionLength == 0)
        {
            rtbNative.SelectAll();
        }

        rtbNative.SelectionAlignment = alignment;
        rtbNative.Focus();
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        klblStatus.Values.Text =
            $"Krypton SelectionParagraphAlignment={krtbDemo.SelectionParagraphAlignment}; " +
            $"SelectionAlignment={krtbDemo.SelectionAlignment} " +
            $"(Justify reads as Left via SelectionAlignment).  |  " +
            $"Native SelectionAlignment={rtbNative.SelectionAlignment} " +
            "(no Justify API on HorizontalAlignment).";
    }
}
