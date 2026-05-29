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
///  Displays a dialog window from which the user can select a file.
/// </summary>
[DesignerCategory(@"code")]
[Description("Displays a Kryptonised version of the standard 'SaveFile dialog window' from which the user can select a file.")]
[ToolboxBitmap(typeof(SaveFileDialog), @"ToolboxBitmaps.KryptonSaveFileDialog.bmp")]
[ToolboxItem(true)]
public class KryptonSaveFileDialog : FileSaveDialogWrapper, IDisposable
{
    private readonly SaveFileDialog _internalSaveFileDialog = new SaveFileDialog();// { AutoUpgradeEnabled = true };

    /// <inheritdoc />
    protected override DialogResult ShowActualDialog(IWin32Window? owner) => _internalSaveFileDialog.ShowDialog(owner);

#if NET8_0_OR_GREATER
        /// <summary>
        ///  Gets or sets a value indicating whether the dialog box verifies if the creation of the specified file will be successful.
        ///  If this flag is not set, the calling application must handle errors, such as denial of access, discovered when the item is created.
        /// </summary>
        [Category(@"Behavior")]
        [DefaultValue(true)]
        [Description(@"Gets or sets a value indicating whether the dialog box verifies if the creation of the specified file will be successful.")]
        public bool CheckWriteAccess
        {
            get => _internalSaveFileDialog.CheckWriteAccess;
            set => _internalSaveFileDialog.CheckWriteAccess = value;
        }

        ///// <summary>
        /////  Gets or sets a value indicating whether the dialog box is always opened in the expanded mode.
        ///// </summary>
        //[Category(@"Behavior")]
        //[DefaultValue(true)]
        //[Description(@"Gets or sets a value indicating whether the dialog box is always opened in the expanded mode.")]
        //public bool ExpandedMode
        //{
        //    get => _internalSaveFileDialog.ExpandedMode;
        //    set => _internalSaveFileDialog.ExpandedMode = value;
        //}
#endif

    /// <summary>
    ///  Gets or sets a value indicating whether the dialog box prompts the user for
    ///  permission to create a file if the user specifies a file that does not exist.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"Gets or sets a value indicating whether the dialog box prompts the use for permission")]
    public bool CreatePrompt
    {
        get => _internalSaveFileDialog.CreatePrompt;
        set => _internalSaveFileDialog.CreatePrompt = value;
    }

    /// <summary>
    ///  Gets or sets a value indicating whether the Save As dialog box displays a warning if the user specifies
    ///  a file name that already exists.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@" Gets or sets a value indicating whether the Save As dialog box displays an overwrite warning")]
    public bool OverwritePrompt
    {
        get => _internalSaveFileDialog.OverwritePrompt;
        set => _internalSaveFileDialog.OverwritePrompt = value;
    }

    /// <summary>Opens the file selected by the user, with read-only permission. The file is specified by the <see cref="P:System.Windows.Forms.FileDialog.FileName" /> property.</summary>
    /// <returns>A <see cref="T:System.IO.Stream" /> that specifies the read-only file selected by the user.</returns>
    /// <exception cref="T:System.ArgumentNullException">The file name is <see langword="null" />.</exception>
    public Stream OpenFile() => _internalSaveFileDialog.OpenFile();

    /// <inheritdoc />
    public override bool AddExtension
    {
        get => _internalSaveFileDialog.AddExtension;
        set => _internalSaveFileDialog.AddExtension = value;
    }

    /// <inheritdoc />
    public override bool CheckFileExists
    {
        get => _internalSaveFileDialog.CheckFileExists;
        set => _internalSaveFileDialog.CheckFileExists = value;
    }

    /// <inheritdoc />
    public override bool CheckPathExists
    {
        get => _internalSaveFileDialog.CheckPathExists;
        set => _internalSaveFileDialog.CheckPathExists = value;
    }

#if NET8_0_OR_GREATER
        /// <inheritdoc />
        public override Guid? ClientGuid
        { 
            get => _internalSaveFileDialog.ClientGuid;
            set => _internalSaveFileDialog.ClientGuid = value;
        }
#endif

    /// <inheritdoc />
    [AllowNull]
    public override string DefaultExt
    {
        get => _internalSaveFileDialog.DefaultExt;
        set => _internalSaveFileDialog.DefaultExt = value;
    }

    /// <inheritdoc />
    public override bool DereferenceLinks
    {
        get => _internalSaveFileDialog.DereferenceLinks;
        set => _internalSaveFileDialog.DereferenceLinks = value;
    }

    /// <inheritdoc />
    [AllowNull]
    public override string FileName
    {
        get => _internalSaveFileDialog.FileName;
        set => _internalSaveFileDialog.FileName = value;
    }

    /// <inheritdoc />
    [AllowNull]
    public override string[] FileNames => _internalSaveFileDialog.FileNames;

    /// <inheritdoc />
    [AllowNull]
    public override string Filter
    {
        get => _internalSaveFileDialog.Filter;
        set => _internalSaveFileDialog.Filter = value;
    }

    /// <inheritdoc />
    public override int FilterIndex
    {
        get => _internalSaveFileDialog.FilterIndex;
        set => _internalSaveFileDialog.FilterIndex = value;
    }

    /// <inheritdoc />
    [AllowNull]
    public override string InitialDirectory
    {
        get => _internalSaveFileDialog.InitialDirectory;
        set => _internalSaveFileDialog.InitialDirectory = value;
    }

    /// <inheritdoc />
    public override bool RestoreDirectory
    {
        get => _internalSaveFileDialog.RestoreDirectory;
        set => _internalSaveFileDialog.RestoreDirectory = value;
    }

    /// <inheritdoc />
    public override bool SupportMultiDottedExtensions
    {
        get => _internalSaveFileDialog.SupportMultiDottedExtensions;
        set => _internalSaveFileDialog.SupportMultiDottedExtensions = value;
    }

    /// <inheritdoc />
    [AllowNull]
    public override string Title
    {
        get => _internalSaveFileDialog.Title;
        set => _internalSaveFileDialog.Title = value;
    }

    /// <inheritdoc />
    public override bool ValidateNames
    {
        get => _internalSaveFileDialog.ValidateNames;
        set => _internalSaveFileDialog.ValidateNames = value;
    }

    /// <inheritdoc />
    public override event CancelEventHandler? FileOk
    {
        add => _internalSaveFileDialog.FileOk += value;
        remove => _internalSaveFileDialog.FileOk -= value;
    }

    /// <summary>Resets all properties to their default values.</summary>
    public override void Reset() => _internalSaveFileDialog.Reset();

    /// <inheritdoc />
    public override string ToString() => _internalSaveFileDialog.ToString();

    /// <inheritdoc />
    public override FileDialogCustomPlacesCollection CustomPlaces => _internalSaveFileDialog.CustomPlaces;

    /// <inheritdoc />
    public void Dispose() => _internalSaveFileDialog.Dispose();

}