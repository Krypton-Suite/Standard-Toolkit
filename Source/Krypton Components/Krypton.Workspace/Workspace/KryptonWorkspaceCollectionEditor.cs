#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

using System.Linq;

namespace Krypton.Workspace;

internal partial class KryptonWorkspaceCollectionEditor : KryptonDesignerCollectionEditor
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonWorkspaceCollectionEditor class.
    /// </summary>
    public KryptonWorkspaceCollectionEditor()
        : base(typeof(KryptonWorkspaceCollection))
    {
    }
    #endregion

    #region Protected
    /// <summary>
    /// Gets access to the owning workspace instance.
    /// </summary>
    public KryptonWorkspace Workspace
    {
        get
        {
            var sequence = Context!.Instance as KryptonWorkspaceSequence ?? throw new NullReferenceException(GlobalStaticFunctions.VariableCannotBeNull(nameof(Context.Instance)));
            return sequence.WorkspaceControl;
        }
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Creates the Krypton-themed workspace collection editor form.
    /// </summary>
    /// <returns>Editor form instance.</returns>
    protected override VisualDesignerCollectionForm CreateKryptonDesignerCollectionForm() =>
        new KryptonWorkspaceCollectionForm(this);

    #endregion
}
