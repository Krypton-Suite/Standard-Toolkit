#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2024. All rights reserved. 
 */
#endregion

// This file holds the global definitions >= C# 10

global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.Collections.ObjectModel;
global using System.Collections.Specialized;
global using System.ComponentModel;
global using System.ComponentModel.Design;
global using System.Diagnostics;
global using System.Diagnostics.CodeAnalysis;
global using System.Drawing;
global using System.Drawing.Design;
global using System.Drawing.Drawing2D;
global using System.Drawing.Imaging;
global using System.Drawing.Printing;
global using System.Drawing.Text;
global using System.Globalization;
global using System.IO;
global using System.Linq;
global using System.Media;
global using System.Reflection;
global using System.Resources;
global using System.Runtime.CompilerServices;
global using System.Runtime.InteropServices;
global using System.Runtime.Serialization;
global using System.Runtime.Serialization.Formatters.Binary;
global using System.Security;
global using System.Security.Principal;
global using System.Text;
global using System.Text.RegularExpressions;
global using System.Timers;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Windows.Forms;
global using System.Windows.Forms.Design;
global using System.Windows.Forms.Design.Behavior;
global using System.Windows.Forms.VisualStyles;
global using System.Xml;

global using Krypton.Toolkit.ResourceFiles.Arrows;
global using Krypton.Toolkit.ResourceFiles.ButtonSpecs;
global using Krypton.Toolkit.ResourceFiles.CheckBoxes;
global using Krypton.Toolkit.ResourceFiles.CommandLink;
global using Krypton.Toolkit.ResourceFiles.ControlBox;
global using Krypton.Toolkit.ResourceFiles.Dialogs;
global using Krypton.Toolkit.ResourceFiles.DropDown;
global using Krypton.Toolkit.ResourceFiles.Gallery;
global using Krypton.Toolkit.ResourceFiles.Generic;
global using Krypton.Toolkit.ResourceFiles.Grid;
global using Krypton.Toolkit.ResourceFiles.Logos;
global using Krypton.Toolkit.ResourceFiles.MDI;
global using Krypton.Toolkit.ResourceFiles.MessageBox;
global using Krypton.Toolkit.ResourceFiles.Pendants;
global using Krypton.Toolkit.ResourceFiles.Pin;
global using Krypton.Toolkit.ResourceFiles.RadioButtons;
global using Krypton.Toolkit.ResourceFiles.SizeGripStyles;
global using Krypton.Toolkit.ResourceFiles.TaskDialog;
global using Krypton.Toolkit.ResourceFiles.ToastNotification;
global using Krypton.Toolkit.ResourceFiles.Toolbars;
global using Krypton.Toolkit.ResourceFiles.TreeItems;
global using Krypton.Toolkit.ResourceFiles.UAC;

global using Microsoft.Win32;
global using Microsoft.Win32.SafeHandles;


[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: Dependency(nameof(System), LoadHint.Always)]
[assembly: Dependency(@"System.Drawing", LoadHint.Always)]
[assembly: Dependency(@"System.Windows.Forms", LoadHint.Always)]
[assembly: InternalsVisibleTo(@"Krypton.Navigator, PublicKey=a87e673e9ecb6e8e", AllInternalsVisible = true)]
[assembly: InternalsVisibleTo(@"Krypton.Ribbon, PublicKey=a87e673e9ecb6e8e", AllInternalsVisible = true)]