// Decompiled with JetBrains decompiler
// Type: MsdnMag.LocalWindowsHook
// Assembly: WindowsHook, Version=1.0.921.18849, Culture=neutral, PublicKeyToken=null
// Original from http://msdn.microsoft.com/msdnmag/issues/02/10/cuttingedge
/*
 * Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 20123 - 2023. All rights reserved.
 */

using Krypton.Toolkit;

// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming


namespace MsdnMag
{
    internal enum HookType
    {
        WH_JOURNALRECORD,
        WH_JOURNALPLAYBACK,
        WH_KEYBOARD,
        WH_GETMESSAGE,
        WH_CALLWNDPROC,
        WH_CBT,
        WH_SYSMSGFILTER,
        WH_MOUSE,
        WH_HARDWARE,
        WH_DEBUG,
        WH_SHELL,
        WH_FOREGROUNDIDLE,
        WH_CALLWNDPROCRET,
        WH_KEYBOARD_LL,
        WH_MOUSE_LL
    }

    internal class LocalWindowsHook
    {
        protected IntPtr m_hHook = IntPtr.Zero;
        protected HookProc m_filterFunc;
        protected readonly HookType m_hookType;

        public event HookEventHandler? HookInvoked;

        protected void OnHookInvoked(HookEventArgs e) => HookInvoked?.Invoke((object)this, e);

        protected LocalWindowsHook(HookType hook)
        {
            m_hookType = hook;
            m_filterFunc = CoreHookProc;
        }

        protected LocalWindowsHook(HookType hook, HookProc func)
        {
            m_hookType = hook;
            m_filterFunc = func;
        }

        protected int CoreHookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
            {
                return CallNextHookEx(m_hHook, code, wParam, lParam);
            }

            OnHookInvoked(new HookEventArgs
            {
                HookCode = code,
                wParam = wParam,
                lParam = lParam
            });
            return CallNextHookEx(m_hHook, code, wParam, lParam);
        }

        public void Install() =>
#pragma warning disable CS0618
            m_hHook = SetWindowsHookEx(m_hookType, m_filterFunc, IntPtr.Zero, AppDomain.GetCurrentThreadId());//Thread.CurrentThread.ManagedThreadId);
#pragma warning restore CS0618

        public void Uninstall() => UnhookWindowsHookEx(m_hHook);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern IntPtr SetWindowsHookEx( HookType code, HookProc func, IntPtr hInstance, int threadID);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern int UnhookWindowsHookEx(IntPtr hHook);

        [DllImport(Libraries.User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        protected static extern int CallNextHookEx( IntPtr hHook, int code, IntPtr wParam, IntPtr lParam);

        public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

        public delegate void HookEventHandler(object sender, HookEventArgs e);
    }
}
