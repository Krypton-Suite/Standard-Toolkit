#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, Lesandro, tobitege et al. 2026 - 2026. All rights reserved. 
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Internal form class for the print preview dialog.
/// </summary>
internal partial class VisualPrintPreviewForm : KryptonForm
{
    #region Instance Fields

    private KryptonPrintDocument? _document;
    private bool _useAntiAlias = true;
    private bool _disposed;

    #endregion

    #region Identity

    public VisualPrintPreviewForm()
    {
        InitializeComponent();
        SetupToolbar();
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the PrintDocument to preview.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonPrintDocument? Document
    {
        get => _document;
        set
        {
            _document = value;
            _previewControl.Document = value;
            UpdatePageInfo();
        }
    }

    /// <summary>
    /// Gets the KryptonPrintPreviewControl contained in this form.
    /// </summary>
    public KryptonPrintPreviewControl PrintPreviewControl => _previewControl;

    /// <summary>
    /// Gets the underlying PrintPreviewControl for compatibility.
    /// </summary>
    public PrintPreviewControl PrintPreviewControlBase => _previewControl.PrintPreviewControl;

    /// <summary>
    /// Gets or sets whether to use anti-aliasing.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool UseAntiAlias
    {
        get => _useAntiAlias;
        set
        {
            _useAntiAlias = value;
            _previewControl.UseAntiAlias = value;
        }
    }

    #endregion

    #region Private Methods

    private void SetupToolbar()
    {
        // Ensure proper spacing and layout
        _toolbarPanel.Padding = new Padding(4);

        // Initialize zoom trackbar with current zoom value
        UpdateZoomTrackBar();
    }

    private void UpdateZoomTrackBar()
    {
        // Map zoom (double, e.g., 0.25-5.0) to trackbar value (int, e.g., 25-500)
        int trackValue = (int)(_previewControl.Zoom * 100);
        trackValue = Math.Max(_zoomTrackBar.Minimum, Math.Min(_zoomTrackBar.Maximum, trackValue));
        _zoomTrackBar.Value = trackValue;
    }

    private void BtnPrint_Click(object? sender, EventArgs e)
    {
        if (_document != null)
        {
            using var printDialog = new KryptonPrintDialog
            {
                Document = _document
            };
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                _document.Print();
            }
        }
    }

    private void BtnZoomIn_Click(object? sender, EventArgs e)
    {
        _previewControl.Zoom += 0.25;
        UpdateZoomTrackBar();
    }

    private void BtnZoomOut_Click(object? sender, EventArgs e)
    {
        if (_previewControl.Zoom > 0.25)
        {
            _previewControl.Zoom -= 0.25;
            UpdateZoomTrackBar();
        }
    }

    private void ZoomTrackBar_ValueChanged(object? sender, EventArgs e)
    {
        // Map trackbar value (int, e.g., 25-500) to zoom (double, e.g., 0.25-5.0)
        double zoom = _zoomTrackBar.Value / 100.0;
        _previewControl.Zoom = zoom;

        klblZoomFactor.Text = $"{_zoomTrackBar.Value}%";
    }

    private void BtnOnePage_Click(object? sender, EventArgs e)
    {
        _previewControl.Rows = 1;
        _previewControl.Columns = 1;
    }

    private void BtnTwoPages_Click(object? sender, EventArgs e)
    {
        _previewControl.Rows = 1;
        _previewControl.Columns = 2;
    }

    private void BtnThreePages_Click(object? sender, EventArgs e)
    {
        _previewControl.Rows = 1;
        _previewControl.Columns = 3;
    }

    private void BtnFourPages_Click(object? sender, EventArgs e)
    {
        _previewControl.Rows = 2;
        _previewControl.Columns = 2;
    }

    private void BtnSixPages_Click(object? sender, EventArgs e)
    {
        _previewControl.Rows = 2;
        _previewControl.Columns = 3;
    }

    private void BtnClose_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void UpdatePageInfo()
    {
        if (_document != null && _previewControl.Document != null)
        {
            // Note: PrintPreviewControl doesn't expose page count directly
            // This is a simplified implementation
            _lblPageInfo.Text = @"Preview";
        }
    }

    #endregion

    #region Disposal

    private new void Dispose(bool isDisposing)
    {
        if (!_disposed)
        {
            if (isDisposing)
            {
                _document?.Dispose();
            }

            _disposed = true;
        }
    }

    ~VisualPrintPreviewForm() => Dispose(false);

    public new void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    #endregion
}
