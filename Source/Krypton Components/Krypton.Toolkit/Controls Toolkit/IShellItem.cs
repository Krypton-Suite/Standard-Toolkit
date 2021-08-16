// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace System.Windows.Forms
{
    internal enum SIGDN
    {
        NORMALDISPLAY = 0,
        PARENTRELATIVEPARSING = unchecked((int)0x80018001),
        DESKTOPABSOLUTEPARSING = unchecked((int)0x80028000),
        PARENTRELATIVEEDITING = unchecked((int)0x80031001),
        DESKTOPABSOLUTEEDITING = unchecked((int)0x8004c000),
        FILESYSPATH = unchecked((int)0x80058000),
        URL = unchecked((int)0x80068000),
        PARENTRELATIVEFORADDRESSBAR = unchecked((int)0x8007c001),
        PARENTRELATIVE = unchecked((int)0x80080001),
        PARENTRELATIVEFORUI = unchecked((int)0x80094001)
    }

    [Flags]
    internal enum SFGAOF : uint
    {
        CANCOPY = 0x00000001,
        CANMOVE = 0x00000002,
        CANLINK = 0x00000004,
        STORAGE = 0x00000008,
        CANRENAME = 0x00000010,
        CANDELETE = 0x00000020,
        HASPROPSHEET = 0x00000040,
        DROPTARGET = 0x00000100,
        CAPABILITYMASK = 0x00000177,
        SYSTEM = 0x00001000,
        ENCRYPTED = 0x00002000,
        ISSLOW = 0x00004000,
        GHOSTED = 0x00008000,
        LINK = 0x00010000,
        SHARE = 0x00020000,
        READONLY = 0x00040000,
        HIDDEN = 0x00080000,
        DISPLAYATTRMASK = 0x000FC000,
        FILESYSANCESTOR = 0x10000000,
        FOLDER = 0x20000000,
        FILESYSTEM = 0x40000000,
        HASSUBFOLDER = 0x80000000,
        CONTENTSMASK = 0x80000000,
        VALIDATE = 0x01000000,
        REMOVABLE = 0x02000000,
        COMPRESSED = 0x04000000,
        BROWSABLE = 0x08000000,
        NONENUMERATED = 0x00100000,
        NEWCONTENT = 0x00200000,
        CANMONIKER = 0x00400000,
        HASSTORAGE = 0x00400000,
        STREAM = 0x00400000,
        STORAGEANCESTOR = 0x00800000,
        STORAGECAPMASK = 0x70C50008,
        PKEYSFGAOMASK = 0x81044000
    }

    [ComImport]
    [Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellItem
    {
        [PreserveSig]
        HRESULT BindToHandler(IntPtr pbc, ref Guid bhid, ref Guid riid, out IntPtr ppv);

        [PreserveSig]
        HRESULT GetParent(out IShellItem ppsi);

        [PreserveSig]
        HRESULT GetDisplayName(SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

        [PreserveSig]
        HRESULT GetAttributes(SFGAOF sfgaoMask, out SFGAOF psfgaoAttribs);

        [PreserveSig]
        HRESULT Compare(IShellItem psi, uint hint, out int piOrder);
    }

}

namespace Krypton.Toolkit
{
    using System.Windows.Forms;

    internal static partial class PI
    {
        [DllImport(@"shell32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern HRESULT SHCreateItemFromParsingName(string pszPath, IntPtr pbc, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv);


        [DllImport(@"shell32.dll", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern HRESULT SHCreateShellItem(IntPtr pidlParent, IntPtr psfParent, IntPtr pidl, out IShellItem ppsi);

        internal static IShellItem GetShellItemForPath(string path)
        {
            if (PI.SHParseDisplayName(path, IntPtr.Zero, out IntPtr pidl, 0, out uint _).Succeeded())
            {
                // No parent specified
                if (SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, pidl, out IShellItem ret).Succeeded())
                {
                    return ret;
                }
            }

            throw new FileNotFoundException();
        }

        [DllImport(@"shell32.dll", CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]

        private static extern HRESULT SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name, IntPtr bindingContext, [Out()] out IntPtr pidl, uint sfgaoIn, [Out()] out uint psfgaoOut);

        [DllImport(@"shell32.dll", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern int SHGetSpecialFolderLocation(IntPtr hwnd, int csidl, out CoTaskMemSafeHandle ppidl);

        [DllImport(@"shell32.dll", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern CoTaskMemSafeHandle SHBrowseForFolderW(ref BROWSEINFO lpbi);

        internal delegate int BrowseCallbackProc(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData);

        internal static class BrowseInfoFlags
        {
            public const uint BIF_RETURNONLYFSDIRS = 0x00000001;
            public const uint BIF_DONTGOBELOWDOMAIN = 0x00000002;
            public const uint BIF_RETURNFSANCESTORS = 0x00000008;
            public const uint BIF_EDITBOX = 0x00000010;
            public const uint BIF_NEWDIALOGSTYLE = 0x00000040;
            public const uint BIF_NONEWFOLDERBUTTON = 0x00000200;

            public const uint BIF_BROWSEFORCOMPUTER = 0x00001000;
            public const uint BIF_BROWSEFORPRINTER = 0x00002000;
            public const uint BIF_BROWSEFOREVERYTHING = 0x00004000;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal unsafe struct BROWSEINFO
        {
            public IntPtr hwndOwner;

            public CoTaskMemSafeHandle pidlRoot;

            public IntPtr pszDisplayName;

            public string lpszTitle;

            public uint ulFlags;

            public BrowseCallbackProc lpfn;

            public IntPtr lParam;

            public int iImage;
        }

        [DllImport(@"shell32.dll", CharSet = CharSet.Auto)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern BOOL SHGetPathFromIDListEx(IntPtr pidl, IntPtr pszPath, int cchPath, int flags);

        internal partial class Kernel32
        {
            public const int MAX_PATH = 260;
            public const int MAX_UNICODESTRING_LEN = short.MaxValue;
        }

        internal static bool SHGetPathFromIDListLongPath(IntPtr pidl, out string path)
        {
            path = null;
            IntPtr pszPath = Marshal.AllocHGlobal((Kernel32.MAX_PATH + 1) * sizeof(char));
            int length = Kernel32.MAX_PATH;
            try
            {
                // SHGetPathFromIDListEx is basically a helper to get IShellFolder.DisplayNameOf() with some
                // extra functionally built in if the various flags are set.

                // SHGetPathFromIDListEx copies into the output buffer using StringCchCopyW, which truncates
                // when there isn't enough space (with a terminating null) and fails. Long paths can be
                // extracted by simply increasing the buffer size whenever the buffer is full.

                // To get the equivalent functionality we could call SHBindToParent on the PIDL to get IShellFolder
                // and then invoke IShellFolder.DisplayNameOf directly. This would avoid long path contortions as
                // we could directly convert from STRRET, calling CoTaskMemFree manually. (Presuming the type is
                // STRRET_WSTR, of course. Otherwise we can just fall back to StrRetToBufW and give up for > MAX_PATH.
                // Presumption is that we shouldn't be getting back ANSI results, and if we are they are likely
                // some very old component that won't have a > MAX_PATH string.)

                // While we could avoid contortions and avoid intermediate buffers by invoking IShellFolder directly,
                // it isn't without cost as we'd be initializing a COM wrapper (RCW) for IShellFolder. Presumably
                // this is much less overhead then looping and copying to intermediate buffers before creating a string.
                // Additionally, implementing this would allow us to short circuit the one caller (FolderBrowserDialog)
                // who doesn't care about the path, but just wants to know that we have an IShellFolder.
                string stringAuto = null;
                while (SHGetPathFromIDListEx(pidl, pszPath, length, 0).IsFalse())
                {
                    stringAuto = Marshal.PtrToStringAuto(pszPath);
                    if (stringAuto.Length == 0 || stringAuto.Length >= length)
                    {
                        length *= 2;
                        if (length > Kernel32.MAX_UNICODESTRING_LEN)
                        {
                            length = Kernel32.MAX_UNICODESTRING_LEN;
                        }

                        pszPath = Marshal.ReAllocHGlobal(pszPath, (IntPtr)(length * Marshal.SystemDefaultCharSize));
                    }
                    else
                        return false;
                }

                path = stringAuto;
                return true;
            }
            finally
            {
                Marshal.FreeHGlobal(pszPath);
            }
        }

    }
}