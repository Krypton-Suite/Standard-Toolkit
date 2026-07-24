#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

public class KryptonProgressStatusStrip : StatusStrip
{
    #region Variables
    private readonly ProgressStatusStripValues _values;

    private ToolStripProgressBar? _progressBar;
    #endregion

    #region Properties

    /// <summary>
    /// Gets the expandable progress-bar configuration values for designer and runtime use.
    /// </summary>
    [Category("Progress Bar")]
    [Description("Progress bar rendering and value settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ProgressStatusStripValues ProgressValues => _values;

    private bool ShouldSerializeProgressValues() => !_values.IsDefault;

    private void ResetProgressValues() => _values.Reset();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool UseAsProgressBar { get => _values.UseAsProgressBar; set => _values.UseAsProgressBar = value; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color BarColour { get => _values.BarColour; set => _values.BarColour = value; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color BarShade { get => _values.BarShade; set => _values.BarShade = value; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float CurrentValue { get => _values.CurrentValue; set => _values.CurrentValue = value; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float MaximumValue { get => _values.MaximumValue; set => _values.MaximumValue = value; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public float MinimumValue { get => _values.MinimumValue; set => _values.MinimumValue = value; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ToolStripProgressBar? ProgressBar { get => _progressBar; set => _progressBar = value; }
    #endregion

    #region Constructor
    public KryptonProgressStatusStrip()
    {
        _values = new ProgressStatusStripValues(this);

        RenderMode = ToolStripRenderMode.ManagerRenderMode;
    }
    #endregion

    #region Overrides
    protected override void OnRendererChanged(EventArgs e)
    {
        try
        {
            // Note: This is too buggy!!!
            //if (ToolStripManager.Renderer is KryptonProfessionalRenderer kpr)
            //{
            //    ProgressBar.BackColor = kpr.KCT.StatusStripGradientEnd;
            //}
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }

        base.OnRendererChanged(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        float progressPercent = _values.CurrentValue / (_values.MaximumValue - _values.MinimumValue);

        if (_values.UseAsProgressBar)
        {
            RenderMode = ToolStripRenderMode.System;

            if (progressPercent > 0)
            {
                RectangleF progressRectangle = e.Graphics.VisibleClipBounds;

                progressRectangle.Width *= progressPercent;

                LinearGradientBrush lgb = new LinearGradientBrush(progressRectangle, _values.BarColour, _values.BarShade, LinearGradientMode.Horizontal);

                e.Graphics.FillRectangle(lgb, progressRectangle);

                lgb.Dispose();
            }
        }
        else
        {
            RenderMode = ToolStripRenderMode.ManagerRenderMode;
        }

        base.OnPaint(e);
    }
    #endregion
}