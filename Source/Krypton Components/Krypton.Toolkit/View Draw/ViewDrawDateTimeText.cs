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
/// Draw the date time picker text.
/// </summary>
public class ViewDrawDateTimeText : ViewLeaf
{
    #region Type Declarations
    private class FormatHandler
    {
        #region Instance Fields

        private int _activeFragment;
        private FormatFragmentList _fragments;
        private string? _inputDigits;
        private readonly KryptonDateTimePicker _dateTimePicker;
        private readonly NeedPaintHandler _needPaint;
        private readonly ViewDrawDateTimeText _timeText;
        #endregion

        #region Identity
        /// <summary>
        /// Initalize a new instance of the FormatHandler class.
        /// </summary>
        /// <param name="dateTimePicker">Reference to owning date time picker.</param>
        /// <param name="timeText">Reference to owning time text element.</param>
        /// <param name="needPaint">Delegate for invoking repainting.</param>
        public FormatHandler(KryptonDateTimePicker dateTimePicker,
            ViewDrawDateTimeText timeText,
            NeedPaintHandler needPaint)
        {
            _dateTimePicker = dateTimePicker;
            _timeText = timeText;
            _needPaint = needPaint;
            _fragments = [];
            _activeFragment = -1;
            _inputDigits = null;
            RightToLeftLayout = false;
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            StringBuilder ret = new StringBuilder();
            foreach (FormatFragment fmt in _fragments)
            {
                ret.Append(fmt.GetDisplay(DateTime));
            }

            return ret.ToString();
        }
        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the need to show focus.
        /// </summary>
        public bool HasFocus { get; set; }

        /// <summary>
        /// Gets and sets the right to left layout of text.
        /// </summary>
        public bool RightToLeftLayout { get; set; }

        /// <summary>
        /// Gets a value indicating if there is an active char fragment.
        /// </summary>
        public bool HasActiveFragment => _activeFragment >= 0;

