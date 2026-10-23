#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public class KryptonInternalKCT : KryptonColorTable
{
    #region Instance Fields
    private KryptonColorTable _baseKCT;
    private readonly Color[] _colors;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonInternalKCT class.
    /// </summary>
    /// <param name="baseKCT">Initial base KCT to inherit values from.</param>
    /// <param name="palette">Reference to associated palette.</param>
    public KryptonInternalKCT([DisallowNull] KryptonColorTable baseKCT,
        PaletteBase palette)
        : base(palette)
    {
        Debug.Assert(baseKCT != null);

        // Remember the base used for inheriting
        _baseKCT = baseKCT!;

        // Always assume the same use of system colors
        UseSystemColors = _baseKCT.UseSystemColors;

        // Create the array for storing colors
        _colors = new Color[(int)PaletteColorIndex.Count];

        // Initialise all the colors to empty
        for (var i = 0; i < _colors.Length; i++)
        {
            _colors[i] = GlobalStaticValues.EMPTY_COLOR;
        }

        // Initialise other storage values
        InternalUseRoundedEdges = InheritBool.Inherit;
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    public bool IsDefault => InternalUseRoundedEdges == InheritBool.Inherit;

    #endregion

    #region Button
    #region ButtonCheckedGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used when the button is checked.
    /// </summary>
    public override Color ButtonCheckedGradientBegin =>
        _colors[(int)PaletteColorIndex.ButtonCheckedGradientBegin] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonCheckedGradientBegin
            : _colors[(int)PaletteColorIndex.ButtonCheckedGradientBegin];

    /// <summary>
    /// Sets and sets the internal ButtonCheckedGradientBegin value.
    /// </summary>
    public Color InternalButtonCheckedGradientBegin
    {
        get => _colors[(int)PaletteColorIndex.ButtonCheckedGradientBegin];
        set => _colors[(int)PaletteColorIndex.ButtonCheckedGradientBegin] = value;
    }
    #endregion

    #region ButtonCheckedGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used when the button is checked.
    /// </summary>
    public override Color ButtonCheckedGradientEnd =>
        _colors[(int)PaletteColorIndex.ButtonCheckedGradientEnd] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonCheckedGradientEnd
            : _colors[(int)PaletteColorIndex.ButtonCheckedGradientEnd];

    /// <summary>
    /// Sets and sets the internal ButtonCheckedGradientEnd value.
    /// </summary>
    public Color InternalButtonCheckedGradientEnd
    {
        get => _colors[(int)PaletteColorIndex.ButtonCheckedGradientEnd];
        set => _colors[(int)PaletteColorIndex.ButtonCheckedGradientEnd] = value;
    }
    #endregion

    #region ButtonCheckedGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used when the button is checked.
    /// </summary>
    public override Color ButtonCheckedGradientMiddle =>
        _colors[(int)PaletteColorIndex.ButtonCheckedGradientMiddle] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonCheckedGradientMiddle
            : _colors[(int)PaletteColorIndex.ButtonCheckedGradientMiddle];

    /// <summary>
    /// Sets and sets the internal ButtonCheckedGradientMiddle value.
    /// </summary>
    public Color InternalButtonCheckedGradientMiddle
    {
        get => _colors[(int)PaletteColorIndex.ButtonCheckedGradientMiddle];
        set => _colors[(int)PaletteColorIndex.ButtonCheckedGradientMiddle] = value;
    }
    #endregion

    #region ButtonCheckedHighlight
    /// <summary>
    /// Gets the solid color used when the button is checked.
    /// </summary>
    public override Color ButtonCheckedHighlight =>
        _colors[(int)PaletteColorIndex.ButtonCheckedHighlight] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonCheckedHighlight
            : _colors[(int)PaletteColorIndex.ButtonCheckedHighlight];

    /// <summary>
    /// Sets and sets the internal ButtonCheckedHighlight value.
    /// </summary>
    public Color InternalButtonCheckedHighlight
    {
        get => _colors[(int)PaletteColorIndex.ButtonCheckedHighlight];
        set => _colors[(int)PaletteColorIndex.ButtonCheckedHighlight] = value;
    }
    #endregion

    #region ButtonCheckedHighlightBorder
    /// <summary>
    /// Gets the border color to use with ButtonCheckedHighlight.
    /// </summary>
    public override Color ButtonCheckedHighlightBorder =>
        _colors[(int)PaletteColorIndex.ButtonCheckedHighlightBorder] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonCheckedHighlightBorder
            : _colors[(int)PaletteColorIndex.ButtonCheckedHighlightBorder];

    /// <summary>
    /// Sets and sets the internal ButtonCheckedHighlightBorder value.
    /// </summary>
    public Color InternalButtonCheckedHighlightBorder
    {
        get => _colors[(int)PaletteColorIndex.ButtonCheckedHighlightBorder];
        set => _colors[(int)PaletteColorIndex.ButtonCheckedHighlightBorder] = value;
    }
    #endregion

    #region ButtonPressedBorder
    /// <summary>
    /// Gets the border color to use with the ButtonPressedGradientBegin, ButtonPressedGradientMiddle, and ButtonPressedGradientEnd colors.
    /// </summary>
    public override Color ButtonPressedBorder =>
        _colors[(int)PaletteColorIndex.ButtonPressedBorder] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonPressedBorder
            : _colors[(int)PaletteColorIndex.ButtonPressedBorder];

    /// <summary>
    /// Sets and sets the internal ButtonPressedBorder value.
    /// </summary>
    public Color InternalButtonPressedBorder
    {
        get => _colors[(int)PaletteColorIndex.ButtonPressedBorder];
        set => _colors[(int)PaletteColorIndex.ButtonPressedBorder] = value;
    }
    #endregion

    #region ButtonPressedGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used when the button is pressed.
    /// </summary>
    public override Color ButtonPressedGradientBegin =>
        _colors[(int)PaletteColorIndex.ButtonPressedGradientBegin] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonPressedGradientBegin
            : _colors[(int)PaletteColorIndex.ButtonPressedGradientBegin];

    /// <summary>
    /// Sets and sets the internal ButtonPressedGradientBegin value.
    /// </summary>
    public Color InternalButtonPressedGradientBegin
    {
        get => _colors[(int)PaletteColorIndex.ButtonPressedGradientBegin];
        set => _colors[(int)PaletteColorIndex.ButtonPressedGradientBegin] = value;
    }
    #endregion

    #region ButtonPressedGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used when the button is pressed.
    /// </summary>
    public override Color ButtonPressedGradientEnd =>
        _colors[(int)PaletteColorIndex.ButtonPressedGradientEnd] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonPressedGradientEnd
            : _colors[(int)PaletteColorIndex.ButtonPressedGradientEnd];

    /// <summary>
    /// Sets and sets the internal ButtonPressedGradientEnd value.
    /// </summary>
    public Color InternalButtonPressedGradientEnd
    {
        get => _colors[(int)PaletteColorIndex.ButtonPressedGradientEnd];
        set => _colors[(int)PaletteColorIndex.ButtonPressedGradientEnd] = value;
    }
    #endregion

    #region ButtonPressedGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used when the button is pressed.
    /// </summary>
    public override Color ButtonPressedGradientMiddle =>
        _colors[(int)PaletteColorIndex.ButtonPressedGradientMiddle] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonPressedGradientMiddle
            : _colors[(int)PaletteColorIndex.ButtonPressedGradientMiddle];

    /// <summary>
    /// Sets and sets the internal ButtonPressedGradientMiddle value.
    /// </summary>
    public Color InternalButtonPressedGradientMiddle
    {
        get => _colors[(int)PaletteColorIndex.ButtonPressedGradientMiddle];
        set => _colors[(int)PaletteColorIndex.ButtonPressedGradientMiddle] = value;
    }
    #endregion

    #region ButtonPressedHighlight
    /// <summary>
    /// Gets the solid color used when the button is pressed.
    /// </summary>
    public override Color ButtonPressedHighlight =>
        _colors[(int)PaletteColorIndex.ButtonPressedHighlight] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonPressedHighlight
            : _colors[(int)PaletteColorIndex.ButtonPressedHighlight];

    /// <summary>
    /// Sets and sets the internal ButtonPressedHighlight value.
    /// </summary>
    public Color InternalButtonPressedHighlight
    {
        get => _colors[(int)PaletteColorIndex.ButtonPressedHighlight];
        set => _colors[(int)PaletteColorIndex.ButtonPressedHighlight] = value;
    }
    #endregion

    #region ButtonPressedHighlightBorder
    /// <summary>
    /// Gets the border color to use with ButtonPressedHighlight.
    /// </summary>
    public override Color ButtonPressedHighlightBorder =>
        _colors[(int)PaletteColorIndex.ButtonPressedHighlightBorder] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonPressedHighlightBorder
            : _colors[(int)PaletteColorIndex.ButtonPressedHighlightBorder];

    /// <summary>
    /// Sets and sets the internal ButtonPressedHighlightBorder value.
    /// </summary>
    public Color InternalButtonPressedHighlightBorder
    {
        get => _colors[(int)PaletteColorIndex.ButtonPressedHighlightBorder];
        set => _colors[(int)PaletteColorIndex.ButtonPressedHighlightBorder] = value;
    }
    #endregion

    #region ButtonSelectedBorder
    /// <summary>
    /// Gets the border color to use with the ButtonSelectedGradientBegin, ButtonSelectedGradientMiddle, and ButtonSelectedGradientEnd colors.
    /// </summary>
    public override Color ButtonSelectedBorder =>
        _colors[(int)PaletteColorIndex.ButtonSelectedBorder] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonSelectedBorder
            : _colors[(int)PaletteColorIndex.ButtonSelectedBorder];

    /// <summary>
    /// Sets and sets the internal ButtonSelectedBorder value.
    /// </summary>
    public Color InternalButtonSelectedBorder
    {
        get => _colors[(int)PaletteColorIndex.ButtonSelectedBorder];
        set => _colors[(int)PaletteColorIndex.ButtonSelectedBorder] = value;
    }
    #endregion

    #region ButtonSelectedGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used when the button is selected.
    /// </summary>
    public override Color ButtonSelectedGradientBegin =>
        _colors[(int)PaletteColorIndex.ButtonSelectedGradientBegin] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonSelectedGradientBegin
            : _colors[(int)PaletteColorIndex.ButtonSelectedGradientBegin];

    /// <summary>
    /// Sets and sets the internal ButtonSelectedGradientBegin value.
    /// </summary>
    public Color InternalButtonSelectedGradientBegin
    {
        get => _colors[(int)PaletteColorIndex.ButtonSelectedGradientBegin];
        set => _colors[(int)PaletteColorIndex.ButtonSelectedGradientBegin] = value;
    }
    #endregion

    #region ButtonSelectedGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used when the button is selected.
    /// </summary>
    public override Color ButtonSelectedGradientEnd =>
        _colors[(int)PaletteColorIndex.ButtonSelectedGradientEnd] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonSelectedGradientEnd
            : _colors[(int)PaletteColorIndex.ButtonSelectedGradientEnd];

    /// <summary>
    /// Sets and sets the internal ButtonSelectedGradientEnd value.
    /// </summary>
    public Color InternalButtonSelectedGradientEnd
    {
        get => _colors[(int)PaletteColorIndex.ButtonSelectedGradientEnd];
        set => _colors[(int)PaletteColorIndex.ButtonSelectedGradientEnd] = value;
    }
    #endregion

    #region ButtonSelectedGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used when the button is selected.
    /// </summary>
    public override Color ButtonSelectedGradientMiddle =>
        _colors[(int)PaletteColorIndex.ButtonSelectedGradientMiddle] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonSelectedGradientMiddle
            : _colors[(int)PaletteColorIndex.ButtonSelectedGradientMiddle];

    /// <summary>
    /// Sets and sets the internal ButtonSelectedGradientMiddle value.
    /// </summary>
    public Color InternalButtonSelectedGradientMiddle
    {
        get => _colors[(int)PaletteColorIndex.ButtonSelectedGradientMiddle];
        set => _colors[(int)PaletteColorIndex.ButtonSelectedGradientMiddle] = value;
    }
    #endregion

    #region ButtonSelectedHighlight
    /// <summary>
    /// Gets the solid color used when the button is selected.
    /// </summary>
    public override Color ButtonSelectedHighlight =>
        _colors[(int)PaletteColorIndex.ButtonSelectedHighlight] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonSelectedHighlight
            : _colors[(int)PaletteColorIndex.ButtonSelectedHighlight];

    /// <summary>
    /// Sets and sets the internal ButtonSelectedHighlight value.
    /// </summary>
    public Color InternalButtonSelectedHighlight
    {
        get => _colors[(int)PaletteColorIndex.ButtonSelectedHighlight];
        set => _colors[(int)PaletteColorIndex.ButtonSelectedHighlight] = value;
    }
    #endregion

    #region ButtonSelectedHighlightBorder
    /// <summary>
    /// Gets the border color to use with ButtonSelectedHighlight.
    /// </summary>
    public override Color ButtonSelectedHighlightBorder =>
        _colors[(int)PaletteColorIndex.ButtonSelectedHighlightBorder] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ButtonSelectedHighlightBorder
            : _colors[(int)PaletteColorIndex.ButtonSelectedHighlightBorder];

    /// <summary>
    /// Sets and sets the internal ButtonSelectedHighlightBorder value.
    /// </summary>
    public Color InternalButtonSelectedHighlightBorder
    {
        get => _colors[(int)PaletteColorIndex.ButtonSelectedHighlightBorder];
        set => _colors[(int)PaletteColorIndex.ButtonSelectedHighlightBorder] = value;
    }
    #endregion
    #endregion

    #region Check
    #region CheckBackground
    /// <summary>
    /// Gets the solid color to use when the button is checked and gradients are being used.
    /// </summary>
    public override Color CheckBackground =>
        _colors[(int)PaletteColorIndex.CheckBackground] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.CheckBackground
            : _colors[(int)PaletteColorIndex.CheckBackground];

    /// <summary>
    /// Sets and sets the internal CheckBackground value.
    /// </summary>
    public Color InternalCheckBackground
    {
        get => _colors[(int)PaletteColorIndex.CheckBackground];
        set => _colors[(int)PaletteColorIndex.CheckBackground] = value;
    }
    #endregion

    #region CheckPressedBackground
    /// <summary>
    /// Gets the solid color to use when the button is checked and selected and gradients are being used.
    /// </summary>
    public override Color CheckPressedBackground =>
        _colors[(int)PaletteColorIndex.CheckPressedBackground] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.CheckPressedBackground
            : _colors[(int)PaletteColorIndex.CheckPressedBackground];

    /// <summary>
    /// Sets and sets the internal CheckPressedBackground value.
    /// </summary>
    public Color InternalCheckPressedBackground
    {
        get => _colors[(int)PaletteColorIndex.CheckPressedBackground];
        set => _colors[(int)PaletteColorIndex.CheckPressedBackground] = value;
    }
    #endregion

    #region CheckSelectedBackground
    /// <summary>
    /// Gets the solid color to use when the button is checked and selected and gradients are being used.
    /// </summary>
    public override Color CheckSelectedBackground =>
        _colors[(int)PaletteColorIndex.CheckSelectedBackground] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.CheckSelectedBackground
            : _colors[(int)PaletteColorIndex.CheckSelectedBackground];

    /// <summary>
    /// Sets and sets the internal CheckSelectedBackground value.
    /// </summary>
    public Color InternalCheckSelectedBackground
    {
        get => _colors[(int)PaletteColorIndex.CheckSelectedBackground];
        set => _colors[(int)PaletteColorIndex.CheckSelectedBackground] = value;
    }
    #endregion
    #endregion

    #region Grip
    #region GripDark
    /// <summary>
    /// Gets the color to use for shadow effects on the grip (move handle).
    /// </summary>
    public override Color GripDark => _colors[(int)PaletteColorIndex.GripDark] == GlobalStaticValues.EMPTY_COLOR ? BaseKCT.GripDark : _colors[(int)PaletteColorIndex.GripDark];

    /// <summary>
    /// Sets and sets the internal GripDark value.
    /// </summary>
    public Color InternalGripDark
    {
        get => _colors[(int)PaletteColorIndex.GripDark];
        set => _colors[(int)PaletteColorIndex.GripDark] = value;
    }
    #endregion

    #region GripLight
    /// <summary>
    /// Gets the color to use for highlight effects on the grip (move handle).
    /// </summary>
    public override Color GripLight => _colors[(int)PaletteColorIndex.GripLight] == GlobalStaticValues.EMPTY_COLOR ? BaseKCT.GripLight : _colors[(int)PaletteColorIndex.GripLight];

    /// <summary>
    /// Sets and sets the internal GripLight value.
    /// </summary>
    public Color InternalGripLight
    {
        get => _colors[(int)PaletteColorIndex.GripLight];
        set => _colors[(int)PaletteColorIndex.GripLight] = value;
    }
    #endregion
    #endregion

    #region ImageMargin
    #region ImageMarginGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the image margin of a ToolStripDropDownMenu.
    /// </summary>
    public override Color ImageMarginGradientBegin =>
        _colors[(int)PaletteColorIndex.ImageMarginGradientBegin] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ImageMarginGradientBegin
            : _colors[(int)PaletteColorIndex.ImageMarginGradientBegin];

    /// <summary>
    /// Sets and sets the internal ImageMarginGradientBegin value.
    /// </summary>
    public Color InternalImageMarginGradientBegin
    {
        get => _colors[(int)PaletteColorIndex.ImageMarginGradientBegin];
        set => _colors[(int)PaletteColorIndex.ImageMarginGradientBegin] = value;
    }
    #endregion

    #region ImageMarginGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the image margin of a ToolStripDropDownMenu.
    /// </summary>
    public override Color ImageMarginGradientEnd =>
        _colors[(int)PaletteColorIndex.ImageMarginGradientEnd] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ImageMarginGradientEnd
            : _colors[(int)PaletteColorIndex.ImageMarginGradientEnd];

    /// <summary>
    /// Sets and sets the internal ImageMarginGradientEnd value.
    /// </summary>
    public Color InternalImageMarginGradientEnd
    {
        get => _colors[(int)PaletteColorIndex.ImageMarginGradientEnd];
        set => _colors[(int)PaletteColorIndex.ImageMarginGradientEnd] = value;
    }
    #endregion

    #region ImageMarginGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used in the image margin of a ToolStripDropDownMenu.
    /// </summary>
    public override Color ImageMarginGradientMiddle =>
        _colors[(int)PaletteColorIndex.ImageMarginGradientMiddle] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ImageMarginGradientMiddle
            : _colors[(int)PaletteColorIndex.ImageMarginGradientMiddle];

    /// <summary>
    /// Sets and sets the internal ImageMarginGradientMiddle value.
    /// </summary>
    public Color InternalImageMarginGradientMiddle
    {
        get => _colors[(int)PaletteColorIndex.ImageMarginGradientMiddle];
        set => _colors[(int)PaletteColorIndex.ImageMarginGradientMiddle] = value;
    }
    #endregion

    #region ImageMarginRevealedGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.
    /// </summary>
    public override Color ImageMarginRevealedGradientBegin =>
        _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientBegin] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ImageMarginRevealedGradientBegin
            : _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientBegin];

    /// <summary>
    /// Sets and sets the internal ImageMarginRevealedGradientBegin value.
    /// </summary>
    public Color InternalImageMarginRevealedGradientBegin
    {
        get => _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientBegin];
        set => _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientBegin] = value;
    }
    #endregion

    #region ImageMarginRevealedGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.
    /// </summary>
    public override Color ImageMarginRevealedGradientEnd =>
        _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientEnd] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ImageMarginRevealedGradientEnd
            : _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientEnd];

    /// <summary>
    /// Sets and sets the internal ImageMarginRevealedGradientEnd value.
    /// </summary>
    public Color InternalImageMarginRevealedGradientEnd
    {
        get => _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientEnd];
        set => _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientEnd] = value;
    }
    #endregion

    #region ImageMarginRevealedGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.
    /// </summary>
    public override Color ImageMarginRevealedGradientMiddle =>
        _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientMiddle] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ImageMarginRevealedGradientMiddle
            : _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientMiddle];

    /// <summary>
    /// Sets and sets the internal ImageMarginRevealedGradientMiddle value.
    /// </summary>
    public Color InternalImageMarginRevealedGradientMiddle
    {
        get => _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientMiddle];
        set => _colors[(int)PaletteColorIndex.ImageMarginRevealedGradientMiddle] = value;
    }
    #endregion
    #endregion

    #region Menu
    #region MenuBorder
    /// <summary>
    /// Gets the color that is the border color to use on a MenuStrip.
    /// </summary>
    public override Color MenuBorder => _colors[(int)PaletteColorIndex.MenuBorder] == GlobalStaticValues.EMPTY_COLOR ? BaseKCT.MenuBorder : _colors[(int)PaletteColorIndex.MenuBorder];

    /// <summary>
    /// Sets and sets the internal MenuBorder value.
    /// </summary>
    public Color InternalMenuBorder
    {
        get => _colors[(int)PaletteColorIndex.MenuBorder];
        set => _colors[(int)PaletteColorIndex.MenuBorder] = value;
    }
    #endregion

    #region MenuItemText
    /// <summary>
    /// Gets the color used to draw menu item text.
    /// </summary>
    public override Color MenuItemText =>
        _colors[(int)PaletteColorIndex.MenuItemText] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.MenuItemText
            : _colors[(int)PaletteColorIndex.MenuItemText];

    /// <summary>
    /// Sets and sets the internal MenuItemText value.
    /// </summary>
    public Color InternalMenuItemText
    {
        get => _colors[(int)PaletteColorIndex.MenuItemText];
        set => _colors[(int)PaletteColorIndex.MenuItemText] = value;
    }
    #endregion

    #region MenuStripFont
    /// <summary>
    /// Gets the font used to draw text on a status strip.
    /// </summary>
    public override Font MenuStripFont => InternalMenuStripFont ?? BaseKCT.MenuStripFont;

    /// <summary>
    /// Sets and sets the internal MenuStripFont value.
    /// </summary>
    [DefaultValue(null)]
    public Font? InternalMenuStripFont { get; set; }

    #endregion

    #region MenuItemBorder
    /// <summary>
    /// Gets the border color to use with a ToolStripMenuItem.
    /// </summary>
    public override Color MenuItemBorder =>
        _colors[(int)PaletteColorIndex.MenuItemBorder] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.MenuItemBorder
            : _colors[(int)PaletteColorIndex.MenuItemBorder];

    /// <summary>
    /// Sets and sets the internal MenuItemBorder value.
    /// </summary>
    public Color InternalMenuItemBorder
    {
        get => _colors[(int)PaletteColorIndex.MenuItemBorder];
        set => _colors[(int)PaletteColorIndex.MenuItemBorder] = value;
    }
    #endregion

    #region MenuItemPressedGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used when a top-level ToolStripMenuItem is pressed.
    /// </summary>
    public override Color MenuItemPressedGradientBegin =>
        _colors[(int)PaletteColorIndex.MenuItemPressedGradientBegin] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.MenuItemPressedGradientBegin
            : _colors[(int)PaletteColorIndex.MenuItemPressedGradientBegin];

    /// <summary>
    /// Sets and sets the internal MenuItemPressedGradientBegin value.
    /// </summary>
    public Color InternalMenuItemPressedGradientBegin
    {
        get => _colors[(int)PaletteColorIndex.MenuItemPressedGradientBegin];
        set => _colors[(int)PaletteColorIndex.MenuItemPressedGradientBegin] = value;
    }
    #endregion

    #region MenuItemPressedGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used when a top-level ToolStripMenuItem is pressed.
    /// </summary>
    public override Color MenuItemPressedGradientEnd =>
        _colors[(int)PaletteColorIndex.MenuItemPressedGradientEnd] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.MenuItemPressedGradientEnd
            : _colors[(int)PaletteColorIndex.MenuItemPressedGradientEnd];

    /// <summary>
    /// Sets and sets the internal MenuItemPressedGradientEnd value.
    /// </summary>
    public Color InternalMenuItemPressedGradientEnd
    {
        get => _colors[(int)PaletteColorIndex.MenuItemPressedGradientEnd];
        set => _colors[(int)PaletteColorIndex.MenuItemPressedGradientEnd] = value;
    }
    #endregion

    #region MenuItemPressedGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used when a top-level ToolStripMenuItem is pressed.
    /// </summary>
    public override Color MenuItemPressedGradientMiddle =>
        _colors[(int)PaletteColorIndex.MenuItemPressedGradientMiddle] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.MenuItemPressedGradientMiddle
            : _colors[(int)PaletteColorIndex.MenuItemPressedGradientMiddle];

    /// <summary>
    /// Sets and sets the internal MenuItemPressedGradientMiddle value.
    /// </summary>
    public Color InternalMenuItemPressedGradientMiddle
    {
        get => _colors[(int)PaletteColorIndex.MenuItemPressedGradientMiddle];
        set => _colors[(int)PaletteColorIndex.MenuItemPressedGradientMiddle] = value;
    }
    #endregion

    #region MenuItemSelected
    /// <summary>
    /// Gets the solid color to use when a ToolStripMenuItem other than the top-level ToolStripMenuItem is selected.
    /// </summary>
    public override Color MenuItemSelected =>
        _colors[(int)PaletteColorIndex.MenuItemSelected] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.MenuItemSelected
            : _colors[(int)PaletteColorIndex.MenuItemSelected];

    /// <summary>
    /// Sets and sets the internal MenuItemSelected value.
    /// </summary>
    public Color InternalMenuItemSelected
    {
        get => _colors[(int)PaletteColorIndex.MenuItemSelected];
        set => _colors[(int)PaletteColorIndex.MenuItemSelected] = value;
    }
    #endregion

    #region MenuItemSelectedGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used when the ToolStripMenuItem is selected.
    /// </summary>
    public override Color MenuItemSelectedGradientBegin =>
        _colors[(int)PaletteColorIndex.MenuItemSelectedGradientBegin] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.MenuItemSelectedGradientBegin
            : _colors[(int)PaletteColorIndex.MenuItemSelectedGradientBegin];

    /// <summary>
    /// Sets and sets the internal MenuItemSelectedGradientBegin value.
    /// </summary>
    public Color InternalMenuItemSelectedGradientBegin
    {
        get => _colors[(int)PaletteColorIndex.MenuItemSelectedGradientBegin];
        set => _colors[(int)PaletteColorIndex.MenuItemSelectedGradientBegin] = value;
    }
    #endregion

    #region MenuItemSelectedGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used when the ToolStripMenuItem is selected.
    /// </summary>
    public override Color MenuItemSelectedGradientEnd =>
        _colors[(int)PaletteColorIndex.MenuItemSelectedGradientEnd] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.MenuItemSelectedGradientEnd
            : _colors[(int)PaletteColorIndex.MenuItemSelectedGradientEnd];

    /// <summary>
    /// Sets and sets the internal MenuItemSelectedGradientEnd value.
    /// </summary>
    public Color InternalMenuItemSelectedGradientEnd
    {
        get => _colors[(int)PaletteColorIndex.MenuItemSelectedGradientEnd];
        set => _colors[(int)PaletteColorIndex.MenuItemSelectedGradientEnd] = value;
    }
    #endregion

    #region MenuStripText
    /// <summary>
    /// Gets the color used to draw text on a menu strip.
    /// </summary>
    public override Color MenuStripText
    {
        get
        {
            if (_colors.Length > (int)PaletteColorIndex.MenuStripText)
            {
                return _colors[(int)PaletteColorIndex.MenuStripText] == GlobalStaticValues.EMPTY_COLOR
                    ? BaseKCT.MenuStripText
                    : _colors[(int)PaletteColorIndex.MenuStripText];
            }
            return BaseKCT.MenuStripText;
        }
    }

    /// <summary>
    /// Sets and sets the internal MenuStripText value.
    /// </summary>
    public Color InternalMenuStripText
    {
        get => _colors.Length > (int)PaletteColorIndex.MenuStripText ? _colors[(int)PaletteColorIndex.MenuStripText] : BaseKCT.MenuStripText;
        set => _colors[(int)PaletteColorIndex.MenuStripText] = value;
    }
    #endregion

    #region MenuStripGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the MenuStrip.
    /// </summary>
    public override Color MenuStripGradientBegin =>
        _colors[(int)PaletteColorIndex.MenuStripGradientBegin] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.MenuStripGradientBegin
            : _colors[(int)PaletteColorIndex.MenuStripGradientBegin];

    /// <summary>
    /// Sets and sets the internal MenuStripGradientBegin value.
    /// </summary>
    public Color InternalMenuStripGradientBegin
    {
        get => _colors[(int)PaletteColorIndex.MenuStripGradientBegin];
        set => _colors[(int)PaletteColorIndex.MenuStripGradientBegin] = value;
    }
    #endregion

    #region MenuStripGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the MenuStrip.
    /// </summary>
    public override Color MenuStripGradientEnd =>
        _colors[(int)PaletteColorIndex.MenuStripGradientEnd] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.MenuStripGradientEnd
            : _colors[(int)PaletteColorIndex.MenuStripGradientEnd];

    /// <summary>
    /// Sets and sets the internal MenuStripGradientEnd value.
    /// </summary>
    public Color InternalMenuStripGradientEnd
    {
        get => _colors[(int)PaletteColorIndex.MenuStripGradientEnd];
        set => _colors[(int)PaletteColorIndex.MenuStripGradientEnd] = value;
    }
    #endregion
    #endregion

    #region OverflowButton
    #region OverflowButtonGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    public override Color OverflowButtonGradientBegin =>
        _colors[(int)PaletteColorIndex.OverflowButtonGradientBegin] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.OverflowButtonGradientBegin
            : _colors[(int)PaletteColorIndex.OverflowButtonGradientBegin];

    /// <summary>
    /// Sets and sets the internal OverflowButtonGradientBegin value.
    /// </summary>
    public Color InternalOverflowButtonGradientBegin
    {
        get => _colors[(int)PaletteColorIndex.OverflowButtonGradientBegin];
        set => _colors[(int)PaletteColorIndex.OverflowButtonGradientBegin] = value;
    }
    #endregion

    #region OverflowButtonGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    public override Color OverflowButtonGradientEnd =>
        _colors[(int)PaletteColorIndex.OverflowButtonGradientEnd] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.OverflowButtonGradientEnd
            : _colors[(int)PaletteColorIndex.OverflowButtonGradientEnd];

    /// <summary>
    /// Sets and sets the internal OverflowButtonGradientEnd value.
    /// </summary>
    public Color InternalOverflowButtonGradientEnd
    {
        get => _colors[(int)PaletteColorIndex.OverflowButtonGradientEnd];
        set => _colors[(int)PaletteColorIndex.OverflowButtonGradientEnd] = value;
    }
    #endregion

    #region OverflowButtonGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used in the ToolStripOverflowButton.
    /// </summary>
    public override Color OverflowButtonGradientMiddle =>
        _colors[(int)PaletteColorIndex.OverflowButtonGradientMiddle] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.OverflowButtonGradientMiddle
            : _colors[(int)PaletteColorIndex.OverflowButtonGradientMiddle];

    /// <summary>
    /// Sets and sets the internal OverflowButtonGradientMiddle value.
    /// </summary>
    public Color InternalOverflowButtonGradientMiddle
    {
        get => _colors[(int)PaletteColorIndex.OverflowButtonGradientMiddle];
        set => _colors[(int)PaletteColorIndex.OverflowButtonGradientMiddle] = value;
    }
    #endregion
    #endregion

    #region RaftingContainer
    #region RaftingContainerGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripContainer.
    /// </summary>
    public override Color RaftingContainerGradientBegin =>
        _colors[(int)PaletteColorIndex.RaftingContainerGradientBegin] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.RaftingContainerGradientBegin
            : _colors[(int)PaletteColorIndex.RaftingContainerGradientBegin];

    /// <summary>
    /// Sets and sets the internal RaftingContainerGradientBegin value.
    /// </summary>
    public Color InternalRaftingContainerGradientBegin
    {
        get => _colors[(int)PaletteColorIndex.RaftingContainerGradientBegin];
        set => _colors[(int)PaletteColorIndex.RaftingContainerGradientBegin] = value;
    }
    #endregion

    #region RaftingContainerGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripContainer.
    /// </summary>
    public override Color RaftingContainerGradientEnd =>
        _colors[(int)PaletteColorIndex.RaftingContainerGradientEnd] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.RaftingContainerGradientEnd
            : _colors[(int)PaletteColorIndex.RaftingContainerGradientEnd];

    /// <summary>
    /// Sets and sets the internal RaftingContainerGradientEnd value.
    /// </summary>
    public Color InternalRaftingContainerGradientEnd
    {
        get => _colors[(int)PaletteColorIndex.RaftingContainerGradientEnd];
        set => _colors[(int)PaletteColorIndex.RaftingContainerGradientEnd] = value;
    }
    #endregion
    #endregion

    #region Separator
    #region SeparatorDark
    /// <summary>
    /// Gets the color to use to for shadow effects on the ToolStripSeparator.
    /// </summary>
    public override Color SeparatorDark =>
        _colors[(int)PaletteColorIndex.SeparatorDark] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.SeparatorDark
            : _colors[(int)PaletteColorIndex.SeparatorDark];

    /// <summary>
    /// Sets and sets the internal SeparatorDark value.
    /// </summary>
    public Color InternalSeparatorDark
    {
        get => _colors[(int)PaletteColorIndex.SeparatorDark];
        set => _colors[(int)PaletteColorIndex.SeparatorDark] = value;
    }
    #endregion

    #region SeparatorLight
    /// <summary>
    /// Gets the color to use to for highlight effects on the ToolStripSeparator.
    /// </summary>
    public override Color SeparatorLight =>
        _colors[(int)PaletteColorIndex.SeparatorLight] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.SeparatorLight
            : _colors[(int)PaletteColorIndex.SeparatorLight];

    /// <summary>
    /// Sets and sets the internal SeparatorLight value.
    /// </summary>
    public Color InternalSeparatorLight
    {
        get => _colors[(int)PaletteColorIndex.SeparatorLight];
        set => _colors[(int)PaletteColorIndex.SeparatorLight] = value;
    }
    #endregion
    #endregion

    #region StatusStrip
    #region StatusStripText
    /// <summary>
    /// Gets the color used to draw text on a status strip.
    /// </summary>
    public override Color StatusStripText =>
        _colors[(int)PaletteColorIndex.StatusStripText] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.StatusStripText
            : _colors[(int)PaletteColorIndex.StatusStripText];

    /// <summary>
    /// Sets and sets the internal StatusStripText value.
    /// </summary>
    public Color InternalStatusStripText
    {
        get => _colors[(int)PaletteColorIndex.StatusStripText];
        set => _colors[(int)PaletteColorIndex.StatusStripText] = value;
    }
    #endregion

    #region StatusStripFont
    /// <summary>
    /// Gets the font used to draw text on a status strip.
    /// </summary>
    public override Font StatusStripFont => InternalStatusStripFont ?? BaseKCT.StatusStripFont;

    /// <summary>
    /// Sets and sets the internal StatusStripFont value.
    /// </summary>
    [DefaultValue(null)]
    public Font? InternalStatusStripFont { get; set; }

    #endregion

    #region StatusStripGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used on the StatusStrip.
    /// </summary>
    public override Color StatusStripGradientBegin =>
        _colors[(int)PaletteColorIndex.StatusStripGradientBegin] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.StatusStripGradientBegin
            : _colors[(int)PaletteColorIndex.StatusStripGradientBegin];

    /// <summary>
    /// Sets and sets the internal StatusStripGradientBegin value.
    /// </summary>
    public Color InternalStatusStripGradientBegin
    {
        get => _colors[(int)PaletteColorIndex.StatusStripGradientBegin];
        set => _colors[(int)PaletteColorIndex.StatusStripGradientBegin] = value;
    }
    #endregion

    #region StatusStripGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used on the StatusStrip.
    /// </summary>
    public override Color StatusStripGradientEnd =>
        _colors[(int)PaletteColorIndex.StatusStripGradientEnd] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.StatusStripGradientEnd
            : _colors[(int)PaletteColorIndex.StatusStripGradientEnd];

    /// <summary>
    /// Sets and sets the internal StatusStripGradientEnd value.
    /// </summary>
    public Color InternalStatusStripGradientEnd
    {
        get => _colors[(int)PaletteColorIndex.StatusStripGradientEnd];
        set => _colors[(int)PaletteColorIndex.StatusStripGradientEnd] = value;
    }
    #endregion
    #endregion

    #region ToolStrip
    #region ToolStripText
    /// <summary>
    /// Gets the color used to draw text on a tool strip.
    /// </summary>
    public override Color ToolStripText =>
        _colors[(int)PaletteColorIndex.ToolStripText] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ToolStripText
            : _colors[(int)PaletteColorIndex.ToolStripText];

    /// <summary>
    /// Sets and sets the internal ToolStripText value.
    /// </summary>
    public Color InternalToolStripText
    {
        get => _colors[(int)PaletteColorIndex.ToolStripText];
        set => _colors[(int)PaletteColorIndex.ToolStripText] = value;
    }
    #endregion

    #region ToolStripFont
    /// <summary>
    /// Gets the font used to draw text on a tool strip.
    /// </summary>
    public override Font ToolStripFont => InternalToolStripFont ?? BaseKCT.ToolStripFont;

    /// <summary>
    /// Sets and sets the internal ToolStripFont value.
    /// </summary>
    [DefaultValue(null)]
    public Font? InternalToolStripFont { get; set; }

    #endregion

    #region ToolStripBorder
    /// <summary>
    /// Gets the border color to use on the bottom edge of the ToolStrip.
    /// </summary>
    public override Color ToolStripBorder =>
        _colors[(int)PaletteColorIndex.ToolStripBorder] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ToolStripBorder
            : _colors[(int)PaletteColorIndex.ToolStripBorder];

    /// <summary>
    /// Sets and sets the internal ToolStripBorder value.
    /// </summary>
    public Color InternalToolStripBorder
    {
        get => _colors[(int)PaletteColorIndex.ToolStripBorder];
        set => _colors[(int)PaletteColorIndex.ToolStripBorder] = value;
    }
    #endregion

    #region ToolStripContentPanelGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripContentPanel.
    /// </summary>
    public override Color ToolStripContentPanelGradientBegin =>
        _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientBegin] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ToolStripContentPanelGradientBegin
            : _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientBegin];

    /// <summary>
    /// Sets and sets the internal ToolStripContentPanelGradientBegin value.
    /// </summary>
    public Color InternalToolStripContentPanelGradientBegin
    {
        get => _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientBegin];
        set => _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientBegin] = value;
    }
    #endregion

    #region ToolStripContentPanelGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripContentPanel.
    /// </summary>
    public override Color ToolStripContentPanelGradientEnd =>
        _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientEnd] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ToolStripContentPanelGradientEnd
            : _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientEnd];

    /// <summary>
    /// Sets and sets the internal ToolStripContentPanelGradientEnd value.
    /// </summary>
    public Color InternalToolStripContentPanelGradientEnd
    {
        get => _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientEnd];
        set => _colors[(int)PaletteColorIndex.ToolStripContentPanelGradientEnd] = value;
    }
    #endregion

    #region ToolStripDropDownBackground
    /// <summary>
    /// Gets the solid background color of the ToolStripDropDown.
    /// </summary>
    public override Color ToolStripDropDownBackground =>
        _colors[(int)PaletteColorIndex.ToolStripDropDownBackground] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ToolStripDropDownBackground
            : _colors[(int)PaletteColorIndex.ToolStripDropDownBackground];

    /// <summary>
    /// Sets and sets the internal ToolStripDropDownBackground value.
    /// </summary>
    public Color InternalToolStripDropDownBackground
    {
        get => _colors[(int)PaletteColorIndex.ToolStripDropDownBackground];
        set => _colors[(int)PaletteColorIndex.ToolStripDropDownBackground] = value;
    }
    #endregion

    #region ToolStripGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStrip background.
    /// </summary>
    public override Color ToolStripGradientBegin =>
        _colors[(int)PaletteColorIndex.ToolStripGradientBegin] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ToolStripGradientBegin
            : _colors[(int)PaletteColorIndex.ToolStripGradientBegin];

    /// <summary>
    /// Sets and sets the internal ToolStripGradientBegin value.
    /// </summary>
    public Color InternalToolStripGradientBegin
    {
        get => _colors[(int)PaletteColorIndex.ToolStripGradientBegin];
        set => _colors[(int)PaletteColorIndex.ToolStripGradientBegin] = value;
    }
    #endregion

    #region ToolStripGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStrip background.
    /// </summary>
    public override Color ToolStripGradientEnd =>
        _colors[(int)PaletteColorIndex.ToolStripGradientEnd] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ToolStripGradientEnd
            : _colors[(int)PaletteColorIndex.ToolStripGradientEnd];

    /// <summary>
    /// Sets and sets the internal ToolStripGradientEnd value.
    /// </summary>
    public Color InternalToolStripGradientEnd
    {
        get => _colors[(int)PaletteColorIndex.ToolStripGradientEnd];
        set => _colors[(int)PaletteColorIndex.ToolStripGradientEnd] = value;
    }
    #endregion

    #region ToolStripGradientMiddle
    /// <summary>
    /// Gets the middle color of the gradient used in the ToolStrip background.
    /// </summary>
    public override Color ToolStripGradientMiddle =>
        _colors[(int)PaletteColorIndex.ToolStripGradientMiddle] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ToolStripGradientMiddle
            : _colors[(int)PaletteColorIndex.ToolStripGradientMiddle];

    /// <summary>
    /// Sets and sets the internal ToolStripGradientMiddle value.
    /// </summary>
    public Color InternalToolStripGradientMiddle
    {
        get => _colors[(int)PaletteColorIndex.ToolStripGradientMiddle];
        set => _colors[(int)PaletteColorIndex.ToolStripGradientMiddle] = value;
    }
    #endregion

    #region ToolStripPanelGradientBegin
    /// <summary>
    /// Gets the starting color of the gradient used in the ToolStripPanel.
    /// </summary>
    public override Color ToolStripPanelGradientBegin =>
        _colors[(int)PaletteColorIndex.ToolStripPanelGradientBegin] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ToolStripPanelGradientBegin
            : _colors[(int)PaletteColorIndex.ToolStripPanelGradientBegin];

    /// <summary>
    /// Sets and sets the internal ToolStripPanelGradientBegin value.
    /// </summary>
    public Color InternalToolStripPanelGradientBegin
    {
        get => _colors[(int)PaletteColorIndex.ToolStripPanelGradientBegin];
        set => _colors[(int)PaletteColorIndex.ToolStripPanelGradientBegin] = value;
    }
    #endregion

    #region ToolStripPanelGradientEnd
    /// <summary>
    /// Gets the end color of the gradient used in the ToolStripPanel.
    /// </summary>
    public override Color ToolStripPanelGradientEnd =>
        _colors[(int)PaletteColorIndex.ToolStripPanelGradientEnd] == GlobalStaticValues.EMPTY_COLOR
            ? BaseKCT.ToolStripPanelGradientEnd
            : _colors[(int)PaletteColorIndex.ToolStripPanelGradientEnd];

    /// <summary>
    /// Sets and sets the internal ToolStripPanelGradientEnd value.
    /// </summary>
    public Color InternalToolStripPanelGradientEnd
    {
        get => _colors[(int)PaletteColorIndex.ToolStripPanelGradientEnd];
        set => _colors[(int)PaletteColorIndex.ToolStripPanelGradientEnd] = value;
    }
    #endregion
    #endregion

    #region UseRoundedEdges
    /// <summary>
    /// Gets a value indicating if rounded edges are required.
    /// </summary>
    public override InheritBool UseRoundedEdges => InternalUseRoundedEdges == InheritBool.Inherit ? BaseKCT.UseRoundedEdges : InternalUseRoundedEdges;

    /// <summary>
    /// Sets and sets the internal UseRoundedEdges value.
    /// </summary>
    public InheritBool InternalUseRoundedEdges { get; set; }

    #endregion

    #region Internal
    internal KryptonColorTable BaseKCT
    {
        get => _baseKCT;

        set
        {
            // Use the new inheritance
            _baseKCT = value;

            // Always assume the same use of system colors
            UseSystemColors = _baseKCT.UseSystemColors;
        }
    }
    #endregion
}