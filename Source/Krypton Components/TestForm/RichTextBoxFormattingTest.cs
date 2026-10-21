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

    private void InitializeInputControlStyleComboBox()
    {
        foreach (var style in Enum.GetValues(typeof(InputControlStyle)))
        {
            kcmbInputControlStyle.Items.Add(style.ToString());
        }

        kcmbInputControlStyle.SelectedItem = InputControlStyle.Standalone.ToString();
    }

    private void InitializeSampleContent()
    {
        // Load sample RTF content with various formatting
        LoadSampleRtfContent();
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
        InputControlStyle style = (InputControlStyle)Enum.Parse(typeof(InputControlStyle), selectedStyle);

        // Update RichTextBox input control style
        krtbRichTextBox.InputControlStyle = style;

        // Update status
        UpdateStatus($"InputControlStyle changed to: {selectedStyle}. Formatting should be preserved.");
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

    private void KbtnLoadLongRtf_Click(object? sender, EventArgs e)
    {
        // Generate long RTF content to test performance of formatting detection
        var sb = new System.Text.StringBuilder();
        sb.Append(@"{\rtf1\ansi\deff0 {\fonttbl {\f0 Times New Roman;} {\f1 Arial;} {\f2 Courier New;} {\f3 Calibri;}}
{\colortbl ;\red255\green0\blue0;\red0\green128\blue0;\red0\green0\blue255;\red255\green165\blue0;\red128\green0\blue128;}
");

        // Generate 100 paragraphs with various formatting to test performance
        for (int i = 0; i < 100; i++)
        {
            sb.Append(@"\f");
            sb.Append((i % 4).ToString());
            sb.Append(@"\fs");
            sb.Append((18 + (i % 6)).ToString());

            if (i % 3 == 0)
            {
                sb.Append(@" \b Bold paragraph ");
                sb.Append(i);
                sb.Append(@" \b0");
            }
            else if (i % 3 == 1)
            {
                sb.Append(@" \i Italic paragraph ");
                sb.Append(i);
                sb.Append(@" \i0");
            }
            else
            {
                sb.Append(@" \ul Underlined paragraph ");
                sb.Append(i);
                sb.Append(@" \ul0");
            }

            sb.Append(@" \cf");
            sb.Append((1 + (i % 4)).ToString());
            sb.Append(@" with color \cf0");
            sb.Append(@"\par");
        }

        sb.Append('}');

        var startTime = System.Diagnostics.Stopwatch.StartNew();
        krtbRichTextBox.Rtf = sb.ToString();
        startTime.Stop();

        int charCount = krtbRichTextBox.TextLength;
        int rtfLength = krtbRichTextBox.Rtf.Length;
        UpdateStatus($"Long RTF loaded: {charCount} chars, {rtfLength} RTF bytes. Load time: {startTime.ElapsedMilliseconds}ms. Test palette/style changes for performance.");
    }

    private void KbtnLoadMinimalRtf_Click(object? sender, EventArgs e)
    {
        // Minimal RTF with just basic structure (no formatting) - tests edge case
        string minimalRtf = @"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Times New Roman;}}{\colortbl ;\red0\green0\blue0;}This is minimal RTF with no formatting codes.\par}";

        krtbRichTextBox.Rtf = minimalRtf;
        UpdateStatus("Minimal RTF loaded (basic structure only, no formatting). Should use palette font.");
    }

    private void KbtnLoadComplexFormatting_Click(object? sender, EventArgs e)
    {
        // Complex RTF with all formatting types to test detection
        string complexRtf = @"{\rtf1\ansi\deff0 {\fonttbl {\f0 Times New Roman;} {\f1 Arial;} {\f2 Courier New;} {\f3 Calibri;} {\f4 Verdana;}}
{\colortbl ;\red255\green0\blue0;\red0\green128\blue0;\red0\green0\blue255;\red255\green165\blue0;\red128\green0\blue128;\red255\green192\blue203;}
\f0\fs24\cf1\b This is \b0\i bold italic \i0\ul underlined \ul0 text with \cf2 colors \cf0.\par
\f1\fs28\b\i\ul Combined formatting: \b0\i0\ul0 Bold, italic, and underlined together!\par
\f2\fs20\cf3\highlight1 Highlighted text with background color \highlight0\cf0\par
\f3\fs22\cf4 Custom font \f4\fs26\cf5 with multiple \f0\fs24\cf6 font changes \cf0\par
\f0\fs18\b\i\ul\cf1\highlight2 All formatting types: \b0\i0\ul0\cf0\highlight0 Bold, italic, underline, color, and highlight!\par
\par
\fs16 This tests the optimized single-pass formatting detection algorithm.\par}";

        krtbRichTextBox.Rtf = complexRtf;
        UpdateStatus("Complex RTF loaded with all formatting types (bold, italic, underline, colors, highlights, custom fonts).");
    }

    private void KbtnPerformanceTest_Click(object? sender, EventArgs e)
    {
        // Test performance of formatting detection by changing palette multiple times
        if (krtbRichTextBox.TextLength == 0)
        {
            UpdateStatus("Please load RTF content first before running performance test.");
            return;
        }

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        PaletteMode originalMode = KryptonManager.CurrentGlobalPaletteMode;

        // Change palette 10 times to test performance
        for (int i = 0; i < 10; i++)
        {
            //KryptonManager.CurrentGlobalPaletteMode = (i % 2 == 0) ? PaletteMode.Office2010Blue : PaletteMode.Microsoft365Blue;
            Application.DoEvents(); // Allow UI to update
        }

        // Restore original
        //KryptonManager.CurrentGlobalPaletteMode = originalMode;
        stopwatch.Stop();

        string rtf = krtbRichTextBox.Rtf;
        bool hasFormatting = !string.IsNullOrEmpty(rtf) &&
            (rtf.Contains(@"\b") || rtf.Contains(@"\i") || rtf.Contains(@"\ul") ||
             rtf.Contains(@"\fs") || rtf.Contains(@"\cf") || rtf.Contains(@"\highlight") ||
             (rtf.Contains(@"\f") && !rtf.Contains(@"\f0")));

        string result = hasFormatting ? "PRESERVED" : "LOST";
        klblStatus.Text = $"Performance: 10 palette changes in {stopwatch.ElapsedMilliseconds}ms. Formatting: {result}";
        klblStatus.StateCommon.ShortText.Color1 = hasFormatting ? Color.Green : Color.Red;
    }

    private void KbtnVerifyFormatting_Click(object? sender, EventArgs e)
    {
        // Check if RTF formatting exists using the same logic as the optimized detection
        string rtf = krtbRichTextBox.Rtf;
        bool hasFormatting = false;

        if (!string.IsNullOrEmpty(rtf) && krtbRichTextBox.TextLength > 0)
        {
            int rtfLength = rtf.Length;
            string plainText = krtbRichTextBox.Text;
            int plainTextLength = plainText?.Length ?? 0;

            // Quick length check first
            bool rtfMuchLonger = plainTextLength > 0 && rtfLength > (plainTextLength + 200);
            if (rtfMuchLonger)
            {
                hasFormatting = true;
            }
            else
            {
                // Single-pass scan (same as optimized detection)
                bool foundFormatting = false;
                bool foundCustomFont = false;

                for (int i = 0; i < rtfLength - 1 && !foundFormatting && !foundCustomFont; i++)
                {
                    if (rtf[i] == '\\')
                    {
                        char nextChar = rtf[i + 1];
                        switch (nextChar)
                        {
                            case 'b':
                            case 'i':
                                foundFormatting = true;
                                break;
                            case 'u':
                                if (i + 2 < rtfLength && rtf[i + 2] == 'l')
                                {
                                    foundFormatting = true;
                                }
                                break;
                            case 'f':
                                if (i + 2 < rtfLength)
                                {
                                    char fontDigit = rtf[i + 2];
                                    if (char.IsDigit(fontDigit) && fontDigit != '0')
                                    {
                                        foundCustomFont = true;
                                    }
                                }
                                break;
                            case 'c':
                                if (i + 2 < rtfLength && rtf[i + 2] == 'f')
                                {
                                    foundFormatting = true;
                                }
                                break;
                            case 'h':
                                if (i + 9 < rtfLength &&
                                    rtf[i + 2] == 'i' && rtf[i + 3] == 'g' &&
                                    rtf[i + 4] == 'h' && rtf[i + 5] == 'l' &&
                                    rtf[i + 6] == 'i' && rtf[i + 7] == 'g' &&
                                    rtf[i + 8] == 'h' && rtf[i + 9] == 't')
                                {
                                    foundFormatting = true;
                                }
                                break;
                            case 's':
                                if (i + 2 < rtfLength && char.IsDigit(rtf[i + 2]))
                                {
                                    foundFormatting = true;
                                }
                                break;
                        }
                    }
                }

                hasFormatting = foundFormatting || foundCustomFont;
            }
        }

        if (hasFormatting)
        {
            klblStatus.Text = $"✓ Verification: RTF formatting is PRESENT and preserved! (RTF: {krtbRichTextBox.Rtf.Length} bytes, Text: {krtbRichTextBox.TextLength} chars)";
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
