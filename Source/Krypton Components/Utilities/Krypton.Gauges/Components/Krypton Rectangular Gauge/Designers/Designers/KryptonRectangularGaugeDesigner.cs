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
/// Designer for <see cref="KryptonRectangularGauge"/>.
/// </summary>
internal class KryptonRectangularGaugeDesigner : ControlDesigner
{
    /// <summary>
    /// Initialize a new instance of the <see cref="KryptonRectangularGaugeDesigner"/> class.
    /// </summary>
    public KryptonRectangularGaugeDesigner() => AutoResizeHandles = true;

    /// <inheritdoc />
    public override DesignerActionListCollection ActionLists
    {
        get
        {
            var actions = new DesignerActionListCollection();
            actions.Add(new KryptonRectangularGaugeActionList(this));
            return actions;
        }
    }
}
