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
/// Storage for status strip entries of the professional color table.
/// </summary>
public class KryptonPaletteTMSStatusStrip : KryptonPaletteTMSBase
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteKCTStatusStrip class.
    /// </summary>
    /// <param name="internalKCT">Reference to inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    internal KryptonPaletteTMSStatusStrip(KryptonInternalKCT internalKCT,
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
    public override bool IsDefault => (InternalKCT.InternalStatusStripText == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalStatusStripFont == null) &&
                                      (InternalKCT.InternalStatusStripGradientBegin == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalStatusStripGradientEnd == GlobalStaticValues.EMPTY_COLOR);

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        StatusStripText = InternalKCT.StatusStripText;
        StatusStripFont = InternalKCT.StatusStripFont;
        StatusStripGradientBegin = InternalKCT.StatusStripGradientBegin;
        StatusStripGradientEnd = InternalKCT.StatusStripGradientEnd;
    }
    #endregion

    #region StatusStripText
    /// <summary>
    /// Gets and sets the color to draw text on the status strip.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Color to draw text on the StatusStrip.")]
    [KryptonDefaultColor]
    public Color StatusStripText
    {
        get => InternalKCT.InternalStatusStripText;

        set
        {
            InternalKCT.InternalStatusStripText = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the StatusStripText property to its default value.
    /// </summary>
    public void ResetStatusStripText() => StatusStripText = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region StatusStripFont
    /// <summary>
    /// Gets and sets the font to draw text on the status strip.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Font to draw text on the StatusStrip.")]
    [DefaultValue(null)]
    public Font? StatusStripFont
    {
        get => InternalKCT.InternalStatusStripFont;

        set
        {
            InternalKCT.InternalStatusStripFont = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the StatusStripFont property to its default value.
    /// </summary>
    public void ResetStatusStripFont() => StatusStripText = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region StatusStripGradientBegin
    /// <summary>
    /// Gets and sets the starting color of the gradient used on the StatusStrip.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Starting color of the gradient used on the StatusStrip.")]
    [KryptonDefaultColor]
    public Color StatusStripGradientBegin
    {
        get => InternalKCT.InternalStatusStripGradientBegin;

        set 
        { 
            InternalKCT.InternalStatusStripGradientBegin = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the StatusStripGradientBegin property to its default value.
    /// </summary>
    public void ResetStatusStripGradientBegin() => StatusStripGradientBegin = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region StatusStripGradientEnd
    /// <summary>
    /// Gets and sets the ending color of the gradient used on the StatusStrip.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Ending color of the gradient used on the StatusStrip.")]
    [KryptonDefaultColor]
    public Color StatusStripGradientEnd
    {
        get => InternalKCT.InternalStatusStripGradientEnd;

        set 
        { 
            InternalKCT.InternalStatusStripGradientEnd = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the StatusStripGradientEnd property to its default value.
    /// </summary>
    public void ResetStatusStripGradientEnd() => StatusStripGradientEnd = GlobalStaticValues.EMPTY_COLOR;
    #endregion
}