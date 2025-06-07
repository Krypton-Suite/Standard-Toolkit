#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit
{
    internal partial class VisualExceptionDialogForm : KryptonForm
    {
        #region Instance Fields

        private readonly bool? _showCopyButton;

        private readonly bool? _showSearchBox;

        private readonly Exception? _exception;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="VisualExceptionDialogForm" /> class.</summary>
        /// <param name="showCopyButton">The show copy button.</param>
        /// <param name="showSearchBox">Shows the search box.</param>
        /// <param name="exception">The exception.</param>
        public VisualExceptionDialogForm(bool? showCopyButton, bool? showSearchBox, Exception? exception)
        {
            SetInheritedControlOverride();
            InitializeComponent();
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

            ktxtSearchBox.Visible = _showSearchBox ?? true;

            if (_exception is not null)
            {
                etvExceptionOutline.Populate(_exception);
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

        private void VisualExceptionDialogForm_Load(object sender, EventArgs e)
        {

        }

        private void etvExceptionOutline_AfterSelect(object sender, TreeViewEventArgs? e)
        {
            // Display the selected exception's details in the RichTextBox
            var selectedException = etvExceptionOutline.SelectedException;

            if (e!.Node!.Text == KryptonManager.Strings.ExceptionDialogStrings.InnerException ||
                e.Node.Text == KryptonManager.Strings.ExceptionDialogStrings.StackTrace)
            {
                rtbExceptionDetails.Text = KryptonManager.Strings.ExceptionDialogStrings.MoreDetails;
            }
            else 
            {
                rtbExceptionDetails.Text = selectedException != null ? FormatExceptionDetails(selectedException) : e?.Node.Text; // Display general node text if no exception is associated
            }
        }

        private void kbtnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(rtbExceptionDetails.Text);
        }

        private void kbtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private static string FormatExceptionDetails(Exception exception) =>
            // Format exception details
            $"{KryptonManager.Strings.ExceptionDialogStrings.Type}: {exception.GetType().Name}\n" +
            $"{KryptonManager.Strings.ExceptionDialogStrings.Message}: {exception.Message}\n\n" +
            $"{KryptonManager.Strings.ExceptionDialogStrings.StackTrace}:\n{exception.StackTrace}\n\n" +
            $"{KryptonManager.Strings.ExceptionDialogStrings.InnerException}:\n{(exception.InnerException != null ? exception.InnerException.Message : $"{KryptonManager.Strings.ExceptionDialogStrings.None}")}\n";

        private void etvExceptionOutline_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs? e)
        {
            if (e is not null)
            {
                kbtnCopy.Enabled = e.Node is { IsSelected: true };
            }
        }

        private void ktxtSearchBox_TextChanged(object sender, EventArgs e)
        {
            string searchQueryText = ktxtSearchBox.Text.Trim().ToLowerInvariant();

            foreach (TreeNode node in etvExceptionOutline.Nodes)
            {
                FilterNode(node, searchQueryText);
            }
        }

        private bool FilterNode(TreeNode node, string searchQueryText)
        {
            bool match = string.IsNullOrEmpty(searchQueryText) || node.Text.ToLowerInvariant().Contains(searchQueryText);
            
            foreach (TreeNode child in node.Nodes)
            {
                match |= FilterNode(child, searchQueryText);
            }

            node.BackColor = match ? SystemColors.Window : Color.LightGray;
            node.ForeColor = match ? SystemColors.ControlText : Color.Gray;
            node.EnsureVisible(); // Optional: expands the matched nodes

            return match;
        }

        #endregion

        #region Show

        internal static void Show(Exception exception, bool? showCopyButton, bool? showSearchBox)
        {
            using var ved = new VisualExceptionDialogForm(showCopyButton, showSearchBox, exception);

            ved.ShowDialog();
        }

        #endregion

        private void bsaClear_Click(object sender, EventArgs e) => ktxtSearchBox.Clear();
    }
}
