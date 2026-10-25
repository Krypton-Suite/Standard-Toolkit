#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2025. All rights reserved. 
 *  
 */
#endregion


// ReSharper disable ConvertToAutoProperty
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

// ReSharper disable UnusedVariable
#pragma warning disable IDE1006 // Naming Styles

namespace Krypton.Toolkit;

// Unashamedly stolen from .Net 6 Framework
// Then Applied ReSharper
// And then applied some .net48 framework code to remove the unsafes and deal with lower versions of window OS
/// <summary>
///  Allows users to select a printer and choose which portions of the document to print.
/// Note: Does not support UseExDialog 
/// </summary>
/// <remarks>
/// The PrintDlgEx does not have a "Good" way of grabbing the controls after they have been created.
/// So have decided to keep the code commented out (It works as a control, just not a Kryptonised one, and just needs to have false for the embedding)
/// This may go into the extended toolkit as a "Full replacement" if it is deemed necessary.
/// </remarks>
[DefaultProperty(nameof(Document))]
[ToolboxBitmap(typeof(PrintDialog), "ToolboxBitmaps.KryptonPrintDialog.png")]
[Description(nameof(PrintDialog))]
[Designer("System.Windows.Forms.Design.PrintDialogDesigner")]
public class KryptonPrintDialog : /*!! sealed PrintDialog !!*/ CommonDialog
{
    private readonly CommonDialogHandler _commonDialogHandler;

    private const PD printRangeMask = PD.ALLPAGES | PD.PAGENUMS | PD.SELECTION | PD.CURRENTPAGE;

    // If PrintDocument != null, settings == printDocument.PrinterSettings
    private PrinterSettings? settings;
    private PrintDocument? printDocument;

    //private bool _useExDialog;

    /// <summary>
    /// Changes the title of the common Print Dialog
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
    public string Title
    {
        get => _commonDialogHandler.Title;
        set => _commonDialogHandler.Title = value;
    }

    /// <summary>
    /// Changes the default Icon to Developer set
    /// </summary>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
    public Icon Icon
    {
        get => _commonDialogHandler.Icon;
        set => _commonDialogHandler.Icon = value;
    }

    /// <summary>
    /// Changes the default Icon to Developer set
    /// </summary>
    [DefaultValue(false)]
    public bool ShowIcon
    {
        get => _commonDialogHandler.ShowIcon;
        set => _commonDialogHandler.ShowIcon = value;
    }

    // Implementing "current page" would require switching to PrintDlgEx, which is windows 2000 and later only

    /// <summary>
    ///  Initializes a new instance of the <see cref='KryptonPrintDialog'/> class.
    /// </summary>
    public KryptonPrintDialog() =>
        _commonDialogHandler = new CommonDialogHandler(true)
        {
            ClickCallback = ClickCallback,
            Icon = DialogImageResources.Printer_V10,
            ShowIcon = false
        };

    private void ClickCallback(CommonDialogHandler.Attributes originalControl)
    {
        // When the radio button is clicked
        // Check what buttons need to be unchecked
        if (originalControl.Button is not KryptonRadioButton krbo)
        {
            return;
        }

        foreach (var control in _commonDialogHandler.Controls)
        {
            if (((control.WinInfo.dwStyle & PI.BS_.AUTORADIOBUTTON) == PI.BS_.AUTORADIOBUTTON)
                || ((control.WinInfo.dwStyle & PI.BS_.RADIOBUTTON) == PI.BS_.RADIOBUTTON))
            {
                if (control.Button is KryptonRadioButton krb)
                {
                    krb.Checked = false;
                }
            }
        }
        krbo.Checked = true;

    }

    private IntPtr? _editHwnd;
    private KryptonCheckBox? _collateCheckbox;

