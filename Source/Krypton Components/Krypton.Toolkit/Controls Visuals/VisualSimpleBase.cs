#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Extends the control base with some common changes relevant to krypton simple controls.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public abstract class VisualSimpleBase : VisualControlBase
{
    #region Instance Fields

    private bool _isRightToLeftLayout;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the VisualSimpleBase class.
    /// </summary>
    protected VisualSimpleBase()
    {
        _isRightToLeftLayout = false;
    }
    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the auto size mode.
    /// </summary>
    [Category(@"Layout")]
    [Description(@"Specifies if the control grows and shrinks to fit the contents exactly.")]
    [DefaultValue(AutoSizeMode.GrowOnly)]
    public virtual AutoSizeMode AutoSizeMode
    {
        // ReSharper disable RedundantBaseQualifier
        get => base.GetAutoSizeMode();
        // ReSharper restore RedundantBaseQualifier

        set
        {
            // ReSharper disable RedundantBaseQualifier
            if (value != base.GetAutoSizeMode())
            {
                base.SetAutoSizeMode(value);
                // ReSharper restore RedundantBaseQualifier

                // Only perform an immediate layout if
                // currently performing auto size operations
                if (AutoSize)
                {
                    PerformNeedPaint(true);
                }
            }
        }
    }

    /// <summary>
    /// Get the preferred size of the control based on a proposed size.
    /// </summary>
    /// <param name="proposedSize">Starting size proposed by the caller.</param>
    /// <returns>Calculated preferred size.</returns>
    public override Size GetPreferredSize(Size proposedSize)
    {
        // Do we have a manager to ask for a preferred size?
        if (ViewManager != null)
        {
            // When AutoSize is enabled, always use an unconstrained proposed size to get the true preferred size.
            // This ensures consistent behavior between GrowOnly and GrowAndShrink modes.
            // The proposedSize parameter represents available space, but for AutoSize controls,
            // we need the actual preferred size without constraints.
            Size layoutProposedSize = AutoSize
                ? new Size(int.MaxValue, int.MaxValue)
                : proposedSize;

            // Ask the view to perform a layout
            Size retSize = ViewManager.GetPreferredSize(Renderer, layoutProposedSize);

#if NETFRAMEWORK
            // Add padding to ensure consistent behavior between .NET Framework and .NET
            // In .NET Framework, Control.GetPreferredSize() didn't include Padding,
            // but in .NET it does, so we need to add it explicitly here for consistency.
            // Note: proposedSize represents available space, not current control size,
            // so we always add padding to the content size to get the total required size.
            retSize.Width += Padding.Horizontal;

            retSize.Height += Padding.Vertical;
#endif

            // For AutoSize with GrowAndShrink, use base class size only as fallback when the view
            // failed to calculate a valid preferred size (e.g. renderer not ready in designer).
            // Do NOT use it as a floor when we have valid content-based size, or labels with
            // short text (e.g. "K") would incorrectly stay at DefaultSize 90x25 instead of shrinking.
            if (AutoSize && GetAutoSizeMode() == AutoSizeMode.GrowAndShrink
                && (retSize.Width <= 0 || retSize.Height <= 0))
            {
                Size baseSize = base.GetPreferredSize(new Size(int.MaxValue, int.MaxValue));
                if (baseSize.Width > 0 && baseSize.Height > 0)
                {
                    retSize.Width = Math.Max(retSize.Width, baseSize.Width);
                    retSize.Height = Math.Max(retSize.Height, baseSize.Height);
                }
            }

            // Apply the maximum sizing
            if (MaximumSize.Width > 0)
            {
                retSize.Width = Math.Min(MaximumSize.Width, retSize.Width);
            }

            if (MaximumSize.Height > 0)
            {
                retSize.Height = Math.Min(MaximumSize.Height, retSize.Height);
            }

            // Apply the minimum sizing
            if (MinimumSize.Width > 0)
            {
                retSize.Width = Math.Max(MinimumSize.Width, retSize.Width);
            }

            if (MinimumSize.Height > 0)
            {
                retSize.Height = Math.Max(MinimumSize.Height, retSize.Height);
            }

            return retSize;
        }
        else
        {
            // Fall back on default control processing
            return base.GetPreferredSize(proposedSize);
        }
    }

    /// <summary>
    /// Gets or sets the background color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    /// <summary>
    /// Gets or sets the font of the text displayed by the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [AmbientValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override Font Font
    {
        get => base.Font;
        set => base.Font = value;
    }

    /// <summary>
    /// Gets or sets the foreground color for the control.
    /// </summary>
    [Browsable(false)]
    [Bindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [AllowNull]
    public override Color ForeColor
    {
        get => base.ForeColor;
        set => base.ForeColor = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.
    /// </summary>
    [Category(@"Appearance")]
    [Localizable(true)]
    [Description(@"Indicates whether the control should support RightToLeft layouts.")]
    [DefaultValue(typeof(RightToLeft), "No")]
    public override RightToLeft RightToLeft
    {
        get => base.RightToLeft;
        set
        {
            if (base.RightToLeft != value)
            {
                base.RightToLeft = value;
                PerformNeedPaint(true);
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the layout of the control is from right to left.
    /// </summary>
    [Category(@"Appearance")]
    [Localizable(true)]
    [Description(@"Indicates whether the layout of the control is from right to left.")]
    [DefaultValue(false)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool RightToLeftLayout
    {
        get => _isRightToLeftLayout;
        set
        {
            if (_isRightToLeftLayout != value)
            {
                _isRightToLeftLayout = value;
                OnRightToLeftLayoutChanged(EventArgs.Empty);
                PerformNeedPaint(true);
            }
        }
    }
    #endregion

    #region Protected Overrides
    /// <summary>
    /// Raises the RightToLeftChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing event data.</param>
    protected override void OnRightToLeftChanged(EventArgs e)
    {
        // Need re-layout to reflect change of layout direction
        PerformNeedPaint(true);

        base.OnRightToLeftChanged(e);
    }

    /// <summary>
    /// Raises the RightToLeftLayoutChanged event.
    /// </summary>
    /// <param name="e">An EventArgs containing event data.</param>
    /// <remarks>
    /// This method is provided for controls that don't have OnRightToLeftLayoutChanged in their base class.
    /// Derived classes can override this to provide custom handling when RightToLeftLayout changes.
    /// </remarks>
    protected virtual void OnRightToLeftLayoutChanged(EventArgs e) =>
        // Need re-layout to reflect change of layout direction
        PerformNeedPaint(true);

    #endregion
}