#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

public partial class ApplicationStringsTest : KryptonForm
{
    private const string DemoKey = "SaveDraft";
    private const string DemoStringSetName = "DemoApp";

    private readonly DemoAppStrings _demoAppStrings = new DemoAppStrings();
    private readonly KryptonButton _btnExportXml = new KryptonButton();
    private readonly KryptonButton _btnImportXml = new KryptonButton();
    private readonly KryptonButton _btnExportJson = new KryptonButton();
    private readonly KryptonButton _btnImportJson = new KryptonButton();
    private readonly KryptonWrapLabel _lblStatus = new KryptonWrapLabel();

    public ApplicationStringsTest()
    {
        InitializeComponent();

        KryptonCustomStrings.Set(DemoKey, "S&ave Draft");
        KryptonCustomStrings.RegisterStringSet(DemoStringSetName, _demoAppStrings);

        ConfigurePersistenceDemoUi();

        ApplyDictionaryDemoString();
        ApplyTypedDemoString();
    }

    private void ApplyDictionaryDemoString()
    {
        kbtnDemo.Text = KryptonCustomStrings.Get(DemoKey, DemoKey);
        ktxtValue.Text = KryptonCustomStrings.Get(DemoKey);
    }

    private void ApplyTypedDemoString()
    {
        DemoAppStrings? strings = KryptonCustomStrings.GetStringSet<DemoAppStrings>(DemoStringSetName);
        if (strings != null)
        {
            kbtnTypedDemo.Text = strings.SaveDraft;
            ktxtTypedValue.Text = strings.SaveDraft;
        }
    }

    private void kbtnUpdate_Click(object sender, EventArgs e)
    {
        KryptonCustomStrings.Set(DemoKey, ktxtValue.Text);
        ApplyDictionaryDemoString();
    }

    private void kbtnReset_Click(object sender, EventArgs e)
    {
        KryptonCustomStrings.Values.Remove(DemoKey);
        kbtnDemo.Text = DemoKey;
        ktxtValue.Text = string.Empty;
    }

    private void kbtnUpdateTyped_Click(object sender, EventArgs e)
    {
        DemoAppStrings? strings = KryptonCustomStrings.GetStringSet<DemoAppStrings>(DemoStringSetName);
        if (strings != null)
        {
            strings.SaveDraft = ktxtTypedValue.Text;
            ApplyTypedDemoString();
        }
    }

    private void kbtnResetTyped_Click(object sender, EventArgs e)
    {
        DemoAppStrings? strings = KryptonCustomStrings.GetStringSet<DemoAppStrings>(DemoStringSetName);
        strings?.Reset();
        ApplyTypedDemoString();
    }

    private void ConfigurePersistenceDemoUi()
    {
        Text = @"Custom Strings Persistence Test";
        ClientSize = new Size(484, 420);
        kryptonPanel1.Height = 84;
        kryptonPanel1.Location = new Point(0, ClientSize.Height - kryptonPanel1.Height);
        kryptonBorderEdge1.Location = new Point(0, kryptonPanel1.Top - 1);
        kryptonPanel2.Size = new Size(484, kryptonBorderEdge1.Top);

        kryptonWrapLabel1.Text =
            @"Issue #3757: drop KryptonCustomStringsManager on a form and edit CustomStrings in the designer, or use KryptonCustomStrings at runtime. " +
            @"This demo also exercises XML/JSON persistence for dictionary values and registered typed string sets.";

        var secondRowTop = 47;

        ConfigureBottomButton(_btnImportJson, new Point(12, secondRowTop), @"Import JSON", kbtnImportJson_Click);
        ConfigureBottomButton(_btnExportJson, new Point(120, secondRowTop), @"Export JSON", kbtnExportJson_Click);
        ConfigureBottomButton(_btnImportXml, new Point(228, secondRowTop), @"Import XML", kbtnImportXml_Click);
        ConfigureBottomButton(_btnExportXml, new Point(336, secondRowTop), @"Export XML", kbtnExportXml_Click);

        kryptonPanel1.Controls.Add(_btnImportJson);
        kryptonPanel1.Controls.Add(_btnExportJson);
        kryptonPanel1.Controls.Add(_btnImportXml);
        kryptonPanel1.Controls.Add(_btnExportXml);

        _lblStatus.AutoSize = false;
        _lblStatus.Location = new Point(13, 285);
        _lblStatus.Size = new Size(459, 105);
        _lblStatus.Text =
            @"Persistence demo:" + Environment.NewLine +
            @"1) Edit the dictionary and typed values." + Environment.NewLine +
            @"2) Export XML or JSON." + Environment.NewLine +
            @"3) Change or reset values, then import the file back and verify both sections restore.";
        kryptonPanel2.Controls.Add(_lblStatus);
    }

