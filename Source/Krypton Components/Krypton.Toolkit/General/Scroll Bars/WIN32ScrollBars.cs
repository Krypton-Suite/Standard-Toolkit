#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit
{
    public class WIN32ScrollBars
    {
        #region "   Members   "

        // offset of window style value
        public const int GWL_STYLE = -16;

        // window style constants for scrollbars
        public const int WM_HSCROLL = 0x114;
        public const int WM_VSCROLL = 0x115;
        public const int WM_NCCALCSIZE = 0x0083;
        public const int WM_SIZE = 0x5;

        public const int LB_SETHORIZONTALEXTENT = 0x194;

        public const Int32 LVM_FIRST = 0x1000;
        public const Int32 LVM_SCROLL = LVM_FIRST + 20;

        public const int WS_VSCROLL = 0x200000;
        public const int WS_HSCROLL = 0x100000;
        public const long WS_CHILDWINDOW = 0x40000000L;
        public const int WM_ERASEBKGND = 0x0014;


        public const int SB_LINEUP = 0;
        public const int SB_LINELEFT = 0;
        public const int SB_LINEDOWN = 1;
        public const int SB_LINERIGHT = 1;
        public const int SB_PAGEUP = 2;
        public const int SB_PAGELEFT = 2;
        public const int SB_PAGEDOWN = 3;
        public const int SB_PAGERIGHT = 3;
        public const int SB_THUMBPOSITION = 4;
        public const int SB_THUMBTRACK = 5;
        public const int SB_TOP = 6;
        public const int SB_LEFT = 6;
        public const int SB_BOTTOM = 7;
        public const int SB_RIGHT = 7;
        public const int SB_ENDSCROLL = 8;
        public const int SB_HORZ = 0;
        public const int SB_VERT = 1;
        public const int SB_CTL = 2;
        public const int SB_BOTH = 3;

        public const int SBM_ENABLE_ARROWS = 0x00E4; /*not in win3.1 */
        public const int SBM_SETSCROLLINFO = 0x00E9;
        public const int SBM_GETSCROLLINFO = 0x00EA;

        public const uint WM_COMMAND = 0x0111;
        public const uint WM_USER = 0x0400;
        public const uint WM_REFLECT = WM_USER + 0x1C00;
        public const uint WM_LBUTTONDOWN = 0x0201;
        public const uint WM_LBUTTONUP = 0x0202;


        public const uint CBN_DROPDOWN = 7;
        public const uint CBN_CLOSEUP = 8;

        public const int COLOR_SCROLLBAR = 0; //Scroll-bar gray area
        public const int COLOR_BACKGROUND = 1; //Desktop
        public const int COLOR_ACTIVECAPTION = 2; //Active window caption
        public const int COLOR_INACTIVECAPTION = 3; //Inactive window caption
        public const int COLOR_MENU = 4; //Menu background
        public const int COLOR_WINDOW = 5; //Window background
        public const int COLOR_WINDOWFRAME = 6; //Window frame
        public const int COLOR_MENUTEXT = 7; //Text in menus
        public const int COLOR_WINDOWTEXT = 8; //Text in windows
        public const int COLOR_CAPTIONTEXT = 9; //Text in caption, size box,scroll bar arrow box
        public const int COLOR_ACTIVEBORDER = 10; //Active window border
        public const int COLOR_INACTIVEBORDER = 11; //Inactive window border
        public const int COLOR_APPWORKSPACE = 12; //Background color of multipledocument interface (MDI)applications
        public const int COLOR_HIGHLIGHT = 13; //Items selected item in a control
        public const int COLOR_HIGHLIGHTTEXT = 14; //Text of item selected in a control
        public const int COLOR_BTNFACE = 15; //Face shading on push button
        public const int COLOR_BTNSHADOW = 16; //Edge shading on push button
        public const int COLOR_GRAYTEXT = 17; //Grayed (disabled) text. This color is set to 0 if the current display driver does not support a solid gray color.
        public const int COLOR_BTNTEXT = 18; //Text on push buttons

        #endregion

        #region "   PInvoke   "
        [DllImport("user32", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32", SetLastError = true)]
        public static extern bool GetScrollInfo(IntPtr hWnd, int nBar, ref ScrollInfo lpScrollInfo);
        [DllImport("user32", SetLastError = true)]
        public static extern bool SetScrollInfo(int hwnd, int nBar, ref ScrollInfo lpcScrollInfo, bool redraw);
        [DllImport("user32")]
        public static extern int SetScrollInfo(IntPtr hwnd, int nBar, ref ScrollInfo lpcScrollInfo, bool redraw);

        [DllImport("user32")]
        public static extern int GetScrollPos(IntPtr hWnd, int nBar);
        [DllImport("user32")]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);
        [DllImport("user32")]
        public static extern bool GetScrollRange(IntPtr Handle, Int32 nBar, ref IntPtr min, ref IntPtr max);

        [DllImport("user32")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32")]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32")]
        public static extern bool ShowScrollBar(IntPtr hWnd, int nBar, bool bShow);
        [DllImport("user32")]
        public static extern bool EnableScrollBar(IntPtr hWnd, int nBar, int wArrows);

        [DllImport("user32")]
        public static extern bool PostMessageA(IntPtr hWnd, int wMsg, int wParam, int lParam);
        [DllImport("user32")]
        public static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        [DllImport("user32")]
        public static extern IntPtr SendMessage(IntPtr Handle, Int32 msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32", EntryPoint = "SendMessageW", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessageCb(IntPtr hWnd, int msg, IntPtr wp, out WIN32ScrollBars.ComboBoxInfo lp);
        [DllImport("user32", EntryPoint = "SendMessageW", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessageRc(IntPtr hWnd, int msg, IntPtr wp, out RECT lp);

        [DllImport("user32")]
        public static extern bool GetComboBoxInfo(IntPtr hwndCombo, ref ComboBoxInfo info);
        [DllImport("user32")]
        public static extern int GetClientRect(IntPtr hwnd, ref RECT lpRect);
        [DllImport("user32.")]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32")]
        public static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] ref RECT rc, int points);

        [DllImport("user32.dll")]
        public static extern uint GetSysColor(int nIndex);
        [DllImport("user32.dll")]
        public static extern bool SetSysColors(int cElements, int[] lpaElements, int[] lpaRgbValues);

        #endregion

        #region "   ScrollInfoStruct & Enum   "

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct ComboBoxInfo
        {
            public int cbSize;
            public RECT rcItem;
            public RECT rcButton;
            public IntPtr stateButton;
            public IntPtr hwndCombo;
            public IntPtr hwndEdit;
            public IntPtr hwndList;
        }
        [StructLayout(LayoutKind.Sequential)]

        public struct ScrollInfo
        {
            public int cbSize;
            public int fMask;
            public int nMin;
            public int nMax;
            public int nPage;
            public int nPos;
            public int nTrackPos;
        }

        public enum ScrollInfoMask
        {
            SIF_RANGE = 0x1,
            SIF_PAGE = 0x2,
            SIF_POS = 0x4,
            SIF_DISABLENOSCROLL = 0x8,
            SIF_TRACKPOS = 0x16,
            SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS)
        }

        public enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        public enum ScrollBarType
        {
            Vertical = 0,
            Horizontal = 1
        }

        public enum ScrollBarArrows
        {
            ESB_ENABLE_BOTH = 0,
            ESB_DISABLE_BOTH = 3,
            ESB_DISABLE_LEFT = 1,
            ESB_DISABLE_RIGHT = 2,
            ESB_DISABLE_UP = 1,
            ESB_DISABLE_DOWN = 2,
            ESB_DISABLE_LTUP = 1,
            ESB_DISABLE_RTDN = 2
        }
        #endregion

        #region "   Functions   "
        public static uint LoWord(IntPtr ptr)
        {
            return ((uint)ptr >> 16);
        }
        public static uint HiWord(IntPtr ptr)
        {
            return ((uint)ptr >> 16) & 0xffff;
        }
        public static uint HiWord(int n)
        {
            return (uint)(n >> 16) & 0xffff;
        }

        public static int MakeLParam(int LoWord, int HiWord)
        {
            return ((HiWord << 16) | (LoWord & 0xffff));
        }
        #endregion

        #region InitComboBoxInfo Method - Private
        // TODO: Clean this up
        /*
        public static bool InitComboBoxInfo(ref KryptonComboBoxEnhanced cbo, ref WIN32ScrollBars.ComboBoxInfo cbi)
        {

            cbi.cbSize = Marshal.SizeOf(cbi);
            if (!WIN32ScrollBars.GetComboBoxInfo(cbo.Handle, ref cbi))
            {
                return false;
            }
            return true;
        }
        */

        public static void SetSystemColor(Color color)
        {
            //array of elements to change
            int[] elements = { WIN32ScrollBars.COLOR_SCROLLBAR };

            //array of corresponding colors
            int[] colors = { System.Drawing.ColorTranslator.ToWin32(color) };

            //set the desktop color using p/invoke
            WIN32ScrollBars.SetSysColors(elements.Length, elements, colors);
        }

        #endregion
    }
}
