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
///     The base interface for any Animator class, custom or build-in
/// </summary>
internal interface IAnimator
{
    /// <summary>
    ///     Gets the current status of the animation
    /// </summary>
    AnimatorStatus CurrentStatus { get; }

    /// <summary>
    ///     Gets or sets a value indicating whether animator should repeat the animation after its ending
    /// </summary>
    bool Repeat { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether animator should repeat the animation in reverse after its ending.
    /// </summary>
    bool ReverseRepeat { get; set; }

    /// <summary>
    ///     Starts the playing of the animation
    /// </summary>
    /// <param name="targetObject">
    ///     The target object to change the property
    /// </param>
    /// <param name="propertyName">
    ///     The name of the property to change
    /// </param>
    void Play(object targetObject, string propertyName);

    /// <summary>
    ///     Starts the playing of the animation
    /// </summary>
    /// <param name="targetObject">
    ///     The target object to change the property
    /// </param>
    /// <param name="propertyName">
    ///     The name of the property to change
    /// </param>
    /// <param name="endCallback">
    ///     The callback to get invoked at the end of the animation
    /// </param>
    void Play(object targetObject, string propertyName, SafeInvoker? endCallback);

    /// <summary>
    ///     Starts the playing of the animation
    /// </summary>
    /// <param name="targetObject">
    ///     The target object to change the property
    /// </param>
    /// <param name="propertySetter">
    ///     The expression that represents the property of the target object
    /// </param>
    /// <typeparam name="T">
    ///     Any object containing a property
    /// </typeparam>
    void Play<T>(T targetObject, Expression<Func<T, object>> propertySetter);

    /// <summary>
    ///     Starts the playing of the animation
    /// </summary>
    /// <param name="targetObject">
    ///     The target object to change the property
    /// </param>
    /// <param name="propertySetter">
    ///     The expression that represents the property of the target object
    /// </param>
    /// <param name="endCallback">
    ///     The callback to get invoked at the end of the animation
    /// </param>
    /// <typeparam name="T">
    ///     Any object containing a property
    /// </typeparam>
    void Play<T>(T targetObject, Expression<Func<T, object>> propertySetter, SafeInvoker? endCallback);

    /// <summary>
    ///     Resume the animation from where it paused
    /// </summary>
    void Resume();

    /// <summary>
    ///     Stops the animation and resets its status, resume is no longer possible
    /// </summary>
    void Stop();

    /// <summary>
    ///     Pause the animation
    /// </summary>
    void Pause();
}