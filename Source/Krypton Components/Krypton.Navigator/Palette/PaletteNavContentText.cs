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

namespace Krypton.Navigator;

/// <summary>
/// Implement storage for palette content text details.
/// </summary>
public class PaletteNavContentText : PaletteContentText
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the PaletteNavContentText class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public PaletteNavContentText(NeedPaintHandler needPaint)
        : base(needPaint)
    {
    }
    #endregion

    #region Font
    /// <summary>
    /// Gets the font for the text.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Font? Font
    {
        get => base.Font;
        set => base.Font = value;
    }
    #endregion

    #region Hint
    /// <summary>
    /// Gets the text rendering hint for the text.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override PaletteTextHint Hint
    {
        get => base.Hint;
        set => base.Hint = value;
    }
    #endregion

    #region Color1
    /// <summary>
    /// Gets and sets the first color for the text.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color Color1
    {
        get => base.Color1;
        set => base.Color1 = value;
    }
    #endregion

    #region Color2
    /// <summary>
    /// Gets and sets the second color for the text.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color Color2
    {
        get => base.Color2;
        set => base.Color2 = value;
    }
    #endregion

    #region ColorStyle
    /// <summary>
    /// Gets and sets the color drawing style for the text.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override PaletteColorStyle ColorStyle
    {
        get => base.ColorStyle;
        set => base.ColorStyle = value;
    }
    #endregion

    #region ColorAlign
    /// <summary>
    /// Gets and set the color alignment for the text.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override PaletteRectangleAlign ColorAlign
    {
        get => base.ColorAlign;
        set => base.ColorAlign = value;
    }
    #endregion

    #region ColorAngle
    /// <summary>
    /// Gets and sets the color angle for the text.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override float ColorAngle
    {
        get => base.ColorAngle;
        set => base.ColorAngle = value;
    }
    #endregion

    #region Image
    /// <summary>
    /// Gets and sets the image for the text.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Image? Image
    {
        get => base.Image;
        set => base.Image = value;
    }
    #endregion

    #region ImageStyle
    /// <summary>
    /// Gets and sets the image style for the text.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override PaletteImageStyle ImageStyle
    {
        get => base.ImageStyle;
        set => base.ImageStyle = value;
    }
    #endregion

    #region ImageAlign
    /// <summary>
    /// Gets and set the image alignment for the text.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override PaletteRectangleAlign ImageAlign
    {
        get => base.ImageAlign;
        set => base.ImageAlign = value;
    }
    #endregion
}