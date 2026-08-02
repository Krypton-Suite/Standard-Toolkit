#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Navigator.Utilities;

#region Enum NavigatorFormIntegrationMode

/// <summary>
/// How <see cref="KryptonNavigatorFormIntegrator"/> combines a navigator with a form.
/// </summary>
public enum NavigatorFormIntegrationMode
{
    /// <summary>
    /// Form keeps its caption control box; navigator sits in the client area.
    /// Selected page text can optionally sync to the form title.
    /// </summary>
    CaptionAdjacent = 0,

    /// <summary>
    /// Form caption control box is hidden; the navigator hosts minimize/maximize/close
    /// button specs (Chrome / Edge / Explorer-style client chrome ownership).
    /// </summary>
    ClientChrome = 1,

    /// <summary>
    /// Tab strip is injected into the <see cref="KryptonForm"/> caption (Ribbon-style
    /// <c>InjectViewElement</c>). The navigator shows page content only (<see cref="NavigatorMode.Panel"/>);
    /// the form keeps its control box in the caption.
    /// </summary>
    CaptionIntegrated = 2
}

#endregion
