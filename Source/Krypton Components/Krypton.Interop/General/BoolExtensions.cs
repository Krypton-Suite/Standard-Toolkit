namespace Krypton.Toolkit;

/// <summary>
/// Extension methods for working with the PI.BOOL type and standard boolean values.
/// These methods provide convenient conversions between native Windows BOOL values and .NET bool values.
/// </summary>
internal static class BoolExtensions
{
    /// <summary>
    /// Determines whether the BOOL value represents true.
    /// </summary>
    /// <param name="b">The BOOL value to check</param>
    /// <returns>True if the BOOL value is not FALSE; otherwise, false</returns>
    public static bool IsTrue(this PI.BOOL b) => b != PI.BOOL.FALSE;

    /// <summary>
    /// Determines whether the BOOL value represents false.
    /// </summary>
    /// <param name="b">The BOOL value to check</param>
    /// <returns>True if the BOOL value is FALSE; otherwise, false</returns>
    public static bool IsFalse(this PI.BOOL b) => b == PI.BOOL.FALSE;

    /// <summary>
    /// Converts a .NET boolean value to a Windows BOOL value.
    /// </summary>
    /// <param name="b">The boolean value to convert</param>
    /// <returns>PI.BOOL.TRUE if the boolean is true; otherwise, PI.BOOL.FALSE</returns>
    public static PI.BOOL ToBOOL(this bool b) => b ? PI.BOOL.TRUE : PI.BOOL.FALSE;
}