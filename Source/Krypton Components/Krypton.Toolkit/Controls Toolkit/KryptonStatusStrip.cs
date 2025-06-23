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

[ToolboxBitmap(typeof(StatusStrip)), Description(@"A Krypton based status strip."), ToolboxItem(true)]
public class KryptonStatusStrip : StatusStrip
{
    #region Properties
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ToolStripProgressBar[] ProgressBars { get; set; }
    #endregion

    #region Constructor
    public KryptonStatusStrip() =>
        // Use Krypton
        RenderMode = ToolStripRenderMode.ManagerRenderMode;

    #endregion

    #region Overrides
    protected override void OnRendererChanged(EventArgs e)
    {
        if (ToolStripManager.Renderer is KryptonProfessionalRenderer kpr)
        {
            if (ProgressBars != null)
            {
                foreach (ToolStripProgressBar progressBar in ProgressBars)
                {
                    progressBar.BackColor = kpr.KCT.StatusStripGradientEnd;
                }
            }
        }

        base.OnRendererChanged(e);
    }
    #endregion
}