#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.IO;
using System.Text;
using System.Xml;

using Krypton.Navigator;
using Krypton.Workspace;

namespace TestForm;

/// <summary>
/// Demo for issue #3959: workspace layout persistence of <see cref="KryptonPage.Tag"/> via TypeConverter,
/// with <see cref="KryptonWorkspace.PageSaving"/> / <see cref="KryptonWorkspace.PageLoading"/> for non-convertible objects.
/// </summary>
public sealed class Feature3959WorkspacePageTagPersistDemo : KryptonForm
{
    private const string UniqueString = @"TagStringPage";
    private const string UniqueInt = @"TagIntPage";
    private const string UniqueCustom = @"TagCustomPage";
    private const string CustomElement = @"CustomTag";

    private readonly KryptonWorkspace _workspace;
    private readonly KryptonCheckBox _chkUsePageEvents;
    private readonly KryptonWrapLabel _lblResults;
    private byte[]? _savedLayout;
    private bool _pageEventsHooked;

    public Feature3959WorkspacePageTagPersistDemo()
    {
        Text = @"Feature #3959 - Workspace Page.Tag Persistence";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(900, 640);
        MinimumSize = new Size(720, 520);

        var lblInfo = new KryptonWrapLabel
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 100,
            Text =
                @"Issue #3959: KryptonWorkspace persists Page.Tag when it is a string or has a TypeConverter that round-trips to/from string (TAG + TAGT attributes)." +
                Environment.NewLine +
                @"Non-convertible objects are skipped unless you handle PageSaving/PageLoading. Steps: Reset Tags → Save Layout → Clear Tags → Load Layout → inspect results."
        };

        _chkUsePageEvents = new KryptonCheckBox
        {
            Dock = DockStyle.Top,
            Text = @"Use PageSaving/PageLoading for the custom Tag object",
            Checked = true,
            Padding = new Padding(8, 4, 8, 4)
        };
        _chkUsePageEvents.CheckedChanged += (_, _) => UpdatePageEventHooks();

