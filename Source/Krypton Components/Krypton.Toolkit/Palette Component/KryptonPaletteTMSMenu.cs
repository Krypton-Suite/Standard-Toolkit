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
/// Storage for menu entries of the professional color table.
/// </summary>
public class KryptonPaletteTMSMenu : KryptonPaletteTMSBase
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonPaletteKCTMenu class.
    /// </summary>
    /// <param name="internalKCT">Reference to inherited values.</param>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    internal KryptonPaletteTMSMenu(KryptonInternalKCT internalKCT,
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
    public override bool IsDefault => (InternalKCT.InternalImageMarginGradientBegin == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalImageMarginGradientEnd == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalImageMarginGradientMiddle == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalImageMarginRevealedGradientBegin == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalImageMarginRevealedGradientEnd == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalImageMarginRevealedGradientMiddle == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalMenuBorder == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalMenuItemText == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalMenuItemBorder == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalMenuItemPressedGradientBegin == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalMenuItemPressedGradientEnd == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalMenuItemPressedGradientMiddle == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalMenuItemSelected == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalMenuItemSelectedGradientBegin == GlobalStaticValues.EMPTY_COLOR) &&
                                      (InternalKCT.InternalMenuItemSelectedGradientEnd == GlobalStaticValues.EMPTY_COLOR);

    #endregion

    #region PopulateFromBase
    /// <summary>
    /// Populate values from the base palette.
    /// </summary>
    public void PopulateFromBase()
    {
        ImageMarginGradientBegin = InternalKCT.ImageMarginGradientBegin;
        ImageMarginGradientEnd = InternalKCT.ImageMarginGradientEnd;
        ImageMarginGradientMiddle = InternalKCT.ImageMarginGradientMiddle;
        ImageMarginRevealedGradientBegin = InternalKCT.ImageMarginRevealedGradientBegin;
        ImageMarginRevealedGradientEnd = InternalKCT.ImageMarginRevealedGradientEnd;
        ImageMarginRevealedGradientMiddle = InternalKCT.ImageMarginRevealedGradientMiddle;
        MenuBorder = InternalKCT.MenuBorder;
        MenuItemText = InternalKCT.MenuItemText;
        MenuItemBorder = InternalKCT.MenuItemBorder;
        MenuItemPressedGradientBegin = InternalKCT.MenuItemPressedGradientBegin;
        MenuItemPressedGradientEnd = InternalKCT.MenuItemPressedGradientEnd;
        MenuItemPressedGradientMiddle = InternalKCT.MenuItemPressedGradientMiddle;
        MenuItemSelected = InternalKCT.MenuItemSelected;
        MenuItemSelectedGradientBegin = InternalKCT.MenuItemSelectedGradientBegin;
        MenuItemSelectedGradientEnd = InternalKCT.MenuItemSelectedGradientEnd;
    }
    #endregion

    #region ImageMarginGradientBegin
    /// <summary>
    /// Gets and sets the starting color of the gradient used in the image margin of a ToolStripDropDownMenu.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Staring color of the gradient used in the image margin of a ToolStripDropDownMenu.")]
    [KryptonDefaultColor]
    public Color ImageMarginGradientBegin
    {
        get => InternalKCT.InternalImageMarginGradientBegin;

        set
        {
            InternalKCT.InternalImageMarginGradientBegin = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// Resets the ImageMarginGradientBegin property to its default value.
    /// </summary>
    public void ResetImageMarginGradientBegin() => ImageMarginGradientBegin = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region ImageMarginGradientEnd
    /// <summary>
    /// Gets and sets the ending color of the gradient used in the image margin of a ToolStripDropDownMenu.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Ending color of the gradient used in the image margin of a ToolStripDropDownMenu.")]
    [KryptonDefaultColor]
    public Color ImageMarginGradientEnd
    {
        get => InternalKCT.InternalImageMarginGradientEnd;

        set
        {
            InternalKCT.InternalImageMarginGradientEnd = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the ImageMarginGradientEnd property to its default value.
    /// </summary>
    public void ResetImageMarginGradientEnd() => ImageMarginGradientEnd = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region ImageMarginGradientMiddle
    /// <summary>
    /// Gets and sets the middle color color of the gradient used in the image margin of a ToolStripDropDownMenu.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Middle color color of the gradient used in the image margin of a ToolStripDropDownMenu.")]
    [KryptonDefaultColor]
    public Color ImageMarginGradientMiddle
    {
        get => InternalKCT.InternalImageMarginGradientMiddle;

        set
        {
            InternalKCT.InternalImageMarginGradientMiddle = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the ImageMarginGradientMiddle property to its default value.
    /// </summary>
    public void ResetImageMarginGradientMiddle() => ImageMarginGradientMiddle = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region ImageMarginRevealedGradientBegin
    /// <summary>
    /// Gets and sets the starting color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Starting color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.")]
    [KryptonDefaultColor]
    public Color ImageMarginRevealedGradientBegin
    {
        get => InternalKCT.InternalImageMarginRevealedGradientBegin;

        set
        {
            InternalKCT.InternalImageMarginRevealedGradientBegin = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the ImageMarginRevealedGradientBegin property to its default value.
    /// </summary>
    public void ResetImageMarginRevealedGradientBegin() => ImageMarginRevealedGradientBegin = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region ImageMarginRevealedGradientEnd
    /// <summary>
    /// Gets and sets the ending color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Ending color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.")]
    [KryptonDefaultColor]
    public Color ImageMarginRevealedGradientEnd
    {
        get => InternalKCT.InternalImageMarginRevealedGradientEnd;

        set
        {
            InternalKCT.InternalImageMarginRevealedGradientEnd = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the ImageMarginRevealedGradientEnd property to its default value.
    /// </summary>
    public void ResetImageMarginRevealedGradientEnd() => ImageMarginRevealedGradientEnd = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region ImageMarginRevealedGradientMiddle
    /// <summary>
    /// Gets and sets the middle color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Middle color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.")]
    [KryptonDefaultColor]
    public Color ImageMarginRevealedGradientMiddle
    {
        get => InternalKCT.InternalImageMarginRevealedGradientMiddle;

        set
        {
            InternalKCT.InternalImageMarginRevealedGradientMiddle = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the ImageMarginRevealedGradientMiddle property to its default value.
    /// </summary>
    public void ResetImageMarginRevealedGradientMiddle() => ImageMarginRevealedGradientMiddle = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region MenuBorder
    /// <summary>
    /// Gets and sets the color that is the border color to use on a MenuStrip.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Color that is the border color to use on a MenuStrip.")]
    [KryptonDefaultColor]
    public Color MenuBorder
    {
        get => InternalKCT.InternalMenuBorder;

        set
        {
            InternalKCT.InternalMenuBorder = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the MenuBorder property to its default value.
    /// </summary>
    public void ResetMenuBorder() => MenuBorder = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region MenuItemText
    /// <summary>
    /// Gets and sets the color to draw text for individual menu items.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Color to draw text for individual a ToolStripMenuItem.")]
    [KryptonDefaultColor]
    public Color MenuItemText
    {
        get => InternalKCT.InternalMenuItemText;

        set
        {
            InternalKCT.InternalMenuItemText = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the MenuItemText property to its default value.
    /// </summary>
    public void ResetMenuItemText() => MenuItemText = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region MenuItemBorder
    /// <summary>
    /// Gets and sets the border color to use with a ToolStripMenuItem.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Border color to use with a ToolStripMenuItem.")]
    [KryptonDefaultColor]
    public Color MenuItemBorder
    {
        get => InternalKCT.InternalMenuItemBorder;

        set
        {
            InternalKCT.InternalMenuItemBorder = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the MenuItemBorder property to its default value.
    /// </summary>
    public void ResetMenuItemBorder() => MenuItemBorder = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region MenuItemPressedGradientBegin
    /// <summary>
    /// Gets and sets the starting color of the gradient used when a top-level ToolStripMenuItem is pressed.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Starting color of the gradient used when a top-level ToolStripMenuItem is pressed.")]
    [KryptonDefaultColor]
    public Color MenuItemPressedGradientBegin
    {
        get => InternalKCT.InternalMenuItemPressedGradientBegin;

        set
        {
            InternalKCT.InternalMenuItemPressedGradientBegin = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the MenuItemPressedGradientBegin property to its default value.
    /// </summary>
    public void ResetMenuItemPressedGradientBegin() => MenuItemPressedGradientBegin = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region MenuItemPressedGradientEnd
    /// <summary>
    /// Gets and sets the ending color of the gradient used when a top-level ToolStripMenuItem is pressed.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Ending color of the gradient used when a top-level ToolStripMenuItem is pressed.")]
    [KryptonDefaultColor]
    public Color MenuItemPressedGradientEnd
    {
        get => InternalKCT.InternalMenuItemPressedGradientEnd;

        set
        {
            InternalKCT.InternalMenuItemPressedGradientEnd = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the MenuItemPressedGradientEnd property to its default value.
    /// </summary>
    public void ResetMenuItemPressedGradientEnd() => MenuItemPressedGradientEnd = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region MenuItemPressedGradientMiddle
    /// <summary>
    /// Gets and sets the middle color of the gradient used when a top-level ToolStripMenuItem is pressed.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Middle color of the gradient used when a top-level ToolStripMenuItem is pressed.")]
    [KryptonDefaultColor]
    public Color MenuItemPressedGradientMiddle
    {
        get => InternalKCT.InternalMenuItemPressedGradientMiddle;

        set
        {
            InternalKCT.InternalMenuItemPressedGradientMiddle = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the MenuItemPressedGradientMiddle property to its default value.
    /// </summary>
    public void ResetMenuItemPressedGradientMiddle() => MenuItemPressedGradientMiddle = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region MenuItemSelected
    /// <summary>
    /// Gets and sets the solid color to use when a ToolStripMenuItem other than the top-level ToolStripMenuItem is selected.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Solid color to use when a ToolStripMenuItem other than the top-level ToolStripMenuItem is selected.")]
    [KryptonDefaultColor]
    public Color MenuItemSelected
    {
        get => InternalKCT.InternalMenuItemSelected;

        set
        {
            InternalKCT.InternalMenuItemSelected = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the MenuItemSelected property to its default value.
    /// </summary>
    public void ResetMenuItemSelected() => MenuItemSelected = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region MenuItemSelectedGradientBegin
    /// <summary>
    /// Gets and sets the starting color of the gradient used when the ToolStripMenuItem is selected.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Starting color of the gradient used when the ToolStripMenuItem is selected.")]
    [KryptonDefaultColor]
    public Color MenuItemSelectedGradientBegin
    {
        get => InternalKCT.InternalMenuItemSelectedGradientBegin;

        set
        {
            InternalKCT.InternalMenuItemSelectedGradientBegin = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the MenuItemSelectedGradientBegin property to its default value.
    /// </summary>
    public void ResetMenuItemSelectedGradientBegin() => MenuItemSelectedGradientBegin = GlobalStaticValues.EMPTY_COLOR;
    #endregion

    #region MenuItemSelectedGradientEnd
    /// <summary>
    /// Gets and sets the ending color of the gradient used when the ToolStripMenuItem is selected.
    /// </summary>
    [KryptonPersist(false)]
    [Category(@"ToolMenuStatus")]
    [Description(@"Ending color of the gradient used when the ToolStripMenuItem is selected.")]
    [KryptonDefaultColor]
    public Color MenuItemSelectedGradientEnd
    {
        get => InternalKCT.InternalMenuItemSelectedGradientEnd;

        set
        {
            InternalKCT.InternalMenuItemSelectedGradientEnd = value;
            PerformNeedPaint(false);
        }
    }

    /// <summary>
    /// esets the MenuItemSelectedGradientEnd property to its default value.
    /// </summary>
    public void ResetMenuItemSelectedGradientEnd() => MenuItemSelectedGradientEnd = GlobalStaticValues.EMPTY_COLOR;
    #endregion
}