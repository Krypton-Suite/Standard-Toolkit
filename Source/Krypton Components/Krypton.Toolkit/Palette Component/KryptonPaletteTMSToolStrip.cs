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
/// Storage for tool strip entries of the professional color table.
/// </summary>
public class KryptonPaletteTMSToolStrip : KryptonPaletteTMSBase
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteKCTToolStrip class.
    /// </summary>
    /// <param name="internalKCT">Reference to inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    internal KryptonPaletteTMSToolStrip(KryptonInternalKCT internalKCT,
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
    public override bool IsDefault => (InternalKCT.InternalToolStripText == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalToolStripFont == null) &&
                                      (InternalKCT.InternalToolStripBorder == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalToolStripContentPanelGradientBegin == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalToolStripContentPanelGradientEnd == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalToolStripDropDownBackground == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalToolStripGradientBegin == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalToolStripGradientEnd == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalToolStripGradientMiddle == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalToolStripPanelGradientBegin == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalToolStripPanelGradientEnd == GlobalStaticValues.EMPTY_COLOR);

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        ToolStripText = InternalKCT.ToolStripText;
        ToolStripFont = InternalKCT.ToolStripFont;
        ToolStripBorder = InternalKCT.ToolStripBorder;
        ToolStripContentPanelGradientBegin = InternalKCT.ToolStripContentPanelGradientBegin;
        ToolStripContentPanelGradientEnd = InternalKCT.ToolStripContentPanelGradientEnd;
        ToolStripDropDownBackground = InternalKCT.ToolStripDropDownBackground;
        ToolStripGradientBegin = InternalKCT.ToolStripGradientBegin;
        ToolStripGradientEnd = InternalKCT.ToolStripGradientEnd;
        ToolStripGradientMiddle = InternalKCT.ToolStripGradientMiddle;
        ToolStripPanelGradientBegin = InternalKCT.ToolStripPanelGradientBegin;
        ToolStripPanelGradientEnd = InternalKCT.ToolStripPanelGradientEnd;
    }
    #endregion

    #region ToolStripText
    /// <summary>
    /// Gets and sets the color to draw text on the tool strip.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Color to draw text on the ToolStrip.")]
    [KryptonDefaultColor]
    public Color ToolStripText
    {
        get => InternalKCT.InternalToolStripText;

        set
        {
            InternalKCT.InternalToolStripText = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the ToolStripText property to its default value.
    /// </summary>
    public void ResetToolStripText() => ToolStripText = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region ToolStripFont
    /// <summary>
    /// Gets and sets the font to draw text on the tool strip.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Font to draw text on the ToolStrip.")]
    [DefaultValue(null)]
    public Font? ToolStripFont
    {
        get => InternalKCT.InternalToolStripFont;

        set
        {
            InternalKCT.InternalToolStripFont = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the ToolStripFont property to its default value.
    /// </summary>
    public void ResetToolStripFont() => ToolStripFont = null;
    #endregion

    #region ToolStripBorder
    /// <summary>
    /// Gets and sets the border color to use on the bottom edge of the ToolStrip.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Border color to use on the bottom edge of the ToolStrip.")]
    [KryptonDefaultColor]
    public Color ToolStripBorder
    {
        get => InternalKCT.InternalToolStripBorder;

        set 
        { 
            InternalKCT.InternalToolStripBorder = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the ToolStripBorder property to its default value.
    /// </summary>
    public void ResetToolStripBorder() => ToolStripBorder = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region ToolStripContentPanelGradientBegin
    /// <summary>
    /// Gets and sets the starting color of the gradient used in the ToolStripContentPanel..
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Starting color of the gradient used in the ToolStripContentPanel..")]
    [KryptonDefaultColor]
    public Color ToolStripContentPanelGradientBegin
    {
        get => InternalKCT.InternalToolStripContentPanelGradientBegin;

        set 
        { 
            InternalKCT.InternalToolStripContentPanelGradientBegin = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the ToolStripContentPanelGradientBegin property to its default value.
    /// </summary>
    public void ResetToolStripContentPanelGradientBegin() => ToolStripContentPanelGradientBegin = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region ToolStripContentPanelGradientEnd
    /// <summary>
    /// Gets and sets the ending color of the gradient used in the ToolStripContentPanel.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Ending color of the gradient used in the ToolStripContentPanel.")]
    [KryptonDefaultColor]
    public Color ToolStripContentPanelGradientEnd
    {
        get => InternalKCT.InternalToolStripContentPanelGradientEnd;

        set 
        { 
            InternalKCT.InternalToolStripContentPanelGradientEnd = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the ToolStripContentPanelGradientEnd property to its default value.
    /// </summary>
    public void ResetToolStripContentPanelGradientEnd() => ToolStripContentPanelGradientEnd = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region ToolStripDropDownBackground
    /// <summary>
    /// Gets and sets the solid background color of the ToolStripDropDown..
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Solid background color solid background color of the ToolStripDropDown..")]
    [KryptonDefaultColor]
    public Color ToolStripDropDownBackground
    {
        get => InternalKCT.InternalToolStripDropDownBackground;

        set 
        { 
            InternalKCT.InternalToolStripDropDownBackground = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the ToolStripDropDownBackground property to its default value.
    /// </summary>
    public void ResetToolStripDropDownBackground() => ToolStripDropDownBackground = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region ToolStripGradientBegin
    /// <summary>
    /// Gets and sets the starting color of the gradient used in the ToolStrip background..
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Starting color of the gradient used in the ToolStrip background..")]
    [KryptonDefaultColor]
    public Color ToolStripGradientBegin
    {
        get => InternalKCT.InternalToolStripGradientBegin;

        set 
        { 
            InternalKCT.InternalToolStripGradientBegin = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the ToolStripGradientBegin property to its default value.
    /// </summary>
    public void ResetToolStripGradientBegin() => ToolStripGradientBegin = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region ToolStripGradientEnd
    /// <summary>
    /// Gets and sets the ending color of the gradient used in the ToolStrip background..
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Ending color of the gradient used in the ToolStrip background..")]
    [KryptonDefaultColor]
    public Color ToolStripGradientEnd
    {
        get => InternalKCT.InternalToolStripGradientEnd;

        set 
        { 
            InternalKCT.InternalToolStripGradientEnd = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the ToolStripGradientEnd property to its default value.
    /// </summary>
    public void ResetToolStripGradientEnd() => ToolStripGradientEnd = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region ToolStripGradientMiddle
    /// <summary>
    /// Gets and sets the ending color of the gradient used in the ToolStrip background..
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Ending color of the gradient used in the ToolStrip background..")]
    [KryptonDefaultColor]
    public Color ToolStripGradientMiddle
    {
        get => InternalKCT.InternalToolStripGradientMiddle;

        set 
        { 
            InternalKCT.InternalToolStripGradientMiddle = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the ToolStripGradientMiddle property to its default value.
    /// </summary>
    public void ResetToolStripGradientMiddle() => ToolStripGradientMiddle = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region ToolStripPanelGradientBegin
    /// <summary>
    /// Gets and sets the starting color of the gradient used in the ToolStripPanel..
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Starting color of the gradient used in the ToolStripPanel..")]
    [KryptonDefaultColor]
    public Color ToolStripPanelGradientBegin
    {
        get => InternalKCT.InternalToolStripPanelGradientBegin;

        set 
        { 
            InternalKCT.InternalToolStripPanelGradientBegin = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the ToolStripPanelGradientBegin property to its default value.
    /// </summary>
    public void ResetToolStripPanelGradientBegin() => ToolStripPanelGradientBegin = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region ToolStripPanelGradientEnd
    /// <summary>
    /// Gets and sets the ending color of the gradient used in the ToolStripPanel..
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Ending color of the gradient used in the ToolStripPanel..")]
    [KryptonDefaultColor]
    public Color ToolStripPanelGradientEnd
    {
        get => InternalKCT.InternalToolStripPanelGradientEnd;

        set 
        { 
            InternalKCT.InternalToolStripPanelGradientEnd = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the ToolStripPanelGradientEnd property to its default value.
    /// </summary>
    public void ResetToolStripPanelGradientEnd() => ToolStripPanelGradientEnd = GlobalStaticValues.EMPTY_COLOR;
    #endregion
}