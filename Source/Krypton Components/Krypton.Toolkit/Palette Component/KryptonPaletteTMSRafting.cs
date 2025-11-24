#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for rafting entries of the professional color table.
/// </summary>
public class KryptonPaletteTMSRafting : KryptonPaletteTMSBase
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteKCTRafting class.
    /// </summary>
    /// <param name="internalKCT">Reference to inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    internal KryptonPaletteTMSRafting(KryptonInternalKCT internalKCT,
        NeedPaintHandler needPaint)
        : base(internalKCT, needPaint)
    {
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (InternalKCT.InternalRaftingContainerGradientBegin == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalRaftingContainerGradientEnd == GlobalStaticValues.EMPTY_COLOR);

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        RaftingContainerGradientBegin = InternalKCT.RaftingContainerGradientBegin;
        RaftingContainerGradientEnd = InternalKCT.RaftingContainerGradientEnd;
    }
    #endregion

    #region RaftingContainerGradientBegin
    /// <summary>
    /// Gets and sets the starting color of the gradient used in the ToolStripContainer.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Starting color of the gradient used in the ToolStripContainer.")]
    [KryptonDefaultColor]
    public Color RaftingContainerGradientBegin
    {
        get => InternalKCT.InternalRaftingContainerGradientBegin;

        set 
        { 
            InternalKCT.InternalRaftingContainerGradientBegin = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the RaftingContainerGradientBegin property to its default value.
    /// </summary>
    public void ResetRaftingContainerGradientBegin() => RaftingContainerGradientBegin = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region RaftingContainerGradientEnd
    /// <summary>
    /// Gets and sets the ending color of the gradient used in the ToolStripContainer.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Ending color of the gradient used in the ToolStripContainer.")]
    [KryptonDefaultColor]
    public Color RaftingContainerGradientEnd
    {
        get => InternalKCT.InternalRaftingContainerGradientEnd;

        set 
        { 
            InternalKCT.InternalRaftingContainerGradientEnd = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the RaftingContainerGradientEnd property to its default value.
    /// </summary>
    public void ResetRaftingContainerGradientEnd() => RaftingContainerGradientEnd = GlobalStaticValues.EMPTY_COLOR;
    #endregion
}