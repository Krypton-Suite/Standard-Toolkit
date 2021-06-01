﻿#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Krypton.Toolkit;

namespace Krypton.Ribbon
{
    internal class RibbonToContent : IPaletteContent
    {
        #region Instance Fields

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the RibbonToContent class.
        /// </summary>
        /// <param name="ribbonGeneral">Source for general ribbon settings.</param>
        public RibbonToContent(PaletteRibbonGeneral ribbonGeneral)
        {
            Debug.Assert(ribbonGeneral != null);
            RibbonGeneral = ribbonGeneral;
        }
        #endregion

        #region RibbonGeneral
        /// <summary>
        /// Gets access to the ribbon general instance.
        /// </summary>
        public PaletteRibbonGeneral RibbonGeneral { get; }

        #endregion

        #region IPaletteContent
        /// <summary>
        /// Gets a value indicating if content should be drawn.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public virtual InheritBool GetContentDraw(PaletteState state)
        {
            return InheritBool.True;
        }

        /// <summary>
        /// Gets a value indicating if content should be drawn with focus indication.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public virtual InheritBool GetContentDrawFocus(PaletteState state)
        {
            return InheritBool.False;
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public virtual PaletteRelativeAlign GetContentImageH(PaletteState state)
        {
            return PaletteRelativeAlign.Center;
        }

        /// <summary>
        /// Gets the vertical relative alignment of the image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public virtual PaletteRelativeAlign GetContentImageV(PaletteState state)
        {
            return PaletteRelativeAlign.Center;
        }

        /// <summary>
        /// Gets the effect applied to drawing of the image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteImageEffect value.</returns>
        public virtual PaletteImageEffect GetContentImageEffect(PaletteState state)
        {
            return PaletteImageEffect.Normal;
        }

        /// <summary>
        /// Gets the image color to remap into another color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public virtual Color GetContentImageColorMap(PaletteState state)
        {
            return Color.Empty;
        }

        /// <summary>
        /// Gets the color to use in place of the image map color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public virtual Color GetContentImageColorTo(PaletteState state)
        {
            return Color.Empty;
        }

        /// <summary>
        /// Gets the font for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public virtual Font GetContentShortTextFont(PaletteState state)
        {
            return RibbonGeneral.GetRibbonTextFont(state);
        }

        /// <summary>
        /// Gets the font for the long text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public Font GetContentShortTextNewFont(PaletteState state)
        {
            return RibbonGeneral.GetRibbonTextFont(state);
        }

        /// <summary>
        /// Gets the rendering hint for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public virtual PaletteTextHint GetContentShortTextHint(PaletteState state)
        {
            return RibbonGeneral.GetRibbonTextHint(state);
        }

        /// <summary>
        /// Gets the prefix drawing setting for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public virtual PaletteTextHotkeyPrefix GetContentShortTextPrefix(PaletteState state)
        {
            return PaletteTextHotkeyPrefix.None;
        }

        /// <summary>
        /// Gets the flag indicating if multiline text is allowed for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public virtual InheritBool GetContentShortTextMultiLine(PaletteState state)
        {
            return InheritBool.False;
        }

        /// <summary>
        /// Gets the text trimming to use for short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        public virtual PaletteTextTrim GetContentShortTextTrim(PaletteState state)
        {
            return PaletteTextTrim.EllipsisCharacter;
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public virtual PaletteRelativeAlign GetContentShortTextH(PaletteState state)
        {
            return PaletteRelativeAlign.Center;
        }

        /// <summary>
        /// Gets the vertical relative alignment of the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public virtual PaletteRelativeAlign GetContentShortTextV(PaletteState state)
        {
            return PaletteRelativeAlign.Center;
        }

        /// <summary>
        /// Gets the horizontal relative alignment of multiline short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public virtual PaletteRelativeAlign GetContentShortTextMultiLineH(PaletteState state)
        {
            return PaletteRelativeAlign.Near;
        }

        /// <summary>
        /// Gets the first back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public virtual Color GetContentShortTextColor1(PaletteState state)
        {
            return Color.Empty;
        }

        /// <summary>
        /// Gets the second back color for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public virtual Color GetContentShortTextColor2(PaletteState state)
        {
            return Color.Empty;
        }

        /// <summary>
        /// Gets the color drawing style for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public virtual PaletteColorStyle GetContentShortTextColorStyle(PaletteState state)
        {
            return PaletteColorStyle.Solid;
        }

        /// <summary>
        /// Gets the color alignment for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public virtual PaletteRectangleAlign GetContentShortTextColorAlign(PaletteState state)
        {
            return PaletteRectangleAlign.Local;
        }

        /// <summary>
        /// Gets the color background angle for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public virtual float GetContentShortTextColorAngle(PaletteState state)
        {
            return 0f;
        }

        /// <summary>
        /// Gets a background image for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public virtual Image GetContentShortTextImage(PaletteState state)
        {
            return null;
        }

        /// <summary>
        /// Gets the background image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public virtual PaletteImageStyle GetContentShortTextImageStyle(PaletteState state)
        {
            return PaletteImageStyle.Stretch;
        }

        /// <summary>
        /// Gets the image alignment for the short text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public virtual PaletteRectangleAlign GetContentShortTextImageAlign(PaletteState state)
        {
            return PaletteRectangleAlign.Local;
        }

        /// <summary>
        /// Gets the font for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public virtual Font GetContentLongTextFont(PaletteState state)
        {
            return RibbonGeneral.GetRibbonTextFont(state);
        }

        /// <summary>
        /// Gets the font for the long text by generating a new font instance.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Font value.</returns>
        public virtual Font GetContentLongTextNewFont(PaletteState state)
        {
            return RibbonGeneral.GetRibbonTextFont(state);
        }

        /// <summary>
        /// Gets the rendering hint for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextHint value.</returns>
        public virtual PaletteTextHint GetContentLongTextHint(PaletteState state)
        {
            return RibbonGeneral.GetRibbonTextHint(state);
        }

        /// <summary>
        /// Gets the flag indicating if multiline text is allowed for long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public virtual InheritBool GetContentLongTextMultiLine(PaletteState state)
        {
            return InheritBool.False;
        }

        /// <summary>
        /// Gets the text trimming to use for long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextTrim value.</returns>
        public virtual PaletteTextTrim GetContentLongTextTrim(PaletteState state)
        {
            return PaletteTextTrim.EllipsisCharacter;
        }

        /// <summary>
        /// Gets the prefix drawing setting for long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteTextPrefix value.</returns>
        public virtual PaletteTextHotkeyPrefix GetContentLongTextPrefix(PaletteState state)
        {
            return PaletteTextHotkeyPrefix.None;
        }

        /// <summary>
        /// Gets the horizontal relative alignment of the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public virtual PaletteRelativeAlign GetContentLongTextH(PaletteState state)
        {
            return PaletteRelativeAlign.Center;
        }

        /// <summary>
        /// Gets the vertical relative alignment of the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public virtual PaletteRelativeAlign GetContentLongTextV(PaletteState state)
        {
            return PaletteRelativeAlign.Center;
        }

        /// <summary>
        /// Gets the horizontal relative alignment of multiline long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>RelativeAlignment value.</returns>
        public virtual PaletteRelativeAlign GetContentLongTextMultiLineH(PaletteState state)
        {
            return PaletteRelativeAlign.Near;
        }

        /// <summary>
        /// Gets the first back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public virtual Color GetContentLongTextColor1(PaletteState state)
        {
            return Color.Empty;
        }

        /// <summary>
        /// Gets the second back color for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public virtual Color GetContentLongTextColor2(PaletteState state)
        {
            return Color.Empty;
        }

        /// <summary>
        /// Gets the color drawing style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public virtual PaletteColorStyle GetContentLongTextColorStyle(PaletteState state)
        {
            return PaletteColorStyle.Solid;
        }

        /// <summary>
        /// Gets the color alignment for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public virtual PaletteRectangleAlign GetContentLongTextColorAlign(PaletteState state)
        {
            return PaletteRectangleAlign.Local;
        }

        /// <summary>
        /// Gets the color background angle for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public virtual float GetContentLongTextColorAngle(PaletteState state)
        {
            return 0f;
        }

        /// <summary>
        /// Gets a background image for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public virtual Image GetContentLongTextImage(PaletteState state)
        {
            return null;
        }

        /// <summary>
        /// Gets the background image style for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public virtual PaletteImageStyle GetContentLongTextImageStyle(PaletteState state)
        {
            return PaletteImageStyle.Stretch;
        }

        /// <summary>
        /// Gets the image alignment for the long text.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public virtual PaletteRectangleAlign GetContentLongTextImageAlign(PaletteState state)
        {
            return PaletteRectangleAlign.Local;
        }

        /// <summary>
        /// Gets the padding between the border and content drawing.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Padding value.</returns>
        public virtual Padding GetContentPadding(PaletteState state)
        {
            return Padding.Empty;
        }

        /// <summary>
        /// Gets the padding between adjacent content items.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Integer value.</returns>
        public virtual int GetContentAdjacentGap(PaletteState state)
        {
            return 3;
        }

        /// <summary>
        /// Gets the style appropriate for this content.
        /// </summary>
        /// <returns>Content style.</returns>
        public virtual PaletteContentStyle GetContentStyle()
        {
            return PaletteContentStyle.LabelNormalPanel;
        }
        #endregion
    }
}
