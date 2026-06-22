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
///     The possible statuses for an animator instance
/// </summary>
public enum AnimatorStatus
{
    /// <summary>
    ///     Animation is stopped
    /// </summary>
    Stopped,

    /// <summary>
    ///     Animation is now playing
    /// </summary>
    Playing,

    /// <summary>
    ///     Animation is now on hold for path delay, consider this value as playing
    /// </summary>
    OnHold,

    /// <summary>
    ///     Animation is paused
    /// </summary>
    Paused
}

/// <summary>
///     FPS limiter known values
/// </summary>
public enum FPSLimiterKnownValues
{
    /// <summary>
    ///     Limits maximum frames per second to 10fps
    /// </summary>
    LimitTen = 10,

    /// <summary>
    ///     Limits maximum frames per second to 20fps
    /// </summary>
    LimitTwenty = 20,

    /// <summary>
    ///     Limits maximum frames per second to 30fps
    /// </summary>
    LimitThirty = 30,

    /// <summary>
    ///     Limits maximum frames per second to 60fps
    /// </summary>
    LimitSixty = 60,

    /// <summary>
    ///     Limits maximum frames per second to 100fps
    /// </summary>
    LimitOneHundred = 100,

    /// <summary>
    ///     Limits maximum frames per second to 200fps
    /// </summary>
    LimitTwoHundred = 200,

    /// <summary>
    ///     Adds no limit to the number of frames per second
    /// </summary>
    NoFPSLimit = -1
}

/// <summary>
///     Contains a list of all known animation functions
/// </summary>
public enum KnownAnimationFunctions
{
    /// <summary>
    ///     No known animation function
    /// </summary>
    None,

    /// <summary>
    ///     The cubic ease-in animation function.
    /// </summary>
    CubicEaseIn,

    /// <summary>
    ///     The cubic ease-in and ease-out animation function.
    /// </summary>
    CubicEaseInOut,

    /// <summary>
    ///     The cubic ease-out animation function.
    /// </summary>
    CubicEaseOut,

    /// <summary>
    ///     The linear animation function.
    /// </summary>
    Linear,

    /// <summary>
    ///     The circular ease-in and ease-out animation function.
    /// </summary>
    CircularEaseInOut,

    /// <summary>
    ///     The circular ease-in animation function.
    /// </summary>
    CircularEaseIn,

    /// <summary>
    ///     The circular ease-out animation function.
    /// </summary>
    CircularEaseOut,

    /// <summary>
    ///     The quadratic ease-in animation function.
    /// </summary>
    QuadraticEaseIn,

    /// <summary>
    ///     The quadratic ease-out animation function.
    /// </summary>
    QuadraticEaseOut,

    /// <summary>
    ///     The quadratic ease-in and ease-out animation function.
    /// </summary>
    QuadraticEaseInOut,

    /// <summary>
    ///     The quartic ease-in animation function.
    /// </summary>
    QuarticEaseIn,

    /// <summary>
    ///     The quartic ease-out animation function.
    /// </summary>
    QuarticEaseOut,

    /// <summary>
    ///     The quartic ease-in and ease-out animation function.
    /// </summary>
    QuarticEaseInOut,

    /// <summary>
    ///     The quintic ease-in and ease-out animation function.
    /// </summary>
    QuinticEaseInOut,

    /// <summary>
    ///     The quintic ease-in animation function.
    /// </summary>
    QuinticEaseIn,

    /// <summary>
    ///     The quintic ease-out animation function.
    /// </summary>
    QuinticEaseOut,

    /// <summary>
    ///     The sinusoidal ease-in animation function.
    /// </summary>
    SinusoidalEaseIn,

    /// <summary>
    ///     The sinusoidal ease-out animation function.
    /// </summary>
    SinusoidalEaseOut,

    /// <summary>
    ///     The sinusoidal ease-in and ease-out animation function.
    /// </summary>
    SinusoidalEaseInOut,

    /// <summary>
    ///     The exponential ease-in animation function.
    /// </summary>
    ExponentialEaseIn,

    /// <summary>
    ///     The exponential ease-out animation function.
    /// </summary>
    ExponentialEaseOut,

    /// <summary>
    ///     The exponential ease-in and ease-out animation function.
    /// </summary>
    ExponentialEaseInOut
}

/// <summary>
///     The known one dimensional properties of WinForm controls
/// </summary>
public enum KnownProperties
{
    /// <summary>
    ///     The property named 'Value' of the object
    /// </summary>
    Value = 0,

    /// <summary>
    ///     The property named 'Text' of the object
    /// </summary>
    Text = 1,

    /// <summary>
    ///     The property named 'Caption' of the object
    /// </summary>
    Caption = 2,

    /// <summary>
    ///     The property named 'BackColor' of the object
    /// </summary>
    BackColor = 3,

    /// <summary>
    ///     The property named 'ForeColor' of the object
    /// </summary>
    ForeColor = 4,

    /// <summary>
    ///     The property named 'Opacity' of the object
    /// </summary>
    Opacity = 5
}

/// <summary>
///     The known two-dimensional properties of WinForm controls
/// </summary>
public enum KnownTwoDimensionalProperties
{
    /// <summary>
    ///     The property named 'Size' of the object
    /// </summary>
    Size,

    /// <summary>
    ///     The property named 'Location' of the object
    /// </summary>
    Location
}

/// <summary>
///     The known three-dimensional properties of WinForm controls
/// </summary>
public enum KnownColorProperties
{
    /// <summary>
    ///     The property named 'BackColor' of the object
    /// </summary>
    BackColor,

    /// <summary>
    ///     The property named 'ForeColor' of the object
    /// </summary>
    ForeColor
}