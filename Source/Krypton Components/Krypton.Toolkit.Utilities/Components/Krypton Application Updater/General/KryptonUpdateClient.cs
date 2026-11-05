#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed, tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// HTTPS downloads that refuse to leave https, SHA-256 and Authenticode checks, and installer or helper launch.
/// One <see cref="HttpClient"/> is kept for the process. Redirects are followed only while the next address is still https.
/// </summary>
internal static class KryptonUpdateClient
{
    internal const string HelperResourceName = "Krypton.Toolkit.Utilities.UpdateHelper.exe";

    private static readonly HttpClient Http = CreateClient(true);

    // Range requests stay compressed so a gzip body is visible and is not appended to the saved bytes.
    private static readonly HttpClient RawHttp = CreateClient(false);

    internal static bool IsHttps(string location) =>
        Uri.TryCreate(location, UriKind.Absolute, out var uri) && uri.Scheme == Uri.UriSchemeHttps;

    internal static bool TryGetLocalPath(string location, out string path)
    {
        path = string.Empty;
        if (string.IsNullOrWhiteSpace(location))
        {
            return false;
        }

        if (Uri.TryCreate(location, UriKind.Absolute, out var uri) && uri.IsFile)
        {
            path = uri.LocalPath;
            return File.Exists(path);
        }

        if (IsHttps(location))
        {
            return false;
        }

        if (File.Exists(location))
        {
            path = Path.GetFullPath(location);
            return true;
        }

        return false;
    }

    internal static async Task<string> ReadTextAsync(string location, string? bearerToken, string? accept, CancellationToken cancellationToken)
    {
        if (TryGetLocalPath(location, out var path))
        {
            return File.ReadAllText(path);
        }

        if (!IsHttps(location))
        {
            throw new KryptonUpdateException("The update address must be https or a local file.");
        }

        using var response = await SendAsync(location, bearerToken, accept, null, cancellationToken).ConfigureAwait(false);
        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
    }

    /// <summary>Downloads at most <paramref name="maxBytes"/> from an https address. Throws when the response is larger.</summary>
    /// <param name="location">HTTPS address.</param>
    /// <param name="maxBytes">Upper bound for the body.</param>
    /// <param name="cancellationToken">Cancels the copy.</param>
    internal static async Task<byte[]> DownloadBytesAsync(string location, int maxBytes, CancellationToken cancellationToken)
    {
        if (!IsHttps(location))
        {
            throw new KryptonUpdateException("The banner image address must be https or a local file.");
        }

        using var response = await SendAsync(location, null, null, null, cancellationToken).ConfigureAwait(false);
        if (response.Content.Headers.ContentLength.HasValue && response.Content.Headers.ContentLength.Value > maxBytes)
        {
            throw new KryptonUpdateException("The banner image is larger than " + maxBytes + " bytes.");
        }

        using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        using var buffer = new MemoryStream(Math.Min(maxBytes, 64 * 1024));
        var chunk = new byte[8192];
        int read;
        while ((read = await stream.ReadAsync(chunk, 0, chunk.Length, cancellationToken).ConfigureAwait(false)) > 0)
        {
            if (buffer.Length + read > maxBytes)
            {
                throw new KryptonUpdateException("The banner image is larger than " + maxBytes + " bytes.");
            }

            buffer.Write(chunk, 0, read);
        }

        return buffer.ToArray();
    }

    internal static async Task<string> DownloadPackageAsync(string location, IProgress<KryptonUpdateDownloadProgress>? progress, CancellationToken cancellationToken)
    {
        var destination = CreateDownloadPath(location);
        try
        {
            await DownloadToAsync(location, destination, false, progress, null, cancellationToken).ConfigureAwait(false);
            return destination;
        }
        catch
        {
            TryDelete(destination);
            throw;
        }
    }

