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
            // Ensure WPF Application is available
            var wpfApp = System.Windows.Application.Current;

            // If WPF Application is not initialized, we cannot proceed with syncing the jump list
            if (wpfApp == null)
            {
                return false;
            }

            // Perform the sync on the WPF UI thread to ensure thread safety
            void DoSync()
            {
                // Create a new WPF JumpList and populate it based on the provided JumpListValues
                var wpfJumpList = new System.Windows.Shell.JumpList
                {
                    ShowFrequentCategory = values.ShowFrequentCategory,
                    ShowRecentCategory = values.ShowRecentCategory
                };

                // Add user tasks to the WPF JumpList
                foreach (var task in values.UserTasks)
                {
                    // Skip tasks that do not have a valid path
                    if (string.IsNullOrEmpty(task.Path))
                    {
                        continue;
                    }

                    // Resolve the application path for the task, handling cases where the path may be a simple executable name
                    var appPath = ResolveExecutablePath(task.Path);

                    // Create a new JumpTask for the WPF JumpList with the appropriate properties set
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

                // Add categorized items to the WPF JumpList
                foreach (var category in values.Categories)
                {
                    // Skip categories that do not have any items
                    foreach (var item in category.Value)
                    {
                        // Skip items that do not have a valid path
                        if (string.IsNullOrEmpty(item.Path))
                        {
                            continue;
                        }

                        // Create a JumpItem for the WPF JumpList, using JumpPath for file paths and JumpTask for executable tasks
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

                // Set the WPF JumpList for the application and apply the changes to update the taskbar jump list
                System.Windows.Shell.JumpList.SetJumpList(wpfApp, wpfJumpList);

                // Apply the jump list to ensure the changes take effect immediately
                wpfJumpList.Apply();
            }

            // Check if we are already on the WPF UI thread; if so, execute the sync directly, otherwise invoke it on the WPF UI thread
            if (wpfApp.Dispatcher.CheckAccess())
            {
                // We are on the WPF UI thread, so we can perform the sync directly
                DoSync();
            }
            else
            {
                // We are not on the WPF UI thread, so we need to invoke the sync on the WPF UI thread to ensure thread safety
                wpfApp.Dispatcher.Invoke(DoSync);
            }

            // If we reached this point without exceptions, the sync was successful
            return true;
        }
        catch
        {
            // If any exceptions occur during the sync process, we catch them and return false to indicate that the sync failed
            return false;
        }
    }

    /// <summary>
    /// Resolves the full path of an executable, handling cases where the path may be a simple executable name without a directory.
    /// </summary>
    /// <param name="path">The original path to resolve.</param>
    /// <returns></returns>
    private static string ResolveExecutablePath(string path)
    {
        // If the path is already rooted or contains directory separators, we assume it's a valid path and return it as is
        if (Path.IsPathRooted(path) || path.IndexOf(Path.DirectorySeparatorChar) >= 0 || path.IndexOf(Path.AltDirectorySeparatorChar) >= 0)
        {
            // The path is already a full path or contains directory information, so we return it without modification
            return path;
        }

        // If the path is not rooted and does not contain directory separators, we attempt to resolve it as an executable in the system directories
        var sysDir = Environment.GetFolderPath(Environment.SpecialFolder.System);

        // First, we check the System directory for the executable
        var sysX86 = Environment.GetFolderPath(Environment.SpecialFolder.SystemX86);

        // Combine the System directory with the provided path to get the full path to the executable
        var fullPath = Path.Combine(sysDir, path);

        // If the executable exists in the System directory, we return that path
        if (File.Exists(fullPath))
        {
            // The executable was found in the System directory, so we return the full path to it
            return fullPath;
        }

        // If the executable was not found in the System directory, we check the SystemX86 directory as well
        fullPath = Path.Combine(sysX86, path);

        // If the executable exists in the SystemX86 directory, we return that path; otherwise, we return the original path as a fallback
        return File.Exists(fullPath) ? fullPath : path;
    }
}
