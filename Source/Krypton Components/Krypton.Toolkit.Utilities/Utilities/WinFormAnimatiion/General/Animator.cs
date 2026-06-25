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
///     The one dimensional animator class, useful for animating raw values
/// </summary>
internal class Animator : IAnimator
{
    private readonly List<Path> _paths = [];

    private readonly List<Path> _tempPaths = [];

    private readonly Timer _timer;

    private bool _tempReverseRepeat;

    /// <summary>
    ///     The callback to get invoked at the end of the animation
    /// </summary>
    protected SafeInvoker? EndCallback;

    /// <summary>
    ///     The callback to get invoked at each frame
    /// </summary>
    protected SafeInvoker<float>? FrameCallback;

    /// <summary>
    ///     The target object to change the property of
    /// </summary>
    protected object? TargetObject;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Animator" /> class.
    /// </summary>
    public Animator()
        : this([])
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Animator" /> class.
    /// </summary>
    /// <param name="fpsLimiter">
    ///     Limits the maximum frames per seconds
    /// </param>
    public Animator(FPSLimiterKnownValues fpsLimiter)
        : this([], fpsLimiter)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Animator" /> class.
    /// </summary>
    /// <param name="path">
    ///     The path of the animation
    /// </param>
    public Animator(Path path)
        : this([path])
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Animator" /> class.
    /// </summary>
    /// <param name="path">
    ///     The path of the animation
    /// </param>
    /// <param name="fpsLimiter">
    ///     Limits the maximum frames per seconds
    /// </param>
    public Animator(Path path, FPSLimiterKnownValues fpsLimiter)
        : this([path], fpsLimiter)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Animator" /> class.
    /// </summary>
    /// <param name="paths">
    ///     An array containing the list of paths of the animation
    /// </param>
    public Animator(Path[] paths) : this(paths, FPSLimiterKnownValues.LimitThirty)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Animator" /> class.
    /// </summary>
    /// <param name="paths">
    ///     An array containing the list of paths of the animation
    /// </param>
    /// <param name="fpsLimiter">
    ///     Limits the maximum frames per seconds
    /// </param>
    public Animator(Path[] paths, FPSLimiterKnownValues fpsLimiter)
    {
        CurrentStatus = AnimatorStatus.Stopped;
        _timer = new Timer(Elapsed, fpsLimiter);
        Paths = paths;
    }

    /// <summary>
    ///     Gets or sets an array containing the list of paths of the animation
    /// </summary>
    /// <exception cref="InvalidOperationException">Animation is running</exception>
    public Path[] Paths
    {
        get => _paths.ToArray();
        set
        {
            if (CurrentStatus == AnimatorStatus.Stopped)
            {
                _paths.Clear();
                _paths.AddRange(value);
            }
            else
            {
                throw new InvalidOperationException("Animation is running.");
            }
        }
    }

    /// <summary>
    ///     Gets the currently active path.
    /// </summary>
    public Path? ActivePath { get; private set; }

