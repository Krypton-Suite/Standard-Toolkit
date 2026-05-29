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

public class KryptonPaletteCueHintText : Storage
{
    #region Identity

    public KryptonPaletteCueHintText(NeedPaintHandler needPaint)
    {
        NeedPaint = needPaint;

        CueColor = GlobalStaticValues.EMPTY_COLOR;

        CueFont = null;
    }

    #endregion
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (CueColor == GlobalStaticValues.EMPTY_COLOR) && (CueFont == null);

    #region Colour

    public Color CueColor { get; set; }

    public void ResetCueColor() => CueColor = GlobalStaticValues.EMPTY_COLOR;

    #endregion

    #region Font

    public Font? CueFont { get; set; }

    public void ResetCueFont() => CueFont = null;

    #endregion
}