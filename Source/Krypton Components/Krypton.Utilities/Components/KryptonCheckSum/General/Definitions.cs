using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton.Utilities;

#region Enum CheckSumStatus

public enum CheckSumStatus
{
    Cancelling = 0,
    Canceled = 1,
    Computing = 2,
    Ready = 3,
    Saving = 4,
    Opening = 5,
    Verifying = 6,
    Invalid = 7,
    Valid = 8,
    Waiting = 9,
}

#endregion

#region Enum SafeNETAndNewerSupportedHashAlgorithms

public enum SafeNETAndNewerSupportedHashAlgorithms
{
    MD5,
    SHA1,
    SHA256,
    SHA384,
    SHA512
}

#endregion

#region Enum SupportedHashAlgorithms

public enum SupportedHashAlgorithims
{
    MD5,
    SHA1,
    SHA256,
    SHA384,
    SHA512,
    RIPEMD160
}

#endregion