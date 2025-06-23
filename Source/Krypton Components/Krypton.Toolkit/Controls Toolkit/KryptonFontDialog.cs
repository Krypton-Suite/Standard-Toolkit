#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Represents a common dialog box that displays a list of fonts
/// that are currently installed on the system.
/// </summary>
[ToolboxBitmap(typeof(FontDialog), "ToolboxBitmaps.KryptonFontDialog.png"),
 Description("Displays a Kryptonised version of the standard Font dialog, that prompts the user to choose a font from those installed on the local computer.")]
public class KryptonFontDialog : FontDialog
{
    private readonly CommonDialogHandler _commonDialogHandler;
    private bool _displayExtendedColorsButton;

    /// <summary>
    /// Changes the title of the common Font Dialog
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible)]
    public string Title
    {
        get => _commonDialogHandler.Title;
        set => _commonDialogHandler.Title = value;
    }

    /// <summary>
    /// Changes the default Icon to Developer set
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Icon Icon
    {
        get => _commonDialogHandler.Icon;
        set => _commonDialogHandler.Icon = value;
    }

    /// <summary>
    /// Changes the default Icon to Developer set
    /// </summary>
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ShowIcon
    {
        get => _commonDialogHandler.ShowIcon;
        set => _commonDialogHandler.ShowIcon = value;
    }

    /// <summary>
    /// Represents a common dialog box that displays a list of fonts
    /// that are currently installed on the system.
    /// </summary>
    public KryptonFontDialog() =>
        _commonDialogHandler = new CommonDialogHandler(true)
        {
            Icon = DialogImageResources.font,
            ShowIcon = false
        };
    /// <summary>
    /// Display the Legacy Extended colours choice
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool DisplayExtendedColorsButton
    {
        get => _displayExtendedColorsButton;
        set
        {
            _displayExtendedColorsButton = value;
            if (value) // just make life easier to enforce placement.
            {
                ShowColor = true;
            }
        }
    }

    /// <summary>
    /// Place an informative area at the bottom of the form stating if this will also be used on printers
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool DisplayIsPrinterFontDescription
    {
        get
        {
            // internal bool GetOption(int option) => (uint) (this.options & option) > 0U;
            var funcSetOption = typeof(FontDialog).GetMethod(@"GetOption", BindingFlags.NonPublic | BindingFlags.Instance);
            return (bool)funcSetOption!.Invoke(this, [0x02])!;
        }
        set
        {
            //internal void SetOption(int option, bool value)
            var funcSetOption = typeof(FontDialog).GetMethod(@"SetOption", BindingFlags.NonPublic | BindingFlags.Instance);
            funcSetOption!.Invoke(this, [0x02, value]);
        }
    }


    //protected override bool RunDialog(IntPtr hWndOwner)
    //{
    //    var ret = base.RunDialog(hWndOwner);
    //    //return ret;// || _commonDialogHandler._T;
    //}

    private const int CLR_COMBOBOX_ID = 1139;
    private IntPtr _clrComboBoxHwnd;
    /// <inheritdoc />
    protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
    {
        var (handled, retValue) = _commonDialogHandler.HookProc(hWnd, msg, wparam, lparam);
        if (_displayExtendedColorsButton
            && msg == PI.WM_.INITDIALOG
           )
        {
            // Identify the color comboBox
            var clrComboBox = _commonDialogHandler.Controls.FirstOrDefault(ctl => ctl.DlgCtrlId == CLR_COMBOBOX_ID);
            if (clrComboBox != null)
            {
                _clrComboBoxHwnd = clrComboBox.hWnd;
                var rcClient = clrComboBox.WinInfo.rcClient;
                var lpPoint = new PI.POINT(rcClient.left, rcClient.top);
                PI.ScreenToClient(hWnd, ref lpPoint);
                clrComboBox.ClientLocation = new Point(lpPoint.X, lpPoint.Y + 2);
                clrComboBox.Size = new Size(rcClient.right - rcClient.left, rcClient.bottom - rcClient.top + 4);
                var panel = new KryptonPanel
                {
                    Size = clrComboBox.Size
                };
                panel.Location = clrComboBox.ClientLocation;
                PI.SetParent(panel.Handle, PI.GetParent(clrComboBox.hWnd));
                var kryptonColorButton1 = new KryptonColorButton
                {
                    Dock = DockStyle.Fill,
                    Name = "kryptonColorButton1",
                    SelectedColor = Color,
                    Splitter = false,
                    SchemeStandard = ColorScheme.OfficeThemes,
                    SchemeThemes = ColorScheme.Basic16
                };
                panel.Controls.Add(kryptonColorButton1);
                kryptonColorButton1.Values.Text = Color.Name;
                kryptonColorButton1.StateCommon.Content.AdjacentGap = 3;
                kryptonColorButton1.StateCommon.Content.Image!.ImageH = PaletteRelativeAlign.Near;
                kryptonColorButton1.StateCommon.Content.Image.ImageV = PaletteRelativeAlign.Center;
                kryptonColorButton1.StateCommon.Content.Padding = new Padding(5, 0, 0, 0);
                kryptonColorButton1.StateCommon.Content.ShortText.TextH = PaletteRelativeAlign.Near;
                kryptonColorButton1.StateCommon.Content.ShortText.TextV = PaletteRelativeAlign.Center;
                kryptonColorButton1.SelectedColorChanged += (_, e) =>
                {
                    kryptonColorButton1.Values.Text = e.Color.Name;
                    Color = e.Color;
                    var stringColor = Marshal.StringToHGlobalUni(e.Color.Name);
                    var curIndex = PI.SendDlgItemMessage(hWnd, CLR_COMBOBOX_ID, PI.WM_.CB_FINDSTRING, IntPtr.Zero, stringColor);
                    if (curIndex.ToInt32() == -1)
                    {
                        curIndex = PI.SendDlgItemMessage(hWnd, CLR_COMBOBOX_ID, PI.WM_.CB_ADDSTRING, IntPtr.Zero, stringColor);
                    }

                    PI.SendDlgItemMessage(hWnd, CLR_COMBOBOX_ID, PI.WM_.CB_SETITEMDATA, curIndex, (IntPtr)ColorTranslator.ToWin32(e.Color));
                    PI.SendDlgItemMessage(hWnd, CLR_COMBOBOX_ID, PI.WM_.CB_SETCURSEL, curIndex, IntPtr.Zero);
                    PI.PostMessage(hWnd, PI.WM_.COMMAND, PI.MakeWParam(CLR_COMBOBOX_ID, (int)PI.CBN_.SELENDOK), _clrComboBoxHwnd);
                    PI.PostMessage(hWnd, PI.WM_.COMMAND, PI.MakeWParam(CLR_COMBOBOX_ID, (int)PI.CBN_.SELCHANGE), _clrComboBoxHwnd);
                };
                clrComboBox.Button = kryptonColorButton1;
                PI.ShowWindow(clrComboBox.hWnd, PI.ShowWindowCommands.SW_HIDE);

            }
        }

        if (!handled)
        {
            if (msg == PI.WM_.PRINTCLIENT)
            {
                // Supposedly finished init, so go finalise the checkboxes
                foreach (var control in _commonDialogHandler.Controls)
                {
                    if (control.Button is KryptonCheckBox checkBox)
                    {
                        var state = PI.IsDlgButtonChecked(hWnd, control.DlgCtrlId);
                        checkBox.Checked = state != PI.BST_.UNCHECKED;
                    }
                }
            }

            Debug.WriteLine(@"0x{0:X} : {1}", msg, hWnd);
        }

        return handled ? retValue : base.HookProc(hWnd, msg, wparam, lparam);
    }

}