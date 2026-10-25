#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Toolkit.Utilities;

namespace TestForm;

/// <summary>
/// Demonstrates the Most-Recently-Used menu items in <c>Krypton.Toolkit.Utilities</c>: <see cref="KryptonMRUMenuItem"/>
/// (backed by <see cref="MostRecentlyUsedFileManager"/>), <see cref="KryptonMRUOpenFileMenuItem"/>, and — instantiated for
/// completeness only — <see cref="KryptonMRUSaveFileMenuItem"/> / <see cref="KryptonMRUSaveAsFileMenuItem"/>, which are
/// property stubs in the current library with no <c>OnClick</c> handler wired up yet.
/// </summary>
public class ToolStripMRUDemo : KryptonForm
{
    private const string ApplicationName = @"KryptonToolStripMRUDemo";

    private readonly KryptonMRUMenuItem _mruMenuItem;
    private readonly KryptonMRUOpenFileMenuItem _mruOpenItem;
    private readonly KryptonMRUSaveFileMenuItem _mruSaveItem;
    private readonly KryptonMRUSaveAsFileMenuItem _mruSaveAsItem;

    private readonly KryptonTextBox _txtOutput;
    private readonly KryptonButton _btnAddSampleFiles;
    private readonly KryptonLabel _lblStatus;

    private readonly List<string> _sampleFiles = [];

    public ToolStripMRUDemo()
    {
        Text = @"ToolStrip MRU Demo";
        Size = new Size(900, 600);
        StartPosition = FormStartPosition.CenterParent;

        // ----- Output control that MRU entries load their file contents into -----
        _txtOutput = new KryptonTextBox
        {
            Dock = DockStyle.Fill,
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            ReadOnly = true
        };

        // ----- File menu: MRU parent, Open (wired to the MRU list), and the Save/SaveAs stubs -----
        var menuStrip = new MenuStrip { Dock = DockStyle.Top };
        var fileMenu = new ToolStripMenuItem(@"&File");

        _mruOpenItem = new KryptonMRUOpenFileMenuItem
        {
            OutputControl = _txtOutput,
            ApplicationName = ApplicationName,
            StandardDialogFilter = @"Text files (*.txt)|*.txt|All files (*.*)|*.*"
        };

        _mruMenuItem = new KryptonMRUMenuItem
        {
            OutputControl = _txtOutput,
            ApplicationName = ApplicationName
        };
        // KryptonMRUOpenFileMenuItem records newly opened files into the same MRU list once ParentMRUMenuItem is set.
        _mruOpenItem.ParentMRUMenuItem = _mruMenuItem;

        _mruSaveItem = new KryptonMRUSaveFileMenuItem(_mruMenuItem.RecentlyUsedFileManager)
        {
            OutputControl = _txtOutput,
            ApplicationName = ApplicationName,
            ParentMRUMenuItem = _mruMenuItem,
            // Save/SaveAs are property-only stubs today (no OnClick handler in the library) - disabled so the
            // demo does not imply a working save; they are still instantiated so every MRU type is represented.
            Enabled = false,
            ToolTipText = @"Stub only: KryptonMRUSaveFileMenuItem has no OnClick handler wired up yet."
        };

        _mruSaveAsItem = new KryptonMRUSaveAsFileMenuItem(_mruMenuItem.RecentlyUsedFileManager)
        {
            OutputControl = _txtOutput,
            ApplicationName = ApplicationName,
            ParentMRUMenuItem = _mruMenuItem,
            Enabled = false,
            ToolTipText = @"Stub only: KryptonMRUSaveAsFileMenuItem has no OnClick handler wired up yet."
        };

        fileMenu.DropDownItems.Add(_mruOpenItem);
        fileMenu.DropDownItems.Add(_mruSaveItem);
        fileMenu.DropDownItems.Add(_mruSaveAsItem);
        fileMenu.DropDownItems.Add(new ToolStripSeparator());
        fileMenu.DropDownItems.Add(_mruMenuItem);

        menuStrip.Items.Add(fileMenu);
        Controls.Add(menuStrip);

        // ----- Main content -----
        var mainPanel = new KryptonPanel { Dock = DockStyle.Fill, Padding = new Padding(12) };

        var instructions = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 96,
            Text = @"File > Open uses KryptonMRUOpenFileMenuItem and records the chosen file into the shared " +
                   @"KryptonMRUMenuItem list (registry-backed via MostRecentlyUsedFileManager). Click 'Add Sample " +
                   @"Files to MRU' to seed the list without a dialog (calls AddRecentFile directly), then open " +
                   @"File > Most Recently Used... and click an entry to load it into the text box below. A " +
                   @"'&Clear list' entry appears automatically once the list is non-empty. Save/Save As are shown " +
                   @"disabled - see the tooltip on each."
        };
        mainPanel.Controls.Add(instructions);

        var buttonRow = new FlowLayoutPanel { Dock = DockStyle.Top, Top = 100, AutoSize = true };
        _btnAddSampleFiles = new KryptonButton { Values = { Text = @"Add Sample Files to MRU" } };
        buttonRow.Controls.Add(_btnAddSampleFiles);
        mainPanel.Controls.Add(buttonRow);

        var outputLabel = new KryptonLabel { Dock = DockStyle.Top, Top = 136, Values = { Text = @"OutputControl (KryptonTextBox):" } };
        mainPanel.Controls.Add(outputLabel);

        _lblStatus = new KryptonLabel { Dock = DockStyle.Bottom, Values = { Text = @"Ready." } };
        mainPanel.Controls.Add(_lblStatus);
        mainPanel.Controls.Add(_txtOutput);

        Controls.Add(mainPanel);

        _btnAddSampleFiles.Click += (_, _) => AddSampleFilesToMru();

        FormClosed += (_, _) => CleanUpSampleFiles();
    }

    private void AddSampleFilesToMru()
    {
        for (int i = 1; i <= 3; i++)
        {
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $@"{ApplicationName}-Sample{i}.txt");
            System.IO.File.WriteAllText(path, $@"Sample MRU file #{i} created at {DateTime.Now}.");
            _sampleFiles.Add(path);

            _mruMenuItem.AddRecentFile(path);
        }

        UpdateStatus(@"Added 3 sample files via KryptonMRUMenuItem.AddRecentFile(). Open File > Most Recently Used... to load one.");
    }

    private void CleanUpSampleFiles()
    {
        foreach (string path in _sampleFiles)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch (System.IO.IOException)
            {
                // Best-effort cleanup only; ignore if the file is locked or already removed.
            }
        }
    }

    private void UpdateStatus(string message) => _lblStatus.Values.Text = message;
}
