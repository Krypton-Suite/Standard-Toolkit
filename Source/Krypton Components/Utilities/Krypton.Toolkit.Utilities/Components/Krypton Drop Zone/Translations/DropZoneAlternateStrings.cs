#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

public class DropZoneAlternateStrings : Storage
{
    #region Static Fields

    private const string DEFAULT_DROP_ZONE_TEXT = "Drop Here";

    #endregion

    #region Instance Fields

    private KryptonDropZoneAlternate _dropZoneAlternate;

    #endregion

    #region Identity

    public DropZoneAlternateStrings(KryptonDropZoneAlternate dropZoneAlternate)
    {
        _dropZoneAlternate = dropZoneAlternate;

        Reset();
    }

    #endregion

    [Localizable(true)]
    [Category("Appearance")]
    [Description("The text displayed in the drop zone when no files are present.")]
    [DefaultValue(DEFAULT_DROP_ZONE_TEXT)]
    public static string DropZoneText { get; set; }

    public override bool IsDefault => throw new NotImplementedException();


    #region Implementation

    public void Reset()
    {
        DropZoneText = DEFAULT_DROP_ZONE_TEXT;
    }

    #endregion
}