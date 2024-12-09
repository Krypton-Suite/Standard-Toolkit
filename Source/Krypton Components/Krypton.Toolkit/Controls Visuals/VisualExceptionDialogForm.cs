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

        private readonly Exception? _exception;

        #endregion

        public VisualExceptionDialogForm(Exception exception, bool? showCopyButton)
        {
            SetInheritedControlOverride();

            InitializeComponent();

            _showCopyButton = showCopyButton ?? true;

            _exception = exception;

            SetupUI();
        }

        private void SetupUI()
        {
            Text = KryptonManager.Strings.ExceptionDialogStrings.WindowTitle;

            kwlblExceptionDetails.Text = KryptonManager.Strings.ExceptionDialogStrings.ExceptionDetailsHeader;

            kwlblExceptionOutline.Text = KryptonManager.Strings.ExceptionDialogStrings.ExceptionOutlineHeader;

            kbtnCopy.Text = KryptonManager.Strings.GeneralStrings.Copy;

            kbtnOk.Text = KryptonManager.Strings.GeneralStrings.OK;

            kbtnCopy.Visible = _showCopyButton ?? true;
        }

        private void VisualExceptionDialogForm_Load(object sender, EventArgs e)
        {
            if (_exception is not null)
            {
                ketvExceptionOutline.Populate(_exception);
            }
        }

        private void ketvExceptionOutline_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Display the selected exception's details in the RichTextBox
            var selectedException = ketvExceptionOutline.SelectedException;
            
            // Display general node text if no exception is associated
            krtbExceptionDetails.Text = selectedException != null ? FormatExceptionDetails(selectedException) : e.Node.Text;
        }

        private string FormatExceptionDetails(Exception exception) =>
            // Format exception details
            $"Type: {exception.GetType().Name}\n" +
            $"Message: {exception.Message}\n\n" +
            $"Stack Trace:\n{exception.StackTrace}\n\n" +
            $"Inner Exception:\n{(exception.InnerException != null ? exception.InnerException.Message : "None")}\n";

        private void kbtnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(krtbExceptionDetails.Text);
        }

        private void kbtnOk_Click(object sender, EventArgs e)
        {

        }

        internal static void Show(Exception exception, bool? showCopyButton)
        {
            using var ked = new VisualExceptionDialogForm(exception, showCopyButton);

            ked.ShowDialog();
        }
    }
}
