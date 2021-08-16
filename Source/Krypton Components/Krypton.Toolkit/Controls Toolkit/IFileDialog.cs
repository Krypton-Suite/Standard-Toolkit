using Krypton.Toolkit;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace System.Windows.Forms
{
    internal static class CLSID
    {
        // C0B4E2F3-BA21-4773-8DBA-335EC946EB8B
        internal static Guid FileSaveDialog = new Guid(0xC0B4E2F3, 0xBA21, 0x4773, 0x8D, 0xBA, 0x33, 0x5E, 0xC9, 0x46, 0xEB, 0x8B);

        // DC1C5A9C-E88A-4DDE-A5A1-60F82A20AEF7
        internal static Guid FileOpenDialog = new Guid(0xDC1C5A9C, 0xE88A, 0x4DDE, 0xA5, 0xA1, 0x60, 0xF8, 0x2A, 0x20, 0xAE, 0xF7);
    }

    [Flags]
    internal enum FOS : uint
    {
        OVERWRITEPROMPT = 0x2,
        STRICTFILETYPES = 0x4,
        NOCHANGEDIR = 0x8,
        PICKFOLDERS = 0x20,
        FORCEFILESYSTEM = 0x40,     // Ensure that items returned are filesystem items.
        ALLNONSTORAGEITEMS = 0x80,  // Allow choosing items that have no storage.
        NOVALIDATE = 0x100,
        ALLOWMULTISELECT = 0x200,
        PATHMUSTEXIST = 0x800,
        FILEMUSTEXIST = 0x1000,
        CREATEPROMPT = 0x2000,
        SHAREAWARE = 0x4000,
        NOREADONLYRETURN = 0x8000,
        NOTESTFILECREATE = 0x10000,
        HIDEMRUPLACES = 0x20000,
        HIDEPINNEDPLACES = 0x40000,
        NODEREFERENCELINKS = 0x100000,
        OKBUTTONNEEDSINTERACTION = 0x200000,
        DONTADDTORECENT = 0x2000000,
        FORCESHOWHIDDEN = 0x10000000,
        DEFAULTNOMINIMODE = 0x20000000,
        FORCEPREVIEWPANEON = 0x40000000,
        SUPPORTSTREAMABLEITEMS = 0x80000000
    }

    internal enum FDAP : uint
    {
        BOTTOM = 0,
        TOP = 1
    }

    [ComImport]
    [Guid("42f85136-db7e-439c-85f1-e4075d135fc8")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFileDialog
    {
        [PreserveSig]
        HRESULT Show(IntPtr parent);

        [PreserveSig]
        HRESULT SetFileTypes(uint cFileTypes, [MarshalAs(UnmanagedType.LPArray)] COMDLG_FILTERSPEC[] rgFilterSpec);

        [PreserveSig]
        HRESULT SetFileTypeIndex(uint iFileType);

        void GetFileTypeIndex(out uint piFileType);

        void Advise(IFileDialogEvents pfde, out uint pdwCookie);

        void Unadvise(uint dwCookie);

        void SetOptions(FOS fos);

        void GetOptions(out FOS pfos);

        void SetDefaultFolder(IShellItem psi);

        void SetFolder(IShellItem psi);

        void GetFolder(out IShellItem ppsi);

        [PreserveSig]
        HRESULT GetCurrentSelection(out IShellItem ppsi);

        void SetFileName([MarshalAs(UnmanagedType.LPWStr)] string pszName);

        void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

        void SetTitle([MarshalAs(UnmanagedType.LPWStr)] string pszTitle);

        [PreserveSig]
        HRESULT SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] string pszText);

        [PreserveSig]
        HRESULT SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

        void GetResult(out IShellItem ppsi);

        [PreserveSig]
        HRESULT AddPlace(IShellItem psi, FDAP fdap);

        void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);

        void Close([MarshalAs(UnmanagedType.Error)] int hr);

        void SetClientGuid(ref Guid guid);

        [PreserveSig]
        HRESULT ClearClientData();

        [PreserveSig]
        HRESULT SetFilter(IntPtr pFilter);
    }
}