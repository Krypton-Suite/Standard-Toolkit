#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit.JumpList;

/// <summary>
/// Bridges Krypton <see cref="JumpListValues"/> to WPF <see cref="System.Windows.Shell.JumpList"/> for reliable taskbar jump list support on WinForms.
/// Requires WPF <c>System.Windows.Application</c> to be initialized before use, e.g. in <c>Program.Main</c>: <c>_ = new System.Windows.Application();</c>
/// </summary>
public static class WpfJumpListBridge
{
    /// <summary>
    /// Syncs the given <see cref="JumpListValues"/> to the Windows shell via WPF <see cref="System.Windows.Shell.JumpList"/>.
    /// Returns <c>true</c> if the sync succeeded; <c>false</c> if WPF Application is not available or an error occurred.
    /// </summary>
    /// <param name="values">The jump list configuration to apply.</param>
    /// <returns><c>true</c> if the jump list was applied successfully; otherwise <c>false</c>.</returns>
    public static bool Sync(JumpListValues? values)
    {
        if (values == null)
        {
            return false;
        }

        try
        {
            var wpfApp = System.Windows.Application.Current;
            if (wpfApp == null)
            {
                return false;
            }

            void DoSync()
            {
                var wpfJumpList = new System.Windows.Shell.JumpList
                {
                    ShowFrequentCategory = values.ShowFrequentCategory,
                    ShowRecentCategory = values.ShowRecentCategory
                };

                foreach (var task in values.UserTasks)
                {
                    if (string.IsNullOrEmpty(task.Path))
                    {
                        continue;
                    }

                    var appPath = ResolveExecutablePath(task.Path);

                    wpfJumpList.JumpItems.Add(new System.Windows.Shell.JumpTask
                    {
                        Title = task.Title,
                        ApplicationPath = appPath,
                        Arguments = string.IsNullOrEmpty(task.Arguments) ? " " : task.Arguments,
                        Description = task.Description,
                        IconResourcePath = string.IsNullOrEmpty(task.IconPath) ? appPath : task.IconPath,
                        IconResourceIndex = task.IconIndex,
                        WorkingDirectory = string.IsNullOrEmpty(task.WorkingDirectory) ? null : task.WorkingDirectory
                    });
                }

                foreach (var category in values.Categories)
                {
                    foreach (var item in category.Value)
                    {
                        if (string.IsNullOrEmpty(item.Path))
                        {
                            continue;
                        }

                        System.Windows.Shell.JumpItem jumpItem = File.Exists(item.Path)
                            ? new System.Windows.Shell.JumpPath
                            {
                                Path = item.Path,
                                CustomCategory = category.Key
                            }
                            : new System.Windows.Shell.JumpTask
                            {
                                Title = item.Title,
                                ApplicationPath = item.Path,
                                Arguments = item.Arguments ?? string.Empty,
                                Description = item.Description,
                                IconResourcePath = string.IsNullOrEmpty(item.IconPath) ? item.Path : item.IconPath,
                                IconResourceIndex = item.IconIndex,
                                CustomCategory = category.Key
                            };
                        wpfJumpList.JumpItems.Add(jumpItem);
                    }
                }

                System.Windows.Shell.JumpList.SetJumpList(wpfApp, wpfJumpList);
                wpfJumpList.Apply();
            }

            if (wpfApp.Dispatcher.CheckAccess())
            {
                DoSync();
            }
            else
            {
                wpfApp.Dispatcher.Invoke(DoSync);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    private static string ResolveExecutablePath(string path)
    {
        if (Path.IsPathRooted(path) || path.IndexOf(Path.DirectorySeparatorChar) >= 0 || path.IndexOf(Path.AltDirectorySeparatorChar) >= 0)
        {
            return path;
        }

        var sysDir = Environment.GetFolderPath(Environment.SpecialFolder.System);
        var sysX86 = Environment.GetFolderPath(Environment.SpecialFolder.SystemX86);
        var fullPath = Path.Combine(sysDir, path);
        if (File.Exists(fullPath))
        {
            return fullPath;
        }

        fullPath = Path.Combine(sysX86, path);
        return File.Exists(fullPath) ? fullPath : path;
    }
}
