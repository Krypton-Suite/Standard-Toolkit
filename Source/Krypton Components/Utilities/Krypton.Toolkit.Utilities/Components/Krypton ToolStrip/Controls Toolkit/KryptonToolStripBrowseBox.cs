#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

[ToolboxBitmap(typeof(KryptonTextBox)), ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ContextMenuStrip | ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ToolStrip)]
public class KryptonToolStripBrowseBox : KryptonToolStripControlHostFixed
{
    #region Public

    [Category("Control")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public InternalBrowseBox? KryptonBrowseBox => Control as InternalBrowseBox;

    #endregion

    #region Identity

    public KryptonToolStripBrowseBox() : base(new InternalBrowseBox())
    {

    }

    #endregion
}

#region Class: InternalBrowseBox

[ToolboxItem(false), EditorBrowsable(EditorBrowsableState.Never)]
public class InternalBrowseBox : KryptonTextBox
{
    #region Instance Fields

    private readonly BrowseBoxValues _values;

    private ButtonSpecAny _bsaBrowse;

    private ButtonSpecAny _bsaReset;

    private KryptonCommand _kcBrowse;

    private KryptonCommand _kcReset;

    #endregion

    #region Public

    /// <summary>
    /// Gets the expandable browse/reset configuration values for designer and runtime use.
    /// </summary>
    [Category("Behavior")]
    [Description("Browse dialog and reset button settings.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public BrowseBoxValues BrowseValues => _values;

    private bool ShouldSerializeBrowseValues() => !_values.IsDefault;

    private void ResetBrowseValues() => _values.Reset();

    /// <summary>Gets or sets a value indicating whether [use save dialog].</summary>
    /// <value><c>true</c> if [use save dialog]; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool UseSaveDialog { get => _values.UseSaveDialog; set => _values.UseSaveDialog = value; }

    /// <summary>Gets or sets a value indicating whether to show the reset button.</summary>
    /// <value><c>true</c> if [show reset button]; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowResetButton { get => _values.ShowResetButton; set => _values.ShowResetButton = value; }

    /// <summary>Gets or sets a value indicating whether the open dialog is a folder picker.</summary>
    /// <value><c>true</c> if the open dialog is a folder picker; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsFolderPicker { get => _values.IsFolderPicker; set => _values.IsFolderPicker = value; }

    /// <summary>Gets or sets the file dialog filter string (WinForms filter format).</summary>
    /// <value>The file dialog filter.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? FileDialogFilter { get => _values.FileDialogFilter; set => _values.FileDialogFilter = value; }

    /// <summary>Gets or sets the initial directory.</summary>
    /// <value>The initial directory.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? InitialDirectory { get => _values.InitialDirectory; set => _values.InitialDirectory = value; }

    /// <summary>Gets or sets the reset text.</summary>
    /// <value>The reset text.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new string ResetText { get => _values.ResetText; set => _values.ResetText = value; }

    /// <summary>Gets or sets the reset text tool tip heading.</summary>
    /// <value>The reset text tool tip heading.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ResetTextToolTipHeading { get => _values.ResetTextToolTipHeading; set => _values.ResetTextToolTipHeading = value; }

    /// <summary>Gets or sets the reset text tool tip description.</summary>
    /// <value>The reset text tool tip description.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ResetTextToolTipDescription { get => _values.ResetTextToolTipDescription; set => _values.ResetTextToolTipDescription = value; }

    /// <summary>Gets or sets the small reset image.</summary>
    /// <value>The small reset image.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Image? SmallResetImage { get => _values.SmallResetImage; set => _values.SmallResetImage = value; }

    /// <summary>Gets or sets the large reset image.</summary>
    /// <value>The large reset image.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Image? LargeResetImage { get => _values.LargeResetImage; set => _values.LargeResetImage = value; }

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="InternalBrowseBox" /> class.</summary>
    public InternalBrowseBox()
    {
        _values = new BrowseBoxValues(this);

        _bsaBrowse = new ButtonSpecAny();

        _bsaReset = new ButtonSpecAny();

        _kcBrowse = new KryptonCommand();

        _kcReset = new KryptonCommand();

        _bsaBrowse.Text = @"...";

        _bsaBrowse.KryptonCommand = _kcBrowse;

        _kcBrowse.Text = @"...";

        _bsaReset.ToolTipImage = _values.SmallResetImage;

        _bsaReset.ToolTipBody = _values.ResetTextToolTipDescription;

        _bsaReset.ToolTipTitle = _values.ResetTextToolTipHeading;

        _bsaReset.Text = _values.ResetText;

        _bsaReset.Image = _values.SmallResetImage;

        _bsaReset.KryptonCommand = _kcReset;

        _kcReset.Text = _values.ResetText;

        _bsaReset.Enabled = ButtonEnabled.False;

        _kcBrowse.Execute += Browse_Execute;

        _kcReset.ImageLarge = _values.LargeResetImage;

        _kcReset.ImageSmall = _values.SmallResetImage;

        _kcReset.Execute += Reset_Execute;

        ButtonSpecs.AddRange(new[] { _bsaBrowse, _bsaReset });
    }

    #endregion

    #region Implementation

    /// <summary>Handles the Execute event of the Browse control.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void Browse_Execute(object? sender, EventArgs e)
    {
        if (_values is { IsFolderPicker: true, UseSaveDialog: false })
        {
            using KryptonFolderBrowserDialog folderDialog = new KryptonFolderBrowserDialog();

            if (!string.IsNullOrEmpty(_values.InitialDirectory))
            {
                folderDialog.SelectedPath = _values.InitialDirectory!;
            }

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                Text = folderDialog.SelectedPath;
            }

            return;
        }

        if (_values.UseSaveDialog)
        {
            using KryptonSaveFileDialog saveFileDialog = new KryptonSaveFileDialog();

            if (!string.IsNullOrEmpty(_values.InitialDirectory))
            {
                saveFileDialog.InitialDirectory = _values.InitialDirectory!;
            }

            if (!string.IsNullOrEmpty(_values.FileDialogFilter))
            {
                saveFileDialog.Filter = _values.FileDialogFilter!;
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Text = Path.GetFullPath(saveFileDialog.FileName!);
            }
        }
        else
        {
            using KryptonOpenFileDialog dialog = new KryptonOpenFileDialog();

            if (!string.IsNullOrEmpty(_values.InitialDirectory))
            {
                dialog.InitialDirectory = _values.InitialDirectory!;
            }

            if (!string.IsNullOrEmpty(_values.FileDialogFilter))
            {
                dialog.Filter = _values.FileDialogFilter!;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Text = Path.GetFullPath(dialog.FileName!);
            }
        }
    }

    /// <summary>Handles the Execute event of the Reset control.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void Reset_Execute(object? sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Text))
        {
            Text = string.Empty;

            _bsaReset.Enabled = ButtonEnabled.False;
        }
    }

    #endregion

    #region Protected

    /// <summary>Raises the Paint event.</summary>
    /// <param name="e">A PaintEventArgs containing the event data.</param>
    protected override void OnPaint(PaintEventArgs? e)
    {
        _bsaReset.Visible = _values.ShowResetButton;

        base.OnPaint(e);
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged">TextChanged</see> event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs">EventArgs</see> that contains the event data.</param>
    protected override void OnTextChanged(EventArgs e)
    {
        if (!string.IsNullOrEmpty(Text))
        {
            _bsaReset.Enabled = ButtonEnabled.True;
        }
        else
        {
            _bsaReset.Enabled = ButtonEnabled.False;
        }

        base.OnTextChanged(e);
    }

    #endregion
}

#endregion