    /// <summary>
    /// Copies <paramref name="location"/> to <paramref name="destination"/>.
    /// A resume sends <c>Range</c> when the file already has bytes. A 200, 416, or compressed range replaces the file.
    /// Failures leave whatever bytes are already on disk.
    /// </summary>
    internal static async Task DownloadToAsync(string location, string destination, bool resume, IProgress<KryptonUpdateDownloadProgress>? progress, Action? startingAgain, CancellationToken cancellationToken)
    {
        var directory = Path.GetDirectoryName(destination);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (TryGetLocalPath(location, out var local))
        {
            var length = new FileInfo(local).Length;
            File.Copy(local, destination, true);
            progress?.Report(new KryptonUpdateDownloadProgress(length, length));
            return;
        }

        if (!IsHttps(location))
        {
            throw new KryptonUpdateException("The package address must be https or a local file.");
        }

        long existing = 0;
        if (resume && File.Exists(destination))
        {
            existing = new FileInfo(destination).Length;
        }

        if (existing > 0)
        {
            HttpResponseMessage response;
            try
            {
                response = await SendAsync(location, null, null, existing, cancellationToken).ConfigureAwait(false);
            }
            catch (KryptonUpdateException exception) when (exception.HttpStatus == 416)
            {
                startingAgain?.Invoke();
                await CopyHttpAsync(location, destination, progress, cancellationToken).ConfigureAwait(false);
                return;
            }

            using (response)
            {
                var compressed = IsCompressed(response);
                if (response.StatusCode == HttpStatusCode.PartialContent && !compressed && RangeMatches(response, existing))
                {
                    await CopyBodyAsync(response, destination, existing, ReadTotal(response, existing), progress, cancellationToken).ConfigureAwait(false);
                    return;
                }

                if (response.StatusCode == HttpStatusCode.OK && !compressed)
                {
                    startingAgain?.Invoke();
                    await CopyBodyAsync(response, destination, 0, response.Content.Headers.ContentLength, progress, cancellationToken).ConfigureAwait(false);
                    return;
                }

                if (response.StatusCode != HttpStatusCode.OK &&
                    response.StatusCode != HttpStatusCode.PartialContent)
                {
                    throw new KryptonUpdateException("The server returned " + (int)response.StatusCode + ".");
                }
            }

            startingAgain?.Invoke();
            await CopyHttpAsync(location, destination, progress, cancellationToken).ConfigureAwait(false);
            return;
        }

        await CopyHttpAsync(location, destination, progress, cancellationToken).ConfigureAwait(false);
    }

    internal static void Verify(string path, string sha256, string? thumbprint)
    {
        var expected = ParseSha256(sha256);
        byte[] actual;
        using (var stream = File.OpenRead(path))
        using (var algorithm = SHA256.Create())
        {
            actual = algorithm.ComputeHash(stream);
        }

        if (!FixedTimeEquals(expected, actual))
        {
            throw new KryptonUpdateException("The downloaded file does not match the expected SHA-256 hash.");
        }

        if (thumbprint == null || string.IsNullOrWhiteSpace(thumbprint))
        {
            return;
        }

        X509Certificate2 certificate;
        try
        {
            // X509CertificateLoader does not read an Authenticode signature out of a PE file.
#pragma warning disable SYSLIB0057
            certificate = new X509Certificate2(X509Certificate.CreateFromSignedFile(path));
#pragma warning restore SYSLIB0057
        }
        catch (Exception exception)
        {
            throw new KryptonUpdateException("The downloaded file is not Authenticode-signed.", exception);
        }

        var actualThumb = (certificate.Thumbprint ?? string.Empty).Replace(" ", string.Empty);
        var expectedThumb = thumbprint.Replace(" ", string.Empty);
        if (!string.Equals(actualThumb, expectedThumb, StringComparison.OrdinalIgnoreCase))
        {
            throw new KryptonUpdateException("The Authenticode certificate thumbprint does not match the manifest.");
        }
    }

    internal static void StartInstaller(string path, string? arguments)
    {
        ProcessStartInfo start;
        if (path.EndsWith(".msi", StringComparison.OrdinalIgnoreCase))
        {
            start = new ProcessStartInfo
            {
                FileName = "msiexec",
                Arguments = "/i \"" + path + "\" " + (arguments ?? string.Empty),
                UseShellExecute = true
            };
        }
        else
        {
            start = new ProcessStartInfo
            {
                FileName = path,
                Arguments = arguments ?? string.Empty,
                UseShellExecute = true
            };
        }

        if (Process.Start(start) == null)
        {
            throw new KryptonUpdateException("The installer did not start.");
        }
    }

