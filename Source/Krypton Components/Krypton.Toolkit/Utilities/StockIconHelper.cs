#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV) & Giduac, et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class StockIconHelper
{
    public enum StockIconId
    {
        DocumentNotAssociated = 0,
        DocumentAssociated = 1,
        Application = 2,
        Folder = 3,
        FolderOpen = 4,
        Drive525 = 5,
        Drive35 = 6,
        DriveRemove = 7,
        DriveFixed = 8,
        DriveNetwork = 9,
        DriveNetworkDisconnected = 10,
        DriveCD = 11,
        DriveRAM = 12,
        World = 13,
        Server = 15,
        Printer = 16,
        MyNetwork = 17,
        Find = 22,
        Help = 23,
        Share = 28,
        Link = 29,
        SlowFile = 30,
        Recycler = 31,
        RecyclerFull = 32,
        MediaCDAudio = 40,
        Lock = 47,
        AutoList = 49,
        PrinterNet = 50,
        ServerShare = 51,
        PrinterFax = 52,
        PrinterFaxNet = 53,
        PrintNetwork = 54,
        PrintNet2 = 55,
        DocumentMSOffice = 56,
        ApplicationLogo = 57,
        RecycleBin = 31,
        RecycleBinFull = 32,
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct SHSTOCKICONINFO
    {
        public uint cbSize;
        public IntPtr hIcon;
        public int iSysImageIndex;
        public int iIcon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szPath;
    }

    [DllImport("shell32.dll", SetLastError = false)]
    private static extern int SHGetStockIconInfo(
        StockIconId siid,
        uint uFlags,
        ref SHSTOCKICONINFO psii);

    private const uint SHGSI_ICON = 0x000000100;

    /// <summary>Gets the stock icon.</summary>
    /// <param name="stockIconId">The stock icon identifier.</param>
    /// <returns>The selected icon.</returns>
    /// <exception cref="InvalidOperationException">Failed to retrieve icon</exception>
    public static Icon GetStockIcon(StockIconId stockIconId)
    {
        SHSTOCKICONINFO info = new SHSTOCKICONINFO();
        info.cbSize = (uint)Marshal.SizeOf(typeof(SHSTOCKICONINFO));

        int result = SHGetStockIconInfo(stockIconId, SHGSI_ICON, ref info);

        if (result == 0)
        {
            Icon icon = Icon.FromHandle(info.hIcon);

            DestroyIcon(info.hIcon);

            return icon;
        }
        else
        {
            throw new InvalidOperationException("Failed to retrieve icon");
        }
    }

    /// <summary>Destroys the icon.</summary>
    /// <param name="handle">The handle.</param>
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern bool DestroyIcon(IntPtr handle);
}