    /// <inheritdoc />
    protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
    {
        var (handled, retValue) = _commonDialogHandler.HookProc(hWnd, msg, wparam, lparam);
        if (msg == PI.WM_.INITDIALOG)
        {
            _editHwnd = _commonDialogHandler.Controls.First(ctl => ctl.DlgCtrlId == 0x00000482).hWnd;
            _collateCheckbox = _commonDialogHandler.Controls.First(ctl => ctl.DlgCtrlId == 0x00000411).Button as KryptonCheckBox;
        }

        if (!handled)
        {
            switch (msg)
            {
                case PI.WM_.COMMAND
                    when (_editHwnd == lparam)
                         && (PI.HIWORD(wparam) == PI.EN_CHANGE):
                {
                    var text = new StringBuilder(8);
                    PI.GetWindowText(_editHwnd.Value, text, 8);
                    if (_collateCheckbox != null)
                    {
                        _collateCheckbox.Enabled = int.Parse(text.ToString()) > 1;
                    }
                    break;
                }
                case PI.WM_.PRINTCLIENT:
                {
                    // Supposedly finished init, so go finalise the checkboxes and radios
                    foreach (var control in _commonDialogHandler.Controls)
                    {
                        switch (control.Button)
                        {
                            case KryptonCheckBox checkBox:
                            {
                                var state = PI.IsDlgButtonChecked(hWnd, control.DlgCtrlId);
                                checkBox.Checked = state != PI.BST_.UNCHECKED;
                                break;
                            }
                            case KryptonRadioButton radioBut:
                            {
                                var state = PI.IsDlgButtonChecked(hWnd, control.DlgCtrlId);
                                radioBut.Checked = state != PI.BST_.UNCHECKED;
                                break;
                            }
                        }
                    }

                    break;
                }
                case PI.WM_.ERASEBKGND:
                    // Got to prevent the CommonDialog redrawing over the KryptonControls !!
                    return IntPtr.Zero;
            }

            Debug.WriteLine(@"0x{0:X} : {1}", msg, hWnd);
        }

        return handled ? retValue : base.HookProc(hWnd, msg, wparam, lparam);
    }

    /// <summary>
    ///  Gets or sets a value indicating whether the Current Page option button is enabled.
    /// </summary>
    [DefaultValue(false)]
    [Description(@"Allow Current Page: whether the Current Page option button is enabled")]
    public bool AllowCurrentPage { get; set; }

    /// <summary>
    ///  Gets or sets a value indicating whether the Pages option button is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"Allow Pages: whether the Pages option button is enabled")]
    public bool AllowSomePages { get; set; }

    /// <summary>
    ///  Gets or sets a value indicating whether the Print to file check box is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@"Allow Print To File: whether the Print to file check box is enabled")]
    public bool AllowPrintToFile { get; set; }

    /// <summary>
    ///  Gets or sets a value indicating whether the From... To... Page option button is enabled.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"Allow Selection: whether the From... To... Page option button is enabled")]
    public bool AllowSelection { get; set; }

    /// <summary>
    ///  Gets or sets a value indicating the <see cref='PrintDocument'/> used to obtain Drawing.Printing.PrinterSettings.
    /// </summary>
    [Category(@"Data")]
    [DefaultValue(null)]
    [Description(nameof(Document))]
    public PrintDocument? Document
    {
        get => printDocument;
        set
        {
            printDocument = value;
            settings = printDocument is null
                ? new PrinterSettings()
                : printDocument.PrinterSettings;
        }
    }

    private PageSettings? PageSettings => Document is null ? PrinterSettings.DefaultPageSettings : Document.DefaultPageSettings;

    /// <summary>
    ///  Gets or sets the Drawing.Printing.PrinterSettings the
    ///  dialog box will be modifying.
    /// </summary>
    [Category(@"Data")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description(@"Printer Settings")]
    [AllowNull]
    public PrinterSettings PrinterSettings
    {
        get => settings ??= new PrinterSettings();
        set
        {
            if (value != PrinterSettings)
            {
                settings = value;
                printDocument = null;
            }
        }
    }

    /// <summary>
    ///  Gets or sets a value indicating whether the Print to file check box is checked.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"Print To File: whether the Print to file check box is checked")]
    public bool PrintToFile { get; set; }

    /// <summary>
    ///  Gets or sets a value indicating whether the Help button is Displayed.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(false)]
    [Description(@"Show Help")]
    public bool ShowHelp { get; set; }

    /// <summary>
    ///  Gets or sets a value indicating whether the Network button is Displayed.
    /// </summary>
    [Category(@"Behavior")]
    [DefaultValue(true)]
    [Description(@"Show Network: whether the Network button is Displayed")]
    public bool ShowNetwork { get; set; } = true;

    ///// <summary>
    /////  UseEXDialog = true means to use the EX versions of the dialogs and to ignore the
    /////  ShowHelp &amp; ShowNetwork properties.
    /////  UseEXDialog = false means to never use the EX versions of the dialog.
    /////  ShowHelp &amp; ShowNetwork will work in this case.
    ///// </summary>
    //[DefaultValue(false)]
    //[Description(@"UseEX Dialog")]
    //public bool UseEXDialog
    //{
    //    get => _useExDialog;
    //    set => _useExDialog = value;
    //}

