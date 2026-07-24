#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

[ToolboxItem(false), ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip)]
public class KryptonMRUOpenFileMenuItem : ToolStripMenuItem
{
    #region Instance Fields

    private readonly MruOpenFileMenuItemValues _values;

    private MostRecentlyUsedFileManager? _recentlyUsedFileManager;

    #endregion

    #region Public

    /// <summary>
    /// Gets the expandable configuration values for designer and runtime use.
    /// </summary>
    [Category("Behavior")]
    [Description("Output control, dialog, and MRU settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public MruOpenFileMenuItemValues MruValues => _values;

    private bool ShouldSerializeMruValues() => !_values.IsDefault;

    private void ResetMruValues() => _values.Reset();

    /// <summary>Gets or sets the control to load the file content text into.</summary>
    /// <value>The control to load the file content text into.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control? OutputControl { get => _values.OutputControl; set => _values.OutputControl = value; }

    /// <summary>Gets or sets the text displayed on the tool strip menu item.</summary>
    /// <value>The text displayed on the tool strip menu item.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string DefaultText { get => _values.DefaultText; set => _values.DefaultText = value; }

    /// <summary>Gets or sets the name of your application. This is used to store the MRU list in the Windows registry.</summary>
    /// <value>The name of the application.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? ApplicationName { get => _values.ApplicationName; set => _values.ApplicationName = value; }

    /// <summary>Gets or sets the open file dialog title.</summary>
    /// <value>The open file dialog title.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string OpenFileDialogTitle { get => _values.OpenFileDialogTitle; set => _values.OpenFileDialogTitle = value; }

    /// <summary>Gets or sets the standard WinForms dialog filter string.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string StandardDialogFilter { get => _values.StandardDialogFilter; set => _values.StandardDialogFilter = value; }

    /// <summary>Gets or sets the starting directory of the open file dialog.</summary>
    /// <value>The starting directory of the open file dialog.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string StartingDirectory { get => _values.StartingDirectory; set => _values.StartingDirectory = value; }

    /// <summary>Gets or sets the parent MRU menu item.</summary>
    /// <value>The parent MRU menu item.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonMRUMenuItem? ParentMRUMenuItem { get => _values.ParentMRUMenuItem; set => _values.ParentMRUMenuItem = value; }

    #endregion

    #region Identity

    public KryptonMRUOpenFileMenuItem()
    {
        _values = new MruOpenFileMenuItemValues(this);

        Text = _values.DefaultText;

        Image = Properties.Resources.Open;

        ShortcutKeys = Keys.Control | Keys.O;

        ShortcutKeyDisplayString = @"Ctrl + O";
    }

    public KryptonMRUOpenFileMenuItem(string? defaultText)
        : this()
    {
        if (!string.IsNullOrEmpty(defaultText))
        {
            _values.DefaultText = defaultText!;
            Text = _values.DefaultText;
        }
    }

    #endregion

    #region Implementation

    private void OpenFile(string? filePath, Control? outputControl)
    {
        try
        {
            if (File.Exists(filePath))
            {
                if (outputControl != null)
                {
                    using StreamReader sr = new StreamReader(filePath!);

                    outputControl.Text = sr.ReadToEnd();
                }

                _recentlyUsedFileManager?.AddRecentFile(filePath);
            }
            else
            {
                KryptonMessageBox.Show($"Error: file '{filePath}' could not be found!");
            }
        }
        catch (IOException e)
        {
            Debug.WriteLine(e.ToString());
        }
    }

    private void EnsureRecentFileManager()
    {
        if (_recentlyUsedFileManager != null || _values.ParentMRUMenuItem == null || string.IsNullOrEmpty(_values.ApplicationName))
        {
            return;
        }

        _recentlyUsedFileManager = new MostRecentlyUsedFileManager(
            _values.ParentMRUMenuItem,
            _values.ApplicationName!,
            (sender, _) => OpenFile(sender is ToolStripItem item ? item.Text : null, _values.OutputControl));
    }

    #endregion

    #region Protected

    protected override void OnClick(EventArgs e)
    {
        try
        {
            EnsureRecentFileManager();

            using KryptonOpenFileDialog ofd = new KryptonOpenFileDialog();

            ofd.Title = _values.OpenFileDialogTitle;

            ofd.Filter = _values.StandardDialogFilter;

            ofd.InitialDirectory = string.IsNullOrWhiteSpace(_values.StartingDirectory)
                ? Environment.CurrentDirectory
                : _values.StartingDirectory;

            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            OpenFile(ofd.FileName, _values.OutputControl);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }

        base.OnClick(e);
    }

    #endregion
}
