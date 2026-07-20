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
///     The two-dimensional animator class, useful for animating values
///     created from two underlying values
/// </summary>
internal class Animator2D : IAnimator
{
    private readonly List<Path2D> _paths = [];


    /// <summary>
    ///     The callback to get invoked at the end of the animation
    /// </summary>
    protected SafeInvoker? EndCallback;

    /// <summary>
    ///     The callback to get invoked at each frame
    /// </summary>
    protected SafeInvoker<Float2D>? FrameCallback;

    /// <summary>
    ///     A boolean value indicating if the EndInvoker already invoked
    /// </summary>
    protected bool IsEnded;

    /// <summary>
    ///     The target object to change the property of
    /// </summary>
    protected object? TargetObject;

    /// <summary>
    ///     The latest horizontal value
    /// </summary>
    protected float? XValue;

    /// <summary>
    ///     The latest vertical value
    /// </summary>
    protected float? YValue;


    /// <summary>
    ///     Initializes a new instance of the <see cref="Animator2D" /> class.
    /// </summary>
    public Animator2D()
        : this([])
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Animator2D" /> class.
    /// </summary>
    /// <param name="fpsLimiter">
    ///     Limits the maximum frames per seconds
    /// </param>
    public Animator2D(FPSLimiterKnownValues fpsLimiter)
        : this([], fpsLimiter)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Animator2D" /> class.
    /// </summary>
    /// <param name="path">
    ///     The path of the animation
    /// </param>
    public Animator2D(Path2D path)
        : this([path])
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Animator2D" /> class.
    /// </summary>
    /// <param name="path">
    ///     The path of the animation
    /// </param>
    /// <param name="fpsLimiter">
    ///     Limits the maximum frames per seconds
    /// </param>
    public Animator2D(Path2D path, FPSLimiterKnownValues fpsLimiter)
        : this([path], fpsLimiter)
    {
    }


    /// <summary>
    ///     Initializes a new instance of the <see cref="Animator2D" /> class.
    /// </summary>
    /// <param name="paths">
    ///     An array containing the list of paths of the animation
    /// </param>
    public Animator2D(Path2D[] paths) : this(paths, FPSLimiterKnownValues.LimitThirty)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Animator2D" /> class.
    /// </summary>
    /// <param name="paths">
    ///     An array containing the list of paths of the animation
    /// </param>
    /// <param name="fpsLimiter">
    ///     Limits the maximum frames per seconds
    /// </param>
    public Animator2D(Path2D[] paths, FPSLimiterKnownValues fpsLimiter)
    {
        HorizontalAnimator = new Animator(fpsLimiter);
        VerticalAnimator = new Animator(fpsLimiter);
        Paths = paths;
    }

    /// <summary>
    ///     Gets the currently active path.
    /// </summary>
    public Path2D ActivePath => new(HorizontalAnimator.ActivePath!, VerticalAnimator.ActivePath!);

    /// <summary>
    ///     Gets the horizontal animator.
    /// </summary>
    public Animator HorizontalAnimator { get; protected set; }

    /// <summary>
    ///     Gets the vertical animator.
    /// </summary>
    public Animator VerticalAnimator { get; protected set; }

    /// <summary>
    ///     Gets or sets an array containing the list of paths of the animation
    /// </summary>
    /// <exception cref="InvalidOperationException">Animation is running</exception>
    public Path2D[] Paths
    {
        get => _paths.ToArray();
        set
        {
            if (CurrentStatus == AnimatorStatus.Stopped)
            {
                _paths.Clear();
                _paths.AddRange(value);
                var pathsH = new List<Path>();
                var pathsV = new List<Path>();
                foreach (var p in value)
                {
                    pathsH.Add(p.HorizontalPath);
                    pathsV.Add(p.VerticalPath);
                }

                HorizontalAnimator.Paths = pathsH.ToArray();
                VerticalAnimator.Paths = pathsV.ToArray();
            }
            else
            {
                throw new InvalidOperationException("Animation is running.");
            }
        }
    }

