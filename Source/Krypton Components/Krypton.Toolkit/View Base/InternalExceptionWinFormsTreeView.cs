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
    internal class InternalExceptionWinFormsTreeView : TreeView
    {
        #region Public

        public Exception? SelectedException => SelectedNode?.Tag as Exception;

        #endregion

        #region Public Override

        [Browsable(false)]
        public override Font Font { get; set; }

        #endregion

        #region Identity

        public InternalExceptionWinFormsTreeView()
        {
            Font = new Font(@"Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
        }

        #endregion

        #region Implementation

        public void Populate(Exception? exception)
        {
            this.Nodes.Clear(); // Clear existing nodes
            if (exception != null)
            {
                TreeNode rootNode = CreateNodeFromException(exception);
                this.Nodes.Add(rootNode);
            }
        }

        private TreeNode CreateNodeFromException(Exception? exception)
        {
            TreeNode rootNode = new TreeNode($"{exception.GetType().Name}: {exception.Message}")
            {
                Tag = exception // Store the exception in the Tag property
            };

            // Stack trace node
            if (!string.IsNullOrWhiteSpace(exception.StackTrace))
            {
                TreeNode stackTraceNode = new TreeNode(KryptonManager.Strings.ExceptionDialogStrings.StackTrace);
                stackTraceNode.Nodes.Add(exception.StackTrace);
                rootNode.Nodes.Add(stackTraceNode);
            }

            // Inner exception nodes
            if (exception.InnerException != null)
            {
                TreeNode innerExceptionNode = new TreeNode(KryptonManager.Strings.ExceptionDialogStrings.InnerException);
                innerExceptionNode.Nodes.Add(CreateNodeFromException(exception.InnerException));
                rootNode.Nodes.Add(innerExceptionNode);
            }

            // Data dictionary
            if (exception.Data != null && exception.Data.Count > 0)
            {
                TreeNode dataNode = new TreeNode(KryptonManager.Strings.ExceptionDialogStrings.Data);
                foreach (var key in exception.Data.Keys)
                {
                    dataNode.Nodes.Add(new TreeNode($"{key}: {exception.Data[key]}"));
                }
                rootNode.Nodes.Add(dataNode);
            }

            return rootNode;
        }

        #endregion
    }
}