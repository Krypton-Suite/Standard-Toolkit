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
/// Renders a <see cref="KryptonLogEvent"/> using a layout string.
/// </summary>
/// <remarks>
/// Supported tokens: <c>Timestamp</c>, <c>Level</c>, <c>Category</c>, <c>Message</c>,
/// <c>NewLine</c>, <c>Exception</c>, <c>ThreadId</c>, <c>MachineName</c>. Timestamp accepts a
/// .NET date format after a colon, e.g. <c>{Timestamp:yyyy-MM-dd HH:mm:ss.fff}</c>.
/// </remarks>
public sealed class KryptonLogLayout
{
    /// <summary>The default file/debug layout.</summary>
    public const string DefaultPattern =
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Category} {Message}{NewLine}{Exception}";

    private readonly KryptonLogMessageTemplatePlaceholder[] _parts;

    /// <summary>
    /// Initializes a new instance of the <see cref="KryptonLogLayout"/> class.
    /// </summary>
    /// <param name="pattern">Layout pattern. When null or empty, <see cref="DefaultPattern"/> is used.</param>
    public KryptonLogLayout(string? pattern)
    {
        var resolved = string.IsNullOrWhiteSpace(pattern) ? DefaultPattern : pattern!;
        Pattern = resolved;
        _parts = Parse(resolved);
    }

    /// <summary>Gets the shared default layout.</summary>
    public static KryptonLogLayout Default { get; } = new(DefaultPattern);

    /// <summary>Gets the layout pattern.</summary>
    public string Pattern { get; }

    /// <summary>
    /// Renders <paramref name="logEvent"/> according to <see cref="Pattern"/>.
    /// </summary>
    /// <param name="logEvent">The event to render. Cannot be null.</param>
    public string Render(KryptonLogEvent logEvent)
    {
        ThrowHelper.ThrowIfNull(logEvent);

        var sb = new StringBuilder();
        foreach (var part in _parts)
        {
            sb.Append(part.Render(logEvent));
        }

        return sb.ToString();
    }

    private static KryptonLogMessageTemplatePlaceholder[] Parse(string pattern)
    {
        var parts = new List<KryptonLogMessageTemplatePlaceholder>();
        var literal = new StringBuilder();
        var i = 0;
        while (i < pattern.Length)
        {
            if (pattern[i] == '{' && i + 1 < pattern.Length && pattern[i + 1] == '{')
            {
                literal.Append('{');
                i += 2;
                continue;
            }

            if (pattern[i] == '}' && i + 1 < pattern.Length && pattern[i + 1] == '}')
            {
                literal.Append('}');
                i += 2;
                continue;
            }

            if (pattern[i] != '{')
            {
                literal.Append(pattern[i]);
                i++;
                continue;
            }

            var close = pattern.IndexOf('}', i + 1);
            if (close < 0)
            {
                literal.Append(pattern[i]);
                i++;
                continue;
            }

            if (literal.Length > 0)
            {
                parts.Add(KryptonLogMessageTemplatePlaceholder.Literal(literal.ToString()));
                literal.Clear();
            }

            var body = pattern.Substring(i + 1, close - i - 1);
            var colon = body.IndexOf(':');
            var name = colon < 0 ? body : body.Substring(0, colon);
            var format = colon < 0 ? null : body.Substring(colon + 1);
            parts.Add(KryptonLogMessageTemplatePlaceholder.Token(name, format));
            i = close + 1;
        }

        if (literal.Length > 0)
        {
            parts.Add(KryptonLogMessageTemplatePlaceholder.Literal(literal.ToString()));
        }

        return parts.ToArray();
    }

    private readonly struct KryptonLogMessageTemplatePlaceholder
    {
        private readonly string? _literal;
        private readonly string? _token;
        private readonly string? _format;

        private KryptonLogMessageTemplatePlaceholder(string? literal, string? token, string? format)
        {
            _literal = literal;
            _token = token;
            _format = format;
        }

        public static KryptonLogMessageTemplatePlaceholder Literal(string text) =>
            new(text, null, null);

        public static KryptonLogMessageTemplatePlaceholder Token(string name, string? format) =>
            new(null, name, format);

        public string Render(KryptonLogEvent logEvent)
        {
            if (_literal != null)
            {
                return _literal;
            }

            switch (_token)
            {
                case "Timestamp":
                    return logEvent.Timestamp.ToString(_format ?? "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                case "Level":
                    return logEvent.Level.ToString();
                case "Category":
                    return logEvent.Category;
                case "Message":
                    return logEvent.Message;
                case "NewLine":
                    return Environment.NewLine;
                case "Exception":
                    return logEvent.Exception == null ? string.Empty : logEvent.Exception.ToString();
                case "ThreadId":
                    return logEvent.ThreadId.ToString(CultureInfo.InvariantCulture);
                case "MachineName":
                    return logEvent.MachineName ?? string.Empty;
                default:
                    return string.Empty;
            }
        }
    }
}
