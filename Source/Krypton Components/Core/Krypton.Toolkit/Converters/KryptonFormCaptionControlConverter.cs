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
/// Lists form controls for <see cref="KryptonFormCaptionItem.Control"/> in the designer.
/// </summary>
internal sealed class KryptonFormCaptionControlConverter : ReferenceConverter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonFormCaptionControlConverter"/> class.
    /// </summary>
    public KryptonFormCaptionControlConverter()
        : base(typeof(Control))
    {
    }

    /// <inheritdoc />
    protected override bool IsValueAllowed(ITypeDescriptorContext context, object value) =>
        value is Control control && !(control is Form);
}
