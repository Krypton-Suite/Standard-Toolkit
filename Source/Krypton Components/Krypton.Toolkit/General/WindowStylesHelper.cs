#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *  
 */
#endregion


namespace Krypton.Toolkit
{
    internal class WindowStylesHelper
    {
        public WindowStylesHelper(IntPtr handle)
        {
            WinStyle = PI.GetWindowLong(handle, PI.GWL_.STYLE);
            WinExStyle = PI.GetWindowLong(handle, PI.GWL_.EXSTYLE);
        }

        public WindowStylesHelper(IntPtr style, IntPtr exStyle)
        {
            WinStyle = (uint) style.ToInt32();
            WinExStyle = (uint) exStyle.ToInt32();
        }

        public override string ToString()
        {
            StringBuilder str1 = new();
            if (Caption)
            {
                str1.Append("Caption+");
            }

            if (Framed)
            {
                str1.Append("Frame+");
            }

            str1.Append("Styles: ");
            if ((WinStyle & 8388608) != 0)
            {
                str1.Append("WS_BORDER");
            }

            if ((WinStyle & 4194304) != 0)
            {
                str1.Append("+WS_DLGFRAME ");
            }

            if ((WinStyle & 262144) != 0)
            {
                str1.Append("+WS_THICKFRAME ");
            }

            if ((WinStyle & 134217728) != 0)
            {
                str1.Append("+WS_DISABLED ");
            }

            if ((WinStyle & 1048576) != 0)
            {
                str1.Append("+WS_HSCROLL ");
            }

            if ((WinStyle & 2097152) != 0)
            {
                str1.Append("+WS_VSCROLL ");
            }

            if ((WinStyle & 131072) != 0)
            {
                str1.Append("+WS_MINIMIZEBOX ");
            }

            if ((WinStyle & 65536) != 0)
            {
                str1.Append("+WS_MAXIMIZEBOX ");
            }

            if ((WinStyle & 2147483648L) != 0L)
            {
                str1.Append("+WS_POPUP ");
            }

            if ((WinStyle & 524288) != 0)
            {
                str1.Append("+WS_SYSMENU ");
            }

            if ((WinStyle & 536870912) != 0)
            {
                str1.Append("+WS_MINIMIZE ");
            }

            if ((WinStyle & 16777216) != 0)
            {
                str1.Append("+WS_MAXIMIZE ");
            }

            if ((WinExStyle & 1) != 0)
            {
                str1.Append("+WS_EX_DLGMODALFRAME ");
            }

            if ((WinExStyle & 512) != 0)
            {
                str1.Append("+WS_EX_CLIENTEDGE ");
            }

            if ((WinExStyle & 128) != 0)
            {
                str1.Append("+WS_EX_TOOLWINDOW ");
            }

            return str1.ToString();
        }

        public uint WinStyle { get; set; }

        public uint WinExStyle { get; set; }

        public bool IsEmpty => WinStyle == 0 ? WinExStyle == 0 : false;

        public bool Border
        {
            get => (WinStyle & 8388608) != 0;
            set => WinStyle |= 8388608;
        }

        public bool Caption
        {
            get => (WinStyle & 12582912) == 12582912;
            set => WinStyle |= 12582912;
        }

        public bool Child => (WinStyle & 1073741824) != 0;

        public bool ClipChildren
        {
            get => (WinStyle & 33554432) != 0;
            set => WinStyle |= 33554432;
        }

        public bool ClipSiblings
        {
            get => (WinStyle & 67108864) != 0;
            set => WinStyle |= 67108864;
        }

        public bool Disabled
        {
            get => (WinStyle & 134217728) != 0;
            set => WinStyle |= 134217728;
        }

        public bool DlgFrame
        {
            get => (WinStyle & 4194304) != 0;
            set => WinStyle |= 4194304;
        }

        public bool Group
        {
            get => (WinStyle & 131072) != 0;
            set => WinStyle |= 131072;
        }

        public bool HScrollBar
        {
            get => (WinStyle & 1048576) != 0;
            set => WinStyle |= 1048576;
        }

        public bool VScrollBar
        {
            get => (WinStyle & 2097152) != 0;
            set => WinStyle |= 2097152;
        }

        public bool ScrollBars => (WinStyle & 3145728) != 0;

        public bool MaximizeBox
        {
            get => (WinStyle & 65536) != 0;
            set => WinStyle |= 65536;
        }

        public bool MinimizeBox
        {
            get => (WinStyle & 131072) != 0;
            set => WinStyle |= 131072;
        }

        public bool SysMenu
        {
            get => (WinStyle & 524288) != 0;
            set => WinStyle |= 524288;
        }

        public bool Minimized => (WinStyle & 536870912) != 0;

        public bool Maximized => (WinStyle & 16777216) != 0;

        public bool OverlappedWindow
        {
            get => (WinStyle & 13565952) == 13565952;
            set => WinStyle |= 13565952;
        }

        public bool Popup
        {
            get => (WinStyle & 2147483648L) != 0L;
            set => WinStyle |= 2147483648U;
        }

        public bool ThickFrame
        {
            get => (WinStyle & 262144) != 0;
            set => WinStyle |= 262144;
        }

        public bool Sizable => (WinStyle & 262144) != 0;

        public bool Visible
        {
            get => (WinStyle & 268435456) != 0;
            set => WinStyle |= 268435456;
        }

        public bool ModalDialog => (WinStyle & 12583040) == 12583040;

        public bool Restored => (WinStyle & 553648128) == 0;

        public bool TabStop => (WinStyle & 65536) != 0;

        public bool HelpBox => (WinExStyle & 1024) != 0;

        public bool ClientEdge => (WinExStyle & 512) != 0;

        public bool StaticEdge => (WinExStyle & 131072) != 0;

        public bool FixedFrame
        {
            get
            {
                if ((WinStyle & 262144) != 0)
                {
                    return false;
                }

                return (WinStyle & 4194304) == 0 ? (WinExStyle & 1) != 0 : true;
            }
        }

        public bool DialogModalFrame => (WinExStyle & 1) != 0;

        public bool ToolWindow
        {
            get => (WinExStyle & 128) != 0;
            set => WinExStyle |= 128;
        }

        public bool TopMost => (WinExStyle & 8) != 0;

        public bool MdiChild => (WinExStyle & 64) != 0;

        public bool Layered
        {
            get => (WinExStyle & 524288) != 0;
            set => WinExStyle |= 524288;
        }

        public bool Transparent
        {
            get => (WinExStyle & 32) != 0;
            set => WinExStyle |= 32;
        }

        public bool Composited
        {
            get => (WinExStyle & 33554432) != 0;
            set => WinExStyle |= 33554432;
        }

        public bool Framed => (WinStyle & 12845056) == 0 ? (WinExStyle & 513) != 0 : true;
    }
}