    private PD GetFlags()
    {
        var flags = PD.ALLPAGES;

        // Only set this flag when using PRINTDLG and PrintDlg,
        // and not when using PrintDlgEx and PRINTDLGEX.
        //            if (!UseEXDialog || Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 5)
        {
            flags |= PD.ENABLEPRINTHOOK;
        }

        if (!AllowCurrentPage)
        {
            flags |= PD.NOCURRENTPAGE;
        }

        if (!AllowSomePages)
        {
            flags |= PD.NOPAGENUMS;
        }

        if (!AllowPrintToFile)
        {
            flags |= PD.DISABLEPRINTTOFILE;
        }

        if (!AllowSelection)
        {
            flags |= PD.NOSELECTION;
        }

        if (PrinterSettings != null)
        {
            flags |= (PD)PrinterSettings.PrintRange;

            if (PrintToFile)
            {
                flags |= PD.PRINTTOFILE;
            }

            if (ShowHelp)
            {
                flags |= PD.SHOWHELP;
            }

            if (!ShowNetwork)
            {
                flags |= PD.NONETWORKBUTTON;
            }

            if (PrinterSettings.Collate)
            {
                flags |= PD.COLLATE;
            }
        }

        return flags;
    }

    /// <summary>
    ///  Resets all options, the last selected printer, and the page
    ///  settings to their default values.
    /// </summary>
    public override void Reset()
    {
        AllowCurrentPage = false;
        AllowSomePages = false;
        AllowPrintToFile = true;
        AllowSelection = false;
        printDocument = null;
        PrintToFile = false;
        settings = null;
        ShowHelp = false;
        ShowNetwork = true;
    }

    protected override bool RunDialog(IntPtr hwndOwner) =>
        //if (!this.UseEXDialog || Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 5)
        ShowPrintDialog(hwndOwner, CreatePRINTDLG());

    //return ShowPrintDialog(hwndOwner, CreatePRINTDLGEX());
    internal static PRINTDLG CreatePRINTDLG()
    {
        PRINTDLG printdlg = IntPtr.Size != 4 ? new PRINTDLG_64() : new PRINTDLG_32();
        printdlg.lStructSize = Marshal.SizeOf(printdlg);
        printdlg.hwndOwner = IntPtr.Zero;
        printdlg.hDevMode = IntPtr.Zero;
        printdlg.hDevNames = IntPtr.Zero;
        printdlg.Flags = 0;
        printdlg.hDC = IntPtr.Zero;
        printdlg.nFromPage = 1;
        printdlg.nToPage = 1;
        printdlg.nMinPage = 0;
        printdlg.nMaxPage = 9999;
        printdlg.nCopies = 1;
        printdlg.hInstance = IntPtr.Zero;
        printdlg.lCustData = IntPtr.Zero;
        printdlg.lpfnPrintHook = null;
        printdlg.lpfnSetupHook = null;
        printdlg.lpPrintTemplateName = null;
        printdlg.lpSetupTemplateName = null;
        printdlg.hPrintTemplate = IntPtr.Zero;
        printdlg.hSetupTemplate = IntPtr.Zero;
        return printdlg;
    }

