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
/// Provides the root XML element of a ribbon translations document during save or load
/// so applications can persist extra keys without a toolkit schema change.
/// </summary>
public sealed class RibbonTranslationsXmlEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RibbonTranslationsXmlEventArgs"/> class.
    /// </summary>
    /// <param name="ribbon">The ribbon being saved or loaded.</param>
    /// <param name="root">The <c>KryptonRibbonTranslations</c> root element.</param>
    public RibbonTranslationsXmlEventArgs(KryptonRibbon ribbon, XmlElement root)
    {
        if (ribbon == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(ribbon));
        }

        if (root == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(root));
        }

        Ribbon = ribbon;
        Root = root;
    }

    /// <summary>Gets the ribbon instance being saved or loaded.</summary>
    public KryptonRibbon Ribbon { get; }

    /// <summary>Gets the document root. Handlers may append or read extra child elements.</summary>
    public XmlElement Root { get; }
}

/// <summary>
/// Provides coverage produced while analyzing or importing a ribbon translations document.
/// </summary>
public sealed class RibbonTranslationsCoverageEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RibbonTranslationsCoverageEventArgs"/> class.
    /// </summary>
    /// <param name="coverage">The coverage result.</param>
    public RibbonTranslationsCoverageEventArgs(RibbonTranslationsCoverage coverage)
    {
        if (coverage == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(coverage));
        }

        Coverage = coverage;
    }

    /// <summary>Gets the coverage result.</summary>
    public RibbonTranslationsCoverage Coverage { get; }
}
