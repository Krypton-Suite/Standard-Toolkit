#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2021. All rights reserved. 
 */
#endregion

// ReSharper disable UnusedType.Global

namespace Krypton.Toolkit
{
    /// <summary>
    ///  Represents
    ///  a common dialog box that displays a list of fonts that are currently installed
    ///  on
    ///  the system.
    /// </summary>
    public class KryptonFontDialog : FontDialog
    {
        private readonly CommonDialogHandler _commonDialogHandler;

        /// <summary>
        ///  Represents
        ///  a common dialog box that displays a list of fonts that are currently installed
        ///  on
        ///  the system.
        /// </summary>
        public KryptonFontDialog()
        {
            _commonDialogHandler = new CommonDialogHandler(true);
        }

        //protected override bool RunDialog(IntPtr hWndOwner)
        //{
        //    var ret = base.RunDialog(hWndOwner);

        //    var wndProc = new PI.WndProc(HookProc);
        //    var cf = new PI.CHOOSEFONT();
        //    IntPtr dc = PI.GetDC(IntPtr.Zero);
        //    var logfont = new PI.LOGFONT();
        //    Graphics graphics = Graphics.FromHdcInternal(dc);
        //    try
        //    {
        //        this.Font.ToLogFont((object)logfont, graphics);
        //    }
        //    finally
        //    {
        //        graphics.Dispose();
        //    }
        //    PI.ReleaseDC(IntPtr.Zero, dc);
        //    IntPtr num = IntPtr.Zero;
        //    try
        //    {
        //        num = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(PI.LOGFONT)));
        //        Marshal.StructureToPtr((object)logfont, num, false);
        //        cf.lStructSize = Marshal.SizeOf(typeof(PI.CHOOSEFONT));
        //        cf.hwndOwner = hWndOwner;
        //        cf.hDC = IntPtr.Zero;
        //        cf.lpLogFont = num;
        //        cf.Flags = this.Options | 64 | 8;
        //        //if (this.minSize > 0 || this.maxSize > 0)
        //            cf.Flags |= 8192;
        //        cf.rgbColors = /*this.ShowColor || this.ShowEffects ? ColorTranslator.ToWin32(this.color) :*/ ColorTranslator.ToWin32(SystemColors.ControlText);
        //        cf.lpfnHook = wndProc;
        //        cf.hInstance = PI.GetModuleHandle((string)null);
        //        cf.nSizeMin = 1;//this.minSize;
        //        cf.nSizeMax = /*this.maxSize != 0 ? this.maxSize :*/ int.MaxValue;
        //        if (!PI.ChooseFont(cf))
        //            return false;
        //        PI.LOGFONT structure = (PI.LOGFONT)PI.PtrToStructure(num, typeof(PI.LOGFONT));
        //        if (structure.lfFaceName != null && structure.lfFaceName.Length > 0)
        //        {
        //            //this.UpdateFont(structure);
        //            //this.UpdateColor(cf.rgbColors);
        //        }
        //        return true;
        //    }
        //    finally
        //    {
        //        if (num != IntPtr.Zero)
        //            Marshal.FreeCoTaskMem(num);
        //    }
        //    //return ret;// || _commonDialogHandler._T;
        //}

        protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
        {
            (var handled, var retValue) = _commonDialogHandler.HookProc(hWnd, msg, wparam, lparam);
            return handled ? retValue :base.HookProc(hWnd, msg, wparam, lparam);
        }

        private bool EnumerateChildWindow(IntPtr hwnd, IntPtr lParam)
        {
            bool result = false;
            GCHandle gch = GCHandle.FromIntPtr(lParam);
            if (gch.Target is List<IntPtr> list)
            {
                list.Add(hwnd);
                result = true; // return true as long as children are found
            }
            return result;
        }
    }
}
