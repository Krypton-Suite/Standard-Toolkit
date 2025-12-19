#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public partial class VisualExceptionDialogForm : KryptonForm
{
    #region Instance Fields

    private readonly bool? _showCopyButton;

    private readonly bool? _showSearchBox;

    private readonly Color? _highlightColor;

    private readonly Exception? _exception;

    private readonly Action<Exception>? _bugReportCallback;

    private List<KryptonTreeNode> _originalNodes = new List<KryptonTreeNode>();

    #endregion

    #region Identity

    public VisualExceptionDialogForm(bool? showCopyButton, bool? showSearchBox, Color? highlightColor, Exception exception, Action<Exception>? bugReportCallback = null)
    {
        InitializeComponent();

        SetInheritedControlOverride();

        _showCopyButton = showCopyButton ?? false;

        _showSearchBox = showSearchBox ?? false;

        _highlightColor = highlightColor ?? Color.LightYellow;

        _exception = exception;

        _bugReportCallback = bugReportCallback;

        // Set highlight color
        isbSearchArea.HighlightColor = (Color)_highlightColor;

        SetupUI();
    }

    #endregion

    #region Implementation

    private void SetupUI()
    {
        Text = KryptonManager.Strings.ExceptionDialogStrings.WindowTitle;
        kwlblExceptionDetails.Text = KryptonManager.Strings.ExceptionDialogStrings.ExceptionDetailsHeader;
        kwlblExceptionOutline.Text = KryptonManager.Strings.ExceptionDialogStrings.ExceptionOutlineHeader;
        kbtnCopy.Text = KryptonManager.Strings.GeneralStrings.Copy;
        kbtnOk.Text = KryptonManager.Strings.GeneralStrings.OK;
        kbtnCopy.Visible = _showCopyButton ?? true;
        isbSearchArea.ShowSearchFeatures = _showSearchBox ?? true;

        if (_bugReportCallback != null && _exception != null)
        {
            kbtnReportBug.Visible = true;
            kbtnReportBug.Text = "Report Bug";
            kbtnReportBug.Click += KbtnReportBug_Click;
        }
        else
        {
            kbtnReportBug.Visible = false;
        }

        isbSearchArea.SearchBox.CueHint.CueHintText = KryptonManager.Strings.ExceptionDialogStrings.SearchBoxCueText;
        if (_exception is not null)
        {
            isbSearchArea.Populate(_exception);

            foreach (KryptonTreeNode node in isbSearchArea.Tree.Nodes)
            {
                _originalNodes.Add((KryptonTreeNode)node.Clone());
            }
        }

        if (GeneralToolkitUtilities.GetCurrentScreenSize() == new Point(1080, 720))
        {
            GeneralToolkitUtilities.AdjustFormDimensions(this, 900, 650);
        }
        else
        {
            GeneralToolkitUtilities.AdjustFormDimensions(this, 1108, 687);
        }
    }

    private string? FormatExceptionDetails(Exception exception) =>
        // Format exception details
        $"{KryptonManager.Strings.ExceptionDialogStrings.Type}: {exception.GetType().Name}\n" +
        $"{KryptonManager.Strings.ExceptionDialogStrings.Message}: {exception.Message}\n\n" +
        $"{KryptonManager.Strings.ExceptionDialogStrings.StackTrace}:\n{exception.StackTrace}\n\n" +
        $"{KryptonManager.Strings.ExceptionDialogStrings.InnerException}:\n{(exception.InnerException != null ? exception.InnerException.Message : $"{KryptonManager.Strings.ExceptionDialogStrings.None}")}\n";

    private void kbtnCopy_Click(object sender, EventArgs e) => Clipboard.SetText(krtbExceptionDetails.Text);

    private void kbtnOk_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;

    private void isbSearchArea_NodeSelected(object sender, TreeViewEventArgs e)
    {
        var selectedException = isbSearchArea.SelectedException;

        if (e.Node!.Text == KryptonManager.Strings.ExceptionDialogStrings.InnerException ||
            e.Node.Text == KryptonManager.Strings.ExceptionDialogStrings.StackTrace)
        {
            krtbExceptionDetails.Text = KryptonManager.Strings.ExceptionDialogStrings.MoreDetails;
        }
        else
        {
            krtbExceptionDetails.Text = selectedException != null
                ? FormatExceptionDetails(selectedException)
                : e.Node.Text;
        }
    }

    private void krtbExceptionDetails_TextChanged(object sender, EventArgs e) => kbtnCopy.Enabled = !string.IsNullOrEmpty(krtbExceptionDetails.Text);

    private void KbtnReportBug_Click(object? sender, EventArgs e)
    {
        if (_exception != null && _bugReportCallback != null)
        {
            _bugReportCallback(_exception);
        }
    }

    #endregion

    #region Show

    internal static void Show(Exception exception, Color? highlightColor, bool? showCopyButton, bool? showSearchBox, Action<Exception>? bugReportCallback = null)
    {
        using var ved = new VisualExceptionDialogForm(showCopyButton, showSearchBox, highlightColor, exception, bugReportCallback);

        ved.ShowDialog();
    }

    #endregion
}