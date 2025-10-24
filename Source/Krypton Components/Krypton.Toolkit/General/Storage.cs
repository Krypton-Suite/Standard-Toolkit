#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Base class for storage implementations.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public abstract class Storage : GlobalId
{
    #region Instance Fields
    private NeedPaintHandler? _needPaint;
    private NeedPaintHandler? _needPaintDelegate;
    #endregion

    #region Identity
    /// <summary>
    /// Returns a string that represents the current defaulted state.
    /// </summary>
    /// <returns>A string that represents the current defaulted state.</returns>
    public override string ToString() => !IsDefault ? @"Modified" : string.Empty;

    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public abstract bool IsDefault { get; }
    #endregion

    #region NeedPaint
    /// <summary>
    /// Gets and sets the need paint delegate for notifying paint requests.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public virtual NeedPaintHandler? NeedPaint
    {
        get => _needPaint;
        set => _needPaint = value;
    }
    #endregion

    #region NeedPaintDelegate
    /// <summary>
    /// Gets access to the need paint delegate.
    /// </summary>
    protected NeedPaintHandler NeedPaintDelegate => _needPaintDelegate ??= OnNeedPaint;

    #endregion

    #region PerformNeedPaint
    /// <summary>
    /// Fires the NeedPaint event.
    /// </summary>
    public void PerformNeedPaint() => OnNeedPaint(this, new NeedLayoutEventArgs(false));

    /// <summary>
    /// Fires the NeedPaint event.
    /// </summary>
    /// <param name="needLayout">Does the palette change require a layout.</param>
    public void PerformNeedPaint(bool needLayout) => OnNeedPaint(this, new NeedLayoutEventArgs(needLayout));

    #endregion

    #region OnNeedPaint
    /// <summary>
    /// Raises the NeedPaint event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An NeedLayoutEventArgs containing event data.</param>
    protected virtual void OnNeedPaint(object? sender, NeedLayoutEventArgs e) => _needPaint?.Invoke(this, e);

    #endregion
}