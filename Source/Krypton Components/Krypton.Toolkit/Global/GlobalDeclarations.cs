#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2025. All rights reserved. 
 */
#endregion

// This file holds the global definitions >= C# 10

#region WinForms Libraries

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
#if NET8_0_OR_GREATER
global using System.Text.Json.Nodes;
#endif
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

#endregion

#region Internal Resources

global using Krypton.Toolkit.Properties;
global using Krypton.Toolkit.ResourceFiles.Arrows;
global using Krypton.Toolkit.ResourceFiles.Buttons;
global using Krypton.Toolkit.ResourceFiles.ButtonSpecs;
global using Krypton.Toolkit.ResourceFiles.CheckBoxes;
global using Krypton.Toolkit.ResourceFiles.CommandLink;
global using Krypton.Toolkit.ResourceFiles.ControlBox;
global using Krypton.Toolkit.ResourceFiles.DataBars;
global using Krypton.Toolkit.ResourceFiles.Dialogs;
global using Krypton.Toolkit.ResourceFiles.DropDown;
global using Krypton.Toolkit.ResourceFiles.Elements;
global using Krypton.Toolkit.ResourceFiles.Gallery;
global using Krypton.Toolkit.ResourceFiles.Generic;
global using Krypton.Toolkit.ResourceFiles.Grid;
global using Krypton.Toolkit.ResourceFiles.Logos;
global using Krypton.Toolkit.ResourceFiles.MDI;
global using Krypton.Toolkit.ResourceFiles.MessageBox;
global using Krypton.Toolkit.ResourceFiles.OutlookGrid;
global using Krypton.Toolkit.ResourceFiles.PaletteSchemas;
global using Krypton.Toolkit.ResourceFiles.Pendants;
global using Krypton.Toolkit.ResourceFiles.Pin;
global using Krypton.Toolkit.ResourceFiles.RadioButtons;
global using Krypton.Toolkit.ResourceFiles.SizeGripStyles;
global using Krypton.Toolkit.ResourceFiles.Sort;
global using Krypton.Toolkit.ResourceFiles.TaskDialog;
global using Krypton.Toolkit.ResourceFiles.ToastNotification;
global using Krypton.Toolkit.ResourceFiles.Toolbars;
global using Krypton.Toolkit.ResourceFiles.ToolkitLogos;
global using Krypton.Toolkit.ResourceFiles.TreeItems;
global using Krypton.Toolkit.ResourceFiles.UAC;
global using Krypton.Toolkit.ResourceFiles.VisualStudio;
global using Krypton.Toolkit.ResourceFiles.WindowsLogos;

#endregion

global using Microsoft.Win32;
global using Microsoft.Win32.SafeHandles;


[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: Dependency(nameof(System), LoadHint.Always)]
[assembly: Dependency(@"System.Drawing", LoadHint.Always)]
[assembly: Dependency(@"System.Windows.Forms", LoadHint.Always)]

// Public key value needs to be the full key. Before, this was the PublicKeyToken value.
// See: https://stackoverflow.com/questions/106880/internalsvisibleto-attribute-isnt-working/107958#107958
[assembly: InternalsVisibleTo( "Krypton.Navigator, PublicKey=00240000048000009400000006020000002400005253413100040000010001001f208b6887f7b4f8fad6c30b9eca9849f09cbfbd37901e222f8e888331622c907dfa686c56389c95966b86b33f0dd0ab4cca46b1f1ed92efd7d5ddee2e2274f485867202c581f68c32bd3278ab1188e978a53ea6851be2c14d87efe9ed78c71df95e1a7f7d6923b6703c00dc56b76fd582f945cd0c1951844ebe478a911fcab4", AllInternalsVisible = true)]
[assembly: InternalsVisibleTo( "Krypton.Ribbon, PublicKey=00240000048000009400000006020000002400005253413100040000010001001f208b6887f7b4f8fad6c30b9eca9849f09cbfbd37901e222f8e888331622c907dfa686c56389c95966b86b33f0dd0ab4cca46b1f1ed92efd7d5ddee2e2274f485867202c581f68c32bd3278ab1188e978a53ea6851be2c14d87efe9ed78c71df95e1a7f7d6923b6703c00dc56b76fd582f945cd0c1951844ebe478a911fcab4", AllInternalsVisible = true)]