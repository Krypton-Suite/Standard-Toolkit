#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Event arguments for tab button spec click events.
/// </summary>
public class TabButtonSpecClickEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the TabButtonSpecClickEventArgs class.
    /// </summary>
    /// <param name="tabIndex">The index of the tab.</param>
    /// <param name="tabPage">The tab page.</param>
    /// <param name="buttonSpec">The button spec that was clicked.</param>
    public TabButtonSpecClickEventArgs(int tabIndex, TabPage tabPage, ButtonSpecAny buttonSpec)
    {
        TabIndex = tabIndex;
        TabPage = tabPage;
        ButtonSpec = buttonSpec;
    }

    /// <summary>
    /// Gets the index of the tab.
    /// </summary>
    public int TabIndex { get; }

    /// <summary>
    /// Gets the tab page.
    /// </summary>
    public TabPage TabPage { get; }

    /// <summary>
    /// Gets the button spec that was clicked.
    /// </summary>
    public ButtonSpecAny ButtonSpec { get; }
}