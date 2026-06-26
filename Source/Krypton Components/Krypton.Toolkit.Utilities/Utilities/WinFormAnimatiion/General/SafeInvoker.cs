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
///     The safe invoker class is a delegate reference holder that always
///     execute them in the correct thread based on the passed control.
/// </summary>
internal class SafeInvoker
{
    private MethodInfo? _invokeMethod;

    private PropertyInfo? _invokeRequiredProperty;
    private object? _targetControl;

    /// <summary>
    ///     Initializes a new instance of the SafeInvoker class.
    /// </summary>
    /// <param name="action">
    ///     The callback to be invoked
    /// </param>
    /// <param name="targetControl">
    ///     The control to be used to invoke the callback in UI thread
    /// </param>
    public SafeInvoker(Action action, object? targetControl) : this((Delegate) action, targetControl)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the SafeInvoker class.
    /// </summary>
    /// <param name="action">
    ///     The callback to be invoked
    /// </param>
    /// <param name="targetControl">
    ///     The control to be used to invoke the callback in UI thread
    /// </param>
    protected SafeInvoker(Delegate action, object? targetControl)
    {
        UnderlyingDelegate = action;
        if (targetControl != null)
        {
            TargetControl = targetControl;
        }
    }

    /// <summary>
    ///     Initializes a new instance of the SafeInvoker class.
    /// </summary>
    /// <param name="action">
    ///     The callback to be invoked
    /// </param>
    public SafeInvoker(Action action) : this(action, null)
    {
    }

    /// <summary>
    ///     Gets or sets the reference to the control that's going to be used to invoke the callback in UI thread
    /// </summary>
    protected object? TargetControl
    {
        get => _targetControl;
        set
        {
            if (value == null)
            {
                return;
            }

            _invokeRequiredProperty = value.GetType()
                .GetProperty("InvokeRequired", BindingFlags.Instance | BindingFlags.Public);
            _invokeMethod = value.GetType()
                .GetMethod(
                    "Invoke",
                    BindingFlags.Instance | BindingFlags.Public,
                    Type.DefaultBinder,
                    [typeof(Delegate)],
                    null);
            if (_invokeRequiredProperty != null && _invokeMethod != null)
            {
                _targetControl = value;
            }
        }
    }


    /// <summary>
    ///     Gets the reference to the callback to be invoked
    /// </summary>
    protected Delegate UnderlyingDelegate { get; }

    /// <summary>
    ///     Invoke the contained callback
    /// </summary>
    public virtual void Invoke()
    {
        Invoke(null);
    }

    /// <summary>
    ///     Invoke the referenced callback
    /// </summary>
    /// <param name="value">The argument to send to the callback</param>
    protected void Invoke(object? value)
    {
        try
        {
            ThreadPool.QueueUserWorkItem(
                state =>
                {
                    try
                    {
                        if (TargetControl != null
                            && _invokeRequiredProperty != null
                            && _invokeMethod != null
                            && (bool)_invokeRequiredProperty.GetValue(TargetControl)!)
                        {
                            _invokeMethod.Invoke(
                                TargetControl,
                                [
                                    new Action(
                                        () => UnderlyingDelegate.DynamicInvoke(value != null
                                            ? [value]
                                            : null))
                                ]);
                            return;
                        }
                    }
                    catch
                    {
                        // ignored
                    }

                    UnderlyingDelegate.DynamicInvoke(value != null ? [value] : null);
                });
        }
        catch
        {
            // ignored
        }
    }
}
