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
    internal class InternalExceptionTreeView : KryptonTreeView
    {
        #region Public

        /// <summary>Gets the selected exception.</summary>
        /// <value>The selected exception.</value>
        public Exception? SelectedException => SelectedNode?.Tag as Exception;

        #endregion

        #region Identity

        /// <summary>Initializes a new instance of the <see cref="InternalExceptionTreeView" /> class.</summary>
        public InternalExceptionTreeView()
        {
            
        }

        #endregion

        #region Implementation

        /// <summary>Populates the specified exception.</summary>
        /// <param name="exception">The exception.</param>
        public void Populate([AllowNull] Exception exception)
        {
            Nodes.Clear();

            if (exception is not null)
            {
                TreeNode rootNode = CreateNodeFromException(exception);

                Nodes.Add(rootNode);
            }
        }

        /// <summary>Creates the node from exception.</summary>
        /// <param name="exception">The exception.</param>
        /// <returns>The <see cref="KryptonTreeView"/> node structure.</returns>
        private TreeNode CreateNodeFromException([AllowNull]Exception exception)
        {
            var rootNode = new TreeNode($"{exception?.GetType().Name}: {exception?.Message}")
            {
                Tag = exception // Store the exception in the Tag property
            };

            // Stack trace node
            if (!string.IsNullOrWhiteSpace(exception?.StackTrace))
            {
                var stackTraceNode = new TreeNode("Stack Trace");
                stackTraceNode.Nodes.Add(exception?.StackTrace);
                rootNode.Nodes.Add(stackTraceNode);
            }

            // Inner exception nodes
            if (exception?.InnerException != null)
            {
                var innerExceptionNode = new TreeNode("Inner Exception");
                innerExceptionNode.Nodes.Add(CreateNodeFromException(exception.InnerException));
                rootNode.Nodes.Add(innerExceptionNode);
            }

            // Data dictionary
            if (exception?.Data is not null && exception.Data.Count > 0)
            {
                var dataNode = new TreeNode("Data");
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