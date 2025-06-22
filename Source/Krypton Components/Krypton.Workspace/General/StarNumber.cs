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

namespace Krypton.Workspace;

/// <summary>
/// Handle a star number which consists of a number with optional asterisk at the end.
/// </summary>
public class StarNumber
{
    #region Internal Fields
    private string _value;

    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the StarNumber class.
    /// </summary>
    public StarNumber()
    {
        _value = "*";
        UsingStar = true;
        FixedSize = 0;
        StarSize = 1;
    }

    /// <summary>
    /// Initialize a new instance of the StarNumber class.
    /// </summary>
    /// <param name="value">Initial value to process.</param>
    public StarNumber(string value) => Value = value;

    /// <summary>
    /// Gets a string representing the value of the instance.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => Value;

    #endregion

    #region Public
    /// <summary>
    /// Gets and sets the star notation and breaks it apart.
    /// </summary>
    public string Value
    {
        get => _value;

        set
        {
            // Validate the incoming value
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), @"Cannot be assigned a null value.");
            }

            // If it ends with an asterisk...
            if (value.EndsWith(@"*"))
            {
                // If there is only an asterisk in the string
                StarSize = value.Length == 1 ? 1 : double.Parse(value.Substring(0, value.Length - 1));

                UsingStar = true;
            }
            else
            {
                // No asterisk, so it should be just be an integer number
                FixedSize = int.Parse(value);
                UsingStar = false;
            }

            _value = value;
        }
    }

    /// <summary>
    /// Gets a value indicating if stars are being specified.
    /// </summary>
    public bool UsingStar { get; private set; }

    /// <summary>
    /// Gets the fixed size value.
    /// </summary>
    public int FixedSize { get; private set; }

    /// <summary>
    /// Gets the star size value.
    /// </summary>
    public double StarSize { get; private set; }

    #endregion

    #region Internal
    internal string PersistString
    {
        get
        {
            var builder = new StringBuilder();
            builder.Append(UsingStar ? "T," : "F,");
            builder.Append($"{FixedSize},");
            builder.Append(CommonHelper.DoubleToString(StarSize));
            return builder.ToString();
        }

        set
        {
            var parts = value.Split(',');
            UsingStar = (parts[0] == "T");
            FixedSize = int.Parse(parts[1]);
            StarSize = CommonHelper.StringToDouble(parts[2]);
        }
    }
    #endregion
}