#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for button entries of the professional color table.
/// </summary>
public class KryptonPaletteTMSButton : KryptonPaletteTMSBase
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteKCTButton class.
    /// </summary>
    /// <param name="internalKCT">Reference to inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    internal KryptonPaletteTMSButton(KryptonInternalKCT internalKCT,
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
    public override bool IsDefault => (InternalKCT.InternalButtonCheckedGradientBegin == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalButtonCheckedGradientEnd == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalButtonCheckedGradientMiddle == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalButtonCheckedHighlight == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalButtonCheckedHighlightBorder == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalButtonPressedBorder == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalButtonPressedGradientBegin == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalButtonPressedGradientEnd == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalButtonPressedGradientMiddle == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalButtonPressedHighlight == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalButtonPressedHighlightBorder == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalButtonSelectedBorder == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalButtonSelectedGradientBegin == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalButtonSelectedGradientEnd == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalButtonSelectedGradientMiddle == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalButtonSelectedHighlight == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalButtonSelectedHighlightBorder == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalCheckBackground == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalCheckPressedBackground == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalCheckSelectedBackground == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalOverflowButtonGradientBegin == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalOverflowButtonGradientEnd == GlobalStaticVariables.EMPTY_COLOR) &&
                                      (InternalKCT.InternalOverflowButtonGradientMiddle == GlobalStaticVariables.EMPTY_COLOR);

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        ButtonCheckedGradientBegin = InternalKCT.ButtonCheckedGradientBegin;
        ButtonCheckedGradientEnd = InternalKCT.ButtonCheckedGradientEnd;
        ButtonCheckedGradientMiddle = InternalKCT.ButtonCheckedGradientMiddle;
        ButtonCheckedHighlight = InternalKCT.ButtonCheckedHighlight;
        ButtonCheckedHighlightBorder = InternalKCT.ButtonCheckedHighlightBorder;
        ButtonPressedBorder = InternalKCT.ButtonPressedBorder;
        ButtonPressedGradientBegin = InternalKCT.ButtonPressedGradientBegin;
        ButtonPressedGradientEnd = InternalKCT.ButtonPressedGradientEnd;
        ButtonPressedGradientMiddle = InternalKCT.ButtonPressedGradientMiddle;
        ButtonPressedHighlight = InternalKCT.ButtonPressedHighlight;
        ButtonPressedHighlightBorder = InternalKCT.ButtonPressedHighlightBorder;
        ButtonSelectedBorder = InternalKCT.ButtonSelectedBorder;
        ButtonSelectedGradientBegin = InternalKCT.ButtonSelectedGradientBegin;
        ButtonSelectedGradientEnd = InternalKCT.ButtonSelectedGradientEnd;
        ButtonSelectedGradientMiddle = InternalKCT.ButtonSelectedGradientMiddle;
        ButtonSelectedHighlight = InternalKCT.ButtonSelectedHighlight;
        ButtonSelectedHighlightBorder = InternalKCT.ButtonSelectedHighlightBorder;
        CheckBackground = InternalKCT.CheckBackground;
        CheckPressedBackground = InternalKCT.CheckPressedBackground;
        CheckSelectedBackground = InternalKCT.CheckSelectedBackground;
        OverflowButtonGradientBegin = InternalKCT.OverflowButtonGradientBegin;
        OverflowButtonGradientEnd = InternalKCT.OverflowButtonGradientEnd;
        OverflowButtonGradientMiddle = InternalKCT.OverflowButtonGradientMiddle;
    }
    #endregion

    #region ButtonCheckedGradientBegin
    /// <summary>
    /// Gets and sets the starting color of the gradient used when the button is checked.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Starting color of the gradient used when the button is checked.")]
    [KryptonDefaultColor]
    public Color ButtonCheckedGradientBegin
    {
        get => InternalKCT.InternalButtonCheckedGradientBegin;

        set 
        { 
            InternalKCT.InternalButtonCheckedGradientBegin = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonCheckedGradientBegin property to its default value.
    /// </summary>
    public void ResetButtonCheckedGradientBegin() => ButtonCheckedGradientBegin = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region ButtonCheckedGradientEnd
    /// <summary>
    /// Gets and sets the ending color of the gradient used when the button is checked.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Ending color of the gradient used when the button is checked.")]
    [KryptonDefaultColor]
    public Color ButtonCheckedGradientEnd
    {
        get => InternalKCT.InternalButtonCheckedGradientEnd;

        set 
        { 
            InternalKCT.InternalButtonCheckedGradientEnd = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonCheckedGradientEnd property to its default value.
    /// </summary>
    public void ResetButtonCheckedGradientEnd() => ButtonCheckedGradientEnd = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region ButtonCheckedGradientMiddle
    /// <summary>
    /// Gets and sets the middle color of the gradient used when the button is checked.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Middle color of the gradient used when the button is checked.")]
    [KryptonDefaultColor]
    public Color ButtonCheckedGradientMiddle
    {
        get => InternalKCT.InternalButtonCheckedGradientMiddle;

        set 
        { 
            InternalKCT.InternalButtonCheckedGradientMiddle = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonCheckedGradientMiddle property to its default value.
    /// </summary>
    public void ResetButtonCheckedGradientMiddle() => ButtonCheckedGradientMiddle = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region ButtonCheckedHighlight
    /// <summary>
    /// Gets and sets the highlight color used when the button is checked.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Highlight color used when the button is checked.")]
    [KryptonDefaultColor]
    public Color ButtonCheckedHighlight
    {
        get => InternalKCT.InternalButtonCheckedHighlight;

        set 
        { 
            InternalKCT.InternalButtonCheckedHighlight = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonCheckedHighlight property to its default value.
    /// </summary>
    public void ResetButtonCheckedHighlight() => ButtonCheckedHighlight = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region ButtonCheckedHighlightBorder
    /// <summary>
    /// Gets and sets the border color to use with ButtonCheckedHighlight.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Border color to use with ButtonCheckedHighlight.")]
    [KryptonDefaultColor]
    public Color ButtonCheckedHighlightBorder
    {
        get => InternalKCT.InternalButtonCheckedHighlightBorder;

        set 
        { 
            InternalKCT.InternalButtonCheckedHighlightBorder = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonCheckedHighlightBorder property to its default value.
    /// </summary>
    public void ResetButtonCheckedHighlightBorder() => ButtonCheckedHighlightBorder = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region ButtonPressedBorder
    /// <summary>
    /// Gets and sets the border color to use with the ButtonPressedGradientBegin, ButtonPressedGradientMiddle, and ButtonPressedGradientEnd colors.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Border color to use with the ButtonPressedGradientBegin, ButtonPressedGradientMiddle, and ButtonPressedGradientEnd colors.")]
    [KryptonDefaultColor]
    public Color ButtonPressedBorder
    {
        get => InternalKCT.InternalButtonPressedBorder;

        set 
        { 
            InternalKCT.InternalButtonPressedBorder = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonPressedBorder property to its default value.
    /// </summary>
    public void ResetButtonPressedBorder() => ButtonPressedBorder = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region ButtonPressedGradientBegin
    /// <summary>
    /// Gets and sets the starting color of the gradient used when the button is pressed.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Starting color of the gradient used when the button is pressed.")]
    [KryptonDefaultColor]
    public Color ButtonPressedGradientBegin
    {
        get => InternalKCT.InternalButtonPressedGradientBegin;

        set 
        { 
            InternalKCT.InternalButtonPressedGradientBegin = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonPressedGradientBegin property to its default value.
    /// </summary>
    public void ResetButtonPressedGradientBegin() => ButtonPressedGradientBegin = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region ButtonPressedGradientEnd
    /// <summary>
    /// Gets and sets the ending color of the gradient used when the button is pressed.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Ending color of the gradient used when the button is pressed.")]
    [KryptonDefaultColor]
    public Color ButtonPressedGradientEnd
    {
        get => InternalKCT.InternalButtonPressedGradientEnd;

        set 
        { 
            InternalKCT.InternalButtonPressedGradientEnd = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonPressedGradientEnd property to its default value.
    /// </summary>
    public void ResetButtonPressedGradientEnd() => ButtonPressedGradientEnd = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region ButtonPressedGradientMiddle
    /// <summary>
    /// Gets and sets the middle color of the gradient used when the button is pressed.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Middle color of the gradient used when the button is pressed.")]
    [KryptonDefaultColor]
    public Color ButtonPressedGradientMiddle
    {
        get => InternalKCT.InternalButtonPressedGradientMiddle;

        set 
        { 
            InternalKCT.InternalButtonPressedGradientMiddle = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonPressedGradientMiddle property to its default value.
    /// </summary>
    public void ResetButtonPressedGradientMiddle() => ButtonPressedGradientMiddle = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region ButtonPressedHighlight
    /// <summary>
    /// Gets and sets the solid color used when the button is pressed.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Solid color used when the button is pressed.")]
    [KryptonDefaultColor]
    public Color ButtonPressedHighlight
    {
        get => InternalKCT.InternalButtonPressedHighlight;

        set 
        { 
            InternalKCT.InternalButtonPressedHighlight = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonPressedHighlight property to its default value.
    /// </summary>
    public void ResetButtonPressedHighlight() => ButtonPressedHighlight = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region ButtonPressedHighlightBorder
    /// <summary>
    /// Gets and sets the border color to use with ButtonPressedHighlight.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Border color to use with ButtonPressedHighlight.")]
    [KryptonDefaultColor]
    public Color ButtonPressedHighlightBorder
    {
        get => InternalKCT.InternalButtonPressedHighlightBorder;

        set 
        { 
            InternalKCT.InternalButtonPressedHighlightBorder = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonPressedHighlightBorder property to its default value.
    /// </summary>
    public void ResetButtonPressedHighlightBorder() => ButtonPressedHighlightBorder = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region ButtonSelectedBorder
    /// <summary>
    /// Gets and sets the border color to use with the ButtonSelectedGradientBegin, ButtonSelectedGradientMiddle, and ButtonSelectedGradientEnd colors.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Border color to use with the ButtonSelectedGradientBegin, ButtonSelectedGradientMiddle, and ButtonSelectedGradientEnd colors.")]
    [KryptonDefaultColor]
    public Color ButtonSelectedBorder
    {
        get => InternalKCT.InternalButtonSelectedBorder;

        set 
        { 
            InternalKCT.InternalButtonSelectedBorder = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonSelectedBorder property to its default value.
    /// </summary>
    public void ResetButtonSelectedBorder() => ButtonSelectedBorder = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region ButtonSelectedGradientBegin
    /// <summary>
    /// Gets and sets the starting color of the gradient used when the button is selected.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Starting color of the gradient used when the button is selected.")]
    [KryptonDefaultColor]
    public Color ButtonSelectedGradientBegin
    {
        get => InternalKCT.InternalButtonSelectedGradientBegin;

        set 
        { 
            InternalKCT.InternalButtonSelectedGradientBegin = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonSelectedGradientBegin property to its default value.
    /// </summary>
    public void ResetButtonSelectedGradientBegin() => ButtonSelectedGradientBegin = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region ButtonSelectedGradientEnd
    /// <summary>
    /// Gets and sets the ending color of the gradient used when the button is selected.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Ending color of the gradient used when the button is selected.")]
    [KryptonDefaultColor]
    public Color ButtonSelectedGradientEnd
    {
        get => InternalKCT.InternalButtonSelectedGradientEnd;

        set 
        { 
            InternalKCT.InternalButtonSelectedGradientEnd = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonSelectedGradientEnd property to its default value.
    /// </summary>
    public void ResetButtonSelectedGradientEnd() => ButtonSelectedGradientEnd = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region ButtonSelectedGradientMiddle
    /// <summary>
    /// Gets and sets the middle color of the gradient used when the button is selected.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Middle color of the gradient used when the button is selected.")]
    [KryptonDefaultColor]
    public Color ButtonSelectedGradientMiddle
    {
        get => InternalKCT.InternalButtonSelectedGradientMiddle;

        set 
        { 
            InternalKCT.InternalButtonSelectedGradientMiddle = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonSelectedGradientMiddle property to its default value.
    /// </summary>
    public void ResetButtonSelectedGradientMiddle() => ButtonSelectedGradientMiddle = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region ButtonSelectedHighlight
    /// <summary>
    /// Gets and sets the solid color used when the button is selected.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Solid color used when the button is selected.")]
    [KryptonDefaultColor]
    public Color ButtonSelectedHighlight
    {
        get => InternalKCT.InternalButtonSelectedHighlight;

        set 
        { 
            InternalKCT.InternalButtonSelectedHighlight = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonSelectedHighlight property to its default value.
    /// </summary>
    public void ResetButtonSelectedHighlight() => ButtonSelectedHighlight = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region ButtonSelectedHighlightBorder
    /// <summary>
    /// Gets and sets the border color to use with ButtonSelectedHighlight.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Border color to use with ButtonSelectedHighlight.")]
    [KryptonDefaultColor]
    public Color ButtonSelectedHighlightBorder
    {
        get => InternalKCT.InternalButtonSelectedHighlightBorder;

        set 
        { 
            InternalKCT.InternalButtonSelectedHighlightBorder = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ButtonSelectedHighlightBorder property to its default value.
    /// </summary>
    public void ResetButtonSelectedHighlightBorder() => ButtonSelectedHighlightBorder = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region CheckBackground
    /// <summary>
    /// Gets and sets the solid color to use when the button is checked and gradients are being used.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Solid color to use when the button is checked and gradients are being used.")]
    [KryptonDefaultColor]
    public Color CheckBackground
    {
        get => InternalKCT.InternalCheckBackground;

        set 
        { 
            InternalKCT.InternalCheckBackground = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the CheckBackground property to its default value.
    /// </summary>
    public void ResetCheckBackground() => CheckBackground = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region CheckPressedBackground
    /// <summary>
    /// Gets and sets the solid color to use when the button is checked and selected and gradients are being used.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Solid color to use when the button is checked and selected and gradients are being used.")]
    [KryptonDefaultColor]
    public Color CheckPressedBackground
    {
        get => InternalKCT.InternalCheckPressedBackground;

        set 
        { 
            InternalKCT.InternalCheckPressedBackground = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the CheckPressedBackground property to its default value.
    /// </summary>
    public void ResetCheckPressedBackground() => CheckPressedBackground = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region CheckSelectedBackground
    /// <summary>
    /// Gets and sets the solid color to use when the button is checked and selected and gradients are being used.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Solid color to use when the button is checked and selected and gradients are being used.")]
    [KryptonDefaultColor]
    public Color CheckSelectedBackground
    {
        get => InternalKCT.InternalCheckSelectedBackground;

        set 
        { 
            InternalKCT.InternalCheckSelectedBackground = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the CheckSelectedBackground property to its default value.
    /// </summary>
    public void ResetCheckSelectedBackground() => CheckSelectedBackground = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region OverflowButtonGradientBegin
    /// <summary>
    /// Gets and sets the starting color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Starting color of the gradient used in the ToolStripOverflowButton.")]
    [KryptonDefaultColor]
    public Color OverflowButtonGradientBegin
    {
        get => InternalKCT.InternalOverflowButtonGradientBegin;

        set 
        { 
            InternalKCT.InternalOverflowButtonGradientBegin = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the OverflowButtonGradientBegin property to its default value.
    /// </summary>
    public void ResetOverflowButtonGradientBegin() => OverflowButtonGradientBegin = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region OverflowButtonGradientEnd
    /// <summary>
    /// Gets and sets the ending color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Ending color of the gradient used in the ToolStripOverflowButton.")]
    [KryptonDefaultColor]
    public Color OverflowButtonGradientEnd
    {
        get => InternalKCT.InternalOverflowButtonGradientEnd;

        set 
        { 
            InternalKCT.InternalOverflowButtonGradientEnd = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the OverflowButtonGradientEnd property to its default value.
    /// </summary>
    public void ResetOverflowButtonGradientEnd() => OverflowButtonGradientEnd = GlobalStaticVariables.EMPTY_COLOR;
    #endregion

    #region OverflowButtonGradientMiddle
    /// <summary>
    /// Gets and sets the middle color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Middle color of the gradient used in the ToolStripOverflowButton.")]
    [KryptonDefaultColor]
    public Color OverflowButtonGradientMiddle
    {
        get => InternalKCT.InternalOverflowButtonGradientMiddle;

        set 
        { 
            InternalKCT.InternalOverflowButtonGradientMiddle = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the OverflowButtonGradientMiddle property to its default value.
    /// </summary>
    public void ResetOverflowButtonGradientMiddle() => OverflowButtonGradientMiddle = GlobalStaticVariables.EMPTY_COLOR;
    #endregion
}