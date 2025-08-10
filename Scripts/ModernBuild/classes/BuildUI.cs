#region BSD License
/*
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), tobitege et al. 2025 - 2025. All rights reserved.
 */
#endregion

namespace Krypton.Build;

public static class BuildUI
{
    private static readonly byte TASKS_AREA_WIDTH = 40;

    internal static Toplevel Create(AppState state)
    {
        var top = new Toplevel
        {
            X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill()
        };
        var win = new Window
        {
            Title = "ModernBuild",
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        top.Add(win);

        var status = new Label
        {
            Text = string.Empty,
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = 1
        };

        var tasksFrame = new FrameView
        {
            Title = "Tasks: Ops",
            X = 0,
            Y = 1,
            Width = TASKS_AREA_WIDTH,
            Height = Dim.Auto(DimAutoStyle.Content)
        };
        var hotkeyScheme = new ColorScheme
        {
            Normal    = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightYellow, Terminal.Gui.Color.Black),
            Focus     = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightYellow, Terminal.Gui.Color.Black),
            HotNormal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightYellow, Terminal.Gui.Color.Black),
            HotFocus  = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightYellow, Terminal.Gui.Color.Black)
        };

        Button MakeHK(string key, int row, Action onClick)
        {
            var b = new Button
            {
                Text = key,
                X = 0,
                Y = row,
                ColorScheme = hotkeyScheme,
                CanFocus = true,
                NoDecorations = true,
                NoPadding = true
            };
            b.Accepting += (_, __) =>
            {
                onClick();
            };
            return b;
        }

        Label MakeText(int row)
        {
            return new Label
            {
                Text = string.Empty,
                X = 4,
                Y = row,
                Width = Dim.Fill(),
                CanFocus = false
            };
        }

        var hk1 = MakeHK("F1", 0, () =>
        {
            state.Channel = BuildLogic.NextChannel(state.Channel);
            state.Configuration = state.Channel == ChannelType.Nightly ? "Debug" : BuildLogic.DefaultConfig(state.Channel);
            state.RequestRenderAll?.Invoke();
        });
        var tx1 = MakeText(0);

        var hk2 = MakeHK("F2", 1, () =>
        {
            if (state.TasksPage == TasksPage.NuGet)
            {
                state.NuGetAction = NextNuGetAction(state.NuGetAction);
            }
            else
            {
                state.Action = NextOpsAction(state.Action);
            }
            state.RequestRenderAll?.Invoke();
        });
        var tx2 = MakeText(1);

        var hk3 = MakeHK("F3", 2, () =>
        {
            state.Configuration = BuildLogic.NextConfig(state.Channel, state.Configuration);
            state.RequestRenderAll?.Invoke();
        });
        var tx3 = MakeText(2);

        var hk4 = MakeHK("F4", 3, () =>
        {
            state.TasksPage = state.TasksPage == TasksPage.Ops ? TasksPage.NuGet : TasksPage.Ops;
            if (state.TasksPage == TasksPage.Ops && state.Action == BuildAction.NuGetTools)
            {
                state.Action = BuildAction.Build;
            }
            if (state.TasksPage == TasksPage.NuGet)
            {
                state.Configuration = "Release";
            }
            state.RequestRenderAll?.Invoke();
        });
        var tx4 = MakeText(3);

        var hk5 = MakeHK("F5", 4, () =>
        {
            if (state.IsRunning)
            {
                BuildLogic.StopBuild(state);
            }
            else
            {
                BuildLogic.StartBuild(state);
            }
        });
        var tx5 = MakeText(4);

        var hk6 = MakeHK("F6", 5, () =>
        {
            if (state.TasksPage == TasksPage.NuGet)
            {
                state.NuGetIncludeSymbols = !state.NuGetIncludeSymbols;
            }
            else
            {
                BuildLogic.CycleTailSize(state);
            }
            state.RequestRenderAll?.Invoke();
        });
        var tx6 = MakeText(5);

        var hk7 = MakeHK("F7", 6, () =>
        {
            if (state.TasksPage == TasksPage.NuGet)
            {
                state.NuGetSkipDuplicate = !state.NuGetSkipDuplicate;
            }
            else
            {
                _ = BuildLogic.StartCleanAsync(state);
            }
        });
        var tx7 = MakeText(6);

        Action? clearOutput = null;
        var hk8 = MakeHK("F8", 7, () =>
        {
            if (state.TasksPage == TasksPage.NuGet)
            {
                state.NuGetSource = NextNuGetSource(state.NuGetSource);
            }
            else
            {
                clearOutput?.Invoke();
            }
        });
        var tx8 = MakeText(7);

        var hk9 = MakeHK("F9", 8, () =>
        {
            state.PackMode = BuildLogic.NextPackMode(state.PackMode);
            state.RequestRenderAll?.Invoke();
        });
        var tx9 = MakeText(8);
        var hkEsc = MakeHK("ESC", 9, () =>
        {
            Application.RequestStop();
        });
        var txEsc = MakeText(9);
        var spacerAfterEsc = new Label
        {
            Text = string.Empty,
            X = 0,
            Y = 10,
            Width = Dim.Fill(),
            Height = 1,
            CanFocus = false
        };
        var hkTest = MakeHK("TEST", 11, () =>
        {
            BuildLogic.PreviewNuGetCommands(state);
        });
        var txTest = MakeText(11);
        var hint = new Label
        {
            Text = string.Empty,
            X = 0,
            Y = 12,
            Width = Dim.Fill(),
            CanFocus = false
        };

        var autoScrollCb = new CheckBox
        {
            Text = "Auto-Scroll",
            X = 0,
            Y = 16,
            AllowCheckStateNone = false,
            CheckedState = state.AutoScroll ? CheckState.Checked : CheckState.UnChecked
        };

        autoScrollCb.CheckedStateChanged += (s, e) =>
        {
            state.AutoScroll = autoScrollCb.CheckedState == CheckState.Checked;
        };

        // Initially hide TEST, will be shown on NuGet page during RenderTasks
        hkTest.Visible = false;
        txTest.Visible = false;
        tasksFrame.Add(hk1, tx1, hk2, tx2, hk3, tx3, hk4, tx4, hk5, tx5, hk6, tx6, hk7, tx7, hk8, tx8, hk9, tx9,
                       hkEsc, txEsc, spacerAfterEsc, hkTest, txTest, hint, autoScrollCb);
        tasksFrame.Height = Dim.Func(() =>
        {
            return autoScrollCb.Frame.Y + autoScrollCb.Frame.Height + 2;
        });
        win.Add(tasksFrame);

        var overviewFrame = new FrameView
        {
            Title = "Build Settings",
            X = 0,
            Y = Pos.Bottom(tasksFrame),
            Width = TASKS_AREA_WIDTH,
            Height = (Dim.Fill()! - 14)
        };
        var overview = new TextView
        {
            ReadOnly = true,
            WordWrap = false,
            CanFocus = true,
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        overview.VerticalScrollBar.AutoShow = true;
        overviewFrame.Add(overview);
        win.Add(overviewFrame);

        var outputFrame = new FrameView
        {
            Title = "Live Output",
            X = Pos.Right(tasksFrame),
            Y = 1,
            Width = Dim.Fill(),
            Height = (Dim.Fill()! - 14)
        };
        var logLines = new ObservableCollection<string>();
        var outputList = new ListView
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            CanFocus = true,
            AllowsMarking = false,
            AllowsMultipleSelection = false
        };
        outputList.SetSource(logLines);
        outputList.VerticalScrollBar.AutoShow = true;
        outputList.HorizontalScrollBar.AutoShow = true;

        clearOutput = () =>
        {
            state.Tail.Clear();
            logLines.Clear();
        };
        outputList.OpenSelectedItem += (s, a) =>
        {
            try
            {
                if (Terminal.Gui.Clipboard.IsSupported && outputList.SelectedItem >= 0 && outputList.SelectedItem < logLines.Count)
                {
                    Terminal.Gui.Clipboard.Contents = logLines[outputList.SelectedItem];
                    state.OnOutput?.Invoke("Copied line to clipboard.");
                }
            }
            catch
            {
            }
        };
        outputFrame.Add(outputList);
        win.Add(outputFrame);

        var statusSpacerTop = new Label
        {
            Text = string.Empty,
            X = 0,
            Y = Pos.Bottom(outputFrame),
            Width = Dim.Fill(),
            Height = 1,
            CanFocus = false
        };
        status.Y = Pos.Bottom(statusSpacerTop);

        var statusSpacerBottom = new Label
        {
            Text = string.Empty,
            X = 0,
            Y = Pos.Bottom(status),
            Width = Dim.Fill(),
            Height = 1,
            CanFocus = false
        };

        var summaryFrame = new FrameView
        {
            Title = "Summary",
            X = 0,
            Y = Pos.Bottom(statusSpacerBottom),
            Width = Dim.Fill(),
            Height = 11
        };
        var summary = new TextView
        {
            ReadOnly = true,
            WordWrap = true,
            CanFocus = true,
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        summary.VerticalScrollBar.AutoShow = true;
        summaryFrame.Add(summary);
        win.Add(statusSpacerTop, status, statusSpacerBottom, summaryFrame);

        var panelScheme = new ColorScheme
        {
            Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.Gray, Terminal.Gui.Color.Black),
            Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.White, Terminal.Gui.Color.Black),
            HotNormal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightYellow, Terminal.Gui.Color.Black),
            HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightYellow, Terminal.Gui.Color.Black)
        };
        var nugetScheme = new ColorScheme
        {
            Normal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.Black),
            Focus = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightRed, Terminal.Gui.Color.Black),
            HotNormal = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightYellow, Terminal.Gui.Color.Black),
            HotFocus = new Terminal.Gui.Attribute(Terminal.Gui.Color.BrightYellow, Terminal.Gui.Color.Black)
        };

        tasksFrame.ColorScheme = panelScheme;
        overviewFrame.ColorScheme = panelScheme;
        outputFrame.ColorScheme = panelScheme;
        summaryFrame.ColorScheme = panelScheme;

        state.OnOutput = line => Application.Invoke(() =>
        {
            var viewport = outputList.Viewport;
            int currentCount = logLines.Count;
            int bottomVisible = viewport.Y + viewport.Height - 1;
            bool userScrolledUp = bottomVisible < (currentCount - 1);
            logLines.Add(Colorize(line));
            if (state.AutoScroll || !userScrolledUp)
            {
                outputList.MoveEnd();
            }
            RenderStatus(state, new UIContext { Status = status });
            RenderSummary(state, new UIContext { Summary = summary });
        });

        var ui = new UIContext
        {
            Status = status,
            TasksFrame = tasksFrame,
            SummaryFrame = summaryFrame,
            PanelScheme = panelScheme,
            NugetScheme = nugetScheme,
            Tx1 = tx1,
            Tx2 = tx2,
            Tx3 = tx3,
            Tx4 = tx4,
            Tx5 = tx5,
            Tx6 = tx6,
            Tx7 = tx7,
            Tx8 = tx8,
            Tx9 = tx9,
            TxEsc = txEsc,
            TestBtn = hkTest,
            TxTest = txTest,
            Hint = hint,
            Overview = overview,
            Summary = summary,
            OutputList = outputList
        };

        var tick = new System.Timers.Timer(1000);
        tick.AutoReset = true;
        tick.Elapsed += (_, __) =>
        {
            Application.Invoke(() =>
            {
                RenderStatus(state, ui);
            });
        };
        tick.Start();

        state.RequestRenderAll = () => Application.Invoke(() =>
        {
            RenderStatus(state, ui);
            RenderTasks(state, ui);
            RenderOverview(state, ui);
            RenderSummary(state, ui);
        });

        if (state.Channel == ChannelType.Nightly)
        {
            state.Configuration = "Debug";
        }
        RenderAll(state, ui);
        top.KeyDown += (object? _, Key key) =>
        {
            switch (key.KeyCode)
            {
                case KeyCode.F1:
                {
                    state.Channel = BuildLogic.NextChannel(state.Channel);
                    state.Configuration = state.Channel == ChannelType.Nightly ? "Debug" : BuildLogic.DefaultConfig(state.Channel);
                    RenderAll(state, ui);
                    break;
                }
                case KeyCode.F2:
                {
                    if (state.TasksPage == TasksPage.NuGet)
                    {
                        state.NuGetAction = NextNuGetAction(state.NuGetAction);
                    }
                    else
                    {
                        state.Action = NextOpsAction(state.Action);
                    }
                    RenderAll(state, ui);
                    break;
                }
                case KeyCode.F3:
                {
                    state.Configuration = BuildLogic.NextConfig(state.Channel, state.Configuration);
                    RenderAll(state, ui);
                    break;
                }
                case KeyCode.F4:
                {
                    state.TasksPage = state.TasksPage == TasksPage.Ops ? TasksPage.NuGet : TasksPage.Ops;
                    if (state.TasksPage == TasksPage.Ops && state.Action == BuildAction.NuGetTools)
                    {
                        state.Action = BuildAction.Build;
                    }
                    if (state.TasksPage == TasksPage.NuGet)
                    {
                        state.Configuration = "Release";
                    }
                    RenderAll(state, ui);
                    break;
                }
                case KeyCode.F5:
                {
                    if (state.IsRunning)
                    {
                        BuildLogic.StopBuild(state);
                    }
                    else
                    {
                        BuildLogic.StartBuild(state);
                    }
                    break;
                }
                case KeyCode.F6:
                {
                    if (state.TasksPage == TasksPage.NuGet)
                    {
                        state.NuGetIncludeSymbols = !state.NuGetIncludeSymbols;
                        RenderAll(state, ui);
                    }
                    else
                    {
                        BuildLogic.CycleTailSize(state);
                        RenderAll(state, ui);
                    }
                    break;
                }
                case KeyCode.F7:
                {
                    if (state.TasksPage == TasksPage.NuGet)
                    {
                        state.NuGetSkipDuplicate = !state.NuGetSkipDuplicate;
                        RenderAll(state, ui);
                    }
                    else
                    {
                        _ = BuildLogic.StartCleanAsync(state);
                    }
                    break;
                }
                case KeyCode.F8:
                {
                    if (state.TasksPage == TasksPage.NuGet)
                    {
                        state.NuGetSource = NextNuGetSource(state.NuGetSource);
                        RenderAll(state, ui);
                    }
                    else
                    {
                        state.Tail.Clear();
                        logLines.Clear();
                        if (outputList.Viewport.X < 0)
                        {
                            outputList.Viewport = outputList.Viewport with { X = 0 };
                        }
                    }
                    break;
                }
                case KeyCode.F9:
                {
                    state.PackMode = BuildLogic.NextPackMode(state.PackMode);
                    RenderAll(state, ui);
                    break;
                }
                case KeyCode.PageUp:
                {
                    if (ui.Summary != null && ui.Summary.HasFocus)
                    {
                        // let TextView handle
                        break;
                    }
                    state.SummaryOffset += 20;
                    RenderSummary(state, ui);
                    break;
                }
                case KeyCode.PageDown:
                {
                    if (ui.Summary != null && ui.Summary.HasFocus)
                    {
                        // let TextView handle
                        break;
                    }
                    state.SummaryOffset = Math.Max(0, state.SummaryOffset - 20);
                    RenderSummary(state, ui);
                    break;
                }
                case KeyCode.Home:
                {
                    if (ui.Summary != null && ui.Summary.HasFocus)
                    {
                        // let TextView handle
                        break;
                    }
                    state.SummaryOffset = int.MaxValue;
                    RenderSummary(state, ui);
                    break;
                }
                case KeyCode.End:
                {
                    if (ui.Summary != null && ui.Summary.HasFocus)
                    {
                        // let TextView handle
                        break;
                    }
                    state.SummaryOffset = 0;
                    RenderSummary(state, ui);
                    break;
                }
                case KeyCode.Esc:
                {
                    Application.RequestStop();
                    break;
                }
                case KeyCode.F10:
                {
                    Application.RequestStop();
                    break;
                }
            }
        };

        return top;
    }

    internal sealed class UIContext
    {
        public FrameView? TasksFrame { get; set; }
        public FrameView? SummaryFrame { get; set; }
        public ColorScheme? PanelScheme { get; set; }
        public ColorScheme? NugetScheme { get; set; }
        public Label? Status { get; set; }
        public Label? Tx1 { get; set; }
        public Label? Tx2 { get; set; }
        public Label? Tx3 { get; set; }
        public Label? Tx4 { get; set; }
        public Label? Tx5 { get; set; }
        public Label? Tx6 { get; set; }
        public Label? Tx7 { get; set; }
        public Label? Tx8 { get; set; }
        public Label? Tx9 { get; set; }
        public Label? TxEsc { get; set; }
        public Button? TestBtn { get; set; }
        public Label? TxTest { get; set; }
        public Label? Hint { get; set; }
        public TextView? Overview { get; set; }
        public TextView? Summary { get; set; }
        public ListView? OutputList { get; set; }
    }

    private static void RenderAll(AppState state, UIContext ui)
    {
        BuildLogic.EnsurePaths(state);
        RenderStatus(state, ui);
        RenderTasks(state, ui);
        RenderOverview(state, ui);
        RenderSummary(state, ui);
    }

    private static BuildAction NextOpsAction(BuildAction action)
    {
        // Same cycle as BuildLogic.NextAction but skipping NuGetTools
        return action switch
        {
            BuildAction.Build       => BuildAction.Rebuild,
            BuildAction.Rebuild     => BuildAction.Pack,
            BuildAction.Pack        => BuildAction.BuildPack,
            BuildAction.BuildPack   => BuildAction.Debug,
            BuildAction.Debug       => BuildAction.Installer,
            BuildAction.NuGetTools  => BuildAction.Build, // sanitize if ever present
            BuildAction.Installer   => BuildAction.Build,
            _                       => BuildAction.Build
        };
    }

    private static NuGetAction NextNuGetAction(NuGetAction action)
    {
        return action switch
        {
            NuGetAction.RebuildPack   => NuGetAction.Push,
            NuGetAction.Push          => NuGetAction.PackPush,
            NuGetAction.PackPush      => NuGetAction.BuildPackPush,
            NuGetAction.BuildPackPush => NuGetAction.Tools,
            _                         => NuGetAction.RebuildPack
        };
    }

    private static string FormatNuGetAction(NuGetAction action)
    {
        return action switch
        {
            NuGetAction.RebuildPack     => "Rebuild+Pack",
            NuGetAction.Push            => "Push",
            NuGetAction.PackPush        => "Pack+Push",
            NuGetAction.BuildPackPush   => "Rebuild+Pack+Push",
            NuGetAction.Tools           => "Update NuGet",
            _                           => action.ToString()
        };
    }

    private static NuGetSource NextNuGetSource(NuGetSource source)
    {
        return source switch
        {
            NuGetSource.Default     => NuGetSource.NuGetOrg,
            NuGetSource.NuGetOrg    => NuGetSource.GitHub,
            NuGetSource.GitHub      => NuGetSource.Custom,
            _                       => NuGetSource.Default
        };
    }

    private static string FormatNuGetSource(NuGetSource source, string custom)
    {
        return source switch
        {
            NuGetSource.Default     => "Default",
            NuGetSource.NuGetOrg    => "NuGet.org",
            NuGetSource.GitHub      => "GitHub",
            NuGetSource.Custom      => string.IsNullOrWhiteSpace(custom) ? "Custom (unset)" : custom,
            _                       => source.ToString()
        };
    }

    private static void RenderStatus(AppState state, UIContext ui)
    {
        string st = state.IsRunning ? "RUNNING" : (state.LastExitCode == 0 ? "DONE" : "IDLE");
        if (!state.IsRunning && state.LastExitCode != 0 && state.LastExitCode != int.MinValue)
        {
            st = "FAILED";
        }
        TimeSpan? elapsed = state.IsRunning && state.StartTimeUtc.HasValue ? DateTime.UtcNow - state.StartTimeUtc.Value : (TimeSpan?)null;
        string elapsedText = elapsed.HasValue ? elapsed.Value.ToString("hh\\:mm\\:ss") : "--:--:--";
        if (ui.SummaryFrame != null)
        {
            string ts = DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss zzz");
            ui.SummaryFrame.Title = $"Summary — {ts}";
        }
        if (ui.Status != null)
        {
            ui.Status.Text = $"{st}   Elapsed: {elapsedText}   Errors: {state.ErrorCount}   Warnings: {state.WarningCount}";
        }
    }

        private static void RenderTasks(AppState state, UIContext ui)
    {
        if (state.TasksPage == TasksPage.Ops)
        {
            ChannelType nextChannel = BuildLogic.NextChannel(state.Channel);
            BuildAction nextAction = NextOpsAction(state.Action);
            string FormatAction(BuildAction a)
            {
                return a == BuildAction.Installer ? "Installer" : a.ToString();
            }
            if (ui.TasksFrame != null)
            {
                ui.TasksFrame.Title = "Tasks: Ops";
                if (ui.PanelScheme != null)
                {
                    ui.TasksFrame.ColorScheme = ui.PanelScheme;
                }
            }
            string nextConfig = BuildLogic.NextConfig(state.Channel, state.Configuration);
            if (ui.Tx1 != null)
            {
                ui.Tx1.Text = $"Channel   {state.Channel} (next: {nextChannel})";
            }
            if (ui.Tx2 != null)
            {
                ui.Tx2.Text = $"Action    {FormatAction(state.Action)} (next: {FormatAction(nextAction)})";
            }
            if (ui.Tx3 != null)
            {
                ui.Tx3.Text = $"Config    {state.Configuration} (next: {nextConfig})";
            }
            if (ui.Tx4 != null)
            {
                ui.Tx4.Text = "Page      Ops (F4 to switch)";
            }
            if (ui.Tx6 != null) ui.Tx6.Text = $"Tail      {state.TailLines}";
            if (ui.Tx5 != null)
            {
                ui.Tx5.Text = $"Run/Stop  {(state.IsRunning ? "Stop" : "Run")}";
            }
            if (ui.Tx7 != null) ui.Tx7.Text = "Clean     Delete Bin/obj/Logs";
            if (ui.Tx8 != null) ui.Tx8.Text = "Clear     Clear live output";
            if (ui.TxEsc != null)
            {
                ui.TxEsc.Text = "Exit      Exit application";
            }
            bool showF9 = (state.Channel == ChannelType.Stable) &&
                          (state.Action == BuildAction.Pack || state.Action == BuildAction.BuildPack);
            if (showF9)
            {
                PackMode nextPack = BuildLogic.NextPackMode(state.PackMode);
                if (ui.Tx9 != null)
                {
                    ui.Tx9.Text = $"PackMode  {state.PackMode} (next: {nextPack})";
                }
            }
            else
            {
                if (ui.Tx9 != null) { ui.Tx9.Text = "PackMode  (Stable only)"; }
            }
            if (ui.TestBtn != null) ui.TestBtn.Visible = false;
            if (ui.TxTest != null) { ui.TxTest.Visible = false; ui.TxTest.Text = string.Empty; }
            if (ui.Hint != null)
            {
                ui.Hint.Text = "F-keys cycle options.\nF5 toggles Run/Stop.";
            }
        }
        else
        {
            // NuGet page: keep channel/config visible, show NuGet-specific actions
            ChannelType nextChannel = BuildLogic.NextChannel(state.Channel);
            string nextConfig = BuildLogic.NextConfig(state.Channel, state.Configuration);
            if (ui.TasksFrame != null)
            {
                ui.TasksFrame.Title = "Tasks: NuGet";
                if (ui.NugetScheme != null)
                {
                    ui.TasksFrame.ColorScheme = ui.NugetScheme;
                }
            }
            if (ui.Tx1 != null)
            {
                ui.Tx1.Text = $"Channel   {state.Channel} (next: {nextChannel})";
            }
            if (ui.Tx2 != null)
            {
                ui.Tx2.Text = $"Action    {FormatNuGetAction(state.NuGetAction)}";
            }
            if (ui.Tx3 != null)
            {
                ui.Tx3.Text = $"Config    {state.Configuration} (next: {nextConfig})";
            }
            if (ui.Tx4 != null)
            {
                ui.Tx4.Text = "Page      NuGet (F4 to switch)";
            }
            if (ui.Tx5 != null)
            {
                ui.Tx5.Text = $"Run/Stop  {(state.IsRunning ? "Stop" : "Run")}";
            }
            if (ui.Tx6 != null) ui.Tx6.Text = $"Symbols   {(state.NuGetIncludeSymbols ? "Yes" : "No")}";
            if (ui.Tx7 != null) ui.Tx7.Text = $"SkipDup   {(state.NuGetSkipDuplicate ? "Yes" : "No")}";
            if (ui.Tx8 != null)
            {
                string src = FormatNuGetSource(state.NuGetSource, state.NuGetCustomSource);
                ui.Tx8.Text = $"Source    {src}";
            }
            if (ui.TestBtn != null) ui.TestBtn.Visible = true;
            if (ui.TxTest != null) { ui.TxTest.Visible = true; ui.TxTest.Text = "Test      Preview commands"; }
            if (ui.Tx9 != null) { ui.Tx9.Text = "PackMode  (Stable only)"; }
            if (ui.TxEsc != null)
            {
                ui.TxEsc.Text = "Exit      Exit application";
            }
            if (ui.Hint != null)
            {
                ui.Hint.Text = "NuGet page:\nF2 cycles NuGet actions.\nF5 runs the selected action(s).";
            }
        }
    }

    private static void RenderOverview(AppState state, UIContext ui)
    {
        string prefix = state.Channel.ToString().ToLowerInvariant();
        var sb = new StringBuilder();

        int CalcWrapWidth()
        {
            // Use the fixed layout width for the left column (Build Settings)
            int width = TASKS_AREA_WIDTH - 4; // usable row width as requested
            return Math.Max(10, width);
        }

        void AddKV(string label, string value)
        {
            sb.AppendLine(label);
            int max = CalcWrapWidth();
            int start = 0;
            while (start < value.Length)
            {
                int len = Math.Min(max, value.Length - start);
                string chunk = value.Substring(start, len);
                sb.AppendLine("  " + chunk);
                start += len;
            }
        }

        string TrimScriptsPath(string fullPath)
        {
            char sep = Path.DirectorySeparatorChar;
            string pattern = $"{sep}Scripts{sep}";
            int idx = fullPath.IndexOf(pattern, StringComparison.OrdinalIgnoreCase);
            if (idx < 0 && Path.DirectorySeparatorChar != Path.AltDirectorySeparatorChar)
            {
                string altPattern = $"{Path.AltDirectorySeparatorChar}Scripts{Path.AltDirectorySeparatorChar}";
                idx = fullPath.IndexOf(altPattern, StringComparison.OrdinalIgnoreCase);
            }
            if (idx >= 0)
            {
                return fullPath.Substring(idx + 1);
            }
            return fullPath;
        }

        string TrimLogsPath(string fullPath)
        {
            char sep = Path.DirectorySeparatorChar;
            string pattern = $"{sep}Logs{sep}";
            int idx = fullPath.IndexOf(pattern, StringComparison.OrdinalIgnoreCase);
            if (idx < 0 && Path.DirectorySeparatorChar != Path.AltDirectorySeparatorChar)
            {
                string altPattern = $"{Path.AltDirectorySeparatorChar}Logs{Path.AltDirectorySeparatorChar}";
                idx = fullPath.IndexOf(altPattern, StringComparison.OrdinalIgnoreCase);
            }
            if (idx >= 0)
            {
                return fullPath.Substring(idx + 1);
            }
            return fullPath;
        }

        AddKV("Project", TrimScriptsPath(state.ProjectFile));
        AddKV("MSBuild", state.MsBuildPath);

        AddKV("Text Log", TrimLogsPath(Path.Combine(state.RootPath, "Logs", prefix + "-build-summary.log")));
        AddKV("BinLog", TrimLogsPath(Path.Combine(state.RootPath, "Logs", prefix + "-build.binlog")));
        if (ui.Overview != null)
        {
            ui.Overview.Text = sb.ToString();
        }
    }

    private static void RenderSummary(AppState state, UIContext ui)
    {
        if (ui.Summary == null)
        {
            return;
        }
        if (!state.SummaryReady)
        {
            ui.Summary.Text = "Summary will appear here after the build finishes.";
            return;
        }
        IReadOnlyList<string> all = state.SummaryLines ?? Array.Empty<string>();
        int total = all.Count;
        int pageSize = 20;
        int maxOffset = Math.Max(0, total - pageSize);
        state.SummaryOffset = Math.Clamp(state.SummaryOffset, 0, maxOffset);
        int start = Math.Max(0, total - pageSize - state.SummaryOffset);
        int end = Math.Min(total, start + pageSize);
        var sb = new StringBuilder();
        for (int i = start; i < end; i++)
        {
            sb.AppendLine(all[i]);
        }
        ui.Summary.Text = sb.ToString();
        ui.Summary.MoveEnd();
    }

    private static string Colorize(string line)
    {
        string text = line.Replace("[red]", string.Empty).Replace("[/]", string.Empty).Replace("[yellow]", string.Empty);
        return text.Replace("     ", "  ");
    }
}
