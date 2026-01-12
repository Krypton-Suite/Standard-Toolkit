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
/// RTL-aware palette redirector for <see cref="KryptonForm"/>.
/// </summary>
internal class RTLPaletteRedirect : PaletteRedirect
{
    #region Instance Fields

    private readonly PaletteRedirect _baseRedirector;

    private readonly KryptonForm _ownerForm;

    #endregion

    #region Identity

    public RTLPaletteRedirect(PaletteRedirect baseRedirector, KryptonForm ownerForm) : base(baseRedirector)
    {
        _baseRedirector = baseRedirector;

        _ownerForm = ownerForm;
    }

    #endregion

    #region Public Overrides

    public override PaletteRelativeEdgeAlign GetButtonSpecEdge(PaletteButtonSpecStyle style)
    {
        var originalEdge = _baseRedirector.GetButtonSpecEdge(style);

        // If RTL layout is enabled, reverse the edge alignment
        if (_ownerForm.RightToLeftLayout)
        {
            return originalEdge switch
            {
                PaletteRelativeEdgeAlign.Near => PaletteRelativeEdgeAlign.Far,
                PaletteRelativeEdgeAlign.Far => PaletteRelativeEdgeAlign.Near,
                _ => originalEdge
            };
        }

        return originalEdge;
    }

    #endregion
}