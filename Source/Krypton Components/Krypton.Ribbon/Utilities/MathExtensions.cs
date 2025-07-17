#region Original MIT License

/*
 * Copyright(C) 2018 Michael Winsor
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 *
 * Created: September 28, 2018 9:03:32 PM
 */

#endregion

#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Fluent extensions for mathematical operations on various numeric types.
/// </summary>
/// <remarks>
/// <para>
/// This provides a fluent interface to numeric types (e.g. <see cref="float"/>, <see cref="double"/>, <see cref="int"/>, etc...) that will expose common mathematical functions without relying on 
/// methods from <see cref="System.Math"/>. This makes it easy to chain together several functions to retrieve a result, for example:
/// </para>
/// <code language="csharp">
/// <![CDATA[
/// int myValueTooBig = 150;
/// int myValueTooSmall = 5;
/// 
/// // Ensure the value does not exceed 100, but is greater than 10.
/// Console.WriteLine($"{myValueTooBig.Min(100).Max(10)}, {myValueTooSmall.Min(100).Max(10)}");  
///
/// // Outputs: 100, 10
/// ]]>
/// </code>
/// <para>
/// Other mathematical functions are included, such as <see cref="Sin(float)"/>, <see cref="Cos(float)"/>, <see cref="Tan(float)"/>, etc... 
/// </para>
/// <para>
/// CS3002 Warning<br/>
/// Extensions handling NON CLS-compliant data types have been disabled.<br/>
/// This goes for all unsigned types, except byte.<br/>
/// The attribute [System.CLSCompliant(true)] has been added to the class header<br/>
/// See: <see href="https://learn.microsoft.com/en-us/dotnet/csharp/misc/cs3002?f1url=%3FappId%3Droslyn%26k%3Dk(CS3002)"></see>
/// </para>
/// </remarks>
[System.CLSCompliant(true)]  
public static class MathExtensions
{
    #region Constants.
    // Constant containing the value used to convert degrees to radians.
    private const float DEG_CONVERT = ((float)Math.PI / 180.0f);
    // Constant containing the value used to convert radians to degrees.
    private const float RAD_CONVERT = (180.0f / (float)Math.PI);

    /// <summary>
    /// Constant value for &#x03C0;.
    /// </summary>
    public const float PI = 3.141593f;
    #endregion

    #region Implementation
    /// <summary>
    /// Function to perform an approximation of a sine calculation.
    /// </summary>
    /// <param name="rads">The angle, in radians.</param>
    /// <returns>The sine value for the angle.</returns>
    /// <remarks>
    /// <para>
    /// This method will produce an approximation of the value returned by <see cref="Sin(float)"/>. Because this is an approximation, this method should not be used when accuracy is important. 
    /// </para>
    /// <para>
    /// This version of the sine function has better performance than the <see cref="Sin(float)"/> method, and as such, should be used in performance intensive situations.
    /// </para>
    /// <para>
    /// This code was adapted from the <a href="http://www.gamedev.net" target="_blank">GameDev.Net</a> post by L.Spiro found here: 
    /// <a href="http://www.gamedev.net/topic/681723-faster-sin-and-cos/" target="_blank">http://www.gamedev.net/topic/681723-faster-sin-and-cos/</a>.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float FastSin(this float rads)
    {
        int i32I = (int)(rads * 0.31830988618379067153776752674503);  // 1 / PI.
        double radians = rads - ((i32I) * 3.1415926535897932384626433832795);

        double fX2 = radians * radians;

        return (float)(((i32I & 1) == 1)
            ? -radians * ((1.00000000000000000000e+00) +
                          (fX2 * ((-1.66666671633720397949e-01) +
                                  (fX2 * ((8.33333376795053482056e-03) +
                                          (fX2 * ((-1.98412497411482036114e-04) +
                                                  (fX2 * ((2.75565571428160183132e-06) +
                                                          (fX2 * ((-2.50368472620721149724e-08) +
                                                                  (fX2 * ((1.58849267073435385100e-10) +
                                                                          (fX2 * (-6.58925550841432672300e-13)))))))))))))))
            : radians * ((1.00000000000000000000e+00) +
                         (fX2 * ((-1.66666671633720397949e-01) +
                                 (fX2 * ((8.33333376795053482056e-03) +
                                         (fX2 * ((-1.98412497411482036114e-04) +
                                                 (fX2 * ((2.75565571428160183132e-06) +
                                                         (fX2 * ((-2.50368472620721149724e-08) +
                                                                 (fX2 * ((1.58849267073435385100e-10) +
                                                                         (fX2 * (-6.58925550841432672300e-13))))))))))))))));
    }

