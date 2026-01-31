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
[Description("Displays a Kryptonised version of the standard 'OpenFile dialog window' from which the user can select a file.")]
[ToolboxBitmap(typeof(OpenFileDialog), @"ToolboxBitmaps.KryptonOpenFileDialog.bmp")]
[ToolboxItem(true)]
public class KryptonOpenFileDialog : FileDialogWrapper, IDisposable
{
    private readonly OpenFileDialog _internalOpenFileDialog = new OpenFileDialog();// { AutoUpgradeEnabled = true };

    /// <inheritdoc />
    protected override DialogResult ShowActualDialog(IWin32Window? owner) => _internalOpenFileDialog.ShowDialog(owner);

    /// <summary>Gets or sets a value indicating whether the dialog box allows multiple files to be selected.</summary>
    /// <returns>
    /// <see langword="true" /> if the dialog box allows multiple files to be selected together or concurrently; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the dialog box allows multiple files to be selected.")]
    public bool Multiselect
    {
        get => _internalOpenFileDialog.Multiselect;
        set => _internalOpenFileDialog.Multiselect = value;
    }

    /// <summary>Gets or sets a value indicating whether the read-only check box is selected.</summary>
    /// <returns>
    /// <see langword="true" /> if the read-only check box is selected; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the read-only check box is selected.")]
    public bool ReadOnlyChecked
    {
        get => _internalOpenFileDialog.ReadOnlyChecked;
        set => _internalOpenFileDialog.ReadOnlyChecked = value;
    }

    /// <summary>Gets or sets a value indicating whether the dialog box contains a read-only check box.</summary>
    /// <returns>
    /// <see langword="true" /> if the dialog box contains a read-only check box; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowReadOnly
    {
        get => throw new System.Data.InvalidConstraintException(@"Do not use 'ShowReadOnly' if you want the 'new' experience!");
        set => throw new System.Data.InvalidConstraintException(@"Do not use 'ShowReadOnly' if you want the 'new' experience!");
    }

    /// <summary>Opens the file selected by the user, with read-only permission. The file is specified by the <see cref="P:System.Windows.Forms.FileDialog.FileName" /> property.</summary>
    /// <returns>A <see cref="T:System.IO.Stream" /> that specifies the read-only file selected by the user.</returns>
    /// <exception cref="T:System.ArgumentNullException">The file name is <see langword="null" />.</exception>
    public Stream OpenFile() => _internalOpenFileDialog.OpenFile();

    /// <inheritdoc />
    public override bool AddExtension
    {
        get => _internalOpenFileDialog.AddExtension;
        set => _internalOpenFileDialog.AddExtension = value;
    }

    /// <inheritdoc />
    public override bool CheckFileExists
    {
        get => _internalOpenFileDialog.CheckFileExists;
        set => _internalOpenFileDialog.CheckFileExists = value;
    }

    /// <inheritdoc />
    public override bool CheckPathExists
    {
        get => _internalOpenFileDialog.CheckPathExists;
        set => _internalOpenFileDialog.CheckPathExists = value;
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
    public override string DefaultExt
    {
        get => _internalOpenFileDialog.DefaultExt;
        set => _internalOpenFileDialog.DefaultExt = value;
    }

    /// <inheritdoc />
    public override bool DereferenceLinks
    {
        get => _internalOpenFileDialog.DereferenceLinks;
        set => _internalOpenFileDialog.DereferenceLinks = value;
    }

    /// <inheritdoc />
    [AllowNull]
    public override string FileName
    {
        get => _internalOpenFileDialog.FileName;
        set => _internalOpenFileDialog.FileName = value;
    }

    /// <inheritdoc />
    [AllowNull]
    public override string[] FileNames => _internalOpenFileDialog.FileNames;

    /// <inheritdoc />
    [AllowNull]
    public override string Filter
    {
        get => _internalOpenFileDialog.Filter;
        set => _internalOpenFileDialog.Filter = value;
    }

    /// <inheritdoc />
    public override int FilterIndex
    {
        get => _internalOpenFileDialog.FilterIndex;
        set => _internalOpenFileDialog.FilterIndex = value;
    }

    /// <inheritdoc />
    [AllowNull]
    public override string InitialDirectory
    {
        get => _internalOpenFileDialog.InitialDirectory;
        set => _internalOpenFileDialog.InitialDirectory = value;
    }

    /// <inheritdoc />
    public override bool RestoreDirectory
    {
        get => _internalOpenFileDialog.RestoreDirectory;
        set => _internalOpenFileDialog.RestoreDirectory = value;
    }

    /// <inheritdoc />
    public override bool SupportMultiDottedExtensions
    {
        get => _internalOpenFileDialog.SupportMultiDottedExtensions;
        set => _internalOpenFileDialog.SupportMultiDottedExtensions = value;
    }

    /// <inheritdoc />
    [AllowNull]
    public override string Title
    {
        get => _internalOpenFileDialog.Title;
        set => _internalOpenFileDialog.Title = value;
    }

    /// <inheritdoc />
    public override bool ValidateNames
    {
        get => _internalOpenFileDialog.ValidateNames;
        set => _internalOpenFileDialog.ValidateNames = value;
    }

    /// <inheritdoc />
    public override event CancelEventHandler? FileOk
    {
        add => _internalOpenFileDialog.FileOk += value;
        remove => _internalOpenFileDialog.FileOk -= value;
    }

    /// <summary>Resets all properties to their default values.</summary>
    public override void Reset() => _internalOpenFileDialog.Reset();

    /// <inheritdoc />
    public override string ToString() => _internalOpenFileDialog.ToString();

    /// <inheritdoc />
    public override FileDialogCustomPlacesCollection CustomPlaces => _internalOpenFileDialog.CustomPlaces;

    /// <summary>Gets the file name and extension for the file selected in the dialog box. The file name does not include the path.</summary>
    /// <returns>The file name and extension for the file selected in the dialog box. The file name does not include the path. The default value is an empty string.</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SafeFileName => _internalOpenFileDialog.SafeFileName;

    /// <summary>Gets an array of file names and extensions for all the selected files in the dialog box. The file names do not include the path.</summary>
    /// <returns>An array of file names and extensions for all the selected files in the dialog box. The file names do not include the path. If no files are selected, an empty array is returned.</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string[] SafeFileNames => _internalOpenFileDialog.SafeFileNames;

    /// <inheritdoc />
    public void Dispose() => _internalOpenFileDialog.Dispose();

}