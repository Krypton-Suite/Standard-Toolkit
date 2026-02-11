using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Utilities;

[StructLayout(LayoutKind.Sequential)]
public struct CWPRETSTRUCT
{
    public IntPtr lResult;
    public IntPtr lParam;
    public IntPtr wParam;
    public uint message;
    public IntPtr hwnd;
};