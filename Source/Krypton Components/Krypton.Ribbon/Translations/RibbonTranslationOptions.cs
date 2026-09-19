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
/// Options for exporting or importing a <see cref="KryptonRibbon"/> translations document.
/// </summary>
public sealed class RibbonTranslationOptions
{
    /// <summary>
    /// When <see langword="true"/>, emit every localizable string even when it matches the designer default.
    /// </summary>
    public bool IncludeDefaults { get; set; }

    /// <summary>
    /// When <see langword="true"/>, include the process-wide <c>KryptonManager.Strings.RibbonStrings</c> chrome block.
    /// </summary>
    public bool IncludeChrome { get; set; }

    /// <summary>
    /// When <see langword="true"/>, include tooltip title/body/heading/description strings. Defaults to <see langword="true"/>.
    /// </summary>
    public bool IncludeToolTips { get; set; } = true;

    /// <summary>
    /// When <see langword="true"/>, include KeyTip strings. Defaults to <see langword="true"/>.
    /// </summary>
    public bool IncludeKeyTips { get; set; } = true;

    /// <summary>
    /// When <see langword="true"/>, include editor content such as text-box <c>Text</c>. Defaults to <see langword="false"/>.
    /// </summary>
    public bool IncludeContentStrings { get; set; }

    /// <summary>
    /// When <see langword="true"/> on import, reset exportable strings on existing nodes to defaults before applying the file.
    /// </summary>
    public bool ResetFirst { get; set; } = true;

    /// <summary>
    /// Optional designer change service so imported values are serialized into the host form.
    /// </summary>
    public IComponentChangeService? ChangeService { get; set; }
}
