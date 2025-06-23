#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

internal class KryptonThemeBrowserDesigner : ComponentDesigner
{

    #region Public Overrides

    public override void Initialize([DisallowNull] IComponent component)
    {
        base.Initialize(component);
    }

    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actionList = new DesignerActionListCollection
            {
                new KryptonThemeBrowserActionList(this)
            };

            return actionList;
        }
    }

    public override DesignerVerbCollection Verbs { get; }

    #endregion
}