    /// <summary>
    /// Function to perform an approximation of a cosine calculation.
    /// </summary>
    /// <param name="rads">The angle, in radians.</param>
    /// <returns>The cosine value for the angle.</returns>
    /// <remarks>
    /// <para>
    /// This method will produce an approximation of the value returned by <see cref="Cos(float)"/>. Because this is an approximation, this method should not be used when accuracy is important. 
    /// </para>
    /// <para>
    /// This version of the cosine function has better performance than the <see cref="Cos(float)"/> method, and as such, should be used in performance intensive situations.
    /// </para>
    /// <para>
    /// This code was adapted from the <a href="http://www.gamedev.net" target="_blank">GameDev.Net</a> post by L.Spiro found here: 
    /// <a href="http://www.gamedev.net/topic/681723-faster-sin-and-cos/" target="_blank">http://www.gamedev.net/topic/681723-faster-sin-and-cos/</a>.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float FastCos(this float rads)
    {
        int i32I = (int)(rads * 0.31830988618379067153776752674503);  // 1 / PI.
        double radians = rads - ((i32I) * 3.1415926535897932384626433832795);

        double fX2 = radians * radians;

        return (float)(((i32I & 1) == 1)
            ? -(1.00000000000000000000e+00) -
              (fX2 * ((-5.00000000000000000000e-01) +
                      (fX2 * ((4.16666641831398010254e-02) +
                              (fX2 * ((-1.38888671062886714935e-03) +
                                      (fX2 * ((2.48006890615215525031e-05) +
                                              (fX2 * ((-2.75369927749125054106e-07) +
                                                      (fX2 * ((2.06207229069832465029e-09) +
                                                              (fX2 * (-9.77507137733812925262e-12))))))))))))))
            : (1.00000000000000000000e+00) +
              (fX2 * ((-5.00000000000000000000e-01) +
                      (fX2 * ((4.16666641831398010254e-02) +
                              (fX2 * ((-1.38888671062886714935e-03) +
                                      (fX2 * ((2.48006890615215525031e-05) +
                                              (fX2 * ((-2.75369927749125054106e-07) +
                                                      (fX2 * ((2.06207229069832465029e-09) +
                                                              (fX2 * (-9.77507137733812925262e-12)))))))))))))));
    }

    /// <summary>
    /// Function to return the maximum value between two <see cref="byte"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The larger of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte Max(this byte value1, byte value2) => (value1 > value2) ? value1 : value2;

    /// <summary>
    /// Function to return the minimum value between two <see cref="byte"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The smaller of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte Min(this byte value1, byte value2) => (value1 < value2) ? value1 : value2;

    /* Disabled CLS Non Compliant
    /// <summary>
    /// Function to return the maximum value between two <see cref="ushort"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The larger of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort Max(this ushort value1, ushort value2) => (value1 > value2) ? value1 : value2;
    */

    /* Disabled CLS Non Compliant
    /// <summary>
    /// Function to return the minimum value between two <see cref="ushort"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The smaller of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort Min(this ushort value1, ushort value2) => (value1 < value2) ? value1 : value2;
    */
    /// <summary>
    /// Function to return the maximum value between two <see cref="short"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The larger of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Max(this short value1, short value2) => (value1 > value2) ? value1 : value2;

    /// <summary>
    /// Function to return the minimum value between two <see cref="short"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The smaller of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Min(this short value1, short value2) => (value1 < value2) ? value1 : value2;

    /* Disabled CLS Non Compliant
    /// <summary>
    /// Function to return the maximum value between two <see cref="uint"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The larger of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Max(this uint value1, uint value2) => (value1 > value2) ? value1 : value2;
    */

    /* Disabled CLS Non Compliant
    /// <summary>
    /// Function to return the minimum value between two <see cref="uint"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The smaller of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Min(this uint value1, uint value2) => (value1 < value2) ? value1 : value2;
    */

    /// <summary>
    /// Function to return the maximum value between two <see cref="int"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The larger of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Max(this int value1, int value2) => (value1 > value2) ? value1 : value2;

    /// <summary>
    /// Function to return the minimum value between two <see cref="int"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The smaller of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Min(this int value1, int value2) => (value1 < value2) ? value1 : value2;

    /* Disabled CLS Non Compliant
    /// <summary>
    /// Function to return the maximum value between two <see cref="ulong"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The larger of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Max(this ulong value1, ulong value2) => (value1 > value2) ? value1 : value2;
    */

    /* Disabled CLS Non Compliant
    /// <summary>
    /// Function to return the minimum value between two <see cref="ulong"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The smaller of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Min(this ulong value1, ulong value2) => (value1 < value2) ? value1 : value2;
    */

    /// <summary>
    /// Function to return the maximum value between two <see cref="long"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The larger of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Max(this long value1, long value2) => (value1 > value2) ? value1 : value2;

    /// <summary>
    /// Function to return the minimum value between two <see cref="long"/> values..
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The smaller of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Min(this long value1, long value2) => (value1 < value2) ? value1 : value2;

    /// <summary>
    /// Function to return the maximum value between two <see cref="float"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The larger of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Max(this float value1, float value2) => (value1 > value2) ? value1 : value2;

    /// <summary>
    /// Function to return the minimum value between two <see cref="float"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The smaller of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Min(this float value1, float value2) => (value1 < value2) ? value1 : value2;

    /// <summary>
    /// Function to return the maximum value between two <see cref="double"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The larger of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Max(this double value1, double value2) => (value1 > value2) ? value1 : value2;

    /// <summary>
    /// Function to return the minimum value between two <see cref="double"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The smaller of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Min(this double value1, double value2) => (value1 < value2) ? value1 : value2;