    private void ConfigureBottomButton(KryptonButton button, Point location, string text, EventHandler onClick)
    {
        button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        button.Location = location;
        button.Size = new Size(100, 25);
        button.Values.Text = text;
        button.Click += onClick;
    }

    private void kbtnExportXml_Click(object? sender, EventArgs e)
    {
        using var sfd = new SaveFileDialog
        {
            OverwritePrompt = true,
            DefaultExt = @"xml",
            FileName = @"CustomTranslations",
            Filter = @"Custom translations files (*.xml)|*.xml|All files (*.*)|(*.*)",
            Title = @"Save Custom Strings (XML)"
        };

        if (sfd.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(sfd.FileName))
        {
            return;
        }

        KryptonCustomStrings.ExportToXmlFile(sfd.FileName, includeDefaults: true);
        _lblStatus.Text = $@"Exported custom strings to XML: {sfd.FileName}";
    }

    private void kbtnImportXml_Click(object? sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog
        {
            CheckFileExists = true,
            CheckPathExists = true,
            DefaultExt = @"xml",
            FileName = @"CustomTranslations",
            Filter = @"Custom translations files (*.xml)|*.xml|All files (*.*)|(*.*)",
            Title = @"Load Custom Strings (XML)"
        };

        if (ofd.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(ofd.FileName))
        {
            return;
        }

        KryptonCustomStrings.ImportFromXmlFile(ofd.FileName, resetFirst: true);
        ApplyDictionaryDemoString();
        ApplyTypedDemoString();
        _lblStatus.Text = $@"Imported custom strings from XML: {ofd.FileName}";
    }

    private void kbtnExportJson_Click(object? sender, EventArgs e)
    {
        using var sfd = new SaveFileDialog
        {
            OverwritePrompt = true,
            DefaultExt = @"json",
            FileName = @"CustomTranslations",
            Filter = @"Custom translations files (*.json)|*.json|All files (*.*)|(*.*)",
            Title = @"Save Custom Strings (JSON)"
        };

        if (sfd.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(sfd.FileName))
        {
            return;
        }

        KryptonCustomStrings.ExportToJsonFile(sfd.FileName, includeDefaults: true);
        _lblStatus.Text = $@"Exported custom strings to JSON: {sfd.FileName}";
    }

    private void kbtnImportJson_Click(object? sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog
        {
            CheckFileExists = true,
            CheckPathExists = true,
            DefaultExt = @"json",
            FileName = @"CustomTranslations",
            Filter = @"Custom translations files (*.json)|*.json|All files (*.*)|(*.*)",
            Title = @"Load Custom Strings (JSON)"
        };

        if (ofd.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(ofd.FileName))
        {
            return;
        }

        KryptonCustomStrings.ImportFromJsonFile(ofd.FileName, resetFirst: true);
        ApplyDictionaryDemoString();
        ApplyTypedDemoString();
        _lblStatus.Text = $@"Imported custom strings from JSON: {ofd.FileName}";
    }
}
