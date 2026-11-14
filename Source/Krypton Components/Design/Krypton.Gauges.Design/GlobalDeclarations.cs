#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, tobitege, Lesandro, KamaniAR, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

global using System;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.ComponentModel.Design;
global using System.Diagnostics.CodeAnalysis;
global using System.Drawing;
global using System.Windows.Forms;
global using System.Windows.Forms.Design;

global using Krypton.Gauges;
global using Krypton.Toolkit;

global using ControlDesigner = Microsoft.DotNet.DesignTools.Designers.ControlDesigner;
global using DesignerActionList = Microsoft.DotNet.DesignTools.Designers.Actions.DesignerActionList;
global using DesignerActionListCollection = Microsoft.DotNet.DesignTools.Designers.Actions.DesignerActionListCollection;
global using DesignerActionItemCollection = Microsoft.DotNet.DesignTools.Designers.Actions.DesignerActionItemCollection;
global using DesignerActionPropertyItem = Microsoft.DotNet.DesignTools.Designers.Actions.DesignerActionPropertyItem;
global using DesignerActionHeaderItem = Microsoft.DotNet.DesignTools.Designers.Actions.DesignerActionHeaderItem;
global using Microsoft.DotNet.DesignTools.TypeRouting;

[assembly: CLSCompliant(false)]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