    /// <summary>
    /// Function to return the maximum value between two <see cref="decimal"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The larger of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Max(this decimal value1, decimal value2) => (value1 > value2) ? value1 : value2;

    /// <summary>
    /// Function to return the minimum value between two <see cref="decimal"/> values.
    /// </summary>
    /// <param name="value1">The first value to test.</param>
    /// <param name="value2">The second value to test.</param>
    /// <returns>The smaller of the two values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Min(this decimal value1, decimal value2) => (value1 < value2) ? value1 : value2;

    /// <summary>
    /// Function to return the absolute value of a <see cref="float"/> value.
    /// </summary>
    /// <param name="value">Value to evaluate.</param>
    /// <returns>The absolute value of <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Abs(this short value) => Math.Abs(value);

    /// <summary>
    /// Function to return the absolute value of an <see cref="int"/> value.
    /// </summary>
    /// <param name="value">Value to evaluate.</param>
    /// <returns>The absolute value of <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Abs(this int value) => Math.Abs(value);

    /// <summary>
    /// Function to return the absolute value of a <see cref="long"/> value.
    /// </summary>
    /// <param name="value">Value to evaluate.</param>
    /// <returns>The absolute value of <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Abs(this long value) => Math.Abs(value);

    /// <summary>
    /// Function to return the absolute value of a <see cref="double"/> value.
    /// </summary>
    /// <param name="value">Value to evaluate.</param>
    /// <returns>The absolute value of <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Abs(this double value) => Math.Abs(value);

    /// <summary>
    /// Function to return the absolute value of a <see cref="decimal"/> value.
    /// </summary>
    /// <param name="value">Value to evaluate.</param>
    /// <returns>The absolute value of <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Abs(this decimal value) => Math.Abs(value);

    /// <summary>
    /// Function to return the absolute value of a <see cref="float"/> value.
    /// </summary>
    /// <param name="value">Value to evaluate.</param>
    /// <returns>The absolute value of <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Abs(this float value) => Math.Abs(value);

    /// <summary>
    /// Function to round a <see cref="float"/> value to the nearest whole or fractional number.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <param name="decimalCount">[Optional] The number of decimal places to round to.</param>
    /// <param name="rounding">[Optional] The type of rounding to perform.</param>
    /// <returns>The <see cref="float"/> value rounded to the nearest whole number.</returns>
    /// <remarks>  
    /// See <see cref="System.Math.Round(double,int,MidpointRounding)"/> for more information.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Round(this float value, int decimalCount = 0, MidpointRounding rounding = MidpointRounding.ToEven) => (float)(Math.Round(value, decimalCount, rounding));

    /// <summary>
    /// Function to round a <see cref="decimal"/> value to the nearest whole or fractional number.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <param name="decimalCount">[Optional] The number of decimal places to round to.</param>
    /// <param name="rounding">[Optional] The type of rounding to perform.</param>
    /// <returns>The <see cref="float"/> value rounded to the nearest whole number.</returns>
    /// <remarks>  
    /// See <see cref="System.Math.Round(decimal,int,MidpointRounding)"/> for more information.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Round(this decimal value, int decimalCount = 0, MidpointRounding rounding = MidpointRounding.ToEven) => Math.Round(value, decimalCount, rounding);

    /// <summary>
    /// Function to round a <see cref="double"/> value to the nearest whole or fractional number.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <param name="decimalCount">[Optional] The number of decimal places to round to.</param>
    /// <param name="rounding">[Optional] The type of rounding to perform.</param>
    /// <returns>The <see cref="float"/> value rounded to the nearest whole number.</returns>
    /// <remarks>  
    /// See <see cref="System.Math.Round(double,int,MidpointRounding)"/> for more information.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Round(this double value, int decimalCount = 0, MidpointRounding rounding = MidpointRounding.ToEven) => Math.Round(value, decimalCount, rounding);

    /// <summary>
    /// Function to convert a <see cref="float"/> value representing a radian into an angle in degrees.
    /// </summary>
    /// <param name="radians">The value to convert.</param>
    /// <returns>The angle in degrees.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ToDegrees(this float radians) => radians * RAD_CONVERT;

    /// <summary>
    /// Function to convert a <see cref="float"/> value representing an angle in degrees into a radian value.
    /// </summary>
    /// <param name="degrees">The angle value to convert.</param>
    /// <returns>The angle in radians.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ToRadians(this float degrees) => degrees * DEG_CONVERT;

    /// <summary>
    /// Function to convert a <see cref="decimal"/> value representing a radian into an angle in degrees.
    /// </summary>
    /// <param name="radians">The value to convert.</param>
    /// <returns>The angle in degrees.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal ToDegrees(this decimal radians) => radians * (decimal)RAD_CONVERT;

    /// <summary>
    /// Function to convert a <see cref="decimal"/> value representing an angle in degrees into a radian value.
    /// </summary>
    /// <param name="degrees">The angle value to convert.</param>
    /// <returns>The angle in radians.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal ToRadians(this decimal degrees) => degrees * (decimal)DEG_CONVERT;