    private bool ShowPrintDialog(IntPtr hwndOwner, PRINTDLG data)
    {
        data.nFromPage = 1;
        data.nToPage = 1;
        data.nMinPage = 0;
        data.nMaxPage = 9999;
        data.Flags = GetFlags();
        data.nCopies = PrinterSettings.Copies;
        data.hwndOwner = hwndOwner;

        // ReSharper disable once RedundantDelegateCreation
        data.lpfnPrintHook = new PI.WndProc(HookProc);

        try
        {
            data.hDevMode = PageSettings is null ? PrinterSettings.GetHdevmode() : PrinterSettings.GetHdevmode(PageSettings);

            data.hDevNames = PrinterSettings.GetHdevnames();
        }
        catch (InvalidPrinterException)
        {
            // Leave those fields null; Windows will fill them in
            data.hDevMode = IntPtr.Zero;
            data.hDevNames = IntPtr.Zero;
        }

        try
        {
            // Windows doesn't like it if page numbers are invalid
            if (AllowSomePages)
            {
                if (PrinterSettings.FromPage < PrinterSettings.MinimumPage
                    || PrinterSettings.FromPage > PrinterSettings.MaximumPage)
                {
                    throw new ArgumentException($@"PageOutOfRange [{PrinterSettings.FromPage}]");
                }

                if (PrinterSettings.ToPage < PrinterSettings.MinimumPage
                    || PrinterSettings.ToPage > PrinterSettings.MaximumPage)
                {
                    throw new ArgumentException($@"PageOutOfRange [{PrinterSettings.ToPage}]");
                }

                if (PrinterSettings.ToPage < PrinterSettings.FromPage)
                {
                    throw new ArgumentException($@"PageOutOfRange [{PrinterSettings.FromPage}]");
                }

                data.nFromPage = (short)PrinterSettings.FromPage;
                data.nToPage = (short)PrinterSettings.ToPage;
                data.nMinPage = (short)PrinterSettings.MinimumPage;
                data.nMaxPage = (short)PrinterSettings.MaximumPage;
            }

            if (PrintDlg(data).IsFalse())
            {
#if DEBUG
                var result = PI.CommDlgExtendedError();
#endif
                return false;
            }

            UpdatePrinterSettings(data.hDevMode, data.hDevNames, data.nCopies, data.Flags, settings, PageSettings);

            PrintToFile = (data.Flags & PD.PRINTTOFILE) != 0;
            PrinterSettings.PrintToFile = PrintToFile;

            if (AllowSomePages)
            {
                PrinterSettings.FromPage = data.nFromPage;
                PrinterSettings.ToPage = data.nToPage;
            }

            // When the flag PD_USEDEVMODECOPIESANDCOLLATE is not set,
            // PRINTDLG.nCopies or PRINTDLG.nCopies indicates the number of copies the user wants
            // to print, and the PD_COLLATE flag in the Flags member indicates
            // whether the user wants to print them collated.
            if ((data.Flags & PD.USEDEVMODECOPIESANDCOLLATE) == 0)
            {
                PrinterSettings.Copies = data.nCopies;
                PrinterSettings.Collate = (data.Flags & PD.COLLATE) == PD.COLLATE;
            }

            return true;
        }
        finally
        {
            PI.GlobalFree(data.hDevMode);
            PI.GlobalFree(data.hDevNames);
        }
    }

    //internal static PRINTDLGEX CreatePRINTDLGEX()
    //{
    //    PRINTDLGEX printdlg = new PRINTDLGEX();

    //    printdlg.lStructSize = Marshal.SizeOf(printdlg);
    //    printdlg.hwndOwner = IntPtr.Zero;
    //    printdlg.hDevMode = IntPtr.Zero;
    //    printdlg.hDevNames = IntPtr.Zero;
    //    printdlg.hDC = IntPtr.Zero;
    //    printdlg.Flags = 0;
    //    printdlg.Flags2 = 0;
    //    printdlg.ExclusionFlags = 0;
    //    printdlg.nPageRanges = 0;
    //    printdlg.nMaxPageRanges = 1;
    //    printdlg.pageRanges = PI.GlobalAlloc(PI.GMEM.GPTR, (printdlg.nMaxPageRanges * Marshal.SizeOf(typeof(PRINTPAGERANGE))));
    //    printdlg.nMinPage = 0;
    //    printdlg.nMaxPage = 9999;
    //    printdlg.nCopies = 1;
    //    printdlg.hInstance = IntPtr.Zero;
    //    printdlg.lpPrintTemplateName = null;
    //    printdlg.nPropertyPages = 0;
    //    printdlg.lphPropertyPages = IntPtr.Zero;
    //    printdlg.nStartPage = START_PAGE_GENERAL;
    //    printdlg.dwResultAction = PD_RESULT.CANCEL;
    //    return printdlg;
    //}

    //internal class PrintDialogCallback : IPrintDialogCallback
    //{
    //    private readonly CommonDialogHandler _handler;
    //    private IntPtr _hDlg;

    //    public PrintDialogCallback(CommonDialogHandler handler)
    //    {
    //        _handler = handler;
    //    }

    //    public int InitDone()
    //    {
    //        // it does not give enough control over the objects being created, and some of them do not show up (i.e. the pictures and some text!)
    //        _handler.HookProc(_hDlg, PI.WM_.INITDIALOG, IntPtr.Zero, IntPtr.Zero);
    //        return 0;
    //    }

    //    public int SelectionChanged()
    //    {
    //        // Why is this fired before the InitDone ??
    //        return 0;
    //    }

    //    public int HandleMessage(IntPtr hDlg, int msg, IntPtr wParam, IntPtr lParam, IntPtr pResult)
    //    {
    //        _hDlg = hDlg;
    //        Debug.WriteLine(msg.ToString());
    //        return 0;
    //        //var (handled, retValue) = _handler.HookProc(hDlg, msg, wParam, lParam);
    //        //if (!handled)
    //        //pResult = retValue;
    //        //return handled ? 1 : 0;

