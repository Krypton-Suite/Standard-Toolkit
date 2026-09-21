#region BSD License
/*
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac, Ahmed Abdelhameed, tobitege, KamaniAR, Lesandro Gotardo (aka lesandrog), Jorge A. Avilés (aka mcpbcs) et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Workspace;

internal partial class KryptonWorkspaceCollectionForm
{
    #region Windows Form Designer generated code

    private IContainer components = null!;

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new Container();
        _treeView = new KryptonTreeView();
        _buttonMoveUp = new KryptonButton();
        _buttonMoveDown = new KryptonButton();
        _buttonAddPage = new KryptonButton();
        _buttonAddCell = new KryptonButton();
        _buttonAddSequence = new KryptonButton();
        _buttonDelete = new KryptonButton();
        _propertyGrid = new KryptonPropertyGrid();
        _labelItemProperties = new KryptonLabel();
        _labelWorkspaceCollection = new KryptonLabel();
        kpnlContent = new KryptonPanel();
        kpnlButtonBar = new InternalDesignerEditorButtonBarPanel();
        ((ISupportInitialize)kpnlContent).BeginInit();
        kpnlContent.SuspendLayout();
        SuspendLayout();
        // 
        // treeView
        // 
        _treeView.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom)
                            | AnchorStyles.Left)
                           | AnchorStyles.Right;
        _treeView.Location = new Point(12, 32);
        _treeView.Name = nameof(_treeView);
        _treeView.Size = new Size(251, 339);
        _treeView.TabIndex = 1;
        _treeView.HideSelection = false;
        _treeView.AfterSelect += treeView_AfterSelect;
        // 
        // buttonMoveUp
        // 
        ConfigureToolbarButton(_buttonMoveUp, GeneralImageResources.arrow_up_blue, "Move Up", buttonMoveUp_Click);
        _buttonMoveUp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _buttonMoveUp.Location = new Point(272, 32);
        _buttonMoveUp.Name = nameof(_buttonMoveUp);
        _buttonMoveUp.Size = new Size(95, 28);
        _buttonMoveUp.TabIndex = 2;
        // 
        // buttonMoveDown
        // 
        ConfigureToolbarButton(_buttonMoveDown, GeneralImageResources.arrow_down_blue, "Move Down", buttonMoveDown_Click);
        _buttonMoveDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _buttonMoveDown.Location = new Point(272, 66);
        _buttonMoveDown.Name = nameof(_buttonMoveDown);
        _buttonMoveDown.Size = new Size(95, 28);
        _buttonMoveDown.TabIndex = 3;
        // 
        // buttonDelete
        // 
        ConfigureToolbarButton(_buttonDelete, GeneralImageResources.Delete, "Delete Item", buttonDelete_Click);
        _buttonDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _buttonDelete.Location = new Point(272, 234);
        _buttonDelete.Name = nameof(_buttonDelete);
        _buttonDelete.Size = new Size(95, 28);
        _buttonDelete.TabIndex = 5;
        // 
        // propertyGrid
        // 
        _propertyGrid.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom)
                               | AnchorStyles.Right;
        _propertyGrid.HelpVisible = false;
        _propertyGrid.Location = new Point(376, 32);
        _propertyGrid.Name = nameof(_propertyGrid);
        _propertyGrid.Size = new Size(246, 339);
        _propertyGrid.TabIndex = 7;
        _propertyGrid.ToolbarVisible = false;
        // 
        // labelItemProperties
        // 
        _labelItemProperties.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _labelItemProperties.AutoSize = true;
        _labelItemProperties.Location = new Point(370, 13);
        _labelItemProperties.Name = nameof(_labelItemProperties);
        _labelItemProperties.Size = new Size(81, 13);
        _labelItemProperties.TabIndex = 6;
        _labelItemProperties.Text = "Item Properties";
        // 
        // labelWorkspaceCollection
        // 
        _labelWorkspaceCollection.AutoSize = true;
        _labelWorkspaceCollection.Location = new Point(12, 13);
        _labelWorkspaceCollection.Name = nameof(_labelWorkspaceCollection);
        _labelWorkspaceCollection.Size = new Size(142, 13);
        _labelWorkspaceCollection.TabIndex = 0;
        _labelWorkspaceCollection.Text = "Workspace Collection";
        // 
        // buttonAddPage
        // 
        ConfigureToolbarButton(_buttonAddPage, GeneralImageResources.add, "Page", buttonAddPage_Click);
        _buttonAddPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _buttonAddPage.Location = new Point(272, 114);
        _buttonAddPage.Name = nameof(_buttonAddPage);
        _buttonAddPage.Size = new Size(95, 28);
        _buttonAddPage.TabIndex = 4;
        // 
        // buttonAddCell
        // 
        ConfigureToolbarButton(_buttonAddCell, GeneralImageResources.add, "Cell", buttonAddCell_Click);
        _buttonAddCell.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _buttonAddCell.Location = new Point(272, 148);
        _buttonAddCell.Name = nameof(_buttonAddCell);
        _buttonAddCell.Size = new Size(95, 28);
        _buttonAddCell.TabIndex = 9;
        // 
        // buttonAddSequence
        // 
        ConfigureToolbarButton(_buttonAddSequence, GeneralImageResources.add, "Sequence", buttonAddSequence_Click);
        _buttonAddSequence.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _buttonAddSequence.Location = new Point(272, 182);
        _buttonAddSequence.Name = nameof(_buttonAddSequence);
        _buttonAddSequence.Size = new Size(95, 28);
        _buttonAddSequence.TabIndex = 9;

        var contentLayout = new Panel { Dock = DockStyle.Fill };
        contentLayout.Controls.Add(_treeView);
        contentLayout.Controls.Add(_buttonMoveUp);
        contentLayout.Controls.Add(_buttonMoveDown);
        contentLayout.Controls.Add(_buttonAddPage);
        contentLayout.Controls.Add(_buttonAddCell);
        contentLayout.Controls.Add(_buttonAddSequence);
        contentLayout.Controls.Add(_propertyGrid);
        contentLayout.Controls.Add(_buttonDelete);
        contentLayout.Controls.Add(_labelWorkspaceCollection);
        contentLayout.Controls.Add(_labelItemProperties);

        kpnlContent.Controls.Add(contentLayout);
        kpnlContent.Dock = DockStyle.Fill;
        kpnlContent.Name = "kpnlContent";
        kpnlContent.Padding = new Padding(9);
        kpnlContent.PanelBackStyle = PaletteBackStyle.PanelClient;

        kpnlButtonBar.Dock = DockStyle.Bottom;
        kpnlButtonBar.Name = "kpnlButtonBar";

        // 
        // KryptonWorkspaceCollectionForm
        // 
        AutoScaleDimensions = new SizeF(6F, 13F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(634, 464);
        Controls.Add(kpnlContent);
        Controls.Add(kpnlButtonBar);
        VisibleChanged += OnVisibleChanged;
        Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        MinimumSize = new Size(501, 394);
        Name = nameof(KryptonWorkspaceCollectionForm);
        Text = "Workspace Collection Editor";
        ((ISupportInitialize)kpnlContent).EndInit();
        kpnlContent.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private KryptonTreeView _treeView;
    private KryptonPropertyGrid _propertyGrid;
    private KryptonButton _buttonMoveUp;
    private KryptonButton _buttonMoveDown;
    private KryptonButton _buttonAddPage;
    private KryptonButton _buttonAddCell;
    private KryptonButton _buttonAddSequence;
    private KryptonButton _buttonDelete;
    private KryptonLabel _labelItemProperties;
    private KryptonLabel _labelWorkspaceCollection;
    private KryptonPanel kpnlContent;
    private InternalDesignerEditorButtonBarPanel kpnlButtonBar;
}