    /// <summary>
    /// Function to convert a <see cref="double"/> value representing a radian into an angle in degrees.
    /// </summary>
    /// <param name="radians">The value to convert.</param>
    /// <returns>The angle in degrees.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ToDegrees(this double radians) => radians * RAD_CONVERT;

    /// <summary>
    /// Function to convert a <see cref="double"/> value representing an angle in degrees into a radian value.
    /// </summary>
    /// <param name="degrees">The angle value to convert.</param>
    /// <returns>The angle in radians.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ToRadians(this double degrees) => degrees * DEG_CONVERT;

    /// <summary>
    /// Function to determine if a <see cref="float"/> value is equal to another within a given tolerance.
    /// </summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <param name="epsilon">[Optional] The epsilon representing the error tolerance.</param>
    /// <returns><b>true</b> if equal, <b>false</b> if not.</returns>
    /// <remarks>
    /// <para>
    /// Floating point values are prone to error buildup due to their limited precision. Therefore, when performing a comparison between two floating point values: <c>4.23212f == 4.23212f</c> may 
    /// actually be <c>4.232120000005422f == 4.232120000005433f</c>. Obviously, the result will not be <b>true</b> when the values are actually considered equal. This method ensures that the comparison will 
    /// return true by removing the error through the <paramref name="epsilon"/> parameter.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EqualsEpsilon(this float left, float right, float epsilon = 1e-06f) => Abs(right - left) <= epsilon;

    /// <summary>
    /// Function to determine if a <see cref="double"/> value is equal to another within a given tolerance.
    /// </summary>
    /// <param name="left">The left value to compare.</param>
    /// <param name="right">The right value to compare.</param>
    /// <param name="epsilon">[Optional] The epsilon representing the error tolerance.</param>
    /// <returns><b>true</b> if equal, <b>false</b> if not.</returns>
    /// <remarks>
    /// <para>
    /// Floating point values are prone to error buildup due to their limited precision. Therefore, when performing a comparison between two floating point values: <c>4.23212f == 4.23212f</c> may 
    /// actually be <c>4.232120000005422f == 4.232120000005433f</c>. Obviously, the result will not be <b>true</b> when the values are actually considered equal. This method ensures that the comparison will 
    /// return true by removing the error through the <paramref name="epsilon"/> parameter.
    /// </para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EqualsEpsilon(this double left, double right, double epsilon = 1e-12) => Abs(right - left) <= epsilon;

    /// <summary>
    /// Function to return the inverse of the square root for a <see cref="double"/> value.
    /// </summary>
    /// <param name="value">The value to get the inverse square root of.</param>
    /// <returns>The inverted square root of the value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double InverseSqrt(this double value) => 1.0 / Math.Sqrt(value);

    /// <summary>
    /// Function to return the inverse of the square root for a <see cref="float"/> value.
    /// </summary>
    /// <param name="value">The value to get the inverse square root of.</param>
    /// <returns>The inverted square root of the value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InverseSqrt(this float value) => 1.0f / (float)Math.Sqrt(value);

    /// <summary>
    /// Function to return the square root for a <see cref="double"/> value.
    /// </summary>
    /// <param name="value">The value to get the square root of.</param>
    /// <returns>The square root of the value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Sqrt(this double value) => Math.Sqrt(value);

    /// <summary>
    /// Function to return the square root for a <see cref="float"/> value.
    /// </summary>
    /// <param name="value">The value to get the square root of.</param>
    /// <returns>The square root of the value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Sqrt(this float value) => (float)Math.Sqrt(value);

    /// <summary>
    /// Function to return the sine value of a <see cref="float"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="angle">The angle, in radians.</param>
    /// <returns>The sine value of the <paramref name="angle"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Sin(this float angle) => (float)Math.Sin(angle);

    /// <summary>
    /// Function to return the sine value of a <see cref="decimal"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="angle">The angle, in radians.</param>
    /// <returns>The sine value of the <paramref name="angle"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Sin(this decimal angle) => (decimal)Math.Sin((double)angle);

    /// <summary>
    /// Function to return the sine value of a <see cref="double"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="angle">The angle, in radians.</param>
    /// <returns>The sine value of the <paramref name="angle"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Sin(this double angle) => Math.Sin(angle);

    /// <summary>
    /// Function to return the cosine value of a <see cref="float"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="angle">The angle, in radians.</param>
    /// <returns>The cosine value of the <paramref name="angle"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Cos(this float angle) => (float)Math.Cos(angle);

    /// <summary>
    /// Function to return the cosine value of a <see cref="decimal"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="angle">The angle, in radians.</param>
    /// <returns>The cosine value of the <paramref name="angle"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Cos(this decimal angle) => (decimal)Math.Cos((double)angle);

    /// <summary>
    /// Function to return the cosine value of a <see cref="double"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="angle">The angle, in radians.</param>
    /// <returns>The cosine value of the <paramref name="angle"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Cos(this double angle) => Math.Cos(angle);

    /// <summary>
    /// Function to return the tangent value of a <see cref="float"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="angle">The angle, in radians.</param>
    /// <returns>The tangent value of the <paramref name="angle"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Tan(this float angle) => (float)Math.Tan(angle);

