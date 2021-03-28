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