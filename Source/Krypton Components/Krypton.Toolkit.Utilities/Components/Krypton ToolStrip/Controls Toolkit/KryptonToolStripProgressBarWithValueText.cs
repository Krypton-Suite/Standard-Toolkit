#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.StatusStrip), ToolboxBitmap(typeof(ToolStripProgressBar)), Description(@"")]
public class KryptonToolStripProgressBarWithValueText : ToolStripProgressBar
{
    #region Instance Fields

    private readonly ProgressBarValueTextValues _values;

    // ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
    private readonly PaletteBase _palette;
    // ReSharper restore PrivateFieldCanBeConvertedToLocalVariable

    #endregion

    #region Public

    /// <summary>
    /// Gets the expandable configuration values for designer and runtime use.
    /// </summary>
    [Category("Appearance")]
    [Description("Display value and text colour settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ProgressBarValueTextValues ValueTextValues => _values;

    private bool ShouldSerializeValueTextValues() => !_values.IsDefault;

    private void ResetValueTextValues() => _values.Reset();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool DisplayValue { get => _values.DisplayValue; set => _values.DisplayValue = value; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color DisplayTextColour { get => _values.DisplayTextColour; set => _values.DisplayTextColour = value; }

    #endregion

    #region Identity

    public KryptonToolStripProgressBarWithValueText()
        : this(KryptonManager.CurrentGlobalPalette ?? throw new InvalidOperationException("No current global palette is available."))
    {
    }

    public KryptonToolStripProgressBarWithValueText(PaletteBase palette)
    {
        _values = new ProgressBarValueTextValues(this);

        _palette = palette;
        _values.DisplayTextColour = _palette.ColorTable.StatusStripText;

        Font = _palette.ColorTable.StatusStripFont;

        Control.HandleCreated += Control_HandleCreated!;
    }

    #endregion

    #region Implementation

    private void Control_HandleCreated(object sender, EventArgs e)
    {
        var s = new ProgressBarHandler((ProgressBar)Control, _values.DisplayValue, _values.DisplayTextColour);
    }

    #endregion

    #region ProgressBarHandler

    public class ProgressBarHandler : NativeWindow
    {
        #region Instance Fields

        private readonly bool _useDisplayText;

        private readonly Color _displayTextColour;

        private readonly ProgressBar _progressBar;

        #endregion

        #region Identity

        public ProgressBarHandler(ProgressBar progressBar, bool useDisplayText, Color displayTextColour)
        {
            _progressBar = progressBar;

            _useDisplayText = useDisplayText;

            _displayTextColour = displayTextColour;

            AssignHandle(_progressBar.Handle);
        }

        #endregion

        #region Protected

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (_useDisplayText)
            {
                if (m.Msg == 0xF)
                {
                    using (var g = _progressBar.CreateGraphics())
                    {
                        TextRenderer.DrawText(g, $"{_progressBar.Value}", _progressBar.Font, _progressBar.ClientRectangle, _displayTextColour);
                    }
                }
            }
        }

        #endregion
    }

    #endregion
}