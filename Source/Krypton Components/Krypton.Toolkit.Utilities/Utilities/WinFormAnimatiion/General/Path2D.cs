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
///     The Path2D class is a representation of a line in a 2D plane and the
///     speed in which the animator plays it
/// </summary>
internal class Path2D
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Path2D" /> class.
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
    public Path2D(
        float startX,
        float endX,
        float startY,
        float endY,
        ulong duration,
        ulong delay,
        AnimationFunctions.Function function)
        : this(new Path(startX, endX, duration, delay, function), new Path(startY, endY, duration, delay, function))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path2D" /> class.
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
    /// <param name="duration">
    ///     The time in milliseconds that the animator must play this path
    /// </param>
    /// <param name="delay">
    ///     The time in milliseconds that the animator must wait before playing this path
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Duration is less than zero
    /// </exception>
    public Path2D(
        float startX,
        float endX,
        float startY,
        float endY,
        ulong duration,
        ulong delay)
        : this(new Path(startX, endX, duration, delay), new Path(startY, endY, duration, delay))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path2D" /> class.
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
    /// <param name="duration">
    ///     The time in milliseconds that the animator must play this path
    /// </param>
    /// <param name="function">
    ///     The animation function
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Duration is less than zero
    /// </exception>
    public Path2D(
        float startX,
        float endX,
        float startY,
        float endY,
        ulong duration,
        AnimationFunctions.Function function)
        : this(new Path(startX, endX, duration, function), new Path(startY, endY, duration, function))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path2D" /> class.
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
    /// <param name="duration">
    ///     The time in milliseconds that the animator must play this path
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Duration is less than zero
    /// </exception>
    public Path2D(
        float startX,
        float endX,
        float startY,
        float endY,
        ulong duration)
        : this(new Path(startX, endX, duration), new Path(startY, endY, duration))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path2D" /> class.
    /// </summary>
    /// <param name="start">
    ///     The starting point or location
    /// </param>
    /// <param name="end">
    ///     The ending point or location
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
    public Path2D(Float2D start, Float2D end, ulong duration, ulong delay, AnimationFunctions.Function function)
        : this(
            new Path(start.X, end.X, duration, delay, function),
            new Path(start.Y, end.Y, duration, delay, function))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path2D" /> class.
    /// </summary>
    /// <param name="start">
    ///     The starting point or location
    /// </param>
    /// <param name="end">
    ///     The ending point or location
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
    public Path2D(Float2D start, Float2D end, ulong duration, ulong delay)
        : this(
            new Path(start.X, end.X, duration, delay),
            new Path(start.Y, end.Y, duration, delay))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path2D" /> class.
    /// </summary>
    /// <param name="start">
    ///     The starting point or location
    /// </param>
    /// <param name="end">
    ///     The ending point or location
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
    public Path2D(Float2D start, Float2D end, ulong duration, AnimationFunctions.Function function)
        : this(
            new Path(start.X, end.X, duration, function),
            new Path(start.Y, end.Y, duration, function))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path2D" /> class.
    /// </summary>
    /// <param name="start">
    ///     The starting point or location
    /// </param>
    /// <param name="end">
    ///     The ending point or location
    /// </param>
    /// <param name="duration">
    ///     The time in milliseconds that the animator must play this path
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Duration is less than zero
    /// </exception>
    public Path2D(Float2D start, Float2D end, ulong duration)
        : this(
            new Path(start.X, end.X, duration),
            new Path(start.Y, end.Y, duration))
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path2D" /> class.
    /// </summary>
    /// <param name="x">
    ///     The horizontal path.
    /// </param>
    /// <param name="y">
    ///     The vertical path.
    /// </param>
    public Path2D(Path x, Path y)
    {
        HorizontalPath = x;
        VerticalPath = y;
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
    ///     Gets the starting point of the path
    /// </summary>
    public Float2D Start => new(HorizontalPath.Start, VerticalPath.Start);


    /// <summary>
    ///     Gets the ending point of the path
    /// </summary>
    public Float2D End => new(HorizontalPath.End, VerticalPath.End);

    /// <summary>
    ///     Creates and returns a new <see cref="Path2D" /> based on the current path but in reverse order
    /// </summary>
    /// <returns>
    ///     A new <see cref="Path2D" /> which is the reverse of the current <see cref="Path2D" />
    /// </returns>
    public Path2D Reverse()
    {
        return new Path2D(HorizontalPath.Reverse(), VerticalPath.Reverse());
    }
}