    /// <summary>
    ///     Gets or sets a value indicating whether animator should repeat the animation after its ending
    /// </summary>
    public virtual bool Repeat
    {
        get => HorizontalAnimator.Repeat && VerticalAnimator.Repeat;

        set => HorizontalAnimator.Repeat = VerticalAnimator.Repeat = value;
    }

    /// <summary>
    ///     Gets or sets a value indicating whether animator should repeat the animation in reverse after its ending.
    /// </summary>
    public virtual bool ReverseRepeat
    {
        get => HorizontalAnimator.ReverseRepeat && VerticalAnimator.ReverseRepeat;

        set => HorizontalAnimator.ReverseRepeat = VerticalAnimator.ReverseRepeat = value;
    }

    /// <summary>
    ///     Gets the current status of the animation
    /// </summary>
    public virtual AnimatorStatus CurrentStatus
    {
        get
        {
            if (HorizontalAnimator.CurrentStatus == AnimatorStatus.Stopped
                && VerticalAnimator.CurrentStatus == AnimatorStatus.Stopped)
            {
                return AnimatorStatus.Stopped;
            }

            if (HorizontalAnimator.CurrentStatus == AnimatorStatus.Paused
                && VerticalAnimator.CurrentStatus == AnimatorStatus.Paused)
            {
                return AnimatorStatus.Paused;
            }

            if (HorizontalAnimator.CurrentStatus == AnimatorStatus.OnHold
                && VerticalAnimator.CurrentStatus == AnimatorStatus.OnHold)
            {
                return AnimatorStatus.OnHold;
            }

            return AnimatorStatus.Playing;
        }
    }

    /// <summary>
    ///     Pause the animation
    /// </summary>
    public virtual void Pause()
    {
        if (CurrentStatus == AnimatorStatus.OnHold || CurrentStatus == AnimatorStatus.Playing)
        {
            HorizontalAnimator.Pause();
            VerticalAnimator.Pause();
        }
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
            new SafeInvoker<Float2D>(
                value =>
                    prop.SetValue(TargetObject, Convert.ChangeType(value, prop.PropertyType), null),
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
            new SafeInvoker<Float2D>(
                value =>
                    property.SetValue(TargetObject, Convert.ChangeType(value, property.PropertyType), null),
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
            HorizontalAnimator.Resume();
            VerticalAnimator.Resume();
        }
    }

    /// <summary>
    ///     Stops the animation and resets its status, resume is no longer possible
    /// </summary>
    public virtual void Stop()
    {
        HorizontalAnimator.Stop();
        VerticalAnimator.Stop();
        XValue = YValue = null;
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
    public void Play(object targetObject, KnownProperties property)
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
    public void Play(object targetObject, KnownProperties property, SafeInvoker? endCallback)
    {
        Play(targetObject, property.ToString(), endCallback);
    }

    /// <summary>
    ///     Starts the playing of the animation
    /// </summary>
    /// <param name="frameCallback">
    ///     The callback to get invoked at each frame
    /// </param>
    public void Play(SafeInvoker<Float2D> frameCallback)
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
    public void Play(SafeInvoker<Float2D> frameCallback, SafeInvoker? endCallback)
    {
        Stop();
        FrameCallback = frameCallback;
        EndCallback = endCallback;
        HorizontalAnimator.Play(
            new SafeInvoker<float>(
                value =>
                {
                    XValue = value;
                    InvokeSetter();
                }),
            new SafeInvoker(InvokeFinisher));
        VerticalAnimator.Play(
            new SafeInvoker<float>(
                value =>
                {
                    YValue = value;
                    InvokeSetter();
                }),
            new SafeInvoker(InvokeFinisher));
    }

    private void InvokeFinisher()
    {
        if (EndCallback != null && !IsEnded)
        {
            lock (EndCallback)
            {
                if (CurrentStatus == AnimatorStatus.Stopped)
                {
                    IsEnded = true;
                    EndCallback.Invoke();
                }
            }
        }
    }

    private void InvokeSetter()
    {
        if (XValue != null && YValue != null)
        {
            FrameCallback!.Invoke(new Float2D(XValue.Value, YValue.Value));
        }
    }
}