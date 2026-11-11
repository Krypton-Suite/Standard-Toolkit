#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Parent menu item for a most-recently-used file list backed by <see cref="MostRecentlyUsedFileManager"/>.
/// </summary>
[ToolboxItem(false)]
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
public class KryptonMRUMenuItem : ToolStripMenuItem
{
    #region Instance Fields

    private readonly MruMenuItemValues _values;
    private MostRecentlyUsedFileManager? _recentlyUsedFileManager;

    #endregion

    #region Public

    /// <summary>
    /// Gets the expandable configuration values for designer and runtime use.
    /// </summary>
    [Category("Behavior")]
    [Description("Default text, output control, and application name settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public MruMenuItemValues MruValues => _values;

    private bool ShouldSerializeMruValues() => !_values.IsDefault;

    private void ResetMruValues() => _values.Reset();

    /// <summary>Gets or sets the default caption text.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string DefaultText
    {
        get => _values.DefaultText;
        set => _values.DefaultText = value;
    }

    /// <summary>Gets or sets the control that receives file contents when an MRU entry is opened.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control? OutputControl
    {
        get => _values.OutputControl;
        set => _values.OutputControl = value;
    }

    /// <summary>Gets or sets the application name used for the registry MRU key.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? ApplicationName
    {
        get => _values.ApplicationName;
        set => _values.ApplicationName = value;
    }

    /// <summary>Gets the backing MRU manager when configured.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public MostRecentlyUsedFileManager? RecentlyUsedFileManager => _recentlyUsedFileManager;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonMRUMenuItem"/> class with default text.
    /// </summary>
    public KryptonMRUMenuItem()
        : this(KryptonManager.Strings.ToolStripItemStrings.MostRecentlyUsedText)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonMRUMenuItem"/> class with specified default text.
    /// </summary>
    /// <param name="defaultText">The default text for the menu item.</param>
    public KryptonMRUMenuItem(string defaultText)
    {
        _values = new MruMenuItemValues(this);
        _values.DefaultText = defaultText;
        Enabled = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonMRUMenuItem"/> class with specified default text, application name, and optional output control.
    /// </summary>
    /// <param name="defaultText">The default text for the menu item.</param>
    /// <param name="applicationName">The application name used for the registry MRU key.</param>
    /// <param name="outputControl">The control that receives file contents when an MRU entry is opened.</param>
    public KryptonMRUMenuItem(string defaultText, string applicationName, Control? outputControl = null)
        : this(defaultText)
    {
        _values.OutputControl = outputControl;
        ApplicationName = applicationName;
    }

    #endregion

    #region Implementation

    /// <summary>Creates the MRU manager when <see cref="ApplicationName"/> is set.</summary>
    public void EnsureManager()
    {
        if (string.IsNullOrEmpty(_values.ApplicationName) || _recentlyUsedFileManager != null)
        {
            return;
        }

        _recentlyUsedFileManager = new MostRecentlyUsedFileManager(
            this,
            _values.ApplicationName!,
            MyOwnRecentFileGotClicked_Handler,
            MyOwnRecentFilesGotCleared_Handler,
            useConfirmClearListDialogue: true);
    }

    /// <summary>Adds a path to the MRU list.</summary>
    public void AddRecentFile(string? filePath)
    {
        EnsureManager();
        _recentlyUsedFileManager?.AddRecentFile(filePath);
    }

    private void MyOwnRecentFileGotClicked_Handler(object sender, EventArgs e)
    {
        var fileName = (sender as ToolStripItem)?.Text;

        if (string.IsNullOrEmpty(fileName))
        {
            return;
        }

        if (!File.Exists(fileName))
        {
            if (KryptonMessageBox.Show($"'{fileName}' {KryptonManager.Strings.ToolStripItemStrings.MostRecentlyUsedFileNotFoundText}", KryptonManager.Strings.ToolStripItemStrings.MostRecentlyUsedFileNotFoundCaption,
                    KryptonMessageBoxButtons.YesNo, KryptonMessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _recentlyUsedFileManager?.RemoveRecentFile(fileName);
            }

            return;
        }

        OpenFile(fileName, _values.OutputControl);
    }

    private void MyOwnRecentFilesGotCleared_Handler(object sender, EventArgs e)
    {
    }

    private void OpenFile(string? filePath, Control? outputControl)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            if (outputControl != null)
            {
                using StreamReader sr = new StreamReader(filePath!);
                outputControl.Text = sr.ReadToEnd();
            }
        }
        catch (IOException e)
        {
            Debug.WriteLine(e.ToString());
        }
    }

    #endregion
}
