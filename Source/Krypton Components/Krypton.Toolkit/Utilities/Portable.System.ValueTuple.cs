#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), et al. 2021 - 2024. All rights reserved. 
 */
#endregion

// ReSharper disable UnusedMember.Global
// ReSharper disable SuspiciousTypeConversion.Global
// ReSharper disable JoinDeclarationAndInitializer

// Above comment from https://github.com/igor-tkachev/Portable.System.ValueTuple
// Smurf-IV: Then
// - Reduced to only support Tuple'T2 and T4
// - Remove other frameworks that Krypton does not support
// - Remove public API
// - Apply "Preview" coding style(s)

#if NET462

namespace System
{
    using Collections;
    using Collections.Generic;

    using Runtime.CompilerServices;
    using Runtime.InteropServices;

    namespace Runtime.CompilerServices
    {
        /// <summary>
        /// This interface is required for types that want to be indexed into by dynamic patterns.
        /// </summary>
        internal interface ITuple
        {
            /// <summary>
            /// The number of positions in this data structure.
            /// </summary>
            int Length { get; }

            /// <summary>
            /// Get the element at position <param name="index"/>.
            /// </summary>
            object this[int index] { get; }
        }

        /// <summary>
        /// Indicates that the use of <see cref="T:System.ValueTuple" /> on a member is meant to be treated as a tuple with element names.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property |
                        AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter |
                        AttributeTargets.ReturnValue)]
        internal sealed class TupleElementNamesAttribute : Attribute
        {
            private readonly string[] _transformNames;

            /// <summary>
            /// Specifies, in a pre-order depth-first traversal of a type's
            /// construction, which <see cref="T:System.ValueTuple" /> elements are
            /// meant to carry element names.
            /// </summary>
            public IList<string> TransformNames => _transformNames;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.TupleElementNamesAttribute" /> class.
            /// </summary>
            /// <param name="transformNames">
            /// Specifies, in a pre-order depth-first traversal of a type's
            /// construction, which <see cref="T:System.ValueTuple" /> occurrences are
            /// meant to carry element names.
            /// </param>
            /// <remarks>
            /// This constructor is meant to be used on types that contain an
            /// instantiation of <see cref="T:System.ValueTuple" /> that contains
            /// element names.  For instance, if <c>C</c> is a generic type with
            /// two type parameters, then a use of the constructed type <c>C{<see cref="T:System.ValueTuple`2" />, <see cref="T:System.ValueTuple`3" /></c> might be intended to
            /// treat the first type argument as a tuple with element names and the
            /// second as a tuple without element names. In which case, the
            /// appropriate attribute specification should use a
            /// <c>transformNames</c> value of <c>{ "name1", @"name2", null, null,
            /// null }</c>.
            /// </remarks>
            public TupleElementNamesAttribute(string[] transformNames) => _transformNames = transformNames ?? throw new ArgumentNullException(nameof(transformNames));
        }
    }

    /// <summary>
    /// Helper so we can call some tuple methods recursively without knowing the underlying types.
    /// </summary>
    internal interface IValueTupleInternal : ITuple
    {
        int GetHashCode(IEqualityComparer comparer);
        string ToStringEnd();
    }

    /// <summary>
    /// The ValueTuple types (from arity 0 to 8) comprise the runtime implementation that underlies tuples in C# and struct tuples in F#.
    /// Aside from created via language syntax, they are most easily created via the ValueTuple.Create factory methods.
    /// The System.ValueTuple types differ from the System.Tuple types in that:
    /// - they are structs rather than classes,
    /// - they are mutable rather than readonly, and
    /// - their members (such as Item1, Item2, etc) are fields rather than properties.
    /// </summary>
    [Serializable]
    internal struct ValueTuple : IEquatable<ValueTuple>,
        IStructuralEquatable, IStructuralComparable,
            IComparable, IComparable<ValueTuple>, IValueTupleInternal
    {
        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple"/> instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is a <see cref="ValueTuple"/>.</returns>
        public override bool Equals(object obj) => obj is ValueTuple;

        /// <summary>Returns a value indicating whether this instance is equal to a specified value.</summary>
        /// <param name="other">An instance to compare to this instance.</param>
        /// <returns>true if <paramref name="other"/> has the same value as this instance; otherwise, false.</returns>
        public bool Equals(ValueTuple other) => true;

        int IComparable.CompareTo(object other)
        {
            return other switch
            {
                null => 1,
                ValueTuple => 0,
                _ => throw new ArgumentException(@"Incorrect type", nameof(other))
            };
        }

        /// <summary>Compares this instance to a specified instance and returns an indication of their relative values.</summary>
        /// <param name="other">An instance to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.
        /// Returns less than zero if this instance is less than <paramref name="other"/>, zero if this
        /// instance is equal to <paramref name="other"/>, and greater than zero if this instance is greater
        /// than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(ValueTuple other) => 0;

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode() => 0;

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer) => other is ValueTuple;

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer) => 0;

        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            return other switch
            {
                null => 1,
                ValueTuple => 0,
                _ => throw new ArgumentException(@"Incorrect type", nameof(other))
            };
        }

        int IValueTupleInternal.GetHashCode(IEqualityComparer comparer) => 0;

        /// <summary>
        /// Returns a string that represents the value of this <see cref="ValueTuple"/> instance.
        /// </summary>
        /// <returns>The string representation of this <see cref="ValueTuple"/> instance.</returns>
        /// <remarks>
        /// The string returned by this method takes the form <c>()</c>.
        /// </remarks>
        public override string ToString() => @"()";

        string IValueTupleInternal.ToStringEnd() => @")";

        /// <summary>
        /// The number of positions in this data structure.
        /// </summary>
        int ITuple.Length => 0;

        /// <summary>
        /// Get the element at position <param name="index"/>.
        /// </summary>
        object ITuple.this[int index] => throw new IndexOutOfRangeException();

        /// <summary>Creates a new struct 0-tuple.</summary>
        /// <returns>A 0-tuple.</returns>
        public static ValueTuple Create() => new();

        /// <summary>Creates a new struct 2-tuple, or pair.</summary>
        /// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
        /// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
        /// <param name="item1">The value of the first component of the tuple.</param>
        /// <param name="item2">The value of the second component of the tuple.</param>
        /// <returns>A 2-tuple (pair) whose value is (item1, item2).</returns>
        public static ValueTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2) => new(item1, item2);

		/// <summary>Creates a new struct 4-tuple, or quadruple.</summary>
		/// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
		/// <typeparam name="T3">The type of the third component of the tuple.</typeparam>
		/// <typeparam name="T4">The type of the fourth component of the tuple.</typeparam>
		/// <param name="item1">The value of the first component of the tuple.</param>
		/// <param name="item2">The value of the second component of the tuple.</param>
		/// <param name="item3">The value of the third component of the tuple.</param>
		/// <param name="item4">The value of the fourth component of the tuple.</param>
		/// <returns>A 4-tuple (quadruple) whose value is (item1, item2, item3, item4).</returns>
		public static ValueTuple<T1,T2,T3,T4> Create<T1,T2,T3,T4>(T1 item1, T2 item2, T3 item3, T4 item4) =>
			new ValueTuple<T1,T2,T3,T4>(item1, item2, item3, item4);

        internal static int CombineHashCodes(int h1, int h2) => Combine(Combine(_randomSeed, h1), h2);

		internal static int CombineHashCodes(int h1, int h2, int h3) => Combine(CombineHashCodes(h1, h2), h3);
        
        internal static int CombineHashCodes(int h1, int h2, int h3, int h4) => Combine(CombineHashCodes(h1, h2, h3), h4);

        private static readonly int _randomSeed = new Random().Next(int.MinValue, int.MaxValue);

        private static int Combine(int h1, int h2)
        {
            // RyuJIT optimizes this to use the ROL instruction
            // Related GitHub pull request: dotnet/coreclr#1830
            var rol5 = ((uint)h1 << 5) | ((uint)h1 >> 27);
            return ((int)rol5 + h1) ^ h2;
        }
    }

    /// <summary>
    /// Represents a 2-tuple, or pair, as a value type.
    /// </summary>
    /// <typeparam name="T1">The type of the tuple's first component.</typeparam>
    /// <typeparam name="T2">The type of the tuple's second component.</typeparam>
    [Serializable]
    [StructLayout(LayoutKind.Auto)]
    public struct ValueTuple<T1, T2> : IEquatable<ValueTuple<T1, T2>>,
        IStructuralEquatable, IStructuralComparable,
            IComparable, IComparable<ValueTuple<T1, T2>>, IValueTupleInternal
    {
        /// <summary>
        /// The current <see cref="ValueTuple{T1,T2}"/> instance's first component.
        /// </summary>
        public readonly T1 Item1;

        /// <summary>
        /// The current <see cref="ValueTuple{T1,T2}"/> instance's first component.
        /// </summary>
        public readonly T2 Item2;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1,T2}"/> value type.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        public ValueTuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1,T2}"/> instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified object; otherwise, <see langword="false"/>.</returns>
        ///
        /// <remarks>
        /// The <paramref name="obj"/> parameter is considered to be equal to the current instance under the following conditions:
        /// <list type="bullet">
        ///     <item><description>It is a <see cref="ValueTuple{T1,T2}"/> value type.</description></item>
        ///     <item><description>Its components are of the same types as those of the current instance.</description></item>
        ///     <item><description>Its components are equal to those of the current instance. Equality is determined by the default object equality comparer for each component.</description></item>
        /// </list>
        /// </remarks>
        public override bool Equals(object obj) => obj is ValueTuple<T1, T2> tuple && Equals(tuple);

        /// <summary>
        /// Returns a value that indicates whether the current <see cref="ValueTuple{T1,T2}"/> instance is equal to a specified <see cref="ValueTuple{T1,T2}"/>.
        /// </summary>
        /// <param name="other">The tuple to compare with this instance.</param>
        /// <returns><see langword="true"/> if the current instance is equal to the specified tuple; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The <paramref name="other"/> parameter is considered to be equal to the current instance if each of its fields
        /// are equal to that of the current instance, using the default comparer for that field's type.
        /// </remarks>
        public bool Equals(ValueTuple<T1, T2> other) =>
            EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
            && EqualityComparer<T2>.Default.Equals(Item2, other.Item2);

        int IComparable.CompareTo(object other)
        {
            return other switch
            {
                null => 1,
                not ValueTuple<T1, T2> => throw new ArgumentException(@"Incorrect type", nameof(other)),
                _ => CompareTo((ValueTuple<T1, T2>)other)
            };
        }

        /// <summary>Compares this instance to a specified instance and returns an indication of their relative values.</summary>
        /// <param name="other">An instance to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.
        /// Returns less than zero if this instance is less than <paramref name="other"/>, zero if this
        /// instance is equal to <paramref name="other"/>, and greater than zero if this instance is greater
        /// than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(ValueTuple<T1, T2> other)
        {
            var c = Comparer<T1>.Default.Compare(Item1, other.Item1);
            return c != 0 ? c : Comparer<T2>.Default.Compare(Item2, other.Item2);
        }

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
        {
            return other switch
            {
                ValueTuple<T1, T2>(var item1, var item2) => comparer.Equals(Item1, item1) && comparer.Equals(Item2, item2),
                _ => false
            };
        }

        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            switch (other)
            {
                case null:
                    return 1;
                case ValueTuple<T1, T2>(var item1, var item2):
                {
                    var c = comparer.Compare(Item1, item1);

                    return c != 0 ? c : comparer.Compare(Item2, item2);
                }
                default:
                    throw new ArgumentException(@"Incorrect type", nameof(other));
            }
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer) => GetHashCodeCore(comparer);

        /// <summary>
        /// Returns the hash code for the current <see cref="ValueTuple{T1,T2}"/> instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode() =>
            ValueTuple.CombineHashCodes(Item1?.GetHashCode() ?? 0,
                Item2?.GetHashCode() ?? 0);

        private int GetHashCodeCore(IEqualityComparer comparer) =>
            ValueTuple.CombineHashCodes(comparer.GetHashCode(Item1),
                comparer.GetHashCode(Item2));

        int IValueTupleInternal.GetHashCode(IEqualityComparer comparer) => GetHashCodeCore(comparer);

        /// <summary>
        /// Returns a string that represents the value of this <see cref="ValueTuple{T1,T2}"/> instance.
        /// </summary>
        /// <returns>The string representation of this <see cref="ValueTuple{T1,T2}"/> instance.</returns>
        /// <remarks>
        /// The string returned by this method takes the form <c>(Item1, Item2)</c>,
        /// where <c>Item1</c> and <c>Item2</c> represent the values of the <see cref="Item1"/>
        /// and <see cref="Item2"/> fields. If either field value is <see langword="null"/>,
        /// it is represented as <see cref="String.Empty"/>.
        /// </remarks>
        public override string ToString() => $@"({Item1}, {Item2})";

        string IValueTupleInternal.ToStringEnd() => $@"{Item1}, {Item2})";

        /// <summary>
        /// The number of positions in this data structure.
        /// </summary>
        int ITuple.Length => 2;

        /// <summary>
        /// Get the element at position <param name="index"/>.
        /// </summary>
        object ITuple.this[int index]
        {
            get
            {
                return index switch
                {
                    0 => Item1,
                    1 => Item2,
                    _ => throw new IndexOutOfRangeException()
                };
            }
        }
    }

	/// <summary>
	/// Represents a 4-tuple, or quadruple, as a value type.
	/// </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct ValueTuple<T1,T2,T3,T4> : IEquatable<ValueTuple<T1,T2,T3,T4>>,
		IStructuralEquatable, IStructuralComparable,
		IComparable, IComparable<ValueTuple<T1,T2,T3,T4>>, IValueTupleInternal
	{
		/// <summary>
		/// The current <see cref="ValueTuple{T1,T2,T3,T4}"/> instance's first component.
		/// </summary>
		public readonly T1 Item1;
		/// <summary>
		/// The current <see cref="ValueTuple{T1,T2,T3,T4}"/> instance's second component.
		/// </summary>
		public readonly T2 Item2;
		/// <summary>
		/// The current <see cref="ValueTuple{T1,T2,T3,T4}"/> instance's third component.
		/// </summary>
		public readonly T3 Item3;
		/// <summary>
		/// The current <see cref="ValueTuple{T1,T2,T3,T4}"/> instance's fourth component.
		/// </summary>
		public readonly T4 Item4;

		/// <summary>
		/// Initializes a new instance of the <see cref="ValueTuple{T1,T2,T3,T4}"/> value type.
		/// </summary>
		/// <param name="item1">The value of the tuple's first component.</param>
		/// <param name="item2">The value of the tuple's second component.</param>
		/// <param name="item3">The value of the tuple's third component.</param>
		/// <param name="item4">The value of the tuple's fourth component.</param>
		public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
		}

		/// <summary>
		/// Returns a value that indicates whether the current <see cref="ValueTuple{T1,T2,T3,T4}"/> instance is equal to a specified object.
		/// </summary>
		/// <param name="obj">The object to compare with this instance.</param>
		/// <returns><see langword="true"/> if the current instance is equal to the specified object; otherwise, <see langword="false"/>.</returns>
		/// <remarks>
		/// The <paramref name="obj"/> parameter is considered to be equal to the current instance under the following conditions:
		/// <list type="bullet">
		///     <item><description>It is a <see cref="ValueTuple{T1,T2,T3,T4}"/> value type.</description></item>
		///     <item><description>Its components are of the same types as those of the current instance.</description></item>
		///     <item><description>Its components are equal to those of the current instance. Equality is determined by the default object equality comparer for each component.</description></item>
		/// </list>
		/// </remarks>
		public override bool Equals(object obj)
		{
			return obj is ValueTuple<T1,T2,T3,T4> && Equals((ValueTuple<T1,T2,T3,T4>)obj);
		}

		/// <summary>
		/// Returns a value that indicates whether the current <see cref="ValueTuple{T1,T2,T3,T4}"/>
		/// instance is equal to a specified <see cref="ValueTuple{T1,T2,T3,T4}"/>.
		/// </summary>
		/// <param name="other">The tuple to compare with this instance.</param>
		/// <returns><see langword="true"/> if the current instance is equal to the specified tuple; otherwise, <see langword="false"/>.</returns>
		/// <remarks>
		/// The <paramref name="other"/> parameter is considered to be equal to the current instance if each of its fields
		/// are equal to that of the current instance, using the default comparer for that field's type.
		/// </remarks>
		public bool Equals(ValueTuple<T1,T2,T3,T4> other)
		{
			return
				EqualityComparer<T1>.Default.Equals(Item1, other.Item1) &&
				EqualityComparer<T2>.Default.Equals(Item2, other.Item2) &&
				EqualityComparer<T3>.Default.Equals(Item3, other.Item3) &&
				EqualityComparer<T4>.Default.Equals(Item4, other.Item4);
		}

		int IComparable.CompareTo(object other)
		{
			if (other == null) return 1;

			if (!(other is ValueTuple<T1,T2,T3,T4>))
				throw new ArgumentException();

			return CompareTo((ValueTuple<T1,T2,T3,T4>)other);
		}

		/// <summary>Compares this instance to a specified instance and returns an indication of their relative values.</summary>
		/// <param name="other">An instance to compare.</param>
		/// <returns>
		/// A signed number indicating the relative values of this instance and <paramref name="other"/>.
		/// Returns less than zero if this instance is less than <paramref name="other"/>, zero if this
		/// instance is equal to <paramref name="other"/>, and greater than zero if this instance is greater
		/// than <paramref name="other"/>.
		/// </returns>
		public int CompareTo(ValueTuple<T1,T2,T3,T4> other)
		{
			int c;

			c = Comparer<T1>.Default.Compare(Item1, other.Item1); if (c != 0) return c;
			c = Comparer<T2>.Default.Compare(Item2, other.Item2); if (c != 0) return c;
			c = Comparer<T3>.Default.Compare(Item3, other.Item3); if (c != 0) return c;

			return Comparer<T4>.Default.Compare(Item4, other.Item4);
		}

		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (!(other is ValueTuple<T1, T2, T3, T4>)) return false;

			var objTuple = (ValueTuple<T1,T2,T3,T4>)other;

			return
				comparer.Equals(Item1, objTuple.Item1) &&
				comparer.Equals(Item2, objTuple.Item2) &&
				comparer.Equals(Item3, objTuple.Item3) &&
				comparer.Equals(Item4, objTuple.Item4);
		}

		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null) return 1;

			if (!(other is ValueTuple<T1,T2,T3,T4>))
				throw new ArgumentException();

			var objTuple = (ValueTuple<T1,T2,T3,T4>)other;

			int c;

			c = comparer.Compare(Item1, objTuple.Item1); if (c != 0) return c;
			c = comparer.Compare(Item2, objTuple.Item2); if (c != 0) return c;
			c = comparer.Compare(Item3, objTuple.Item3); if (c != 0) return c;

			return comparer.Compare(Item4, objTuple.Item4);
		}

		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return GetHashCodeCore(comparer);
		}

		/// <summary>
		/// Returns the hash code for the current <see cref="ValueTuple{T1,T2,T3,T4}"/> instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return ValueTuple.CombineHashCodes(
				Item1?.GetHashCode() ?? 0,
				Item2?.GetHashCode() ?? 0,
				Item3?.GetHashCode() ?? 0,
				Item4?.GetHashCode() ?? 0);
		}

		int GetHashCodeCore(IEqualityComparer comparer)
		{
			return ValueTuple.CombineHashCodes(
				comparer.GetHashCode(Item1),
				comparer.GetHashCode(Item2),
				comparer.GetHashCode(Item3),
				comparer.GetHashCode(Item4));
		}

		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return GetHashCodeCore(comparer);
		}

		/// <summary>
		/// Returns a string that represents the value of this <see cref="ValueTuple{T1,T2,T3,T4}"/> instance.
		/// </summary>
		/// <returns>The string representation of this <see cref="ValueTuple{T1,T2,T3,T4}"/> instance.</returns>
		/// <remarks>
		/// The string returned by this method takes the form <c>(Item1, Item2, Item3, Item4)</c>.
		/// If any field value is <see langword="null"/>, it is represented as <see cref="String.Empty"/>.
		/// </remarks>
		public override string ToString()
		{
			return $"({Item1}, {Item2}, {Item3}, {Item4})";
		}

		string IValueTupleInternal.ToStringEnd()
		{
			return $"{Item1}, {Item2}, {Item3}, {Item4})";
		}

		/// <summary>
		/// The number of positions in this data structure.
		/// </summary>
		int ITuple.Length => 4;

		/// <summary>
		/// Get the element at position <param name="index"/>.
		/// </summary>
		object ITuple.this[int index]
		{
			get
			{
				switch (index)
				{
					case 0 : return Item1;
					case 1 : return Item2;
					case 2 : return Item3;
					case 3 : return Item4;
					default: throw new IndexOutOfRangeException();
				}
			}
		}
	}
}


#endif
