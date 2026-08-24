#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Parses and caches <c>{Name}</c> / <c>{0}</c> message templates. No Serilog destructuring.
/// </summary>
internal sealed class KryptonLogMessageTemplate
{
    private static readonly ConcurrentDictionary<string, KryptonLogMessageTemplate> Cache = new();
    private static readonly KryptonLogProperty[] EmptyProperties = Array.Empty<KryptonLogProperty>();

    private readonly string[] _literals;
    private readonly Hole[] _holes;

    private KryptonLogMessageTemplate(string[] literals, Hole[] holes)
    {
        _literals = literals;
        _holes = holes;
    }

    public static string Render(string? template, object?[]? args, out KryptonLogProperty[] properties)
    {
        if (template is null || template.Length == 0)
        {
            properties = EmptyProperties;
            return string.Empty;
        }

        if (args == null || args.Length == 0)
        {
            if (template.IndexOf('{') < 0)
            {
                properties = EmptyProperties;
                return template;
            }
        }

        var parsed = Cache.GetOrAdd(template, static t => Parse(t));
        return parsed.Format(args, out properties);
    }

    private string Format(object?[]? args, out KryptonLogProperty[] properties)
    {
        if (_holes.Length == 0)
        {
            properties = EmptyProperties;
            return _literals[0];
        }

        properties = new KryptonLogProperty[_holes.Length];
        var sb = new StringBuilder();
        for (var i = 0; i < _holes.Length; i++)
        {
            sb.Append(_literals[i]);
            var argIndex = _holes[i].Index;
            object? value = args != null && argIndex >= 0 && argIndex < args.Length ? args[argIndex] : null;
            properties[i] = new KryptonLogProperty(_holes[i].Name, value);
            sb.Append(FormatValue(value, _holes[i].Format));
        }

        sb.Append(_literals[_holes.Length]);
        return sb.ToString();
    }

    private static string FormatValue(object? value, string? format)
    {
        if (value == null)
        {
            return string.Empty;
        }

        if (string.IsNullOrEmpty(format))
        {
            return Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty;
        }

        return value is IFormattable formattable
            ? formattable.ToString(format, CultureInfo.InvariantCulture) ?? string.Empty
            : Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty;
    }

    private static KryptonLogMessageTemplate Parse(string template)
    {
        var literals = new List<string>();
        var holes = new List<Hole>();
        var literal = new StringBuilder();
        var positional = 0;
        var i = 0;
        while (i < template.Length)
        {
            var ch = template[i];
            if (ch == '{' && i + 1 < template.Length && template[i + 1] == '{')
            {
                literal.Append('{');
                i += 2;
                continue;
            }

            if (ch == '}' && i + 1 < template.Length && template[i + 1] == '}')
            {
                literal.Append('}');
                i += 2;
                continue;
            }

            if (ch != '{')
            {
                literal.Append(ch);
                i++;
                continue;
            }

            var close = template.IndexOf('}', i + 1);
            if (close < 0)
            {
                literal.Append(ch);
                i++;
                continue;
            }

            literals.Add(literal.ToString());
            literal.Clear();
            var body = template.Substring(i + 1, close - i - 1);
            var colon = body.IndexOf(':');
            var name = colon < 0 ? body.Trim() : body.Substring(0, colon).Trim();
            var format = colon < 0 ? null : body.Substring(colon + 1);
            if (string.IsNullOrEmpty(name))
            {
                name = positional.ToString(CultureInfo.InvariantCulture);
            }

            var index = int.TryParse(name, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedIndex)
                ? parsedIndex
                : positional;
            holes.Add(new Hole(name, format, index));
            positional++;
            i = close + 1;
        }

        literals.Add(literal.ToString());
        return new KryptonLogMessageTemplate(literals.ToArray(), holes.ToArray());
    }

    private readonly struct Hole
    {
        public Hole(string name, string? format, int index)
        {
            Name = name;
            Format = format;
            Index = index;
        }

        public string Name { get; }
        public string? Format { get; }
        public int Index { get; }
    }
}
