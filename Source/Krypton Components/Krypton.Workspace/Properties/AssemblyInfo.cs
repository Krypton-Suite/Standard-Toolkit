#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2021. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

[assembly: AssemblyCopyright("© Component Factory Pty Ltd, 2006 - 2016. Then modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV) 2017 - 2021. All rights reserved.")]
[assembly: AssemblyDefaultAlias("Krypton.Workspace.dll")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: StringFreezing]
[assembly: ComVisible(true)]
[assembly: CLSCompliant(true)]
[assembly: AllowPartiallyTrustedCallers()]
[assembly: Dependency("System", LoadHint.Always)]
[assembly: Dependency("System.Drawing", LoadHint.Always)]
[assembly: Dependency("System.Windows.Forms", LoadHint.Always)]
[assembly: Dependency("System.XML", LoadHint.Always)]
[assembly: Dependency("Krypton.Toolkit", LoadHint.Always)]
[assembly: Dependency("Krypton.Navigator", LoadHint.Always)]
#if !NET35
[assembly: SecurityRules(SecurityRuleSet.Level1)]
#endif