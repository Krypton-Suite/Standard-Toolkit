#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using Krypton.Utilities;
using Krypton.Ribbon;

namespace TestForm;

/// <summary>
/// Comprehensive demo showing KryptonRibbon merging and UserControl hosting capabilities.
/// </summary>
public partial class RibbonMergerDemo : KryptonForm
{
    private readonly Dictionary<string, (UserControl Control, KryptonRibbon Ribbon)> _loadedPlugins = new();

    public RibbonMergerDemo()
    {
        InitializeComponent();
        InitializeMainRibbon();
        UpdatePluginButtons();
        LogMessage("Ribbon Merger Demo initialized. Ready to load plugins.");
    }

    private void InitializeMainRibbon()
    {
        // Create main application ribbon tabs
        var homeTab = new KryptonRibbonTab { Text = "Home" };
        var homeGroup = new KryptonRibbonGroup { TextLine1 = "Main" };
        var newButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "New"
        };
        newButton.Click += (s, e) => LogMessage("Main ribbon: New button clicked");
        var triple1 = new KryptonRibbonGroupTriple();
        triple1.Items?.Add(newButton);
        homeGroup.Items.Add(triple1);
        homeTab.Groups.Add(homeGroup);

        var viewTab = new KryptonRibbonTab { Text = "View" };
        var viewGroup = new KryptonRibbonGroup { TextLine1 = "Display" };
        var refreshButton = new KryptonRibbonGroupButton
        {
            TextLine1 = "Refresh"
        };
        refreshButton.Click += (s, e) => LogMessage("Main ribbon: Refresh button clicked");
        var triple2 = new KryptonRibbonGroupTriple();
        triple2.Items?.Add(refreshButton);
        viewGroup.Items.Add(triple2);
        viewTab.Groups.Add(viewGroup);

        mainRibbon.RibbonTabs.Add(homeTab);
        mainRibbon.RibbonTabs.Add(viewTab);
    }

    private void LoadPlugin(string pluginName)
    {
        if (_loadedPlugins.ContainsKey(pluginName))
        {
            LogMessage($"Plugin '{pluginName}' is already loaded.");
            return;
        }

        try
        {
            UserControl pluginControl;
            KryptonRibbon pluginRibbon;

            // Create plugin UserControl based on name
            switch (pluginName)
            {
                case "Image Editor":
                    pluginControl = new ImageEditorPlugin();
                    pluginRibbon = ((ImageEditorPlugin)pluginControl).Ribbon;
                    break;

                case "Text Tools":
                    pluginControl = new TextToolsPlugin();
                    pluginRibbon = ((TextToolsPlugin)pluginControl).Ribbon;
                    break;

                case "Data Manager":
                    pluginControl = new DataManagerPlugin();
                    pluginRibbon = ((DataManagerPlugin)pluginControl).Ribbon;
                    break;

                default:
                    LogMessage($"Unknown plugin: {pluginName}");
                    return;
            }

            // Hide the ribbon on the UserControl (it will be merged into main ribbon)
            pluginRibbon.Visible = false;

            // Merge plugin ribbon into main ribbon
            mainRibbon.Merge(pluginRibbon);
            LogMessage($"✓ Merged '{pluginName}' ribbon into main ribbon.");

            // Store plugin reference
            _loadedPlugins[pluginName] = (pluginControl, pluginRibbon);

            // Add UserControl to content panel
            pluginControl.Dock = DockStyle.Fill;
            contentPanel.Controls.Clear();
            contentPanel.Controls.Add(pluginControl);

            UpdatePluginButtons();
            LogMessage($"Plugin '{pluginName}' loaded successfully. Tabs: {pluginRibbon.RibbonTabs.Count}");
        }
        catch (Exception ex)
        {
            LogMessage($"✗ Error loading plugin '{pluginName}': {ex.Message}");
        }
    }

    private void UnloadPlugin(string pluginName)
    {
        if (!_loadedPlugins.TryGetValue(pluginName, out var pluginData))
        {
            LogMessage($"Plugin '{pluginName}' is not loaded.");
            return;
        }

        try
        {
            // Unmerge plugin ribbon from main ribbon
            mainRibbon.Unmerge(pluginData.Ribbon);
            LogMessage($"✓ Unmerged '{pluginName}' ribbon from main ribbon.");

            // Remove UserControl
            contentPanel.Controls.Clear();

            // Dispose plugin
            pluginData.Control.Dispose();

            // Remove from dictionary
            _loadedPlugins.Remove(pluginName);

            UpdatePluginButtons();
            LogMessage($"Plugin '{pluginName}' unloaded successfully.");
        }
        catch (Exception ex)
        {
            LogMessage($"✗ Error unloading plugin '{pluginName}': {ex.Message}");
        }
    }

    private void UpdatePluginButtons()
    {
        btnLoadImageEditor.Enabled = !_loadedPlugins.ContainsKey("Image Editor");
        btnUnloadImageEditor.Enabled = _loadedPlugins.ContainsKey("Image Editor");

        btnLoadTextTools.Enabled = !_loadedPlugins.ContainsKey("Text Tools");
        btnUnloadTextTools.Enabled = _loadedPlugins.ContainsKey("Text Tools");

        btnLoadDataManager.Enabled = !_loadedPlugins.ContainsKey("Data Manager");
        btnUnloadDataManager.Enabled = _loadedPlugins.ContainsKey("Data Manager");

        lblStatus.Text = $"Loaded Plugins: {_loadedPlugins.Count} | Main Ribbon Tabs: {mainRibbon.RibbonTabs.Count}";
    }

    private void LogMessage(string message)
    {
        if (logTextBox.InvokeRequired)
        {
            logTextBox.Invoke(new Action(() => LogMessage(message)));
            return;
        }

        string timestamp = DateTime.Now.ToString("HH:mm:ss");
        logTextBox.AppendText($"[{timestamp}] {message}\r\n");
        logTextBox.SelectionStart = logTextBox.Text.Length;
        logTextBox.ScrollToCaret();
    }

    private void BtnLoadImageEditor_Click(object sender, EventArgs e) => LoadPlugin("Image Editor");

    private void BtnUnloadImageEditor_Click(object sender, EventArgs e) => UnloadPlugin("Image Editor");

    private void BtnLoadTextTools_Click(object sender, EventArgs e) => LoadPlugin("Text Tools");

    private void BtnUnloadTextTools_Click(object sender, EventArgs e) => UnloadPlugin("Text Tools");

    private void BtnLoadDataManager_Click(object sender, EventArgs e) => LoadPlugin("Data Manager");

    private void BtnUnloadDataManager_Click(object sender, EventArgs e) => UnloadPlugin("Data Manager");

    private void BtnClearLog_Click(object sender, EventArgs e) => logTextBox.Clear();

    private void BtnUnloadAll_Click(object sender, EventArgs e)
    {
        var pluginNames = _loadedPlugins.Keys.ToArray();
        foreach (string pluginName in pluginNames)
        {
            UnloadPlugin(pluginName);
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        // Unload all plugins before closing
        BtnUnloadAll_Click(this, EventArgs.Empty);
        base.OnFormClosing(e);
    }
}