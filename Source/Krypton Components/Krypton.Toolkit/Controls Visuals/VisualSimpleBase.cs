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
/// Extends the control base with some common changes relevant to krypton simple controls.
/// </summary>
[ToolboxItem(false)]
[DesignerCategory(@"code")]
public abstract class VisualSimpleBase : VisualControlBase
{
    #region Identity
    /// <summary>
    /// Initialize a new instance of the VisualSimpleBase class.
    /// </summary>
    protected VisualSimpleBase()
    {
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
            // Do NOT use it as a floor when we have valid content-based size, or controls with
            // short text would incorrectly stay at their initial default size instead of shrinking.
            if (AutoSize && GetAutoSizeMode() == AutoSizeMode.GrowAndShrink)
            {
                if (retSize.Width <= 0 || retSize.Height <= 0)
                {
                    Size baseSize = base.GetPreferredSize(new Size(int.MaxValue, int.MaxValue));
                    if (baseSize.Width > 0 && baseSize.Height > 0)
                    {
                        retSize.Width = Math.Max(retSize.Width, baseSize.Width);
                        retSize.Height = Math.Max(retSize.Height, baseSize.Height);
                    }
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
    /// Sets the bounds of the control. For AutoSize + GrowAndShrink controls, force the size
    /// to preferred size so shrink/grow behavior tracks content in runtime and designer paths.
    /// </summary>
    /// <param name="x">The new Left property value of the control.</param>
    /// <param name="y">The new Top property value of the control.</param>
    /// <param name="width">The new Width property value of the control.</param>
    /// <param name="height">The new Height property value of the control.</param>
    /// <param name="specified">A bitwise combination of the BoundsSpecified values.</param>
    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
        if (AutoSize)
        {
            Size preferredSize = GetPreferredSize(new Size(int.MaxValue, int.MaxValue));
            Rectangle virtualScreen = SystemInformation.VirtualScreen;
            int maxSensibleWidth = (int)Math.Min(int.MaxValue, Math.Max(1L, (long)Math.Abs(virtualScreen.Width) * 2L));
            int maxSensibleHeight = (int)Math.Min(int.MaxValue, Math.Max(1L, (long)Math.Abs(virtualScreen.Height) * 2L));

            // Only apply sensible calculated sizes to avoid unstable initialization values.
            // Use the OS virtual screen size as the baseline instead of a hard-coded pixel limit.
            if (preferredSize.Width > 0 && preferredSize.Height > 0
                && preferredSize.Width <= maxSensibleWidth && preferredSize.Height <= maxSensibleHeight)
            {
                if (GetAutoSizeMode() == AutoSizeMode.GrowAndShrink)
                {
                    width = preferredSize.Width;
                    height = preferredSize.Height;
                }
                else
                {
                    // GrowOnly semantics: allow growth to preferred size, never force shrink.
                    width = Math.Max(Width, preferredSize.Width);
                    height = Math.Max(Height, preferredSize.Height);
                }

                specified |= BoundsSpecified.Size;
            }
        }

        base.SetBoundsCore(x, y, width, height, specified);
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
    #endregion
}
