#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable BuiltInTypeReferenceStyle
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CommentTypo
// ReSharper disable UnusedType.Local
// ReSharper disable MemberHidesStaticFromOuterClass
// ReSharper disable EnumUnderlyingTypeIsInt
// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedVariable
#pragma warning disable IDE0090

#pragma warning disable 649

// Note: DO NOT REMOVE!!!
using Microsoft.Win32.SafeHandles;
using static System.Runtime.InteropServices.Marshal;


namespace Krypton.Toolkit;
    internal static class Libraries
    {
        public const string Comctl32 = "comctl32.dll";
        public const string Comdlg32 = "comdlg32.dll";
        public const string DWMApi = @"dwmapi.dll";
        public const string Gdi32 = "gdi32.dll";
        public const string Gdiplus = "gdiplus.dll";
        public const string Hhctrl = "hhctrl.ocx";
        public const string Imm32 = "imm32.dll";
        public const string Kernel32 = "kernel32.dll";
        public const string NtDll = "ntdll.dll";
        public const string Ole32 = "ole32.dll";
        public const string Oleacc = "oleacc.dll";
        public const string Oleaut32 = "oleaut32.dll";
        public const string Powrprof = "Powrprof.dll";
        public const string Propsys = "Propsys.dll";
        public const string RichEdit41 = "MsftEdit.DLL";
        public const string SHCore = "SHCore.dll";
        public const string Shell32 = "shell32.dll";
        public const string Shlwapi = "shlwapi.dll";
        public const string UiaCore = "UIAutomationCore.dll";
        public const string User32 = "user32.dll";
        public const string UxTheme = "uxtheme.dll";
    }

    // inherits from SafeHandleZeroOrMinusOneIsInvalid, so IsInvalid is already implemented.
    internal sealed class SafeModuleHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        // A default constructor is required for P/Invoke to instantiate the class
        // ReSharper disable once ConvertToPrimaryConstructor
        public SafeModuleHandle()
            : base(true)
        {
        }

        protected override bool ReleaseHandle() => PI.FreeLibrary(handle);
    }

    internal partial class PI
    {
        #region statics

        internal static readonly IntPtr InvalidIntPtr = new IntPtr(-1);
        internal static readonly IntPtr LPSTR_TEXTCALLBACK = new IntPtr(-1);
        internal static readonly HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);

        /// <summary>
        ///     Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
        /// </summary>
        internal static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        /// <summary>
        ///     Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
        /// </summary>
        internal static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        /// <summary>
        ///     Places the window at the top of the Z order.
        /// </summary>
        internal static readonly IntPtr HWND_TOP = new IntPtr(0);

        /// <summary>
        ///     Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
        /// </summary>
        internal static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        internal const int BM_CLICK = 0x00F5;
        #endregion

        internal delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        internal static object? PtrToStructure(IntPtr lparam, Type cls) => Marshal.PtrToStructure(lparam, cls);
        internal static void StructureToPtr(object cls, IntPtr lparam, bool deleteOld = false) => Marshal.StructureToPtr(cls, lparam, deleteOld);

        #region Constants
        public enum DeviceCap
        {
            /// <summary>
            /// Device driver version
            /// </summary>
            DRIVERVERSION = 0,
            /// <summary>
            /// Device classification
            /// </summary>
            TECHNOLOGY = 2,
            /// <summary>
            /// Horizontal size in millimeters
            /// </summary>
            HORZSIZE = 4,
            /// <summary>
            /// Vertical size in millimeters
            /// </summary>
            VERTSIZE = 6,
            /// <summary>
            /// Horizontal width in pixels
            /// </summary>
            HORZRES = 8,
            /// <summary>
            /// Vertical height in pixels
            /// </summary>
            VERTRES = 10,
            /// <summary>
            /// Number of bits per pixel
            /// </summary>
            BITSPIXEL = 12,
            /// <summary>
            /// Number of planes
            /// </summary>
            PLANES = 14,
            /// <summary>
            /// Number of brushes the device has
            /// </summary>
            NUMBRUSHES = 16,
            /// <summary>
            /// Number of pens the device has
            /// </summary>
            NUMPENS = 18,
            /// <summary>
            /// Number of markers the device has
            /// </summary>
            NUMMARKERS = 20,
            /// <summary>
            /// Number of fonts the device has
            /// </summary>
            NUMFONTS = 22,
            /// <summary>
            /// Number of colors the device supports
            /// </summary>
            NUMCOLORS = 24,
            /// <summary>
            /// Size required for device descriptor
            /// </summary>
            PDEVICESIZE = 26,
            /// <summary>
            /// Curve capabilities
            /// </summary>
            CURVECAPS = 28,
            /// <summary>
            /// Line capabilities
            /// </summary>
            LINECAPS = 30,
            /// <summary>
            /// Polygonal capabilities
            /// </summary>
            POLYGONALCAPS = 32,
            /// <summary>
            /// Text capabilities
            /// </summary>
            TEXTCAPS = 34,
            /// <summary>
            /// Clipping capabilities
            /// </summary>
            CLIPCAPS = 36,
            /// <summary>
            /// Bitblt capabilities
            /// </summary>
            RASTERCAPS = 38,
            /// <summary>
            /// Length of the X leg
            /// </summary>
            ASPECTX = 40,
            /// <summary>
            /// Length of the Y leg
            /// </summary>
            ASPECTY = 42,
            /// <summary>
            /// Length of the hypotenuse
            /// </summary>
            ASPECTXY = 44,
            /// <summary>
            /// Shading and Blending caps
            /// </summary>
            SHADEBLENDCAPS = 45,

            /// <summary>
            /// Logical pixels inch in X
            /// </summary>
            LOGPIXELSX = 88,
            /// <summary>
            /// Logical pixels inch in Y
            /// </summary>
            LOGPIXELSY = 90,

            /// <summary>
            /// Number of entries in physical palette
            /// </summary>
            SIZEPALETTE = 104,
            /// <summary>
            /// Number of reserved entries in palette
            /// </summary>
            NUMRESERVED = 106,
            /// <summary>
            /// Actual color resolution
            /// </summary>
            COLORRES = 108,

            // Printing related DeviceCaps. These replace the appropriate Escapes
            /// <summary>
            /// Physical Width in device units
            /// </summary>
            PHYSICALWIDTH = 110,
            /// <summary>
            /// Physical Height in device units
            /// </summary>
            PHYSICALHEIGHT = 111,
            /// <summary>
            /// Physical Printable Area x margin
            /// </summary>
            PHYSICALOFFSETX = 112,
            /// <summary>
            /// Physical Printable Area y margin
            /// </summary>
            PHYSICALOFFSETY = 113,
            /// <summary>
            /// Scaling factor x
            /// </summary>
            SCALINGFACTORX = 114,
            /// <summary>
            /// Scaling factor y
            /// </summary>
            SCALINGFACTORY = 115,

            /// <summary>
            /// Current vertical refresh rate of the display device (for displays only) in Hz
            /// </summary>
            VREFRESH = 116,
            /// <summary>
            /// Vertical height of entire desktop in pixels
            /// </summary>
            DESKTOPVERTRES = 117,
            /// <summary>
            /// Horizontal width of entire desktop in pixels
            /// </summary>
            DESKTOPHORZRES = 118,
            /// <summary>
            /// Preferred blt alignment
            /// </summary>
            BLTALIGNMENT = 119
        }

        /// <summary>
        ///  Blittable version of Windows BOOL type. It is convenient in situations where
        ///  manual marshalling is required, or to avoid overhead of regular bool marshalling.
        /// </summary>
        /// <remarks>
        ///  Some Windows APIs return arbitrary integer values although the return type is defined
        ///  as BOOL. It is best to never compare BOOL to TRUE. Always use bResult != BOOL.FALSE
        ///  or bResult == BOOL.FALSE .
        /// </remarks>
        internal enum BOOL : int
        {
            FALSE = 0,
            TRUE = 1
        }

        internal enum CBN_
        {
            ERRSPACE = -1,
            SELCHANGE = 1,
            DBLCLK = 2,
            SETFOCUS = 3,
            KILLFOCUS = 4,
            EDITCHANGE = 5,
            EDITUPDATE = 6,
            DROPDOWN = 7,
            CLOSEUP = 8,
            SELENDOK = 9,
            SELENDCANCEL = 10
        }

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-isdlgbuttonchecked
        internal enum BST_ : uint
        {
            UNCHECKED = 0x0000,
            CHECKED = 0x0001,
            INDETERMINATE = 0x0002
        }

        #region ScrollBar

        internal const int SETREDRAW = 11;

        internal enum SIF_
        {
            RANGE = 0x1,
            PAGE = 0x2,
            POS = 0x4,
            DISABLENOSCROLL = 0x8,
            TRACKPOS = 0x16,
            ALL = RANGE | PAGE | POS | TRACKPOS
        }

#pragma warning disable CA1069 // Enums values should not be duplicated
        internal enum SB_
        {
            LINEUP = 0,
            LINELEFT = 0,
            LINEDOWN = 1,
            LINERIGHT = 1,
            PAGEUP = 2,
            PAGELEFT = 2,
            PAGEDOWN = 3,
            PAGERIGHT = 3,
            THUMBPOSITION = 4,
            THUMBTRACK = 5,
            TOP = 6,
            LEFT = 6,
            BOTTOM = 7,
            RIGHT = 7,
            ENDSCROLL = 8,

            HORZ = 0,
            VERT = 1,
            CTL = 2,
            BOTH = 3
        }
#pragma warning restore CA1069 // Enums values should not be duplicated

        internal enum SBM_
        {
            ENABLE_ARROWS = 0x00E4, /*not in win3.1 */
            SETSCROLLINFO = 0x00E9,
            GETSCROLLINFO = 0x00EA
        }

        internal const int LB_SETHORIZONTALEXTENT = 0x194;

        internal const int LVM_FIRST = 0x1000;
        internal const int LVM_SCROLL = LVM_FIRST + 20;

        internal enum ScrollBarType
        {
            Vertical = 0,
            Horizontal = 1
        }

#pragma warning disable CA1069 // Enums values should not be duplicated
        internal enum ESB_
        {
            ENABLE_BOTH = 0,
            DISABLE_BOTH = 3,
            DISABLE_LEFT = 1,
            DISABLE_RIGHT = 2,
            DISABLE_UP = 1,
            DISABLE_DOWN = 2,
            DISABLE_LTUP = 1,
            DISABLE_RTDN = 2
        }
