#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, (Version 4.5.0.0) All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2025. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

[ToolboxItem(false)]
[ToolboxBitmap(typeof(KryptonContextMenuComboBox), "ToolboxBitmaps.KryptonComboBox.bmp")]
[DesignerCategory(@"code")]
[DesignTimeVisible(false)]
[DefaultProperty("Text")]
[DefaultEvent("SelectedIndexChanged")]
public class KryptonContextMenuComboBox : KryptonContextMenuItemBase
{
    public override int ItemChildCount { get; }

    public override KryptonContextMenuItemBase? this[int index] => throw new NotImplementedException();

    public override bool ProcessShortcut(Keys keyData) => throw new NotImplementedException();

    public override ViewBase GenerateView(IContextMenuProvider provider, object parent, ViewLayoutStack columns, bool standardStyle,
        bool imageColumn) => throw new NotImplementedException();
}