#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2025. All rights reserved. 
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
global using System.IO;
global using System.Linq;
global using System.Reflection;
global using System.Runtime.InteropServices;
global using System.Text;
global using System.Windows.Forms;
global using System.Windows.Forms.Design;

global using Krypton.Toolkit;

global using System.Runtime.CompilerServices;

global using Krypton.Navigator.Resources;

[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: Dependency(nameof(System), LoadHint.Always)]
[assembly: Dependency("System.Drawing", LoadHint.Always)]
[assembly: Dependency("System.Windows.Forms", LoadHint.Always)]
[assembly: Dependency("Krypton.Toolkit", LoadHint.Always)]