#region Original MIT License

/* The MIT License (MIT)
 *
 * Copyright (c) 2016 - 2020 Soroush Falahati
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

#endregion

#region Modified License

/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */

#endregion

namespace WinFormAnimation;

/// <summary>
///     Contains public extension methods about Float2D and Float3D classes
/// </summary>
internal static class FloatExtensions
{
    /// <summary>
    ///     Creates and returns a new instance of the <see cref="Float2D" /> class from this instance
    /// </summary>
    /// <param name="point">The object to create the <see cref="Float2D" /> instance from</param>
    /// <returns>The newly created <see cref="Float2D" /> instance</returns>
    public static Float2D ToFloat2D(this Point point) => Float2D.FromPoint(point);

    /// <summary>
    ///     Creates and returns a new instance of the <see cref="Float2D" /> class from this instance
    /// </summary>
    /// <param name="point">The object to create the <see cref="Float2D" /> instance from</param>
    /// <returns>The newly created <see cref="Float2D" /> instance</returns>
    public static Float2D ToFloat2D(this PointF point) => Float2D.FromPoint(point);

    /// <summary>
    ///     Creates and returns a new instance of the <see cref="Float2D" /> class from this instance
    /// </summary>
    /// <param name="size">The object to create the <see cref="Float2D" /> instance from</param>
    /// <returns>The newly created <see cref="Float2D" /> instance</returns>
    public static Float2D ToFloat2D(this Size size) => Float2D.FromSize(size);

    /// <summary>
    ///     Creates and returns a new instance of the <see cref="Float2D" /> class from this instance
    /// </summary>
    /// <param name="size">The object to create the <see cref="Float2D" /> instance from</param>
    /// <returns>The newly created <see cref="Float2D" /> instance</returns>
    public static Float2D ToFloat2D(this SizeF size) => Float2D.FromSize(size);

    /// <summary>
    ///     Creates and returns a new instance of the <see cref="Float3D" /> class from this instance
    /// </summary>
    /// <param name="color">The object to create the <see cref="Float3D" /> instance from</param>
    /// <returns>The newly created <see cref="Float3D" /> instance</returns>
    public static Float3D ToFloat3D(this Color color) => Float3D.FromColor(color);
}
