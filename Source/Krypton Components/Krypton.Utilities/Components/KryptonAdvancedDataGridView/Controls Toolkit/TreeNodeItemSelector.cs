#region BSD License
/*
 *
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

internal class TreeNodeItemSelector : TreeNode
{
    #region public enum

    public enum CustomNodeType : byte
    {
        Default,
        SelectAll,
        SelectEmpty,
        DateTimeNode
    }

    #endregion


    #region class properties

    private CheckState _checkState = CheckState.Unchecked;
    private TreeNodeItemSelector? _parent;

    #endregion


    #region constructor

    /// <summary>
    /// TreeNodeItemSelector constructor
    /// </summary>
    /// <param name="text"></param>
    /// <param name="value"></param>
    /// <param name="state"></param>
    /// <param name="nodeType"></param>
    private TreeNodeItemSelector(string? text, object? value, CheckState state, CustomNodeType nodeType)
        : base(text)
    {
        CheckState = state;
        NodeType = nodeType;
        Value = value;
    }

    #endregion


    #region public clone method

    /// <summary>
    /// Clone a Node
    /// </summary>
    /// <returns></returns>
    public new TreeNodeItemSelector Clone()
    {
        TreeNodeItemSelector n = new TreeNodeItemSelector(Text, Value, _checkState, NodeType)
        {
            NodeFont = NodeFont
        };

        if (GetNodeCount(false) > 0)
        {
            foreach (TreeNodeItemSelector? child in Nodes)
            {
                if (child != null)
                {
                    n.AddChild(child.Clone());
                }
            }
        }

        return n;
    }

    #endregion


    #region public getters / setters

    /// <summary>
    /// Get Node NodeType
    /// </summary>
    public CustomNodeType NodeType { get; private set; }

    /// <summary>
    /// Get Node value
    /// </summary>
    public object? Value { get; private set; }

    /// <summary>
    /// Get Node parent
    /// </summary>
    public new TreeNodeItemSelector? Parent
    {
        get => _parent;
        set => _parent = value;
    }

    /// <summary>
    /// Node is Checked
    /// </summary>
    public new bool Checked
    {
        get => _checkState == CheckState.Checked;
        set => CheckState = value ? CheckState.Checked : CheckState.Unchecked;
    }

    /// <summary>
    /// Get or Set the current Node CheckState
    /// </summary>
    public CheckState CheckState
    {
        get => _checkState;
        set
        {
            _checkState = value;
            StateImageIndex = _checkState switch
            {
                CheckState.Checked => 1,
                CheckState.Indeterminate => 2,
                _ => 0
            };
        }
    }

    #endregion


    #region public create nodes methods

    /// <summary>
    /// Create a Node
    /// </summary>
    /// <param name="text"></param>
    /// <param name="value"></param>
    /// <param name="state"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static TreeNodeItemSelector CreateNode(string? text, object? value, CheckState state, CustomNodeType type)
    {
        return new TreeNodeItemSelector(text, value, state, type);
    }

    /// <summary>
    /// Create a child Node
    /// </summary>
    /// <param name="text"></param>
    /// <param name="value"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public TreeNodeItemSelector? CreateChildNode(string? text, object? value, CheckState state)
    {
        TreeNodeItemSelector? n = null;

        //specific method for datetimenode
        if (NodeType == CustomNodeType.DateTimeNode)
        {
            n = new TreeNodeItemSelector(text, value, state, CustomNodeType.DateTimeNode);
        }

        if (n != null)
        {
            AddChild(n);
        }

        return n;
    }
    public TreeNodeItemSelector? CreateChildNode(string? text, object? value)
    {
        return CreateChildNode(text, value, _checkState);
    }

    /// <summary>
    /// Add a child Node to this Node
    /// </summary>
    /// <param name="child"></param>
    protected void AddChild(TreeNodeItemSelector? child)
    {
        child!.Parent = this;
        Nodes.Add(child);
    }

    #endregion
}
