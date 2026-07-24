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
/// Deals with the back-end logic of a most recently used file <see cref="KryptonMRUMenuItem"/>.
/// Adapted from (https://www.codeproject.com/Articles/407513/Add-Most-Recently-Used-Files-MRU-List-to-Windows).
/// </summary>
public class MostRecentlyUsedFileManager
{
    #region Instance Fields

    private bool _useConfirmClearListDialogue;

    private string? _applicationName;
    private readonly string _subKeyName;
    private string _filePath = string.Empty;
    private readonly KryptonMRUMenuItem _parentMenuItem;

    #endregion

    #region Events

    private Action<object, EventArgs>? OnRecentFileClick;

    private Action<object, EventArgs>? OnClearRecentFilesClick;

    #endregion

    #region Public

    public bool UseConfirmClearListDialogue { get => _useConfirmClearListDialogue; set => _useConfirmClearListDialogue = value; }

    public string? ApplicationName { get => _applicationName; set => _applicationName = value; }

    public string FilePath { get => _filePath; set => _filePath = value; }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="MostRecentlyUsedFileManager" /> class.</summary>
    /// <param name="parentMenuItem">The parent menu item.</param>
    /// <param name="applicationName">Name of the application.</param>
    /// <param name="onRecentFileClick">The on recent file click.</param>
    /// <param name="onClearRecentFilesClick">The on clear recent files click.</param>
    /// <param name="useConfirmClearListDialogue">if set to <c>true</c> [use confirm clear list dialogue].</param>
    /// <exception cref="System.ArgumentException">Bad argument.</exception>
    public MostRecentlyUsedFileManager(KryptonMRUMenuItem parentMenuItem, string applicationName, Action<object, EventArgs> onRecentFileClick, Action<object, EventArgs>? onClearRecentFilesClick = null, bool useConfirmClearListDialogue = false)
    {
        if (string.IsNullOrEmpty(applicationName) || applicationName.Contains("\\"))
        {
            throw new ArgumentException("Bad argument.");
        }

        _parentMenuItem = parentMenuItem ?? throw new ArgumentException("Bad argument.");

        _applicationName = applicationName;

        OnRecentFileClick = onRecentFileClick ?? throw new ArgumentException("Bad argument.");

        OnClearRecentFilesClick = onClearRecentFilesClick;

        _subKeyName = $"Software\\{applicationName}\\MRU";

        UseConfirmClearListDialogue = useConfirmClearListDialogue;

        RefreshRecentFilesMenu();
    }

    #endregion

    #region Implementation

    /// <summary>
    /// Called when [clear recent files click].
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <param name="evt">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void OnClearRecentFiles_Click(object? obj, EventArgs evt)
    {
        try
        {
            if (UseConfirmClearListDialogue)
            {
                if (KryptonMessageBox.Show(
                        "You are about to clear your recent files list. Do you want to continue?",
                        "Clear Recent Files",
                        KryptonMessageBoxButtons.YesNo,
                        KryptonMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ClearRecentFiles();
                }
            }
            else
            {
                ClearRecentFiles();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        if (obj != null)
        {
            OnClearRecentFilesClick?.Invoke(obj, evt);
        }
    }

    /// <summary>
    /// Clears the recent files.
    /// </summary>
    private void ClearRecentFiles()
    {
        try
        {
            RegistryKey? rK = Registry.CurrentUser.OpenSubKey(_subKeyName, true);

            if (rK == null)
            {
                return;
            }

            string[] values = rK.GetValueNames();

            foreach (string valueName in values)
            {
                rK.DeleteValue(valueName, true);
            }

            rK.Close();

            _parentMenuItem.DropDownItems.Clear();

            _parentMenuItem.Enabled = false;
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex);
        }
    }

    /// <summary>
    /// Refreshes the recent files menu.
    /// </summary>
    private void RefreshRecentFilesMenu()
    {
        RegistryKey? rK;

        string? s;

        ToolStripItem tSI;

        try
        {
            rK = Registry.CurrentUser.OpenSubKey(_subKeyName, false);

            if (rK == null)
            {
                _parentMenuItem.Enabled = false;

                return;
            }
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex);

            return;
        }

        _parentMenuItem.DropDownItems.Clear();

        string[] valueNames = rK.GetValueNames();

        foreach (string valueName in valueNames)
        {
            s = rK.GetValue(valueName, null) as string;

            if (s == null)
            {
                continue;
            }

            tSI = _parentMenuItem.DropDownItems.Add(s);

            tSI.Click += (sender, e) => OnRecentFileClick?.Invoke(sender!, e);
        }

        if (_parentMenuItem.DropDownItems.Count == 0)
        {
            _parentMenuItem.Enabled = false;

            return;
        }

        _parentMenuItem.DropDownItems.Add("-");

        tSI = _parentMenuItem.DropDownItems.Add("&Clear list");

        tSI.Click += OnClearRecentFiles_Click;

        _parentMenuItem.Enabled = true;
    }
    #endregion

    #region Public members
    /// <summary>
    /// Adds the recent file.
    /// </summary>
    /// <param name="fileNameWithFullPath">The file name with full path.</param>
    public void AddRecentFile(string? fileNameWithFullPath)
    {
        string? s;

        try
        {
            RegistryKey? rK = Registry.CurrentUser.CreateSubKey(_subKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree);

            for (int i = 0; ; i++)
            {
                s = rK?.GetValue(i.ToString(), null) as string;

                if (s == null)
                {
                    rK?.SetValue(i.ToString(), fileNameWithFullPath!);

                    rK?.Close();

                    break;
                }
                else if (s == fileNameWithFullPath)
                {
                    rK?.Close();

                    break;
                }
            }
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex);
        }

        RefreshRecentFilesMenu();
    }


    /// <summary>
    /// Removes the recent file.
    /// </summary>
    /// <param name="fileNameWithFullPath">The file name with full path.</param>
    public void RemoveRecentFile(string? fileNameWithFullPath)
    {
        try
        {
            RegistryKey? rK = Registry.CurrentUser.OpenSubKey(_subKeyName, true);

            string[] valuesNames = rK!.GetValueNames();

            foreach (string valueName in valuesNames)
            {
                if (rK.GetValue(valueName, null) as string == fileNameWithFullPath)
                {
                    rK.DeleteValue(valueName, true);

                    RefreshRecentFilesMenu();

                    break;
                }
            }
        }
        catch (Exception ex)
        {
            KryptonExceptionHandler.CaptureException(ex);
        }

        RefreshRecentFilesMenu();
    }

    #endregion
}
