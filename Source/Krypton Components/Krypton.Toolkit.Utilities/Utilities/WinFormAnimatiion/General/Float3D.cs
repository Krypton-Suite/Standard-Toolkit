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
///     The Float3D class contains two <see langword="float" /> values and
///     represents a point in a 3D plane
/// </summary>
internal class Float3D : IConvertible, IEquatable<Float3D>, IEquatable<Color>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Float3D" /> class.
    /// </summary>
    /// <param name="x">
    ///     The horizontal value
    /// </param>
    /// <param name="y">
    ///     The vertical value
    /// </param>
    /// <param name="z">
    ///     The depth value
    /// </param>
    public Float3D(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Float3D" /> class.
    /// </summary>
    public Float3D() : this(0, 0, 0)
    {
    }

    /// <summary>
    ///     Gets the horizontal value of the point
    /// </summary>
    public float X { get; set; }

    /// <summary>
    ///     Gets the vertical value of the point
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    ///     Gets the depth value of the point
    /// </summary>
    public float Z { get; set; }

    /// <summary>
    ///     Returns the <see cref="T:System.TypeCode" /> for this instance.
    /// </summary>
    /// <returns>
    ///     The enumerated constant that is the <see cref="T:System.TypeCode" /> of the class or value type that implements
    ///     this interface.
    /// </returns>
    public TypeCode GetTypeCode()
    {
        return TypeCode.Object;
    }


    /// <summary>
    ///     Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting
    ///     information.
    /// </summary>
    /// <returns>
    ///     A Boolean value equivalent to the value of this instance.
    /// </returns>
    /// <param name="provider">
    ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
    ///     culture-specific formatting information.
    /// </param>
    /// <exception cref="InvalidCastException">This method is not supported</exception>
    public bool ToBoolean(IFormatProvider? provider)
    {
        throw new InvalidCastException();
    }

    /// <summary>
    ///     Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific
    ///     formatting information.
    /// </summary>
    /// <returns>
    ///     An 8-bit unsigned integer equivalent to the value of this instance.
    /// </returns>
    /// <param name="provider">
    ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
    ///     culture-specific formatting information.
    /// </param>
    /// <exception cref="InvalidCastException">This method is not supported</exception>
    public byte ToByte(IFormatProvider? provider)
    {
        throw new InvalidCastException();
    }

    /// <summary>
    ///     Converts the value of this instance to an equivalent Unicode character using the specified culture-specific
    ///     formatting information.
    /// </summary>
    /// <returns>
    ///     A Unicode character equivalent to the value of this instance.
    /// </returns>
    /// <param name="provider">
    ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
    ///     culture-specific formatting information.
    /// </param>
    /// <exception cref="InvalidCastException">This method is not supported</exception>
    public char ToChar(IFormatProvider? provider)
    {
        throw new InvalidCastException();
    }


    /// <summary>
    ///     Converts the value of this instance to an equivalent <see cref="T:System.DateTime" /> using the specified
    ///     culture-specific formatting information.
    /// </summary>
    /// <returns>
    ///     A <see cref="T:System.DateTime" /> instance equivalent to the value of this instance.
    /// </returns>
    /// <param name="provider">
    ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
    ///     culture-specific formatting information.
    /// </param>
    /// <exception cref="InvalidCastException">This method is not supported</exception>
    public DateTime ToDateTime(IFormatProvider? provider)
    {
        throw new InvalidCastException();
    }


    /// <summary>
    ///     Converts the value of this instance to an equivalent <see cref="T:System.Decimal" /> number using the specified
    ///     culture-specific formatting information.
    /// </summary>
    /// <returns>
    ///     A <see cref="T:System.Decimal" /> number equivalent to the value of this instance.
    /// </returns>
    /// <param name="provider">
    ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
    ///     culture-specific formatting information.
    /// </param>
    /// <exception cref="InvalidCastException">This method is not supported</exception>
    public decimal ToDecimal(IFormatProvider? provider)
    {
        throw new InvalidCastException();
    }


    /// <summary>
    ///     Converts the value of this instance to an equivalent double-precision floating-point number using the specified
    ///     culture-specific formatting information.
    /// </summary>
    /// <returns>
    ///     A double-precision floating-point number equivalent to the value of this instance.
    /// </returns>
    /// <param name="provider">
    ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
    ///     culture-specific formatting information.
    /// </param>
    /// <exception cref="InvalidCastException">This method is not supported</exception>
    public double ToDouble(IFormatProvider? provider)
    {
        throw new InvalidCastException();
    }


    /// <summary>
    ///     Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific
    ///     formatting information.
    /// </summary>
    /// <returns>
    ///     An 16-bit signed integer equivalent to the value of this instance.
    /// </returns>
    /// <param name="provider">
    ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
    ///     culture-specific formatting information.
    /// </param>
    /// <exception cref="InvalidCastException">This method is not supported</exception>
    public short ToInt16(IFormatProvider? provider)
    {
        throw new InvalidCastException();
    }

    /// <summary>
    ///     Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific
    ///     formatting information.
    /// </summary>
    /// <returns>
    ///     An 32-bit signed integer equivalent to the value of this instance.
    /// </returns>
    /// <param name="provider">
    ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
    ///     culture-specific formatting information.
    /// </param>
    /// <exception cref="InvalidCastException">This method is not supported</exception>
    public int ToInt32(IFormatProvider? provider)
    {
        throw new InvalidCastException();
    }


    /// <summary>
    ///     Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific
    ///     formatting information.
    /// </summary>
    /// <returns>
    ///     An 64-bit signed integer equivalent to the value of this instance.
    /// </returns>
    /// <param name="provider">
    ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
    ///     culture-specific formatting information.
    /// </param>
    /// <exception cref="InvalidCastException">This method is not supported</exception>
    public long ToInt64(IFormatProvider? provider)
    {
        throw new InvalidCastException();
    }


    /// <summary>
    ///     Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific
    ///     formatting information.
    /// </summary>
    /// <returns>
    ///     An 8-bit signed integer equivalent to the value of this instance.
    /// </returns>
    /// <param name="provider">
    ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
    ///     culture-specific formatting information.
    /// </param>
    /// <exception cref="InvalidCastException">This method is not supported</exception>
    public sbyte ToSByte(IFormatProvider? provider)
    {
        throw new InvalidCastException();
    }


    /// <summary>
    ///     Converts the value of this instance to an equivalent single-precision floating-point number using the specified
    ///     culture-specific formatting information.
    /// </summary>
    /// <returns>
    ///     A single-precision floating-point number equivalent to the value of this instance.
    /// </returns>
    /// <param name="provider">
    ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
    ///     culture-specific formatting information.
    /// </param>
    /// <exception cref="InvalidCastException">This method is not supported</exception>
    public float ToSingle(IFormatProvider? provider)
    {
        throw new InvalidCastException();
    }

    /// <summary>
    ///     Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific
    ///     formatting information.
    /// </summary>
    /// <returns>
    ///     An 16-bit unsigned integer equivalent to the value of this instance.
    /// </returns>
    /// <param name="provider">
    ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
    ///     culture-specific formatting information.
    /// </param>
    /// <exception cref="InvalidCastException">This method is not supported</exception>
    public ushort ToUInt16(IFormatProvider? provider)
    {
        throw new InvalidCastException();
    }


    /// <summary>
    ///     Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific
    ///     formatting information.
    /// </summary>
    /// <returns>
    ///     An 32-bit unsigned integer equivalent to the value of this instance.
    /// </returns>
    /// <param name="provider">
    ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
    ///     culture-specific formatting information.
    /// </param>
    /// <exception cref="InvalidCastException">This method is not supported</exception>
    public uint ToUInt32(IFormatProvider? provider)
    {
        throw new InvalidCastException();
    }


    /// <summary>
    ///     Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific
    ///     formatting information.
    /// </summary>
    /// <returns>
    ///     An 64-bit unsigned integer equivalent to the value of this instance.
    /// </returns>
    /// <param name="provider">
    ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
    ///     culture-specific formatting information.
    /// </param>
    /// <exception cref="InvalidCastException">This method is not supported</exception>
    public ulong ToUInt64(IFormatProvider? provider)
    {
        throw new InvalidCastException();
    }

    /// <summary>
    ///     Converts the value of this instance to an equivalent <see cref="T:System.String" /> using the specified
    ///     culture-specific formatting information.
    /// </summary>
    /// <returns>
    ///     A <see cref="T:System.String" /> instance equivalent to the value of this instance.
    /// </returns>
    /// <param name="provider">
    ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
    ///     culture-specific formatting information.
    /// </param>
    public string ToString(IFormatProvider? provider)
    {
        return ToString();
    }

    /// <summary>
    ///     Converts the value of this instance to an <see cref="T:System.Object" /> of the specified
    ///     <see cref="T:System.Type" /> that has an equivalent value, using the specified culture-specific formatting
    ///     information.
    /// </summary>
    /// <returns>
    ///     An <see cref="T:System.Object" /> instance of type <paramref name="conversionType" /> whose value is equivalent to
    ///     the value of this instance.
    /// </returns>
    /// <param name="conversionType">The <see cref="T:System.Type" /> to which the value of this instance is converted. </param>
    /// <param name="provider">
    ///     An <see cref="T:System.IFormatProvider" /> interface implementation that supplies
    ///     culture-specific formatting information.
    /// </param>
    public object ToType(Type conversionType, IFormatProvider? provider)
    {
        if (conversionType == typeof(Color))
        {
            return (Color) this;
        }

        throw new InvalidCastException();
    }

    /// <summary>
    ///     Indicates whether the current object is equal to a <see cref="Color" /> object.
    /// </summary>
    /// <returns>
    ///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
    /// </returns>
    /// <param name="other">An object to compare with this object.</param>
    public bool Equals(Color other)
    {
        return this == other;
    }

    /// <summary>
    ///     Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <returns>
    ///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
    /// </returns>
    /// <param name="other">An object to compare with this object.</param>
    public bool Equals(Float3D? other)
    {
        return other is not null && this == other;
    }

    /// <summary>
    ///     Determines whether the specified <see cref="T:System.Object" /> is equal to the current
    ///     <see cref="T:System.Object" />.
    /// </summary>
    /// <returns>
    ///     true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />;
    ///     otherwise, false.
    /// </returns>
    /// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        var conversionType = obj.GetType();
        if (conversionType == typeof(Color))
        {
            return this == (Color) obj;
        }

        return obj.GetType() == GetType() && Equals((Float3D) obj);
    }

    /// <summary>
    ///     Serves as a hash function for a particular type. This code will change of the values of the X and Y changes. Make
    ///     sure to not change the values while stored in a hash dependent data structure.
    /// </summary>
    /// <returns>
    ///     A hash code for the current <see cref="Float3D" />.
    /// </returns>
    public override int GetHashCode()
    {
        unchecked
        {
            // ReSharper disable NonReadonlyMemberInGetHashCode
            var hashCode = X.GetHashCode();
            hashCode = (hashCode * 397) ^ Y.GetHashCode();
            hashCode = (hashCode * 397) ^ Z.GetHashCode();
            return hashCode;
            // ReSharper restore NonReadonlyMemberInGetHashCode
        }
    }


    /// <summary>
    ///     Compares two <see cref="Float3D" /> objects for equality
    /// </summary>
    /// <param name="left">Left <see cref="Float3D" /> object</param>
    /// <param name="right">Right <see cref="Float3D" /> object</param>
    /// <returns>true if both objects are equal, otherwise false</returns>
    public static bool operator ==(Float3D left, Float3D right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
        {
            return ReferenceEquals(left, null) && ReferenceEquals(right, null);
        }

        // ReSharper disable CompareOfFloatsByEqualityOperator
        return ReferenceEquals(left, right) ||
               ((double) left.X == right.X && (double) left.Y == right.Y && (double) left.Z == right.Z);
        // ReSharper restore CompareOfFloatsByEqualityOperator
    }

    /// <summary>
    ///     Compares two <see cref="Float3D" /> objects for in-equality
    /// </summary>
    /// <param name="left">Left <see cref="Float3D" /> object</param>
    /// <param name="right">Right <see cref="Float3D" /> object</param>
    /// <returns>false if both objects are equal, otherwise true</returns>
    public static bool operator !=(Float3D left, Float3D right)
    {
        return !(left == right);
    }

    /// <summary>
    ///     Represents the values as an instance of the <see cref="Color" /> class
    /// </summary>
    /// <param name="float3D">
    ///     The <see cref="Float3D" /> class to convert
    /// </param>
    /// <returns>
    ///     A new instance of the <see cref="Color" /> class representing the values in the <see cref="Float3D" /> instance
    /// </returns>
    public static implicit operator Color(Float3D float3D)
    {
        return Color.FromArgb((int) float3D.X, (int) float3D.Y, (int) float3D.Z);
    }

    /// <summary>
    ///     Returns a string that represents the current <see cref="Float3D" />.
    /// </summary>
    /// <returns>
    ///     A string that represents the current <see cref="Float3D" />.
    /// </returns>
    public override string ToString() => $"( {X:0.00}, {Y:0.00}, {Z:0.00} )";

    /// <summary>
    ///     Creates and returns a new instance of the <see cref="Float3D" /> class from a <see cref="Color" /> instance
    /// </summary>
    /// <param name="color">The object to create the <see cref="Float3D" /> instance from</param>
    /// <returns>The newly created <see cref="Float3D" /> instance</returns>
    public static Float3D FromColor(Color color) => new(color.R, color.G, color.B);
}