    //    }
    //}

    //// Due to the nature of PRINTDLGEX vs PRINTDLG, separate but similar methods
    //// are required for showing the print dialog on Win2k and newer OS'.
    //private bool ShowPrintDialog(IntPtr hwndOwner, PRINTDLGEX data)
    //{
    //    data.Flags = GetFlags();
    //    data.nCopies = PrinterSettings.Copies;
    //    data.hwndOwner = hwndOwner;

    //    // The following works, but it does not give enough control over the objects being created, and some of them do not show up (i.e. the pictures and some text!)
    //    //data.lpCallback = new PrintDialogCallback(_commonDialogHandler);

    //    try
    //    {
    //        data.hDevMode = PageSettings is null ? PrinterSettings.GetHdevmode() : PrinterSettings.GetHdevmode(PageSettings);
    //        data.hDevNames = PrinterSettings.GetHdevnames();
    //    }
    //    catch (InvalidPrinterException)
    //    {
    //        data.hDevMode = IntPtr.Zero;
    //        data.hDevNames = IntPtr.Zero;
    //        // Leave those fields null; Windows will fill them in
    //    }

    //    try
    //    {
    //        // Windows doesn't like it if page numbers are invalid
    //        if (AllowSomePages)
    //        {
    //            if (PrinterSettings.FromPage < PrinterSettings.MinimumPage
    //                || PrinterSettings.FromPage > PrinterSettings.MaximumPage)
    //            {
    //                throw new ArgumentException($@"PageOutOfRange [{PrinterSettings.FromPage}]");
    //            }

    //            if (PrinterSettings.ToPage < PrinterSettings.MinimumPage
    //                || PrinterSettings.ToPage > PrinterSettings.MaximumPage)
    //            {
    //                throw new ArgumentException($@"PageOutOfRange [{PrinterSettings.ToPage}]");
    //            }

    //            if (PrinterSettings.ToPage < PrinterSettings.FromPage)
    //            {
    //                throw new ArgumentException($@"PageOutOfRange[{PrinterSettings.FromPage}]");
    //            }

    //            // PAGENUMS Allways set !
    //            var pageRangePtr = data.pageRanges;
    //            PRINTPAGERANGE pageRangeStruct = (PRINTPAGERANGE)Marshal.PtrToStructure(pageRangePtr, typeof(PRINTPAGERANGE));
    //            pageRangeStruct.nFromPage = PrinterSettings.FromPage;
    //            pageRangeStruct.nToPage = PrinterSettings.ToPage;
    //            Marshal.StructureToPtr(pageRangeStruct, data.pageRanges, false);

    //            data.nPageRanges = 1;

    //            data.nMinPage = PrinterSettings.MinimumPage;
    //            data.nMaxPage = PrinterSettings.MaximumPage;
    //        }

    //        //
    //        // The flags NativeMethods.PD_SHOWHELP and NativeMethods.PD_NONETWORKBUTTON don't work with
    //        // PrintDlgEx. So we have to strip them out.
    //        data.Flags &= ~(PD.SHOWHELP | PD.NONETWORKBUTTON);

    //        PI.HRESULT hr = PrintDlgEx(data);
    //        if (hr.Failed()
    //            || data.dwResultAction == PD_RESULT.CANCEL)
    //        {
    //            return false;
    //        }

    //        UpdatePrinterSettings(data.hDevMode, data.hDevNames, (short)data.nCopies, data.Flags, PrinterSettings, PageSettings);

    //        PrintToFile = (data.Flags & PD.PRINTTOFILE) != 0;
    //        PrinterSettings.PrintToFile = PrintToFile;
    //        if (AllowSomePages)
    //        {
    //            var pageRangePtr = data.pageRanges;
    //            PRINTPAGERANGE pageRangeStruct = (PRINTPAGERANGE)Marshal.PtrToStructure(pageRangePtr, typeof(PRINTPAGERANGE));
    //            PrinterSettings.FromPage = pageRangeStruct.nFromPage;
    //            PrinterSettings.ToPage = pageRangeStruct.nToPage;
    //        }

