#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2025 - 2026. All rights reserved.
 */
#endregion

namespace Krypton.Toolkit;

[ToolboxBitmap(typeof(KryptonThemeComboBox))]
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
public class KryptonToolStripThemeComboBox : ToolStripControlHostFixed, ISupportInitialize
{
    #region Identity

    public KryptonToolStripThemeComboBox() : base(new KryptonThemeComboBox())
    {
        AutoSize = false;
    }

    #endregion

    #region Host Control

    [RefreshProperties(RefreshProperties.All)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description(@"Access to the hosted KryptonThemeComboBox.")]
    public KryptonThemeComboBox? KryptonThemeComboBoxControl => Control as KryptonThemeComboBox;

    #endregion

    #region Proxied Properties

    [Category(@"Visuals")]
    [Description(@"The default palette mode.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public PaletteMode DefaultPalette
    {
        get => KryptonThemeComboBoxControl?.DefaultPalette ?? PaletteMode.Global;
        set
        {
            if (KryptonThemeComboBoxControl != null)
            {
                KryptonThemeComboBoxControl.DefaultPalette = value;
            }
        }
    }

    [Category(@"Layout")]
    [Description(@"Drop-down width of the hosted combo box.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int DropDownWidth
    {
        get => KryptonThemeComboBoxControl?.DropDownWidth ?? 0;
        set
        {
            if (KryptonThemeComboBoxControl != null)
            {
                KryptonThemeComboBoxControl.DropDownWidth = value;
            }
        }
    }

    #endregion

    #region ISupportInitialize

    public void BeginInit()
    {
        (KryptonThemeComboBoxControl as ISupportInitialize)?.BeginInit();
    }

    public void EndInit()
    {
        (KryptonThemeComboBoxControl as ISupportInitialize)?.EndInit();
    }

    #endregion
}
