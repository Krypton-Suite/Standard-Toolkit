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
/// 
/// </summary>
public class KryptonProfessionalRenderer : ToolStripProfessionalRenderer
{
    #region Identity
    /// <summary>
    /// Initialise a new instance of the KryptonProfessionalRenderer class.
    /// </summary>
    /// <param name="kct">Source for text colors.</param>
    public KryptonProfessionalRenderer([DisallowNull] KryptonColorTable kct)
        : base(kct)
    {
        Debug.Assert(kct is not null);
        KCT = kct ?? throw new ArgumentNullException(nameof(kct));
    }
    #endregion

    #region KCT
    /// <summary>
    /// Gets access to the KryptonColorTable instance.
    /// </summary>
    public KryptonColorTable KCT { get; }

    #endregion

    #region OnRenderItemImage
    /// <summary>
    /// Raises the RenderItemImage event. 
    /// </summary>
    /// <param name="e">An ToolStripItemImageRenderEventArgs containing the event data.</param>
    protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
    {
        // Is this a min/restore/close pendant button
        if (e.Item.GetType().ToString() == "System.Windows.Forms.MdiControlStrip+ControlBoxMenuItem")
        {
            // Get access to the owning form of the mdi control strip
            if (e.ToolStrip!.Parent!.TopLevelControl is Form f)
            {
                // Get the mdi control strip instance
                PropertyInfo? piMCS = typeof(Form).GetProperty(@"MdiControlStrip", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField)!;
                if (piMCS != null)
                {
                    var mcs = piMCS.GetValue(f, null);
                    if (mcs != null)
                    {
                        // Get the min/restore/close internal menu items
                        Type mcsType = mcs.GetType();
                        FieldInfo? fiM = mcsType.GetField("minimize", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField)!;
                        FieldInfo? fiR = mcsType.GetField("restore", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField)!;
                        FieldInfo? fiC = mcsType.GetField("close", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField)!;
                        if ((fiM != null) && (fiR != null) && (fiC != null))
                        {
#pragma warning disable IDE0019 // Use pattern matching
                            var m = fiM.GetValue(mcs) as ToolStripMenuItem;
                            var r = fiR.GetValue(mcs) as ToolStripMenuItem;
                            var c = fiC.GetValue(mcs) as ToolStripMenuItem;
#pragma warning restore IDE0019 // Use pattern matching
                            if ((m != null) && (r != null) && (c != null))
                            {
                                // Compare the event provided image with the internal cached ones to discover the type of pendant button we are drawing
                                var specStyle = PaletteButtonSpecStyle.Generic;
                                if (m.Image == e.Image)
                                {
                                    specStyle = PaletteButtonSpecStyle.PendantMin;
                                }
                                else if (r.Image == e.Image)
                                {
                                    specStyle = PaletteButtonSpecStyle.PendantRestore;
                                }
                                else if (c.Image == e.Image)
                                {
                                    specStyle = PaletteButtonSpecStyle.PendantClose;
                                }

                                // A match, means we have a known pendant button
                                if (specStyle != PaletteButtonSpecStyle.Generic)
                                {
                                    // Grab the palette pendant details needed for drawing
                                    Image? paletteImage = KCT.Palette.GetButtonSpecImage(specStyle, PaletteState.Normal);
                                    Color transparentColor = KCT.Palette.GetButtonSpecImageTransparentColor(specStyle);

                                    // Finally we actually have an image to draw!
                                    if (paletteImage != null)
                                    {
                                        using var attribs = new ImageAttributes();
                                        // Setup mapping to make required color transparent
                                        var remap = new ColorMap
                                        {
                                            OldColor = transparentColor,
                                            NewColor = Color.Transparent
                                        };
                                        attribs.SetRemapTable([remap]);

                                        // Phew, actually draw the darn thing
                                        e.Graphics.DrawImage(paletteImage, e.ImageRectangle,
                                            0, 0, e.Image!.Width, e.Image.Height,
                                            GraphicsUnit.Pixel, attribs);

                                        // Do not let base class draw system defined image
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        base.OnRenderItemImage(e);
    }
    #endregion

    #region OnRenderToolStripBorder
    /// <summary>
    /// Raises the RenderToolStripBorder event. 
    /// </summary>
    /// <param name="e">An ToolStripRenderEventArgs containing the event data.</param>
    protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
    {
        // D0 not draw the annoying status strip single line that is not needed
        if (e.ToolStrip is not StatusStrip)
        {
            base.OnRenderToolStripBorder(e);
        }
    }
    #endregion
}