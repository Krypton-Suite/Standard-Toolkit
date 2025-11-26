using System;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace MultiSelectionTest
{
    public partial class MultiSelectionTestForm : Form
    {
        public MultiSelectionTestForm()
        {
            InitializeComponent();
            SetupTest();
        }

        private void SetupTest()
        {
            // Add event handlers
            _checkedListBox.SelectedIndexChanged += OnSelectedIndexChanged;
            _checkedListBox.ItemCheck += OnItemCheck;

            // Add mode selection buttons
            var singleModeBtn = new Button
            {
                Text = "Single Mode",
                Location = new System.Drawing.Point(320, 40),
                Size = new System.Drawing.Size(100, 30)
            };
            singleModeBtn.Click += (s, e) => 
            {
                _checkedListBox.SelectionMode = CheckedSelectionMode.One;
                _statusLabel.Text = "Mode: Single Selection";
            };

            var multiSimpleBtn = new Button
            {
                Text = "Multi Simple",
                Location = new System.Drawing.Point(320, 80),
                Size = new System.Drawing.Size(100, 30)
            };
            multiSimpleBtn.Click += (s, e) => 
            {
                _checkedListBox.SelectionMode = CheckedSelectionMode.MultiSimple;
                _statusLabel.Text = "Mode: Multi Simple Selection";
            };

            var multiExtendedBtn = new Button
            {
                Text = "Multi Extended",
                Location = new System.Drawing.Point(320, 120),
                Size = new System.Drawing.Size(100, 30)
            };
            multiExtendedBtn.Click += (s, e) => 
            {
                _checkedListBox.SelectionMode = CheckedSelectionMode.MultiExtended;
                _statusLabel.Text = "Mode: Multi Extended Selection";
            };

            var radioBtn = new Button
            {
                Text = "Radio Mode",
                Location = new System.Drawing.Point(320, 160),
                Size = new System.Drawing.Size(100, 30)
            };
            radioBtn.Click += (s, e) => 
            {
                _checkedListBox.SelectionMode = CheckedSelectionMode.Radio;
                _statusLabel.Text = "Mode: Radio (Single Check)";
            };

            var clearBtn = new Button
            {
                Text = "Clear All",
                Location = new System.Drawing.Point(320, 200),
                Size = new System.Drawing.Size(100, 30)
            };
            clearBtn.Click += (s, e) => 
            {
                _checkedListBox.ClearSelected();
                _checkedListBox.ClearChecked();
                _statusLabel.Text = "Cleared all selections and checks";
            };

            this.Controls.Add(singleModeBtn);
            this.Controls.Add(multiSimpleBtn);
            this.Controls.Add(multiExtendedBtn);
            this.Controls.Add(radioBtn);
            this.Controls.Add(clearBtn);

            _statusLabel.Text = "Mode: Multi Simple Selection (default)";
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCount = _checkedListBox.SelectedIndices.Count;
            var checkedCount = _checkedListBox.CheckedIndices.Count;
            _statusLabel.Text = $"Selected: {selectedCount}, Checked: {checkedCount}";
        }

        private void OnItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Update status when an item's check state changes
            var selectedCount = _checkedListBox.SelectedIndices.Count;
            var checkedCount = _checkedListBox.CheckedIndices.Count;
            _statusLabel.Text = $"Selected: {selectedCount}, Checked: {checkedCount} (Item {e.Index}: {e.NewValue})";
        }
    }

}
