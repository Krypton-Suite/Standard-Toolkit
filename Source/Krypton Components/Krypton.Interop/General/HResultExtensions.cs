namespace Krypton.Interop;

internal static class HResultExtensions
{
    public static bool Succeeded(this NativeFunctions.HRESULT hr) => (int)hr >= 0;

    public static bool Failed(this NativeFunctions.HRESULT hr) => (int)hr < 0;

    public static string AsString(this NativeFunctions.HRESULT hr)
        => Enum.IsDefined(typeof(NativeFunctions.HRESULT), hr)
            ? $"HRESULT {hr} [0x{(int)hr:X} ({(int)hr:D})]"
            : $"HRESULT [0x{(int)hr:X} ({(int)hr:D})]";

    public static Exception GetExceptionForHR(this NativeFunctions.HRESULT errorCode) => Marshal.GetExceptionForHR((int)errorCode)!;

    public static void ThrowExceptionIfFailed(this NativeFunctions.HRESULT hr)
    {
        if (Failed(hr))
        {
            throw GetExceptionForHR(hr);
        }
    }

}