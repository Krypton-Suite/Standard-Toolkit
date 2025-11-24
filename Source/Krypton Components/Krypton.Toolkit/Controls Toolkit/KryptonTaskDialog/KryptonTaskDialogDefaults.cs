#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public record struct KryptonTaskDialogDefaults
{
    public KryptonTaskDialogDefaults(int clientWidth)
    {
        ClientWidth = clientWidth;
        ClientHeight = 600;
        PanelLeft = 10;
        PanelTop = 10;
        PanelBottom = 10;
        PanelRight = 10;
        ComponentSpace = 10;

        NullPadding = new(0);
        NullMargin = new(0);

        PanelPadding1 = new Padding(PanelLeft, PanelTop, PanelRight, PanelBottom);

        CornerRoundingRatio = 10;

        ButtonSize_75x24 = new Size(75, 24);
        ButtonSize_24x75 = new Size(24, 75);

        TLP = new KryptonTaskDialogDefaultsTLP()
        {
            StdMinSize = new Size(ClientWidth - PanelLeft - PanelRight, 0),
            StdMaxSize = new Size(ClientWidth - PanelLeft - PanelRight, 0),
            AnchorTopLeftRight = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
        };
    }

    #region Properties
    public int ClientWidth { get; }
    public int ClientHeight { get; }
    public int PanelLeft { get; }
    public int PanelTop { get; }
    public int PanelBottom { get; }
    public int PanelRight { get; }
    public int ComponentSpace { get; }
    public int CornerRoundingRatio { get; }

    public Padding NullPadding { get; }
    public Padding NullMargin { get; }

    /// <summary>
    /// Standard button size: 75 x 24;
    /// </summary>
    public Size ButtonSize_75x24 { get; }

    /// <summary>
    /// Standard button size for vertical use: 24 x 75;
    /// </summary>
    public Size ButtonSize_24x75 { get; }

    /// <summary>
    /// Panel padding that centers it's contents relative to :<br/>
    /// PanelLeft, PanelTop, PanelRight, PanelBottom
    /// </summary>
    public Padding PanelPadding1 { get; }

    /// <summary>
    /// Default for TableLayoutPanels
    /// </summary>
    public KryptonTaskDialogDefaultsTLP TLP { get; }

    public int GetCornerRouding( bool roundingEnabled)
    {
        return roundingEnabled ? CornerRoundingRatio : -1;
    }
    #endregion
}

public record struct KryptonTaskDialogDefaultsTLP
{
    /// <summary>
    /// Table Layout Panel Standard Size<br/>
    /// ClientWidth - PanelLeft - PanelRight
    /// </summary>
    public Size StdMinSize { get; internal set; }

    /// <summary>
    /// Table Layout Panel Standard Size<br/>
    /// ClientWidth - PanelLeft - PanelRight
    /// </summary>
    public Size StdMaxSize { get; internal set; }

    /// <summary>
    /// Anchors the TLP to the Left Top and the Right.
    /// </summary>
    public AnchorStyles AnchorTopLeftRight { get; internal set; }
}

