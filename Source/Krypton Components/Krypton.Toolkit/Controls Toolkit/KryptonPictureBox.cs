#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2023 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

[Category(@"code"), ToolboxBitmap(typeof(PictureBox)), Description(@""), ToolboxItem(true)]
public class KryptonPictureBox : PictureBox
{
    #region Identity

    public KryptonPictureBox()
    {
        BackColor = Color.Transparent;
    }

    #endregion

    #region Removed Designer Visibility

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Color BackColor { get => base.BackColor; set => base.BackColor = value; }

    #endregion
}