    //        // When the flag PD_USEDEVMODECOPIESANDCOLLATE is not set,
    //        // PRINTDLG.nCopies or PRINTDLG.nCopies indicates the number of copies the user wants
    //        // to print, and the PD_COLLATE flag in the Flags member indicates
    //        // whether the user wants to print them collated.
    //        if ((data.Flags & PD.USEDEVMODECOPIESANDCOLLATE) == 0)
    //        {
    //            PrinterSettings.Copies = (short)(data.nCopies);
    //            PrinterSettings.Collate = (data.Flags & PD.COLLATE) == PD.COLLATE;
    //        }

    //        // We should return true only if the user pressed the "Print" button while dismissing the dialog.
    //        return data.dwResultAction == PD_RESULT.PRINT;
    //    }
    //    finally
    //    {
    //        if (data.hDevMode != IntPtr.Zero)
    //        {
    //            PI.GlobalFree(data.hDevMode);
    //        }

    //        if (data.hDevNames != IntPtr.Zero)
    //        {
    //            PI.GlobalFree(data.hDevNames);
    //        }

    //        if (data.pageRanges != IntPtr.Zero)
    //        {
    //            PI.GlobalFree(data.pageRanges);
    //        }
    //    }
    //}

    // Due to the nature of PRINTDLGEX vs PRINTDLG, separate but similar methods
    // are required for updating the settings from the structure utilized by the dialog.
    // Take information from print dialog and put in PrinterSettings
    private static void UpdatePrinterSettings(IntPtr hDevMode, IntPtr hDevNames, short copies, PD flags, PrinterSettings? settings, PageSettings? pageSettings)
    {
        // Mode
        if (settings != null)
        {
            settings.SetHdevmode(hDevMode);
            settings.SetHdevnames(hDevNames);

            pageSettings?.SetHdevmode(hDevMode);

            //Check for Copies == 1 since we might get the Right number of Copies from hdevMode.dmCopies...
            if (settings.Copies == 1)
            {
                settings.Copies = copies;
            }

            settings.PrintRange = (PrintRange)(flags & printRangeMask);
        }
    }

