#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Marks a ribbon object that can carry a stable, non-localized identity for
/// <c>RibbonTranslations.xml</c> round-trips.
/// </summary>
public interface IRibbonTranslationIdentity
{
    /// <summary>
    /// Gets or sets a consumer-owned identity used when matching translation file nodes.
    /// Empty means fall back to <c>Site.Name</c>, <c>UniqueName</c>, <c>ContextName</c>, or index.
    /// </summary>
    string TranslationId { get; set; }
}