    /// <summary>
    /// Function to return the tangent value of a <see cref="decimal"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="angle">The angle, in radians.</param>
    /// <returns>The tangent value of the <paramref name="angle"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Tan(this decimal angle) => (decimal)Math.Tan((double)angle);

    /// <summary>
    /// Function to return the tangent value of a <see cref="double"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="angle">The angle, in radians.</param>
    /// <returns>The tangent value of the <paramref name="angle"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Tan(this double angle) => Math.Tan(angle);

    /// <summary>
    /// Function to return the inverse sine value of a <see cref="float"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="sine">The sine value.</param>
    /// <returns>The inverse sine value of the <paramref name="sine"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ASin(this float sine) => (float)Math.Asin(sine);

    /// <summary>
    /// Function to return the inverse sine value of a <see cref="decimal"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="sine">The sine value.</param>
    /// <returns>The inverse sine value of the <paramref name="sine"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal ASin(this decimal sine) => (decimal)Math.Asin((double)sine);

    /// <summary>
    /// Function to return the inverse sine value of a <see cref="double"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="sine">The sine value.</param>
    /// <returns>The inverse sine value of the <paramref name="sine"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ASin(this double sine) => Math.Asin(sine);

    /// <summary>
    /// Function to return the inverse cosine value of a <see cref="float"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="cosine">The cosine value.</param>
    /// <returns>The inverse cosine value of the <paramref name="cosine"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ACos(this float cosine) => (float)Math.Acos(cosine);

    /// <summary>
    /// Function to return the inverse cosine value of a <see cref="decimal"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="cosine">The cosine value.</param>
    /// <returns>The inverse cosine value of the <paramref name="cosine"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal ACos(this decimal cosine) => (decimal)Math.Acos((double)cosine);

    /// <summary>
    /// Function to return the inverse cosine value of a <see cref="double"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="cosine">The cosine value.</param>
    /// <returns>The inverse cosine value of the <paramref name="cosine"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ACos(this double cosine) => Math.Acos(cosine);

    /// <summary>
    /// Function to return the inverse tangent value of a <see cref="float"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="tangent">The tangent value.</param>
    /// <returns>The tangent sine value of the <paramref name="tangent"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ATan(this float tangent) => (float)Math.Atan(tangent);

    /// <summary>
    /// Function to return the inverse tangent value of a <see cref="decimal"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="tangent">The tangent value.</param>
    /// <returns>The tangent sine value of the <paramref name="tangent"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal ATan(this decimal tangent) => (decimal)Math.Atan((double)tangent);

    /// <summary>
    /// Function to return the inverse tangent value of a <see cref="double"/> value representing an angle, in radians.
    /// </summary>
    /// <param name="tangent">The tangent value.</param>
    /// <returns>The tangent sine value of the <paramref name="tangent"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ATan(this double tangent) => Math.Atan(tangent);

    /// <summary>
    /// Function to return the inverse tangent of two <see cref="float"/> values representing the horizontal and vertical offset of a slope.
    /// </summary>
    /// <param name="y">Vertical slope value to retrieve the inverse tangent from.</param>
    /// <param name="x">Horizontal slope value to retrieve the inverse tangent from.</param>
    /// <returns>The inverse tangent of the slope.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ATan(this float y, float x) => (float)Math.Atan2(y, x);

    /// <summary>
    /// Function to return the inverse tangent of two <see cref="decimal"/> values representing the horizontal and vertical offset of a slope.
    /// </summary>
    /// <param name="y">Vertical slope value to retrieve the inverse tangent from.</param>
    /// <param name="x">Horizontal slope value to retrieve the inverse tangent from.</param>
    /// <returns>The inverse tangent of the slope.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal ATan(this decimal y, decimal x) => (decimal)Math.Atan2((double)y, (double)x);

    /// <summary>
    /// Function to return the inverse tangent of two <see cref="double"/> values representing the horizontal and vertical offset of a slope.
    /// </summary>
    /// <param name="y">Vertical slope value to retrieve the inverse tangent from.</param>
    /// <param name="x">Horizontal slope value to retrieve the inverse tangent from.</param>
    /// <returns>The inverse tangent of the slope.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ATan(this double y, double x) => Math.Atan2(y, x);

    /// <summary>
    /// Function to return <b><i>e</i></b> raised to a <see cref="double"/> value as the power.
    /// </summary>
    /// <param name="power">The value representing a power to raise to.</param>
    /// <returns><b><i>e</i></b> raised to the <paramref name="power"/> specified.</returns>
    /// <remarks>
    /// <b><i>e</i></b> is a constant value of ~2.71828.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Exp(this double power) => Math.Exp(power);

    /// <summary>
    /// Function to return <b><i>e</i></b> raised to a <see cref="decimal"/> value as the power.
    /// </summary>
    /// <param name="power">The value representing a power to raise to.</param>
    /// <returns><b><i>e</i></b> raised to the <paramref name="power"/> specified.</returns>
    /// <remarks>
    /// <b><i>e</i></b> is a constant value of ~2.71828.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Exp(this decimal power) => (decimal)Math.Exp((double)power);
    /// <summary>
    /// Function to return <b><i>e</i></b> raised to a <see cref="float"/> value as the power.
    /// </summary>
    /// <param name="power">The value representing a power to raise to.</param>
    /// <returns><b><i>e</i></b> raised to the <paramref name="power"/> specified.</returns>
    /// <remarks>
    /// <b><i>e</i></b> is a constant value of ~2.71828.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Exp(this float power) => (float)Math.Exp(power);

