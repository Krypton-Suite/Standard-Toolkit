#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2017 - 2023. All rights reserved. 
 *  
 *  Modified: Monday 12th April, 2021 @ 18:00 GMT
 *
 */
#endregion

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// Taken from Winforms (5 or 6) Github ($\src\System.Windows.Forms\src\AssemblyRef.cs)
// And then applied .NetFramework requirements for the designers
internal static class FXAssembly
{
    // NB: this must never-ever change to facilitate type-forwarding from
    // .NET Framework, if those are referenced in .NET project.
    // internal const string Version = "4.0.0.0";
    
    // SKC: Not sure what the above is saying, but it's does not seem to make the controls work !
#if NETFRAMEWORK || NETCOREAPP3_1
    internal const string Version = "4.0.0.0";
#elif NET5_0
    internal const string Version = "5.0.0.0";
#elif NET6_0
    internal const string Version = "6.0.0.0";
#elif NET7_0
    internal const string Version = "7.0.0.0";
#endif
}

internal static class AssemblyRef
{
    internal const string MicrosoftPublicKey = "b03f5f7f11d50a3a";
    internal const string SystemDesign = "System.Design, Version=" + FXAssembly.Version + ", Culture=neutral, PublicKeyToken=" + MicrosoftPublicKey;
#if NETFRAMEWORK
    internal const string SystemDrawingDesign = "System.Design, Version=" + FXAssembly.Version + ", Culture=neutral, PublicKeyToken=" + MicrosoftPublicKey;
#else
    internal const string SystemDrawingDesign = "System.Drawing.Design, Version=" + FXAssembly.Version + ", Culture=neutral, PublicKeyToken=" + MicrosoftPublicKey;
#endif
    internal const string SystemDrawing = "System.Drawing, Version=" + FXAssembly.Version + ", Culture=neutral, PublicKeyToken=" + MicrosoftPublicKey;
#if NETFRAMEWORK
    internal const string SystemWinformsDesign = "System.Design, Version=" + FXAssembly.Version + ", Culture=neutral, PublicKeyToken=" + MicrosoftPublicKey;
#else
    internal const string SystemWinformsDesign = "System.Windows.Forms.Design, Version=" + FXAssembly.Version + ", Culture=neutral, PublicKeyToken=" + MicrosoftPublicKey;
#endif
}
