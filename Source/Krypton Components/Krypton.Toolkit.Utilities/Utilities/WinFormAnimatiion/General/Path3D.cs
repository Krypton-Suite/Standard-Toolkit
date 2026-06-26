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
///     The Path3D class is a representation of a line in a 3D plane and the
///     speed in which the animator plays it
/// </summary>
internal class Path3D
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Path3D" /> class.
    /// </summary>
    /// <param name="startX">
    ///     The starting horizontal value
    /// </param>
    /// <param name="endX">
    ///     The ending horizontal value
    /// </param>
    /// <param name="startY">
    ///     The starting vertical value
    /// </param>
    /// <param name="endY">
    ///     The ending vertical value
    /// </param>
    /// <param name="startZ">
    ///     The starting depth value
    /// </param>
    /// <param name="endZ">
    ///     The ending depth value
    /// </param>
    /// <param name="duration">
    ///     The time in milliseconds that the animator must play this path
    /// </param>
    /// <param name="delay">
    ///     The time in milliseconds that the animator must wait before playing this path
    /// </param>
    /// <param name="function">
    ///     The animation function
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Duration is less than zero
    /// </exception>
    public Path3D(
        float startX,
        float endX,
        float startY,
        float endY,
        float startZ,
        float endZ,
        ulong duration,
        ulong delay,
        AnimationFunctions.Function function)
        : this(
            new Path(startX, endX, duration, delay, function),
            new Path(startY, endY, duration, delay, function),
            new Path(startZ, endZ, duration, delay, function))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path3D" /> class.
    /// </summary>
    /// <param name="startX">
    ///     The starting horizontal value
    /// </param>
    /// <param name="endX">
    ///     The ending horizontal value
    /// </param>
    /// <param name="startY">
    ///     The starting vertical value
    /// </param>
    /// <param name="endY">
    ///     The ending vertical value
    /// </param>
    /// <param name="startZ">
    ///     The starting depth value
    /// </param>
    /// <param name="endZ">
    ///     The ending depth value
    /// </param>
    /// <param name="duration">
    ///     The time in milliseconds that the animator must play this path
    /// </param>
    /// <param name="delay">
    ///     The time in milliseconds that the animator must wait before playing this path
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Duration is less than zero
    /// </exception>
    public Path3D(
        float startX,
        float endX,
        float startY,
        float endY,
        float startZ,
        float endZ,
        ulong duration,
        ulong delay)
        : this(
            new Path(startX, endX, duration, delay),
            new Path(startY, endY, duration, delay),
            new Path(startZ, endZ, duration, delay))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path3D" /> class.
    /// </summary>
    /// <param name="startX">
    ///     The starting horizontal value
    /// </param>
    /// <param name="endX">
    ///     The ending horizontal value
    /// </param>
    /// <param name="startY">
    ///     The starting vertical value
    /// </param>
    /// <param name="endY">
    ///     The ending vertical value
    /// </param>
    /// <param name="startZ">
    ///     The starting depth value
    /// </param>
    /// <param name="endZ">
    ///     The ending depth value
    /// </param>
    /// <param name="duration">
    ///     The time in milliseconds that the animator must play this path
    /// </param>
    /// <param name="function">
    ///     The animation function
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Duration is less than zero
    /// </exception>
    public Path3D(
        float startX,
        float endX,
        float startY,
        float endY,
        float startZ,
        float endZ,
        ulong duration,
        AnimationFunctions.Function function)
        : this(
            new Path(startX, endX, duration, function),
            new Path(startY, endY, duration, function),
            new Path(startZ, endZ, duration, function))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path3D" /> class.
    /// </summary>
    /// <param name="startX">
    ///     The starting horizontal value
    /// </param>
    /// <param name="endX">
    ///     The ending horizontal value
    /// </param>
    /// <param name="startY">
    ///     The starting vertical value
    /// </param>
    /// <param name="endY">
    ///     The ending vertical value
    /// </param>
    /// <param name="startZ">
    ///     The starting depth value
    /// </param>
    /// <param name="endZ">
    ///     The ending depth value
    /// </param>
    /// <param name="duration">
    ///     The time in milliseconds that the animator must play this path
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Duration is less than zero
    /// </exception>
    public Path3D(
        float startX,
        float endX,
        float startY,
        float endY,
        float startZ,
        float endZ,
        ulong duration)
        : this(new Path(startX, endX, duration), new Path(startY, endY, duration), new Path(startZ, endZ, duration))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path3D" /> class.
    /// </summary>
    /// <param name="start">
    ///     The starting point in a 3D plane
    /// </param>
    /// <param name="end">
    ///     The ending point in a 3D plane
    /// </param>
    /// <param name="duration">
    ///     The time in milliseconds that the animator must play this path
    /// </param>
    /// <param name="delay">
    ///     The time in milliseconds that the animator must wait before playing this path
    /// </param>
    /// <param name="function">
    ///     The animation function
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Duration is less than zero
    /// </exception>
    public Path3D(Float3D start, Float3D end, ulong duration, ulong delay, AnimationFunctions.Function function)
        : this(
            new Path(start.X, end.X, duration, delay, function),
            new Path(start.Y, end.Y, duration, delay, function),
            new Path(start.Z, end.Z, duration, delay, function))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path3D" /> class.
    /// </summary>
    /// <param name="start">
    ///     The starting point in a 3D plane
    /// </param>
    /// <param name="end">
    ///     The ending point in a 3D plane
    /// </param>
    /// <param name="duration">
    ///     The time in milliseconds that the animator must play this path
    /// </param>
    /// <param name="delay">
    ///     The time in milliseconds that the animator must wait before playing this path
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Duration is less than zero
    /// </exception>
    public Path3D(Float3D start, Float3D end, ulong duration, ulong delay)
        : this(
            new Path(start.X, end.X, duration, delay),
            new Path(start.Y, end.Y, duration, delay),
            new Path(start.Z, end.Z, duration, delay))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path3D" /> class.
    /// </summary>
    /// <param name="start">
    ///     The starting point in a 3D plane
    /// </param>
    /// <param name="end">
    ///     The ending point in a 3D plane
    /// </param>
    /// <param name="duration">
    ///     The time in milliseconds that the animator must play this path
    /// </param>
    /// <param name="function">
    ///     The animation function
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Duration is less than zero
    /// </exception>
    public Path3D(Float3D start, Float3D end, ulong duration, AnimationFunctions.Function function)
        : this(
            new Path(start.X, end.X, duration, function),
            new Path(start.Y, end.Y, duration, function),
            new Path(start.Z, end.Z, duration, function))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path3D" /> class.
    /// </summary>
    /// <param name="start">
    ///     The starting point in a 3D plane
    /// </param>
    /// <param name="end">
    ///     The ending point in a 3D plane
    /// </param>
    /// <param name="duration">
    ///     The time in milliseconds that the animator must play this path
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Duration is less than zero
    /// </exception>
    public Path3D(Float3D start, Float3D end, ulong duration)
        : this(
            new Path(start.X, end.X, duration),
            new Path(start.Y, end.Y, duration),
            new Path(start.Z, end.Z, duration))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path3D" /> class.
    /// </summary>
    /// <param name="x">
    ///     The horizontal path.
    /// </param>
    /// <param name="y">
    ///     The vertical path.
    /// </param>
    /// <param name="z">
    ///     The depth path.
    /// </param>
    public Path3D(Path x, Path y, Path z)
    {
        HorizontalPath = x;
        VerticalPath = y;
        DepthPath = z;
    }

    /// <summary>
    ///     Gets the horizontal path
    /// </summary>
    public Path HorizontalPath { get; }

    /// <summary>
    ///     Gets the vertical path
    /// </summary>
    public Path VerticalPath { get; }

    /// <summary>
    ///     Gets the depth path
    /// </summary>
    public Path DepthPath { get; }


    /// <summary>
    ///     Gets the starting point of the path
    /// </summary>
    public Float3D Start => new(HorizontalPath.Start, VerticalPath.Start, DepthPath.Start);


    /// <summary>
    ///     Gets the ending point of the path
    /// </summary>
    public Float3D End => new(HorizontalPath.End, VerticalPath.End, DepthPath.End);

    /// <summary>
    ///     Creates and returns a new <see cref="Path3D" /> based on the current path but in reverse order
    /// </summary>
    /// <returns>
    ///     A new <see cref="Path" /> which is the reverse of the current <see cref="Path3D" />
    /// </returns>
    public Path3D Reverse()
    {
        return new Path3D(HorizontalPath.Reverse(), VerticalPath.Reverse(), DepthPath.Reverse());
    }
}
