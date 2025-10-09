#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace TestForm;

public partial class IconExtractionTest : KryptonForm
{
    public IconExtractionTest()
    {
        InitializeComponent();
    }

    private void IconExtractionTest_Load(object sender, EventArgs e)
    {
        LoadImageresIcons();
        LoadShell32Icons();
    }

    private void LoadImageresIcons()
    {
        // Extract UAC Shield icon from imageres.dll
        var shieldIcon = GraphicsExtensions.ExtractIconFromImageres((int)ImageresIconID.Shield, IconSize.ExtraLarge);
        if (shieldIcon != null)
        {
            kpbImageresShield.Image = shieldIcon.ToBitmap();
            klblImageresShield.Text = "Shield (imageres.dll)";
        }

        // Extract Lock icon from imageres.dll
        var lockIcon = GraphicsExtensions.ExtractIconFromImageres((int)ImageresIconID.Lock, IconSize.ExtraLarge);
        if (lockIcon != null)
        {
            kpbImageresLock.Image = lockIcon.ToBitmap();
            klblImageresLock.Text = "Lock (imageres.dll)";
        }

        // Extract User icon from imageres.dll
        var userIcon = GraphicsExtensions.ExtractIconFromImageres((int)ImageresIconID.User, IconSize.ExtraLarge);
        if (userIcon != null)
        {
            kpbImageresUser.Image = userIcon.ToBitmap();
            klblImageresUser.Text = "User (imageres.dll)";
        }
    }

    private void LoadShell32Icons()
    {
        // Extract Folder icon from shell32.dll
        var folderIcon = GraphicsExtensions.ExtractIconFromShell32((int)Shell32IconID.Folder, IconSize.ExtraLarge);
        if (folderIcon != null)
        {
            kpbShell32Folder.Image = folderIcon.ToBitmap();
            klblShell32Folder.Text = "Folder (shell32.dll)";
        }

        // Extract Computer icon from shell32.dll
        var computerIcon = GraphicsExtensions.ExtractIconFromShell32((int)Shell32IconID.Computer, IconSize.ExtraLarge);
        if (computerIcon != null)
        {
            kpbShell32Computer.Image = computerIcon.ToBitmap();
            klblShell32Computer.Text = "Computer (shell32.dll)";
        }

        // Extract Recycle Bin icon from shell32.dll
        var recycleBinIcon = GraphicsExtensions.ExtractIconFromShell32((int)Shell32IconID.RecycleBinEmpty, IconSize.ExtraLarge);
        if (recycleBinIcon != null)
        {
            kpbShell32RecycleBin.Image = recycleBinIcon.ToBitmap();
            klblShell32RecycleBin.Text = "Recycle Bin (shell32.dll)";
        }

        // Extract Network icon from shell32.dll
        var networkIcon = GraphicsExtensions.ExtractIconFromShell32((int)Shell32IconID.Network, IconSize.ExtraLarge);
        if (networkIcon != null)
        {
            kpbShell32Network.Image = networkIcon.ToBitmap();
            klblShell32Network.Text = "Network (shell32.dll)";
        }

        // Extract Printer icon from shell32.dll
        var printerIcon = GraphicsExtensions.ExtractIconFromShell32((int)Shell32IconID.Printer, IconSize.ExtraLarge);
        if (printerIcon != null)
        {
            kpbShell32Printer.Image = printerIcon.ToBitmap();
            klblShell32Printer.Text = "Printer (shell32.dll)";
        }

        // Extract Search icon from shell32.dll
        var searchIcon = GraphicsExtensions.ExtractIconFromShell32((int)Shell32IconID.Search, IconSize.ExtraLarge);
        if (searchIcon != null)
        {
            kpbShell32Search.Image = searchIcon.ToBitmap();
            klblShell32Search.Text = "Search (shell32.dll)";
        }
    }
}

