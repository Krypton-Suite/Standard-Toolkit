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

using MsdnMag;

namespace Krypton.Toolkit;

/// <summary>
///  Wraps the Shell dialog window When launched.
/// </summary>
public abstract class ShellDialogWrapper
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
            if (!string.IsNullOrWhiteSpace(Title))
            {
                _commonDialogHandler.Title = Title;
            }

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
    internal ShellDialogWrapper()
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
        (var handled, var retValue) = _commonDialogHandler.HookProc(e.hWnd, e.message, e.wParam, e.lParam);
        e.retValue = retValue;
        actioned = handled;

        if (e.message == PI.WM_.INITDIALOG)
        {
            _scaleFactor = _commonDialogHandler._wrapperForm!.DeviceDpi / 96.0f;
            _commonDialogHandler._wrapperForm.Resize += FormResize;
            _commonDialogHandler._wrapperForm.MinimumSize = new SizeF(440 * _scaleFactor, 345 * _scaleFactor).ToSize();
        }
    }

    private readonly LocalCbtHook _cbt;
    private readonly LocalCallWndProc _cwp;
    private protected IntPtr _handle = IntPtr.Zero;
    private bool _alreadySetup;
    private protected float _scaleFactor;

    private protected virtual void FormResize(object? sender, EventArgs e)
    {
        if (_commonDialogHandler._wrapperForm == null)
        {
            return;
        }
        _commonDialogHandler._wrapperForm.SuspendLayout();
        // Align the button underneath the drop-down
        var clientSize = _commonDialogHandler._wrapperForm.ClientSize;
        foreach (var button in _commonDialogHandler.Controls.Where(static ctl => ctl.Button != null))
        {
            if (button.Button?.Parent is Panel panel)
            {
                switch (button.DlgCtrlId)
                {
                    // Do not use strings as they will be localised
                    case 1: // @"&Save"
                        // case @"&Open":
                        // case @"Select Folder":
                        panel.Location = new Point(
                            (int)(clientSize.Width - 116 * _scaleFactor - button.Button.Width * 1.1),
                            (int)(clientSize.Height - 12 * _scaleFactor - button.Button.Height));
                        break;
                    case 2:
                        //case @"Cancel":
                        panel.Location = new Point(
                            (int)(clientSize.Width - 30 * _scaleFactor - button.Button.Width),
                            (int)(clientSize.Height - 12 * _scaleFactor - button.Button.Height));
                        break;
                }
            }
        }
        _commonDialogHandler._wrapperForm.ResumeLayout(false);
    }

    private void WndCreated(object sender, CbtEventArgs e)
    {
        if (e.IsDialogWindow)
        {
            _alreadySetup = false;
            Console.WriteLine(@"Shell Dialog created");
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
            _commonDialogHandler._wrapperForm?.Close();
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
        Console.WriteLine(@"Shell Dialog activated");

        // Modify the Shell Dialog window
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

#if NET8_0_OR_GREATER
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
#endif //#ifndef NETFRAMEWORK

    ///// <summary>Gets or sets a value indicating whether the Help button is displayed in the file dialog box.</summary>
    ///// <returns>
    ///// <see langword="true" /> if the dialog box includes a help button; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public bool ShowHelp
    //{
    //    get => throw new System.Data.InvalidConstraintException(@"Do not use 'ShowHelp' if you want the 'new' experience!");
    //    set => throw new System.Data.InvalidConstraintException(@"Do not use 'ShowHelp' if you want the 'new' experience!");
    //}

    /// <summary>Gets or sets the file dialog box title.</summary>
    /// <returns>The file dialog box title. The default value is an empty string ("").</returns>
    [Category("Appearance")]
    [DefaultValue("")]
    [Localizable(true)]
    [Description("Gets or sets the file dialog box title")]
    [AllowNull]
    public abstract string Title { get; set; }

    /// <summary>Resets all properties to their default values.</summary>
    public abstract void Reset();

    /// <summary>Provides a string version of this object.</summary>
    /// <returns>A string version of this object.</returns>
    public new abstract string ToString();

}