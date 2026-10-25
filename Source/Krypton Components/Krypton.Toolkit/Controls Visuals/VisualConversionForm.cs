#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

internal partial class VisualConversionForm : KryptonForm
{
    #region Instance Fields

    private Timer _progressReportingTimer;

    #endregion

    #region Properties

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool OpenConversionLogOnCompletion { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool OpenOutputPathOnCompletion { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal BackgroundWorker? ConversionWorker { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal string ConversionLogFilePath { get; set; } = string.Empty;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal string OutputPath { get; set; } = string.Empty;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal KryptonProgressBar ConversionProgressBar => kpbConversionProgress;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal KryptonWrapLabel ConversionLog => kwlMessage;

    #endregion

    #region Identity

    public VisualConversionForm()
    {
        InitializeComponent();
    }

    #endregion

    private void kbtnCancel_Click(object sender, EventArgs e)
    {
        if (ConversionWorker != null)
        {
            try
            {
                ConversionWorker.CancelAsync();
            }
            catch (Exception exception)
            {
                KryptonExceptionHandler.CaptureException(exception);
            }
        }
    }

    private void VisualConversionForm_Load(object sender, EventArgs e)
    {
        _progressReportingTimer = new Timer
        {
            Interval = 1000
        };

        _progressReportingTimer.Tick += ProgressReportingTimer_Tick!;
    }

    private void ProgressReportingTimer_Tick(object sender, EventArgs e)
    {
        if (kpbConversionProgress.Value == kpbConversionProgress.Maximum)
        {
            if (OpenConversionLogOnCompletion)
            {
                try
                {
                    if (File.Exists(ConversionLogFilePath))
                    {
                        Process.Start(@"notepad.exe", ConversionLogFilePath);
                    }
                }
                catch (Exception exception)
                {
                    KryptonExceptionHandler.CaptureException(exception);
                }
            }
        }
    }

    private void VisualConversionForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (ConversionWorker != null && ConversionWorker.IsBusy)
        {
            try
            {
                ConversionWorker.CancelAsync();
            }
            catch (Exception exception)
            {
                KryptonExceptionHandler.CaptureException(exception);
            }
        }
    }
}