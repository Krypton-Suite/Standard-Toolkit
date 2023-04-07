#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2023. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using MsdnMag;

namespace Krypton.Toolkit
{
    /// <summary>
    ///  Displays a dialog window from which the user can select a file.
    /// </summary>
    [DefaultEvent(nameof(FileOk))]
    [DefaultProperty(nameof(FileName))]
    public abstract class FileDialogWrapper
    {
        /// <summary>
        ///  Runs a common dialog box.
        /// </summary>
        public DialogResult ShowDialog() => ShowDialog(owner: null);

        private protected CommonDialogHandler _commonDialogHandler;

        /// <summary>
        ///  Runs a common dialog box, parented to the given IWin32Window.
        /// </summary>
        public DialogResult ShowDialog(IWin32Window? owner)
        {
            try
            {
                // Set up CBT
                _cbt.Install();
                _cwp.Install();
                _commonDialogHandler = new CommonDialogHandler(true);
                _commonDialogHandler.SetResizable(true);
                if (Icon != null)
                {
                    _commonDialogHandler.Icon = Icon;
                    _commonDialogHandler.ShowIcon = true;
                }

                return ShowActualDialog(owner);
            }
            finally
            {
                // Destroy CBT
                _cwp.Uninstall();
                _cbt.Uninstall();
                if (owner != null)
                {
                    PI.SetWindowPos(owner.Handle, PI.HWND_TOP, 0, 0, 0, 0,
                        PI.SWP_.NOACTIVATE | PI.SWP_.NOMOVE |
                        PI.SWP_.NOSIZE |
                        PI.SWP_.ASYNCWINDOWPOS);

                    //PI.SetForegroundWindow(owner.Handle);
                }
            }
        }

        #region Do_CBT
        internal FileDialogWrapper()
        {
            _cbt = new LocalCbtHook();
            _cbt.WindowCreated += WndCreated;
            _cbt.WindowDestroyed += WndDestroyed;
            _cbt.WindowActivated += (sender, e) => WndActivated(sender, e);
            _cwp = new LocalCallWndProc();
            _cwp.WindowMessage += WndMessage;
        }

        private protected virtual void WndMessage(object sender, CWPRETSTRUCT e, out bool actioned)
        {
            (bool handled, IntPtr retValue) = _commonDialogHandler.HookProc(e.hWnd, e.message, e.wParam, e.lParam);
            e.retValue = retValue;
            actioned = handled;

            //e.retValue = retValue;
            //actioned = handled;
            if (e.message == PI.WM_.INITDIALOG)
            {
                _scaleFactor = _commonDialogHandler._wrapperForm.DeviceDpi / 96.0f;
                _commonDialogHandler._wrapperForm.Resize += FormResize;
                _commonDialogHandler._wrapperForm.MinimumSize = new SizeF(440 *_scaleFactor, 345 * _scaleFactor).ToSize();
            }
        }

        private readonly LocalCbtHook _cbt;
        private readonly LocalCallWndProc _cwp;
        private protected IntPtr _handle = IntPtr.Zero;
        private bool _alreadySetup;
        private protected float _scaleFactor;

        private protected virtual void FormResize(object sender, EventArgs e)
        {
            // Align the button underneath the drop down
            var clientSize = _commonDialogHandler._wrapperForm.ClientSize;
            foreach (var button in _commonDialogHandler.Controls.Where(static ctl => ctl.Button != null))
            {
                if (button.Button?.Parent is Panel panel)
                {
                    switch (button.DlgCtrlId)
                    {
                        case 1: // @"&Save"
                        // case @"&Open":
                            panel.Location = new Point((int)(clientSize.Width - 112 * _scaleFactor - button.Button.Width),
                                (int)(clientSize.Height - 12 * _scaleFactor - button.Button.Height));
                            break;
                        case 2:
                        //case @"Cancel":
                            panel.Location = new Point((int)(clientSize.Width - 30 * _scaleFactor - button.Button.Width),
                                (int)(clientSize.Height - 12 * _scaleFactor - button.Button.Height));
                            break;
                    }
                }
            }
        }

        private void WndCreated(object sender, CbtEventArgs e)
        {
            if (e.IsDialogWindow)
            {
                _alreadySetup = false;
                Console.WriteLine(@"FileDialog created");
                _handle = e.Handle;
                _cwp.TargetWnd = _handle;
            }
        }

