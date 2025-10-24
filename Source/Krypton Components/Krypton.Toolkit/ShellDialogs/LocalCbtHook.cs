// Decompiled with JetBrains decompiler
// Type: MsdnMag.LocalCbtHook
// Assembly: CbtHook, Version=1.0.921.20088, Culture=neutral, PublicKeyToken=null
// MVID: A8EAF865-DCC9-4DAB-9028-CAAB4F95DBE5
// Assembly location: Z:\Samples\MessageBoxCallbacks\MsgBoxCbtHook\bin\Debug\CbtHook.dll
// Original from http://msdn.microsoft.com/msdnmag/issues/02/10/cuttingedge
/*
 * Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 */

using Krypton.Toolkit;
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming


namespace MsdnMag;

internal class HookEventArgs : EventArgs
{
    public int HookCode;
    public IntPtr wParam;
    public IntPtr lParam;
}

internal enum CbtHookAction
{
    HCBT_MOVESIZE,
    HCBT_MINMAX,
    HCBT_QS,
    HCBT_CREATEWND,
    HCBT_DESTROYWND,
    HCBT_ACTIVATE,
    HCBT_CLICKSKIPPED,
    HCBT_KEYSKIPPED,
    HCBT_SYSCOMMAND,
    HCBT_SETFOCUS
}

internal class CbtEventArgs : EventArgs
{
    public IntPtr Handle;
    public string Title;
    public string ClassName;
    public bool IsDialogWindow;
}

internal class LocalCbtHook : LocalWindowsHook
{
    protected IntPtr m_hWnd = IntPtr.Zero;
    protected string m_title;
    protected string m_class;
    protected bool m_isDialog ;

    public event CbtEventHandler? WindowCreated;

    public event CbtEventHandler? WindowDestroyed;

    public event CbtEventHandler? WindowActivated;

    public LocalCbtHook()
        : base(HookType.WH_CBT) => HookInvoked += CbtHookInvoked;

    public LocalCbtHook(HookProc func)
        : base(HookType.WH_CBT, func) => HookInvoked += CbtHookInvoked;

    private void CbtHookInvoked(object sender, HookEventArgs e)
    {
        var hookCode = (CbtHookAction)e.HookCode;
        var wParam = e.wParam;
        var lParam = e.lParam;
        switch (hookCode)
        {
            case CbtHookAction.HCBT_CREATEWND:
                HandleCreateWndEvent(wParam, lParam);
                break;
            case CbtHookAction.HCBT_DESTROYWND:
                HandleDestroyWndEvent(wParam, lParam);
                break;
            case CbtHookAction.HCBT_ACTIVATE:
                HandleActivateEvent(wParam, lParam);
                break;
        }
    }

    private void HandleCreateWndEvent(IntPtr wParam, IntPtr lParam)
    {
        UpdateWindowData(wParam);
        OnWindowCreated();
    }

    private void HandleDestroyWndEvent(IntPtr wParam, IntPtr lParam)
    {
        UpdateWindowData(wParam);
        OnWindowDestroyed();
    }

    private void HandleActivateEvent(IntPtr wParam, IntPtr lParam)
    {
        UpdateWindowData(wParam);
        OnWindowActivated();
    }

    private void UpdateWindowData(IntPtr wParam)
    {
        m_hWnd = wParam;
        var lpClassName = new StringBuilder
        {
            Capacity = 40
        };
        GetClassName(m_hWnd, lpClassName, 40);
        m_class = lpClassName.ToString();
        var lpString = new StringBuilder
        {
            Capacity = 256
        };
        GetWindowText(m_hWnd, lpString, 256);
        m_title = lpString.ToString();
        m_isDialog = m_class == "#32770";
    }

    protected virtual void OnWindowCreated()
    {
        if (WindowCreated == null)
        {
            return;
        }

        var e = new CbtEventArgs();
        PrepareEventData(e);
        WindowCreated(this, e);
    }

    protected virtual void OnWindowDestroyed()
    {
        if (WindowDestroyed == null)
        {
            return;
        }

        var e = new CbtEventArgs();
        PrepareEventData(e);
        WindowDestroyed(this, e);
    }

    protected virtual void OnWindowActivated()
    {
        if (WindowActivated == null)
        {
            return;
        }

        var e = new CbtEventArgs();
        PrepareEventData(e);
        WindowActivated(this, e);
    }

    private void PrepareEventData(CbtEventArgs e)
    {
        e.Handle = m_hWnd;
        e.Title = m_title;
        e.ClassName = m_class;
        e.IsDialogWindow = m_isDialog;
    }

    [DllImport(Libraries.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport(Libraries.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    public delegate void CbtEventHandler(object sender, CbtEventArgs e);
}