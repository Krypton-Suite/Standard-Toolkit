// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Windows.Forms.Design;

internal class KryptonInitialDirectoryEditor : FolderNameEditor
{
    protected override void InitializeDialog(FolderBrowser folderBrowser)
    {
        folderBrowser.Description = //SR.InitialDirectoryEditorLabel;
            @"Select the directory that will initially be opened in the dialog.";
    }
}