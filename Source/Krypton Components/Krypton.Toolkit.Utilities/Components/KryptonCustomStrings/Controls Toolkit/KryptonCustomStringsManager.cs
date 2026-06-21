#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Provides designer and component tray access to global custom strings exposed by <see cref="KryptonCustomStrings"/>.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(KryptonCustomStringsManager), "ToolboxBitmaps.KryptonCustomStringsManager.bmp")]
[Designer(typeof(KryptonCustomStringsManagerDesigner))]
[DefaultProperty(nameof(CustomStrings))]
[Description(@"Access global custom string values that can be localised.")]
public sealed class KryptonCustomStringsManager : Component
{
    #region Identity

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonCustomStringsManager"/> class.
    /// </summary>
    public KryptonCustomStringsManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonCustomStringsManager"/> class.
    /// </summary>
    /// <param name="container">The container that owns this component.</param>
    public KryptonCustomStringsManager(IContainer container)
        : this()
    {
        container?.Add(this);
    }

    #endregion

    #region Public

    /// <summary>
    /// Gets the custom string values that can be localised.
    /// </summary>
    [Category(@"Data")]
    [Description(@"A collection of custom string values that can be localised.")]
    [MergableProperty(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public KryptonCustomStringValues CustomStrings => KryptonCustomStrings.Values;

    private bool ShouldSerializeCustomStrings() => !KryptonCustomStrings.Values.IsDefault;

    private void ResetCustomStrings() => KryptonCustomStrings.ResetValues();

    #endregion
}
