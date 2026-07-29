#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

[ToolboxBitmap(typeof(BackgroundWorker))]
public class KryptonLoadingCircleToolStripMenuItem : ToolStripControlHost
{
    // Constants =========================================================

    // Attributes ========================================================

    // Properties ========================================================
    /// <summary>
    /// Gets the loading circle control.
    /// </summary>
    /// <value>The loading circle control.</value>
    [RefreshProperties(RefreshProperties.All),
     DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public KryptonLoadingCircle? LoadingCircleControl => Control as KryptonLoadingCircle;

    // Constructor ========================================================
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonLoadingCircleToolStripMenuItem"/> class.
    /// </summary>
    public KryptonLoadingCircleToolStripMenuItem()
        : base(new KryptonLoadingCircle())
    {

    }

    /// <summary>
    /// Retrieves the size of a rectangular area into which a control can be fitted.
    /// </summary>
    /// <param name="constrainingSize">The custom-sized area for a control.</param>
    /// <returns>
    /// An ordered pair of type <see cref="T:System.Drawing.Size"></see> representing the width and height of a rectangle.
    /// </returns>
    /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public override Size GetPreferredSize(Size constrainingSize)
    {
        //return base.GetPreferredSize(constrainingSize);
        return LoadingCircleControl!.GetPreferredSize(constrainingSize);
    }


    /// <summary>
    /// Subscribes events from the hosted control.
    /// </summary>
    /// <param name="control">The control from which to subscribe events.</param>
    protected override void OnSubscribeControlEvents(Control? control)
    {
        base.OnSubscribeControlEvents(control);

        //Add your code here to subsribe to Control Events
    }

    /// <summary>
    /// Unsubscribes events from the hosted control.
    /// </summary>
    /// <param name="control">The control from which to unsubscribe events.</param>
    protected override void OnUnsubscribeControlEvents(Control? control)
    {
        base.OnUnsubscribeControlEvents(control);

        //Add your code here to unsubscribe from control events.
    }
}