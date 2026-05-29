#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Krypton.Toolkit;

/// <summary>
///  Displays a dialog window from which the user can select a file.
/// </summary>
[DefaultEvent(nameof(FileOk))]
[DefaultProperty(nameof(FileName))]
public abstract class FileDialogWrapper : ShellDialogWrapper
{
    /// <summary>Gets or sets a value indicating whether the dialog box automatically adds an extension to a file name if the user omits the extension.</summary>
    /// <returns>
    /// <see langword="true" /> if the dialog box adds an extension to a file name if the user omits the extension; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the dialog box automatically adds an extension to a file name if the user omits the extension.")]
    public abstract bool AddExtension { get; set; }

    /// <summary>Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist.</summary>
    /// <returns>
    /// <see langword="true" /> if the dialog box displays a warning if the user specifies a file name that does not exist; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist.")]
    public abstract bool CheckFileExists { get; set; }

    /// <summary>Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a path that does not exist.</summary>
    /// <returns>
    /// <see langword="true" /> if the dialog box displays a warning when the user specifies a path that does not exist; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a path that does not exist")]
    public abstract bool CheckPathExists { get; set; }

    /// <summary>Gets or sets the default file name extension.</summary>
    /// <returns>The default file name extension. The returned string does not include the period. The default value is an empty string ("").</returns>
    [Category("Behavior")]
    [DefaultValue("")]
    [Description("Gets or sets the default file name extension.")]
    [AllowNull]
    public abstract string DefaultExt { get; set; }

    /// <summary>Gets or sets a value indicating whether the dialog box returns the location of the file referenced by the shortcut or whether it returns the location of the shortcut (.lnk).</summary>
    /// <returns>
    /// <see langword="true" /> if the dialog box returns the location of the file referenced by the shortcut; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the dialog box returns the location of the file referenced by the shortcut or whether it returns the location of the shortcut (.lnk).")]
    public abstract bool DereferenceLinks { get; set; }

    /// <summary>Gets or sets a string containing the file name selected in the file dialog box.</summary>
    /// <returns>The file name selected in the file dialog box. The default value is an empty string ("").</returns>
    [Category("Data")]
    [DefaultValue("")]
    [Description("Gets or sets a string containing the file name selected in the file dialog box.")]
    [AllowNull]
    public abstract string FileName { get; set; }

    /// <summary>Gets the file names of all selected files in the dialog box.</summary>
    /// <returns>An array of type <see cref="T:System.String" />, containing the file names of all selected files in the dialog box.</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the file names of all selected files in the dialog box.")]
    [AllowNull]
    public abstract string[] FileNames { get; }

    /// <summary>Gets or sets the current file name filter string, which determines the choices that appear in the "Save as file type" or "Files of type" box in the dialog box.</summary>
    /// <returns>The file filtering options available in the dialog box.</returns>
    [Category("Behavior")]
    [DefaultValue("")]
    [Localizable(true)]
    [Description("Gets or sets the current file name filter string, which determines the choices that appear in the \"Save as file type\" or \"Files of type\" box in the dialog box.")]
    [AllowNull]
    public abstract string Filter { get; set; }

    /// <summary>Gets or sets the index of the filter currently selected in the file dialog box.</summary>
    /// <returns>A value containing the index of the filter currently selected in the file dialog box. The default value is 1.</returns>
    [Category("Behavior")]
    [DefaultValue(1)]
    [Description("Gets or sets the index of the filter currently selected in the file dialog box.")]
    public abstract int FilterIndex { get; set; }

    /// <summary>Gets or sets the initial directory displayed by the file dialog box.</summary>
    /// <returns>The initial directory displayed by the file dialog box. The default is an empty string ("").</returns>
    [Category("CatData")]
    [DefaultValue("")]
    [Description("Gets or sets the initial directory displayed by the file dialog box.")]
    [AllowNull]
    public abstract string InitialDirectory { get; set; }

    /// <summary>Gets or sets a value indicating whether the dialog box restores the directory to the previously selected directory before closing.</summary>
    /// <returns>
    /// <see langword="true" /> if the dialog box restores the current directory to the previously selected directory if the user changed the directory while searching for files; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the dialog box restores the directory to the previously selected directory before closing.")]
    public abstract bool RestoreDirectory { get; set; }

    /// <summary>Gets or sets whether the dialog box supports displaying and saving files that have multiple file name extensions.</summary>
    /// <returns>
    /// <see langword="true" /> if the dialog box supports multiple file name extensions; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets whether the dialog box supports displaying and saving files that have multiple file name extensions.")]
    public abstract bool SupportMultiDottedExtensions { get; set; }

    /// <summary>Gets or sets a value indicating whether the dialog box accepts only valid Win32 file names.</summary>
    /// <returns>
    /// <see langword="true" /> if the dialog box accepts only valid Win32 file names; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the dialog box accepts only valid Win32 file names.")]
    public abstract bool ValidateNames { get; set; }

    /// <summary>Occurs when the user clicks on the Open or Save button on a file dialog box.</summary>
    [Description("Occurs when the user clicks on the Open or Save button on a file dialog box.")]
    public abstract event CancelEventHandler? FileOk;

    /// <summary>Gets the custom places collection for this <see cref="T:System.Windows.Forms.FileDialog" /> instance.</summary>
    /// <returns>The custom places collection for this <see cref="T:System.Windows.Forms.FileDialog" /> instance.</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public abstract FileDialogCustomPlacesCollection CustomPlaces { get; }
}