#region BSD License
/*
 * New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 * Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 */
#endregion

global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.ComponentModel.Design;
global using System.Diagnostics;
global using System.Diagnostics.CodeAnalysis;
global using System.Drawing;
global using System.Drawing.Design;
global using System.Drawing.Drawing2D;
global using System.IO;
global using System.Linq;
global using System.Reflection;
global using System.Runtime.CompilerServices;
global using System.Runtime.InteropServices;
global using System.Text;
global using System.Windows.Forms;
global using System.Windows.Forms.Design;

global using Krypton.Interop;
global using Krypton.Toolkit;

global using ControlDesigner = Microsoft.DotNet.DesignTools.Designers.ControlDesigner;
global using ParentControlDesigner = Microsoft.DotNet.DesignTools.Designers.ParentControlDesigner;
global using ScrollableControlDesigner = Microsoft.DotNet.DesignTools.Designers.ScrollableControlDesigner;
global using ComponentDesigner = Microsoft.DotNet.DesignTools.Designers.ComponentDesigner;
global using DesignerActionList = Microsoft.DotNet.DesignTools.Designers.Actions.DesignerActionList;
global using DesignerActionListCollection = Microsoft.DotNet.DesignTools.Designers.Actions.DesignerActionListCollection;
global using DesignerActionItemCollection = Microsoft.DotNet.DesignTools.Designers.Actions.DesignerActionItemCollection;
global using DesignerActionPropertyItem = Microsoft.DotNet.DesignTools.Designers.Actions.DesignerActionPropertyItem;
global using DesignerActionMethodItem = Microsoft.DotNet.DesignTools.Designers.Actions.DesignerActionMethodItem;
global using DesignerActionHeaderItem = Microsoft.DotNet.DesignTools.Designers.Actions.DesignerActionHeaderItem;
global using DesignerActionTextItem = Microsoft.DotNet.DesignTools.Designers.Actions.DesignerActionTextItem;
global using SelectionRules = Microsoft.DotNet.DesignTools.Designers.SelectionRules;

// Design.Server wraps Microsoft.DotNet.DesignTools types, which are not CLS-compliant.
[assembly: CLSCompliant(false)]
[assembly: ComVisible(false)]
