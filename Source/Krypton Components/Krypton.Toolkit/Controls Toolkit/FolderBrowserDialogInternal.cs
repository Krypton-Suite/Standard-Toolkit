using Krypton.Toolkit;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable EventNeverSubscribedTo.Global

namespace System.Windows.Forms
{
    /// <summary>
    ///  Represents a common dialog box that allows the user to specify options for
    ///  selecting a folder. 
    /// </summary>
    [DefaultEvent("HelpRequest")]
    [DefaultProperty("SelectedPath")]
    [Designer("System.Windows.Forms.Design.FolderBrowserDialogDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [Description("DescriptionFolderBrowserDialog")]
    public class FolderBrowserDialogInternal : CommonDialog
    {
        // Root node of the tree view.
        private Environment.SpecialFolder _rootFolder;

        // Description text to show.
        private string _descriptionText;

        // Folder picked by the user.
        private string _selectedPath;

        // Initial folder.
        private string _initialDirectory;

        /// <summary>
        ///  Initializes a new instance of the <see cref='FolderBrowserDialog'/> class.
        /// </summary>
        protected FolderBrowserDialogInternal()
        {
            Reset();
        }

        /// <summary>
        ///  Gets or sets whether the dialog will be automatically upgraded to enable new features.
        /// </summary>
        [DefaultValue(true)]
        public bool AutoUpgradeEnabled { get; set; } = true;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new event EventHandler HelpRequest
        {
            add => base.HelpRequest += value;
            remove => base.HelpRequest -= value;
        }

        /// <summary>
        ///  Determines if the 'New Folder' button should be exposed. (Pre Vista Style)
        ///  This property has no effect if the Vista style dialog is used; in that case, the New Folder button is always shown.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(true)]
        [Localizable(false)]
        [Category("FolderBrowsing")]
        [Description("FolderBrowserDialog Show New Folder Button")]
        public bool ShowNewFolderButton { get; set; }


        /// <summary>
        /// Force usage of the "Pre Vista Style"
        /// </summary>
        [Browsable(true)]
        [DefaultValue(false)]
        [Localizable(false)]
        [Category("FolderBrowsing")]
        [Description("FolderBrowserDialog Force Pre Vista Style")]
        public bool UsePreVistaStyle
        {
            get => !AutoUpgradeEnabled;
            set => AutoUpgradeEnabled = !value;
        }

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
        /// or if <see cref="FolderBrowserDialogInternal.AutoUpgradeEnabled"/> is set to <see langword="false"/>.
        /// </para>
        /// </summary>
        [Localizable(false)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Guid? ClientGuid { get; set; }

        /// <summary>
        ///  Gets the directory path of the folder the user picked.
        ///  Sets the directory path of the initial folder shown in the dialog box.
        /// </summary>
        [Browsable(true)]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.SelectedPathEditor", typeof(UITypeEditor))]
        [Localizable(true)]
        [Category("FolderBrowsing")]
        [Description("FolderBrowserDialog SelectedPath")]
        public string SelectedPath
        {
            get => _selectedPath;
            set => _selectedPath = value ?? string.Empty;
        }

        /// <summary>
        ///  Gets or sets the initial directory displayed by the folder browser dialog.
        /// </summary>
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.InitialDirectoryEditor", typeof(UITypeEditor))]
        [Category("FolderBrowsing")]
        [Description("FolderBrowserDialog initial Dirextory")]
        public string InitialDirectory
        {
            get => _initialDirectory;
            set => _initialDirectory = value ?? string.Empty;
        }

        /// <summary>
        ///  Gets/sets the root node of the directory tree.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(Environment.SpecialFolder.Desktop)]
        [Localizable(false)]
        [Category("FolderBrowsing")]
        [Description("FolderBrowserDialog RootFolder")]
        [TypeConverter(typeof(SpecialFolderEnumConverter))]
        public Environment.SpecialFolder RootFolder
        {
            get => _rootFolder;
            set
            {
                if (!Enum.IsDefined(typeof(Environment.SpecialFolder), value))
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(Environment.SpecialFolder));
                }

                _rootFolder = value;
            }
        }

        /// <summary>
        ///  Gets or sets a description to show above the folders. Here you can provide
        ///  instructions for selecting a folder.
        /// </summary>
        [Browsable(true)]
        [DefaultValue("")]
        [Localizable(true)]
        [Category("FolderBrowsing")]
        [Description("FolderBrowserDialog Description")]
        public string Description
        {
            get => _descriptionText;
            set => _descriptionText = value ?? string.Empty;
        }

        /// <summary>
        ///  Gets or sets a value that indicates whether to use the value of the <see cref="Description" /> property
        ///  as the dialog title for Vista style dialogs. This property has no effect on old style dialogs.
        /// </summary>
        /// <value><see langword="true" /> to indicate that the value of the <see cref="Description" /> property is used as dialog title; <see langword="false" />
        ///  to indicate the value is added as additional text to the dialog. The default is <see langword="false" />.</value>
        [Browsable(true)]
        [DefaultValue(false)]
        [Localizable(true)]
        [Category("FolderBrowsing")]
        [Description("FolderBrowserDialog Use Description For Title")]
        public bool UseDescriptionForTitle { get; set; }

        private bool UseVistaDialogInternal => AutoUpgradeEnabled && SystemInformation.BootMode == BootMode.Normal;

        /// <summary>
        ///  Resets all properties to their default values.
        /// </summary>
        public override void Reset()
        {
            _rootFolder = Environment.SpecialFolder.Desktop;
            _descriptionText = string.Empty;
            _selectedPath = string.Empty;
            _initialDirectory = string.Empty;
            ShowNewFolderButton = true;
            ClientGuid = null;
        }

        /// <summary>
        ///  Displays a folder browser dialog box.
        /// </summary>
        /// <param name="hWndOwner">A handle to the window that owns the folder browser dialog.</param>
        /// <returns>
        ///  <see langword="true" /> if the folder browser dialog was successfully run; otherwise, <see langword="false" />.
        /// </returns>
        protected override bool RunDialog(IntPtr hWndOwner)
        {
            // If running the Vista dialog fails (e.g. on Server Core), we fall back to the
            // legacy dialog.
            if (UseVistaDialogInternal && TryRunDialogVista(hWndOwner, out bool returnValue))
                return returnValue;

            return RunDialogOld(hWndOwner);
        }

        private bool TryRunDialogVista(IntPtr owner, out bool returnValue)
        {
            IFileOpenDialog dialog;
            try
            {
                // Creating the Vista dialog can fail on Windows Server Core, even if the
                // Server Core App Compatibility FOD is installed.
                HRESULT hr = PI.CoCreateInstance(
                    ref CLSID.FileOpenDialog,
                    IntPtr.Zero,
                    PI.CLSCTX.INPROC_SERVER | PI.CLSCTX.LOCAL_SERVER | PI.CLSCTX.REMOTE_SERVER,
                    ref PI.ActiveX.IID_IUnknown,
                    out var obj);
                if (!hr.Succeeded())
                {
                    Marshal.ThrowExceptionForHR((int)hr);
                }

                dialog = (IFileOpenDialog)obj;
            }
            catch (COMException)
            {
                returnValue = false;
                return false;
            }

            try
            {
                SetDialogProperties(dialog);
                HRESULT hr = dialog.Show(owner);
                if (!hr.Succeeded())
                {
                    if (hr == HRESULT.ERROR_CANCELLED)
                    {
                        returnValue = false;
                        return true;
                    }

                    throw Marshal.GetExceptionForHR((int)hr);
                }

                GetResult(dialog);
                returnValue = true;
                return true;
            }
            finally
            {
                if (dialog != null)
                {
                    Marshal.FinalReleaseComObject(dialog);
                }
            }
        }

        private void SetDialogProperties(IFileDialog dialog)
        {
            if (ClientGuid is { } clientGuid)
            {
                // IFileDialog::SetClientGuid should be called immediately after creation of the dialog object.
                // https://docs.microsoft.com/windows/win32/api/shobjidl_core/nf-shobjidl_core-ifiledialog-setclientguid#remarks
                dialog.SetClientGuid(clientGuid);
            }

            // Description
            if (!string.IsNullOrEmpty(_descriptionText))
            {
                if (UseDescriptionForTitle)
                {
                    dialog.SetTitle(_descriptionText);
                }
                else
                {
                    IFileDialogCustomize customize = (IFileDialogCustomize)dialog;
                    customize.AddText(0, _descriptionText);
                }
            }

            dialog.SetOptions(FOS.PICKFOLDERS | FOS.FORCEFILESYSTEM | FOS.FILEMUSTEXIST);

            if (!string.IsNullOrEmpty(_initialDirectory))
            {
                try
                {
                    IShellItem initialDirectory = PI.GetShellItemForPath(_initialDirectory);

                    dialog.SetDefaultFolder(initialDirectory);
                    dialog.SetFolder(initialDirectory);
                }
                catch (FileNotFoundException)
                {
                }
            }

            if (!string.IsNullOrEmpty(_selectedPath))
            {
                string parent = Path.GetDirectoryName(_selectedPath);
                if (parent is null || !string.IsNullOrEmpty(_initialDirectory) || !Directory.Exists(parent))
                {
                    dialog.SetFileName(_selectedPath);
                }
                else
                {
                    string folder = Path.GetFileName(_selectedPath);
                    dialog.SetFolder(CreateItemFromParsingName(parent));
                    dialog.SetFileName(folder);
                }
            }
        }

        private static IShellItem CreateItemFromParsingName(string path)
        {
            Guid guid = typeof(IShellItem).GUID;
            HRESULT hr = PI.SHCreateItemFromParsingName(path, IntPtr.Zero, ref guid, out object item);
            if (hr != HRESULT.S_OK)
            {
                throw new Win32Exception((int)hr);
            }

            return (IShellItem)item;
        }

        private void GetResult(IFileDialog dialog)
        {
            dialog.GetResult(out IShellItem item);
            HRESULT hr = item.GetDisplayName(SIGDN.FILESYSPATH, out _selectedPath);
            if (!hr.Succeeded())
            {
                throw Marshal.GetExceptionForHR((int)hr);
            }
        }

        private unsafe bool RunDialogOld(IntPtr hWndOwner)
        {
            PI.SHGetSpecialFolderLocation(hWndOwner, (int)_rootFolder, out PI.CoTaskMemSafeHandle listHandle);
            if (listHandle.IsInvalid)
            {
                PI.SHGetSpecialFolderLocation(hWndOwner, (int)Environment.SpecialFolder.Desktop, out listHandle);
                if (listHandle.IsInvalid)
                {
                    throw new InvalidOperationException(@"FolderBrowserDialog No Root Folder");
                }
            }

            using (listHandle)
            {
                uint mergedOptions = PI.BrowseInfoFlags.BIF_NEWDIALOGSTYLE;
                if (!ShowNewFolderButton)
                {
                    mergedOptions |= PI.BrowseInfoFlags.BIF_NONEWFOLDERBUTTON;
                }

                // The SHBrowserForFolder dialog is OLE/COM based, and documented as only being safe to use under the STA
                // threading model if the BIF_NEWDIALOGSTYLE flag has been requested (which we always do in mergedOptions
                // above). So make sure OLE is initialized, and throw an exception if caller attempts to invoke dialog
                // under the MTA threading model (...dialog does appear under MTA, but is totally non-functional).
                if (Control.CheckForIllegalCrossThreadCalls && Application.OleRequired() != System.Threading.ApartmentState.STA)
                {
                    throw new ThreadStateException(@"DebuggingExceptionOnly: ThreadMustBeSTA");
                }

                var callback = new PI.BrowseCallbackProc(FolderBrowserDialog_BrowseCallbackProc);
                IntPtr hglobal = IntPtr.Zero;

                try
                {
                    hglobal = Marshal.AllocHGlobal(260 * Marshal.SystemDefaultCharSize);
                    {
                        var bi = new PI.BROWSEINFO
                        {
                            pidlRoot = listHandle,
                            hwndOwner = hWndOwner,
                            pszDisplayName = hglobal,
                            lpszTitle = _descriptionText,
                            ulFlags = mergedOptions,
                            lpfn = callback,
                            lParam = IntPtr.Zero,
                            iImage = 0
                        };

                        // Show the dialog
                        using PI.CoTaskMemSafeHandle browseHandle = PI.SHBrowseForFolderW(ref bi);
                        if (browseHandle.IsInvalid)
                        {
                            return false;
                        }

                        // Retrieve the path from the IDList.
                        PI.SHGetPathFromIDListLongPath(browseHandle.DangerousGetHandle(), out _selectedPath);
                        GC.KeepAlive(callback);
                        return true;
                    }
                }
                finally
                {
                    if (hglobal != IntPtr.Zero)
                        Marshal.FreeHGlobal(hglobal);
                }
            }
        }

        internal enum BFFM : uint
        {
            INITIALIZED = 1,
            SELCHANGED = 2,
            VALIDATEFAILEDA = 3,
            VALIDATEFAILEDW = 4,
            IUNKNOWN = 5,
            ENABLEOK = PI.WM_.USER + 101,
            SETSELECTIONW = PI.WM_.USER + 103,
            SETSTATUSTEXTW = PI.WM_.USER + 104,
            SETOKTEXT = PI.WM_.USER + 105,
            SETEXPANDED = PI.WM_.USER + 106
        }

        /// <summary>
        ///  Callback function used to enable/disable the OK button,
        ///  and select the initial folder.
        /// </summary>
        protected virtual int FolderBrowserDialog_BrowseCallbackProc(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData)
        {
            switch ((BFFM)msg)
            {
                case BFFM.INITIALIZED:
                    // Indicates the browse dialog box has finished initializing. The lpData value is zero.

                    if (_initialDirectory.Length != 0)
                    {
                        // Try to expand the folder specified by initialDir
                        PI.SendMessage(hwnd, (uint)BFFM.SETEXPANDED, PI.FromBool(true), _initialDirectory);
                    }

                    if (_selectedPath.Length != 0)
                    {
                        // Try to select the folder specified by selectedPath
                        PI.SendMessage(hwnd, (uint)BFFM.SETSELECTIONW, PI.FromBool(true), _selectedPath);
                    }

                    break;
                case BFFM.SELCHANGED:
                    {
                        // Indicates the selection has changed. The lpData parameter points to the item identifier list for the newly selected item.
                        IntPtr selectedPidl = lParam;
                        if (selectedPidl != IntPtr.Zero)
                        {
                            // Try to retrieve the path from the IDList
                            var isFileSystemFolder = PI.SHGetPathFromIDListLongPath(selectedPidl, out _);
                            PI.SendMessage(hwnd, (int)BFFM.ENABLEOK, IntPtr.Zero, PI.FromBool(isFileSystemFolder));
                        }
                    }
                    break;
            }

            return 0;
        }
    }
}