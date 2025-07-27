#region BSD License
/*
 *
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2017 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Provides the Black color scheme variant of the Office 2007 palette.
/// </summary>
public class PaletteOffice2007Black : PaletteOffice2007Base
{
    #region Static Fields

    #region Image Lists

    private static readonly ImageList _checkBoxList;
    private static readonly ImageList _galleryButtonList;

    #endregion

    #region Image Array

    private static readonly Image?[] _radioButtonArray;

    #endregion

    #region Images

    private static readonly Image? _blackDropDownButton = GenericImageResources.BlackDropDownButton;
    private static readonly Image _blackCloseNormal = Office2007ControlBoxResources.Office2007ControlBoxBlackCloseNormal;
    private static readonly Image _blackCloseActive = Office2007ControlBoxResources.Office2007ControlBoxBlackCloseActive;
    private static readonly Image _blackCloseDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlackCloseDisabled;
    private static readonly Image _blackClosePressed = Office2007ControlBoxResources.Office2007ControlBoxBlackClosePressed;
    private static readonly Image _blackMaximiseNormal = Office2007ControlBoxResources.Office2007ControlBoxBlackMaximiseNormal;
    private static readonly Image _blackMaximiseActive = Office2007ControlBoxResources.Office2007ControlBoxBlackMaximiseActive;
    private static readonly Image _blackMaximiseDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlackMaximiseDisabled;
    private static readonly Image _blackMaximisePressed = Office2007ControlBoxResources.Office2007ControlBoxBlackMaximisePressed;
    private static readonly Image _blackMinimiseNormal = Office2007ControlBoxResources.Office2007ControlBoxBlackMinimiseNormal;
    private static readonly Image _blackMinimiseActive = Office2007ControlBoxResources.Office2007ControlBoxBlackMinimiseActive;
    private static readonly Image _blackMinimiseDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlackMinimiseDisabled;
    private static readonly Image _blackMinimisePressed = Office2007ControlBoxResources.Office2007ControlBoxBlueMinimisePessed;
    private static readonly Image _blackRestoreNormal = Office2007ControlBoxResources.Office2007ControlBoxBlackRestoreNormal;
    private static readonly Image _blackRestoreActive = Office2007ControlBoxResources.Office2007ControlBoxBlackRestoreActive;
    private static readonly Image _blackRestoreDisabled = Office2007ControlBoxResources.Office2007ControlBoxBlackRestoreDisabled;
    private static readonly Image _blackRestorePressed = Office2007ControlBoxResources.Office2007ControlBoxBlackRestorePressed;
    private static readonly Image _blackHelpNormal = Office2007ControlBoxResources.Office2007HelpIconNormal;
    private static readonly Image _blackHelpActive = Office2007ControlBoxResources.Office2007HelpIconHover;
    private static readonly Image _blackHelpDisabled = Office2007ControlBoxResources.Office2007HelpIconDisabled;
    private static readonly Image _blackHelpPressed = Office2007ControlBoxResources.Office2007HelpIconPressed;
    private static readonly Image _blackRibbonMinimize = GenericImageResources.BlackButtonCollapse;
    private static readonly Image _blackRibbonExpand = GenericImageResources.BlackButtonExpand;
    private static readonly Image? _contextMenuSubMenu = GenericImageResources.BlackContextMenuSub;

    #endregion

    #region Colour Arrays

    #endregion

    #endregion

    #region Identity
    static PaletteOffice2007Black()
    {
        _checkBoxList = new ImageList
        {
            ImageSize = new Size(13, 13),
            ColorDepth = ColorDepth.Depth24Bit
        };
        _checkBoxList.Images.AddStrip(CheckBoxStripResources.CheckBoxStrip2007Black);
        _galleryButtonList = new ImageList
        {
            ImageSize = new Size(13, 7),
            ColorDepth = ColorDepth.Depth24Bit,
            TransparentColor = GlobalStaticValues.TRANSPARENCY_KEY_COLOR
        };
        _galleryButtonList.Images.AddStrip(GalleryImageResources.GallerySilverBlack);
        _radioButtonArray =
        [
            Office2007RadioButtonImageResources.RadioButton2007BlueD,
            Office2007RadioButtonImageResources.RadioButton2007BlackN,
            Office2007RadioButtonImageResources.RadioButton2007BlackT,
            Office2007RadioButtonImageResources.RadioButton2007BlackP,
            Office2007RadioButtonImageResources.RadioButton2007BlueDC,
            Office2007RadioButtonImageResources.RadioButton2007BlackNC,
            Office2007RadioButtonImageResources.RadioButton2007BlackTC,
            Office2007RadioButtonImageResources.RadioButton2007BlackPC
        ];
    }

    /// <summary>
    /// Initialize a new instance of the PaletteOffice2007Black class.
    /// </summary>
    public PaletteOffice2007Black()
        : base(
        "Office 2007 - Black",
        new PaletteOffice2007Black_BaseScheme(),
        _checkBoxList,
        _galleryButtonList,
        _radioButtonArray)
    {
    }
    #endregion

    #region Back
    /// <summary>
    /// Gets the color background drawing style.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color drawing style.</returns>
    public override PaletteColorStyle GetBackColorStyle(PaletteBackStyle style, PaletteState state) => style switch
    {
        PaletteBackStyle.ButtonForm => state switch
        {
            PaletteState.Tracking or PaletteState.CheckedTracking or PaletteState.Pressed or PaletteState.CheckedPressed => PaletteColorStyle.GlassBottom,
            _ => PaletteColorStyle.GlassNormalFull
        },
        PaletteBackStyle.HeaderForm => PaletteColorStyle.Rounding3,
        _ => base.GetBackColorStyle(style, state)
    };

    /// <summary>
    /// Gets the second back color.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBackColor2(PaletteBackStyle style, PaletteState state)
    {
        switch (style)
        {
            case PaletteBackStyle.TabDock:
                switch (state)
                {
                    case PaletteState.Normal:
                        return BaseColors!.HeaderPrimaryBack1;
                }
                break;
        }

        return base.GetBackColor2(style, state);
    }
    #endregion

    #region Border
    /// <summary>
    /// Gets the first border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBorderColor1(PaletteBorderStyle style, PaletteState state)
    {
        switch (style)
        {
            case PaletteBorderStyle.TabDock:
                switch (state)
                {
                    case PaletteState.Normal:
                        return BaseColors!.ControlBorder;
                }
                break;
        }

        return base.GetBorderColor1(style, state);
    }

    /// <summary>
    /// Gets the second border color.
    /// </summary>
    /// <param name="style">Border style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetBorderColor2(PaletteBorderStyle style, PaletteState state)
    {
        switch (style)
        {
            case PaletteBorderStyle.TabDock:
                switch (state)
                {
                    case PaletteState.Normal:
                        return BaseColors!.ControlBorder;
                }
                break;
        }

        return base.GetBorderColor2(style, state);
    }
    #endregion

    #region Content
    /// <summary>
    /// Gets the first back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor1(PaletteContentStyle style, PaletteState state)
    {
        if (style == PaletteContentStyle.ButtonForm)
        {
            switch (state)
            {
                case PaletteState.FocusOverride:
                case PaletteState.CheckedNormal:
                    return BaseColors!.TextButtonFormPressed;
            }
        }

        return base.GetContentShortTextColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color for the short text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentShortTextColor2(PaletteContentStyle style, PaletteState state)
    {
        if (style == PaletteContentStyle.ButtonForm)
        {
            switch (state)
            {
                case PaletteState.FocusOverride:
                case PaletteState.CheckedNormal:
                    return BaseColors!.TextButtonFormPressed;
            }
        }

        return base.GetContentShortTextColor2(style, state);
    }

    /// <summary>
    /// Gets the first back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor1(PaletteContentStyle style, PaletteState state)
    {
        if (style == PaletteContentStyle.ButtonForm)
        {
            switch (state)
            {
                case PaletteState.FocusOverride:
                case PaletteState.CheckedNormal:
                    return BaseColors!.TextButtonFormPressed;
            }
        }

        return base.GetContentLongTextColor1(style, state);
    }

    /// <summary>
    /// Gets the second back color for the long text.
    /// </summary>
    /// <param name="style">Content style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>Color value.</returns>
    public override Color GetContentLongTextColor2(PaletteContentStyle style, PaletteState state)
    {
        if (style == PaletteContentStyle.ButtonForm)
        {
            switch (state)
            {
                case PaletteState.FocusOverride:
                case PaletteState.CheckedNormal:
                    return BaseColors!.TextButtonFormPressed;
            }
        }

        return base.GetContentLongTextColor2(style, state);
    }
    #endregion

    #region Images
    /// <summary>
    /// Gets an image indicating a sub-menu on a context menu item.
    /// </summary>
    /// <returns>Appropriate image for drawing; otherwise null.</returns>
    public override Image? GetContextMenuSubMenuImage() => _contextMenuSubMenu;

    #endregion

    #region ButtonSpec
    /// <summary>
    /// Gets the image to display for the button.
    /// </summary>
    /// <param name="style">Style of button spec.</param>
    /// <param name="state">State for which image is required.</param>
    /// <returns>Image value.</returns>
    public override Image? GetButtonSpecImage(PaletteButtonSpecStyle style,
        PaletteState state) => style switch
    {
        PaletteButtonSpecStyle.FormClose => state switch
        {
            PaletteState.Disabled => _blackCloseDisabled,
            PaletteState.Tracking => _blackCloseActive,
            PaletteState.Pressed => _blackClosePressed,
            _ => _blackCloseNormal
        },
        PaletteButtonSpecStyle.FormMin => state switch
        {
            PaletteState.Disabled => _blackMinimiseDisabled,
            PaletteState.Tracking => _blackMinimiseActive,
            PaletteState.Pressed => _blackMinimisePressed,
            _ => _blackMinimiseNormal
        },
        PaletteButtonSpecStyle.FormMax => state switch
        {
            PaletteState.Disabled => _blackMaximiseDisabled,
            PaletteState.Tracking => _blackMaximiseActive,
            PaletteState.Pressed => _blackMaximisePressed,
            _ => _blackMaximiseNormal
        },
        PaletteButtonSpecStyle.FormRestore => state switch
        {
            PaletteState.Disabled => _blackRestoreDisabled,
            PaletteState.Tracking => _blackRestoreActive,
            PaletteState.Pressed => _blackRestorePressed,
            _ => _blackRestoreNormal
        },
        PaletteButtonSpecStyle.FormHelp => state switch
        {
            PaletteState.Disabled => _blackHelpDisabled,
            PaletteState.Tracking => _blackHelpActive,
            _ => _blackHelpNormal
        },
        PaletteButtonSpecStyle.RibbonMinimize => _blackRibbonMinimize,
        PaletteButtonSpecStyle.RibbonExpand => _blackRibbonExpand,
        _ => base.GetButtonSpecImage(style, state)
    };
    #endregion

    #region RibbonBack
    /// <summary>
    /// Gets the method used to draw the background of a ribbon item.
    /// </summary>
    /// <param name="style">Background style.</param>
    /// <param name="state">Palette value should be applicable to this state.</param>
    /// <returns>PaletteRibbonBackStyle value.</returns>
    public override PaletteRibbonColorStyle GetRibbonBackColorStyle(PaletteRibbonBackStyle style, PaletteState state)
    {
        switch (style)
        {
            case PaletteRibbonBackStyle.RibbonTab:
                switch (state)
                {
                    case PaletteState.Tracking:
                    case PaletteState.ContextTracking:
                        return PaletteRibbonColorStyle.RibbonTabGlowing;
                }
                break;
        }

        // Get style from the base class
        return base.GetRibbonBackColorStyle(style, state);
    }
    #endregion

    #region Tab Row Background

    /// <inheritdoc />
    public override Color GetRibbonTabRowGradientColor1(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingDark(PaletteState state) =>
        GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundGradientRaftingLight(PaletteState state) =>
        GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonTabRowBackgroundSolidColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override float GetRibbonTabRowGradientRaftingAngle(PaletteState state) => -1;

    #endregion

    #region AppButton Colors

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabBottomColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTopColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    /// <inheritdoc />
    public override Color GetRibbonFileAppTabTextColor(PaletteState state) => GlobalStaticValues.EMPTY_COLOR;

    #endregion
}