    /// <summary>
    /// Function to raise a <see cref="double"/> to a specified power.
    /// </summary>
    /// <param name="value">The value to raise.</param>
    /// <param name="power">The value representing a power to raise to.</param>
    /// <returns>The <paramref name="value"/> raised to the specified <paramref name="power"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Pow(this double value, double power) => Math.Pow(value, power);

    /// <summary>
    /// Function to raise a <see cref="decimal"/> to a specified power.
    /// </summary>
    /// <param name="value">The value to raise.</param>
    /// <param name="power">The value representing a power to raise to.</param>
    /// <returns>The <paramref name="value"/> raised to the specified <paramref name="power"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Pow(this decimal value, decimal power) => (decimal)Math.Pow((double)value, (double)power);

    /// <summary>
    /// Function to raise a <see cref="float"/> to a specified power.
    /// </summary>
    /// <param name="value">The value to raise.</param>
    /// <param name="power">The value representing a power to raise to.</param>
    /// <returns>The <paramref name="value"/> raised to the specified <paramref name="power"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Pow(this float value, float power) => (float)Math.Pow(value, power);

    /// <summary>
    /// Function to compute the logarithim of a value.
    /// </summary>
    /// <param name="value">The value to compute the logarithim from.</param>
    /// <param name="power">The new base for the logarithm.</param>
    /// <returns>The logarithim value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Log(this float value, float power) => (float)Math.Log(value, power);

    /// <summary>
    /// Function to compute the logarithim of a value.
    /// </summary>
    /// <param name="value">The value to compute the logarithim from.</param>
    /// <param name="power">The new base for the logarithm.</param>
    /// <returns>The logarithim value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Log(this double value, double power) => Math.Log(value, power);

    /// <summary>
    /// Function to compute the logarithim of a value.
    /// </summary>
    /// <param name="value">The value to compute the logarithim from.</param>
    /// <param name="power">The new base for the logarithm.</param>
    /// <returns>The logarithim value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Log(this decimal value, decimal power) => (decimal)Math.Log((double)value, (double)power);

    /// <summary>
    /// Function to return the largest integer less than or equal to the specified <see cref="float"/> value.
    /// </summary>
    /// <param name="value">The value to find the floor for.</param>
    /// <returns>The largest integer less than or equal to <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float FastFloor(this float value)
    {
        int result = (int)value;

        return (value < result) ? result - 1 : result;
    }

    /// <summary>
    /// Function to return the largest integer less than or equal to the specified <see cref="float"/> value.
    /// </summary>
    /// <param name="value">The value to find the floor for.</param>
    /// <returns>The largest integer less than or equal to <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float FastFloor(this double value)
    {
        int result = (int)value;

        return (value < result) ? result - 1 : result;
    }

    /// <summary>
    /// Function to return the largest integer greater than or equal to the specified <see cref="float"/> value.
    /// </summary>
    /// <param name="value">The value to find the ceiling for.</param>
    /// <returns>The largest integer greater than or equal to <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float FastCeiling(this float value)
    {
        int result = (int)value;

        return (value > result) ? result + 1 : result;
    }

    /// <summary>
    /// Function to return the largest integer greater than or equal to the specified <see cref="double"/> value.
    /// </summary>
    /// <param name="value">The value to find the ceiling for.</param>
    /// <returns>The largest integer greater than or equal to <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float FastCeiling(this double value)
    {
        int result = (int)value;

        return (value > result) ? result + 1 : result;
    }

    /// <summary>
    /// Function to return the sign of an <see cref="int"/> value.
    /// </summary>
    /// <param name="value">The value to evaluate.</param>
    /// <returns>0 if the value is 0, -1 if the value is less than 0, and 1 if the value is greater than 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Sign(this int value)
    {
#pragma warning disable IDE0046 // Convert to conditional expression
        if (value == 0)
        {
            return 0;
        }

        return value < 0 ? -1 : 1;
#pragma warning restore IDE0046 // Convert to conditional expression
    }

    /// <summary>
    /// Function to return the sign of a <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The value to evaluate.</param>
    /// <returns>0 if the value is 0, -1 if the value is less than 0, and 1 if the value is greater than 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Sign(this long value)
    {
#pragma warning disable IDE0046 // Convert to conditional expression
        if (value == 0)
        {
            return 0;
        }

        return value < 0 ? -1 : 1;
#pragma warning restore IDE0046 // Convert to conditional expression
    }

    /* Disabled CLS Non Compliant
    /// <summary>
    /// Function to return the sign of a <see cref="sbyte"/> value.
    /// </summary>
    /// <param name="value">The value to evaluate.</param>
    /// <returns>0 if the value is 0, -1 if the value is less than 0, and 1 if the value is greater than 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Sign(this sbyte value)
    {
#pragma warning disable IDE0046 // Convert to conditional expression
        if (value == 0)
        {
            return 0;
        }

        return value < 0 ? -1 : 1;
#pragma warning restore IDE0046 // Convert to conditional expression
    }
    */

