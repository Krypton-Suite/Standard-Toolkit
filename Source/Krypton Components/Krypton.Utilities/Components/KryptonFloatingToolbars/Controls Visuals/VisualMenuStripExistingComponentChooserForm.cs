#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), Giduac, et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Utilities;

public partial class VisualMenuStripExistingComponentChooserForm : KryptonForm
{
    #region Instance Fields

    private readonly List<KryptonMenuStripPanelExtended?> _srcComponentList = [];

    #endregion

    #region Public

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control? SourceComponentContainer
    {
        set
        {
            if (value != null)
            {
                foreach (Control item in value.Controls)
                {
                    if (item is KryptonMenuStripPanelExtended)
                    {
                        _srcComponentList.Add(item as KryptonMenuStripPanelExtended);
                    }
                }

                InitialSettings();
            }
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<KryptonMenuStripPanelExtended>? SelectedComponents
    {
        get
        {
            List<KryptonMenuStripPanelExtended> tspe = [];

            if (klbSelected.Items.Count > 0)
            {
                foreach (KryptonMenuStripPanelExtended? toolStripPanel in _srcComponentList)
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

    #region Identity

    public VisualMenuStripExistingComponentChooserForm(List<KryptonMenuStripPanelExtended>? panels)
    {
        InitializeComponent();

        if (panels != null)
        {
            foreach (KryptonMenuStripPanelExtended item in panels)
            {
                klbSelected.Items.Add(item.Name);
            }
        }
    }
    #endregion

    #region Methods
    private void InitialSettings()
    {
        foreach (KryptonMenuStripPanelExtended? menuStripPanel in _srcComponentList)
        {
            if (menuStripPanel != null && !klbSelected.Items.Contains(menuStripPanel.Name))
            {
                klblAvailable.Items.Add(menuStripPanel.Name);
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