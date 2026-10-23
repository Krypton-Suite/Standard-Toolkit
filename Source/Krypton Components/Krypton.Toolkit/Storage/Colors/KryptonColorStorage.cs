#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2025 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonColorStorage : Storage
{
    public override bool IsDefault { get; }

    public void Reset()
    {
        throw new NotImplementedException();
    }
}