    /// <summary>
    /// Function to return the sign of a <see cref="short"/> value.
    /// </summary>
    /// <param name="value">The value to evaluate.</param>
    /// <returns>0 if the value is 0, -1 if the value is less than 0, and 1 if the value is greater than 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Sign(this short value)
    {
#pragma warning disable IDE0046 // Convert to conditional expression
        if (value == 0)
        {
            return 0;
        }

        return value < 0 ? -1 : 1;
#pragma warning restore IDE0046 // Convert to conditional expression
    }

    /// <summary>
    /// Function to return the sign of a <see cref="decimal"/> value.
    /// </summary>
    /// <param name="value">The value to evaluate.</param>
    /// <returns>0 if the value is 0, -1 if the value is less than 0, and 1 if the value is greater than 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Sign(this decimal value)
    {
#pragma warning disable IDE0046 // Convert to conditional expression
        if (value == 0)
        {
            return 0;
        }

        return value < 0 ? -1 : 1;
#pragma warning restore IDE0046 // Convert to conditional expression
    }

    /// <summary>
    /// Function to return the sign of a <see cref="float"/> value.
    /// </summary>
    /// <param name="value">The value to evaluate.</param>
    /// <returns>0 if the value is 0, -1 if the value is less than 0, and 1 if the value is greater than 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Sign(this float value)
    {
#pragma warning disable IDE0046 // Convert to conditional expression
        if (value.EqualsEpsilon(0))
        {
            return 0;
        }

        return value < 0 ? -1 : 1;
#pragma warning restore IDE0046 // Convert to conditional expression
    }

    /// <summary>
    /// Function to return the sign of a <see cref="double"/> value.
    /// </summary>
    /// <param name="value">The value to evaluate.</param>
    /// <returns>0 if the value is 0, -1 if the value is less than 0, and 1 if the value is greater than 0.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Sign(this double value)
    {
#pragma warning disable IDE0046 // Convert to conditional expression
        if (value.EqualsEpsilon(0))
        {
            return 0;
        }

        return value < 0 ? -1 : 1;
#pragma warning restore IDE0046 // Convert to conditional expression
    }

    /// <summary>
    /// Function to clamp a value to the range specified by the minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte Clamp(this byte value, byte minValue, byte maxValue)
    {
        value = value.Min(maxValue);
        return value.Max(minValue);
    }

    /// <summary>
    /// Function to clamp a value to the range specified by the minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Clamp(this short value, short minValue, short maxValue)
    {
        value = value.Min(maxValue);
        return value.Max(minValue);
    }

    /* Disabled CLS Non Compliant
    /// <summary>
    /// Function to clamp a value to the range specified by the minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort Clamp(this ushort value, ushort minValue, ushort maxValue)
    {
        value = value.Min(maxValue);
        return value.Max(minValue);
    }
    */
    /// <summary>
    /// Function to clamp a value to the range specified by the minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Clamp(this int value, int minValue, int maxValue)
    {
        value = value.Min(maxValue);
        return value.Max(minValue);
    }

    /* Disabled CLS Non Compliant
    /// <summary>
    /// Function to clamp a value to the range specified by the minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Clamp(this uint value, uint minValue, uint maxValue)
    {
        value = value.Min(maxValue);
        return value.Max(minValue);
    }
    */

    /// <summary>
    /// Function to clamp a value to the range specified by the minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Clamp(this long value, long minValue, long maxValue)
    {
        value = value.Min(maxValue);
        return value.Max(minValue);
    }

    /* Disabled CLS Non Compliant
    /// <summary>
    /// Function to clamp a value to the range specified by the minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    [methodimpl(methodimploptions.aggressiveinlining)]
    public static ulong clamp(this ulong value, ulong minvalue, ulong maxvalue)
    {
        value = value.min(maxvalue);
        return value.max(minvalue);
    }
    */

    /// <summary>
    /// Function to clamp a value to the range specified by the minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Clamp(this float value, float minValue, float maxValue)
    {
        value = value.Min(maxValue);
        return value.Max(minValue);
    }

    /// <summary>
    /// Function to clamp a value to the range specified by the minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Clamp(this double value, double minValue, double maxValue)
    {
        value = value.Min(maxValue);
        return value.Max(minValue);
    }

    /// <summary>
    /// Function to clamp a value to the range specified by the minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Clamp(this decimal value, decimal minValue, decimal maxValue)
    {
        value = value.Min(maxValue);
        return value.Max(minValue);
    }

    /// <summary>
    /// Function to limit an angle to the specified minimum and maximum.
    /// </summary>
    /// <param name="angle">The angle to limit.</param>
    /// <param name="min">[Optional] The minimum value for the angle.</param>
    /// <param name="max">[Optional] The maxmum value for the angle.</param>
    /// <returns>The limited angle value.</returns>
    public static float LimitAngle(this float angle, float min = 0, float max = 360)
    {
        if (min.EqualsEpsilon(max))
        {
            return angle.Max(min).Min(max);
        }

        if (min > max)
        {
            (min, max) = (max, min);
        }

        if (angle > max)
        {
            angle = max - angle;
        }

        if (angle < min)
        {
            angle -= min;
        }

        return angle;
    }