        var buttonRow = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            WrapContents = false,
            Padding = new Padding(8, 4, 8, 4)
        };

        buttonRow.Controls.Add(CreateButton(@"Reset Tags", OnResetTagsClick));
        buttonRow.Controls.Add(CreateButton(@"Save Layout", OnSaveClick));
        buttonRow.Controls.Add(CreateButton(@"Clear Tags", OnClearTagsClick));
        buttonRow.Controls.Add(CreateButton(@"Load Layout", OnLoadClick));
        buttonRow.Controls.Add(CreateButton(@"Show Tag Status", OnShowStatusClick));

        _lblResults = new KryptonWrapLabel
        {
            Dock = DockStyle.Bottom,
            AutoSize = false,
            Height = 160,
            Text = @"Results will appear here after Save / Load."
        };

        _workspace = new KryptonWorkspace
        {
            Dock = DockStyle.Fill
        };

        var cell = new KryptonWorkspaceCell();
        cell.Pages.AddRange(new KryptonPage[]
        {
            CreatePage(UniqueString, @"String Tag", @"Sample string tag"),
            CreatePage(UniqueInt, @"Int Tag", 42),
            CreatePage(UniqueCustom, @"Custom Tag", new DemoTagPayload(@"DemoPayload", 7))
        });
        _workspace.Root.Children!.Add(cell);

        Controls.Add(_workspace);
        Controls.Add(_lblResults);
        Controls.Add(buttonRow);
        Controls.Add(_chkUsePageEvents);
        Controls.Add(lblInfo);

        UpdatePageEventHooks();
        ShowTagStatus(@"Initial tags assigned.");
    }

    private void UpdatePageEventHooks()
    {
        if (_chkUsePageEvents.Checked)
        {
            if (!_pageEventsHooked)
            {
                _workspace.PageSaving += OnPageSaving;
                _workspace.PageLoading += OnPageLoading;
                _pageEventsHooked = true;
            }
        }
        else if (_pageEventsHooked)
        {
            _workspace.PageSaving -= OnPageSaving;
            _workspace.PageLoading -= OnPageLoading;
            _pageEventsHooked = false;
        }
    }

    private void OnPageSaving(object? sender, PageSavingEventArgs e)
    {
        if (e.Page.UniqueName != UniqueCustom || e.Page.Tag is not DemoTagPayload payload)
        {
            return;
        }

        e.XmlWriter.WriteStartElement(CustomElement);
        e.XmlWriter.WriteAttributeString(@"Name", payload.Name);
        e.XmlWriter.WriteAttributeString(@"Value", payload.Value.ToString(CultureInfo.InvariantCulture));
        e.XmlWriter.WriteEndElement();
    }

    private void OnPageLoading(object? sender, PageLoadingEventArgs e)
    {
        if (e.Page == null || e.Page.UniqueName != UniqueCustom)
        {
            return;
        }

        XmlReader reader = e.XmlReader;
        if (reader.IsEmptyElement)
        {
            return;
        }

        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == @"CPD")
            {
                break;
            }

            if (reader.NodeType == XmlNodeType.Element && reader.Name == CustomElement)
            {
                string name = reader.GetAttribute(@"Name") ?? string.Empty;
                int value = int.TryParse(reader.GetAttribute(@"Value"), NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsed)
                    ? parsed
                    : 0;
                e.Page.Tag = new DemoTagPayload(name, value);
            }
        }
    }

    private void OnResetTagsClick(object? sender, EventArgs e)
    {
        ApplyKnownTags();
        ShowTagStatus(@"Tags reset to known values.");
    }

    private void OnSaveClick(object? sender, EventArgs e)
    {
        ApplyKnownTags();
        using (var stream = new MemoryStream())
        {
            _workspace.SaveLayoutToStream(stream, Encoding.UTF8);
            _savedLayout = stream.ToArray();
        }

        ShowTagStatus($"Layout saved ({_savedLayout.Length} bytes). TAG/TAGT written for string and int; custom Tag only via PageSaving when checked.");
    }

    private void OnClearTagsClick(object? sender, EventArgs e)
    {
        foreach (KryptonPage page in _workspace.AllPages())
        {
            page.Tag = null;
        }

        ShowTagStatus(@"All page Tags cleared (set to null).");
    }

    private void OnLoadClick(object? sender, EventArgs e)
    {
        if (_savedLayout == null || _savedLayout.Length == 0)
        {
            _lblResults.Text = @"Save a layout first.";
            return;
        }

        using var stream = new MemoryStream(_savedLayout);
        _workspace.LoadLayoutFromStream(stream);
        ShowTagStatus(@"Layout loaded. Expect string + int Tags restored; custom Tag restored only when PageSaving/PageLoading was enabled at save/load.");
    }

    private void OnShowStatusClick(object? sender, EventArgs e) => ShowTagStatus(@"Current Tag status:");

    private void ApplyKnownTags()
    {
        FindPage(UniqueString)!.Tag = @"Sample string tag";
        FindPage(UniqueInt)!.Tag = 42;
        FindPage(UniqueCustom)!.Tag = new DemoTagPayload(@"DemoPayload", 7);
    }

    private void ShowTagStatus(string heading)
    {
        var sb = new StringBuilder();
        sb.AppendLine(heading);
        foreach (KryptonPage page in _workspace.AllPages())
        {
            object? tag = page.Tag;
            string typeName = tag == null ? @"(null)" : tag.GetType().FullName ?? tag.GetType().Name;
            string value = tag switch
            {
                null => @"(null)",
                DemoTagPayload payload => $"{payload.Name}:{payload.Value}",
                _ => Convert.ToString(tag, CultureInfo.InvariantCulture) ?? string.Empty
            };
            sb.AppendLine($"{page.UniqueName}: type={typeName}, value={value}");
        }

        if (_savedLayout != null)
        {
            string xmlPreview = Encoding.UTF8.GetString(_savedLayout);
            bool hasTagt = xmlPreview.Contains(@"TAGT=", StringComparison.Ordinal);
            bool hasCustom = xmlPreview.Contains(CustomElement, StringComparison.Ordinal);
            sb.AppendLine($"Saved XML contains TAGT={hasTagt}, {CustomElement}={hasCustom}");
        }

        _lblResults.Text = sb.ToString();
    }

    private KryptonPage? FindPage(string uniqueName)
    {
        foreach (KryptonPage page in _workspace.AllPages())
        {
            if (page.UniqueName == uniqueName)
            {
                return page;
            }
        }

        return null;
    }

    private static KryptonPage CreatePage(string uniqueName, string text, object? tag)
    {
        var page = new KryptonPage
        {
            UniqueName = uniqueName,
            Text = text,
            TextTitle = text,
            Tag = tag
        };
        page.Controls.Add(new KryptonLabel
        {
            Dock = DockStyle.Fill,
            Text = $@"Page '{text}' — Tag starts as {DescribeTag(tag)}"
        });
        return page;
    }

    private static string DescribeTag(object? tag) =>
        tag switch
        {
            null => @"(null)",
            DemoTagPayload payload => $"DemoTagPayload({payload.Name},{payload.Value})",
            string s => $"string '{s}'",
            _ => $"{tag.GetType().Name}={tag}"
        };

    private static KryptonButton CreateButton(string text, EventHandler onClick)
    {
        var button = new KryptonButton
        {
            Text = text,
            AutoSize = true,
            Margin = new Padding(0, 0, 8, 0)
        };
        button.Click += onClick;
        return button;
    }

    private sealed class DemoTagPayload
    {
        public DemoTagPayload(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public int Value { get; }

        public override string ToString() => $"{Name}:{Value}";
    }
}
