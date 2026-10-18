#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Collection editor for <see cref="KryptonFormCaptionItemCollection"/>.
/// </summary>
public sealed class KryptonFormCaptionItemCollectionEditor : KryptonDesignerStandardCollectionEditor
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonFormCaptionItemCollectionEditor"/> class.
    /// </summary>
    public KryptonFormCaptionItemCollectionEditor()
        : base(typeof(KryptonFormCaptionItem))
    {
    }
}
