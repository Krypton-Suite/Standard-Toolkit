#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

#region AboutBox Definitions

#region Enum AboutToolkitPage

/// <summary>
/// Defines the pages of the About Toolkit dialog.
/// </summary>
internal enum AboutToolkitPage
{
    /// <summary>
    /// The general information page.
    /// </summary>
    GeneralInformation = 0,
    /// <summary>
    /// The Discord page.
    /// </summary>
    Discord = 1,
    /// <summary>
    /// The developer information page.
    /// </summary>
    DeveloperInformation = 2,
    /// <summary>
    /// The versions page.
    /// </summary>
    Versions = 3
}

#endregion

#region Enum AboutBoxFileInformationPage

/// <summary>
/// Defines the pages of the About Box file information dialog.
/// </summary>
public enum AboutBoxFileInformationPage
{
    /// <summary>
    /// The application page.
    /// </summary>
    Application = 0,
    /// <summary>
    /// The assemblies page.
    /// </summary>
    Assemblies = 1,
    /// <summary>
    /// The assembly details page.
    /// </summary>
    AssemblyDetails = 2
}

#endregion

#region Enum AboutBoxPage

/// <summary>
/// Defines the pages of the About Box dialog.
/// </summary>
public enum AboutBoxPage
{
    /// <summary>
    /// The general information page.
    /// </summary>
    GeneralInformation = 0,
    /// <summary>
    /// The description page.
    /// </summary>
    Description = 1,
    /// <summary>
    /// The file information page.
    /// </summary>
    FileInformation = 2,
    /// <summary>
    /// The theme page.
    /// </summary>
    Theme = 3,
    /// <summary>
    /// The toolkit information page.
    /// </summary>
    ToolkitInformation = 4
}

#endregion

#endregion