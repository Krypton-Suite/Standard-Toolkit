#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.Utilities;

/// <summary>
/// Owns the small/large ImageLists. Both lists always hold the same keys in the same order, so a key
/// resolves to one index in either. Image thumbnails load on background threads (2 at a time) and live
/// in an LRU of limited size; other files get their shell icon.
/// </summary>
internal sealed class ThumbnailProvider : IDisposable
{
    private static readonly HashSet<string> ImageExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".tif", ".tiff", ".ico" };
    private const string Placeholder = "placeholder";

    private readonly SynchronizationContext _ui = SynchronizationContext.Current;
    private readonly CancellationTokenSource _cts = new CancellationTokenSource();
    private readonly SemaphoreSlim _throttle = new SemaphoreSlim(2);
    private readonly LinkedList<string> _lru = new LinkedList<string>();
    private readonly Dictionary<string, LinkedListNode<string>> _nodes = new Dictionary<string, LinkedListNode<string>>();
    private readonly HashSet<string> _requested = new HashSet<string>();
    private readonly int _capacity;
    private bool _disposed;

    public ImageList Large { get; }
    public ImageList Small { get; }
    public event Action<FileEntry> Ready;

    public ThumbnailProvider(Size large, Size small, int capacity)
    {
        _capacity = capacity;
        Large = new ImageList { ImageSize = large, ColorDepth = ColorDepth.Depth32Bit };
        Small = new ImageList { ImageSize = small, ColorDepth = ColorDepth.Depth32Bit };

        using (var page = new Bitmap(32, 32, PixelFormat.Format32bppArgb))
        {
            using (Graphics g = Graphics.FromImage(page))
            using (var pen = new Pen(SystemColors.GrayText, 1.5f))
            {
                g.DrawRectangle(pen, 7, 3, 17, 25);
            }

            AddBoth(Placeholder, page, false);
        }
    }

    public int IndexOf(string key) => Small.Images.IndexOfKey(key);

    public string GetKey(FileEntry entry, bool wantThumbnail)
    {
        if (wantThumbnail && ImageExtensions.Contains(entry.Extension))
        {
            string tk = "thumb:" + entry.Path + "|" + entry.Modified.Ticks;
            LinkedListNode<string> node;
            if (_nodes.TryGetValue(tk, out node)) { _lru.Remove(node); _lru.AddFirst(node); return tk; }
            if (_requested.Add(tk))
            {
                Enqueue(entry, tk);
            }
        }
        return IconKey(entry);
    }

    private string IconKey(FileEntry entry)
    {
        string key = "ext:" + (entry.Extension.Length == 0 ? "(none)" : entry.Extension);
        if (Large.Images.ContainsKey(key))
        {
            return key;
        }

        try
        {
            using (Icon icon = Icon.ExtractAssociatedIcon(entry.Path))
            {
                if (icon == null)
                {
                    return Placeholder;
                }

                using (Bitmap bmp = icon.ToBitmap())
                {
                    AddBoth(key, bmp, false);
                }
            }
            return key;
        }
        catch (Exception) { return Placeholder; } // missing file etc.
    }

    private void AddBoth(string key, Bitmap source, bool upscaleLarge)
    {
        using (Bitmap l = Fit(source, Large.ImageSize, upscaleLarge))
        using (Bitmap s = Fit(source, Small.ImageSize, true))
        {
            Large.Images.Add(key, l);
            Small.Images.Add(key, s);
        }
    }

    private void Enqueue(FileEntry entry, string key)
    {
        CancellationToken token = _cts.Token;
        Task.Run(async () =>
        {
            try { await _throttle.WaitAsync(token).ConfigureAwait(false); }
            catch (OperationCanceledException) { return; }

            Bitmap thumb = null;
            try
            {
                token.ThrowIfCancellationRequested();
                using (var fs = new FileStream(entry.Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
                using (Image img = Image.FromStream(fs, false, false))
                {
                    thumb = Fit(img, Large.ImageSize, false);
                }
            }
            catch (Exception) { if (thumb != null) { thumb.Dispose(); } thumb = null; }
            finally { _throttle.Release(); }

            if (thumb == null)
            {
                return;                                  // stays in _requested: no retry
            }

            if (token.IsCancellationRequested) { thumb.Dispose(); return; }
            Post(() => Store(entry, key, thumb));
        });
    }

    private void Store(FileEntry entry, string key, Bitmap thumb)
    {
        using (thumb)
        {
            if (_disposed)
            {
                return;
            }

            Large.Images.Add(key, thumb);
            using (Bitmap s = Fit(thumb, Small.ImageSize, true))
            {
                Small.Images.Add(key, s);
            }
        }
        _nodes[key] = _lru.AddFirst(key);
        while (_lru.Count > _capacity)
        {
            string old = _lru.Last.Value;
            _lru.RemoveLast();
            _nodes.Remove(old);
            _requested.Remove(old); // may be re-requested if scrolled back into view
            Large.Images.RemoveByKey(old);
            Small.Images.RemoveByKey(old);
        }
        Ready?.Invoke(entry);
    }

    private void Post(Action action)
    {
        if (_ui != null)
        {
            _ui.Post(_ => action(), null);
        }
        else
        {
            action();
        }
    }

    private static Bitmap Fit(Image src, Size box, bool upscale)
    {
        var bmp = new Bitmap(box.Width, box.Height, PixelFormat.Format32bppArgb);
        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            float scale = Math.Min((float)box.Width / src.Width, (float)box.Height / src.Height);
            if (!upscale && scale > 1f)
            {
                scale = 1f;
            }

            int w = Math.Max(1, (int)(src.Width * scale)), h = Math.Max(1, (int)(src.Height * scale));
            g.DrawImage(src, (box.Width - w) / 2, (box.Height - h) / 2, w, h);
        }
        return bmp;
    }

    public void Dispose()
    {
        _disposed = true;
        _cts.Cancel();
        _cts.Dispose();
        Large.Dispose();
        Small.Dispose();
    }
}