    /// <summary>
    /// Function to limit an angle to the specified minimum and maximum.
    /// </summary>
    /// <param name="angle">The angle to limit.</param>
    /// <param name="min">[Optional] The minimum value for the angle.</param>
    /// <param name="max">[Optional] The maxmum value for the angle.</param>
    /// <returns>The limited angle value.</returns>
    public static double LimitAngle(this double angle, double min = 0, double max = 360)
    {
        if (min.EqualsEpsilon(max))
        {
            return angle.Max(min).Min(max);
        }

        if (min > max)
        {
            (min, max) = (max, min);
        }

        if (angle > max)
        {
            angle = max - angle;
        }

        if (angle < min)
        {
            angle -= min;
        }

        return angle;
    }

    /// <summary>
    /// Function to limit an angle to the specified minimum and maximum.
    /// </summary>
    /// <param name="angle">The angle to limit.</param>
    /// <param name="min">[Optional] The minimum value for the angle.</param>
    /// <param name="max">[Optional] The maxmum value for the angle.</param>
    /// <returns>The limited angle value.</returns>
    public static decimal LimitAngle(this decimal angle, decimal min = 0, decimal max = 360)
    {
        if (min == max)
        {
            return angle.Max(min).Min(max);
        }

        if (min > max)
        {
            (min, max) = (max, min);
        }

        if (angle > max)
        {
            angle = max - angle;
        }

        if (angle < min)
        {
            angle -= min;
        }

        return angle;
    }

    /// <summary>
    /// Function to limit an angle to the specified minimum and maximum.
    /// </summary>
    /// <param name="angle">The angle to limit.</param>
    /// <param name="min">[Optional] The minimum value for the angle.</param>
    /// <param name="max">[Optional] The maxmum value for the angle.</param>
    /// <returns>The limited angle value.</returns>
    public static short LimitAngle(this short angle, short min = 0, short max = 360)
    {
        if (min == max)
        {
            return angle.Max(min).Min(max);
        }

        if (min > max)
        {
            (min, max) = (max, min);
        }

        if (angle > max)
        {
            angle = (short)(max - angle);
        }

        if (angle < min)
        {
            angle -= min;
        }

        return angle;
    }

    /// <summary>
    /// Function to limit an angle to the specified minimum and maximum.
    /// </summary>
    /// <param name="angle">The angle to limit.</param>
    /// <param name="min">[Optional] The minimum value for the angle.</param>
    /// <param name="max">[Optional] The maxmum value for the angle.</param>
    /// <returns>The limited angle value.</returns>
    public static int LimitAngle(this int angle, int min = 0, int max = 360)
    {
        if (min == max)
        {
            return angle.Max(min).Min(max);
        }

        if (min > max)
        {
            (min, max) = (max, min);
        }


        if (angle > max)
        {
            angle = max - angle;
        }

        if (angle < min)
        {
            angle -= min;
        }

        return angle;
    }

    /// <summary>
    /// Function to limit an angle to the specified minimum and maximum.
    /// </summary>
    /// <param name="angle">The angle to limit.</param>
    /// <param name="min">[Optional] The minimum value for the angle.</param>
    /// <param name="max">[Optional] The maxmum value for the angle.</param>
    /// <returns>The limited angle value.</returns>
    public static long LimitAngle(this long angle, long min = 0, long max = 360)
    {
        if (min == max)
        {
            return angle.Max(min).Min(max);
        }

        if (min > max)
        {
            (min, max) = (max, min);
        }

        if (angle > max)
        {
            angle = max - angle;
        }

        if (angle < min)
        {
            angle -= min;
        }

        return angle;
    }

    /// <summary>
    /// Function to linearly interpolate between two values given a weight amount.
    /// </summary>
    /// <param name="from">The value to start from.</param>
    /// <param name="to">The ending value.</param>
    /// <param name="amount">The weighting amount.</param>
    /// <returns>The linearly interpolated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Lerp(this float from, float to, float amount) => ((1.0f - amount) * from) + (amount * to);

    /// <summary>
    /// Function to linearly interpolate between two values given a weight amount.
    /// </summary>
    /// <param name="from">The value to start from.</param>
    /// <param name="to">The ending value.</param>
    /// <param name="amount">The weighting amount.</param>
    /// <returns>The linearly interpolated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Lerp(this double from, double to, double amount) => ((1.0 - amount) * from) + (amount * to);

    /// <summary>
    /// Function to linearly interpolate between two values given a weight amount.
    /// </summary>
    /// <param name="from">The value to start from.</param>
    /// <param name="to">The ending value.</param>
    /// <param name="amount">The weighting amount.</param>
    /// <returns>The linearly interpolated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal Lerp(this decimal from, decimal to, decimal amount) => ((1.0M - amount) * from) + (amount * to);
    #endregion
}