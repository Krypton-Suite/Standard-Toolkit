#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Comprehensive test form demonstrating KryptonRichTextBox formatting preservation when palette changes.
/// This form tests the fix for issue #2832: KryptonRichTextBox does not retain its formatting when palette is changed.
/// </summary>
public partial class RichTextBoxFormattingTest : KryptonForm
{
    public RichTextBoxFormattingTest()
    {
        InitializeComponent();
        InitializeSampleContent();
        InitializeInputControlStyleComboBox();
    }

    private void InitializeSampleContent()
    {
        // Load sample RTF content with various formatting
        LoadSampleRtfContent();
    }
    
    private void InitializeInputControlStyleComboBox()
    {
        foreach (InputControlStyle controlStyle in Enum.GetValues(typeof(InputControlStyle)))
        {
            kcmbInputControlStyle.Items.Add(controlStyle.ToString());
        }
        
        kcmbInputControlStyle.SelectedIndex = 0; // Default selection
    }

    private void LoadSampleRtfContent()
    {
        // Create RTF content with various formatting
        string rtfContent = @"{\rtf1\ansi\deff0 {\fonttbl {\f0 Times New Roman;} {\f1 Arial;} {\f2 Courier New;}}
{\colortbl ;\red255\green0\blue0;\red0\green128\blue0;\red0\green0\blue255;\red255\green165\blue0;}
\f0\fs24 This is a \b bold \b0 sample text with \i italic \i0 formatting.\par
\par
\f1\fs28 Here's some \ul underlined \ul0 text and \b\i bold italic \b0\i0 text.\par
\par
\f2\fs20 Colors: \cf1 Red text \cf2 Green text \cf3 Blue text \cf4 Orange text \cf0\par
\par
\b\fs24\cf1 Important: \b0\fs20\cf0 This demonstrates that RTF formatting is preserved when the palette changes.\par
\par
\f0\fs18 You can change the palette using the combo box above. The formatting should remain intact!\par
\par
\b\fs22 Test Scenarios:\b0\par
\b1. \b0 Bold text should remain bold\par
\b2. \b0 \i Italic text should remain italic\i0\par
\b3. \b0 \ul Underlined text should remain underlined\ul0\par
\b4. \b0 \cf1 Colored text should remain colored\cf0\par
\b5. \b0 Different font sizes should be preserved\par
\par
\fs16\cf2 Success! \cf0 If you can see all the formatting above after changing palettes, the fix is working correctly.}";

        krtbRichTextBox.Rtf = rtfContent;
        UpdateStatus("Sample RTF content loaded with bold, italic, underline, colors, and different fonts.");
    }

    private void UpdateStatus(string message)
    {
        klblStatus.Text = $"Status: {message}";
        klblStatus.StateCommon.ShortText.Color1 = Color.Blue;
    }

    private void UpdateInputControlStyle()
    {
        if (kcmbInputControlStyle.SelectedItem == null)
        {
            return;
        }

        string selectedStyle = kcmbInputControlStyle.SelectedItem.ToString()!;
        InputControlStyle style = GetInputControlStyleFromString(selectedStyle);

        // Update RichTextBox input control style
        krtbRichTextBox.InputControlStyle = style;

        // Update status
        UpdateStatus($"InputControlStyle changed to: {selectedStyle}. Formatting should be preserved.");
    }

    private InputControlStyle GetInputControlStyleFromString(string styleName)
    {
        return styleName switch
        {
            "Standalone" => InputControlStyle.Standalone,
            "Ribbon" => InputControlStyle.Ribbon,
            "Custom1" => InputControlStyle.Custom1,
            "Custom2" => InputControlStyle.Custom2,
            "Custom3" => InputControlStyle.Custom3,
            "PanelClient" => InputControlStyle.PanelClient,
            "PanelAlternate" => InputControlStyle.PanelAlternate,
            _ => InputControlStyle.Standalone
        };
    }

    private void KcmbInputControlStyle_SelectedIndexChanged(object? sender, EventArgs e)
    {
        UpdateInputControlStyle();
    }

    private void KbtnLoadSample_Click(object? sender, EventArgs e)
    {
        LoadSampleRtfContent();
    }

    private void KbtnLoadPlainText_Click(object? sender, EventArgs e)
    {
        krtbRichTextBox.Text = "This is plain text without any RTF formatting. When you change the palette, this text will use the palette font.";
        UpdateStatus("Plain text loaded. This will use the palette font when palette changes.");
    }

    private void KbtnVerifyFormatting_Click(object? sender, EventArgs e)
    {
        // Check if RTF formatting exists
        string rtf = krtbRichTextBox.Rtf;
        bool hasFormatting = !string.IsNullOrEmpty(rtf) &&
            (rtf.Contains(@"\b") || rtf.Contains(@"\i") || rtf.Contains(@"\ul") ||
             rtf.Contains(@"\fs") || rtf.Contains(@"\cf") || rtf.Contains(@"\highlight") ||
             (rtf.Contains(@"\f") && !rtf.Contains(@"\f0")));

        if (hasFormatting)
        {
            klblStatus.Text = "✓ Verification: RTF formatting is PRESENT and preserved!";
            klblStatus.StateCommon.ShortText.Color1 = Color.Green;
        }
        else
        {
            klblStatus.Text = "ℹ Verification: This is plain text (no RTF formatting).";
            klblStatus.StateCommon.ShortText.Color1 = Color.Blue;
        }
    }

    private void KbtnClear_Click(object? sender, EventArgs e)
    {
        krtbRichTextBox.Clear();
        UpdateStatus("RichTextBox cleared.");
    }
}
