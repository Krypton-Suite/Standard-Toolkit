#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable MemberCanBePrivate.Global

namespace Krypton.Toolkit;

/// <summary>
///  'File Browser dialog' from which the user can select a Directory.
/// </summary>
[DesignerCategory(@"code")]
[Description("Displays a Kryptonised version of the standard 'File Browser dialog' from which the user can select a Directory.")]
[ToolboxBitmap(typeof(FolderBrowserDialog), @"ToolboxBitmaps.KryptonFolderBrowserDialog.bmp")]
[ToolboxItem(true)]
public class KryptonFolderBrowserDialog : ShellDialogWrapper, IDisposable
{
#if NET8_0_OR_GREATER
        private readonly FolderBrowserDialog _internalOpenFileDialog = new();// { AutoUpgradeEnabled = true };
#else
    private readonly ShellBrowserDialogTFM _internalOpenFileDialog = new ShellBrowserDialogTFM();
#endif

    /// <inheritdoc />
    protected override DialogResult ShowActualDialog(IWin32Window? owner) => _internalOpenFileDialog.ShowDialog(owner);

    // If the description is used then the following code will need to be uncommented out, and then sort out the bottom buttons
    //private protected override bool WndActivated(object sender, CbtEventArgs e)
    //{
    //    if (!base.WndActivated(sender, e))
    //    {
    //        // Not handled
    //        return false;
    //    }

    //    // Modify the ShellDialogWrapper window
    //    // When it is the BrowseDlg, the Backgrounds of the list's etc are all messed up if made transparent
    //    PI.SetWindowLong(_handle, PI.GWL_.EXSTYLE,
    //        PI.GetWindowLong(_handle, PI.GWL_.EXSTYLE) ^ PI.WS_EX_.TRANSPARENT);
    //    return true;
    //}

#if NET8_0_OR_GREATER
        /// <inheritdoc />
        public override Guid? ClientGuid 
        { 
            get => _internalOpenFileDialog.ClientGuid;
            set => _internalOpenFileDialog.ClientGuid = value;
        }
#endif
    /// <summary>
    ///  Gets the directory path of the folder the user picked.
    ///  Sets the directory path of the initial folder shown in the dialog box.
    /// </summary>
    [Browsable(true)]
    [DefaultValue("")]
    [Editor(@"System.Windows.Forms.Design.SelectedPathEditor", typeof(UITypeEditor))]
    [Localizable(true)]
    [Category(@"FolderBrowsing")]
    [Description(@"Sets the directory path of the initial folder shown in the dialog box.")]
    [AllowNull]
    public string SelectedPath
    {
        get => _internalOpenFileDialog.SelectedPath;
        set => _internalOpenFileDialog.SelectedPath = value!;
    }

#if NET8_0_OR_GREATER
        /// <summary>
        ///  Gets or sets the initial directory displayed by the folder browser dialog.
        /// </summary>
        [Category(@"FolderBrowsing")]
        [DefaultValue("")]
        [Editor(typeof(KryptonInitialDirectoryEditor), typeof(UITypeEditor))]
        [Description(@"Gets or sets the initial directory displayed by the folder browser dialog")]
        [AllowNull]
        public string InitialDirectory
        {
            get => _internalOpenFileDialog.InitialDirectory;
            set => _internalOpenFileDialog.InitialDirectory = value!;
        }
#endif

    /// <summary>
    ///  Gets/sets the root node of the directory tree.
    /// </summary>
    [Browsable(true)]
    [DefaultValue(Environment.SpecialFolder.Desktop)]
    [Localizable(false)]
    [Category(@"FolderBrowsing")]
    [Description(@"Gets/sets the root node of the directory tree")]
    //[TypeConverter(typeof(SpecialFolderEnumConverter))]
    public Environment.SpecialFolder RootFolder
    {
        get => _internalOpenFileDialog.RootFolder;
        set => _internalOpenFileDialog.RootFolder = value;
    }

    private string? _title;

    /// <inheritdoc />
    [AllowNull]
    public override string Title
    {
        get => _title ?? string.Empty;
        set => _title = value;
    }

    /// <summary>Resets all properties to their default values.</summary>
    public override void Reset() => _internalOpenFileDialog.Reset();

    /// <inheritdoc />
    public override string ToString() => _internalOpenFileDialog.ToString();

    /// <inheritdoc />
    public void Dispose() => _internalOpenFileDialog.Dispose();

}