    /// <summary>
    ///     Gets or sets a value indicating whether animator should repeat the animation after its ending
    /// </summary>
    public virtual bool Repeat { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether animator should repeat the animation in reverse after its ending.
    /// </summary>
    public virtual bool ReverseRepeat { get; set; }

    /// <summary>
    ///     Gets the current status of the animation
    /// </summary>
    public virtual AnimatorStatus CurrentStatus { get; private set; }

    /// <summary>
    ///     Pause the animation
    /// </summary>
    public virtual void Pause()
    {
        if (CurrentStatus != AnimatorStatus.OnHold && CurrentStatus != AnimatorStatus.Playing)
        {
            return;
        }

        _timer.Stop();
        CurrentStatus = AnimatorStatus.Paused;
    }

    /// <summary>
    ///     Starts the playing of the animation
    /// </summary>
    /// <param name="targetObject">
    ///     The target object to change the property
    /// </param>
    /// <param name="propertyName">
    ///     The name of the property to change
    /// </param>
    public virtual void Play(object targetObject, string propertyName)
    {
        Play(targetObject, propertyName, null);
    }

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
    public virtual void Play(object targetObject, string propertyName, SafeInvoker? endCallback)
    {
        TargetObject = targetObject;
        var prop = TargetObject.GetType()
            .GetProperty(
                propertyName,
                BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance |
                BindingFlags.SetProperty);
        if (prop == null)
        {
            return;
        }

        Play(
            new SafeInvoker<float>(
                value => prop.SetValue(TargetObject, Convert.ChangeType(value, prop.PropertyType), null),
                TargetObject),
            endCallback);
    }

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
    public virtual void Play<T>(T targetObject, Expression<Func<T, object>> propertySetter)
    {
        Play(targetObject, propertySetter, null);
    }


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
    public virtual void Play<T>(T targetObject, Expression<Func<T, object>>? propertySetter, SafeInvoker? endCallback)
    {
        if (propertySetter == null)
        {
            return;
        }

        TargetObject = targetObject;

        var property =
            (propertySetter.Body as MemberExpression ??
             ((UnaryExpression) propertySetter.Body).Operand as MemberExpression)?.Member as PropertyInfo;
        if (property == null)
        {
            throw new ArgumentException(nameof(propertySetter));
        }

        Play(
            new SafeInvoker<float>(
                value => property.SetValue(TargetObject, Convert.ChangeType(value, property.PropertyType), null),
                TargetObject),
            endCallback);
    }

    /// <summary>
    ///     Resume the animation from where it paused
    /// </summary>
    public virtual void Resume()
    {
        if (CurrentStatus == AnimatorStatus.Paused)
        {
            _timer.Resume();
        }
    }

    /// <summary>
    ///     Stops the animation and resets its status, resume is no longer possible
    /// </summary>
    public virtual void Stop()
    {
        _timer.Stop();
        lock (_tempPaths)
        {
            _tempPaths.Clear();
        }

        ActivePath = null;
        CurrentStatus = AnimatorStatus.Stopped;
        _tempReverseRepeat = false;
    }

    /// <summary>
    ///     Starts the playing of the animation
    /// </summary>
    /// <param name="targetObject">
    ///     The target object to change the property
    /// </param>
    /// <param name="property">
    ///     The property to change
    /// </param>
    public virtual void Play(object targetObject, KnownProperties property)
    {
        Play(targetObject, property, null);
    }


    /// <summary>
    ///     Starts the playing of the animation
    /// </summary>
    /// <param name="targetObject">
    ///     The target object to change the property
    /// </param>
    /// <param name="property">
    ///     The property to change
    /// </param>
    /// <param name="endCallback">
    ///     The callback to get invoked at the end of the animation
    /// </param>
    public virtual void Play(object targetObject, KnownProperties property, SafeInvoker? endCallback)
    {
        Play(targetObject, property.ToString(), endCallback);
    }

    /// <summary>
    ///     Starts the playing of the animation
    /// </summary>
    /// <param name="frameCallback">
    ///     The callback to get invoked at each frame
    /// </param>
    public virtual void Play(SafeInvoker<float> frameCallback)
    {
        Play(frameCallback, (SafeInvoker?)null);
    }


    /// <summary>
    ///     Starts the playing of the animation
    /// </summary>
    /// <param name="frameCallback">
    ///     The callback to get invoked at each frame
    /// </param>
    /// <param name="endCallback">
    ///     The callback to get invoked at the end of the animation
    /// </param>
    public virtual void Play(SafeInvoker<float> frameCallback, SafeInvoker? endCallback)
    {
        Stop();
        FrameCallback = frameCallback;
        EndCallback = endCallback;
        _timer.ResetClock();
        lock (_tempPaths)
        {
            _tempPaths.AddRange(_paths);
        }

        _timer.Start();
    }

    private void Elapsed(ulong millSinceBeginning = 0)
    {
        while (true)
        {
            lock (_tempPaths)
            {
                if (ActivePath == null && _tempPaths.Count > 0)
                {
                    while (ActivePath == null)
                    {
                        if (_tempReverseRepeat)
                        {
                            ActivePath = _tempPaths[_tempPaths.Count - 1];
                            _tempPaths.RemoveAt(_tempPaths.Count - 1);
                        }
                        else
                        {
                            ActivePath = _tempPaths[0];
                            _tempPaths.RemoveAt(0);
                        }

                        _timer.ResetClock();
                        millSinceBeginning = 0;
                    }
                }

                var ended = ActivePath == null;
                if (ActivePath != null)
                {
                    if (!_tempReverseRepeat && millSinceBeginning < ActivePath.Delay)
                    {
                        CurrentStatus = AnimatorStatus.OnHold;
                        return;
                    }

                    if (millSinceBeginning - (!_tempReverseRepeat ? ActivePath.Delay : 0) <= ActivePath.Duration)
                    {
                        if (CurrentStatus != AnimatorStatus.Playing)
                        {
                            CurrentStatus = AnimatorStatus.Playing;
                        }

                        var value = ActivePath.Function(
                            _tempReverseRepeat
                                ? ActivePath.Duration - millSinceBeginning
                                : millSinceBeginning - ActivePath.Delay, ActivePath.Start, ActivePath.Change,
                            ActivePath.Duration);
                        FrameCallback!.Invoke(value);
                        return;
                    }

                    if (CurrentStatus == AnimatorStatus.Playing)
                    {
                        if (_tempPaths.Count == 0)
                        {
                            // For the last path, we make sure that control is in end point
                            FrameCallback!.Invoke(_tempReverseRepeat ? ActivePath!.Start : ActivePath!.End);
                            ended = true;
                        }
                        else
                        {
                            if ((_tempReverseRepeat && ActivePath.Delay > 0) ||
                                (!_tempReverseRepeat && _tempPaths.FirstOrDefault()?.Delay > 0))
                                // Or if the next path or this one in reverse order has a delay
                            {
                                FrameCallback!.Invoke(_tempReverseRepeat ? ActivePath!.Start : ActivePath!.End);
                            }
                        }
                    }

                    if (_tempReverseRepeat && millSinceBeginning - ActivePath!.Duration < ActivePath!.Delay)
                    {
                        CurrentStatus = AnimatorStatus.OnHold;
                        return;
                    }

                    ActivePath = null;
                }

                if (!ended)
                {
                    return;
                }
            }

            if (Repeat)
            {
                lock (_tempPaths)
                {
                    _tempPaths.AddRange(_paths);
                    _tempReverseRepeat = ReverseRepeat && !_tempReverseRepeat;
                }

                millSinceBeginning = 0;
                continue;
            }

            Stop();
            EndCallback?.Invoke();
            break;
        }
    }
}
