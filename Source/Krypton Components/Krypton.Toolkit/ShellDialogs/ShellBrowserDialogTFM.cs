#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Local

namespace Krypton.Toolkit;

internal class ShellBrowserDialogTFM : ShellDialogWrapper, IDisposable
{
    private readonly OpenFileDialog _internalOpenFileDialog = new OpenFileDialog();// { AutoUpgradeEnabled = true };
    private static readonly Type _ofd = typeof(OpenFileDialog);

    [Flags]
    internal enum FOS_ : uint
    {
        OVERWRITEPROMPT = 0x00000002,
        STRICTFILETYPES = 0x00000004,
        NOCHANGEDIR = 0x00000008,
        PICKFOLDERS = 0x00000020,
        FORCEFILESYSTEM = 0x00000040, // Ensure that items returned are filesystem items.
        ALLNONSTORAGEITEMS = 0x00000080, // Allow choosing items that have no storage.
        NOVALIDATE = 0x00000100,
        ALLOWMULTISELECT = 0x00000200,
        PATHMUSTEXIST = 0x00000800,
        FILEMUSTEXIST = 0x00001000,
        CREATEPROMPT = 0x00002000,
        SHAREAWARE = 0x00004000,
        NOREADONLYRETURN = 0x00008000,
        NOTESTFILECREATE = 0x00010000,
        HIDEMRUPLACES = 0x00020000,
        HIDEPINNEDPLACES = 0x00040000,
        NODEREFERENCELINKS = 0x00100000,
        DONTADDTORECENT = 0x02000000,
        FORCESHOWHIDDEN = 0x10000000,
        DEFAULTNOMINIMODE = 0x20000000
    }

    /// <inheritdoc />
    protected override DialogResult ShowActualDialog(IWin32Window? owner)
    {
        // I tried to use the Shell dialog directly, but that just hangs the calling app when closing
        _internalOpenFileDialog.InitialDirectory ??= Environment.GetFolderPath(_rootFolder);
        _internalOpenFileDialog.AddExtension = false;
        _internalOpenFileDialog.CheckFileExists = false;
        _internalOpenFileDialog.DereferenceLinks = true;
        _internalOpenFileDialog.Filter = "folders|\n";
        _internalOpenFileDialog.Multiselect = false;
        _internalOpenFileDialog.ValidateNames = false;
        _internalOpenFileDialog.CheckPathExists = true;
        _internalOpenFileDialog.FileName = "Folder Selection.";
        var options = _ofd.GetField(@"options", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
        // Check null: if this is running on core !!
        options ??= _ofd.GetField(@"_options", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
        if (options != null)
        {
            var value = (int)options.GetValue(_internalOpenFileDialog)!;
            options.SetValue(_internalOpenFileDialog, value | (int)(FOS_.FORCEFILESYSTEM | FOS_.PICKFOLDERS));
        }

        return _internalOpenFileDialog.ShowDialog(owner);
    }

    private protected override void WndMessage(object sender, CWPRETSTRUCT e, out bool actioned)
    {
        base.WndMessage(sender, e, out actioned);

        if (e.message == PI.WM_.INITDIALOG)
        {
            var button = _commonDialogHandler.Controls.FirstOrDefault(static ctl => ctl.DlgCtrlId == 1);
            if (button?.Button != null)
            {
                button.Button.Text = @"Select Folder";
                if (button.Button.Parent is Panel panel)
                {
                    panel.Left -= (int)(30 * _scaleFactor);
                    panel.Width += (int)(30 * _scaleFactor);
                }
            }
            // Also Hide the Combo filter drop-down 
            var filterCombo = _commonDialogHandler.Controls.FirstOrDefault(static ctl => ctl.DlgCtrlId == 0x470);
            if (filterCombo != null)
            {
                var fileNameCombo = _commonDialogHandler.Controls.First(static ctl => ctl.DlgCtrlId == 0x47C);
                PI.ShowWindow(fileNameCombo.hWnd, PI.ShowWindowCommands.SW_HIDE);
                var fileNameText = _commonDialogHandler.Controls.First(static ctl => ctl.DlgCtrlId == 0x442);
                PI.ShowWindow(fileNameText.hWnd, PI.ShowWindowCommands.SW_HIDE);
                PI.ShowWindow(filterCombo.hWnd, PI.ShowWindowCommands.SW_HIDE);
            }
        }
    }

#if NET8_0_OR_GREATER
        /// <inheritdoc />
        public override Guid? ClientGuid
        {
            get => _internalOpenFileDialog.ClientGuid;
            set => _internalOpenFileDialog.ClientGuid = value;
        }
#endif

    /// <inheritdoc />
    [AllowNull]
    public override string Title
    {
        get => _internalOpenFileDialog.Title;
        set => _internalOpenFileDialog.Title = value;
    }

    public string SelectedPath
    {
        get => Path.GetDirectoryName(_internalOpenFileDialog.FileName)!;
        set => _internalOpenFileDialog.InitialDirectory = value;
    }

    private Environment.SpecialFolder _rootFolder;

    public Environment.SpecialFolder RootFolder
    {
        get => _rootFolder;
        set => _rootFolder = value;
    }

    /// <inheritdoc />
    public override void Reset() => _internalOpenFileDialog.Reset();

    /// <inheritdoc />
    public override string ToString() => _internalOpenFileDialog.ToString();

    /// <inheritdoc />
    public void Dispose() => _internalOpenFileDialog.Dispose();
}