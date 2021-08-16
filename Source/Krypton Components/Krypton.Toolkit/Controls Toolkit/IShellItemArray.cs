using Krypton.Toolkit;
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

namespace System.Windows.Forms
{
    [Flags]
    internal enum GETPROPERTYSTOREFLAGS : uint
    {
        DEFAULT = 0x00000000,
        HANDLERPROPERTIESONLY = 0x00000001,
        READWRITE = 0x00000002,
        TEMPORARY = 0x00000004,
        FASTPROPERTIESONLY = 0x00000008,
        OPENSLOWITEM = 0x00000010,
        DELAYCREATION = 0x00000020,
        BESTEFFORT = 0x00000040,
        NO_OPLOCK = 0x00000080,
        PREFERQUERYPROPERTIES = 0x00000100,
        EXTRINSICPROPERTIES = 0x00000200,
        EXTRINSICPROPERTIESONLY = 0x00000400,
        VOLATILEPROPERTIES = 0x00000800,
        VOLATILEPROPERTIESONLY = 0x00001000,
        MASK_VALID = 0x00001FFF
    }

    internal struct PROPERTYKEY
    {
#pragma warning disable 649
        public Guid fmtid;
#pragma warning restore 649
        public uint pid;
    }

    internal enum SIATTRIBFLAGS : uint
    {
        AND = 0x1,
        OR = 0x2,
        APPCOMPAT = 0x3,
        MASK = 0x3,
        ALLITEMS = 0x4000
    }

    [ComImport]
    [Guid("B63EA76D-1F85-456F-A19C-48159EFA858B")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellItemArray
    {
        [PreserveSig]
        HRESULT BindToHandler(IntPtr pbc, ref Guid rbhid, ref Guid riid, out IntPtr ppvOut);

        [PreserveSig]
        HRESULT GetPropertyStore(GETPROPERTYSTOREFLAGS flags, ref Guid riid, out IntPtr ppv);

        [PreserveSig]
        HRESULT GetPropertyDescriptionList(ref PROPERTYKEY keyType, ref Guid riid, out IntPtr ppv);

        [PreserveSig]
        HRESULT GetAttributes(SIATTRIBFLAGS dwAttribFlags, uint sfgaoMask, out uint psfgaoAttribs);

        void GetCount(out uint pdwNumItems);

        void GetItemAt(uint dwIndex, out IShellItem ppsi);

        [PreserveSig]
        HRESULT EnumItems(out IntPtr ppenumShellItems);
    }
}
