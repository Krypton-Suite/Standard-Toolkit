#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion


namespace Krypton.Toolkit;

/// <summary>
/// This class is a central facility where controls that have problems to react to losing focus register, and by will of the developer have these items closed.
/// </summary>
public static class FocusLostMenuHelper
{
    #region Private fields
    private static ConcurrentSimpleList<IFocusLostMenuItem> _items                   = new();
    private static ConcurrentSimpleList<ContextMenuStrip>   _winformsContextMenus    = new();
    private static ConcurrentSimpleList<ToolStrip>          _winformsToolStrips      = new();
    private static ConcurrentSimpleList<DateTimePicker>     _winformsDateTimePickers = new();
    #endregion

    #region Register
    /// <summary>
    /// Registers items that implement IFocusLostMenuItem.
    /// </summary>
    /// <param name="item">A valid IFocusLostMenuItem object instance.</param>
    public static void Register(IFocusLostMenuItem item)
    {
        _items.Add(item);
    }

    /// <summary>
    /// Registers a Winforms DateTimePicker.
    /// </summary>
    /// <param name="item">A valid DateTimePicker instance.</param>
    public static void Register(DateTimePicker item)
    {
        _winformsDateTimePickers.Add(item);
    }

    /// <summary>
    /// Registers a Winforms ContextMenuStrip.
    /// </summary>
    /// <param name="item">A valid ContextMenuStrip instance.</param>
    public static void Register(ContextMenuStrip item)
    {
        _winformsContextMenus.Add(item);
    }

    /// <summary>
    /// Registers a Winforms ToolStrip.
    /// </summary>
    /// <param name="item">A valid ToolStrip instance.</param>
    public static void Register(ToolStrip item)
    {
        _winformsToolStrips.Add(item);
    }

    /// <summary>
    /// Registers a Winforms MenuStrip.
    /// </summary>
    /// <param name="item">A valid MenuStrip instance.</param>
    public static void Register(MenuStrip item)
    {
        _winformsToolStrips.Add(item);
    }

    /// <summary>
    /// Registers a Winforms StatusStrip.
    /// </summary>
    /// <param name="item">A valid StatusStrip instance.</param>
    public static void Register(StatusStrip item)
    {
        _winformsToolStrips.Add(item);
    }
    #endregion

    #region Deregister
    /// <summary>
    /// Deregisters items that implement IFocusLostMenuItem.
    /// </summary>
    /// <param name="item">A valid IFocusLostMenuItem object instance.</param>
    public static void Deregister(IFocusLostMenuItem item)
    {
        _items.Remove(item);
    }

    /// <summary>
    /// Deregisters the WinForms ContextMenuStrip.
    /// </summary>
    /// <param name="item">A valid ContextMenuStrip instance.</param>
    public static void Deregister(ContextMenuStrip item)
    {
        _winformsContextMenus.Remove(item);
    }

    /// <summary>
    /// Deregisters the WinForms ToolStrip.
    /// </summary>
    /// <param name="item">A valid ToolStrip instance.</param>
    public static void Deregister(ToolStrip item)
    {
        _winformsToolStrips.Remove(item);
    }

    /// <summary>
    /// Deregisters the WinForms MenuStrip.
    /// </summary>
    /// <param name="item">A valid MenuStrip instance.</param>
    public static void Deregister(MenuStrip item)
    {
        _winformsToolStrips.Remove(item);
    }

    /// <summary>
    /// Deregisters the WinForms StatusStrip.
    /// </summary>
    /// <param name="item">A valid StatusStrip instance.</param>
    public static void Deregister(StatusStrip item)
    {
        _winformsToolStrips.Remove(item);
    }
    #endregion

    #region Item processing
    /// <summary>
    /// Processes all registered object and requests them close open menus.
    /// </summary>
    public static void ProcessItems()
    {
        ProcessStandardItems();
        ProcessWinformsContextMenus();
        ProcessWinformsToolStrips();
        ProcessWinformsDateTimePickers();
    }

    private static void ProcessStandardItems()
    {
        // Only process items implementing IFocusLostMenuItem
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i]?.ProcessItem();
        }
    }

    private static void ProcessWinformsContextMenus()
    {
        for (int i = 0; i < _winformsContextMenus.Count; i++)
        {
            _winformsContextMenus[i]?.Close(ToolStripDropDownCloseReason.AppFocusChange);
        }
    }

    private static void ProcessWinformsToolStrips()
    {
        for (int i = 0; i < _winformsToolStrips.Count; i++)
        {
            if (_winformsToolStrips[i] is ToolStrip toolStrip)
            {
                for (int j = 0; j < toolStrip.Items.Count; j++)
                {
                    if (toolStrip.Items[j] is ToolStripDropDownButton dropDownItem)
                    {
                        if (dropDownItem.DropDown.Visible)
                        {
                            dropDownItem.DropDown.Close(ToolStripDropDownCloseReason.AppFocusChange);
                            return;
                        }
                    }
                    else if (toolStrip.Items[j] is ToolStripMenuItem menuItem)
                    {
                        if (menuItem.DropDown.Visible)
                        {
                            menuItem.DropDown.Close(ToolStripDropDownCloseReason.AppFocusChange);
                            return;
                        }
                    }
                }
            }
        }
    }

    private static void ProcessWinformsDateTimePickers()
    {
        for (int i = 0; i < _winformsDateTimePickers.Count; i++)
        {
            PI.SendMessage(_winformsDateTimePickers[i].Handle, PI.DTM_.CLOSEMONTHCAL, IntPtr.Zero, IntPtr.Zero);
        }
    }
    #endregion
}
