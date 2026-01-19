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
/// Represents a dialog box form that contains a <see cref="PrintPreviewControl"/> for printing from a Windows Forms application.
/// </summary>
[DefaultProperty(nameof(Document))]
[ToolboxBitmap(typeof(PrintPreviewDialog), "ToolboxBitmaps.KryptonPrintDialog.png")]
[Description("Displays a Kryptonised version of the standard PrintPreview dialog window.")]
[DesignerCategory(@"code")]
public class KryptonPrintPreviewDialog : Component, IDisposable
{
    #region Instance Fields

    private VisualPrintPreviewForm? _previewForm;
    private KryptonPrintDocument? _document;
    private bool _useAntiAlias = true;
    private Icon? _icon;
    private string _text = @"Print Preview";
    private bool _disposed;

    #endregion

    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref='KryptonPrintPreviewDialog'/> class.
    /// </summary>
    public KryptonPrintPreviewDialog()
    {
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets or sets the <see cref='KryptonPrintDocument'/> to preview.
    /// </summary>
    [DefaultValue(null)]
    [Description("The PrintDocument to preview.")]
    [Category(@"Behavior")]
    public KryptonPrintDocument? Document
    {
        get => _document;
        set
        {
            _document = value;
            _previewForm?.Document = value;
        }
    }

    /// <summary>
    /// Gets the <see cref='KryptonPrintPreviewControl'/> contained in this form.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public KryptonPrintPreviewControl? PrintPreviewControl => _previewForm?.PrintPreviewControl;

    /// <summary>
    /// Gets the underlying <see cref='PrintPreviewControl'/> for compatibility.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PrintPreviewControl? PrintPreviewControlBase => _previewForm?.PrintPreviewControlBase;

    /// <summary>
    /// Gets or sets a value indicating whether printing uses anti-aliasing.
    /// </summary>
    [DefaultValue(true)]
    [Description("Indicates whether printing uses anti-aliasing.")]
    [Category(@"Behavior")]
    public bool UseAntiAlias
    {
        get => _useAntiAlias;
        set
        {
            _useAntiAlias = value;
            _previewForm?.UseAntiAlias = value;
        }
    }

    /// <summary>
    /// Gets or sets the icon for the form.
    /// </summary>
    [DefaultValue(null)]
    [Description("The icon for the form.")]
    [Category(@"Appearance")]
    public Icon? Icon
    {
        get => _icon;
        set
        {
            _icon = value;
            _previewForm?.Icon = value;
        }
    }

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    [DefaultValue(@"Print Preview")]
    [Description("The text associated with this control.")]
    [Category(@"Appearance")]
    [Localizable(true)]
    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            _previewForm?.Text = value;
        }
    }

    /// <summary>
    /// Gets or sets the form's window state.
    /// </summary>
    [DefaultValue(FormWindowState.Normal)]
    [Description("The form's window state.")]
    [Category(@"Window Style")]
    public FormWindowState WindowState { get; set; } = FormWindowState.Normal;

    #endregion

    #region Public Methods

    /// <summary>
    /// Runs a print preview dialog box.
    /// </summary>
    /// <returns>One of the <see cref="DialogResult"/> values.</returns>
    public DialogResult ShowDialog()
    {
        return ShowDialog(null);
    }

    /// <summary>
    /// Runs a print preview dialog box with the specified owner.
    /// </summary>
    /// <param name="owner">Any object that implements <see cref="IWin32Window"/> that represents the top-level window that will own the modal dialog box.</param>
    /// <returns>One of the <see cref="DialogResult"/> values.</returns>
    public DialogResult ShowDialog(IWin32Window? owner)
    {
        if (_document == null)
        {
            throw new ArgumentNullException(nameof(Document), @"Document must be set before showing the dialog.");
        }

        _previewForm?.Dispose();
        _previewForm = new VisualPrintPreviewForm
        {
            Document = _document,
            UseAntiAlias = _useAntiAlias,
            Icon = _icon,
            Text = _text,
            WindowState = WindowState
        };

        return _previewForm.ShowDialog(owner);
    }

    #endregion

    #region Disposal

    /// <inheritdoc />
    protected override void Dispose(bool isDisposing)
    {
        if (!_disposed)
        {
            if (isDisposing)
            {
                _previewForm?.Dispose();
            }

            _disposed = true;
        }
    }

    ~KryptonPrintPreviewDialog() => Dispose(false);

    public new void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    #endregion
}
