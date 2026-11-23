#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

internal static class KryptonLogXmlConfiguration
{
    internal static KryptonLogConfiguration Load(Stream stream)
    {
        var document = new XmlDocument();
        document.Load(stream);
        var root = document.DocumentElement ?? throw new InvalidOperationException("Krypton log XML is empty.");
        var configuration = new KryptonLogConfiguration();

        var min = root.GetAttribute("minimumLevel");
        if (TryParseLevel(min, out var level))
        {
            configuration.MinimumLevel(level);
        }

        var async = root.GetAttribute("async");
        if (string.Equals(async, "false", StringComparison.OrdinalIgnoreCase))
        {
            configuration.Sync();
        }
        else
        {
            var capacityText = root.GetAttribute("queueCapacity");
            var capacity = int.TryParse(capacityText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed)
                ? parsed
                : 4096;
            configuration.Async(capacity);
        }

        var layout = root.GetAttribute("layout");
        if (!string.IsNullOrWhiteSpace(layout))
        {
            configuration.UseLayout(layout);
        }

        foreach (XmlNode node in root.ChildNodes)
        {
            if (node is not XmlElement element)
            {
                continue;
            }

            switch (element.LocalName)
            {
                case "overrides":
                    LoadOverrides(configuration, element);
                    break;
                case "enrich":
                    LoadEnrich(configuration, element);
                    break;
                case "writeTo":
                    LoadWriteTo(configuration, element);
                    break;
            }
        }

        return configuration;
    }

    internal static void Save(Stream stream, KryptonLogConfiguration configuration)
    {
        var settings = new XmlWriterSettings
        {
            Indent = true,
            Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true)
        };

        using var writer = XmlWriter.Create(stream, settings);
        writer.WriteStartElement("kryptonLog");
        writer.WriteAttributeString("minimumLevel", configuration.MinimumLevelValue.ToString());
        writer.WriteAttributeString("async", configuration.UseAsync ? "true" : "false");
        writer.WriteAttributeString("queueCapacity", configuration.AsyncQueueCapacity.ToString(CultureInfo.InvariantCulture));
        writer.WriteAttributeString("layout", configuration.Layout.Pattern);

        writer.WriteStartElement("overrides");
        foreach (var item in configuration.OverrideList)
        {
            writer.WriteStartElement("override");
            writer.WriteAttributeString("category", item.Prefix);
            writer.WriteAttributeString("level", item.Level.ToString());
            writer.WriteEndElement();
        }

        writer.WriteEndElement();

        writer.WriteStartElement("enrich");
        writer.WriteAttributeString("threadId", configuration.EnrichThreadId ? "true" : "false");
        writer.WriteAttributeString("machineName", configuration.EnrichMachineName ? "true" : "false");
        writer.WriteEndElement();

        writer.WriteStartElement("writeTo");
        if (configuration.SyncSinks.Exists(static s => s is DebugLogSink))
        {
            writer.WriteStartElement("debug");
            writer.WriteEndElement();
        }

        if (configuration.SyncSinks.Exists(static s => s is TraceLogSink))
        {
            writer.WriteStartElement("trace");
            writer.WriteEndElement();
        }

        if (configuration.File != null)
        {
            writer.WriteStartElement("file");
            writer.WriteAttributeString("path", configuration.File.Path);
            writer.WriteEndElement();
        }

        if (configuration.Memory != null)
        {
            writer.WriteStartElement("memory");
            writer.WriteAttributeString("capacity", configuration.Memory.Capacity.ToString(CultureInfo.InvariantCulture));
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
        writer.WriteEndElement();
    }

    private static void LoadOverrides(KryptonLogConfiguration configuration, XmlElement element)
    {
        foreach (XmlNode child in element.ChildNodes)
        {
            if (child is not XmlElement overrideElement || overrideElement.LocalName != "override")
            {
                continue;
            }

            var category = overrideElement.GetAttribute("category");
            if (TryParseLevel(overrideElement.GetAttribute("level"), out var level))
            {
                configuration.Override(category, level);
            }
        }
    }

    private static void LoadEnrich(KryptonLogConfiguration configuration, XmlElement element)
    {
        if (KryptonLogPaths.IsTruthy(element.GetAttribute("threadId")))
        {
            configuration.Enrich.WithThreadId();
        }

        if (KryptonLogPaths.IsTruthy(element.GetAttribute("machineName")))
        {
            configuration.Enrich.WithMachineName();
        }
    }

    private static void LoadWriteTo(KryptonLogConfiguration configuration, XmlElement element)
    {
        foreach (XmlNode child in element.ChildNodes)
        {
            if (child is not XmlElement sink)
            {
                continue;
            }

            switch (sink.LocalName)
            {
                case "debug":
                    configuration.WriteTo.Debug();
                    break;
                case "trace":
                    configuration.WriteTo.Trace();
                    break;
                case "file":
                    var path = sink.GetAttribute("path");
                    var size = long.TryParse(sink.GetAttribute("rollOnSizeBytes"), NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedSize)
                        ? parsedSize
                        : 5_000_000;
                    var retained = int.TryParse(sink.GetAttribute("retainedFileCount"), NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedRetained)
                        ? parsedRetained
                        : 7;
                    var rollOnDate = !string.Equals(sink.GetAttribute("rollOnDate"), "false", StringComparison.OrdinalIgnoreCase);
                    configuration.WriteTo.File(string.IsNullOrWhiteSpace(path) ? null : path, size, retained, rollOnDate);
                    break;
                case "memory":
                    var capacity = int.TryParse(sink.GetAttribute("capacity"), NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedCapacity)
                        ? parsedCapacity
                        : 2000;
                    configuration.WriteTo.Memory(capacity);
                    break;
                case "eventLog":
                    configuration.WriteTo.EventLog(
                        string.IsNullOrWhiteSpace(sink.GetAttribute("source")) ? "Krypton" : sink.GetAttribute("source"),
                        string.IsNullOrWhiteSpace(sink.GetAttribute("logName")) ? "Application" : sink.GetAttribute("logName"));
                    break;
            }
        }
    }

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
