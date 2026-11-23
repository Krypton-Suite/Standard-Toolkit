#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

#if NET8_0_OR_GREATER
namespace Krypton.Toolkit.Utilities;

internal static class KryptonLogJsonConfiguration
{
    internal static KryptonLogConfiguration Load(Stream stream)
    {
        using var document = System.Text.Json.JsonDocument.Parse(stream);
        var root = document.RootElement;
        var configuration = new KryptonLogConfiguration();

        if (root.TryGetProperty("minimumLevel", out var min) && TryParseLevel(min.GetString(), out var level))
        {
            configuration.MinimumLevel(level);
        }

        if (root.TryGetProperty("async", out var asyncElement) && asyncElement.ValueKind == System.Text.Json.JsonValueKind.False)
        {
            configuration.Sync();
        }
        else
        {
            var capacity = 4096;
            if (root.TryGetProperty("queueCapacity", out var cap) && cap.TryGetInt32(out var parsed))
            {
                capacity = parsed;
            }

            configuration.Async(capacity);
        }

        if (root.TryGetProperty("layout", out var layout) && layout.ValueKind == System.Text.Json.JsonValueKind.String)
        {
            var pattern = layout.GetString();
            if (!string.IsNullOrWhiteSpace(pattern))
            {
                configuration.UseLayout(pattern);
            }
        }

        if (root.TryGetProperty("overrides", out var overrides) && overrides.ValueKind == System.Text.Json.JsonValueKind.Array)
        {
            foreach (var item in overrides.EnumerateArray())
            {
                var category = item.TryGetProperty("category", out var cat) ? cat.GetString() : string.Empty;
                if (item.TryGetProperty("level", out var lvl) && TryParseLevel(lvl.GetString(), out var overrideLevel))
                {
                    configuration.Override(category ?? string.Empty, overrideLevel);
                }
            }
        }

        if (root.TryGetProperty("enrich", out var enrich) && enrich.ValueKind == System.Text.Json.JsonValueKind.Object)
        {
            if (IsTrue(enrich, "threadId"))
            {
                configuration.Enrich.WithThreadId();
            }

            if (IsTrue(enrich, "machineName"))
            {
                configuration.Enrich.WithMachineName();
            }
        }

        if (root.TryGetProperty("writeTo", out var writeTo) && writeTo.ValueKind == System.Text.Json.JsonValueKind.Array)
        {
            foreach (var sink in writeTo.EnumerateArray())
            {
                var name = sink.TryGetProperty("name", out var n) ? n.GetString() : null;
                switch (name)
                {
                    case "debug":
                        configuration.WriteTo.Debug();
                        break;
                    case "trace":
                        configuration.WriteTo.Trace();
                        break;
                    case "file":
                        var path = sink.TryGetProperty("path", out var p) ? p.GetString() : null;
                        var size = sink.TryGetProperty("rollOnSizeBytes", out var s) && s.TryGetInt64(out var parsedSize)
                            ? parsedSize
                            : 5_000_000;
                        var retained = sink.TryGetProperty("retainedFileCount", out var r) && r.TryGetInt32(out var parsedRetained)
                            ? parsedRetained
                            : 7;
                        var rollOnDate = !sink.TryGetProperty("rollOnDate", out var d) || d.ValueKind != System.Text.Json.JsonValueKind.False;
                        configuration.WriteTo.File(path, size, retained, rollOnDate);
                        break;
                    case "memory":
                        var capacity = sink.TryGetProperty("capacity", out var c) && c.TryGetInt32(out var parsedCapacity)
                            ? parsedCapacity
                            : 2000;
                        configuration.WriteTo.Memory(capacity);
                        break;
                    case "eventLog":
                        var source = sink.TryGetProperty("source", out var src) ? src.GetString() : "Krypton";
                        var logName = sink.TryGetProperty("logName", out var ln) ? ln.GetString() : "Application";
                        configuration.WriteTo.EventLog(source ?? "Krypton", logName ?? "Application");
                        break;
                }
            }
        }

        return configuration;
    }

    private static bool IsTrue(System.Text.Json.JsonElement element, string name) =>
        element.TryGetProperty(name, out var value)
        && (value.ValueKind == System.Text.Json.JsonValueKind.True
            || (value.ValueKind == System.Text.Json.JsonValueKind.String && KryptonLogPaths.IsTruthy(value.GetString())));

    private static bool TryParseLevel(string? text, out KryptonLogLevel level)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            level = KryptonLogLevel.Information;
            return false;
        }

        if (string.Equals(text, "Info", StringComparison.OrdinalIgnoreCase)
            || string.Equals(text, "Information", StringComparison.OrdinalIgnoreCase))
        {
            level = KryptonLogLevel.Information;
            return true;
        }

        return Enum.TryParse(text, ignoreCase: true, out level);
    }
}
#endif
