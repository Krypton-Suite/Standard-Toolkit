#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2025. All rights reserved. 
 *  
 */
#endregion

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
namespace Krypton.Toolkit;

internal partial class PI
{
    internal enum HRESULT : uint
    {
        S_OK = 0,
        S_FALSE = 1,
        DRAGDROP_S_DROP = 0x00040100,
        DRAGDROP_S_CANCEL = 0x00040101,
        DRAGDROP_S_USEDEFAULTCURSORS = 0x00040102,

        E_NOTIMPL = 0x80004001,
        E_NOINTERFACE = 0x80004002,
        E_POINTER = 0x80004003,
        E_ABORT = 0x80004004,
        E_FAIL = 0x80004005,

        // These are CLR PI.HRESULT
        InvalidArgFailure = 0x80008081,
        CoreHostLibLoadFailure = 0x80008082,
        CoreHostLibMissingFailure = 0x80008083,
        CoreHostEntryPointFailure = 0x80008084,
        CoreHostCurHostFindFailure = 0x80008085,
        CoreClrResolveFailure = 0x80008087,
        CoreClrBindFailure = 0x80008088,
        CoreClrInitFailure = 0x80008089,
        CoreClrExeFailure = 0x8000808a,
        LibHostExecModeFailure = 0x80008090,
        LibHostSdkFindFailure = 0x80008091,
        LibHostInvalidArgs = 0x80008092,
        InvalidConfigFile = 0x80008093,
        AppArgNotRunnable = 0x80008094,
        AppHostExeNotBoundFailure = 0x80008095,
        FrameworkMissingFailure = 0x80008096,
        HostApiFailed = 0x80008097,
        HostApiBufferTooSmall = 0x80008098,
        LibHostUnknownCommand = 0x80008099,
        LibHostAppRootFindFailure = 0x8000809a,
        SdkResolverResolveFailure = 0x8000809b,
        FrameworkCompatFailure = 0x8000809c,
        FrameworkCompatRetry = 0x8000809d,

        RPC_E_CHANGED_MODE = 0x80010106,
        DISP_E_MEMBERNOTFOUND = 0x80020003,
        DISP_E_PARAMNOTFOUND = 0x80020004,
        DISP_E_UNKNOWNNAME = 0x80020006,
        DISP_E_EXCEPTION = 0x80020009,
        DISP_E_UNKNOWNLCID = 0x8002000C,
        DISP_E_DIVBYZERO = 0x80020012,
        TYPE_E_BADMODULEKIND = 0x800288BD,
        STG_E_INVALIDFUNCTION = 0x80030001,
        STG_E_FILENOTFOUND = 0x80030002,
        STG_E_ACCESSDENIED = 0x80030005,
        STG_E_INVALIDPOINTER = 0x80030009,
        STG_E_INVALIDPARAMETER = 0x80030057,
        STG_E_INVALIDFLAG = 0x800300FF,
        OLE_E_ADVISENOTSUPPORTED = 0x80040003,
        OLE_E_NOCONNECTION = 0x80040004,
        OLE_E_PROMPTSAVECANCELLED = 0x8004000C,
        OLE_E_INVALIDRECT = 0x8004000D,
        DV_E_FORMATETC = 0x80040064,
        DV_E_TYMED = 0x80040069,
        DV_E_DVASPECT = 0x8004006B,
        DRAGDROP_E_NOTREGISTERED = 0x80040100,
        DRAGDROP_E_ALREADYREGISTERED = 0x80040101,
        VIEW_E_DRAW = 0x80040140,
        INPLACE_E_NOTOOLSPACE = 0x800401A1,
        CO_E_OBJNOTREG = 0x800401FB,
        CO_E_OBJISREG = 0x800401FC,
        E_ACCESSDENIED = 0x80070005,
        E_OUTOFMEMORY = 0x8007000E,
        E_INVALIDARG = 0x80070057,
        ERROR_CANCELLED = 0x800704C7
    }
}

internal static class HResultExtensions
{
    public static bool Succeeded(this PI.HRESULT hr) => (int)hr >= 0;

    public static bool Failed(this PI.HRESULT hr) => (int)hr < 0;

    public static string AsString(this PI.HRESULT hr)
        => Enum.IsDefined(typeof(PI.HRESULT), hr)
            ? $"HRESULT {hr} [0x{(int)hr:X} ({(int)hr:D})]"
            : $"HRESULT [0x{(int)hr:X} ({(int)hr:D})]";

    public static Exception GetExceptionForHR(this PI.HRESULT errorCode) => Marshal.GetExceptionForHR((int)errorCode)!;

    public static void ThrowExceptionIfFailed(this PI.HRESULT hr)
    {
        if (Failed(hr))
        {
            throw GetExceptionForHR(hr);
        }
    }

}