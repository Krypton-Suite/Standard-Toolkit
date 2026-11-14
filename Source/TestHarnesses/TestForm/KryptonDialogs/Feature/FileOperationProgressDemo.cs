#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege,  KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

using System.IO;

namespace TestForm;

/// <summary>
/// Exercises copy, move, rename, recycle, permanent delete, name conflicts, and a long copy.
/// </summary>
public partial class FileOperationProgressDemo : KryptonForm
{
    private readonly string _root = Path.Combine(Path.GetTempPath(), "KryptonFileOperationDemo");

    /// <summary>Creates the demo and a fresh sandbox.</summary>
    public FileOperationProgressDemo()
    {
        InitializeComponent();
        _btnReset.Click += (sender, args) => ResetSandbox();
        _btnCopy.Click += (sender, args) => CopyFiles();
        _btnMove.Click += (sender, args) => MoveFile();
        _btnRename.Click += (sender, args) => RenameFile();
        _btnRecycle.Click += (sender, args) => RecycleFile();
        _btnPermanent.Click += (sender, args) => DeletePermanently();
        _btnConflict.Click += (sender, args) => CopyOverExisting();
        _btnLarge.Click += (sender, args) => CopyLarge();
        ResetSandbox();
    }

    private void ResetSandbox()
    {
        if (Directory.Exists(_root))
        {
            Directory.Delete(_root, true);
        }

        Directory.CreateDirectory(Path.Combine(_root, "Source", "Album"));
        Directory.CreateDirectory(Path.Combine(_root, "Destination"));
        File.WriteAllText(Path.Combine(_root, "Source", "readme.txt"), "Read me.");
        File.WriteAllText(Path.Combine(_root, "Source", "notes.txt"), "Source notes.");
        File.WriteAllText(Path.Combine(_root, "Source", "Album", "photo.txt"), "Photo.");
        File.WriteAllText(Path.Combine(_root, "Destination", "notes.txt"), "Already in the destination.");
        _lblSandbox.Text = _root;
        Log("Sandbox reset.");
    }

    private void CopyFiles()
    {
        string source = Path.Combine(_root, "Source");
        string destination = Path.Combine(_root, "Destination", "Copied");
        using (KryptonFileOperation operation = CreateOperation())
        {
            operation.QueueCopy(Path.Combine(source, "readme.txt"), destination);
            operation.QueueCopy(Path.Combine(source, "Album"), destination);
            Finish("Copy", operation.Perform());
        }
    }

    private void MoveFile()
    {
        string source = Path.Combine(_root, "Source", "readme.txt");
        if (!File.Exists(source))
        {
            Log("readme.txt is not in Source. Reset the sandbox.");
            return;
        }

        using (KryptonFileOperation operation = CreateOperation())
        {
            Finish("Move", operation.Move(source, Path.Combine(_root, "Destination", "Moved")));
        }
    }

    private void RenameFile()
    {
        string source = Path.Combine(_root, "Source", "notes.txt");
        if (!File.Exists(source))
        {
            Log("notes.txt is not in Source. Reset the sandbox.");
            return;
        }

        using (KryptonFileOperation operation = CreateOperation())
        {
            Finish("Rename", operation.Rename(source, "notes-renamed.txt"));
        }
    }

    private void RecycleFile()
    {
        string source = Path.Combine(_root, "Destination", "notes.txt");
        if (!File.Exists(source))
        {
            File.WriteAllText(source, "Recycle me.");
        }

        using (KryptonFileOperation operation = CreateOperation())
        {
            Finish("Recycle", operation.Delete(source, recycle: true));
        }
    }

    private void DeletePermanently()
    {
        string source = Path.Combine(_root, "Source", "Album", "photo.txt");
        if (!File.Exists(source))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(source)!);
            File.WriteAllText(source, "Delete me.");
        }

        using (KryptonFileOperation operation = CreateOperation())
        {
            Finish("Permanent delete", operation.Delete(source, recycle: false));
        }
    }

    private void CopyOverExisting()
    {
        string source = Path.Combine(_root, "Source", "notes.txt");
        if (!File.Exists(source))
        {
            File.WriteAllText(source, "Source notes.");
        }

        string destination = Path.Combine(_root, "Destination");
        string existing = Path.Combine(destination, "notes.txt");
        if (!File.Exists(existing))
        {
            File.WriteAllText(existing, "Already in the destination.");
        }

        using (KryptonFileOperation operation = CreateOperation())
        {
            Finish("Name conflict", operation.Copy(source, destination, "notes.txt"));
        }
    }

    private void CopyLarge()
    {
        string source = Path.Combine(_root, "Source", "large.bin");
        if (!File.Exists(source))
        {
            Log("Writing a 48 MB file so the progress dialog stays open...");
            var buffer = new byte[1024 * 1024];
            using (var stream = new FileStream(source, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                for (int i = 0; i < 48; i++)
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        string destination = Path.Combine(_root, "Destination", "Large");
        using (KryptonFileOperation operation = CreateOperation())
        {
            operation.QueueCopy(source, destination, "large-1.bin");
            operation.QueueCopy(source, destination, "large-2.bin");
            operation.QueueCopy(source, destination, "large-3.bin");
            Finish("Large copy", operation.Perform());
        }
    }

    private KryptonFileOperation CreateOperation()
    {
        var operation = new KryptonFileOperation { Owner = this };
        operation.Error += (sender, args) => Log("Error " + args.Path + ": " + args.Message);
        return operation;
    }

    private void Finish(string name, bool completed)
    {
        Log(completed ? name + " finished." : name + " was cancelled or reported an error.");
    }

    private void Log(string message)
    {
        _log.AppendText(message + Environment.NewLine);
    }
}