        private void WndDestroyed(object sender, CbtEventArgs e)
        {
            if (e.Handle == _handle)
            {
                _alreadySetup = false;
                _handle = IntPtr.Zero;
                _cwp.TargetWnd = _handle;
                _commonDialogHandler._wrapperForm.Close();
            }
        }

        private protected virtual bool WndActivated(object sender, CbtEventArgs e)
        {
            if (_handle != e.Handle)
            {
                return false;
            }

            // Not the first time
            if (_alreadySetup)
            {
                Console.WriteLine(@"Already configured. Not the first time it is activated!");
                return false;
            }
            else
            {
                _alreadySetup = true;
            }
            Console.WriteLine(@"FileDialog activated");

            // Modify the FileDialog window
            PI.SetWindowLong(_handle, PI.GWL_.EXSTYLE,
                PI.GetWindowLong(_handle, PI.GWL_.EXSTYLE) | PI.WS_EX_.TRANSPARENT);
            return true;
        }

        #endregion Do_CBT

        /// <summary>
        ///  Runs a common dialog box, parented to the given IWin32Window.
        /// </summary>
        protected abstract DialogResult ShowActualDialog(IWin32Window? owner);

        /// <summary>Get or Sets the file dialog box Icon.</summary>
        /// <returns>The file dialog box Icon.</returns>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("Gets or sets the file dialog box Icon")]
        public Icon? Icon { get; set; }

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

#if NETFRAMEWORK
#else
        /// <summary>
        /// <para>
        /// Gets or sets the GUID to associate with this dialog state. Typically, state such
        /// as the last visited folder and the position and size of the dialog is persisted
        /// based on the name of the executable file. By specifying a GUID, an application can
        /// have different persisted states for different versions of the dialog within the
        /// same application (for example, an import dialog and an open dialog).
        /// </para>
        /// <para>
        /// This functionality is not available if an application is not using visual styles
        /// or if <see cref="FileDialog.AutoUpgradeEnabled"/> is set to <see langword="false"/>.
        /// </para>
        /// </summary>
        [Localizable(false)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public abstract Guid? ClientGuid { get; set; }
#endif //ndef NETFRAMEWORK

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
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="Filter" /> format is invalid.</exception>
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

        /// <summary>Gets or sets a value indicating whether the Help button is displayed in the file dialog box.</summary>
        /// <returns>
        /// <see langword="true" /> if the dialog box includes a help button; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowHelp
        {
            get => throw new System.Data.InvalidConstraintException(@"Do not use 'ShowHelp' if you want the 'new' experience!");
            set => throw new System.Data.InvalidConstraintException(@"Do not use 'ShowHelp' if you want the 'new' experience!");
        }

        /// <summary>Gets or sets whether the dialog box supports displaying and saving files that have multiple file name extensions.</summary>
        /// <returns>
        /// <see langword="true" /> if the dialog box supports multiple file name extensions; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Gets or sets whether the dialog box supports displaying and saving files that have multiple file name extensions.")]
        public abstract bool SupportMultiDottedExtensions { get; set; }

        /// <summary>Gets or sets the file dialog box title.</summary>
        /// <returns>The file dialog box title. The default value is an empty string ("").</returns>
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("Gets or sets the file dialog box title")]
        [AllowNull]
        public abstract string Title { get; set; }

        /// <summary>Gets or sets a value indicating whether the dialog box accepts only valid Win32 file names.</summary>
        /// <returns>
        /// <see langword="true" /> if the dialog box accepts only valid Win32 file names; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Gets or sets a value indicating whether the dialog box accepts only valid Win32 file names.")]
        public abstract bool ValidateNames { get; set; }

        /// <summary>Occurs when the user clicks on the Open or Save button on a file dialog box.</summary>
        [Description("Occurs when the user clicks on the Open or Save button on a file dialog box.")]
        public abstract event CancelEventHandler FileOk;


        /// <summary>Resets all properties to their default values.</summary>
        public abstract void Reset();

        /// <summary>Provides a string version of this object.</summary>
        /// <returns>A string version of this object.</returns>
        public new abstract string ToString();

        /// <summary>Gets the custom places collection for this <see cref="T:System.Windows.Forms.FileDialog" /> instance.</summary>
        /// <returns>The custom places collection for this <see cref="T:System.Windows.Forms.FileDialog" /> instance.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public abstract FileDialogCustomPlacesCollection CustomPlaces { get; }
    }
}
