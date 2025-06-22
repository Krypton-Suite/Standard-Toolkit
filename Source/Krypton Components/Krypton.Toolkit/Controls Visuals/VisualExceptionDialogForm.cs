#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

public partial class VisualExceptionDialogForm : KryptonForm
{
    #region Instance Fields

    private readonly bool? _showCopyButton;

    private readonly bool? _showSearchBox;

    private readonly Exception? _exception;

    private List<TreeNode> _originalNodes = new List<TreeNode>();

    #endregion

    #region Identity

    public VisualExceptionDialogForm(bool? showCopyButton, bool? showSearchBox, Exception exception)
    {
        InitializeComponent();

        SetInheritedControlOverride();

        _showCopyButton = showCopyButton;

        _showSearchBox = showSearchBox;

        _exception = exception;

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

        isbSearchArea.SearchBox.CueHint.CueHintText = KryptonManager.Strings.ExceptionDialogStrings.SearchBoxCueText;
        if (_exception is not null)
        {
            isbSearchArea.Populate(_exception);

            foreach (TreeNode node in isbSearchArea.Tree.Nodes)
            {
                _originalNodes.Add((TreeNode)node.Clone());
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

    private void kbtnCopy_Click(object sender, EventArgs e) => Clipboard.SetText(rtbExceptionDetails.Text);

    private void kbtnOk_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;

    private void rtbExceptionDetails_TextChanged(object sender, EventArgs e) => kbtnCopy.Enabled = !string.IsNullOrEmpty(rtbExceptionDetails.Text);

    #endregion

    #region Show

    internal static void Show(Exception exception, bool? showCopyButton, bool? showSearchBox)
    {
        using var ved = new VisualExceptionDialogForm(showCopyButton, showSearchBox, exception);

        ved.ShowDialog();
    }

    #endregion

    private void isbSearchArea_NodeSelected(object sender, TreeViewEventArgs e)
    {
        var selectedException = isbSearchArea.SelectedException;

        if (e.Node!.Text == KryptonManager.Strings.ExceptionDialogStrings.InnerException ||
            e.Node.Text == KryptonManager.Strings.ExceptionDialogStrings.StackTrace)
        {
            rtbExceptionDetails.Text = KryptonManager.Strings.ExceptionDialogStrings.MoreDetails;
        }
        else
        {
            rtbExceptionDetails.Text = selectedException != null
                ? FormatExceptionDetails(selectedException)
                : e.Node.Text;
        }
    }
}