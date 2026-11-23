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
/// Rolling file sink. Keeps a single <see cref="StreamWriter"/> open; rolls on size and/or local midnight.
/// </summary>
internal sealed class FileLogSink : IKryptonLogSink
{
    private readonly object _sync = new();
    private readonly KryptonLogLayout _layout;
    private readonly long? _rollOnSizeBytes;
    private readonly int _retainedFileCount;
    private readonly bool _rollOnDate;
    private readonly string _directory;
    private readonly string _fileName;
    private readonly string _extension;
    private StreamWriter? _writer;
    private long _bytesWritten;
    private DateTime _currentDate;
    private int _disposed;

    public FileLogSink(string path, long? rollOnSizeBytes, int retainedFileCount, bool rollOnDate, KryptonLogLayout? layout)
    {
        Path = string.IsNullOrWhiteSpace(path) ? KryptonLogPaths.DefaultFilePath : path;
        _layout = layout ?? KryptonLogLayout.Default;
        _rollOnSizeBytes = rollOnSizeBytes is > 0 ? rollOnSizeBytes : null;
        _retainedFileCount = Math.Max(1, retainedFileCount);
        _rollOnDate = rollOnDate;
        _directory = System.IO.Path.GetDirectoryName(Path) ?? string.Empty;
        if (string.IsNullOrEmpty(_directory))
        {
            _directory = System.IO.Path.GetDirectoryName(KryptonLogPaths.DefaultFilePath) ?? ".";
        }
        _fileName = System.IO.Path.GetFileNameWithoutExtension(Path);
        _extension = System.IO.Path.GetExtension(Path);
        if (string.IsNullOrEmpty(_extension))
        {
            _extension = ".log";
        }

        try
        {
            Directory.CreateDirectory(_directory);
            OpenWriter();
        }
        catch
        {
            _writer = null;
        }
    }

    public string Path { get; }

    public bool IsEnabled(KryptonLogLevel level) => _writer != null;

    public void Emit(KryptonLogEvent logEvent)
    {
        if (_writer == null || Volatile.Read(ref _disposed) != 0)
        {
            return;
        }

        var line = _layout.Render(logEvent);
        lock (_sync)
        {
            try
            {
                MaybeRoll();
                if (_writer == null)
                {
                    return;
                }

                _writer.Write(KryptonLogProtect.Protect(line));
                if (!line.EndsWith(Environment.NewLine, StringComparison.Ordinal))
                {
                    _writer.WriteLine();
                    _bytesWritten += Encoding.UTF8.GetByteCount(line) + Encoding.UTF8.GetByteCount(Environment.NewLine);
                }
                else
                {
                    _bytesWritten += Encoding.UTF8.GetByteCount(line);
                }
            }
            catch
            {
                // ignored
            }
        }
    }

    public void Dispose()
    {
        if (Interlocked.Exchange(ref _disposed, 1) != 0)
        {
            return;
        }

        lock (_sync)
        {
            CloseWriter();
        }
    }

    private void MaybeRoll()
    {
        var today = DateTime.Now.Date;
        var sizeExceeded = _rollOnSizeBytes.HasValue && _bytesWritten >= _rollOnSizeBytes.Value;
        var dateChanged = _rollOnDate && today != _currentDate;
        if (!sizeExceeded && !dateChanged)
        {
            return;
        }

        CloseWriter();
        ArchiveCurrent(_currentDate);
        PruneArchives();
        OpenWriter();
    }

    private void OpenWriter()
    {
        Directory.CreateDirectory(_directory);
        _writer = new StreamWriter(Path, append: true, Encoding.UTF8) { AutoFlush = true };
        _bytesWritten = File.Exists(Path) ? new FileInfo(Path).Length : 0;
        _currentDate = DateTime.Now.Date;
    }

    private void CloseWriter()
    {
        if (_writer == null)
        {
            return;
        }

        try
        {
            _writer.Flush();
            _writer.Dispose();
        }
        catch
        {
            // ignored
        }

        _writer = null;
    }

    private void ArchiveCurrent(DateTime date)
    {
        if (!File.Exists(Path))
        {
            return;
        }

        var stamp = date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        var candidate = System.IO.Path.Combine(_directory, $"{_fileName}.{stamp}{_extension}");
        var index = 1;
        while (File.Exists(candidate))
        {
            candidate = System.IO.Path.Combine(_directory, $"{_fileName}.{stamp}.{index}{_extension}");
            index++;
        }

        try
        {
            File.Move(Path, candidate);
        }
        catch
        {
            // ignored
        }
    }

    private void PruneArchives()
    {
        try
        {
            var prefix = $"{_fileName}.";
            var files = Directory.GetFiles(_directory, $"{_fileName}.*{_extension}")
                .Where(f => !string.Equals(f, Path, StringComparison.OrdinalIgnoreCase)
                            && System.IO.Path.GetFileName(f).StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .Select(f => new FileInfo(f))
                .OrderByDescending(f => f.LastWriteTimeUtc)
                .ToList();

            for (var i = _retainedFileCount; i < files.Count; i++)
            {
                try
                {
                    files[i].Delete();
                }
                catch
                {
                    // ignored
                }
            }
        }
        catch
        {
            // ignored
        }
    }
}
