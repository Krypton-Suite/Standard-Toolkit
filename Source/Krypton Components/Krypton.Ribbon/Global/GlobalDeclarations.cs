#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2022. All rights reserved. 
 *
 */
#endregion

// This file holds the global definitions >= C# 10

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
global using System.Drawing.Imaging;
global using System.Globalization;
global using System.IO;
global using System.Reflection;
global using System.Runtime.InteropServices;
global using System.Text;
global using System.Windows.Forms;
global using System.Windows.Forms.Design;
global using System.Windows.Forms.VisualStyles;

global using Krypton.Toolkit;

using System.Runtime.CompilerServices;

[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: Dependency("System", LoadHint.Always)]
[assembly: Dependency("System.Drawing", LoadHint.Always)]
[assembly: Dependency("System.Windows.Forms", LoadHint.Always)]
[assembly: Dependency("Krypton.Toolkit", LoadHint.Always)]
