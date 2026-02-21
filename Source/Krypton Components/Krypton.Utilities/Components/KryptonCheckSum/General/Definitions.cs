#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

#region Enum CheckSumStatus

/// <summary>
/// Specifies the status of a checksum operation.
/// </summary>
/// <remarks>Use this enumeration to represent the current state of a checksum process, such as computing,
/// verifying, or saving. The values indicate whether the operation is in progress, completed, canceled, or waiting.
/// This status can be used to monitor or control workflow in scenarios involving file integrity checks or similar
/// operations.</remarks>
public enum CheckSumStatus
{
    /// <summary>
    /// Represents a state indicating that a cancellation process is underway.
    /// </summary>
    Cancelling = 0,
    /// <summary>
    /// Indicates that the operation was canceled before completion.
    /// </summary>
    Canceled = 1,
    /// <summary>
    /// Represents the computing category or type within the enumeration.
    /// </summary>
    Computing = 2,
    /// <summary>
    /// Indicates that the operation or resource is ready for use.
    /// </summary>
    Ready = 3,
    /// <summary>
    /// Represents the saving hash file.
    /// </summary>
    Saving = 4,
    /// <summary>
    /// Represents the state indicating that the process or resource is in the opening phase.
    /// </summary>
    Opening = 5,
    /// <summary>
    /// Indicates that the process is currently in the verification stage.
    /// </summary>
    Verifying = 6,
    /// <summary>
    /// Represents an invalid hash or state, indicating that the checksum operation has encountered an error or is not valid.
    /// </summary>
    Invalid = 7,
    /// <summary>
    /// Indicates that the hash is valid, meaning the checksum operation has completed successfully and the integrity of the data is confirmed.
    /// </summary>
    Valid = 8,
    /// <summary>
    /// Indicates that the operation or process is currently waiting for a condition to be met or for further action.
    /// </summary>
    Waiting = 9,
}

#endregion

#region Enum SafeNETAndNewerSupportedHashAlgorithms

/// <summary>
/// Specifies hash algorithms supported by .NET and newer cryptographic providers.
/// </summary>
/// <remarks>This enumeration includes commonly used hash algorithms for cryptographic operations, such as message
/// integrity and digital signatures. The availability of these algorithms may depend on the underlying platform or
/// provider. When selecting an algorithm, consider security recommendations, as some algorithms (such as MD5 and SHA1)
/// are considered deprecated for most security-sensitive applications.</remarks>
public enum SafeNETAndNewerSupportedHashAlgorithms
{
    /// <summary>
    /// Represents the abstract base class for implementations of the MD5 hash algorithm.
    /// </summary>
    /// <remarks>Use this class to compute a 128-bit hash value for data. MD5 is commonly used for integrity
    /// checks but is not recommended for security-sensitive applications due to known vulnerabilities. Derived classes
    /// provide concrete implementations of the algorithm.</remarks>
    MD5,
    /// <summary>
    /// Represents the abstract base class for computing SHA-1 hash values.
    /// </summary>
    /// <remarks>Use this class to compute a secure hash for data using the SHA-1 algorithm. SHA-1 is
    /// considered deprecated for most security purposes due to known vulnerabilities; prefer SHA-256 or stronger
    /// algorithms for new applications. This class provides methods for hashing data streams and byte arrays, and is
    /// typically used for integrity checks rather than cryptographic security.</remarks>
    SHA1,
    /// <summary>
    /// Represents the abstract base class for computing SHA-256 hash values.
    /// </summary>
    /// <remarks>Use this class to generate a cryptographic hash for data using the SHA-256 algorithm. Derived
    /// classes provide implementations for different platforms. This class is thread-safe for static methods, but
    /// instance members are not guaranteed to be thread-safe.</remarks>
    SHA256,
    /// <summary>
    /// Represents the SHA-384 cryptographic hash algorithm for computing a 384-bit hash value.
    /// </summary>
    /// <remarks>Use this class to generate a hash for data integrity verification or digital signatures.
    /// SHA-384 is suitable for scenarios requiring a higher level of security than SHA-256. This algorithm is not
    /// intended for password hashing or encryption.</remarks>
    SHA384,
    /// <summary>
    /// Represents the abstract base class for computing SHA-512 hash values.
    /// </summary>
    /// <remarks>Use this class to implement custom SHA-512 hashing algorithms or to access SHA-512
    /// functionality in derived classes. SHA-512 is part of the SHA-2 family of cryptographic hash algorithms and
    /// produces a 512-bit hash value. This class is thread-safe for static methods, but instance members are not
    /// guaranteed to be thread-safe.</remarks>
    SHA512
}

#endregion

#region Enum SupportedHashAlgorithms

/// <summary>
/// Specifies the set of hash algorithms supported for cryptographic operations.
/// </summary>
/// <remarks>Use this enumeration to select a hash algorithm when performing hashing or digital signature
/// operations. The available algorithms include both legacy and modern options; prefer SHA256 or stronger algorithms
/// for security-sensitive applications, as MD5 and SHA1 are considered deprecated due to known
/// vulnerabilities.</remarks>
public enum SupportedHashAlgorithims
{
    /// <summary>
    /// Represents the abstract base class for computing MD5 hash values.
    /// </summary>
    /// <remarks>Use this class to generate a 128-bit hash value for data integrity verification. Derived
    /// classes implement the actual MD5 algorithm. MD5 is not recommended for security-sensitive applications due to
    /// known vulnerabilities.</remarks>
    MD5,
    /// <summary>
    /// Represents the abstract base class for computing SHA-1 hash values.
    /// </summary>
    /// <remarks>Use this class to compute a secure hash for data using the SHA-1 algorithm. Derived classes
    /// implement the actual hashing logic. SHA-1 is considered deprecated for most security purposes due to known
    /// vulnerabilities; prefer SHA-256 or stronger algorithms for new applications.</remarks>
    SHA1,
    /// <summary>
    /// Represents the abstract base class for computing SHA-256 hash values.
    /// </summary>
    /// <remarks>Use this class to generate a cryptographic hash for data using the SHA-256 algorithm. Derived
    /// classes provide implementations for different platforms. This class is thread-safe for static methods, but
    /// instance methods are not guaranteed to be thread-safe.</remarks>
    SHA256,
    /// <summary>
    /// Represents the SHA-384 cryptographic hash algorithm for computing a 384-bit hash value.
    /// </summary>
    /// <remarks>Use this class to generate a hash for data integrity verification or digital signatures.
    /// SHA-384 is suitable for scenarios requiring a higher level of security than SHA-256. This algorithm is not
    /// intended for storing passwords; use a password-based key derivation function for that purpose.</remarks>
    SHA384,
    /// <summary>
    /// Represents the abstract base class for computing SHA-512 hash values.
    /// </summary>
    /// <remarks>Use this class to implement custom SHA-512 hashing algorithms or to access SHA-512
    /// functionality in derived classes. SHA-512 is part of the SHA-2 family of cryptographic hash algorithms and
    /// provides a 512-bit hash value suitable for security-sensitive applications.</remarks>
    SHA512,
    /// <summary>
    /// Represents the RIPEMD-160 cryptographic hash algorithm.
    /// </summary>
    /// <remarks>RIPEMD-160 is a 160-bit hash function designed for use in cryptographic applications. It is
    /// commonly used for data integrity verification and digital signatures. This class provides methods to compute the
    /// hash value for data streams and byte arrays. For most security scenarios, SHA-2 family algorithms are
    /// recommended over RIPEMD-160 due to stronger cryptographic guarantees.</remarks>
    RIPEMD160
}

#endregion