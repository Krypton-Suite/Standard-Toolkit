#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Additions from: https://stackoverflow.com/questions/43138097/show-text-on-progressbar-in-statusstrip
/// </summary>
/// <seealso cref="System.Windows.Forms.ToolStripProgressBar" />
[Description(""), ToolboxBitmap(typeof(ToolStripProgressBar)), ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
public class KryptonEnhancedToolStripProgressBar : ToolStripProgressBar
{
    #region Variables
    private readonly EnhancedProgressBarValues _values;

    private readonly PaletteBase? _palette;
    #endregion

    #region Properties

    /// <summary>
    /// Gets the expandable configuration values for designer and runtime use.
    /// </summary>
    [Category("Appearance")]
    [Description("Krypton rendering, display text, and text colour settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public EnhancedProgressBarValues ProgressBarValues => _values;

    private bool ShouldSerializeProgressBarValues() => !_values.IsDefault;

    private void ResetProgressBarValues() => _values.Reset();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool UseKryptonRender { get => _values.UseKryptonRender; set => _values.UseKryptonRender = value; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool UseDisplayText { get => _values.UseDisplayText; set => _values.UseDisplayText = value; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color DisplayTextColour { get => _values.DisplayTextColour; set => _values.DisplayTextColour = value; }
    #endregion

    #region Constructor

    /// <summary>Initializes a new instance of the <see cref="KryptonEnhancedToolStripProgressBar" /> class.</summary>
    public KryptonEnhancedToolStripProgressBar()
    {
        _values = new EnhancedProgressBarValues(this);

        _palette = KryptonManager.CurrentGlobalPalette;

        if (_palette != null)
        {
            _values.DisplayTextColour = _palette.ColorTable.StatusStripText;
            Font = _palette.ColorTable.StatusStripFont;
        }

        Control.HandleCreated += Control_HandleCreated!;
    }

    #endregion

    #region Implementation

    private void Control_HandleCreated(object sender, EventArgs e)
    {
        //var s = new ProgressBarHandler((ProgressBar) Control, _values.UseDisplayText, _values.DisplayTextColour);
    }

    #endregion

    #region Overrides
    protected override void OnPaint(PaintEventArgs e)
    {
        if (_values.UseKryptonRender)
        {
            if (ToolStripManager.Renderer is KryptonProfessionalRenderer kpr)
            {
                BackColor = kpr.KCT.StatusStripGradientEnd;
            }
        }

        base.OnPaint(e);
    }
    #endregion
}