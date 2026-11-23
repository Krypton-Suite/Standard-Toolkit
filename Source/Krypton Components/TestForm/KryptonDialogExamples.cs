#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2024 - 2026. All rights reserved.
 *
 */
#endregion

using System.Drawing.Printing;

namespace TestForm;

public partial class KryptonDialogExamples: KryptonForm
{
    public KryptonDialogExamples()
    {
        InitializeComponent();
    }

    private void kbtnColorDialog_Click(object sender, EventArgs e)
    {
        var kcd = new KryptonColorDialog();

        kcd.ShowDialog();
    }

    private void kbtnFontDialog_Click(object sender, EventArgs e)
    {
        var kfd = new KryptonFontDialog();

        kfd.ShowDialog();
    }

    private void kbtnPrintDialog_Click(object sender, EventArgs e)
    {
        var kpd = new KryptonPrintDialog();

        kpd.ShowDialog();
    }

    private void kbtnPrintPreviewDialog_Click(object sender, EventArgs e)
    {
        // Create a simple PrintDocument for testing
        var printDoc = new KryptonPrintDocument
        {
            DocumentName = "Themed Print Preview Test",
            PaletteMode = PaletteMode.Global, // Use current global theme
            UsePaletteColors = true,
            TextStyle = PaletteContentStyle.LabelNormalPanel,
            BackgroundStyle = PaletteBackStyle.PanelClient
        };

        printDoc.PrintPage += (s, args) =>
        {
            var g = args.Graphics!;
            var marginBounds = args.MarginBounds;

            // Draw themed background
            var backColor = printDoc.GetBackgroundColor();
            using (var brush = new SolidBrush(backColor))
            {
                g.FillRectangle(brush, marginBounds);
            }

            // Draw themed text
            var textColor = printDoc.GetTextColor();
            var font = printDoc.GetFont();
            using (var brush = new SolidBrush(textColor))
            {
                g.DrawString("This is a themed print preview!", font, brush,
                    marginBounds.Left + 100, marginBounds.Top + 100);
            }

            // Draw themed rectangle
            var rect = new Rectangle(marginBounds.Left + 50, marginBounds.Top + 150,
                200, 100);
            printDoc.DrawThemedRectangle(g, rect);

            // Draw themed text inside rectangle
            printDoc.DrawThemedText(g, "Themed Content", null,
                new Rectangle(rect.X + 10, rect.Y + 10, rect.Width - 20, rect.Height - 20));

            args.HasMorePages = false;
        };

        var kppd = new KryptonPrintPreviewDialog
        {
            Document = printDoc,
            Text = "Print Preview Test"
        };

        kppd.ShowDialog();
    }

    private void kbtnLongInput_Click(object sender, EventArgs e)
    {
        var data = new KryptonInputBoxData
        {
            Owner = this,
            Caption = "DPI-aware input",
            Prompt = "This prompt is long enough to wrap. The dialog should grow with the text, stay inside the working area, and keep the response box and buttons visible. " +
                      "Resize the display scale and open this again: the window follows the monitor instead of staying at the 96 DPI designer size.",
            CueText = "Type a response"
        };

        KryptonInputBox.Show(data);
    }

    private void kbtnExceptionDialog_Click(object sender, EventArgs e)
    {
        try
        {
            throw new InvalidOperationException("Sample exception used to show the resizable, DPI-clamped exception dialog.");
        }
        catch (InvalidOperationException exception)
        {
            Krypton.Toolkit.Utilities.KryptonExceptionDialog.Show(exception, showCopyButton: true, showSearchBox: true);
        }
    }
}