#pragma warning restore CA1069 // Enums values should not be duplicated

        #endregion  ScrollBar

        #region  TreeView
        [Flags]
        internal enum TVIF_
        {
            TEXT = 0x0001, // The pszText and cchTextMax members are valid.
            IMAGE = 0x0002, // The iImage member is valid.
            PARAM = 0x04, // The lParam member is valid.
            STATE = 0x0008, // The state and stateMask members are valid.
            HANDLE = 0x020, // The hItem member is valid.
            CHILDREN = 0x0040, // The cChildren member is valid.
            SELECTEDIMAGE = 32, // The iSelectedImage member is valid.
            INTEGRAL = 128,
            STATEEX = 256,
            EXPANDEDIMAGE = 512,
            DI_SETITEM = 4096 // The tree-view control will retain the supplied information and will not request it again.This flag is valid only when processing the TVN_GETDISPINFO notification.
        }

        [Flags]
        internal enum TVIS_
        {
            BOLD, // The item is bold.
            CUT, // The item is selected as part of a cut-and-paste operation.
            DROPHILITED, // The item is selected as a drag-and-drop target.
            EXPANDED = 0x0020, // The item's list of child items is currently expanded; that is, the child items are visible. This value applies only to parent items.
            EXPANDEDONCE, // The item's list of child items has been expanded at least once. The TVN_ITEMEXPANDING and TVN_ITEMEXPANDED notification codes are not generated for parent items that have this state set in response to a TVM_EXPAND message. Using TVE_COLLAPSE and TVE_COLLAPSERESET with TVM_EXPAND will cause this state to be reset. This value applies only to parent items.
            EXPANDPARTIAL, // Version 4.70. A partially expanded tree-view item. In this state, some, but not all, of the child items are visible and the parent item's plus symbol is Displayed.
            SELECTED = 0x0002, // The item is selected.Its appearance depends on whether it has the focus.The item will be drawn using the system colors for selection.
            OVERLAYMASK, // Mask for the bits used to specify the item's overlay image index.
            STATEIMAGEMASK = 0xF000, // Mask for the bits used to specify the item's state image index.
            USERMASK = 0xF000 // Same as TVIS_STATEIMAGEMASK.
        }

        internal struct TVM_
        {
            public const int
                TVM_INSERTITEMA = 0x1100 + 0,
                TVM_INSERTITEMW = 0x1100 + 50,
                TVM_DELETEITEM = 0x1100 + 1,
                TVM_EXPAND = 0x1100 + 2,
                TVM_GETITEMRECT = 0x1100 + 4,
                TVM_GETINDENT = 0x1100 + 6,
                TVM_SETINDENT = 0x1100 + 7,
                TVM_GETIMAGELIST = 0x1100 + 8,
                TVM_SETIMAGELIST = 0x1100 + 9,
                TVM_GETNEXTITEM = 0x1100 + 10,
                TVM_SELECTITEM = 0x1100 + 11,
                TVM_GETITEMA = 0x1100 + 12,
                TVM_GETITEMW = 0x1100 + 62,
                TVM_SETITEMA = 0x1100 + 13,
                TVM_SETITEMW = 0x1100 + 63,
                TVM_EDITLABELA = 0x1100 + 14,
                TVM_EDITLABELW = 0x1100 + 65,
                TVM_GETEDITCONTROL = 0x1100 + 15,
                TVM_GETVISIBLECOUNT = 0x1100 + 16,
                TVM_HITTEST = 0x1100 + 17,
                TVM_ENSUREVISIBLE = 0x1100 + 20,
                TVM_ENDEDITLABELNOW = 0x1100 + 22,
                TVM_GETISEARCHSTRINGA = 0x1100 + 23,
                TVM_GETISEARCHSTRINGW = 0x1100 + 64,
                TVM_SETITEMHEIGHT = 0x1100 + 27,
                TVM_GETITEMHEIGHT = 0x1100 + 28,

                SETITEMA = 0x110d,
                SETITEM = 0x110d,
                SETITEMW = 0x113f,
                GETITEM = 0x110C
                //TV_FIRST = 0x1100,
                //TVM_SETBKCOLOR = (TV_FIRST + 29),
                //TVM_SETTEXTCOLOR = (TV_FIRST + 30),
                //TVM_GETLINECOLOR = (TV_FIRST + 41),
                //TVM_SETLINECOLOR = (TV_FIRST + 40),
                //TVM_SETTOOLTIPS = (TV_FIRST + 24),
                //TVM_SORTCHILDRENCB = (TV_FIRST + 21),
                ;
        }

        internal const int TVGN_ROOT = 0x0000;
        internal const int TVGN_NEXT = 0x0001;
        internal const int TVGN_PREVIOUS = 0x0002;
        internal const int TVGN_PARENT = 0x0003;
        internal const int TVGN_CHILD = 0x0004;
        internal const int TVGN_FIRSTVISIBLE = 0x0005;
        internal const int TVGN_NEXTVISIBLE = 0x0006;
        internal const int TVGN_PREVIOUSVISIBLE = 0x0007;
        internal const int TVGN_DROPHILITE = 0x0008;
        internal const int TVGN_CARET = 0x0009;

        // note: this flag has effect only on WinXP and up
        internal const int TVSI_NOSINGLEEXPAND = 0x8000;

        //TVC_UNKNOWN = 0x0000,
        //TVC_BYMOUSE = 0x0001,
        //TVC_BYKEYBOARD = 0x0002,

        internal const int TVE_COLLAPSE = 0x0001;
        internal const int TVE_EXPAND = 0x0002;

        //TVI_ROOT = (unchecked((int)0xFFFF0000)),
        //TVI_FIRST = (unchecked((int)0xFFFF0001)),

        // style
        internal const int TVS_EDITLABELS = 0x0008;
        internal const int TVS_CHECKBOXES = 0x0100;
        //TVS_HASBUTTONS = 0x0001,
        //TVS_HASLINES = 0x0002,
        //TVS_LINESATROOT = 0x0004,
        //TVS_EDITLABELS = 0x0008,
        //TVS_SHOWSELALWAYS = 0x0020,
        //TVS_RTLREADING = 0x0040,
        //TVS_CHECKBOXES = 0x0100,
        //TVS_TRACKSELECT = 0x0200,
        //TVS_FULLROWSELECT = 0x1000,
        //TVS_NONEVENHEIGHT = 0x4000,
        //TVS_INFOTIP = 0x0800,
        //TVS_NOTOOLTIPS = 0x0080,

        //TVN_SELCHANGINGA = ((0 - 400) - 1),
        //TVN_SELCHANGINGW = ((0 - 400) - 50),
        //TVN_GETINFOTIPA = ((0 - 400) - 13),
        //TVN_GETINFOTIPW = ((0 - 400) - 14),
        //TVN_SELCHANGEDA = ((0 - 400) - 2),
        //TVN_SELCHANGEDW = ((0 - 400) - 51),
        //TVN_GETDISPINFOA = ((0 - 400) - 3),
        //TVN_GETDISPINFOW = ((0 - 400) - 52),
        //TVN_SETDISPINFOA = ((0 - 400) - 4),
        //TVN_SETDISPINFOW = ((0 - 400) - 53),
        //TVN_ITEMEXPANDINGA = ((0 - 400) - 5),
        //TVN_ITEMEXPANDINGW = ((0 - 400) - 54),
        //TVN_ITEMEXPANDEDA = ((0 - 400) - 6),
        //TVN_ITEMEXPANDEDW = ((0 - 400) - 55),
        //TVN_BEGINDRAGA = ((0 - 400) - 7),
        //TVN_BEGINDRAGW = ((0 - 400) - 56),
        //TVN_BEGINRDRAGA = ((0 - 400) - 8),
        //TVN_BEGINRDRAGW = ((0 - 400) - 57),
        //TVN_BEGINLABELEDITA = ((0 - 400) - 10),
        //TVN_BEGINLABELEDITW = ((0 - 400) - 59),
        //TVN_ENDLABELEDITA = ((0 - 400) - 11),
        //TVN_ENDLABELEDITW = ((0 - 400) - 60),
        #endregion  TreeView

        [Flags]
        internal enum KEY_
        {
            NONE = 0,
            DOWN = 1,
            TOGGLED = 2
        }

        [Flags]
        internal enum SWP_ : uint
        {
            // https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-setwindowpos
            /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
            NOSIZE = 0x0001,
            /// <summary>Retains the current position (ignores X and Y parameters).</summary>
            NOMOVE = 0x0002,
            /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
            NOZORDER = 0x0004,
            /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to
            /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent
            /// window uncovered as a result of the window being moved. When this flag is set, the application must
            /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
            NOREDRAW = 0x0008,
            /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the
            /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter
            /// parameter).</summary>
            NOACTIVATE = 0x0010,
            /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
            DRAWFRAME = 0x0020,
            /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to
            /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE
            /// is sent only when the window's size is being changed.</summary>
            FRAMECHANGED = 0x0020,
            /// <summary>Displays the window.</summary>
            SHOWWINDOW = 0x0040,
            /// <summary>Hides the window.</summary>
            HIDEWINDOW = 0x0080,
            /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid
            /// contents of the client area are saved and copied back into the client area after the window is sized or
            /// repositioned.</summary>
            NOCOPYBITS = 0x0100,
            /// <summary>Does not change the owner window's position in the Z order.</summary>
            NOOWNERZORDER = 0x0200,
            /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
            NOREPOSITION = 0x0200,
            /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
            DONOTSENDCHANGINGEVENT = 0x0400,
            NOCLIENTSIZE = 0x0800,
            NOCLIENTMOVE = 0x1000,
            /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
            DEFERERASE = 0x2000,
            /// <summary>If the calling thread and the thread that owns the window are attached to different input queues,
            /// the system posts the request to the thread that owns the window. This prevents the calling thread from
            /// blocking its execution while other threads process the request.</summary>
            ASYNCWINDOWPOS = 0x4000,
            STATECHANGED = 0x8000
        }

        internal enum BeepType : uint
        {
            /// <summary>
            /// A simple windows beep
            /// </summary>
            SimpleBeep = 0xFFFFFFFF,
            /// <summary>
            /// A standard windows OK beep
            /// </summary>
            OK = 0x00,
            /// <summary>
            /// A standard windows Question beep
            /// </summary>
            Question = 0x20,
            /// <summary>
            /// A standard windows Exclamation beep
            /// </summary>
            Exclamation = 0x30,
            /// <summary>
            /// A standard windows Asterisk beep
            /// </summary>
            Asterisk = 0x40
        }

        internal enum StretchBltMode
        {
            STRETCH_ANDSCANS = 1,
            STRETCH_ORSCANS = 2,
            STRETCH_DELETESCANS = 3,
            STRETCH_HALFTONE = 4
        }

        internal enum StockObjects
        {
            WHITE_BRUSH = 0,
            LTGRAY_BRUSH = 1,
            GRAY_BRUSH = 2,
            DKGRAY_BRUSH = 3,
            BLACK_BRUSH = 4,
            NULL_BRUSH = 5,
            HOLLOW_BRUSH = NULL_BRUSH,
            WHITE_PEN = 6,
            BLACK_PEN = 7,
            NULL_PEN = 8,
            OEM_FIXED_FONT = 10,
            ANSI_FIXED_FONT = 11,
            ANSI_VAR_FONT = 12,
            SYSTEM_FONT = 13,
            DEVICE_DEFAULT_FONT = 14,
            DEFAULT_PALETTE = 15,
            SYSTEM_FIXED_FONT = 16,
            DEFAULT_GUI_FONT = 17,
            DC_BRUSH = 18,
            DC_PEN = 19
        }

        internal enum SIZE_
        {
            RESTORED = 0,// The window has been resized, but neither the SIZE_MINIMIZED nor SIZE_MAXIMIZED value applies.
            MINIMIZED = 1, // The window has been minimized.
            MAXIMIZED = 2, // The window has been maximized.
            MAXSHOW = 3, // Message is sent to all pop-up windows when some other window has been restored to its former size.
            MAXHIDE = 4 // Message is sent to all pop-up windows when some other window is maximized.
        }

        /// <summary>
        /// Flags used with the Windows API (User32.dll):GetSystemMetrics(SystemMetric smIndex)
        ///
        /// This Enum and declaration signature was written by Gabriel T. Sharp
        /// ai_productions@verizon.net or osirisgothra@hotmail.com
        /// Obtained on pinvoke.net, please contribute your code to support the wiki!
        /// </summary>
        internal enum SM_ : int
        {
            /// <summary>
            /// The flags that specify how the system arranged minimized windows. For more information, see the Remarks section in this topic.
            /// </summary>
            ARRANGE = 56,

            /// <summary>
            /// The value that specifies how the system is started:
            /// 0 Normal boot
            /// 1 Fail-safe boot
            /// 2 Fail-safe with network boot
            /// A fail-safe boot (also called SafeBoot, Safe Mode, or Clean Boot) bypasses the user startup files.
            /// </summary>
            CLEANBOOT = 67,

            /// <summary>
            /// The number of display monitors on a desktop. For more information, see the Remarks section in this topic.
            /// </summary>
            CMONITORS = 80,

            /// <summary>
            /// The number of buttons on a mouse, or zero if no mouse is installed.
            /// </summary>
            CMOUSEBUTTONS = 43,

            /// <summary>
            /// The width of a window border, in pixels. This is equivalent to the SM_CXEDGE value for windows with the 3-D look.
            /// </summary>
            CXBORDER = 5,

            /// <summary>
            /// The width of a cursor, in pixels. The system cannot create cursors of other sizes.
            /// </summary>
            CXCURSOR = 13,

            /// <summary>
            /// This value is the same as SM_CXFIXEDFRAME.
            /// </summary>
            CXDLGFRAME = 7,

            /// <summary>
            /// The width of the rectangle around the location of a first click in a double-click sequence, in pixels. ,
            /// The second click must occur within the rectangle that is defined by SM_CXDOUBLECLK and SM_CYDOUBLECLK for the system
            /// to consider the two clicks a double-click. The two clicks must also occur within a specified time.
            /// To set the width of the double-click rectangle, call SystemParametersInfo with SPI_SETDOUBLECLKWIDTH.
            /// </summary>
            CXDOUBLECLK = 36,

            /// <summary>
            /// The number of pixels on either side of a mouse-down point that the mouse pointer can move before a drag operation begins.
            /// This allows the user to click and release the mouse button easily without unintentionally starting a drag operation.
            /// If this value is negative, it is subtracted from the left of the mouse-down point and added to the right of it.
            /// </summary>
            CXDRAG = 68,

            /// <summary>
            /// The width of a 3-D border, in pixels. This metric is the 3-D counterpart of SM_CXBORDER.
            /// </summary>
            CXEDGE = 45,

            /// <summary>
            /// The thickness of the frame around the perimeter of a window that has a caption but is not sizable, in pixels.
            /// SM_CXFIXEDFRAME is the height of the horizontal border, and SM_CYFIXEDFRAME is the width of the vertical border.
            /// This value is the same as SM_CXDLGFRAME.
            /// </summary>
            CXFIXEDFRAME = 7,

            /// <summary>
            /// The width of the left and right edges of the focus rectangle that the DrawFocusRectdraws.
            /// This value is in pixels.
            /// Windows 2000:  This value is not supported.
            /// </summary>
            CXFOCUSBORDER = 83,

            /// <summary>
            /// This value is the same as SM_CXSIZEFRAME.
            /// </summary>
            CXFRAME = 32,

            /// <summary>
            /// The width of the client area for a full-screen window on the primary display monitor, in pixels.
            /// To get the coordinates of the portion of the screen that is not obscured by the system taskbar or by application desktop toolbars,
            /// call the SystemParametersInfofunction with the SPI_GETWORKAREA value.
            /// </summary>
            CXFULLSCREEN = 16,

            /// <summary>
            /// The width of the arrow bitmap on a horizontal scroll bar, in pixels.
            /// </summary>
            CXHSCROLL = 21,

            /// <summary>
            /// The width of the thumb box in a horizontal scroll bar, in pixels.
            /// </summary>
            CXHTHUMB = 10,

            /// <summary>
            /// The default width of an icon, in pixels. The LoadIcon function can load only icons with the dimensions
            /// that SM_CXICON and SM_CYICON specifies.
            /// </summary>
            CXICON = 11,

            /// <summary>
            /// The width of a grid cell for items in large icon view, in pixels. Each item fits into a rectangle of size
            /// SM_CXICONSPACING by SM_CYICONSPACING when arranged. This value is always greater than or equal to SM_CXICON.
            /// </summary>
            CXICONSPACING = 38,

            /// <summary>
            /// The default width, in pixels, of a maximized top-level window on the primary display monitor.
            /// </summary>
            CXMAXIMIZED = 61,

            /// <summary>
            /// The default maximum width of a window that has a caption and sizing borders, in pixels.
            /// This metric refers to the entire desktop. The user cannot drag the window frame to a size larger than these dimensions.
            /// A window can override this value by processing the WM_GETMINMAXINFO message.
            /// </summary>
            CXMAXTRACK = 59,

            /// <summary>
            /// The width of the default menu check-mark bitmap, in pixels.
            /// </summary>
            CXMENUCHECK = 71,

            /// <summary>
            /// The width of menu bar buttons, such as the child window close button that is used in the multiple document interface, in pixels.
            /// </summary>
            CXMENUSIZE = 54,

            /// <summary>
            /// The minimum width of a window, in pixels.
            /// </summary>
            CXMIN = 28,

            /// <summary>
            /// The width of a minimized window, in pixels.
            /// </summary>
            CXMINIMIZED = 57,

            /// <summary>
            /// The width of a grid cell for a minimized window, in pixels. Each minimized window fits into a rectangle this size when arranged.
            /// This value is always greater than or equal to SM_CXMINIMIZED.
            /// </summary>
            CXMINSPACING = 47,

            /// <summary>
            /// The minimum tracking width of a window, in pixels. The user cannot drag the window frame to a size smaller than these dimensions.
            /// A window can override this value by processing the WM_GETMINMAXINFO message.
            /// </summary>
            CXMINTRACK = 34,

            /// <summary>
            /// The amount of border padding for captioned windows, in pixels. Windows XP/2000:  This value is not supported.
            /// </summary>
            CXPADDEDBORDER = 92,

            /// <summary>
            /// The width of the screen of the primary display monitor, in pixels. This is the same value obtained by calling
            /// GetDeviceCaps as follows: GetDeviceCaps( hdcPrimaryMonitor, HORZRES).
            /// </summary>
            CXSCREEN = 0,

            /// <summary>
            /// The width of a button in a window caption or title bar, in pixels.
            /// </summary>
            CXSIZE = 30,

            /// <summary>
            /// The thickness of the sizing border around the perimeter of a window that can be resized, in pixels.
            /// SM_CXSIZEFRAME is the width of the horizontal border, and SM_CYSIZEFRAME is the height of the vertical border.
            /// This value is the same as SM_CXFRAME.
            /// </summary>
            CXSIZEFRAME = 32,

            /// <summary>
            /// The recommended width of a small icon, in pixels. Small icons typically appear in window captions and in small icon view.
            /// </summary>
            CXSMICON = 49,

            /// <summary>
            /// The width of small caption buttons, in pixels.
            /// </summary>
            CXSMSIZE = 52,

            /// <summary>
            /// The width of the virtual screen, in pixels. The virtual screen is the bounding rectangle of all display monitors.
            /// The SM_XVIRTUALSCREEN metric is the coordinates for the left side of the virtual screen.
            /// </summary>
            CXVIRTUALSCREEN = 78,

            /// <summary>
            /// The width of a vertical scroll bar, in pixels.
            /// </summary>
            CXVSCROLL = 2,

            /// <summary>
            /// The height of a window border, in pixels. This is equivalent to the SM_CYEDGE value for windows with the 3-D look.
            /// </summary>
            CYBORDER = 6,

            /// <summary>
            /// The height of a caption area, in pixels.
            /// </summary>
            CYCAPTION = 4,

            /// <summary>
            /// The height of a cursor, in pixels. The system cannot create cursors of other sizes.
            /// </summary>
            CYCURSOR = 14,

            /// <summary>
            /// This value is the same as SM_CYFIXEDFRAME.
            /// </summary>
            CYDLGFRAME = 8,

            /// <summary>
            /// The height of the rectangle around the location of a first click in a double-click sequence, in pixels.
            /// The second click must occur within the rectangle defined by SM_CXDOUBLECLK and SM_CYDOUBLECLK for the system to consider
            /// the two clicks a double-click. The two clicks must also occur within a specified time. To set the height of the double-click
            /// rectangle, call SystemParametersInfo with SPI_SETDOUBLECLKHEIGHT.
            /// </summary>
            CYDOUBLECLK = 37,

            /// <summary>
            /// The number of pixels above and below a mouse-down point that the mouse pointer can move before a drag operation begins.
            /// This allows the user to click and release the mouse button easily without unintentionally starting a drag operation.
            /// If this value is negative, it is subtracted from above the mouse-down point and added below it.
            /// </summary>
            CYDRAG = 69,

            /// <summary>
            /// The height of a 3-D border, in pixels. This is the 3-D counterpart of SM_CYBORDER.
            /// </summary>
            CYEDGE = 46,

            /// <summary>
            /// The thickness of the frame around the perimeter of a window that has a caption but is not sizable, in pixels.
            /// SM_CXFIXEDFRAME is the height of the horizontal border, and SM_CYFIXEDFRAME is the width of the vertical border.
            /// This value is the same as SM_CYDLGFRAME.
            /// </summary>
            CYFIXEDFRAME = 8,

            /// <summary>
            /// The height of the top and bottom edges of the focus rectangle drawn byDrawFocusRect.
            /// This value is in pixels.
            /// Windows 2000:  This value is not supported.
            /// </summary>
            CYFOCUSBORDER = 84,

            /// <summary>
            /// This value is the same as SM_CYSIZEFRAME.
            /// </summary>
            CYFRAME = 33,

            /// <summary>
            /// The height of the client area for a full-screen window on the primary display monitor, in pixels.
            /// To get the coordinates of the portion of the screen not obscured by the system taskbar or by application desktop toolbars,
            /// call the SystemParametersInfo function with the SPI_GETWORKAREA value.
            /// </summary>
            CYFULLSCREEN = 17,

            /// <summary>
            /// The height of a horizontal scroll bar, in pixels.
            /// </summary>
            CYHSCROLL = 3,

            /// <summary>
            /// The default height of an icon, in pixels. The LoadIcon function can load only icons with the dimensions SM_CXICON and SM_CYICON.
            /// </summary>
            CYICON = 12,

            /// <summary>
            /// The height of a grid cell for items in large icon view, in pixels. Each item fits into a rectangle of size
            /// SM_CXICONSPACING by SM_CYICONSPACING when arranged. This value is always greater than or equal to SM_CYICON.
            /// </summary>
            CYICONSPACING = 39,

            /// <summary>
            /// For double byte character set versions of the system, this is the height of the Kanji window at the bottom of the screen, in pixels.
            /// </summary>
            CYKANJIWINDOW = 18,

            /// <summary>
            /// The default height, in pixels, of a maximized top-level window on the primary display monitor.
            /// </summary>
            CYMAXIMIZED = 62,

            /// <summary>
            /// The default maximum height of a window that has a caption and sizing borders, in pixels. This metric refers to the entire desktop.
            /// The user cannot drag the window frame to a size larger than these dimensions. A window can override this value by processing
            /// the WM_GETMINMAXINFO message.
            /// </summary>
            CYMAXTRACK = 60,

            /// <summary>
            /// The height of a single-line menu bar, in pixels.
            /// </summary>
            CYMENU = 15,

            /// <summary>
            /// The height of the default menu check-mark bitmap, in pixels.
            /// </summary>
            CYMENUCHECK = 72,

            /// <summary>
            /// The height of menu bar buttons, such as the child window close button that is used in the multiple document interface, in pixels.
            /// </summary>
            CYMENUSIZE = 55,

            /// <summary>
            /// The minimum height of a window, in pixels.
            /// </summary>
            CYMIN = 29,

            /// <summary>
            /// The height of a minimized window, in pixels.
            /// </summary>
            CYMINIMIZED = 58,

            /// <summary>
            /// The height of a grid cell for a minimized window, in pixels. Each minimized window fits into a rectangle this size when arranged.
            /// This value is always greater than or equal to SM_CYMINIMIZED.
            /// </summary>
            CYMINSPACING = 48,

            /// <summary>
            /// The minimum tracking height of a window, in pixels. The user cannot drag the window frame to a size smaller than these dimensions.
            /// A window can override this value by processing the WM_GETMINMAXINFO message.
            /// </summary>
            CYMINTRACK = 35,

            /// <summary>
            /// The height of the screen of the primary display monitor, in pixels. This is the same value obtained by calling
            /// GetDeviceCaps as follows: GetDeviceCaps( hdcPrimaryMonitor, VERTRES).
            /// </summary>
            CYSCREEN = 1,

            /// <summary>
            /// The height of a button in a window caption or title bar, in pixels.
            /// </summary>
            CYSIZE = 31,

            /// <summary>
            /// The thickness of the sizing border around the perimeter of a window that can be resized, in pixels.
            /// SM_CXSIZEFRAME is the width of the horizontal border, and SM_CYSIZEFRAME is the height of the vertical border.
            /// This value is the same as SM_CYFRAME.
            /// </summary>
            CYSIZEFRAME = 33,

            /// <summary>
            /// The height of a small caption, in pixels.
            /// </summary>
            CYSMCAPTION = 51,

            /// <summary>
            /// The recommended height of a small icon, in pixels. Small icons typically appear in window captions and in small icon view.
            /// </summary>
            CYSMICON = 50,

            /// <summary>
            /// The height of small caption buttons, in pixels.
            /// </summary>
            CYSMSIZE = 53,

            /// <summary>
            /// The height of the virtual screen, in pixels. The virtual screen is the bounding rectangle of all display monitors.
            /// The SM_YVIRTUALSCREEN metric is the coordinates for the top of the virtual screen.
            /// </summary>
            CYVIRTUALSCREEN = 79,

            /// <summary>
            /// The height of the arrow bitmap on a vertical scroll bar, in pixels.
            /// </summary>
            CYVSCROLL = 20,

            /// <summary>
            /// The height of the thumb box in a vertical scroll bar, in pixels.
            /// </summary>
            CYVTHUMB = 9,

            /// <summary>
            /// Nonzero if User32.dll supports DBCS; otherwise, 0.
            /// </summary>
            DBCSENABLED = 42,

            /// <summary>
            /// Nonzero if the debug version of User.exe is installed; otherwise, 0.
            /// </summary>
            DEBUG = 22,

            /// <summary>
            /// Nonzero if the current operating system is Windows 7 or Windows Server 2008 R2 and the Tablet PC Input
            /// service is started; otherwise, 0. The return value is a bitmask that specifies the type of digitizer input supported by the device.
            /// For more information, see Remarks.
            /// Windows Server 2008, Windows Vista, and Windows XP/2000:  This value is not supported.
            /// </summary>
            DIGITIZER = 94,

            /// <summary>
            /// Nonzero if Input Method Manager/Input Method Editor features are enabled; otherwise, 0.
            /// SM_IMMENABLED indicates whether the system is ready to use a Unicode-based IME on a Unicode application.
            /// To ensure that a language-dependent IME works, check SM_DBCSENABLED and the system ANSI code page.
            /// Otherwise the ANSI-to-Unicode conversion may not be performed correctly, or some components like fonts
            /// or registry settings may not be present.
            /// </summary>
            IMMENABLED = 82,

            /// <summary>
            /// Nonzero if there are digitizers in the system; otherwise, 0. SM_MAXIMUMTOUCHES returns the aggregate maximum of the
            /// maximum number of contacts supported by every digitizer in the system. If the system has only single-touch digitizers,
            /// the return value is 1. If the system has multi-touch digitizers, the return value is the number of simultaneous contacts
            /// the hardware can provide. Windows Server 2008, Windows Vista, and Windows XP/2000:  This value is not supported.
            /// </summary>
            MAXIMUMTOUCHES = 95,

            /// <summary>
            /// Nonzero if the current operating system is the Windows XP, Media Center Edition, 0 if not.
            /// </summary>
            MEDIACENTER = 87,

            /// <summary>
            /// Nonzero if drop-down menus are right-aligned with the corresponding menu-bar item; 0 if the menus are left-aligned.
            /// </summary>
            MENUDROPALIGNMENT = 40,

            /// <summary>
            /// Nonzero if the system is enabled for Hebrew and Arabic languages, 0 if not.
            /// </summary>
            MIDEASTENABLED = 74,

            /// <summary>
            /// Nonzero if a mouse is installed; otherwise, 0. This value is rarely zero, because of support for virtual mice and because
            /// some systems detect the presence of the port instead of the presence of a mouse.
            /// </summary>
            MOUSEPRESENT = 19,

            /// <summary>
            /// Nonzero if a mouse with a horizontal scroll wheel is installed; otherwise 0.
            /// </summary>
            MOUSEHORIZONTALWHEELPRESENT = 91,

            /// <summary>
            /// Nonzero if a mouse with a vertical scroll wheel is installed; otherwise 0.
            /// </summary>
            MOUSEWHEELPRESENT = 75,

            /// <summary>
            /// The least significant bit is set if a network is present; otherwise, it is cleared. The other bits are reserved for future use.
            /// </summary>
            NETWORK = 63,

            /// <summary>
            /// Nonzero if the Microsoft Windows for Pen computing extensions are installed; zero otherwise.
            /// </summary>
            PENWINDOWS = 41,

            /// <summary>
            /// This system metric is used in a Terminal Services environment to determine if the current Terminal Server session is
            /// being remotely controlled. Its value is nonzero if the current session is remotely controlled; otherwise, 0.
            /// You can use terminal services management tools such as Terminal Services Manager (tsadmin.msc) and shadow.exe to
            /// control a remote session. When a session is being remotely controlled, another user can view the contents of that session
            /// and potentially interact with it.
            /// </summary>
            REMOTECONTROL = 0x2001,

            /// <summary>
            /// This system metric is used in a Terminal Services environment. If the calling process is associated with a Terminal Services
            /// client session, the return value is nonzero. If the calling process is associated with the Terminal Services console session,
            /// the return value is 0.
            /// Windows Server 2003 and Windows XP:  The console session is not necessarily the physical console.
            /// For more information, seeWTSGetActiveConsoleSessionId.
            /// </summary>
            REMOTESESSION = 0x1000,

            /// <summary>
            /// Nonzero if all the display monitors have the same color format, otherwise, 0. Two displays can have the same bit depth,
            /// but different color formats. For example, the red, green, and blue pixels can be encoded with different numbers of bits,
            /// or those bits can be located in different places in a pixel color value.
            /// </summary>
            SAMEDISPLAYFORMAT = 81,

            /// <summary>
            /// This system metric should be ignored; it always returns 0.
            /// </summary>
            SECURE = 44,

            /// <summary>
            /// The build number if the system is Windows Server 2003 R2; otherwise, 0.
            /// </summary>
            SERVERR2 = 89,

            /// <summary>
            /// Nonzero if the user requires an application to present information visually in situations where it would otherwise present
            /// the information only in audible form; otherwise, 0.
            /// </summary>
            SHOWSOUNDS = 70,

            /// <summary>
            /// Nonzero if the current session is shutting down; otherwise, 0. Windows 2000:  This value is not supported.
            /// </summary>
            SHUTTINGDOWN = 0x2000,

            /// <summary>
            /// Nonzero if the computer has a low-end (slow) processor; otherwise, 0.
            /// </summary>
            SLOWMACHINE = 73,

            /// <summary>
            /// Nonzero if the current operating system is Windows 7 Starter Edition, Windows Vista Starter, or Windows XP Starter Edition; otherwise, 0.
            /// </summary>
            STARTER = 88,

            /// <summary>
            /// Nonzero if the meanings of the left and right mouse buttons are swapped; otherwise, 0.
            /// </summary>
            SWAPBUTTON = 23,

            /// <summary>
            /// Nonzero if the current operating system is the Windows XP Tablet PC edition or if the current operating system is Windows Vista
            /// or Windows 7 and the Tablet PC Input service is started; otherwise, 0. The SM_DIGITIZER setting indicates the type of digitizer
            /// input supported by a device running Windows 7 or Windows Server 2008 R2. For more information, see Remarks.
            /// </summary>
            TABLETPC = 86,

            /// <summary>
            /// The coordinates for the left side of the virtual screen. The virtual screen is the bounding rectangle of all display monitors.
            /// The SM_CXVIRTUALSCREEN metric is the width of the virtual screen.
            /// </summary>
            XVIRTUALSCREEN = 76,

            /// <summary>
            /// The coordinates for the top of the virtual screen. The virtual screen is the bounding rectangle of all display monitors.
            /// The SM_CYVIRTUALSCREEN metric is the height of the virtual screen.
            /// </summary>
            YVIRTUALSCREEN = 77
        }

        internal enum ShowWindowCommands
        {
            /// <summary>
            /// Hides the window and activates another window.
            /// </summary>
            SW_HIDE = 0,
            /// <summary>
            /// Activates and displays a window. If the window is minimized or
            /// maximized, the system restores it to its original size and position.
            /// An application should specify this flag when displaying the window
            /// for the first time.
            /// </summary>
            SW_NORMAL = 1,
            /// <summary>
            /// Activates the window and displays it as a minimized window.
            /// </summary>
            SW_SHOWMINIMIZED = 2,
            /// <summary>
            /// Activates the window and displays it as a maximized window.
            /// </summary>
            SW_SHOWMAXIMIZED = 3,
            /// <summary>
            /// Displays a window in its most recent size and position. This value
            /// is similar to <see cref="SW_NORMAL"/>, except
            /// the window is not activated.
            /// </summary>
            SW_SHOWNOACTIVATE = 4,
            /// <summary>
            /// Activates the window and displays it in its current size and position.
            /// </summary>
            SW_SHOW = 5,
            /// <summary>
            /// Minimizes the specified window and activates the next top-level
            /// window in the Z order.
            /// </summary>
            SW_MINIMIZE = 6,
            /// <summary>
            /// Displays the window as a minimized window. This value is similar to
            /// <see cref="SW_SHOWMINIMIZED"/>, except the
            /// window is not activated.
            /// </summary>
            SW_SHOWMINNOACTIVE = 7,
            /// <summary>
            /// Displays the window in its current size and position. This value is
            /// similar to <see cref="SW_SHOW"/>, except the
            /// window is not activated.
            /// </summary>
            SW_SHOWNA = 8,
            /// <summary>
            /// Activates and displays the window. If the window is minimized or
            /// maximized, the system restores it to its original size and position.
            /// An application should specify this flag when restoring a minimized window.
            /// </summary>
            SW_RESTORE = 9,
            /// <summary>
            /// Sets the show state based on the SW_* value specified in the
            /// STARTUPINFO structure passed to the CreateProcess function by the
            /// program that started the application.
            /// </summary>
            SW_SHOWDEFAULT = 10,
            /// <summary>
            ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread
            /// that owns the window is not responding. This flag should only be
            /// used when minimizing windows from a different thread.
            /// </summary>
            SW_FORCEMINIMIZE = 11
        }

        internal struct PRF_
        {
            public const int
                CHECKVISIBLE = 0x01,

                NONCLIENT = 0x02;
        }

        /// <summary>
        /// I needed some "Generic" magic to get from an enum to an int for switch and boolean operands
        /// </summary>
        internal struct WM_
        {
            public const int
            // <summary>
            // The WM_NULL message performs no operation. An application sends the WM_NULL message if it wants to post a message that the recipient window will ignore.
            // </summary>
            NULL = 0x0000,
            // <summary>
            // The WM_CREATE message is sent when an application requests that a window be created by calling the CreateWindowEx or CreateWindow function. (The message is sent before the function returns.) The window procedure of the new window receives this message after the window is created, but before the window becomes visible.
            // </summary>
            CREATE = 0x0001,
            // <summary>
            // The WM_DESTROY message is sent when a window is being destroyed. It is sent to the window procedure of the window being destroyed after the window is removed from the screen.
            // This message is sent first to the window being destroyed and then to the child windows (if any) as they are destroyed. During the processing of the message, it can be assumed that all child windows still exist.
            // // </summary>
            DESTROY = 0x0002,
            // <summary>
            // The WM_MOVE message is sent after a window has been moved.
            // </summary>
            MOVE = 0x0003,
            // <summary>
            // The WM_SIZE message is sent to a window after its size has changed.
            // </summary>
            SIZE = 0x0005,
            // <summary>
            // The WM_ACTIVATE message is sent to both the window being activated and the window being deactivated.
            // If the windows use the same input queue, the message is sent synchronously, first to the window procedure of the top-level window being deactivated,
            // then to the window procedure of the top-level window being activated. If the windows use different input queues, the message is sent asynchronously,
            // so the window is activated immediately.
            // </summary>
            ACTIVATE = 0x0006,
            // <summary>
            // The WM_SETFOCUS message is sent to a window after it has gained the keyboard focus.
            // </summary>
            SETFOCUS = 0x0007,
            // <summary>
            // The WM_KILLFOCUS message is sent to a window immediately before it loses the keyboard focus.
            // </summary>
            KILLFOCUS = 0x0008,
            // <summary>
            // The WM_ENABLE message is sent when an application changes the enabled state of a window. It is sent to the window whose enabled state is changing. This message is sent before the EnableWindow function returns, but after the enabled state (WS_DISABLED style bit) of the window has changed.
            // </summary>
            ENABLE = 0x000A,
            // <summary>
            // An application sends the WM_SETREDRAW message to a window to allow changes in that window to be redrawn or to prevent changes in that window from being redrawn.
            // </summary>
            SETREDRAW = 0x000B,
            // <summary>
            // An application sends a WM_SETTEXT message to set the text of a window.
            // </summary>
            SETTEXT = 0x000C,
            // <summary>
            // An application sends a WM_GETTEXT message to copy the text that corresponds to a window into a buffer provided by the caller.
            // </summary>
            GETTEXT = 0x000D,
            // <summary>
            // An application sends a WM_GETTEXTLENGTH message to determine the length, in characters, of the text associated with a window.
            // </summary>
            GETTEXTLENGTH = 0x000E,
            // <summary>
            // The WM_PAINT message is sent when the system or another application makes a request to paint a portion of an application's window. The message is sent when the UpdateWindow or RedrawWindow function is called, or by the DispatchMessage function when the application obtains a WM_PAINT message by using the GetMessage or PeekMessage function.
            // </summary>
            PAINT = 0x000F,
            // <summary>
            // The WM_CLOSE message is sent as a signal that a window or an application should terminate.
            // </summary>
            CLOSE = 0x0010,
            // <summary>
            // The WM_QUERYENDSESSION message is sent when the user chooses to end the session or when an application calls one of the system shutdown functions. If any application returns zero, the session is not ended. The system stops sending WM_QUERYENDSESSION messages as soon as one application returns zero.
            // After processing this message, the system sends the WM_ENDSESSION message with the wParam parameter set to the results of the WM_QUERYENDSESSION message.
            // </summary>
            QUERYENDSESSION = 0x0011,
            // <summary>
            // The WM_QUERYOPEN message is sent to an icon when the user requests that the window be restored to its previous size and position.
            // </summary>
            QUERYOPEN = 0x0013,
            // <summary>
            // The WM_QUIT message indicates a request to terminate an application and is generated when the application calls the PostQuitMessage function. It causes the GetMessage function to return zero.
            // </summary>
            QUIT = 0x0012,
            // <summary>
            // The WM_ERASEBKGND message is sent when the window background must be erased (for example, when a window is resized). The message is sent to prepare an invalidated portion of a window for painting.
            // </summary>
            ERASEBKGND = 0x0014,
            // <summary>
            // This message is sent to all top-level windows when a change is made to a system color setting.
            // </summary>
            SYSCOLORCHANGE = 0x0015,
            // <summary>
            // The WM_ENDSESSION message is sent to an application after the system processes the results of the WM_QUERYENDSESSION message. The WM_ENDSESSION message informs the application whether the session is ending.
            // </summary>
            ENDSESSION = 0x0016,
            // <summary>
            // The WM_SHOWWINDOW message is sent to a window when the window is about to be hidden or shown.
            // </summary>
            SHOWWINDOW = 0x0018,
            CTLCOLOR = 0x0019, //replaced by https://learn.microsoft.com/en-us/windows/win32/devnotes/wm-ctlcolor-#remarks
            // <summary>
            // An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.
            // Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.
            // </summary>
            WININICHANGE = 0x001A,
            // <summary>
            // An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.
            // Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.
            // </summary>
            SETTINGCHANGE = WININICHANGE,
            // <summary>
            // The WM_DEVMODECHANGE message is sent to all top-level windows whenever the user changes device-mode settings.
            // </summary>
            DEVMODECHANGE = 0x001B,
            // <summary>
            // The WM_ACTIVATEAPP message is sent when a window belonging to a different application than the active window is about to be activated.
            // The message is sent to the application whose window is being activated and to the application whose window is being deactivated.
            // </summary>
            ACTIVATEAPP = 0x001C,
            // <summary>
            // An application sends the WM_FONTCHANGE message to all top-level windows in the system after changing the pool of font resources.
            // </summary>
            FONTCHANGE = 0x001D,
            // <summary>
            // A message that is sent whenever there is a change in the system time.
            // </summary>
            TIMECHANGE = 0x001E,
            // <summary>
            // The WM_CANCELMODE message is sent to cancel certain modes, such as mouse capture. For example, the system sends this message to the active window when a dialog box or message box is Displayed. Certain functions also send this message explicitly to the specified window regardless of whether it is the active window. For example, the EnableWindow function sends this message when disabling the specified window.
            // </summary>
            CANCELMODE = 0x001F,
            // <summary>
            // The WM_SETCURSOR message is sent to a window if the mouse causes the cursor to move within a window and mouse input is not captured.
            // </summary>
            SETCURSOR = 0x0020,
            // <summary>
            // The WM_MOUSEACTIVATE message is sent when the cursor is in an inactive window and the user presses a mouse button. The parent window receives this message only if the child window passes it to the DefWindowProc function.
            // </summary>
            MOUSEACTIVATE = 0x0021,
            // <summary>
            // The WM_CHILDACTIVATE message is sent to a child window when the user clicks the window's title bar or when the window is activated, moved, or sized.
            // </summary>
            CHILDACTIVATE = 0x0022,
            // <summary>
            // The WM_QUEUESYNC message is sent by a computer-based training (CBT) application to separate user-input messages from other messages sent through the WH_JOURNALPLAYBACK Hook procedure.
            // </summary>
            QUEUESYNC = 0x0023,
            // <summary>
            // The WM_GETMINMAXINFO message is sent to a window when the size or position of the window is about to change.
            // An application can use this message to override the window's default maximized size and position, or its default minimum or maximum tracking size.
            // </summary>
            GETMINMAXINFO = 0x0024,
            // <summary>
            // Windows NT 3.51 and earlier: The WM_PAINTICON message is sent to a minimized window when the icon is to be painted. This message is not sent by newer versions of Microsoft Windows, except in unusual circumstances explained in the Remarks.
            // </summary>
            PAINTICON = 0x0026,
            // <summary>
            // Windows NT 3.51 and earlier: The WM_ICONERASEBKGND message is sent to a minimized window when the background of the icon must be filled before painting the icon. A window receives this message only if a class icon is defined for the window; otherwise, WM_ERASEBKGND is sent. This message is not sent by newer versions of Windows.
            // </summary>
            ICONERASEBKGND = 0x0027,
            // <summary>
            // The WM_NEXTDLGCTL message is sent to a dialog box procedure to set the keyboard focus to a different control in the dialog box.
            // </summary>
            NEXTDLGCTL = 0x0028,
            // <summary>
            // The WM_SPOOLERSTATUS message is sent from Print Manager whenever a job is added to or removed from the Print Manager queue.
            // </summary>
            SPOOLERSTATUS = 0x002A,
            // <summary>
            // The WM_DRAWITEM message is sent to the parent window of an owner-drawn button, combo box, list box, or menu when a visual aspect of the button, combo box, list box, or menu has changed.
            // </summary>
            DRAWITEM = 0x002B,
            // <summary>
            // The WM_MEASUREITEM message is sent to the owner window of a combo box, list box, list view control, or menu item when the control or menu is created.
            // </summary>
            MEASUREITEM = 0x002C,
            // <summary>
            // Sent to the owner of a list box or combo box when the list box or combo box is destroyed or when items are removed by the LB_DELETESTRING, LB_RESETCONTENT, CB_DELETESTRING, or CB_RESETCONTENT message. The system sends a WM_DELETEITEM message for each deleted item. The system sends the WM_DELETEITEM message for any deleted list box or combo box item with nonzero item data.
            // </summary>
            DELETEITEM = 0x002D,
            // <summary>
            // Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_KEYDOWN message.
            // </summary>
            VKEYTOITEM = 0x002E,
            // <summary>
            // Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_CHAR message.
            // </summary>
            CHARTOITEM = 0x002F,
            // <summary>
            // An application sends a WM_SETFONT message to specify the font that a control is to use when drawing text.
            // </summary>
            SETFONT = 0x0030,
            // <summary>
            // An application sends a WM_GETFONT message to a control to retrieve the font with which the control is currently drawing its text.
            // </summary>
            GETFONT = 0x0031,
            // <summary>
            // An application sends a WM_SETHOTKEY message to a window to associate a hot key with the window. When the user presses the hot key, the system activates the window.
            // </summary>
            SETHOTKEY = 0x0032,
            // <summary>
            // An application sends a WM_GETHOTKEY message to determine the hot key associated with a window.
            // </summary>
            GETHOTKEY = 0x0033,
            // <summary>
            // The WM_QUERYDRAGICON message is sent to a minimized (iconic) window. The window is about to be dragged by the user but does not have an icon defined for its class. An application can return a handle to an icon or cursor. The system displays this cursor or icon while the user drags the icon.
            // </summary>
            QUERYDRAGICON = 0x0037,
            // <summary>
            // The system sends the WM_COMPAREITEM message to determine the relative position of a new item in the sorted list of an owner-drawn combo box or list box. Whenever the application adds a new item, the system sends this message to the owner of a combo box or list box created with the CBS_SORT or LBS_SORT style.
            // </summary>
            COMPAREITEM = 0x0039,
            // <summary>
            // Active Accessibility sends the WM_GETOBJECT message to obtain information about an accessible object contained in a server application.
            // Applications never send this message directly. It is sent only by Active Accessibility in response to calls to AccessibleObjectFromPoint, AccessibleObjectFromEvent, or AccessibleObjectFromWindow. However, server applications handle this message.
            // </summary>
            GETOBJECT = 0x003D,
            // <summary>
            // The WM_COMPACTING message is sent to all top-level windows when the system detects more than 12.5 percent of system time over a 30- to 60-second interval is being spent compacting memory. This indicates that system memory is low.
            // </summary>
            COMPACTING = 0x0041,
            // <summary>
            // WM_COMMNOTIFY is Obsolete for Win32-Based Applications
            // </summary>
            //[Obsolete]
            //COMMNOTIFY = 0x0044,
            // <summary>
            // The WM_WINDOWPOSCHANGING message is sent to a window whose size, position, or place in the Z order is about to change as a result of a call to the SetWindowPos function or another window-management function.
            // </summary>
            WINDOWPOSCHANGING = 0x0046,
            // <summary>
            // The WM_WINDOWPOSCHANGED message is sent to a window whose size, position, or place in the Z order has changed as a result of a call to the SetWindowPos function or another window-management function.
            // </summary>
            WINDOWPOSCHANGED = 0x0047,
            // <summary>
            // Notifies applications that the system, typically a battery-powered personal computer, is about to enter a suspended mode.
            // Use: POWERBROADCAST
            // </summary>
            //[Obsolete]
            //POWER = 0x0048,
            // <summary>
            // An application sends the WM_COPYDATA message to pass data to another application.
            // </summary>
            COPYDATA = 0x004A,
            // <summary>
            // The WM_CANCELJOURNAL message is posted to an application when a user cancels the application's journaling activities. The message is posted with a NULL window handle.
            // </summary>
            CANCELJOURNAL = 0x004B,
            // <summary>
            // Sent by a common control to its parent window when an event has occurred or the control requires some information.
            // </summary>
            NOTIFY = 0x004E,
            // <summary>
            // The WM_INPUTLANGCHANGEREQUEST message is posted to the window with the focus when the user chooses a new input language, either with the hotkey (specified in the Keyboard control panel application) or from the indicator on the system taskbar. An application can accept the change by passing the message to the DefWindowProc function or reject the change (and prevent it from taking place) by returning immediately.
            // </summary>
            INPUTLANGCHANGEREQUEST = 0x0050,
            // <summary>
            // The WM_INPUTLANGCHANGE message is sent to the topmost affected window after an application's input language has been changed. You should make any application-specific settings and pass the message to the DefWindowProc function, which passes the message to all first-level child windows. These child windows can pass the message to DefWindowProc to have it pass the message to their child windows, and so on.
            // </summary>
            INPUTLANGCHANGE = 0x0051,
            // <summary>
            // Sent to an application that has initiated a training card with Microsoft Windows Help. The message informs the application when the user clicks an authorable button. An application initiates a training card by specifying the HELP_TCARD command in a call to the WinHelp function.
            // </summary>
            TCARD = 0x0052,
            // <summary>
            // Indicates that the user pressed the F1 key. If a menu is active when F1 is pressed, WM_HELP is sent to the window associated with the menu; otherwise, WM_HELP is sent to the window that has the keyboard focus. If no window has the keyboard focus, WM_HELP is sent to the currently active window.
            // </summary>
            HELP = 0x0053,
            // <summary>
            // The WM_USERCHANGED message is sent to all windows after the user has logged on or off. When the user logs on or off, the system updates the user-specific settings. The system sends this message immediately after updating the settings.
            // </summary>
            USERCHANGED = 0x0054,
            // <summary>
            // Determines if a window accepts ANSI or Unicode structures in the WM_NOTIFY notification message. WM_NOTIFYFORMAT messages are sent from a common control to its parent window and from the parent window to the common control.
            // </summary>
            NOTIFYFORMAT = 0x0055,
            // <summary>
            // The WM_CONTEXTMENU message notifies a window that the user clicked the right mouse button (right-clicked) in the window.
            // </summary>
            CONTEXTMENU = 0x007B,
            // <summary>
            // The WM_STYLECHANGING message is sent to a window when the SetWindowLong function is about to change one or more of the window's styles.
            // </summary>
            STYLECHANGING = 0x007C,
            // <summary>
            // The WM_STYLECHANGED message is sent to a window after the SetWindowLong function has changed one or more of the window's styles
            // </summary>
            STYLECHANGED = 0x007D,
            // <summary>
            // The WM_DISPLAYCHANGE message is sent to all windows when the display resolution has changed.
            // </summary>
            DISPLAYCHANGE = 0x007E,
            // <summary>
            // The WM_GETICON message is sent to a window to retrieve a handle to the large or small icon associated with a window. The system displays the large icon in the ALT+TAB dialog, and the small icon in the window caption.
            // </summary>
            GETICON = 0x007F,
            // <summary>
            // An application sends the WM_SETICON message to associate a new large or small icon with a window. The system displays the large icon in the ALT+TAB dialog box, and the small icon in the window caption.
            // </summary>
            SETICON = 0x0080,
            // <summary>
            // The WM_NCCREATE message is sent prior to the WM_CREATE message when a window is first created.
            // </summary>
            NCCREATE = 0x0081,
            // <summary>
            // The WM_NCDESTROY message informs a window that its nonclient area is being destroyed. The DestroyWindow function sends the WM_NCDESTROY message to the window following the WM_DESTROY message. WM_DESTROY is used to free the allocated memory object associated with the window.
            // The WM_NCDESTROY message is sent after the child windows have been destroyed. In contrast, WM_DESTROY is sent before the child windows are destroyed.
            // </summary>
            NCDESTROY = 0x0082,
            // <summary>
            // The WM_NCCALCSIZE message is sent when the size and position of a window's client area must be calculated. By processing this message, an application can control the content of the window's client area when the size or position of the window changes.
            // </summary>
            NCCALCSIZE = 0x0083,
            // <summary>
            // The WM_NCHITTEST message is sent to a window when the cursor moves, or when a mouse button is pressed or released. If the mouse is not captured, the message is sent to the window beneath the cursor. Otherwise, the message is sent to the window that has captured the mouse.
            // </summary>
            NCHITTEST = 0x0084,
            // <summary>
            // The WM_NCPAINT message is sent to a window when its frame must be painted.
            // </summary>
            NCPAINT = 0x0085,
            // <summary>
            // The WM_NCACTIVATE message is sent to a window when its nonclient area needs to be changed to indicate an active or inactive state.
            // </summary>
            NCACTIVATE = 0x0086,
            // <summary>
            // The WM_GETDLGCODE message is sent to the window procedure associated with a control. By default, the system handles all keyboard input to the control; the system interprets certain types of keyboard input as dialog box navigation keys. To override this default behavior, the control can respond to the WM_GETDLGCODE message to indicate the types of input it wants to process itself.
            // </summary>
            GETDLGCODE = 0x0087,
            // <summary>
            // The WM_SYNCPAINT message is used to synchronize painting while avoiding linking independent GUI threads.
            // </summary>
            SYNCPAINT = 0x0088,
            // <summary>
            // The WM_NCMOUSEMOVE message is posted to a window when the cursor is moved within the nonclient area of the window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            // </summary>
            NCMOUSEMOVE = 0x00A0,
            // <summary>
            // The WM_NCLBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            // </summary>
            NCLBUTTONDOWN = 0x00A1,
            // <summary>
            // The WM_NCLBUTTONUP message is posted when the user releases the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            // </summary>
            NCLBUTTONUP = 0x00A2,
            // <summary>
            // The WM_NCLBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            // </summary>
            NCLBUTTONDBLCLK = 0x00A3,
            // <summary>
            // The WM_NCRBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            // </summary>
            NCRBUTTONDOWN = 0x00A4,
            // <summary>
            // The WM_NCRBUTTONUP message is posted when the user releases the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            // </summary>
            NCRBUTTONUP = 0x00A5,
            // <summary>
            // The WM_NCRBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            // </summary>
            NCRBUTTONDBLCLK = 0x00A6,
            // <summary>
            // The WM_NCMBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            // </summary>
            NCMBUTTONDOWN = 0x00A7,
            // <summary>
            // The WM_NCMBUTTONUP message is posted when the user releases the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            // </summary>
            NCMBUTTONUP = 0x00A8,
            // <summary>
            // The WM_NCMBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            // </summary>
            NCMBUTTONDBLCLK = 0x00A9,
            // <summary>
            // The WM_NCXBUTTONDOWN message is posted when the user presses the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            // </summary>
            NCXBUTTONDOWN = 0x00AB,
            // <summary>
            // The WM_NCXBUTTONUP message is posted when the user releases the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            // </summary>
            NCXBUTTONUP = 0x00AC,
            // <summary>
            // The WM_NCXBUTTONDBLCLK message is posted when the user double-clicks the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            // </summary>
            NCXBUTTONDBLCLK = 0x00AD,
            // These undocumented messages are sent to draw themed window borders. Block them to prevent drawing borders over the client area.
            NCUAHDRAWCAPTION = 0x00AE,
            // These undocumented messages are sent to draw themed window borders. Block them to prevent drawing borders over the client area.
            NCUAHDRAWFRAME = 0x00AF,
            EM_GETSEL = 0xB0,
            EM_SETSEL = 0xB1,
            EM_GETRECT = 0xB2,
            EM_SETRECT = 0xB3,
            EM_SETRECTNP = 0xB4,
            EM_SCROLL = 0xB5,
            EM_LINESCROLL = 0xB6,
            EM_SCROLLCARET = 0xB7,
            EM_GETMODIFY = 0xB8,
            EM_SETMODIFY = 0xB9,
            EM_GETLINECOUNT = 0xBA,
            EM_LINEINDEX = 0xBB,
            EM_SETHANDLE = 0xBC,
            EM_GETHANDLE = 0xBD,
            EM_GETTHUMB = 0xBE,
            EM_LINELENGTH = 0xC1,
            EM_REPLACESEL = 0xC2,
            EM_GETLINE = 0xC4,
            EM_SETLIMITTEXT = 0xC5,
            EM_CANUNDO = 0xC6,
            EM_UNDO = 0xC7,
            EM_FMTLINES = 0xC8,
            EM_LINEFROMCHAR = 0xC9,
            EM_SETTABSTOPS = 0xCB,
            EM_SETPASSWORDCHAR = 0xCC,
            EM_EMPTYUNDOBUFFER = 0xCD,
            EM_GETFIRSTVISIBLELINE = 0xCE,
            EM_SETREADONLY = 0xCF,
            EM_SETWORDBREAKPROC = 0xD0,
            EM_GETWORDBREAKPROC = 0xD1,
            EM_GETPASSWORDCHAR = 0xD2,
            EM_SETMARGINS = 0xD3,
            EM_GETMARGINS = 0xD4,
            EM_GETLIMITTEXT = 0xD5,
            EM_POSFROMCHAR = 0xD6,
            EM_CHARFROMPOS = 0xD7,
            EM_SETIMESTATUS = 0xD8,
            EM_GETIMESTATUS = 0xD9,
            // <summary>
            // The WM_INPUT_DEVICE_CHANGE message is sent to the window that registered to receive raw input. A window receives this message through its WindowProc function.
            // </summary>
            INPUT_DEVICE_CHANGE = 0x00FE,
            // <summary>
            // The WM_INPUT message is sent to the window that is getting raw input.
            // </summary>
            INPUT = 0x00FF,
            // <summary>
            // This message filters for keyboard messages.
            // </summary>
            KEYFIRST = 0x0100,
            // <summary>
            // The WM_KEYDOWN message is posted to the window with the keyboard focus when a nonsystem key is pressed. A nonsystem key is a key that is pressed when the ALT key is not pressed.
            // </summary>
            KEYDOWN = 0x0100,
            // <summary>
            // The WM_KEYUP message is posted to the window with the keyboard focus when a nonsystem key is released. A nonsystem key is a key that is pressed when the ALT key is not pressed, or a keyboard key that is pressed when a window has the keyboard focus.
            // </summary>
            KEYUP = 0x0101,
            // <summary>
            // The WM_CHAR message is posted to the window with the keyboard focus when a WM_KEYDOWN message is translated by the TranslateMessage function. The WM_CHAR message contains the character code of the key that was pressed.
            // </summary>
            CHAR = 0x0102,
            // <summary>
            // The WM_DEADCHAR message is posted to the window with the keyboard focus when a WM_KEYUP message is translated by the TranslateMessage function. WM_DEADCHAR specifies a character code generated by a dead key. A dead key is a key that generates a character, such as the umlaut (double-dot), that is combined with another character to form a composite character. For example, the umlaut-O character (Ö) is generated by typing the dead key for the umlaut character, and then typing the O key.
            // </summary>
            DEADCHAR = 0x0103,
            // <summary>
            // The WM_SYSKEYDOWN message is posted to the window with the keyboard focus when the user presses the F10 key (which activates the menu bar) or holds down the ALT key and then presses another key. It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYDOWN message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter.
            // </summary>
            SYSKEYDOWN = 0x0104,
            // <summary>
            // The WM_SYSKEYUP message is posted to the window with the keyboard focus when the user releases a key that was pressed while the ALT key was held down. It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYUP message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter.
            // </summary>
            SYSKEYUP = 0x0105,
            // <summary>
            // The WM_SYSCHAR message is posted to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. It specifies the character code of a system character key — that is, a character key that is pressed while the ALT key is down.
            // </summary>
            SYSCHAR = 0x0106,
            // <summary>
            // The WM_SYSDEADCHAR message is sent to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. WM_SYSDEADCHAR specifies the character code of a system dead key — that is, a dead key that is pressed while holding down the ALT key.
            // </summary>
            SYSDEADCHAR = 0x0107,
            // <summary>
            // The WM_UNICHAR message is posted to the window with the keyboard focus when a WM_KEYDOWN message is translated by the TranslateMessage function. The WM_UNICHAR message contains the character code of the key that was pressed.
            // The WM_UNICHAR message is equivalent to WM_CHAR, but it uses Unicode Transformation Format (UTF)-32, whereas WM_CHAR uses UTF-16. It is designed to send or post Unicode characters to ANSI windows and it can can handle Unicode Supplementary Plane characters.
            // </summary>
            UNICHAR = 0x0109,
            // <summary>
            // This message filters for keyboard messages.
            // </summary>
            KEYLAST = 0x0108,
            // <summary>
            // Sent immediately before the IME generates the composition string as a result of a keystroke. A window receives this message through its WindowProc function.
            // </summary>
            IME_STARTCOMPOSITION = 0x010D,
            // <summary>
            // Sent to an application when the IME ends composition. A window receives this message through its WindowProc function.
            // </summary>
            IME_ENDCOMPOSITION = 0x010E,
            // <summary>
            // Sent to an application when the IME changes composition status as a result of a keystroke. A window receives this message through its WindowProc function.
            // </summary>
            IME_COMPOSITION = 0x010F,
            IME_KEYLAST = 0x010F,
            // <summary>
            // The WM_INITDIALOG message is sent to the dialog box procedure immediately before a dialog box is Displayed.
            // Dialog box procedures typically use this message to initialize controls and carry out any other initialization tasks that affect the appearance of the dialog box.
            // </summary>
            INITDIALOG = 0x0110,
            // <summary>
            // The WM_COMMAND message is sent when the user selects a command item from a menu, when a control sends a notification message to its parent window,
            // or when an accelerator keystroke is translated.
            // </summary>
            COMMAND = 0x0111,
            // <summary>
            // A window receives this message when the user chooses a command from the Window menu, clicks the maximize button, minimize button, restore button,
            // close button, or moves the form. You can stop the form from moving by filtering this out.
            // </summary>
            SYSCOMMAND = 0x0112,
            // <summary>
            // The WM_TIMER message is posted to the installing thread's message queue when a timer expires. The message is posted by the GetMessage or PeekMessage function.
            // </summary>
            TIMER = 0x0113,
            // <summary>
            // The WM_HSCROLL message is sent to a window when a scroll event occurs in the window's standard horizontal scroll bar. This message is also sent to the owner of a horizontal scroll bar control when a scroll event occurs in the control.
            // </summary>
            HSCROLL = 0x0114,
            // <summary>
            // The WM_VSCROLL message is sent to a window when a scroll event occurs in the window's standard vertical scroll bar. This message is also sent to the owner of a vertical scroll bar control when a scroll event occurs in the control.
            // </summary>
            VSCROLL = 0x0115,
            // <summary>
            // The WM_INITMENU message is sent when a menu is about to become active. It occurs when the user clicks an item on the menu bar or presses a menu key. This allows the application to modify the menu before it is Displayed.
            // </summary>
            INITMENU = 0x0116,
            // <summary>
            // The WM_INITMENUPOPUP message is sent when a drop-down menu or submenu is about to become active. This allows an application to modify the menu before it is Displayed, without changing the entire menu.
            // </summary>
            INITMENUPOPUP = 0x0117,
            // <summary>
            // Windows uses WM_SYSTIMER for internal actions like scrolling.
            // </summary>
            SYSTIMER = 0x0118,
            // <summary>
            // The WM_MENUSELECT message is sent to a menu's owner window when the user selects a menu item.
            // </summary>
            MENUSELECT = 0x011F,
            // <summary>
            // The WM_MENUCHAR message is sent when a menu is active and the user presses a key that does not correspond to any mnemonic or accelerator key. This message is sent to the window that owns the menu.
            // </summary>
            MENUCHAR = 0x0120,
            // <summary>
            // The WM_ENTERIDLE message is sent to the owner window of a modal dialog box or menu that is entering an idle state. A modal dialog box or menu enters an idle state when no messages are waiting in its queue after it has processed one or more previous messages.
            // </summary>
            ENTERIDLE = 0x0121,
            // <summary>
            // The WM_MENURBUTTONUP message is sent when the user releases the right mouse button while the cursor is on a menu item.
            // </summary>
            MENURBUTTONUP = 0x0122,
            // <summary>
            // The WM_MENUDRAG message is sent to the owner of a drag-and-drop menu when the user drags a menu item.
            // </summary>
            MENUDRAG = 0x0123,
            // <summary>
            // The WM_MENUGETOBJECT message is sent to the owner of a drag-and-drop menu when the mouse cursor enters a menu item or moves from the center of the item to the top or bottom of the item.
            // </summary>
            MENUGETOBJECT = 0x0124,
            // <summary>
            // The WM_UNINITMENUPOPUP message is sent when a drop-down menu or submenu has been destroyed.
            // </summary>
            UNINITMENUPOPUP = 0x0125,
            // <summary>
            // The WM_MENUCOMMAND message is sent when the user makes a selection from a menu.
            // </summary>
            MENUCOMMAND = 0x0126,
            // <summary>
            // An application sends the WM_CHANGEUISTATE message to indicate that the user interface (UI) state should be changed.
            // https://devblogs.microsoft.com/oldnewthing/20130516-00/?p=4343
            // </summary>
            CHANGEUISTATE = 0x0127,
            // <summary>
            // An application sends the WM_UPDATEUISTATE message to change the user interface (UI) state for the specified window and all its child windows.
            // https://devblogs.microsoft.com/oldnewthing/20130516-00/?p=4343
            // </summary>
            UPDATEUISTATE = 0x0128,
            // <summary>
            // An application sends the WM_QUERYUISTATE message to retrieve the user interface (UI) state for a window.
            // </summary>
            QUERYUISTATE = 0x0129,
            // <summary>
            // The WM_CTLCOLORMSGBOX message is sent to the owner window of a message box before Windows draws the message box. By responding to this message, the owner window can set the text and background colors of the message box by using the given display device context handle.
            // </summary>
            CTLCOLORMSGBOX = 0x0132,
            // <summary>
            // An edit control that is not read-only or disabled sends the WM_CTLCOLOREDIT message to its parent window when the control is about to be drawn. By responding to this message, the parent window can use the specified device context handle to set the text and background colors of the edit control.
            // </summary>
            CTLCOLOREDIT = 0x0133,
            // <summary>
            // Sent to the parent window of a list box before the system draws the list box. By responding to this message, the parent window can set the text and background colors of the list box by using the specified display device context handle.
            // </summary>
            CTLCOLORLISTBOX = 0x0134,
            // <summary>
            // The WM_CTLCOLORBTN message is sent to the parent window of a button before drawing the button. The parent window can change the button's text and background colors. However, only owner-drawn buttons respond to the parent window processing this message.
            // </summary>
            CTLCOLORBTN = 0x0135,
            // <summary>
            // The WM_CTLCOLORDLG message is sent to a dialog box before the system draws the dialog box. By responding to this message, the dialog box can set its text and background colors using the specified display device context handle.
            // </summary>
            CTLCOLORDLG = 0x0136,
            // <summary>
            // The WM_CTLCOLORSCROLLBAR message is sent to the parent window of a scroll bar control when the control is about to be drawn. By responding to this message, the parent window can use the display context handle to set the background color of the scroll bar control.
            // </summary>
            CTLCOLORSCROLLBAR = 0x0137,
            // <summary>
            // A static control, or an edit control that is read-only or disabled, sends the WM_CTLCOLORSTATIC message to its parent window when the control is about to be drawn. By responding to this message, the parent window can use the specified device context handle to set the text and background colors of the static control.
            // </summary>
            CTLCOLORSTATIC = 0x0138,
            CB_GETEDITSEL = 0x0140,
            CB_LIMITTEXT = 0x0141,
            CB_SETEDITSEL = 0x0142,
            CB_ADDSTRING = 0x0143,
            CB_DELETESTRING = 0x0144,
            CB_DIR = 0x0145,
            CB_GETCOUNT = 0x0146,
            CB_GETCURSEL = 0x0147,
            CB_GETLBTEXT = 0x0148,
            CB_GETLBTEXTLEN = 0x0149,
            CB_INSERTSTRING = 0x014A,
            CB_RESETCONTENT = 0x014B,
            CB_FINDSTRING = 0x014C,
            CB_SELECTSTRING = 0x014D,
            CB_SETCURSEL = 0x014E,
            CB_SHOWDROPDOWN = 0x014F,
            CB_GETITEMDATA = 0x0150,
            CB_SETITEMDATA = 0x0151,
            CB_GETDROPPEDCONTROLRECT = 0x0152,
            CB_SETITEMHEIGHT = 0x0153,
            CB_GETITEMHEIGHT = 0x0154,
            CB_SETEXTENDEDUI = 0x0155,
            CB_GETEXTENDEDUI = 0x0156,
            CB_GETDROPPEDSTATE = 0x0157,
            CB_FINDSTRINGEXACT = 0x0158,
            CB_SETLOCALE = 0x0159,
            CB_GETLOCALE = 0x015A,
            CB_GETTOPINDEX = 0x015B,
            CB_SETTOPINDEX = 0x015C,
            CB_GETHORIZONTALEXTENT = 0x015D,
            CB_SETHORIZONTALEXTENT = 0x015E,
            CB_GETDROPPEDWIDTH = 0x015F,
            CB_SETDROPPEDWIDTH = 0x0160,
            CB_INITSTORAGE = 0x0161,
            // <summary>
            // Use WM_MOUSEFIRST to specify the first mouse message. Use the PeekMessage() Function.
            // </summary>
            MOUSEFIRST = 0x0200,
            // <summary>
            // The WM_MOUSEMOVE message is posted to a window when the cursor moves. If the mouse is not captured, the message is posted to the window that contains the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            // </summary>
            MOUSEMOVE = 0x0200,
            // <summary>
            // The WM_LBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            // </summary>
            LBUTTONDOWN = 0x0201,
            // <summary>
            // The WM_LBUTTONUP message is posted when the user releases the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            // </summary>
            LBUTTONUP = 0x0202,
            // <summary>
            // The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            // </summary>
            LBUTTONDBLCLK = 0x0203,
            // <summary>
            // The WM_RBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            // </summary>
            RBUTTONDOWN = 0x0204,
            // <summary>
            // The WM_RBUTTONUP message is posted when the user releases the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            // </summary>
            RBUTTONUP = 0x0205,
            // <summary>
            // The WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            // </summary>
            RBUTTONDBLCLK = 0x0206,
            // <summary>
            // The WM_MBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            // </summary>
            MBUTTONDOWN = 0x0207,
            // <summary>
            // The WM_MBUTTONUP message is posted when the user releases the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            // </summary>
            MBUTTONUP = 0x0208,
            // <summary>
            // The WM_MBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            // </summary>
            MBUTTONDBLCLK = 0x0209,
            // <summary>
            // The WM_MOUSEWHEEL message is sent to the focus window when the mouse wheel is rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until it finds a window that processes it.
            // </summary>
            MOUSEWHEEL = 0x020A,
            // <summary>
            // The WM_XBUTTONDOWN message is posted when the user presses the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            // </summary>
            XBUTTONDOWN = 0x020B,
            // <summary>
            // The WM_XBUTTONUP message is posted when the user releases the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            // </summary>
            XBUTTONUP = 0x020C,
            // <summary>
            // The WM_XBUTTONDBLCLK message is posted when the user double-clicks the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            // </summary>
            XBUTTONDBLCLK = 0x020D,
            // <summary>
            // The WM_MOUSEHWHEEL message is sent to the focus window when the mouse's horizontal scroll wheel is tilted or rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until it finds a window that processes it.
            // </summary>
            MOUSEHWHEEL = 0x020E,
            // <summary>
            // Use WM_MOUSELAST to specify the last mouse message. Used with PeekMessage() Function.
            // </summary>
            MOUSELAST = 0x020E,
            // <summary>
            // The WM_PARENTNOTIFY message is sent to the parent of a child window when the child window is created or destroyed,
            // or when the user clicks a mouse button while the cursor is over the child window. When the child window is being created,
            // the system sends WM_PARENTNOTIFY just before the CreateWindow or CreateWindowEx function that creates the window returns.
            // When the child window is being destroyed, the system sends the message before any processing to destroy the window takes place.
            // This message is now extended to include the WM_POINTERDOWN event.
            // </summary>
            PARENTNOTIFY = 0x0210,
            // <summary>
            // The WM_ENTERMENULOOP message informs an application's main window procedure that a menu modal loop has been entered.
            // </summary>
            ENTERMENULOOP = 0x0211,
            // <summary>
            // The WM_EXITMENULOOP message informs an application's main window procedure that a menu modal loop has been exited.
            // </summary>
            EXITMENULOOP = 0x0212,
            // <summary>
            // The WM_NEXTMENU message is sent to an application when the right or left arrow key is used to switch between the menu bar and the system menu.
            // </summary>
            NEXTMENU = 0x0213,
            // <summary>
            // The WM_SIZING message is sent to a window that the user is resizing. By processing this message, an application can monitor the size and position of the drag rectangle and, if needed, change its size or position.
            // </summary>
            SIZING = 0x0214,
            // <summary>
            // The WM_CAPTURECHANGED message is sent to the window that is losing the mouse capture.
            // </summary>
            CAPTURECHANGED = 0x0215,
            // <summary>
            // The WM_MOVING message is sent to a window that the user is moving. By processing this message, an application can monitor the position of the drag rectangle and, if needed, change its position.
            // </summary>
            MOVING = 0x0216,
            // <summary>
            // Notifies applications that a power-management event has occurred.
            // </summary>
            POWERBROADCAST = 0x0218,
            // <summary>
            // Notifies an application of a change to the hardware configuration of a device or the computer.
            // </summary>
            DEVICECHANGE = 0x0219,
            // <summary>
            // An application sends the WM_MDICREATE message to a multiple-document interface (MDI) client window to create an MDI child window.
            // </summary>
            MDICREATE = 0x0220,
            // <summary>
            // An application sends the WM_MDIDESTROY message to a multiple-document interface (MDI) client window to close an MDI child window.
            // </summary>
            MDIDESTROY = 0x0221,
            // <summary>
            // An application sends the WM_MDIACTIVATE message to a multiple-document interface (MDI) client window to instruct the client window to activate a different MDI child window.
            // </summary>
            MDIACTIVATE = 0x0222,
            // <summary>
            // An application sends the WM_MDIRESTORE message to a multiple-document interface (MDI) client window to restore an MDI child window from maximized or minimized size.
            // </summary>
            MDIRESTORE = 0x0223,
            // <summary>
            // An application sends the WM_MDINEXT message to a multiple-document interface (MDI) client window to activate the next or previous child window.
            // </summary>
            MDINEXT = 0x0224,
            // <summary>
            // An application sends the WM_MDIMAXIMIZE message to a multiple-document interface (MDI) client window to maximize an MDI child window. The system resizes the child window to make its client area fill the client window. The system places the child window's window menu icon in the rightmost position of the frame window's menu bar, and places the child window's restore icon in the leftmost position. The system also appends the title bar text of the child window to that of the frame window.
            // </summary>
            MDIMAXIMIZE = 0x0225,
            // <summary>
            // An application sends the WM_MDITILE message to a multiple-document interface (MDI) client window to arrange all of its MDI child windows in a tile format.
            // </summary>
            MDITILE = 0x0226,
            // <summary>
            // An application sends the WM_MDICASCADE message to a multiple-document interface (MDI) client window to arrange all its child windows in a cascade format.
            // </summary>
            MDICASCADE = 0x0227,
            // <summary>
            // An application sends the WM_MDIICONARRANGE message to a multiple-document interface (MDI) client window to arrange all minimized MDI child windows. It does not affect child windows that are not minimized.
            // </summary>
            MDIICONARRANGE = 0x0228,
            // <summary>
            // An application sends the WM_MDIGETACTIVE message to a multiple-document interface (MDI) client window to retrieve the handle to the active MDI child window.
            // </summary>
            MDIGETACTIVE = 0x0229,
            // <summary>
            // An application sends the WM_MDISETMENU message to a multiple-document interface (MDI) client window to replace the entire menu of an MDI frame window, to replace the window menu of the frame window, or both.
            // </summary>
            MDISETMENU = 0x0230,
            // <summary>
            // The WM_ENTERSIZEMOVE message is sent one time to a window after it enters the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns.
            // The system sends the WM_ENTERSIZEMOVE message regardless of whether the dragging of full windows is enabled.
            // </summary>
            ENTERSIZEMOVE = 0x0231,
            // <summary>
            // The WM_EXITSIZEMOVE message is sent one time to a window, after it has exited the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns.
            // </summary>
            EXITSIZEMOVE = 0x0232,
            // <summary>
            // Sent when the user drops a file on the window of an application that has registered itself as a recipient of dropped files.
            // </summary>
            DROPFILES = 0x0233,
            // <summary>
            // An application sends the WM_MDIREFRESHMENU message to a multiple-document interface (MDI) client window to refresh the window menu of the MDI frame window.
            // </summary>
            MDIREFRESHMENU = 0x0234,
            // <summary>
            // Sent to an application when a window is activated. A window receives this message through its WindowProc function.
            // </summary>
            IME_SETCONTEXT = 0x0281,
            // <summary>
            // Sent to an application to notify it of changes to the IME window. A window receives this message through its WindowProc function.
            // </summary>
            IME_NOTIFY = 0x0282,
            // <summary>
            // Sent by an application to direct the IME window to carry out the requested command. The application uses this message to control the IME window that it has created. To send this message, the application calls the SendMessage function with the following parameters.
            // </summary>
            IME_CONTROL = 0x0283,
            // <summary>
            // Sent to an application when the IME window finds no space to extend the area for the composition window. A window receives this message through its WindowProc function.
            // </summary>
            IME_COMPOSITIONFULL = 0x0284,
            // <summary>
            // Sent to an application when the operating system is about to change the current IME. A window receives this message through its WindowProc function.
            // </summary>
            IME_SELECT = 0x0285,
            // <summary>
            // Sent to an application when the IME gets a character of the conversion result. A window receives this message through its WindowProc function.
            // </summary>
            IME_CHAR = 0x0286,
            // <summary>
            // Sent to an application to provide commands and request information. A window receives this message through its WindowProc function.
            // </summary>
            IME_REQUEST = 0x0288,
            // <summary>
            // Sent to an application by the IME to notify the application of a key press and to keep message order. A window receives this message through its WindowProc function.
            // </summary>
            IME_KEYDOWN = 0x0290,
            // <summary>
            // Sent to an application by the IME to notify the application of a key release and to keep message order. A window receives this message through its WindowProc function.
            // </summary>
            IME_KEYUP = 0x0291,
            // <summary>
            // The WM_NCMOUSEHOVER message is posted to a window when the cursor hovers over the nonclient area of the window for the period of time specified in a prior call to TrackMouseEvent.
            // </summary>
            NCMOUSEHOVER = 0x02A0,
            // <summary>
            // The WM_MOUSEHOVER message is posted to a window when the cursor hovers over the client area of the window for the period of time specified in a prior call to TrackMouseEvent.
            // </summary>
            MOUSEHOVER = 0x02A1,
            // <summary>
            // The WM_NCMOUSELEAVE message is posted to a window when the cursor leaves the nonclient area of the window specified in a prior call to TrackMouseEvent.
            // </summary>
            NCMOUSELEAVE = 0x02A2,
            // <summary>
            // The WM_MOUSELEAVE message is posted to a window when the cursor leaves the client area of the window specified in a prior call to TrackMouseEvent.
            // </summary>
            MOUSELEAVE = 0x02A3,
            // <summary>
            // The WM_WTSSESSION_CHANGE message notifies applications of changes in session state.
            // </summary>
            WTSSESSION_CHANGE = 0x02B1,
            TABLET_FIRST = 0x02c0,
            TABLET_LAST = 0x02df,
            // <summary>
            // An application sends a WM_CUT message to an edit control or combo box to delete (cut) the current selection, if any, in the edit control and copy the deleted text to the clipboard in CF_TEXT format.
            // </summary>
            CUT = 0x0300,
            // <summary>
            // An application sends the WM_COPY message to an edit control or combo box to copy the current selection to the clipboard in CF_TEXT format.
            // </summary>
            COPY = 0x0301,
            // <summary>
            // An application sends a WM_PASTE message to an edit control or combo box to copy the current content of the clipboard to the edit control at the current caret position. Data is inserted only if the clipboard contains data in CF_TEXT format.
            // </summary>
            PASTE = 0x0302,
            // <summary>
            // An application sends a WM_CLEAR message to an edit control or combo box to delete (clear) the current selection, if any, from the edit control.
            // </summary>
            CLEAR = 0x0303,
            // <summary>
            // An application sends a WM_UNDO message to an edit control to undo the last operation. When this message is sent to an edit control, the previously deleted text is restored or the previously added text is deleted.
            // </summary>
            UNDO = 0x0304,
            // <summary>
            // The WM_RENDERFORMAT message is sent to the clipboard owner if it has delaid rendering a specific clipboard format and if an application has requested data in that format. The clipboard owner must render data in the specified format and place it on the clipboard by calling the SetClipboardData function.
            // </summary>
            RENDERFORMAT = 0x0305,
            // <summary>
            // The WM_RENDERALLFORMATS message is sent to the clipboard owner before it is destroyed, if the clipboard owner has delaid rendering one or more clipboard formats. For the content of the clipboard to remain available to other applications, the clipboard owner must render data in all the formats it is capable of generating, and place the data on the clipboard by calling the SetClipboardData function.
            // </summary>
            RENDERALLFORMATS = 0x0306,
            // <summary>
            // The WM_DESTROYCLIPBOARD message is sent to the clipboard owner when a call to the EmptyClipboard function empties the clipboard.
            // </summary>
            DESTROYCLIPBOARD = 0x0307,
            // <summary>
            // The WM_DRAWCLIPBOARD message is sent to the first window in the clipboard viewer chain when the content of the clipboard changes. This enables a clipboard viewer window to display the new content of the clipboard.
            // </summary>
            DRAWCLIPBOARD = 0x0308,
            // <summary>
            // The WM_PAINTCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and the clipboard viewer's client area needs repainting.
            // </summary>
            PAINTCLIPBOARD = 0x0309,
            // <summary>
            // The WM_VSCROLLCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and an event occurs in the clipboard viewer's vertical scroll bar. The owner should scroll the clipboard image and update the scroll bar values.
            // </summary>
            VSCROLLCLIPBOARD = 0x030A,
            // <summary>
            // The WM_SIZECLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and the clipboard viewer's client area has changed size.
            // </summary>
            SIZECLIPBOARD = 0x030B,
            // <summary>
            // The WM_ASKCBFORMATNAME message is sent to the clipboard owner by a clipboard viewer window to request the name of a CF_OWNERDISPLAY clipboard format.
            // </summary>
            ASKCBFORMATNAME = 0x030C,
            // <summary>
            // The WM_CHANGECBCHAIN message is sent to the first window in the clipboard viewer chain when a window is being removed from the chain.
            // </summary>
            CHANGECBCHAIN = 0x030D,
            // <summary>
            // The WM_HSCROLLCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window. This occurs when the clipboard contains data in the CF_OWNERDISPLAY format and an event occurs in the clipboard viewer's horizontal scroll bar. The owner should scroll the clipboard image and update the scroll bar values.
            // </summary>
            HSCROLLCLIPBOARD = 0x030E,
            // <summary>
            // This message informs a window that it is about to receive the keyboard focus, giving the window the opportunity to realize its logical palette when it receives the focus.
            // </summary>
            QUERYNEWPALETTE = 0x030F,
            // <summary>
            // The WM_PALETTEISCHANGING message informs applications that an application is going to realize its logical palette.
            // </summary>
            PALETTEISCHANGING = 0x0310,
            // <summary>
            // This message is sent by the OS to all top-level and overlapped windows after the window with the keyboard focus realizes its logical palette.
            // This message enables windows that do not have the keyboard focus to realize their logical palettes and update their client areas.
            // </summary>
            PALETTECHANGED = 0x0311,
            // <summary>
            // The WM_HOTKEY message is posted when the user presses a hot key registered by the RegisterHotKey function. The message is placed at the top of the message queue associated with the thread that registered the hot key.
            // </summary>
            HOTKEY = 0x0312,
            // <summary>
            // The WM_PRINT message is sent to a window to request that it draw itself in the specified device context, most commonly in a printer device context.
            // </summary>
            PRINT = 0x0317,
            // <summary>
            // The WM_PRINTCLIENT message is sent to a window to request that it draw its client area in the specified device context, most commonly in a printer device context.
            // </summary>
            PRINTCLIENT = 0x0318,
            // <summary>
            // The WM_APPCOMMAND message notifies a window that the user generated an application command event, for example, by clicking an application command button using the mouse or typing an application command key on the keyboard.
            // </summary>
            APPCOMMAND = 0x0319,
            // <summary>
            // The WM_THEMECHANGED message is broadcast to every window following a theme change event. Examples of theme change events are the activation of a theme, the deactivation of a theme, or a transition from one theme to another.
            // </summary>
            THEMECHANGED = 0x031A,
            // <summary>
            // Sent when the contents of the clipboard have changed.
            // </summary>
            CLIPBOARDUPDATE = 0x031D,
            // <summary>
            // The system will send a window the WM_DWMCOMPOSITIONCHANGED message to indicate that the availability of desktop composition has changed.
            // </summary>
            DWMCOMPOSITIONCHANGED = 0x031E,
            // <summary>
            // WM_DWMNCRENDERINGCHANGED is called when the non-client area rendering status of a window has changed. Only windows that have set the flag DWM_BLURBEHIND.fTransitionOnMaximized to true will get this message.
            // </summary>
            DWMNCRENDERINGCHANGED = 0x031F,
            // <summary>
            // Sent to all top-level windows when the colorization color has changed.
            // </summary>
            DWMCOLORIZATIONCOLORCHANGED = 0x0320,
            // <summary>
            // WM_DWMWINDOWMAXIMIZEDCHANGE will let you know when a DWM composed window is maximized. You also have to register for this message as well. You'd have other windowd go opaque when this message is sent.
            // </summary>
            DWMWINDOWMAXIMIZEDCHANGE = 0x0321,
            // <summary>
            // Sent to request extended title bar information. A window receives this message through its WindowProc function.
            // </summary>
            GETTITLEBARINFOEX = 0x033F,
            HANDHELDFIRST = 0x0358,
            HANDHELDLAST = 0x035F,
            AFXFIRST = 0x0360,
            AFXLAST = 0x037F,
            PENWINFIRST = 0x0380,
            PENWINLAST = 0x038F,
            // <summary>
            // The WM_APP constant is used by applications to help define private messages, usually of the form WM_APP+X, where X is an integer value.
            // </summary>
            APP = 0x8000,
            // <summary>
            // The WM_USER constant is used by applications to help define private messages for use by private window classes, usually of the form WM_USER+X, where X is an integer value.
            // </summary>
            USER = 0x0400,

            // <summary>
            // An application sends the WM_CPL_LAUNCH message to Windows Control Panel to request that a Control Panel application be started.
            // </summary>
            CPL_LAUNCH = USER + 0x1000,
            // <summary>
            // The WM_CPL_LAUNCHED message is sent when a Control Panel application, started by the WM_CPL_LAUNCH message, has closed. The WM_CPL_LAUNCHED message is sent to the window identified by the wParam parameter of the WM_CPL_LAUNCH message that started the application.
            // </summary>
            CPL_LAUNCHED = USER + 0x1001,

            OCM_CTLCOLOR = 0x2019,
            // if ( msg.Msg == PI.WM_.OCM_NOTIFY )
            //{
            //    PI.NMHEADER h2 = (PI.NMHEADER)m.GetLParam(typeof(PI.NMHEADER));
            //      if (h2.nmhdr.hwndFrom == Handle)
            //    if ( (h2.nmhdr.code == (int)Win32.NM.NM_CUSTOMDRAW))
            //    {
            //        Win32.NMCUSTOMDRAW nmcd = (Win32.NMCUSTOMDRAW)msg.GetLParam( typeof( Win32.NMCUSTOMDRAW ) );
            //        msg.Result = (IntPtr)OnCustomDraw( (int)msg.WParam.ToInt32(), ref nmcd );
            //        messageHandled = true;
            //    }
            //}
            OCM_NOTIFY = 0x0204E,   // https://wiki.winehq.org/List_Of_Windows_Messages


            // Following are the ShellProc messages via RegisterShellHookWindow
            // <summary>
            // The accessibility state has changed.
            // </summary>
            HSHELL_ACCESSIBILITYSTATE = 11,
            // <summary>
            // The shell should activate its main window.
            // </summary>
            HSHELL_ACTIVATESHELLWINDOW = 3,
            // <summary>
            // The user completed an input event (for example, pressed an application command button on the mouse or an application command key on the keyboard), and the application did not handle the WM_APPCOMMAND message generated by that input.
            // If the Shell procedure handles the WM_COMMAND message, it should not call CallNextHookEx. See the Return Value section for more information.
            // </summary>
            HSHELL_APPCOMMAND = 12,
            // <summary>
            // A window is being minimized or maximized. The system needs the coordinates of the minimized rectangle for the window.
            // </summary>
            HSHELL_GETMINRECT = 5,
            // <summary>
            // Keyboard language was changed or a new keyboard layout was loaded.
            // </summary>
            HSHELL_LANGUAGE = 8,
            // <summary>
            // The title of a window in the task bar has been redrawn.
            // </summary>
            HSHELL_REDRAW = 6,
            // <summary>
            // The user has selected the task list. A shell application that provides a task list should return TRUE to prevent Windows from starting its task list.
            // </summary>
            HSHELL_TASKMAN = 7,
            // <summary>
            // A top-level, unowned window has been created. The window exists when the system calls this hook.
            // </summary>
            HSHELL_WINDOWCREATED = 1,
            // <summary>
            // A top-level, unowned window is about to be destroyed. The window still exists when the system calls this hook.
            // </summary>
            HSHELL_WINDOWDESTROYED = 2,
            // <summary>
            // The activation has changed to a different top-level, unowned window.
            // </summary>
            HSHELL_WINDOWACTIVATED = 4,
            // <summary>
            // A top-level window is being replaced. The window exists when the system calls this hook.
            // </summary>
            HSHELL_WINDOWREPLACED = 13;
        }

        /// <summary>
        /// CS_*
        /// </summary>
        [Flags]
        internal enum CS_ : uint
        {
            VREDRAW = 0x0001,
            HREDRAW = 0x0002,
            DBLCLKS = 0x0008,
            OWNDC = 0x0020,
            CLASSDC = 0x0040,
            PARENTDC = 0x0080,
            NOCLOSE = 0x0200,
            SAVEBITS = 0x0800,
            BYTEALIGNCLIENT = 0x1000,
            BYTEALIGNWINDOW = 0x2000,
            GLOBALCLASS = 0x4000,
            IME = 0x00010000,
            DROPSHADOW = 0x00020000
        }

        internal enum GetWindowType : uint
        {
            /// <summary>
            /// The retrieved handle identifies the window of the same type that is highest in the Z order.
            /// <para/>
            /// If the specified window is a topmost window, the handle identifies a topmost window.
            /// If the specified window is a top-level window, the handle identifies a top-level window.
            /// If the specified window is a child window, the handle identifies a sibling window.
            /// </summary>
            GW_HWNDFIRST = 0,
            /// <summary>
            /// The retrieved handle identifies the window of the same type that is lowest in the Z order.
            /// <para />
            /// If the specified window is a topmost window, the handle identifies a topmost window.
            /// If the specified window is a top-level window, the handle identifies a top-level window.
            /// If the specified window is a child window, the handle identifies a sibling window.
            /// </summary>
            GW_HWNDLAST = 1,
            /// <summary>
            /// The retrieved handle identifies the window below the specified window in the Z order.
            /// <para />
            /// If the specified window is a topmost window, the handle identifies a topmost window.
            /// If the specified window is a top-level window, the handle identifies a top-level window.
            /// If the specified window is a child window, the handle identifies a sibling window.
            /// </summary>
            GW_HWNDNEXT = 2,
            /// <summary>
            /// The retrieved handle identifies the window above the specified window in the Z order.
            /// <para />
            /// If the specified window is a topmost window, the handle identifies a topmost window.
            /// If the specified window is a top-level window, the handle identifies a top-level window.
            /// If the specified window is a child window, the handle identifies a sibling window.
            /// </summary>
            GW_HWNDPREV = 3,
            /// <summary>
            /// The retrieved handle identifies the specified window's owner window, if any.
            /// </summary>
            GW_OWNER = 4,
            /// <summary>
            /// The retrieved handle identifies the child window at the top of the Z order,
            /// if the specified window is a parent window; otherwise, the retrieved handle is NULL.
            /// The function examines only child windows of the specified window. It does not examine descendant windows.
            /// </summary>
            GW_CHILD = 5,
            /// <summary>
            /// The retrieved handle identifies the enabled popup window owned by the specified window (the
            /// search uses the first such window found using GW_HWNDNEXT); otherwise, if there are no enabled
            /// popup windows, the retrieved handle is that of the specified window.
            /// </summary>
            GW_ENABLEDPOPUP = 6
        }

        /// <summary>
        /// Window Styles.
        /// The following styles can be specified wherever a window style is required. After the control has been created, these styles cannot be modified, except as noted.
        /// https://www.autohotkey.com/docs/misc/Styles.htm
        /// </summary>
        internal struct WS_
        {
            public const uint
                // <summary>The window has a thin-line border.</summary>
                BORDER = 0x800000,

                // <summary>The window has a title bar (includes the WS_BORDER style).</summary>
                CAPTION = 0xc00000,

                // <summary>The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.</summary>
                CHILD = 0x40000000,

                // <summary>Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window.</summary>
                CLIPCHILDREN = 0x2000000,

                // <summary>
                // Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated.
                // If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window.
                // </summary>
                CLIPSIBLINGS = 0x4000000,

                // <summary>The window is initially disabled. A disabled window cannot receive input from the user. To change this after a window has been created, use the EnableWindow function.</summary>
                DISABLED = 0x8000000,

                // <summary>The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.</summary>
                DLGFRAME = 0x400000,

                // <summary>
                // The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style.
                // The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
                // You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
                // </summary>
                GROUP = 0x20000,

                // <summary>The window is initially maximized.</summary>
                MAXIMIZE = 0x1000000,

                // <summary>The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>
                MAXIMIZEBOX = 0x10000,

                // <summary>The window is initially minimized.</summary>
                MINIMIZE = 0x20000000,

                // <summary>The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>
                MINIMIZEBOX = 0x20000,

                // <summary>The window is an overlapped window. An overlapped window has a title bar and a border.</summary>
                OVERLAPPED = 0x0,

                // <summary>The window is an overlapped window.</summary>
                OVERLAPPEDWINDOW = OVERLAPPED | CAPTION | SYSMENU | SIZEFRAME | MINIMIZEBOX | MAXIMIZEBOX,

                // <summary>The window is a pop-up window. This style cannot be used with the WS_CHILD style.</summary>
                POPUP = 0x80000000u,

                // <summary>The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.</summary>
                POPUPWINDOW = POPUP | BORDER | SYSMENU,

                // <summary>The window has a sizing border.</summary>
                SIZEFRAME = 0x40000,

                // <summary>The window has a window menu on its title bar. The WS_CAPTION style must also be specified.</summary>
                SYSMENU = 0x80000,

                // <summary>
                // The window is a control that can receive the keyboard focus when the user presses the TAB key.
                // Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.
                // You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
                // For user-created windows and modeless dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function.
                // </summary>
                TABSTOP = 0x10000,

                // <summary>The window is initially visible. This style can be turned on and off by using the ShowWindow or SetWindowPos function.</summary>
                VISIBLE = 0x10000000,

                // <summary>The window has a horizontal scroll bar.</summary>
                HSCROLL = 0x100000,

                // <summary>The window has a vertical scroll bar.</summary>
                VSCROLL = 0x200000;
        }

        /// <summary>
        /// Window Styles.
        /// The following styles can be specified wherever a window style is required. After the control has been created, these styles cannot be modified, except as noted.
        /// https://www.autohotkey.com/docs/misc/Styles.htm
        /// </summary>
        internal struct BS_
        {
            public const uint
            PUSHBUTTON = 0x0,   // Creates a push button that posts a WM_COMMAND message to the owner window when the user selects the button.
            DEFPUSHBUTTON = 0x1,   // +/-Default.Creates a push button with a heavy black border.If the button is in a dialog box, the user can select the button by pressing Enter, even when the button does not have the input focus.This style is useful for enabling the user to quickly select the most likely option.
            CHECKBOX = 0x2, // Creates a small, empty check box with text.By default, the text is Displayed to the right of the check box.To display the text to the left of the check box, combine this flag with the BS_LEFTTEXT style (or with the equivalent BS_RIGHTBUTTON style).
            AUTOCHECKBOX = 0x3,  // Creates a button that is the same as a check box, except that the check state automatically toggles between checked and cleared each time the user selects the check box.
            RADIOBUTTON = 0x4, // Creates a small circle with text. By default, the text is Displayed to the right of the circle. To display the text to the left of the circle, combine this flag with the BS_LEFTTEXT style (or with the equivalent BS_RIGHTBUTTON style). Use radio buttons for groups of related, but mutually exclusive choices.
            _3STATE = 0x5,      // Creates a button that is the same as a check box, except that the box can be grayed as well as checked or cleared. Use the grayed state to show that the state of the check box is not determined.
            AUTO3STATE = 0x6,   // Creates a button that is the same as a three-state check box, except that the box changes its state when the user selects it.The state cycles through checked, indeterminate, and cleared.AUTOCHECKBOX = 0x3,   // Creates a button that is the same as a check box, except that the check state automatically toggles between checked and cleared each time the user selects the check box.
            GROUPBOX = 0x7,   // Creates a rectangle in which other controls can be grouped. Any text associated with this style is Displayed in the rectangle's upper left corner.
            USERBUTTON = 0x8, // Obsolete, but provided for compatibility with 16-bit versions of Windows. Applications should use BS_OWNERDRAW instead.
            AUTORADIOBUTTON = 0x9,   // Creates a button that is the same as a radio button, except that when the user selects it, the system automatically sets the button's check state to checked and automatically sets the check state for all other buttons in the same group to cleared.
            // PUSHBOX = 0xA
            OWNERDRAW = 0xB, // Creates an owner-drawn button. The owner window receives a WM_DRAWITEM message when a visual aspect of the button has changed. Do not combine the BS_OWNERDRAW style with any other button styles.
            TYPEMASK = 0xF, // Do not use this style. A composite style bit that results from using the OR operator on BS_* style bits. It can be used to mask out valid BS_* bits from a given bitmask. Note that this is out of date and does not correctly include all valid styles. Thus, you should not use this style.
            LEFTTEXT = 0x20, // Places text on the left side of the radio button or check box when combined with a radio button or check box style. Same as the BS_RIGHTBUTTON style.
            RIGHTBUTTON = 0x20,   // + Right (i.e. +Right includes both BS_RIGHT and BS_RIGHTBUTTON, but -Right removes only BS_RIGHT, not BS_RIGHTBUTTON). Positions a checkbox square or radio button circle on the right side of the control's available width instead of the left.

            TEXT = 0x0, // Specifies that the button displays text.
            ICON = 0x40, // Specifies that the button displays an icon. See the Remarks section for its interaction with BS_BITMAP.
            BITMAP = 0x80,       // Specifies that the button displays a bitmap. See the Remarks section for its interaction with BS_ICON
            LEFT = 0x100,   // +/-Left.Left-aligns the text.
            RIGHT = 0x200,   // +/-Right.Right-aligns the text.
            CENTER = 0x300,   // +/-Center.Centers the text horizontally within the control's available width.
            TOP = 0x400,   // Places text at the top of the control's available height.
            BOTTOM = 0x800,   // Places the text at the bottom of the control's available height.
            VCENTER = 0xC00,   // Vertically centers text in the control's available height.

            PUSHLIKE = 0x1000,   // Makes a checkbox or radio button look and act like a push button.The button looks raised when it isn't pushed or checked, and sunken when it is pushed or checked.
            MULTILINE = 0x2000,   // +/-Wrap.Wraps the text to multiple lines if the text is too long to fit on a single line in the control's available width. This also allows linefeed (`n) to start new lines of text.
            NOTIFY = 0x4000,   // Enables a button to send BN_KILLFOCUS and BN_SETFOCUS notification codes to its parent window. Note that buttons send the BN_CLICKED notification code regardless of whether it has this style.To get BN_DBLCLK notification codes, the button must have the BS_RADIOBUTTON or BS_OWNERDRAW style.
            FLAT = 0x8000;   // Specifies that the button is two-dimensional; it does not use the default shading to create a 3-D effect.

            /*
COMMANDLINK = 0x0, // Creates a command link button that behaves like a BS_PUSHBUTTON style button, but the command link button has a green arrow on the left pointing to the button text. A caption for the button text can be set by sending the BCM_SETNOTE message to the button.
DEFCOMMANDLINK = 0x0, // Creates a command link button that behaves like a BS_PUSHBUTTON style button. If the button is in a dialog box, the user can select the command link button by pressing the ENTER key, even when the command link button does not have the input focus. This style is useful for enabling the user to quickly select the most likely (default) option.
DEFSPLITBUTTON = 0x0, // Creates a split button that behaves like a BS_PUSHBUTTON style button, but also has a distinctive appearance. If the split button is in a dialog box, the user can select the split button by pressing the ENTER key, even when the split button does not have the input focus. This style is useful for enabling the user to quickly select the most likely (default) option.
NOTIFY = 0x0, // Enables a button to send BN_KILLFOCUS and BN_SETFOCUS notification codes to its parent window.
                //Note that buttons send the BN_CLICKED notification code regardless of whether it has this style. To get BN_DBLCLK notification codes, the button must have the BS_RADIOBUTTON or BS_OWNERDRAW style.
SPLITBUTTON = 0x0, // Creates a split button. A split button has a drop-down arrow.
BS_ICON or BS_BITMAP set? 	BM_SETIMAGE called? 	Result
    Yes 	                Yes 	                Show icon only.
    No 	                    Yes 	                Show icon and text.
    Yes 	                No 	                    Show text only.
    No 	                    No 	                    Show text only
            */
        }

        /// <summary>
        /// Window style extended values, WS_EX_*
        /// https://docs.microsoft.com/en-gb/windows/win32/winmsg/extended-window-styles
        /// </summary>
        internal struct WS_EX_
        {
            public const uint
                None = 0,
                DLGMODALFRAME = 0x00000001,
                NOPARENTNOTIFY = 0x00000004,
                TOPMOST = 0x00000008,
                ACCEPTFILES = 0x00000010,
                TRANSPARENT = 0x00000020,
                MDICHILD = 0x00000040,
                TOOLWINDOW = 0x00000080,
                WINDOWEDGE = 0x00000100,
                CLIENTEDGE = 0x00000200,
                CONTEXTHELP = 0x00000400,
                RIGHT = 0x00001000,
                LEFT = 0x00000000,
                RTLREADING = 0x00002000,
                LTRREADING = 0x00000000,
                LEFTSCROLLBAR = 0x00004000,
                RIGHTSCROLLBAR = 0x00000000,
                CONTROLPARENT = 0x00010000,
                STATICEDGE = 0x00020000,
                APPWINDOW = 0x00040000,
                LAYERED = 0x00080000,
                NOINHERITLAYOUT = 0x00100000, // Disable inheritance of mirroring by children
                NOREDIRECTIONBITMAP = 0x00200000, // The window does not render to a redirection surface. This is for windows that do not have visible content or that use mechanisms other than surfaces to provide their visual.
                LAYOUTRTL = 0x00400000, // Right to left mirroring
                COMPOSITED = 0x02000000,
                NOACTIVATE = 0x08000000,
                OVERLAPPEDWINDOW = WINDOWEDGE + CLIENTEDGE,
                PALETTEWINDOW = WINDOWEDGE + TOOLWINDOW + TOPMOST;
        }

        internal enum MF_ : uint
        {
            INSERT = 0x00000000,
            CHANGE = 0x00000080,
            APPEND = 0x00000100,
            DELETE = 0x00000200,
            REMOVE = 0x00001000,

            BYCOMMAND = 0x00000000,
            BYPOSITION = 0x00000400,

            SEPARATOR = 0x00000800,

            ENABLED = 0x00000000,
            GRAYED = 0x00000001,
            DISABLED = 0x00000002,

            UNCHECKED = 0x00000000,
            CHECKED = 0x00000008,
            USECHECKBITMAPS = 0x00000200,

            STRING = 0x00000000,
            BITMAP = 0x00000004,
            OWNERDRAW = 0x00000100,

            POPUP = 0x00000010,
            MENUBARBREAK = 0x00000020,
            MENUBREAK = 0x00000040,

            UNHILITE = 0x00000000,
            HILITE = 0x00000080,

            DEFAULT = 0x00001000,
            SYSMENU = 0x00002000,
            HELP = 0x00004000,
            RIGHTJUSTIFY = 0x00004000,

            MOUSESELECT = 0x00008000
        }

        internal enum SC_
        {
            SIZE = 0xF000,
            MOVE = 0xF010,
            MINIMIZE = 0xF020,
            MAXIMIZE = 0xF030,
            NEXTWINDOW = 0xF040,
            PREVWINDOW = 0xF050,
            CLOSE = 0xF060,
            VSCROLL = 0xF070,
            HSCROLL = 0xF080,
            MOUSEMENU = 0xF090,
            KEYMENU = 0xF100,
            ARRANGE = 0xF110,
            RESTORE = 0xF120,
            TASKLIST = 0xF130,
            SCREENSAVE = 0xF140,
            HOTKEY = 0xF150,
            DEFAULT = 0xF160,
            MONITORPOWER = 0xF170,
            CONTEXTHELP = 0xF180,
            SEPARATOR = 0xF00F,
            /// <summary>
            /// SCF_ISSECURE
            /// </summary>
            F_ISSECURE = 0x00000001,
            ICON = MINIMIZE,
            ZOOM = MAXIMIZE
        }
        /// <summary>
        /// Non-client hit test values, HT*
        /// </summary>
        internal struct HT
        {
            public const int
            ERROR = -2,
            TRANSPARENT = -1,
            NOWHERE = 0,
            CLIENT = 1,
            CAPTION = 2,
            SYSMENU = 3,
            GROWBOX = 4,
            SIZE = GROWBOX,
            MENU = 5,
            HSCROLL = 6,
            VSCROLL = 7,
            MINBUTTON = 8,
            MAXBUTTON = 9,
            LEFT = 10,
            RIGHT = 11,
            TOP = 12,
            TOPLEFT = 13,
            TOPRIGHT = 14,
            BOTTOM = 15,
            BOTTOMLEFT = 16,
            BOTTOMRIGHT = 17,
            BORDER = 18,
            REDUCE = MINBUTTON,
            ZOOM = MAXBUTTON,
            SIZEFIRST = LEFT,
            SIZELAST = BOTTOMRIGHT,
            OBJECT = 19,
            CLOSE = 20,
            HELP = 21;
        }

        /// <summary>
        /// GetWindowLongPtr values, GWL_*
        /// </summary>
        internal enum GWL_
        {
            WNDPROC = -4,
            HINSTANCE = -6,
            HWNDPARENT = -8,
            STYLE = -16,
            EXSTYLE = -20,
            USERDATA = -21,
            ID = -12
        }

        internal enum WH_ : int
        {
            JOURNALRECORD = 0,
            JOURNALPLAYBACK = 1,
            KEYBOARD = 2,
            GETMESSAGE = 3,
            CALLWNDPROC = 4,
            CBT = 5,
            SYSMSGFILTER = 6,
            MOUSE = 7,
            HARDWARE = 8,
            DEBUG = 9,
            SHELL = 10,
            FOREGROUNDIDLE = 11,
            CALLWNDPROCRET = 12,
            KEYBOARD_LL = 13,
            MOUSE_LL = 14
        }

        [Flags]
        public enum LWA_ : uint
        {
            COLORKEY = 0x00000001,
            ALPHA = 0x00000002
        }

        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms644991(v=vs.85).aspx
        /// </summary>
        internal const int HSHELL_REDRAW = 6; // The title of a window in the task bar has been redrawn.

        internal const int CURSOR_SHOWING = 0x00000001;
        internal const int VK_ESCAPE = 0x1B;
        internal const int VK_MENU = 0x12;  // https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        internal const int VK_LMENU = 0xA4;
        internal const int VK_RMENU = 0xA5;
        internal const int PRF_CLIENT = 0x00000004;
        internal const int MA_NOACTIVATE = 0x03;
        internal const int EM_FORMATRANGE = 0x0439;
        internal const int RDW_INVALIDATE = 0x0001;
        internal const int RDW_UPDATENOW = 0x0100;
        internal const int RDW_FRAME = 0x0400;
        internal const int DCX_WINDOW = 0x01;
        internal const int DCX_CACHE = 0x02;
        internal const int DCX_CLIPSIBLINGS = 0x10;
        internal const int DCX_INTERSECTRGN = 0x80;
        internal const int TME_LEAVE = 0x0002;
        internal const int TME_NONCLIENT = 0x0010;
        internal const int ULW_ALPHA = 0x00000002;
        internal const int DEVICE_BITSPIXEL = 12;
        internal const int DEVICE_PLANES = 14;
        internal const int SRCCOPY = 0xCC0020;
        internal const int DTM_SETMCCOLOR = 0x1006;
        internal const int DTT_COMPOSITED = 8192;
        internal const int DTT_GLOWSIZE = 2048;
        internal const int DTT_TEXTCOLOR = 1;
        internal const int MCSC_BACKGROUND = 0;
        internal const byte AC_SRC_OVER = 0x00;
        internal const byte AC_SRC_ALPHA = 0x01;
        internal const int EM_SETCUEBANNER = 0x1501;
        internal const int CB_SETCUEBANNER = 0x1703;
        internal const int EN_CHANGE = 0x0300;
        #endregion

        #region Static Methods
        internal static int LOWORD(IntPtr value)
        {
            var int32 = (int)value.ToInt64() & 0xFFFF;
            return (int32 > 32767) ? int32 - 65536 : int32;
        }

        internal static int HIWORD(IntPtr value)
        {
            var int32 = ((int)value.ToInt64() >> 0x10) & 0xFFFF;
            return (int32 > 32767) ? int32 - 65536 : int32;
        }

        internal static int LOWORD(int value) => value & 0xFFFF;

        internal static int HIWORD(int value) => (value >> 0x10) & 0xFFFF;

        internal static int MAKELOWORD(int value) => value & 0xFFFF;

        internal static int MAKEHIWORD(int value) => (value & 0xFFFF) << 0x10;

        internal static IntPtr MakeLParam(int LoWord, int HiWord) =>
            new IntPtr((long)((HiWord << 16) | (LoWord & 0xffff)));

        internal static IntPtr MakeWParam(int LoWord, int HiWord) =>
            new IntPtr((long)((HiWord << 16) | (LoWord & 0xffff)));

        /// <summary>
        /// Is the specified key currently pressed down.
        /// </summary>
        /// <param name="key">Key to test.</param>
        /// <returns>True if pressed; otherwise false.</returns>
        internal static bool IsKeyDown(Keys key) => KEY_.DOWN == (GetKeyState(key) & KEY_.DOWN);

        /// <summary>
        /// Is the specified key currently toggled.
        /// </summary>
        /// <param name="key">Key to test.</param>
        /// <returns>True if toggled; otherwise false.</returns>
        internal static bool IsKeyToggled(Keys key) => KEY_.TOGGLED == (GetKeyState(key) & KEY_.TOGGLED);

        private static KEY_ GetKeyState(Keys key)
        {
            KEY_ state = KEY_.NONE;

            var retVal = GetKeyState((int)key);

            if ((retVal & 0x8000) == 0x8000)
            {
                state |= KEY_.DOWN;
            }

            if ((retVal & 1) == 1)
            {
                state |= KEY_.TOGGLED;
            }

            return state;
        }
        #endregion

        #region Static User32
        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint GetDpiForWindow(IntPtr hWnd);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern BOOL EndDialog(IntPtr hDlg, IntPtr nResult);

        internal static BOOL EndDialog(HandleRef hDlg, IntPtr nResult)
        {
            BOOL result = EndDialog(hDlg.Handle, nResult);
            GC.KeepAlive(hDlg.Wrapper);
            return result;
        }

        [DllImport(Libraries.User32, SetLastError = true, EntryPoint = "SetWindowTextW", CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool SetWindowText(IntPtr hwnd, String lpString);

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-isdlgbuttonchecked
        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern BST_ IsDlgButtonChecked(IntPtr hDlg, int nIDButton);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int GetDlgCtrlID(IntPtr hwndCtl);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport(Libraries.User32, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport(Libraries.User32, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        internal delegate bool WindowEnumProc(IntPtr hwnd, IntPtr lparam);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc callback, IntPtr lParam);

        [DllImport(Libraries.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr GetDlgItem(IntPtr hWnd, int nIDDlgItem);

        [DllImport(Libraries.User32, EntryPoint = "GetWindowTextW", CharSet = CharSet.Unicode, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        //If the function succeeds, the return value is the length, in characters, of the copied string,
        //not including the terminating null character. If the window has no title bar or text,
        //if the title bar is empty, or if the window or control handle is invalid,
        //the return value is zero. To get extended error information, call GetLastError.
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        public static string GetWindowText(IntPtr hwnd)
        {
            // Allocate correct string length first
            int length = (int)SendMessage(hwnd, WM_.GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
            StringBuilder sb = new StringBuilder(length + 1);
            SendMessage(hwnd, WM_.GETTEXT, (IntPtr)sb.Capacity, sb);
            return sb.ToString();
        }

        [DllImport(Libraries.User32, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool GetScrollInfo(IntPtr hWnd, SB_ nBar, ref WIN32ScrollBars.ScrollInfo lpScrollInfo);

        [DllImport(Libraries.User32, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int SetScrollInfo(IntPtr hwnd, SB_ nBar, ref WIN32ScrollBars.ScrollInfo lpcScrollInfo, bool redraw);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int GetScrollPos(IntPtr hWnd, SB_ nBar);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int SetScrollPos(IntPtr hWnd, SB_ nBar, int nPos, bool bRedraw);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool GetScrollRange(IntPtr Handle, SB_ nBar, ref IntPtr min, ref IntPtr max);

        [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool SetMenu(HandleRef hWnd, HandleRef hMenu);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int UnhookWindowsHookEx(IntPtr idHook);

        internal delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr SetWindowsHookEx(WH_ idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, LWA_ dwFlags);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern short VkKeyScan(char ch);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr WindowFromPoint(POINT pt);

        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(Libraries.User32, EntryPoint = "GetWindowInfo", SetLastError = true)]
        private static extern bool GetWindowInfoInt(IntPtr hwnd, ref WINDOWINFO pwi);

        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWINFO
        {
            public uint cbSize;
            public RECT rcWindow;
            public RECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;

            public WINDOWINFO(int _ = 0) : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
                =>
                    cbSize = (uint)SizeOf(typeof(WINDOWINFO));
        }
        internal static bool GetWindowInfo(IntPtr hwnd, out WINDOWINFO pwi)
        {
            pwi = new WINDOWINFO();
            return GetWindowInfoInt(hwnd, ref pwi);
        }

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint GetWindowLong(IntPtr hWnd, GWL_ nIndex);

        // This is aliased as a macro in 32bit Windows.
        internal static IntPtr GetWindowLongPtr(IntPtr hwnd, GWL_ nIndex)
        {
            IntPtr ret = IntPtr.Size > 4
                ? GetWindowLongPtr64(hwnd, nIndex)
                : new IntPtr(GetWindowLongPtr32(hwnd, nIndex));
            if (IntPtr.Zero == ret)
            {
                throw new Win32Exception();
            }
            return ret;
        }

        [DllImport(Libraries.User32, EntryPoint = "GetWindowLong", SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern int GetWindowLongPtr32(IntPtr hWnd, GWL_ nIndex);

        [DllImport(Libraries.User32, EntryPoint = "GetWindowLongPtr", SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, GWL_ nIndex);

        internal static IntPtr SetClassLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong, bool noThrow = true)
        {
            IntPtr ret = IntPtr.Size > 4
                ? SetClassLongPtr64(hWnd, nIndex, dwNewLong)
                : new IntPtr(SetClassLongPtr32(hWnd, nIndex, unchecked((uint)dwNewLong.ToInt32())));
            if (!noThrow
            && IntPtr.Zero == ret)
            {
                var error = GetLastWin32Error();
                if (error != 0)
                {
                    throw new Win32Exception(error);
                }
            }
            return ret;
        }

        internal static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex, bool noThrow = true)
        {
            IntPtr ret = IntPtr.Size > 4
                ? GetClassLongPtr64(hWnd, nIndex)
                : new IntPtr(GetClassLongPtr32(hWnd, nIndex));
            if (!noThrow
            && IntPtr.Zero == ret)
            {
                var error = GetLastWin32Error();
                if (error != 0)
                {
                    throw new Win32Exception(error);
                }
            }
            return ret;
        }

        [DllImport(Libraries.User32, EntryPoint = @"GetClassLong", SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern uint GetClassLongPtr32(IntPtr hWnd, int nIndex);

        [DllImport(Libraries.User32, EntryPoint = @"GetClassLongPtr", SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport(Libraries.User32, EntryPoint = "SetClassLong")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint SetClassLongPtr32(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport(Libraries.User32, EntryPoint = "SetClassLongPtr")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr SetClassLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport(Libraries.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool IntersectRect([Out] out RECT lprcDst, [In] ref RECT lprcSrc1, [In] ref RECT lprcSrc2);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool InvalidateRect(IntPtr hWnd, RECT rect, bool erase);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool InvalidateRect(IntPtr hWnd, IntPtr rect, bool erase);

        [DllImport(Libraries.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int LoadString(SafeModuleHandle hInstance, uint uID, StringBuilder lpBuffer, int nBufferMax);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool UpdateWindow(IntPtr hWnd);

        [DllImport(Libraries.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint SetWindowLong(IntPtr hwnd, GWL_ nIndex, uint nLong);

        // This is aliased as a macro in 32bit Windows.
        [SuppressMessage("Microsoft.Performance", @"CA1811:AvoidUncalledPrivateCode")]
        internal static IntPtr SetWindowLongPtr(IntPtr hwnd, GWL_ nIndex, IntPtr dwNewLong) =>
            IntPtr.Size > 4
                ? SetWindowLongPtr64(hwnd, nIndex, dwNewLong)
                : new IntPtr(SetWindowLongPtr32(hwnd, nIndex, dwNewLong.ToInt32()));

        [SuppressMessage("Microsoft.Interoperability", @"CA1400:PInvokeEntryPointsShouldExist")]
        [SuppressMessage("Microsoft.Performance", @"CA1811:AvoidUncalledPrivateCode")]
        [DllImport(Libraries.User32, EntryPoint = "SetWindowLong", SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern int SetWindowLongPtr32(IntPtr hWnd, GWL_ nIndex, int dwNewLong);

        [SuppressMessage("Microsoft.Interoperability", @"CA1400:PInvokeEntryPointsShouldExist")]
        [SuppressMessage("Microsoft.Performance", @"CA1811:AvoidUncalledPrivateCode")]
        [DllImport(Libraries.User32, EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, GWL_ nIndex, IntPtr dwNewLong);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr GetActiveWindow();

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int ShowWindow(IntPtr hWnd, ShowWindowCommands cmdShow);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr GetFocus();

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool HideCaret(IntPtr hWnd);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool ShowCaret(IntPtr hWnd);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern ushort GetKeyState(int virtKey);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr SendDlgItemMessage(IntPtr hDlg, int nIDDlgItem, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport(Libraries.User32, EntryPoint = "SendMessageW")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, [Out] StringBuilder lParam);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref TITLEBARINFOEX lParam);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref TV_ITEM lParam);

        [DllImport(Libraries.User32, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        internal static void PostMessageSafe(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (!PostMessage(hWnd, msg, wParam, lParam))
            {
                // An error occurred
                throw new Win32Exception(GetLastWin32Error());
            }
        }


        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr DeferWindowPos(IntPtr hWinPosInfo, IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, SWP_ uFlags);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr BeginDeferWindowPos(int nNumWindows);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool EndDeferWindowPos(IntPtr hWinPosInfo);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndAfter, int X, int Y, int Width, int Height, SWP_ flags);

        [DllImport(Libraries.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool RedrawWindow(IntPtr hWnd, IntPtr rectUpdate, IntPtr hRgnUpdate, uint uFlags);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool RedrawWindow(IntPtr hWnd, ref RECT rectUpdate, IntPtr hRgnUpdate, uint uFlags);

        [DllImport(Libraries.User32, ExactSpelling = true, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst,
            ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pptSrc, uint crKey,
            [In] ref BLENDFUNCTION pblend, uint dwFlags);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool TrackMouseEvent(ref TRACKMOUSEEVENTS tme);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hRgnClip, uint fdwOptions);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern void DisableProcessWindowsGhosting();

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern void AdjustWindowRect(ref RECT rect, uint dwStyle, bool hasMenu);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern void AdjustWindowRectEx(ref RECT rect, uint dwStyle, bool hasMenu, int dwExSytle);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] POINTC pt, int cPoints);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool TranslateMessage([In] ref MSG lpMsg);

        [DllImport(Libraries.User32, CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr BeginPaint(IntPtr hwnd, ref PAINTSTRUCT ps);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool EndPaint(IntPtr hwnd, ref PAINTSTRUCT ps);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool InflateRect(ref RECT lprc, int dx, int dy);

        [DllImport(Libraries.User32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport(Libraries.User32, CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint RegisterWindowMessage(string lpString);

        [SuppressMessage("Microsoft.Performance", @"CA1811:AvoidUncalledPrivateCode")]
        [DllImport(Libraries.User32, EntryPoint = "GetMonitorInfo", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _GetMonitorInfo(IntPtr hMonitor, [In, Out] MONITORINFO lpmi);

        [SuppressMessage("Microsoft.Performance", @"CA1811:AvoidUncalledPrivateCode")]
        internal static MONITORINFO GetMonitorInfo(IntPtr hMonitor)
        {
            var mi = new MONITORINFO();
            if (!_GetMonitorInfo(hMonitor, mi))
            {
                throw new Win32Exception();
            }
            return mi;
        }

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int EnableMenuItem(IntPtr hMenu, SC_ targetId, MF_ state);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr RemoveMenu(IntPtr hMenu, uint nPosition, MF_ wFlags);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int GetSystemMetrics(SM_ smIndex);

        [DllImport(Libraries.User32, EntryPoint = "GetCursorInfo")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool GetCursorInfo(ref CURSORINFO pci);

        [DllImport(Libraries.User32, EntryPoint = "CopyIcon")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr CopyIcon(IntPtr hIcon);

        [DllImport(Libraries.User32, EntryPoint = "GetIconInfo")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

        [DllImport(Libraries.User32, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool MessageBeep(BeepType type);

        /// <summary>
        /// Retrieves a handle to a window that has the specified relationship (Z-Order or owner) to the specified window.
        /// </summary>
        /// <remarks>The EnumChildWindows function is more reliable than calling GetWindow in a loop. An application that
        /// calls GetWindow to perform this task risks being caught in an infinite loop or referencing a handle to a window
        /// that has been destroyed.</remarks>
        /// <param name="hWnd">A handle to a window. The window handle retrieved is relative to this window, based on the
        /// value of the uCmd parameter.</param>
        /// <param name="uCmd">The relationship between the specified window and the window whose handle is to be
        /// retrieved.</param>
        /// <returns>
        /// If the function succeeds, the return value is a window handle. If no window exists with the specified relationship
        /// to the specified window, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport(Libraries.User32, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr GetWindow(IntPtr hWnd, GetWindowType uCmd);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr ChildWindowFromPoint(IntPtr hWndParent, POINT pt);

        [DllImport(Libraries.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int FillRect(IntPtr hDC, [In] ref RECT lprc, IntPtr hbr);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttribData data);

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttribData
        {
            public Dwm.WindowCompositionAttribute Attribute;
            public IntPtr Data;     // Will point to an AccentPolicy struct, where Attribute will be WindowCompositionAttribute.AccentPolicy
            public int SizeOfData;

            public WindowCompositionAttribData(Dwm.WindowCompositionAttribute attribute, IntPtr data, int sizeOfData)
            {
                Attribute = attribute;
                Data = data;
                SizeOfData = sizeOfData;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal class LOGFONT
        {
            public int lfHeight;
            public int lfWidth;
            public int lfEscapement;
            public int lfOrientation;
            public int lfWeight;
            public byte lfItalic;
            public byte lfUnderline;
            public byte lfStrikeOut;
            public byte lfCharSet;
            public byte lfOutPrecision;
            public byte lfClipPrecision;
            public byte lfQuality;
            public byte lfPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string lfFaceName;

            public LOGFONT()
            {
            }

            public LOGFONT(LOGFONT lf)
            {
                lfHeight = lf.lfHeight;
                lfWidth = lf.lfWidth;
                lfEscapement = lf.lfEscapement;
                lfOrientation = lf.lfOrientation;
                lfWeight = lf.lfWeight;
                lfItalic = lf.lfItalic;
                lfUnderline = lf.lfUnderline;
                lfStrikeOut = lf.lfStrikeOut;
                lfCharSet = lf.lfCharSet;
                lfOutPrecision = lf.lfOutPrecision;
                lfClipPrecision = lf.lfClipPrecision;
                lfQuality = lf.lfQuality;
                lfPitchAndFamily = lf.lfPitchAndFamily;
                lfFaceName = lf.lfFaceName;
            }
        }

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern int GetGuiResources(IntPtr hProcess, int uiFlags);

        #endregion

        #region nt.dll
        [DllImport(Libraries.NtDll, SetLastError = true)]
        internal static extern int RtlGetVersion(ref PI.OSVERSIONINFOEX lpVersionInformation);
        #endregion

        #region Static Gdi32

        [DllImport(Libraries.Gdiplus, CharSet = CharSet.Unicode, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int GdipCreateSolidFill(int color, out IntPtr brush);

        [DllImport(Libraries.Gdiplus, EntryPoint = "GdipDeleteBrush", CharSet = CharSet.Unicode, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int GdipDeleteBrush(HandleRef brush);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport(Libraries.Gdi32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern StretchBltMode SetStretchBltMode(IntPtr hdc, StretchBltMode iStretchMode);

        [DllImport(Libraries.Gdi32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr CreateBitmap(int nWidth, int nHeight, uint cPlanes, uint cBitsPerPel, IntPtr lpvBits);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int ExcludeClipRect(IntPtr hDC, int x1, int y1, int x2, int y2);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int IntersectClipRect(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int GetDeviceCaps(IntPtr hDC, DeviceCap nIndex);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr CreateDIBSection(IntPtr hDC, [In] ref BITMAPINFO pBMI, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport(Libraries.Gdi32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        [DllImport(Libraries.Gdi32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint SetPixel(IntPtr hdc, int X, int Y, uint crColor);

        [DllImport(Libraries.Gdi32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int GetObject(IntPtr hgdiobj, int cbBuffer, ref BITMAP lpvObject);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr DeleteObject(IntPtr hObject);

        [DllImport(Libraries.Gdi32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr GetStockObject(StockObjects fnObject);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool DeleteDC(IntPtr hDC);

        [DllImport(Libraries.Gdi32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int SetDCPenColor(IntPtr hdc, int crColor);

        [DllImport(Libraries.Gdi32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int SetDCBrushColor(IntPtr hdc, int crColor);

        [DllImport(Libraries.Gdi32, EntryPoint = "SaveDC", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int IntSaveDC(HandleRef hDC);

        [DllImport(Libraries.Gdi32, EntryPoint = "RestoreDC", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool IntRestoreDC(HandleRef hDC, int nSavedDC);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool GetViewportOrgEx(HandleRef hDC, [In, Out] POINTC point);

        [DllImport(Libraries.Gdi32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool OffsetViewportOrgEx(IntPtr hdc, int nXOffset, int nYOffset, out POINT lpPoint);

        [DllImport(Libraries.Gdi32, EntryPoint = "CreateRectRgn", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr IntCreateRectRgn(int x1, int y1, int x2, int y2);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int GetClipRgn(HandleRef hDC, HandleRef hRgn);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool SetViewportOrgEx(HandleRef hDC, int x, int y, [In, Out] POINTC point);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int GetRgnBox(IntPtr hRegion, ref RECT clipRect);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int CombineRgn(HandleRef hRgn, HandleRef hRgn1, HandleRef hRgn2, int nCombineMode);

        [DllImport(Libraries.Gdi32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool RoundRect(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nEllipseWidth, int nEllipseHeight);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int SelectClipRgn(HandleRef hDC, HandleRef hRgn);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int SelectClipRgn(IntPtr hDC, IntPtr hRgn);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint SetTextColor(IntPtr hdc, int crColor);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint SetBkColor(IntPtr hdc, int crColor);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint SetBkMode(IntPtr hdc, int crColor);

        [DllImport(Libraries.Gdi32, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr CreateSolidBrush(int crColor);

        [DllImport(Libraries.Gdi32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool MoveToEx(IntPtr hdc, int X, int Y, IntPtr lpPoint);

        [DllImport(Libraries.Gdi32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);

        [DllImport(Libraries.Gdi32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool Rectangle(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport(Libraries.Gdi32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern IntPtr GetCurrentObject(IntPtr hdc, int objectType); // objectType 7 = OBJ_BITMAP

        [DllImport(Libraries.Gdi32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern bool GdiFlush();

        #endregion

        #region dwmapi

        public class Dwm
        {

            // Applicable to Vista -> Win 8
            // Warning API's appear deprecated on MSDN for Win 10

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa969508(v=vs.85).aspx

            [DllImport(Libraries.DWMApi)]
            [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
            internal static extern int DwmEnableBlurBehindWindow(IntPtr hwnd, ref DWM_BLURBEHIND blurBehind);

            // https://msdn.microsoft.com/it-it/library/windows/desktop/aa969512(v=vs.85).aspx
            [DllImport(Libraries.DWMApi)]
            [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
            internal static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa969515(v=vs.85).aspx
            [DllImport(Libraries.DWMApi)]
            [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
            internal static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attr, ref int attrValue, int attrSize);

            [DllImport(Libraries.DWMApi)]
            [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
            internal static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attr, out RECT pvAttribute, int attrSize);

            internal static Rectangle DwmGetWindowRect(IntPtr hwnd)
            {
                DwmGetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.ExtendedFrameBounds, out RECT rect, SizeOf(typeof(RECT)));
                return System.Drawing.Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
            }

            //https://msdn.microsoft.com/en-us/library/windows/desktop/aa969524(v=vs.85).aspx
            [DllImport(Libraries.DWMApi)]
            [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
            internal static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attr, ref int attrValue,
                int attrSize);

            [DllImport(Libraries.DWMApi)]
            [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
            internal static extern int DwmIsCompositionEnabled(ref int pfEnabled);

            [DllImport(Libraries.DWMApi, CharSet = CharSet.Auto)]
            [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
            internal static extern int DwmDefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, out IntPtr result);

            public enum DWMWINDOWATTRIBUTE : uint
            {
                NCRenderingEnabled = 1, // Get only attribute
                NCRenderingPolicy, // Enable or disable non-client rendering
                TransitionsForceDisabled,
                AllowNCPaint,
                CaptionButtonBounds,
                NonClientRtlLayout,
                ForceIconicRepresentation,
                Flip3DPolicy,
                ExtendedFrameBounds,
                HasIconicBitmap,
                DisallowPeek,
                ExcludedFromPeek,
                Cloak,
                Cloaked,
                FreezeRepresentation,
                PlaceHolder1,
                PlaceHolder2,
                PlaceHolder3,
                AccentPolicy = 19
            }

            public enum DWMNCRENDERINGPOLICY : uint
            {
                UseWindowStyle, // Enable/disable non-client rendering based on window style
                Disabled, // Disabled non-client rendering; window style is ignored
                Enabled // Enabled non-client rendering; window style is ignored
            }

            // Values designating how Flip3D treats a given window.
            private enum DWMFLIP3DWINDOWPOLICY : uint
            {
                Default,        // Hide or include the window in Flip3D based on window style and visibility.
                ExcludeBelow,   // Display the window under Flip3D and disabled.
                ExcludeAbove    // Display the window above Flip3D and enabled.
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct DWM_BLURBEHIND
            {
                public DWM_BB dwFlags;
                public int fEnable;
                public IntPtr hRgnBlur;
                public int fTransitionOnMaximized;

                public DWM_BLURBEHIND(bool enabled)
                {
                    dwFlags = DWM_BB.Enable;
                    fEnable = enabled ? 1 : 0;
                    hRgnBlur = IntPtr.Zero;
                    fTransitionOnMaximized = 0;
                }
            }

            [Flags]
            public enum DWM_BB
            {
                Enable = 1,
                BlurRegion = 2,
                TransitionOnMaximized = 4
            }

            internal enum WindowCompositionAttribute
            {
                WCA_UNDEFINED = 0x00,
                WCA_NCRENDERING_ENABLED = 0x01,
                WCA_NCRENDERING_POLICY = 0x02,
                WCA_TRANSITIONS_FORCEDISABLED = 0x03,
                WCA_ALLOW_NCPAINT = 0x04,
                WCA_CAPTION_BUTTON_BOUNDS = 0x05,
                WCA_NONCLIENT_RTL_LAYOUT = 0x06,
                WCA_FORCE_ICONIC_REPRESENTATION = 0x07,
                WCA_EXTENDED_FRAME_BOUNDS = 0x08,
                WCA_HAS_ICONIC_BITMAP = 0x09,
                WCA_THEME_ATTRIBUTES = 0x0A,
                WCA_NCRENDERING_EXILED = 0x0B,
                WCA_NCADORNMENTINFO = 0x0C,
                WCA_EXCLUDED_FROM_LIVEPREVIEW = 0x0D,
                WCA_VIDEO_OVERLAY_ACTIVE = 0x0E,
                WCA_FORCE_ACTIVEWINDOW_APPEARANCE = 0x0F,
                WCA_DISALLOW_PEEK = 0x10,
                WCA_CLOAK = 0x11,
                WCA_CLOAKED = 0x12,
                WCA_ACCENT_POLICY = 0x13,
                WCA_FREEZE_REPRESENTATION = 0x14,
                WCA_EVER_UNCLOAKED = 0x15,
                WCA_VISUAL_OWNER = 0x16,
                WCA_HOLOGRAPHIC = 0x17,
                WCA_EXCLUDED_FROM_DDA = 0x18,
                WCA_PASSIVEUPDATEMODE = 0x19,
                WCA_LAST = 0x1A
            }

            public enum DWMACCENTSTATE          // Affects the rendering of the background of a window.
            {
                ACCENT_DISABLED = 0,            // Default value. Background is black.
                ACCENT_ENABLE_GRADIENT = 1,     // Background is GradientColor, alpha channel ignored.
                ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,// Background is GradientColor.
                ACCENT_ENABLE_BLURBEHIND = 3,       // Background is GradientColor, with blur effect.
                ACCENT_ENABLE_ACRYLICBLURBEHIND = 4, // Applicable on Win 10 18H1 -> Background is GradientColor, with acrylic blur effect.
                ACCENT_ENABLE_HOSTBACKDROP = 5,      // RS5 18H5 -> Seems to draw background fully transparent.
                ACCENT_INVALID_STATE = 6        // Unknown. Seems to draw background fully transparent.
            }

            [Flags]
            internal enum AccentFlags
            {
                UserGradientColour = 2,
                // ...
                DrawLeftBorder = 0x20,
                DrawTopBorder = 0x40,
                DrawRightBorder = 0x80,
                DrawBottomBorder = 0x100,
                DrawAllBorders = DrawLeftBorder | DrawTopBorder | DrawRightBorder | DrawBottomBorder
                // ...
            }
            [StructLayout(LayoutKind.Sequential)]
            public struct AccentPolicy
            {
                public DWMACCENTSTATE AccentState;
                public AccentFlags AccentFlags;
                public int GradientColor;
                public int AnimationId;

                public AccentPolicy(DWMACCENTSTATE accentState, AccentFlags accentFlags, int gradientColor, int animationId)
                {
                    AccentState = accentState;
                    AccentFlags = accentFlags;
                    GradientColor = gradientColor;
                    AnimationId = animationId;
                }
            }

            public static bool IsCompositionEnabled()
            {
                var pfEnabled = 0;
                var result = DwmIsCompositionEnabled(ref pfEnabled);
                return pfEnabled == 1;
            }

            public static bool IsNonClientRenderingEnabled(IntPtr hWnd)
            {
                var gwaEnabled = 0;
                var result = DwmGetWindowAttribute(hWnd, DWMWINDOWATTRIBUTE.NCRenderingEnabled, ref gwaEnabled, sizeof(int));
                return gwaEnabled == 1;
            }

            public static bool WindowSetAttribute(IntPtr hWnd, DWMWINDOWATTRIBUTE Attribute, int AttributeValue)
            {
                var result = DwmSetWindowAttribute(hWnd, Attribute, ref AttributeValue, sizeof(int));
                return result == 0;
            }

            public static void Windows10EnableBlurBehind(IntPtr hWnd, bool enabled)
            {
                var accPolicy = new AccentPolicy(enabled ? DWMACCENTSTATE.ACCENT_ENABLE_BLURBEHIND : DWMACCENTSTATE.ACCENT_DISABLED,
                    AccentFlags.UserGradientColour,
                    Color.FromArgb(200, Color.White).ToArgb(),
                    0);
                //{
                //    AccentState = DWMACCENTSTATE.ACCENT_ENABLE_BLURBEHIND,
                //};

                IntPtr accentPtr = IntPtr.Zero;
                try
                {
                    var accentSize = SizeOf(accPolicy);
                    accentPtr = AllocHGlobal(accentSize);
                    Marshal.StructureToPtr(accPolicy, accentPtr, false);
                    var data = new WindowCompositionAttribData(WindowCompositionAttribute.WCA_ACCENT_POLICY,
                        accentPtr, accentSize);

                    SetWindowCompositionAttribute(hWnd, ref data);
                }
                finally
                {
                    if (accentPtr != IntPtr.Zero)
                    {
                        FreeHGlobal(accentPtr);
                    }
                }
            }

            public static bool WindowEnableBlurBehind(IntPtr hWnd, bool enabled)
            {
                //Create and populate the Blur Behind structure
                var Dwm_BB = new DWM_BLURBEHIND(enabled);

                var result = DwmEnableBlurBehindWindow(hWnd, ref Dwm_BB);
                return result == 0;
            }

            public static bool WindowExtendIntoClientArea(IntPtr hWnd, MARGINS Margins)
            {
                // Extend frame on the bottom of client area
                var result = DwmExtendFrameIntoClientArea(hWnd, ref Margins);
                return result == 0;
            }

            public static bool WindowBorderlessDropShadow(IntPtr hWnd, int ShadowSize)
            {
                var Margins = new MARGINS(0, ShadowSize, 0, ShadowSize);
                var result = DwmExtendFrameIntoClientArea(hWnd, ref Margins);
                return result == 0;
            }

            public static bool WindowSheetOfGlass(IntPtr hWnd)
            {
                var Margins = new MARGINS();
                Margins.SheetOfGlass();

                //Margins set to All:-1 - Sheet Of Glass effect
                var result = DwmExtendFrameIntoClientArea(hWnd, ref Margins);
                return result == 0;
            }

            public static bool WindowDisableRendering(IntPtr hWnd)
            {
                var NCRP = DWMNCRENDERINGPOLICY.Disabled;
                var ncrp = (int)NCRP;
                // Disable non-client area rendering on the window.
                var result = DwmSetWindowAttribute(hWnd, DWMWINDOWATTRIBUTE.NCRenderingPolicy, ref ncrp,
                    sizeof(int));
                return result == 0;
            }
        }
        #endregion dwmapi

        #region GDIPlus
        // C# GDI Plus 1.1 provides access to the GDI+ 1.1 functions which are available from Vista onwards
        // Warning: the entire API is deprecated and is listed under Legacy APIs in MSDN.

        /// <summary>
        /// Contains members that specify the nature of a Gaussian blur.
        /// </summary>
        /// <remarks>Cannot be pinned with GCHandle due to bool value.</remarks>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct BlurParams
        {
            /// <summary>
            /// Real number that specifies the blur radius (the radius of the Gaussian convolution kernel) in
            /// pixels. The radius must be in the range 0 through 255. As the radius increases, the resulting
            /// bitmap becomes more blurry.
            /// </summary>
            public float Radius;

            /// <summary>
            /// Boolean value that specifies whether the bitmap expands by an amount equal to the blur radius.
            /// If TRUE, the bitmap expands by an amount equal to the radius so that it can have soft edges.
            /// If FALSE, the bitmap remains the same size and the soft edges are clipped.
            /// </summary>
            public bool ExpandEdges;
        }

        // Constant "FieldInfo" for getting the nativeImage from the Bitmap
        private static readonly FieldInfo FIELD_INFO_NATIVE_IMAGE = typeof(Bitmap).GetField(@"nativeImage", BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic)!;
        /// <summary>
        /// Get the nativeImage field from the bitmap
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns>IntPtr</returns>
        internal static IntPtr GetNativeImage(Bitmap bitmap) => IntPtr.Zero;

        internal static Guid BlurEffectGuid = new Guid("{633C80A4-1843-482B-9EF2-BE2834C5FDD4}");

        [DllImport(Libraries.Gdiplus, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int GdipCreateEffect(Guid guid, out IntPtr effect);

        [DllImport(Libraries.Gdiplus, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int GdipSetEffectParameters(IntPtr effect, IntPtr parameters, uint size);

        [DllImport(Libraries.Gdiplus, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int GdipBitmapApplyEffect(IntPtr bitmap, IntPtr effect, ref RECT rectOfInterest, bool useAuxData, IntPtr auxData, int auxDataSize);

        [DllImport(Libraries.Gdiplus, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int GdipDeleteEffect(IntPtr effect);

        #endregion GDIPlus

        #region Static Ole32
        [DllImport(Libraries.Ole32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern void CoCreateGuid(ref GUIDSTRUCT guid);
        #endregion

        #region Static Uxtheme

        [DllImport(Libraries.UxTheme, CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hDC, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref RECT pRect, ref DTTOPTS pOptions);

        [DllImport(Libraries.UxTheme, EntryPoint = "#94", CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int GetImmersiveColorSetCount();

        [DllImport(Libraries.UxTheme, EntryPoint = "#95", CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint GetImmersiveColorFromColorSetEx(uint dwImmersiveColorSet, uint dwImmersiveColorType, bool bIgnoreHighContrast, uint dwHighContrastCacheMode);

        [DllImport(Libraries.UxTheme, EntryPoint = "#96", CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint GetImmersiveColorTypeFromName(string name);

        [DllImport(Libraries.UxTheme, EntryPoint = "#98", CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint GetImmersiveUserColorSetPreference(bool bForceCheckRegistry, bool bSkipCheckOnFail);

        [DllImport(Libraries.UxTheme, EntryPoint = "#100", CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr GetImmersiveColorNamedTypeByIndex(uint dwIndex);

        [DllImport(Libraries.UxTheme, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool IsAppThemed();

        [DllImport(Libraries.UxTheme, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool IsThemeActive();

        [DllImport(Libraries.UxTheme, CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int SetWindowTheme(IntPtr hWnd, string? subAppName, string? subIdList);
        #endregion

        #region Static Kernel32
        [Flags]
        public enum FORMAT_MESSAGE_ : uint
        {
            ALLOCATE_BUFFER = 0x00000100,
            IGNORE_INSERTS = 0x00000200,
            FROM_STRING = 0x00000400,
            FROM_HMODULE = 0x00000800,
            FROM_SYSTEM = 0x00001000,
            ARGUMENT_ARRAY = 0x00002000
        }

        [Flags]
        public enum GMEM : uint
        {
            FIXED = 0x0000,
            MOVEABLE = 0x0002,
            NOCOMPACT = 0x0010,
            NODISCARD = 0x0020,
            ZEROINIT = 0x0040,
            MODIFY = 0x0080,
            DISCARDABLE = 0x0100,
            NOT_BANKED = 0x1000,
            SHARE = 0x2000,
            DDESHARE = 0x2000,
            NOTIFY = 0x4000,
            LOWER = NOT_BANKED,
            DISCARDED = 0x4000,
            LOCKCOUNT = 0x00ff,
            INVALID_HANDLE = 0x8000,

            GHND = MOVEABLE | ZEROINIT,
            GPTR = FIXED | ZEROINIT
        }

        [DllImport(Libraries.Kernel32, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FreeLibrary([In] IntPtr hModule);

        /// <summary>
        /// Return the length of the string
        /// </summary>
        [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern uint FormatMessage(FORMAT_MESSAGE_ dwFlags, IntPtr lpSource,
            uint dwMessageId, uint dwLanguageId, ref IntPtr lpBuffer,
            uint nSize, string[] Arguments);

        [DllImport(Libraries.Kernel32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr GlobalAlloc(GMEM uFlags, int dwBytes);

        [DllImport(Libraries.Kernel32, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr GlobalFree(IntPtr hMem);

        [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, BestFitMapping = false, ThrowOnUnmappableChar = true, SetLastError = true)]
        //        [DllImport(ExternDll.Kernel32, CharSet=CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.Process)]
        internal static extern IntPtr GetModuleHandle(string? moduleName);

        [DllImport(Libraries.Kernel32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int GetCurrentThreadId();

        [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern SafeModuleHandle
            LoadLibraryEx([In] string lpFileName, IntPtr hFile/*=null*/, [In] LoadLibraryExFlags dwFlags);

        [Flags]
        internal enum LoadLibraryExFlags : uint
        {
            // https://learn.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-loadlibraryexa
            DontResolveDllReferences = 0x00000001,
            LoadLibraryAsDatafile = 0x00000002,
            LoadWithAlteredSearchPath = 0x00000008,
            LoadIgnoreCodeAuthzLevel = 0x00000010,
            LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
            LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
            LOAD_LIBRARY_REQUIRE_SIGNED_TARGET = 0x00000080,
            LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
            LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
            LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
            LoadLibrarySearchSystem32 = 0x00000800, // Introduced in KB2533623; with Windows 7 SP1.
            LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
            LOAD_LIBRARY_SAFE_CURRENT_DIRS = 0x00002000
        }

        [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int MultiByteToWideChar(int codePage, int dwFlags, byte[] lpMultiByteStr,
            int cchMultiByte, char[] lpWideCharStr, int cchWideChar);

        [DllImport(Libraries.Kernel32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern short QueryPerformanceCounter(ref long var);

        [DllImport(Libraries.Kernel32, CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern short QueryPerformanceFrequency(ref long var);
        #endregion

        #region Static Comdlg32
        public enum FNERR : uint
        {
            SUBCLASSFAILURE = 0x3001,
            INVALIDFILENAME = 0x3002,
            BUFFERTOOSMALL = 0x3003
        }

        public struct OFNOTIFYW
        {
            public NMHDR hdr;
            public IntPtr lpOFN;
            public IntPtr pszFile;
        }

        #region DialogChangeStatus
        public enum CDN_ : uint
        {
            FIRST = 0xFFFFFDA7, // -601
            INITDONE = (FIRST - 0x0000),
            SELCHANGE = (FIRST - 0x0001), // -602
            FOLDERCHANGE = (FIRST - 0x0002),
            SHAREVIOLATION = (FIRST - 0x0003), // -604
            HELP = (FIRST - 0x0004),
            FILEOK = (FIRST - 0x0005), // -606
            TYPECHANGE = (FIRST - 0x0006)
        }
        #endregion

        [DllImport(Libraries.Comdlg32, ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern FNERR CommDlgExtendedError();

        [DllImport(Libraries.Comdlg32, CharSet = CharSet.Auto, SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern bool ChooseFont([In, Out] CHOOSEFONT cf);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal class CHOOSEFONT
        {
            public int lStructSize = SizeOf(typeof(CHOOSEFONT));
            public IntPtr hwndOwner;
            public IntPtr hDC;
            public IntPtr lpLogFont;
            public int iPointSize;
            public int Flags;
            public int rgbColors;
            public IntPtr lCustData = IntPtr.Zero;
            internal WndProc lpfnHook;
            public string lpTemplateName;
            public IntPtr hInstance;
            public string lpszStyle;
            public short nFontType;
            public short ___MISSING_ALIGNMENT__;
            public int nSizeMin;
            public int nSizeMax;
        }

        #endregion Static Comdlg32

        #region Structures
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct LVFINDINFO
        {
            public int flags;
            public string psz;
            public IntPtr lParam;
            public int ptX; // was POINT pt
            public int ptY;
            public int vkDirection;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMLVFINDITEM
        {
            public NMHDR hdr;
            public int iStart;
            public LVFINDINFO lvfi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMHDR
        {
            public IntPtr hwndFrom;
            public IntPtr idFrom; //This is declared as UINT_PTR in winuser.h
            public uint code;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class NMHEADER
        {
            public NMHDR nmhdr;
            public int iItem = 0;
            public int iButton = 0;
            public IntPtr pItem = IntPtr.Zero;    // HDITEM*
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct TV_ITEM
        {
            public TVIF_ Mask;
            public IntPtr ItemHandle;
            public TVIS_ State;
            public TVIS_ StateMask;
            public IntPtr TextPtr;
            public int TextMax;
            public int Image;
            public int SelectedImage;
            public int Children;
            public IntPtr LParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct SIZE
        {
            public int cx;
            public int cy;

            public SIZE(int width, int height)
                : this()
            {
                cx = width;
                cy = height;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
                : this()
            {
                X = x;
                Y = y;
            }
            public static implicit operator Point(POINT p) => new Point(p.X, p.Y);

            public static implicit operator POINT(Point p) => new POINT(p.X, p.Y);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class POINTC
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ComboBoxInfo
        {
            internal int cbSize;
            internal RECT rcItem;
            internal RECT rcButton;
            internal IntPtr stateButton;
            internal IntPtr hwndCombo;
            internal IntPtr hwndEdit;
            internal IntPtr hwndList;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;

            public MARGINS(int LeftWidth, int RightWidth, int TopHeight, int BottomHeight)
            {
                leftWidth = LeftWidth;
                rightWidth = RightWidth;
                topHeight = TopHeight;
                bottomHeight = BottomHeight;
            }

            public void NoMargins()
            {
                leftWidth = 0;
                rightWidth = 0;
                topHeight = 0;
                bottomHeight = 0;
            }

            public void SheetOfGlass()
            {
                leftWidth = -1;
                rightWidth = -1;
                topHeight = -1;
                bottomHeight = -1;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct TRACKMOUSEEVENTS
        {
            public uint cbSize;
            public uint dwFlags;
            public IntPtr hWnd;
            public uint dwHoverTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct NCCALCSIZE_PARAMS
        {
            public RECT rectProposed;
            public RECT rectBeforeMove;
            public RECT rectClientBeforeMove;
            public int lpPos;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public SWP_ flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct GUIDSTRUCT
        {
            public ushort Data1;
            public ushort Data2;
            public ushort Data3;
            public ushort Data4;
            public ushort Data5;
            public ushort Data6;
            public ushort Data7;
            public ushort Data8;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MSG
        {
            public IntPtr hwnd;
            public int message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public POINT pt;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct DTTOPTS
        {
            public int dwSize;
            public int dwFlags;
            public int crText;
            public int crBorder;
            public int crShadow;
            public int iTextShadowType;
            public POINT ptShadowOffset;
            public int iBorderSize;
            public int iFontPropId;
            public int iColorPropId;
            public int iStateId;
            public bool fApplyOverlay;
            public int iGlowSize;
            public int pfnDrawTextCallback;
            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct BITMAPINFO
        {
            public uint biSize;
            public int biWidth;
            public int biHeight;
            public ushort biPlanes;
            public ushort biBitCount;
            public uint biCompression;
            public uint biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public uint biClrUsed;
            public uint biClrImportant;
            public byte bmiColors_rgbBlue;
            public byte bmiColors_rgbGreen;
            public byte bmiColors_rgbRed;
            public byte bmiColors_rgbReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PAINTSTRUCT
        {
            private readonly IntPtr hdc;
            public bool fErase;
            public RECT rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CHARRANGE
        {
            public int cpMin;
            public int cpMax;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct FORMATRANGE
        {
            public IntPtr hdc;
            public IntPtr hdcTarget;
            public RECT rc;
            public RECT rcPage;
            public CHARRANGE chrg;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal class MONITORINFO
        {
            /// <summary>
            /// </summary>
            public int cbSize = SizeOf(typeof(MONITORINFO));

            /// <summary>
            /// </summary>
            public RECT rcMonitor = new RECT();

            /// <summary>
            /// </summary>
            public RECT rcWork = new RECT();

            /// <summary>
            /// </summary>
            public int dwFlags = 0;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct ICONINFO
        {
            public bool fIcon;         // Specifies whether this structure defines an icon or a cursor. A value of TRUE specifies
            public int xHotspot;     // Specifies the x-coordinate of a cursor's hot spot. If this structure defines an icon, the hot
            public int yHotspot;     // Specifies the y-coordinate of the cursor's hot spot. If this structure defines an icon, the hot
            public IntPtr hbmMask;     // (HBITMAP) Specifies the icon bitmask bitmap. If this structure defines a black and white icon,
            public IntPtr hbmColor;    // (HBITMAP) Handle to the icon color bitmap. This member can be optional if this
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CURSORINFO
        {
            public int cbSize;        // Specifies the size, in bytes, of the structure.
            public int flags;         // Specifies the cursor state. This parameter can be one of the following values:
                                      //    0                 The cursor is hidden.
                                      //    CURSOR_SHOWING    The cursor is showing.
            public IntPtr hCursor;      // Handle to the cursor.
            public POINT ptScreenPos;   // A POINT structure that receives the screen coordinates of the cursor.
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BLENDFUNCTION
        {
            //
            // currently defined blend operation
            //
            public static byte AC_SRC_OVER = 0x00;

            //
            // currently defined alpha format
            //
            public static byte AC_SRC_ALPHA = 0x01;

            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;

            public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
            {
                BlendOp = op;
                BlendFlags = flags;
                SourceConstantAlpha = alpha;
                AlphaFormat = format;
            }
        }

        public struct BITMAP
        {
            public int bmType;
            public int bmWidth;
            public int bmHeight;
            public int bmWidthBytes;
            public ushort bmPlanes;
            public ushort bmBitsPixel;
            public IntPtr bmBits;
        }

        [Flags]
        internal enum STATE_SYSTEM
        {
            UNAVAILABLE = 0x00000001, // Disabled
            SELECTED = 0x00000002,
            FOCUSED = 0x00000004,
            PRESSED = 0x00000008,
            CHECKED = 0x00000010,
            MIXED = 0x00000020,  // 3-state checkbox or toolbar button
            INDETERMINATE = MIXED,
            READONLY = 0x00000040,
            HOTTRACKED = 0x00000080,
            DEFAULT = 0x00000100,
            EXPANDED = 0x00000200,
            COLLAPSED = 0x00000400,
            BUSY = 0x00000800,
            FLOATING = 0x00001000,  // Children "owned" not "contained" by parent
            MARQUEED = 0x00002000,
            ANIMATED = 0x00004000,
            INVISIBLE = 0x00008000,
            OFFSCREEN = 0x00010000,
            SIZEABLE = 0x00020000,
            MOVEABLE = 0x00040000,
            SELFVOICING = 0x00080000,
            FOCUSABLE = 0x00100000,
            SELECTABLE = 0x00200000,
            LINKED = 0x00400000,
            TRAVERSED = 0x00800000,
            MULTISELECTABLE = 0x01000000,  // Supports multiple selection
            EXTSELECTABLE = 0x02000000,  // Supports extended selection
            ALERT_LOW = 0x04000000,  // This information is of low priority
            ALERT_MEDIUM = 0x08000000,  // This information is of medium priority
            ALERT_HIGH = 0x10000000,  // This information is of high priority
            PROTECTED = 0x20000000,  // access to this is restricted
            VALID = 0x3FFFFFFF
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct TITLEBARINFO
        {
            public uint cbSize;
            public RECT rcTitleBar;
            public STATE_SYSTEM rgstate_TitleBar;
            public STATE_SYSTEM rgstate_Reserved;
            public STATE_SYSTEM rgstate_MinimizeButton;
            public STATE_SYSTEM rgstate_MaximizeButton;
            public STATE_SYSTEM rgstate_HelpButton;
            public STATE_SYSTEM rgstate_CloseButton;
        }

        // New to Vista.
        [StructLayout(LayoutKind.Sequential)]
        internal struct TITLEBARINFOEX
        {
            public uint cbSize;
            public RECT rcTitleBar;
            public STATE_SYSTEM rgstate_TitleBar;
            public STATE_SYSTEM rgstate_Reserved;
            public STATE_SYSTEM rgstate_MinimizeButton;
            public STATE_SYSTEM rgstate_MaximizeButton;
            public STATE_SYSTEM rgstate_HelpButton;
            public STATE_SYSTEM rgstate_CloseButton;
            public RECT rcReserved1;
            public RECT rcReserved2;
            public RECT rcMinimizeButton;
            public RECT rcMaximizeButton;
            public RECT rcHelpButton;
            public RECT rcCloseButton;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct HELPINFO
        {
            public int cbSize;
            public int iContextType;
            public int iCtrlId;
            public IntPtr hItemHandle;
            public IntPtr dwContextId;
            public WINDOWLOCATION MousePos;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWLOCATION
        {
            public int X;
            public int Y;
        }

        /// <summary>
        /// Contains operating system version information. The information includes major and minor version numbers, a build number, a platform identifier,
        /// and information about product suites and the latest Service Pack installed on the system.
        /// This structure is used with the RtlGetVersion, GetVersionEx and VerifyVersionInfo functions.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct OSVERSIONINFOEX
        {
            public uint dwOSVersionInfoSize;
            public uint dwMajorVersion;
            public uint dwMinorVersion;
            public uint dwBuildNumber;
            public uint dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public ushort wServicePackMajor;
            public ushort wServicePackMinor;
            public ushort wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }

        #region For Acrylic

        /*
        // The following code comes from https://stackoverflow.com/questions/56481230/how-to-create-windows-10-acrylic-transparency-effect-in-winform-c-sharp
        // Discovered via:
        // https://withinrafael.com/2015/07/08/adding-the-aero-glass-blur-to-your-windows-10-apps/
        // https://github.com/riverar/sample-win32-acrylicblur/blob/917adc277c7258307799327d12262ebd47fd0308/MainWindow.xaml.cs

        [DllImport(Libraries.User32)]
        public static extern int SetWindowCompositionAttribute(HandleRef hWnd, IntPtr WindowCompositionAttributeData data);

        public IntPtr struct WindowCompositionAttributeData
        {
            public WCA Attribute;
            public void* Data;
            public int DataLength;
        }

        public enum WCA
        {
            ACCENT_POLICY = 19
        }

        public enum ACCENT
        {
            DISABLED = 0,
            ENABLE_GRADIENT = 1,
            ENABLE_TRANSPARENTGRADIENT = 2,
            ENABLE_BLURBEHIND = 3,
            ENABLE_ACRYLICBLURBEHIND = 4,
            INVALID_STATE = 5
        }

        public struct AccentPolicy
        {
            public ACCENT AccentState;
            public uint AccentFlags;
            public uint GradientColor;
            public uint AnimationId;
        }*/

        #endregion

        #endregion
    }

    internal static class BoolExtensions
    {
        public static bool IsTrue(this PI.BOOL b) => b != PI.BOOL.FALSE;
        public static bool IsFalse(this PI.BOOL b) => b == PI.BOOL.FALSE;
        public static PI.BOOL ToBOOL(this bool b) => b ? PI.BOOL.TRUE : PI.BOOL.FALSE;
    }