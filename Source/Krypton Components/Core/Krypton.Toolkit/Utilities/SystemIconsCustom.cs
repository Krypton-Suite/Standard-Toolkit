// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Drawing;

/*internal class SystemIconsCustom
{
    #region Implementation

    /// <summary>
    ///  Gets the specified Windows shell stock icon.
    /// </summary>
    /// <param name="stockIcon">The stock icon to retrieve.</param>
    /// <param name="options">Options for retrieving the icon.</param>
    /// <returns>The requested <see cref="Icon"/>.</returns>
    /// <remarks>
    ///  <para>
    ///   Unlike the static icon properties in <see cref="SystemIcons"/>, this API returns icons that are themed
    ///   for the running version of Windows. Additionally, the returned <see cref="Icon"/> is not cached and
    ///   should be disposed when no longer needed.
    ///  </para>
    /// </remarks>
    /// <exception cref="ArgumentException"><paramref name="stockIcon"/> is an invalid <see cref="StockIconId"/>.</exception>
    public static unsafe Icon GetStockIcon(StockIconId stockIcon, StockIconOptions options = StockIconOptions.Default)
    {
        // Note that we don't explicitly check for invalid StockIconId to allow for accessing newer ids introduced
        // in later OSes. The HRESULT returned for undefined ids gets converted to an ArgumentException.

        Interop.Shell32.SHSTOCKICONINFO info = new()
        {
            cbSize = (uint)sizeof(Interop.Shell32.SHSTOCKICONINFO),
        };

        Interop.HRESULT result = Interop.Shell32.SHGetStockIconInfo(
            (uint)stockIcon,
            (uint)options | Interop.Shell32.SHGSI_ICON,
            ref info);

        // This only throws if there is an error.
        Marshal.ThrowExceptionForHR((int)result);

        return new Icon(info.hIcon, takeOwnership: true);
    }

    /// <inheritdoc cref="GetStockIcon(StockIconId, StockIconOptions)"/>
    /// <param name="size">
    ///  The desired size. If the specified size does not exist, an existing size will be resampled to give the
    ///  requested size.
    /// </param>
    public static unsafe Icon GetStockIcon(StockIconId stockIcon, int size)
    {
        // Note that we don't explicitly check for invalid StockIconId to allow for accessing newer ids introduced
        // in later OSes. The HRESULT returned for undefined ids gets converted to an ArgumentException.

        Interop.Shell32.SHSTOCKICONINFO info = new()
        {
            cbSize = (uint)sizeof(Interop.Shell32.SHSTOCKICONINFO),
        };

        Interop.HRESULT result = Interop.Shell32.SHGetStockIconInfo(
            (uint)stockIcon,
            Interop.Shell32.SHGSI_ICONLOCATION,
            ref info);

        // This only throws if there is an error.
        Marshal.ThrowExceptionForHR((int)result);

        nint hicon = 0;
        result = Interop.Shell32.SHDefExtractIcon(
            info.szPath,
            info.iIcon,
            0,
            &hicon,
            null,
            (uint)(ushort)size << 16 | (ushort)size);

        Marshal.ThrowExceptionForHR((int)result);

        return new Icon(hicon, takeOwnership: true);
    }

    #endregion
}*/