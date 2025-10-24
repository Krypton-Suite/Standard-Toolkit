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
/// Represents a hue, saturation, luminance triple.
/// </summary>
internal class ColorHSL : GlobalId
{
    #region Instance Fields
    private double _hue;
    private double _saturation;
    private double _luminance;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ColorHSL class.
    /// </summary>
    public ColorHSL()
    {
    }

    /// <summary>
    /// Initialize a new instance of the ColorHSL class.
    /// </summary>
    /// <param name="c">Initialize from an existing Color.</param>
    public ColorHSL(Color c)
    {
        // Initialize from the color instance
        _hue = c.GetHue() / 360f;
        _saturation = c.GetBrightness();
        _luminance = c.GetSaturation();
    }
    #endregion

    #region Color
    /// <summary>
    /// Get a Color instance from the HSL triple.
    /// </summary>
    public Color Color
    {
        get
        {
            double red = 0;
            double green = 0;
            double blue = 0;

            if (Luminance > 0)
            {
                if (Saturation == 0)
                {
                    red = green = blue = Luminance;
                }
                else
                {
                    double temp2;

                    if (Luminance <= 0.5)
                    {
                        temp2 = Luminance * (1.0 + Saturation);
                    }
                    else
                    {
                        temp2 = Luminance + Saturation - (Luminance * Saturation);
                    }

                    var temp1 = (2.0 * Luminance) - temp2;

                    double[] t3 = [Hue + (1.0 / 3.0), Hue, Hue - (1.0 / 3.0)];
                    double[] clr = [0, 0, 0];

                    for (var i = 0; i < 3; i++)
                    {
                        if (t3[i] < 0)
                        {
                            t3[i] += 1.0;
                        }

                        if (t3[i] > 1)
                        {
                            t3[i] -= 1.0;
                        }

                        if ((6.0 * t3[i]) < 1.0)
                        {
                            clr[i] = temp1 + ((temp2 - temp1) * t3[i] * 6.0);
                        }
                        else if ((2.0 * t3[i]) < 1.0)
                        {
                            clr[i] = temp2;
                        }
                        else if ((3.0 * t3[i]) < 2.0)
                        {
                            clr[i] = temp1 + ((temp2 - temp1) * ((2.0 / 3.0) - t3[i]) * 6.0);
                        }
                        else
                        {
                            clr[i] = temp1;
                        }
                    }

                    red = clr[0];
                    green = clr[1];
                    blue = clr[2];
                }
            }

            return Color.FromArgb((int)(255 * red), 
                (int)(255 * green), 
                (int)(255 * blue));
        }
    }
    #endregion

    #region Hue
    /// <summary>
    /// Gets and sets the hue.
    /// </summary>
    public double Hue
    {
        get => _hue;

        set
        {
            // Store new value
            _hue = value;

            switch (_hue)
            {
                // Limit check inside range of 0 -> 1
                case > 1:
                    _hue = 1;
                    break;
                case < 0:
                    _hue = 0;
                    break;
            }
        }
    }
    #endregion

    #region Saturation
    /// <summary>
    /// Gets and sets the saturation.
    /// </summary>
    public double Saturation
    {
        get => _saturation;

        set
        {
            // Store new value
            _saturation = value;

            switch (_saturation)
            {
                // Limit check inside range of 0 -> 1
                case > 1:
                    _saturation = 1;
                    break;
                case < 0:
                    _saturation = 0;
                    break;
            }
        }
    }
    #endregion

    #region Luminance
    /// <summary>
    /// Gets and sets the luminance.
    /// </summary>
    public double Luminance
    {
        get => _luminance;

        set
        {
            // Store new value
            _luminance = value;

            switch (_luminance)
            {
                // Limit check inside range of 0 -> 1
                case > 1:
                    _luminance = 1;
                    break;
                case < 0:
                    _luminance = 0;
                    break;
            }
        }
    }
    #endregion
}