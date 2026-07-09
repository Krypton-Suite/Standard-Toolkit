#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal partial class KryptonDesignerImageIndexDropDown
{
    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _listBox = new KryptonListBox
        {
            Dock = DockStyle.Fill
        };

        BorderStyle = BorderStyle.FixedSingle;
        Controls.Add(_listBox);
        Name = nameof(KryptonDesignerImageIndexDropDown);
    }

    #endregion

    private KryptonListBox _listBox;
}