        /// <summary>
        /// Gets and sets the active fragment based on the fragment string.
        /// </summary>
        public string ActiveFragment
        {
            get => !HasActiveFragment ? string.Empty : _fragments[_activeFragment].FragFormat;

            set
            {
                _activeFragment = -1;

                for (var i = 0; i < _fragments.Count; i++)
                {
                    if (_fragments[i].AllowActive)
                    {
                        if (_fragments[i].FragFormat.Equals(value))
                        {
                            _activeFragment = i;
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Clear the active fragment.
        /// </summary>
        public void ClearActiveFragment() => _activeFragment = -1;

        /// <summary>
        /// Gets and sets the date time currently used by the handler.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Moves to the first char fragment.
        /// </summary>
        public void MoveFirst()
        {
            if (ImplRightToLeft)
            {
                _activeFragment = -1;

                for (var i = 0; i < _fragments.Count; i++)
                {
                    if (_fragments[i].AllowActive)
                    {
                        _activeFragment = i;
                    }
                }
            }
            else
            {
                _activeFragment = -1;

                for (var i = 0; i < _fragments.Count; i++)
                {
                    if (_fragments[i].AllowActive)
                    {
                        _activeFragment = i;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Moves to the last char fragment.
        /// </summary>
        public void MoveLast()
        {
            if (ImplRightToLeft)
            {
                _activeFragment = -1;

                for (var i = 0; i < _fragments.Count; i++)
                {
                    if (_fragments[i].AllowActive)
                    {
                        _activeFragment = i;
                        return;
                    }
                }
            }
            else
            {
                _activeFragment = -1;

                for (var i = 0; i < _fragments.Count; i++)
                {
                    if (_fragments[i].AllowActive)
                    {
                        _activeFragment = i;
                    }
                }
            }
        }

        /// <summary>
        /// Moves left one char fragment.
        /// </summary>
        public void MoveLeft()
        {
            if (ImplRightToLeft)
            {
                for (var i = _activeFragment + 1; i < _fragments.Count; i++)
                {
                    if (_fragments[i].AllowActive)
                    {
                        _activeFragment = i;
                        return;
                    }
                }

                _activeFragment = -1;
            }
            else
            {
                for (var i = _activeFragment - 1; i >= 0; i--)
                {
                    if (_fragments[i].AllowActive)
                    {
                        _activeFragment = i;
                        return;
                    }
                }

                _activeFragment = -1;
            }
        }

        /// <summary>
        /// Move to the right one char fragment.
        /// </summary>
        public void MoveRight()
        {
            if (ImplRightToLeft)
            {
                for (var i = _activeFragment - 1; i >= 0; i--)
                {
                    if (_fragments[i].AllowActive)
                    {
                        _activeFragment = i;
                        return;
                    }
                }

                _activeFragment = -1;
            }
            else
            {
                for (var i = _activeFragment + 1; i < _fragments.Count; i++)
                {
                    if (_fragments[i].AllowActive)
                    {
                        _activeFragment = i;
                        return;
                    }
                }

                _activeFragment = -1;
            }
        }

        /// <summary>
        /// Move to the next fragment.
        /// </summary>
        public void MoveNext()
        {
            MoveRight();

            // Rotate around the end of the fragments
            if (!HasActiveFragment)
            {
                MoveFirst();
            }
        }

        /// <summary>
        /// Move to the next fragment.
        /// </summary>
        public void MovePrevious()
        {
            MoveLeft();

            // Rotate around the start of the fragments
            if (!HasActiveFragment)
            {
                MoveLast();
            }
        }

        /// <summary>
        /// Select the nearest fragment to the mouse point.
        /// </summary>
        /// <param name="pt">Mouse point.</param>
        public void SelectFragment(Point pt)
        {
            if (ImplRightToLeft)
            {
                var totalWidth = 0;
                for (var i = _fragments.Count - 1; i >= 0; i--)
                {
                    totalWidth += i == 0 ? _fragments[i].TotalWidth : _fragments[i].TotalWidth - _fragments[i - 1].TotalWidth;
                    if (_fragments[i].AllowActive)
                    {

                        // If the point is after the current fragment, make it the active fragment
                        if (pt.X > (_timeText.ClientRectangle.Right - totalWidth))
                        {
                            EndInputDigits();
                            _activeFragment = i;
                            return;
                        }
                    }
                }

                MoveLast();
            }
            else
            {
                // Adjust point for the location of the text
                pt.X -= _timeText.ClientLocation.X;

                for (var i = 0; i < _fragments.Count; i++)
                {
                    if (_fragments[i].AllowActive)
                    {
                        // If the point is before the current fragment, make it the active fragment
                        if (pt.X < _fragments[i].TotalWidth)
                        {
                            EndInputDigits();
                            _activeFragment = i;
                            return;
                        }
                    }
                }

                MoveLast();
            }
        }

        /// <summary>
        /// Increment the current fragment value.
        /// </summary>
        /// <param name="forward">Forward to add; otherwise subtract.</param>
        /// <returns>Modified date/time.</returns>
        public DateTime Increment(bool forward) =>
            // Pass request onto the fragment itself
            _activeFragment >= 0 ? _fragments[_activeFragment].Increment(DateTime, forward) : DateTime;

        /// <summary>
        /// Invert the AM/PM indicator for the date.
        /// </summary>
        /// <param name="am">Am requested.</param>
        /// <returns>Modified date/time.</returns>
        public DateTime AMPM(bool am) =>
            // Pass request onto the fragment itself
            _activeFragment >= 0 ? _fragments[_activeFragment].AMPM(DateTime, am) : DateTime;

        /// <summary>
        /// Gets a value indicating if input digits are being processed.
        /// </summary>
        public bool IsInputDigits => _inputDigits != null;

        /// <summary>
        /// Process the input of numeric digit.
        /// </summary>
        /// <param name="digit">Input digit.</param>
        public void InputDigit(char digit)
        {
            // We clear the cache if the active fragment says no digits are allowed
            if ((_activeFragment == -1) || (_fragments[_activeFragment].InputDigits == 0))
            {
                _inputDigits = null;
            }
            else
            {
                _inputDigits ??= "";

                // Append the latest digit
                _inputDigits += digit;

                // We need to special case the digit entry for descriptive months
                if (_fragments[_activeFragment].FragFormat.Contains("MMM"))
                {
                    // Get the actual month number entered
                    var monthNumber = int.Parse(_inputDigits);

                    switch (monthNumber)
                    {
                        // If two digits is not valid then use just the last digit
                        case > 12:
                            monthNumber -= 10;
                            break;
                        case 0:
                            monthNumber = 10;
                            break;
                    }

                    // Set the new date using the month number
                    DateTime dt = DateTime.AddMonths(monthNumber - DateTime.Month);
                    if (!dt.Equals(DateTime))
                    {
                        _dateTimePicker.Value = _timeText.ValidateDate(dt);
                        _needPaint(this, new NeedLayoutEventArgs(true));
                    }

                    // Keep the digit if there is just a single '1' or '0' digit that might be start of two digit month
                    if ((_inputDigits.Length > 1) || ((_inputDigits.Length == 1) && monthNumber is > 1 and < 10))
                    {
                        // Do we need to shift to the next field?
                        if ((_inputDigits.Length == 2) && _dateTimePicker.AutoShift)
                        {
                            MoveRight();

                            // Rotate around the end of the fragments
                            if (!HasActiveFragment)
                            {
                                // Use evnet to show that we are overflowing
                                CancelEventArgs cea = new CancelEventArgs();
                                _timeText.OnAutoShiftOverflow(cea);

                                // Event might be cancelled so check we want to overflow
                                if (!cea.Cancel)
                                {
                                    if (_dateTimePicker.ShowCheckBox)
                                    {
                                        _dateTimePicker.InternalViewDrawCheckBox.ForcedTracking = true;
                                    }
                                    else
                                    {
                                        MoveFirst();
                                    }
                                }
                            }
                        }

                        _inputDigits = null;
                    }
                }
                else
                {
                    // If we hit the maximum number of valid digits
                    if (_inputDigits.Length == _fragments[_activeFragment].InputDigits)
                    {
                        // Ask the fragment to process the digits
                        DateTime dt = _fragments[_activeFragment].EndDigits(DateTime, _inputDigits);
                        if (!dt.Equals(DateTime))
                        {
                            _dateTimePicker.Value = _timeText.ValidateDate(dt);
                            _needPaint(this, new NeedLayoutEventArgs(true));
                        }

                        _inputDigits = null;


                        // Do we need to shift to the next field?
                        if (_dateTimePicker.AutoShift)
                        {
                            MoveRight();

                            // Rotate around the end of the fragments
                            if (!HasActiveFragment)
                            {
                                // Use evnet to show that we are overflowing
                                CancelEventArgs cea = new CancelEventArgs();
                                _timeText.OnAutoShiftOverflow(cea);

                                // Event might be cancelled so check we want to overflow
                                if (!cea.Cancel)
                                {
                                    if (_dateTimePicker.ShowCheckBox)
                                    {
                                        _dateTimePicker.InternalViewDrawCheckBox.ForcedTracking = true;
                                    }
                                    else
                                    {
                                        MoveFirst();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Process the end of inputting digits.
        /// </summary>
        public void EndInputDigits()
        {
            // Do we have input waiting to be processed and a matching active fragment
            if ((_inputDigits != null) && (_activeFragment >= 0))
            {
                DateTime dt = _fragments[_activeFragment].EndDigits(DateTime, _inputDigits);
                if (!dt.Equals(DateTime))
                {
                    _dateTimePicker.Value = _timeText.ValidateDate(dt);
                    _needPaint(this, new NeedLayoutEventArgs(true));
                }
                _inputDigits = null;
            }
        }

        /// <summary>
        /// Parse a new format into fragments.
        /// </summary>
        /// <param name="format">Format string to parse.</param>
        /// <param name="g">Graphics instance used to measure text.</param>
        /// <param name="font">Font used to measure text.</param>
        public void ParseFormat(string format, Graphics? g, Font? font)
        {
            // Split the format into a set of fragments
            _fragments = ParseFormatToFragments(format);

            // If there anything to measure?
            if (_fragments.Count > 0)
            {
                // Measure the pixel width of each fragment
                MeasureFragments(g, font, DateTime);
            }

            // If we have an active fragment make sure it is still valid
            ValidateActiveFragment();
        }

        /// <summary>
        /// Render the text.
        /// </summary>
        /// <param name="context">Render context.</param>
        /// <param name="font">Text font.</param>
        /// <param name="rect">Client rectangle area.</param>
        /// <param name="textColor">Text color.</param>
        /// <param name="backColor">Back color.</param>
        /// <param name="enabled">If text enabled.</param>
        public void Render(RenderContext context, Font? font, Rectangle rect, 
            Color textColor, Color backColor, bool enabled)
        {
            if (enabled || string.IsNullOrEmpty(_dateTimePicker.CustomNullText))
            {
                // If there anything to draw?
                if (_fragments.Count > 0)
                {
                    if (ImplRightToLeft)
                    {
                        // Right align by updating the drawing rectangle
                        var textWidth = _fragments[_fragments.Count - 1].TotalWidth;
                        rect.X = rect.Right - textWidth - 1;
                        rect.Width = textWidth;
                    }

                    var lastTotalWidth = 0;
                    for (var i = 0; i < _fragments.Count; i++)
                    {
                        Color foreColor = textColor;
                        var totalWidth = _fragments[i].TotalWidth;
                        if (totalWidth > rect.Width)
                        {
                            totalWidth = rect.Width;
                        }

                        Rectangle drawText = rect with { X = rect.X + lastTotalWidth, Width = totalWidth - lastTotalWidth };
                        if (drawText.Width > 0)
                        {
                            // If we need to draw a focus indication?
                            if (HasFocus)
                            {
                                // Do we have an active fragment?
                                if (_activeFragment == i)
                                {
                                    // Draw background in the highlight color
                                    if (enabled)
                                    {
                                        context.Graphics.FillRectangle(SystemBrushes.Highlight, drawText with { X = drawText.X - 1, Width = drawText.Width + 2 });
                                        foreColor = SystemColors.HighlightText;
                                    }
                                    else
                                    {
                                        using (SolidBrush fillBrush = new SolidBrush(foreColor))
                                        {
                                            context.Graphics.FillRectangle(fillBrush, drawText);
                                        }

                                        foreColor = backColor;
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(_inputDigits) &&
                                (_activeFragment == i) &&
                                !_fragments[_activeFragment].FragFormat.Contains("MMM"))
                            {
                                // Draw input digits for this fragment
                                TextRenderer.DrawText(context.Graphics, _inputDigits, font, drawText, foreColor, DRAW_LEFT_FLAGS);
                            }
                            else
                            {
                                // Draw text for this fragment only
                                TextRenderer.DrawText(context.Graphics, _fragments[i].GetDisplay(DateTime), font, drawText, foreColor, DRAW_LEFT_FLAGS);
                            }

                            lastTotalWidth = totalWidth;
                        }
                    }
                }
            }
            else
            {
                // Draw input digits for this fragment
                TextRenderer.DrawText(context.Graphics, _dateTimePicker.CustomNullText, font, rect, textColor, DRAW_LEFT_FLAGS);
            }
        }
        #endregion

        #region Private
        private void ValidateActiveFragment()
        {
            var firstFocus = -1;
            for (var i = 0; i < _fragments.Count; i++)
            {
                if (_fragments[i].AllowActive)
                {
                    if (firstFocus == -1)
                    {
                        firstFocus = i;
                        break;
                    }
                }
            }

            if (_activeFragment >= 0)
            {
                if (_activeFragment >= _fragments.Count)
                {
                    EndInputDigits();
                    _activeFragment = firstFocus;
                }
                else if (!_fragments[_activeFragment].AllowActive)
                {
                    EndInputDigits();
                    _activeFragment = firstFocus;
                }
            }
        }

        private bool ImplRightToLeft => RightToLeftLayout && (_dateTimePicker.RightToLeft == RightToLeft.Yes);

        private void MeasureFragments(Graphics? g, Font? font, DateTime dt)
        {
            // Create a character range/character region for each of the fragments
            var charRanges = new CharacterRange[_fragments.Count];

            // Generate the output for each fragment and measure the length of that fragment output
            for (var i = 0; i < _fragments.Count; i++)
            {
                charRanges[i] = new CharacterRange(0, _fragments[i].GenerateOutput(dt).Length);
            }

            // Update format with details of the ranges to measure
            StringFormat measureFormat = new StringFormat(StringFormatFlags.FitBlackBox);
            measureFormat.SetMeasurableCharacterRanges(charRanges);

            // Perform measuring using the output of the last fragment (last frag must be the whole output string)
            var charRegion = g?.MeasureCharacterRanges(_fragments[_fragments.Count - 1].Output, font!, _measureRect, measureFormat);

            // Push return values into the individual fragment entries
            for (var i = 0; i < _fragments.Count; i++)
            {
                _fragments[i].TotalWidth = (int)Math.Ceiling(charRegion![i].GetBounds(g!).Width);
            }
        }

        private FormatFragmentList ParseFormatToFragments(string format)
        {
            FormatFragmentList fragList = [];

            // Grab the string used for formatting
            var length = format.Length;
            var current = 0;
            var literal = 0;

            // Use state machine to parse the format one character at a time
            while (current < length)
            {
                // Processing depends on the character
                switch (format[current])
                {
                    case 'h':
                    case 'H':
                    case 'm':
                    case 's':
                    case 't':
                    case 'd':
                    case 'y':
                        ParseCharacter(format[current], fragList, ref literal, ref current, ref format);
                        break;
                    case 'f':
                        ParseCharacter('f', 7, fragList, ref literal, ref current, ref format);
                        break;
                    case 'F':
                        ParseCharacter('F', 7, fragList, ref literal, ref current, ref format);
                        break;
                    case 'M':
                        ParseCharacter('M', 4, fragList, ref literal, ref current, ref format);
                        break;
                    // ' = everything inside the single quotes is a literal
                    case '\'':
                        do
                        {
                            current++;
                            literal++;
                        }
                        while ((current < length) && (format[current] != '\''));

                        if ((current < length) && (format[current] == '\''))
                        {
                            current++;
                            literal++;
                        }
                        break;
                    // Prefix before a single format character
                    case '%':
                        current++;
                        break;
                    // All other characters are literals
                    default:
                        current++;
                        literal++;
                        break;
                }
            }

            // Add any trailing literal
            if (literal > 0)
            {
                fragList.Add(new FormatFragment(current, format, format.Substring(current - literal, literal)));
            }

            return fragList;
        }

        private void ParseCharacter(char charater, FormatFragmentList fragList,
            ref int literal, ref int current, ref string format) =>
            ParseCharacter(charater, int.MaxValue, fragList, ref literal, ref current, ref format);

        private void ParseCharacter(char charater, int max, FormatFragmentList fragList,
            ref int literal, ref int current, ref string format)
        {
            if (literal > 0)
            {
                fragList.Add(new FormatFragment(current, format, format.Substring(current - literal, literal)));
            }

            var count = CountUptoMaxCharacters(charater, max, ref current, ref format);
            fragList.Add(new FormatFragmentChar(current, format, charater, count));
            literal = 0;
        }

        private int CountUptoMaxCharacters(char character, int max, ref int current, ref string format)
        {
            var count = 0;
            var length = format.Length;

            // Keep consuming until we run out of characters
            while ((current < length) && (count < max))
            {
                // Only interested in the specified characters
                if (format[current] == character)
                {
                    count++;
                    current++;
                }
                else
                {
                    break;
                }
            }

            return count;
        }
        #endregion
    }

    private class FormatFragment
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the FormatFragment class.
        /// </summary>
        /// <param name="length">Length of the format string to extract.</param>
        /// <param name="format">Source string to extra fragment from.</param>
        /// <param name="literal">String literal.</param>
        public FormatFragment(int length, string format, string literal)
        {
            FragFormat = literal;

            Fragment = length == 0 ? string.Empty : format.Substring(0, length);
        }

        /// <summary>
        /// Output a text representation of the fragment.
        /// </summary>
        /// <returns>String instance.</returns>
        public override string ToString() => Fragment;

        #endregion

        #region Public
        /// <summary>
        /// Gets access to the fragment string.
        /// </summary>
        public string Fragment { get; }

        /// <summary>
        /// Gets access to the fragment format string.
        /// </summary>
        public virtual string FragFormat { get; }

        /// <summary>
        /// Gets access to the generate output.
        /// </summary>
        public string Output { get; private set; }

        /// <summary>
        /// Gets and sets the total pixel width of this fragments output.
        /// </summary>
        public int TotalWidth { set; get; }

        /// <summary>
        /// Generate the output string from the provided date and the format fragment.
        /// </summary>
        /// <param name="dt">DateTime used to generate output.</param>
        /// <returns>Generated output string.</returns>
        public string GenerateOutput(DateTime dt)
        {
            // Use helper to ensure single character formats are handled correctly
            Output = dt.ToString(CommonHelper.MakeCustomDateFormat(Fragment));
            return Output;
        }

        /// <summary>
        /// Can this field be edited and active.
        /// </summary>
        public virtual bool AllowActive => false;

        /// <summary>
        /// Gets the number of digits allowed to be entered for this fragment.
        /// </summary>
        public virtual int InputDigits => 0;

        /// <summary>
        /// Process the input digits to modify the incoming date time.
        /// </summary>
        /// <param name="dt">Date time to modify.</param>
        /// <param name="digits">Set of digits to process.</param>
        /// <returns>Modified date time.</returns>
        public virtual DateTime EndDigits(DateTime dt, string digits) => dt;

        /// <summary>
        /// Gets the display string for display using the provided date time.
        /// </summary>
        /// <param name="dt">DateTime to format.</param>
        /// <returns>Display string.</returns>
        public virtual string GetDisplay(DateTime dt) => FragFormat.Length == 1 ? dt.ToString($"\\{FragFormat}") : dt.ToString(FragFormat);

        /// <summary>
        /// Increment the current fragment value.
        /// </summary>
        /// <param name="dt">DateTime to be modified.</param>
        /// <param name="forward">Forward to add; otherwise subtract.</param>
        /// <returns>Modified date/time.</returns>
        public virtual DateTime Increment(DateTime dt, bool forward)
        {
            // Cannot change a literal, so do nothing
            Debug.Assert(false);
            return dt;
        }

        /// <summary>
        /// Invert the AM/PM indicator for the date.
        /// </summary>
        /// <param name="dt">DateTime to be modified.</param>
        /// <param name="am">Am requested.</param>
        /// <returns>Modified date/time.</returns>
        public virtual DateTime AMPM(DateTime dt, bool am)
        {
            // Cannot change a literal, so do nothing
            Debug.Assert(false);
            return dt;
        }
        #endregion
    }

    private class FormatFragmentChar : FormatFragment
    {
        #region Instance Fields
        private readonly string _fragFormat;
        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the FormatFragmentChar class.
        /// </summary>
        /// <param name="index">Index after the string we want.</param>
        /// <param name="format">Source string to extra fragment from.</param>
        /// <param name="character">Character that represents the format fragment.</param>
        /// <param name="count">Number characters in the fragment.</param>
        public FormatFragmentChar(int index, string format, char character, int count)
            : base(index, format, string.Empty) =>
            _fragFormat = new string(character, count);

        /// <summary>
        /// Output a text representation of the fragment.
        /// </summary>
        /// <returns>String instance.</returns>
        public override string ToString() => $"{base.ToString()} ({_fragFormat})";

        #endregion

        #region Public
        /// <summary>
        /// Gets access to the fragment format string.
        /// </summary>
        public override string FragFormat => _fragFormat;

        /// <summary>
        /// Can this field be edited and active.
        /// </summary>
        public override bool AllowActive
        {
            get 
            {
                // Only certain formats can be edited
                return FragFormat switch
                {
                    "d" or "dd" or "M" or "MM" or "MMM" or "MMMM" => true,
                    _ => FragFormat.StartsWith("h") ||
                         FragFormat.StartsWith("H") ||
                         FragFormat.StartsWith("m") ||
                         FragFormat.StartsWith("s") ||
                         FragFormat.StartsWith("t") ||
                         FragFormat.StartsWith("f") ||
                         FragFormat.StartsWith("F") ||
                         FragFormat.StartsWith("y")
                };
            }
        }

        /// <summary>
        /// Gets the number of digits allowed to be entered for this fragment.
        /// </summary>
        public override int InputDigits
        {
            get
            {
                // Only certain formats can be edited
                switch (FragFormat)
                {
                    case @"f":
                    case @"F":
                        return 1;
                    case @"d":
                    case @"dd":
                    case @"M":
                    case @"MM":
                    case @"MMM":
                    case @"MMMM":
                    case @"ff":
                    case @"FF":
                        return 2;
                    case @"fff":
                    case @"FFF":
                        return 3;
                    case @"ffff":
                    case @"FFFF":
                        return 4;
                    case @"fffff":
                    case @"FFFFF":
                        return 5;
                    case @"ffffff":
                    case @"FFFFFF":
                        return 6;
                    case @"fffffff":
                    case @"FFFFFFF":
                        return 7;
                }

                if (FragFormat.StartsWith("h") ||
                    FragFormat.StartsWith("H") ||
                    FragFormat.StartsWith("m") ||
                    FragFormat.StartsWith("s"))
                {
                    return 2;
                }

                return FragFormat.StartsWith("y") ? 4 : base.InputDigits;
            }
        }

        /// <summary>
        /// Process the input digits to modify the incoming date time.
        /// </summary>
        /// <param name="dt">Date time to modify.</param>
        /// <param name="digits">Set of digits to process.</param>
        /// <returns>Modified date time.</returns>
        public override DateTime EndDigits(DateTime dt, string digits)
        {
            switch (FragFormat)
            {
                case @"d":
                case @"dd":
                    var dayNumber = int.Parse(digits);
                    if ((dayNumber <= LastDayOfMonth(dt).Day) && (dayNumber > 0))
                    {
                        dt = dt.AddDays(dayNumber - dt.Day);
                    }

                    break;
                case @"M":
                case @"MM":
                    var monthNumber = int.Parse(digits);
                    if (monthNumber is <= 12 and > 0)
                    {
                        dt = dt.AddMonths(monthNumber - dt.Month);
                    }

                    break;
                case @"f":
                case @"F":
                case @"ff":
                case @"FF":
                case @"fff":
                case @"FFF":
                case @"ffff":
                case @"FFFF":
                case @"fffff":
                case @"FFFFF":
                case @"ffffff":
                case @"FFFFFF":
                case @"fffffff":
                case @"FFFFFFF":
                    dt = dt.AddMilliseconds(int.Parse(digits) - dt.Millisecond);
                    break;
            }

            if (FragFormat.StartsWith("h") || FragFormat.StartsWith("H"))
            {
                var hoursNumber = int.Parse(digits);
                if (hoursNumber is < 24 and >= 0)
                {
                    dt = dt.AddHours(hoursNumber - dt.Hour);
                }
            } 
            else if (FragFormat.StartsWith("m"))
            {
                var minutesNumber = int.Parse(digits);
                if (minutesNumber is < 60 and >= 0)
                {
                    dt = dt.AddMinutes(minutesNumber - dt.Minute);
                }
            }
            else if (FragFormat.StartsWith("s"))
            {
                var secondsNumber = int.Parse(digits);
                if (secondsNumber is < 60 and >= 0)
                {
                    dt = dt.AddSeconds(secondsNumber - dt.Second);
                }
            }
            else if (FragFormat.StartsWith("y"))
            {
                var yearNumber = int.Parse(digits);

                // A zero year makes to change to the date
                if (yearNumber != 0)
                {
                    switch (digits.Length)
                    {
                        case 2:
                        {
                            // Two digits causes the century/millenium to be auto added
                            if (yearNumber >= 30)
                            {
                                yearNumber += 1900;
                            }
                            else
                            {
                                yearNumber += 2000;
                            }

                            dt = dt.AddYears(yearNumber - dt.Year);
                            break;
                        }
                        case 4:
                            // Four digits causes us to attempt to use that date
                            dt = dt.AddYears(yearNumber - dt.Year);
                            break;
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// Gets the display string for display using the provided date time.
        /// </summary>
        /// <param name="dt">DateTime to format.</param>
        /// <returns>Display string.</returns>
        public override string GetDisplay(DateTime dt) => dt.ToString(CommonHelper.MakeCustomDateFormat(FragFormat));

        /// <summary>
        /// Increment the current fragment value.
        /// </summary>
        /// <param name="dt">DateTime to be modified.</param>
        /// <param name="forward">Forward to add; otherwise subtract.</param>
        /// <returns>Modified date/time.</returns>
        public override DateTime Increment(DateTime dt, bool forward)
        {
            // Action depends on the format string
            switch (FragFormat)
            {
                case @"d":
                case @"dd":
                    if (forward)
                    {
                        dt = dt.Day == LastDayOfMonth(dt).Day ? dt.AddDays(-(dt.Day - 1)) : dt.AddDays(1);
                    }
                    else
                    {
                        dt = dt.Day == 1 ? dt.AddDays(LastDayOfMonth(dt).Day - 1) : dt.AddDays(-1);
                    }
                    break;
                case @"M":
                case @"MM":
                case @"MMM":
                case @"MMMM":
                    if (forward)
                    {
                        dt = dt.Month == 12 ? dt.AddMonths(-(dt.Month - 1)) : dt.AddMonths(1);
                    }
                    else
                    {
                        dt = dt.Month == 1 ? dt.AddMonths(11) : dt.AddMonths(-1);
                    } 
                    break;
            }

            // Any number of 'h' or 'H' are allowed
            if (FragFormat.StartsWith("h") || FragFormat.StartsWith("H"))
            {
                if (forward)
                {
                    if (dt.Hour == 23)
                    {
                        dt = dt.AddHours(1);
                        dt = dt.AddDays(-1);
                    }
                    else
                    {
                        dt = dt.AddHours(1);
                    }
                }
                else
                {
                    if (dt.Hour == 1)
                    {
                        dt = dt.AddHours(-1);
                        dt = dt.AddDays(1);
                    }
                    else
                    {
                        dt = dt.AddHours(-1);
                    }
                }
            }

            // Any number of 'm'
            if (FragFormat.StartsWith("m"))
            {
                if (forward)
                {
                    dt = dt.Minute == 59 ? dt.AddMinutes(-dt.Minute) : dt.AddMinutes(1);
                }
                else
                {
                    dt = dt.Minute == 0 ? dt.AddMinutes(59 - dt.Minute) : dt.AddMinutes(-1);
                }
            }

            // Any number of 's'
            if (FragFormat.StartsWith("s"))
            {
                if (forward)
                {
                    dt = dt.Second == 59 ? dt.AddSeconds(-dt.Second) : dt.AddSeconds(1);
                }
                else
                {
                    dt = dt.Second == 0 ? dt.AddSeconds(59 - dt.Second) : dt.AddSeconds(-1);
                }
            }

            // Any number of 't'
            if (FragFormat.StartsWith("t"))
            {
                dt = dt.Hour > 11 ? dt.AddHours(-12) : dt.AddHours(12);
            }

            // Any number of 'y'
            if (FragFormat.StartsWith("y"))
            {
                dt = forward ? dt.AddYears(1) : dt.AddYears(-1);
            }

            // Any number of 'f' or 'F'
            if (FragFormat.StartsWith("f") || FragFormat.StartsWith("F"))
            {
                // We increment by the last digit (upto a maximum of 3 digits)
                var digits = Math.Min(FragFormat.Length, 3);

                // Convert to correct increment size
                double increment = 1000;
                for (var i = 0; i < digits; i++)
                {
                    increment /= 10;
                }

                dt = forward ? dt.AddMilliseconds(increment) : dt.AddMilliseconds(-increment);
            }

            return dt;
        }

        /// <summary>
        /// Invert the AM/PM indicator for the date.
        /// </summary>
        /// <param name="dt">DateTime to be modified.</param>
        /// <param name="am">Am requested.</param>
        /// <returns>Modified date/time.</returns>
        public override DateTime AMPM(DateTime dt, bool am)
        {
            if (FragFormat.StartsWith("t"))
            {
                switch (dt.Hour)
                {
                    case > 11 when am:
                        dt = dt.AddHours(-12);
                        break;
                    case < 12 when !am:
                        dt = dt.AddHours(12);
                        break;
                }
            }

            return dt;
        }
        #endregion

        #region Implementation
        private DateTime LastDayOfMonth(DateTime dt)
        {
            dt = dt.AddMonths(1);
            dt = dt.AddDays(-dt.Day);
            return new DateTime(dt.Year, dt.Month, dt.Day);
        }
        #endregion
    }

    private class FormatFragmentList : List<FormatFragment>;
    #endregion

    #region Static Fields
    private static readonly RectangleF _measureRect = new RectangleF(0, 0, 1000, 1000);
    private const TextFormatFlags MEASURE_FLAGS = TextFormatFlags.TextBoxControl | TextFormatFlags.NoPadding | TextFormatFlags.VerticalCenter;
    private const TextFormatFlags DRAW_LEFT_FLAGS = TextFormatFlags.TextBoxControl | TextFormatFlags.NoPadding | TextFormatFlags.VerticalCenter;

    #endregion

    #region Instance Fields
    private readonly KryptonDateTimePicker _dateTimePicker;
    private readonly FormatHandler _formatHandler;
    private readonly NeedPaintHandler _needPaint;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the ViewDrawDateTimeText class.
    /// </summary>
    /// <param name="dateTimePicker">Color to fill drawing area.</param>
    /// <param name="needPaint">Delegate to allow repainting.</param>
    public ViewDrawDateTimeText(KryptonDateTimePicker dateTimePicker,
        NeedPaintHandler needPaint)
    {
        _dateTimePicker= dateTimePicker;
        _needPaint = needPaint;
        _formatHandler = new FormatHandler(dateTimePicker, this, needPaint);
    }

    /// <summary>
    /// Obtains the String representation of this instance.
    /// </summary>
    /// <returns>User readable name of the instance.</returns>
    public override string ToString()
    {
        // Return the display string.
        _formatHandler.DateTime = _dateTimePicker.Value;
        return _formatHandler.ToString();
    }
    #endregion

    #region RightToLeftLayout
    /// <summary>
    /// Gets and sets the right to left layout of text.
    /// </summary>
    public bool RightToLeftLayout
    {
        get => _formatHandler.RightToLeftLayout;
        set => _formatHandler.RightToLeftLayout = value;
    }
    #endregion

    #region AutoShiftOverflow
    /// <summary>
    /// Raises the AutoShiftOverflow event.
    /// </summary>
    /// <param name="e">An CancelEventArgs the contains the event data.</param>
    public void OnAutoShiftOverflow(CancelEventArgs e) => _dateTimePicker.OnAutoShiftOverflow(e);

    #endregion

    #region HasFocus
    /// <summary>
    /// Gets and sets the need to show focus.
    /// </summary>
    public bool HasFocus
    {
        get => _formatHandler.HasFocus;
        set => _formatHandler.HasFocus = value;
    }
    #endregion

    #region HasActiveFragment
    /// <summary>
    /// Gets a value indicating if there is an active char fragment.
    /// </summary>
    public bool HasActiveFragment => _formatHandler.HasActiveFragment;

    #endregion

    #region ClearActiveFragment
    /// <summary>
    /// Remove active fragment.
    /// </summary>
    public void ClearActiveFragment()
    {
        if (_formatHandler.IsInputDigits)
        {
            _formatHandler.EndInputDigits();
        }

        _formatHandler.ClearActiveFragment();
    }
    #endregion

    #region EndInputDigits
    /// <summary>
    /// End the input of input digits.
    /// </summary>
    public void EndInputDigits()
    {
        if (_formatHandler.IsInputDigits)
        {
            _formatHandler.EndInputDigits();
        }
    }
    #endregion

    #region MoveFirstFragment
    /// <summary>
    /// Make the first fragment the active fragment.
    /// </summary>
    public void MoveFirstFragment()
    {
        if (_formatHandler.IsInputDigits)
        {
            _formatHandler.EndInputDigits();
        }

        _formatHandler.MoveFirst();
    }
    #endregion

    #region ActiveFragment
    /// <summary>
    /// Gets and sets the active fragment based on the fragment string.
    /// </summary>
    public string ActiveFragment
    {
        get => _formatHandler.ActiveFragment;
        set => _formatHandler.ActiveFragment = value;
    }
    #endregion

    #region MoveNextFragment
    /// <summary>
    /// Make the next fragment the active fragment.
    /// </summary>
    public void MoveNextFragment()
    {
        if (_formatHandler.IsInputDigits)
        {
            _formatHandler.EndInputDigits();
        }

        _formatHandler.MoveNext();
    }
    #endregion

    #region MovePreviousFragment
    /// <summary>
    /// Make the previous fragment the active fragment.
    /// </summary>
    public void MovePreviousFragment()
    {
        if (_formatHandler.IsInputDigits)
        {
            _formatHandler.EndInputDigits();
        }

        _formatHandler.MovePrevious();
    }
    #endregion

    #region MoveLastFragment
    /// <summary>
    /// Make the last fragment the active fragment.
    /// </summary>
    public void MoveLastFragment()
    {
        if (_formatHandler.IsInputDigits)
        {
            _formatHandler.EndInputDigits();
        }

        _formatHandler.MoveLast();
    }
    #endregion

    #region SelectFragment
    /// <summary>
    /// Select the fragment that is nearest the provided point.
    /// </summary>
    /// <param name="pt">Mouse position relative to control.</param>
    /// <param name="button">Mouse button pressed down.</param>
    /// <returns>True if capturing input; otherwise false.</returns>
    public void SelectFragment(Point pt, MouseButtons button)
    {
        // Tell the format handler to select the nearest fragment
        _formatHandler.SelectFragment(pt);
        PerformNeedPaint(true);
    }
    #endregion

    #region PerformKeyDown
    /// <summary>
    /// Key has been pressed down.
    /// </summary>
    /// <param name="e">A KeyEventArgs that contains the event data.</param>
    public void PerformKeyDown(KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Left:
                if (_formatHandler.IsInputDigits)
                {
                    _formatHandler.EndInputDigits();
                }

                if (_dateTimePicker is { ShowCheckBox: true, InternalViewDrawCheckBox.ForcedTracking: true })
                {
                    if (_dateTimePicker.Checked)
                    {
                        _dateTimePicker.InternalViewDrawCheckBox.ForcedTracking = false;
                        _formatHandler.MoveLast();
                    }
                }
                else
                {
                    _formatHandler.MoveLeft();

                    // Rotate around the start of the fragments
                    if (!_formatHandler.HasActiveFragment)
                    {
                        if (_dateTimePicker.ShowCheckBox)
                        {
                            _dateTimePicker.InternalViewDrawCheckBox.ForcedTracking = true;
                        }
                        else
                        {
                            _formatHandler.MoveLast();
                        }
                    }
                }

                PerformNeedPaint(false);
                break;
            case Keys.Subtract:
            case Keys.Divide:
            case Keys.Decimal:
            case Keys.Right:
                if (_formatHandler.IsInputDigits)
                {
                    _formatHandler.EndInputDigits();
                }

                if (_dateTimePicker is { ShowCheckBox: true, InternalViewDrawCheckBox.ForcedTracking: true })
                {
                    if (_dateTimePicker.Checked)
                    {
                        _dateTimePicker.InternalViewDrawCheckBox.ForcedTracking = false;
                        _formatHandler.MoveFirst();
                    }
                }
                else
                {
                    _formatHandler.MoveRight();

                    // Rotate around the end of the fragments
                    if (!_formatHandler.HasActiveFragment)
                    {
                        if (_dateTimePicker.ShowCheckBox)
                        {
                            _dateTimePicker.InternalViewDrawCheckBox.ForcedTracking = true;
                        }
                        else
                        {
                            _formatHandler.MoveFirst();
                        }
                    }
                }

                PerformNeedPaint(false);
                break;
            case Keys.Up:
                if (_dateTimePicker.Checked)
                {
                    if (_formatHandler.IsInputDigits)
                    {
                        _formatHandler.EndInputDigits();
                    }
                    else
                    {
                        _dateTimePicker.Value = ValidateDate(_formatHandler.Increment(true));
                    }

                    PerformNeedPaint(false);
                }
                break;
            case Keys.Down:
                if (_dateTimePicker.Checked)
                {
                    if (_formatHandler.IsInputDigits)
                    {
                        _formatHandler.EndInputDigits();
                    }
                    else
                    {
                        _dateTimePicker.Value = ValidateDate(_formatHandler.Increment(false));
                    }

                    PerformNeedPaint(false);
                }
                break;
            case Keys.A:
            case Keys.P:
                if (_dateTimePicker.Checked)
                {
                    _dateTimePicker.Value = ValidateDate(_formatHandler.AMPM(e.KeyCode == Keys.A));
                    PerformNeedPaint(false);
                }
                break;
        }
    }
    #endregion

    #region PerformKeyPress
    /// <summary>
    /// Key has been pressed.
    /// </summary>
    /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
    public void PerformKeyPress(KeyPressEventArgs e)
    {
        if (char.IsDigit(e.KeyChar) && _formatHandler.HasActiveFragment)
        {
            _formatHandler.InputDigit(e.KeyChar);
            PerformNeedPaint(true);
        }
    }
    #endregion

    #region Layout
    /// <summary>
    /// Discover the preferred size of the element.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override Size GetPreferredSize([DisallowNull] ViewLayoutContext context)
    {
        Debug.Assert(context != null);

        // Inform handler of the date time currently used
        _formatHandler.DateTime = _dateTimePicker.Value;

        // Grab the font used to draw the text
        Font? font = GetFont();

        // Find the width of the text we are drawing
        Size retSize = TextRenderer.MeasureText(GetFullDisplayText(), font, Size.Empty, MEASURE_FLAGS);

        // The line height gives better appearance as it includes space for overhanging glyphs
        retSize.Height = Math.Max(font!.Height, retSize.Height);

        // Add constant extra sizing to add padding around area
        retSize.Height += 3;

        return retSize;
    }

    /// <summary>
    /// Perform a layout of the elements.
    /// </summary>
    /// <param name="context">Layout context.</param>
    public override void Layout(ViewLayoutContext context)
    {
        // Take all the provide space
        ClientRectangle = context.DisplayRectangle;

        // Inform handler of the date time currently used
        _formatHandler.DateTime = _dateTimePicker.Value;

        // Refresh the format handler with the latest format string
        _formatHandler.ParseFormat(GetFormat(), context.Graphics, GetFont());
    }
    #endregion

    #region Paint
    /// <summary>
    /// Perform a render of the elements.
    /// </summary>
    /// <param name="context">Rendering context.</param>
    public override void Render(RenderContext context)
    {
        // Inform handler of the date time currently used
        _formatHandler.DateTime = _dateTimePicker.Value;

        // Ask the format handler to perform actual rendering of the text
        using Clipping clipped = new Clipping(context.Graphics, ClientRectangle);
        _formatHandler.Render(context, GetFont(), ClientRectangle,
            GetTextColor(), GetBackColor(),
            _dateTimePicker.Checked);
    }
    #endregion

    #region Implementation
    internal DateTime ValidateDate(DateTime dt)
    {
        if (dt < _dateTimePicker.EffectiveMinDate(_dateTimePicker.MinDate))
        {
            return _dateTimePicker.EffectiveMinDate(_dateTimePicker.MinDate);
        }
        else
        {
            return dt > _dateTimePicker.EffectiveMaxDate(_dateTimePicker.MaxDate)
                ? _dateTimePicker.EffectiveMaxDate(_dateTimePicker.MaxDate)
                : dt;
        }
    }

    private void PerformNeedPaint(bool needLayout) => _needPaint(this, new NeedLayoutEventArgs(needLayout));

    private Font? GetFont()
    {
        if (!Enabled || _dateTimePicker.InternalDateTimeNull())
        {
            return _dateTimePicker.StateDisabled.PaletteContent.GetContentShortTextFont(PaletteState.Disabled);
        }
        else
        {
            return _dateTimePicker.IsActive
                ? _dateTimePicker.StateActive.PaletteContent.GetContentShortTextFont(PaletteState.Normal)
                : _dateTimePicker.StateNormal.PaletteContent.GetContentShortTextFont(PaletteState.Normal);
        }
    }

    private Color GetTextColor()
    {
        if (!Enabled || _dateTimePicker.InternalDateTimeNull())
        {
            return _dateTimePicker.StateDisabled.PaletteContent.GetContentShortTextColor1(PaletteState.Disabled);
        }
        else
        {
            return _dateTimePicker.IsActive
                ? _dateTimePicker.StateActive.PaletteContent.GetContentShortTextColor1(PaletteState.Normal)
                : _dateTimePicker.StateNormal.PaletteContent.GetContentShortTextColor1(PaletteState.Normal);
        }
    }

    private Color GetBackColor()
    {
        if (!Enabled || _dateTimePicker.InternalDateTimeNull())
        {
            return _dateTimePicker.StateDisabled.PaletteBack.GetBackColor1(PaletteState.Disabled);
        }
        else
        {
            return _dateTimePicker.IsActive
                ? _dateTimePicker.StateActive.PaletteBack.GetBackColor1(PaletteState.Normal)
                : _dateTimePicker.StateNormal.PaletteBack.GetBackColor1(PaletteState.Normal);
        }
    }

    private string GetFormat()
    {
        var format = _dateTimePicker.Format switch
        {
            DateTimePickerFormat.Long => CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern,
            DateTimePickerFormat.Short => CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern,
            DateTimePickerFormat.Time => CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern,
            DateTimePickerFormat.Custom =>
                // Use helper to ensure single character formats are handled correctly
                CommonHelper.MakeCustomDateFormat(_dateTimePicker.CustomFormat),
            _ => string.Empty
        };

        return format;
    }

    private string GetFullDisplayText()
    {
        try
        {
            return _dateTimePicker.Value.ToString(GetFormat());
        }
        catch
        {
            return string.Empty;
        }
    }
    #endregion
}