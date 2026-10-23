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
global using System.Globalization;
global using System.IO;
global using System.Linq;
global using System.Reflection;
global using System.Runtime.InteropServices;
global using System.Text;
global using System.Windows.Forms;
global using System.Windows.Forms.Design;
global using System.Windows.Forms.VisualStyles;

global using Krypton.Toolkit;

global using Krypton.Ribbon.Resources;

global using System.Runtime.CompilerServices;

[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: Dependency(nameof(System), LoadHint.Always)]
[assembly: Dependency(@"System.Drawing", LoadHint.Always)]
[assembly: Dependency(@"System.Windows.Forms", LoadHint.Always)]
[assembly: Dependency(@"Krypton.Toolkit", LoadHint.Always)]

// Public key value needs to be the full key. Before, this was the PublicKeyToken value.
// See: https://stackoverflow.com/questions/106880/internalsvisibleto-attribute-isnt-working/107958#107958
[assembly: InternalsVisibleTo( "Krypton.Toolkit, PublicKey=00240000048000009400000006020000002400005253413100040000010001001f208b6887f7b4f8fad6c30b9eca9849f09cbfbd37901e222f8e888331622c907dfa686c56389c95966b86b33f0dd0ab4cca46b1f1ed92efd7d5ddee2e2274f485867202c581f68c32bd3278ab1188e978a53ea6851be2c14d87efe9ed78c71df95e1a7f7d6923b6703c00dc56b76fd582f945cd0c1951844ebe478a911fcab4", AllInternalsVisible = true)]
