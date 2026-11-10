#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Gauges;

/// <summary>
/// Designer for <see cref="KryptonEqualizer"/>.
/// </summary>
internal class KryptonEqualizerDesigner : ControlDesigner
{
    #region Identity

    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonEqualizerDesigner"/> class.
    /// </summary>
    public KryptonEqualizerDesigner() => AutoResizeHandles = true;

    #endregion

    #region Public Overrides

    /// <summary>
    /// Gets the design-time action lists supported by the component associated with the designer.
    /// </summary>
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actions = new DesignerActionListCollection();
            actions.Add(new KryptonEqualizerActionList(this));
            return actions;
        }
    }

    #endregion
}
