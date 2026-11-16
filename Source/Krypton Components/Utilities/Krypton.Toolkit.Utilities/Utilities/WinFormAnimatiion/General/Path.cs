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
///     The Path class is a representation of a line in a 1D plane and the
///     speed in which the animator plays it
/// </summary>
internal class Path
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Path" /> class.
    /// </summary>
    public Path() : this(0, 0, 0, 0, null)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path" /> class.
    /// </summary>
    /// <param name="start">
    ///     The starting value
    /// </param>
    /// <param name="end">
    ///     The ending value
    /// </param>
    /// <param name="duration">
    ///     The time in milliseconds that the animator must play this path
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Duration is less than zero
    /// </exception>
    public Path(float start, float end, ulong duration) : this(start, end, duration, 0, null)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path" /> class.
    /// </summary>
    /// <param name="start">
    ///     The starting value
    /// </param>
    /// <param name="end">
    ///     The ending value
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
    public Path(float start, float end, ulong duration, AnimationFunctions.Function function)
        : this(start, end, duration, 0, function)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path" /> class.
    /// </summary>
    /// <param name="start">
    ///     The starting value
    /// </param>
    /// <param name="end">
    ///     The ending value
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
    public Path(float start, float end, ulong duration, ulong delay) : this(start, end, duration, delay, null)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Path" /> class.
    /// </summary>
    /// <param name="start">
    ///     The starting value
    /// </param>
    /// <param name="end">
    ///     The ending value
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
    public Path(float start, float end, ulong duration, ulong delay, AnimationFunctions.Function? function)
    {
        Start = start;
        End = end;
        Function = function ?? AnimationFunctions.Linear;
        Duration = duration;
        Delay = delay;
    }

    /// <summary>
    ///     Gets the difference of starting and ending values
    /// </summary>
    public float Change => End - Start;

    /// <summary>
    ///     Gets or sets the starting delay
    /// </summary>
    public ulong Delay { get; set; }

    /// <summary>
    ///     Gets or sets the duration in milliseconds
    /// </summary>
    public ulong Duration { get; set; }

    /// <summary>
    ///     Gets or sets the ending value
    /// </summary>
    public float End { get; set; }

    /// <summary>
    ///     Gets or sets the animation function
    /// </summary>
    public AnimationFunctions.Function Function { get; set; }

    /// <summary>
    ///     Gets or sets the starting value
    /// </summary>
    public float Start { get; set; }

    /// <summary>
    ///     Creates and returns a new <see cref="Path" /> based on the current path but in reverse order
    /// </summary>
    /// <returns>
    ///     A new <see cref="Path" /> which is the reverse of the current <see cref="Path" />
    /// </returns>
    public Path Reverse()
    {
        return new Path(End, Start, Duration, Delay, Function);
    }
}