    /// <summary>
    /// Starts the embedded helper. When <paramref name="processId"/> is positive the helper waits for that process before extracting.
    /// </summary>
    internal static Process StartHelper(string zipPath, string destination, string? restartExecutable, string? restartArguments, int processId)
    {
        var helperPath = ExtractHelper();
        var jobPath = Path.Combine(Path.GetDirectoryName(helperPath) ?? Path.GetTempPath(), "job.json");
        File.WriteAllText(jobPath, WriteJob(processId, zipPath, destination, restartExecutable, restartArguments), Encoding.UTF8);
        var process = Process.Start(new ProcessStartInfo
        {
            FileName = helperPath,
            Arguments = "--krypton-update-job \"" + jobPath + "\"",
            UseShellExecute = false,
            CreateNoWindow = true
        });
        if (process == null)
        {
            throw new KryptonUpdateException("The update helper did not start.");
        }

        return process;
    }

    internal static string ReadHelperResult(string jobDirectory)
    {
        var result = Path.Combine(jobDirectory, "job.json.result");
        if (!File.Exists(result))
        {
            return "The update helper failed.";
        }

        var text = File.ReadAllText(result);
        return string.IsNullOrWhiteSpace(text) ? "The update helper failed." : text;
    }

    private static HttpClient CreateClient(bool decompress)
    {
        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = false,
            AutomaticDecompression = decompress ? DecompressionMethods.GZip | DecompressionMethods.Deflate : DecompressionMethods.None
        };
        var client = new HttpClient(handler)
        {
            Timeout = TimeSpan.FromMinutes(10)
        };
        client.DefaultRequestHeaders.UserAgent.ParseAdd("KryptonApplicationUpdater");
        return client;
    }

    private static async Task<HttpResponseMessage> SendAsync(string location, string? bearerToken, string? accept, long? rangeFrom, CancellationToken cancellationToken)
    {
        var current = new Uri(location);
        var client = rangeFrom.HasValue ? RawHttp : Http;
        for (var hop = 0; hop < 5; hop++)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, current);
            if (!string.IsNullOrWhiteSpace(accept))
            {
                request.Headers.Accept.ParseAdd(accept);
            }

            if (!string.IsNullOrWhiteSpace(bearerToken))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
            }

            if (rangeFrom.HasValue)
            {
                request.Headers.Range = new System.Net.Http.Headers.RangeHeaderValue(rangeFrom.Value, null);
            }

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
            var status = (int)response.StatusCode;
            if (status >= 300 && status < 400)
            {
                var next = response.Headers.Location;
                response.Dispose();
                if (next == null)
                {
                    throw new KryptonUpdateException("The server redirected without a location.");
                }

                if (!next.IsAbsoluteUri)
                {
                    next = new Uri(current, next);
                }

                if (!string.Equals(next.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
                {
                    throw new KryptonUpdateException("Refusing a redirect that leaves https.");
                }

                current = next;
                continue;
            }

            if (!response.IsSuccessStatusCode)
            {
                response.Dispose();
                throw new KryptonUpdateException("The server returned " + status + ".", status);
            }

            return response;
        }

        throw new KryptonUpdateException("The server redirected too many times.");
    }

    internal static string CreateDownloadPath(string location)
    {
        var name = "package.bin";
        if (Uri.TryCreate(location, UriKind.Absolute, out var uri))
        {
            var leaf = Path.GetFileName(uri.LocalPath);
            if (!string.IsNullOrWhiteSpace(leaf))
            {
                name = leaf;
            }
        }
        else
        {
            var leaf = Path.GetFileName(location);
            if (!string.IsNullOrWhiteSpace(leaf))
            {
                name = leaf;
            }
        }

        foreach (var invalid in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(invalid, '_');
        }

        var directory = Path.Combine(Path.GetTempPath(), "KryptonApplicationUpdater", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        return Path.Combine(directory, name);
    }

    private static string ExtractHelper()
    {
        var assembly = typeof(KryptonApplicationUpdater).Assembly;
        using var stream = assembly.GetManifestResourceStream(HelperResourceName);
        if (stream == null)
        {
            throw new KryptonUpdateException("The update helper is not embedded in Krypton.Toolkit.Utilities. Rebuild the utilities project.");
        }

        var directory = Path.Combine(Path.GetTempPath(), "KryptonApplicationUpdater", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, "Krypton.Toolkit.Utilities.UpdateHelper.exe");
        using (var output = File.Create(path))
        {
            stream.CopyTo(output);
        }

        return path;
    }

    private static string WriteJob(int processId, string zipPath, string destination, string? restartExecutable, string? restartArguments)
    {
        var builder = new StringBuilder();
        builder.Append("{\"processId\":").Append(processId);
        builder.Append(",\"zipPath\":\"").Append(Escape(zipPath)).Append('"');
        builder.Append(",\"destinationDirectory\":\"").Append(Escape(destination)).Append('"');
        builder.Append(",\"restartExecutable\":\"").Append(Escape(restartExecutable ?? string.Empty)).Append('"');
        builder.Append(",\"restartArguments\":\"").Append(Escape(restartArguments ?? string.Empty)).Append('"');
        builder.Append(",\"waitTimeoutSeconds\":120}");
        return builder.ToString();
    }

    private static string Escape(string value)
    {
        var builder = new StringBuilder(value.Length + 8);
        foreach (var character in value)
        {
            switch (character)
            {
                case '\\':
                    builder.Append("\\\\");
                    break;
                case '"':
                    builder.Append("\\\"");
                    break;
                case '\r':
                    builder.Append("\\r");
                    break;
                case '\n':
                    builder.Append("\\n");
                    break;
                default:
                    builder.Append(character);
                    break;
            }
        }

        return builder.ToString();
    }

    private static byte[] ParseSha256(string sha256)
    {
        var hex = (sha256 ?? string.Empty).Trim().Replace(" ", string.Empty);
        if (hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
        {
            hex = hex.Substring(2);
        }

        if (hex.Length != 64)
        {
            throw new KryptonUpdateException("sha256 must be 64 hex characters.");
        }

        var bytes = new byte[32];
        for (var index = 0; index < bytes.Length; index++)
        {
            bytes[index] = Convert.ToByte(hex.Substring(index * 2, 2), 16);
        }

        return bytes;
    }

    private static bool FixedTimeEquals(byte[] left, byte[] right)
    {
        if (left.Length != right.Length)
        {
            return false;
        }

        var difference = 0;
        for (var index = 0; index < left.Length; index++)
        {
            difference |= left[index] ^ right[index];
        }

        return difference == 0;
    }

    private static async Task CopyHttpAsync(string location, string destination, IProgress<KryptonUpdateDownloadProgress>? progress, CancellationToken cancellationToken)
    {
        using var response = await SendAsync(location, null, null, null, cancellationToken).ConfigureAwait(false);
        await CopyBodyAsync(response, destination, 0, response.Content.Headers.ContentLength, progress, cancellationToken).ConfigureAwait(false);
    }

    private static async Task CopyBodyAsync(HttpResponseMessage response, string destination, long existing, long? total, IProgress<KryptonUpdateDownloadProgress>? progress, CancellationToken cancellationToken)
    {
        if (!total.HasValue)
        {
            var length = response.Content.Headers.ContentLength;
            if (length.HasValue)
            {
                total = existing + length.Value;
            }
        }

        if (existing > 0)
        {
            progress?.Report(new KryptonUpdateDownloadProgress(existing, total));
        }

        using (var input = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
        using (var output = new FileStream(destination, existing > 0 ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.None))
        {
            var buffer = new byte[81920];
            var received = existing;
            int read;
            while ((read = await input.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) > 0)
            {
                await output.WriteAsync(buffer, 0, read, cancellationToken).ConfigureAwait(false);
                received += read;
                progress?.Report(new KryptonUpdateDownloadProgress(received, total));
            }
        }
    }

    private static bool IsCompressed(HttpResponseMessage response)
    {
        foreach (var encoding in response.Content.Headers.ContentEncoding)
        {
            if (encoding != null &&
                (encoding.Equals("gzip", StringComparison.OrdinalIgnoreCase) ||
                 encoding.Equals("deflate", StringComparison.OrdinalIgnoreCase) ||
                 encoding.Equals("br", StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
        }

        return false;
    }

    private static bool RangeMatches(HttpResponseMessage response, long existing)
    {
        var range = response.Content.Headers.ContentRange;
        if (range == null)
        {
            return true;
        }

        if (!string.Equals(range.Unit, "bytes", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return !range.From.HasValue || range.From.Value == existing;
    }

    private static long? ReadTotal(HttpResponseMessage response, long existing)
    {
        var range = response.Content.Headers.ContentRange;
        if (range != null && range.Length.HasValue)
        {
            return range.Length.Value;
        }

        var length = response.Content.Headers.ContentLength;
        return length.HasValue ? existing + length.Value : (long?)null;
    }

    private static void TryDelete(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch (Exception)
        {
            // The partial download is left in temp when it cannot be removed.
        }
    }
}
