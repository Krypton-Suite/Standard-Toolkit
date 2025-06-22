#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion


namespace Krypton.Toolkit;

internal class InternalExceptionTreeView : KryptonTreeView
{
    #region Public

    /// <summary>Gets the selected exception.</summary>
    public Exception? SelectedException => SelectedNode?.Tag as Exception;

    #endregion

    #region Identity

    /// <summary>Initializes a new instance of the <see cref="InternalExceptionTreeView" /> class.</summary>
    public InternalExceptionTreeView()
    {
        Font = new Font("Consolas", 8.25F, FontStyle.Regular);
    }

    #endregion

    #region Implementation

    /// <summary>Populates the tree view with a given exception.</summary>
    public void Populate([AllowNull] Exception exception)
    {
        Nodes.Clear();

        if (exception is not null)
        {
            TreeNode rootNode = CreateNodeFromException(exception);
            Nodes.Add(rootNode);
            ExpandAll();
        }
    }

    private TreeNode CreateNodeFromException(Exception exception)
    {
        var rootNode = new TreeNode($"{exception.GetType().Name}: {exception.Message}")
        {
            Tag = exception
        };

        // Stack trace
        if (!string.IsNullOrWhiteSpace(exception.StackTrace))
        {
            TreeNode stackTraceNode = new TreeNode(KryptonManager.Strings.ExceptionDialogStrings.StackTrace);
            var stackTrace = new StackTrace(exception, true);

            var frames = stackTrace.GetFrames();

            if (frames is not null)
            {
                foreach (var frame in frames)
                {
                    var method = frame.GetMethod();
                    var fileName = frame.GetFileName();
                    var lineNumber = frame.GetFileLineNumber();

                    string frameInfo = $"at {method?.DeclaringType?.FullName}.{method?.Name}";
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        frameInfo += $" in {fileName}:line {lineNumber}";
                    }

                    stackTraceNode.Nodes.Add(new TreeNode(frameInfo));
                }
            }

            rootNode.Nodes.Add(stackTraceNode);
        }

        // Inner exception
        if (exception.InnerException is not null)
        {
            var innerNode = new TreeNode(KryptonManager.Strings.ExceptionDialogStrings.InnerException)
            {
                Nodes = { CreateNodeFromException(exception.InnerException) }
            };
            rootNode.Nodes.Add(innerNode);
        }

        // Exception.Data
        if (exception.Data is { Count: > 0 })
        {
            var dataNode = new TreeNode(KryptonManager.Strings.ExceptionDialogStrings.Data);
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