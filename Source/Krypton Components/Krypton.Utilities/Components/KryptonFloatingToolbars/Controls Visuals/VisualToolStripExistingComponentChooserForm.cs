#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

public partial class VisualToolStripExistingComponentChooserForm : KryptonForm
{
    #region Variables
    private readonly List<KryptonToolStripPanelExtended?> _srcComponentList = [];
    #endregion

    #region Properties

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control? SourceComponentContainer
    {
        set
        {
            if (value != null)
            {
                foreach (Control item in value.Controls)
                {
                    if (item is KryptonToolStripPanelExtended)
                    {
                        _srcComponentList.Add(item as KryptonToolStripPanelExtended);
                    }
                }

                InitialSettings();
            }
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<KryptonToolStripPanelExtended?> SelectedComponents
    {
        get
        {
            List<KryptonToolStripPanelExtended?> tspe = [];

            if (klbSelected.Items.Count > 0)
            {
                foreach (KryptonToolStripPanelExtended? toolStripPanel in _srcComponentList)
                {
                    if (toolStripPanel != null && klbSelected.Items.Contains(toolStripPanel.Name))
                    {
                        tspe.Add(toolStripPanel);
                    }
                }
            }

            return tspe;
        }
    }
    #endregion

    #region Constructor
    public VisualToolStripExistingComponentChooserForm(List<KryptonToolStripPanelExtended>? panels)
    {
        InitializeComponent();

        if (panels != null)
        {
            foreach (KryptonToolStripPanelExtended item in panels)
            {
                klbSelected.Items.Add(item.Name);
            }
        }
    }
    #endregion

    #region Methods
    private void InitialSettings()
    {
        foreach (KryptonToolStripPanelExtended? toolStripPanel in _srcComponentList)
        {
            if (toolStripPanel != null && !klbSelected.Items.Contains(toolStripPanel.Name))
            {
                klblAvailable.Items.Add(toolStripPanel.Name);
            }
        }
    }
    #endregion

    private void KlblAvailable_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool flag = klblAvailable.SelectedItems.Count > 0;

        kbtnAddSelected.Enabled = flag;
    }

    private void KlbSelected_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool flag = klbSelected.SelectedItems.Count > 0;

        kbtnRemoveSelected.Enabled = flag;
    }

    private void KbtnAddSelected_Click(object sender, EventArgs e)
    {
        if (klblAvailable.SelectedItem != null)
        {
            klbSelected.Items.Add(klblAvailable.SelectedItem);

            klblAvailable.Items.Remove(klblAvailable.SelectedItem);
        }
    }

    private void KbtnAddAll_Click(object sender, EventArgs e)
    {
        object[] allObjects = new string[klblAvailable.Items.Count];

        klblAvailable.Items.CopyTo(allObjects, 0);

        klblAvailable.Items.Clear();

        klbSelected.Items.AddRange(allObjects);
    }

    private void KbtnRemoveSelected_Click(object sender, EventArgs e)
    {
        if (klbSelected.SelectedItem != null)
        {
            klblAvailable.Items.Add(klbSelected.SelectedItem);

            klbSelected.Items.Remove(klbSelected.SelectedItem);
        }
    }

    private void KbtnRemoveAll_Click(object sender, EventArgs e)
    {
        object[] allObjects = new string[klbSelected.Items.Count];

        klbSelected.Items.CopyTo(allObjects, 0);

        klbSelected.Items.Clear();

        klblAvailable.Items.AddRange(allObjects);
    }
}