    [DllImport(Libraries.Comdlg32, EntryPoint = nameof(PrintDlg), CharSet = CharSet.Auto, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static extern PI.BOOL PrintDlg_32([In, Out] PRINTDLG_32 lppd);

    [DllImport(Libraries.Comdlg32, EntryPoint = nameof(PrintDlg), CharSet = CharSet.Auto, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static extern PI.BOOL PrintDlg_64([In, Out] PRINTDLG_64 lppd);

    internal static PI.BOOL PrintDlg([In, Out] PRINTDLG lppd)
    {
        if (IntPtr.Size == 4)
        {
            if (lppd is PRINTDLG_32 lppd32)
            {
                return PrintDlg_32(lppd32);
            }

            throw new InvalidOperationException($"Expected {nameof(PRINTDLG_32)} data struct");
        }

        if (lppd is PRINTDLG_64 lppd64)
        {
            return PrintDlg_64(lppd64);
        }

        throw new InvalidOperationException($"Expected {nameof(PRINTDLG_64)} data struct");
    }

    //[DllImport(Libraries.Comdlg32, CharSet = CharSet.Auto, SetLastError = true)]
    //[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    //internal static extern PI.HRESULT PrintDlgEx([In, Out] PRINTDLGEX lppdex);

    //private const int START_PAGE_GENERAL = -1;

}

[Flags]
#pragma warning disable CA1069 // Enums values should not be duplicated
internal enum PD : uint
{
    ALLPAGES = 0x00000000,
    SELECTION = 0x00000001,
    PAGENUMS = 0x00000002,
    NOSELECTION = 0x00000004,
    NOPAGENUMS = 0x00000008,
    COLLATE = 0x00000010,
    PRINTTOFILE = 0x00000020,
    PRINTSETUP = 0x00000040,
    NOWARNING = 0x00000080,
    RETURNDC = 0x00000100,
    RETURNIC = 0x00000200,
    RETURNDEFAULT = 0x00000400,
    SHOWHELP = 0x00000800,
    ENABLEPRINTHOOK = 0x00001000,
    ENABLESETUPHOOK = 0x00002000,
    ENABLEPRINTTEMPLATE = 0x00004000,
    ENABLESETUPTEMPLATE = 0x00008000,
    ENABLEPRINTTEMPLATEHANDLE = 0x00010000,
    ENABLESETUPTEMPLATEHANDLE = 0x00020000,
    USEDEVMODECOPIES = 0x00040000,
    USEDEVMODECOPIESANDCOLLATE = 0x00040000,
    DISABLEPRINTTOFILE = 0x00080000,
    HIDEPRINTTOFILE = 0x00100000,
    NONETWORKBUTTON = 0x00200000,
    CURRENTPAGE = 0x00400000,
    NOCURRENTPAGE = 0x00800000,
    EXCLUSIONFLAGS = 0x01000000,
    USELARGETEMPLATE = 0x10000000
}
#pragma warning restore CA1069 // Enums values should not be duplicated

internal interface PRINTDLG
{
    int lStructSize { get; set; }

    IntPtr hwndOwner { get; set; }

    IntPtr hDevMode { get; set; }

    IntPtr hDevNames { get; set; }

    IntPtr hDC { get; set; }

    PD Flags { get; set; }

    short nFromPage { get; set; }

    short nToPage { get; set; }

    short nMinPage { get; set; }

    short nMaxPage { get; set; }

    short nCopies { get; set; }

    IntPtr hInstance { get; set; }

    IntPtr lCustData { get; set; }

    PI.WndProc? lpfnPrintHook { get; set; }

    PI.WndProc? lpfnSetupHook { get; set; }

    string? lpPrintTemplateName { get; set; }

    string? lpSetupTemplateName { get; set; }

    IntPtr hPrintTemplate { get; set; }

    IntPtr hSetupTemplate { get; set; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Auto)]
internal class PRINTDLG_32 : PRINTDLG
{
    private int m_lStructSize;
    private IntPtr m_hwndOwner;
    private IntPtr m_hDevMode;
    private IntPtr m_hDevNames;
    private IntPtr m_hDC;
    private PD m_Flags;
    private short m_nFromPage;
    private short m_nToPage;
    private short m_nMinPage;
    private short m_nMaxPage;
    private short m_nCopies;
    private IntPtr m_hInstance;
    private IntPtr m_lCustData;
    private PI.WndProc? m_lpfnPrintHook;
    private PI.WndProc? m_lpfnSetupHook;
    private string? m_lpPrintTemplateName;
    private string? m_lpSetupTemplateName;
    private IntPtr m_hPrintTemplate;
    private IntPtr m_hSetupTemplate;

    public int lStructSize
    {
        get => m_lStructSize;
        set => m_lStructSize = value;
    }

    public IntPtr hwndOwner
    {
        get => m_hwndOwner;
        set => m_hwndOwner = value;
    }

    public IntPtr hDevMode
    {
        get => m_hDevMode;
        set => m_hDevMode = value;
    }

    public IntPtr hDevNames
    {
        get => m_hDevNames;
        set => m_hDevNames = value;
    }

    public IntPtr hDC
    {
        get => m_hDC;
        set => m_hDC = value;
    }

    public PD Flags
    {
        get => m_Flags;
        set => m_Flags = value;
    }

    public short nFromPage
    {
        get => m_nFromPage;
        set => m_nFromPage = value;
    }

    public short nToPage
    {
        get => m_nToPage;
        set => m_nToPage = value;
    }

    public short nMinPage
    {
        get => m_nMinPage;
        set => m_nMinPage = value;
    }

    public short nMaxPage
    {
        get => m_nMaxPage;
        set => m_nMaxPage = value;
    }

    public short nCopies
    {
        get => m_nCopies;
        set => m_nCopies = value;
    }

    public IntPtr hInstance
    {
        get => m_hInstance;
        set => m_hInstance = value;
    }

    public IntPtr lCustData
    {
        get => m_lCustData;
        set => m_lCustData = value;
    }

    public PI.WndProc? lpfnPrintHook
    {
        get => m_lpfnPrintHook;
        set => m_lpfnPrintHook = value;
    }

    public PI.WndProc? lpfnSetupHook
    {
        get => m_lpfnSetupHook;
        set => m_lpfnSetupHook = value;
    }

    public string? lpPrintTemplateName
    {
        get => m_lpPrintTemplateName;
        set => m_lpPrintTemplateName = value;
    }

    public string? lpSetupTemplateName
    {
        get => m_lpSetupTemplateName;
        set => m_lpSetupTemplateName = value;
    }

    public IntPtr hPrintTemplate
    {
        get => m_hPrintTemplate;
        set => m_hPrintTemplate = value;
    }

    public IntPtr hSetupTemplate
    {
        get => m_hSetupTemplate;
        set => m_hSetupTemplate = value;
    }
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
internal class PRINTDLG_64 : PRINTDLG
{
    private int m_lStructSize;
    private IntPtr m_hwndOwner;
    private IntPtr m_hDevMode;
    private IntPtr m_hDevNames;
    private IntPtr m_hDC;
    private PD m_Flags;
    private short m_nFromPage;
    private short m_nToPage;
    private short m_nMinPage;
    private short m_nMaxPage;
    private short m_nCopies;
    private IntPtr m_hInstance;
    private IntPtr m_lCustData;
    private PI.WndProc? m_lpfnPrintHook;
    private PI.WndProc? m_lpfnSetupHook;
    private string? m_lpPrintTemplateName;
    private string? m_lpSetupTemplateName;
    private IntPtr m_hPrintTemplate;
    private IntPtr m_hSetupTemplate;

    public int lStructSize
    {
        get => m_lStructSize;
        set => m_lStructSize = value;
    }

    public IntPtr hwndOwner
    {
        get => m_hwndOwner;
        set => m_hwndOwner = value;
    }

    public IntPtr hDevMode
    {
        get => m_hDevMode;
        set => m_hDevMode = value;
    }

    public IntPtr hDevNames
    {
        get => m_hDevNames;
        set => m_hDevNames = value;
    }

    public IntPtr hDC
    {
        get => m_hDC;
        set => m_hDC = value;
    }

    public PD Flags
    {
        get => m_Flags;
        set => m_Flags = value;
    }

    public short nFromPage
    {
        get => m_nFromPage;
        set => m_nFromPage = value;
    }

    public short nToPage
    {
        get => m_nToPage;
        set => m_nToPage = value;
    }

    public short nMinPage
    {
        get => m_nMinPage;
        set => m_nMinPage = value;
    }

    public short nMaxPage
    {
        get => m_nMaxPage;
        set => m_nMaxPage = value;
    }

    public short nCopies
    {
        get => m_nCopies;
        set => m_nCopies = value;
    }

    public IntPtr hInstance
    {
        get => m_hInstance;
        set => m_hInstance = value;
    }

    public IntPtr lCustData
    {
        get => m_lCustData;
        set => m_lCustData = value;
    }

    public PI.WndProc? lpfnPrintHook
    {
        get => m_lpfnPrintHook;
        set => m_lpfnPrintHook = value;
    }

    public PI.WndProc? lpfnSetupHook
    {
        get => m_lpfnSetupHook;
        set => m_lpfnSetupHook = value;
    }

    public string? lpPrintTemplateName
    {
        get => m_lpPrintTemplateName;
        set => m_lpPrintTemplateName = value;
    }

    public string? lpSetupTemplateName
    {
        get => m_lpSetupTemplateName;
        set => m_lpSetupTemplateName = value;
    }

    public IntPtr hPrintTemplate
    {
        get => m_hPrintTemplate;
        set => m_hPrintTemplate = value;
    }

    public IntPtr hSetupTemplate
    {
        get => m_hSetupTemplate;
        set => m_hSetupTemplate = value;
    }
}

//[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
//internal class PRINTDLGEX
//{
//    public int lStructSize;
//    public IntPtr hwndOwner;
//    public IntPtr hDevMode;
//    public IntPtr hDevNames;
//    public IntPtr hDC;
//    public PD Flags;
//    public int Flags2;
//    public int ExclusionFlags;
//    public int nPageRanges;
//    public int nMaxPageRanges;
//    public IntPtr pageRanges;
//    public int nMinPage;
//    public int nMaxPage;
//    public int nCopies;
//    public IntPtr hInstance;
//    [MarshalAs(UnmanagedType.LPStr)]
//    public string lpPrintTemplateName;
//    [MarshalAs(UnmanagedType.IUnknown)]
//    public IPrintDialogCallback lpCallback;
//    public int nPropertyPages;
//    public IntPtr lphPropertyPages;
//    public int nStartPage;
//    public PD_RESULT dwResultAction;
//}

//[ComImport()
// , InterfaceType(ComInterfaceType.InterfaceIsIUnknown)
// , Guid("5852A2C3-6530-11D1-B6A3-0000F8757BF9")]
//internal interface IPrintDialogCallback
//{
//    [PreserveSig()]
//    int InitDone();

//    [PreserveSig()]
//    int SelectionChanged();
//    [PreserveSig()]
//    int HandleMessage(IntPtr hDlg, int msg, IntPtr wParam, IntPtr lParam, IntPtr pResult);
//}

//[StructLayout(LayoutKind.Sequential, Pack = 1)]
//internal struct PRINTPAGERANGE
//{
//    public int nFromPage;
//    public int nToPage;
//}

//internal enum PD_RESULT
//{
//    CANCEL = 0,
//    PRINT = 1,
//    APPLY = 2
//}

#pragma warning restore IDE1006 // Naming Styles
