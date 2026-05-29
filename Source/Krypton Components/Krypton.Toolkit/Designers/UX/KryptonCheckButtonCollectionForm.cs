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

internal partial class KryptonCheckButtonCollectionForm : KryptonForm
{
    #region Type Definitions
    private class ListEntry
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ListEntry class.
        /// </summary>
        /// <param name="checkButton">CheckButton to encapsulate.</param>
        public ListEntry([DisallowNull] KryptonCheckButton checkButton)
        {
            Debug.Assert(checkButton is not null);

            CheckButton = checkButton ?? throw new ArgumentNullException(nameof(checkButton));
        }

        /// <summary>
        /// Gets a string representation of the encapsulated check button.
        /// </summary>
        /// <returns>String instance.</returns>
        public override string ToString() => $"{CheckButton.Site!.Name}  (Text: {CheckButton.Text})";

        #endregion

        #region Public
        /// <summary>
        /// Gets access to the encapsulated check button instance.
        /// </summary>
        public KryptonCheckButton CheckButton { get; }

        #endregion
    }
    #endregion

    #region Instance Fields
    private readonly KryptonCheckSet? _checkSet;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the KryptonCheckButtonCollectionForm class.
    /// </summary>
    public KryptonCheckButtonCollectionForm()
        : this(null)
    {
        SetInheritedControlOverride();
    }

    /// <summary>
    /// Initialize a new instance of the KryptonCheckButtonCollectionForm class.
    /// </summary>
    public KryptonCheckButtonCollectionForm(KryptonCheckSet? checkSet)
    {
        SetInheritedControlOverride();
        // Remember the owning control
        _checkSet = checkSet;

        InitializeComponent();
    }
    #endregion

    #region Implementation
    private void KryptonCheckButtonCollectionForm_Load(object sender, EventArgs e)
    {
        // Get access to the container of the check set
        IContainer container = _checkSet!.Container!;

        // Assuming we manage to find a container
        if (container != null)
        {
            // Find all the check buttons inside the container
            foreach (var obj in container.Components)
            {
                // Cast to the correct type
                // We are only interested in check buttons
                if (obj is KryptonCheckButton checkButton)
                {

                    // Add a new entry to the list box but only check it if 
                    // it is already present in the check buttons collection
                    checkedListBox.Items.Add(new ListEntry(checkButton),
                        _checkSet.CheckButtons.Contains(checkButton));
                }
            }
        }
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
        // Create a copy of the current check set buttons
        var copy = new List<KryptonCheckButton>();
        foreach (KryptonCheckButton checkButton in _checkSet!.CheckButtons)
        {
            copy.Add(checkButton);
        }

        // Process each of the list entries in turn
        for(var i=0; i<checkedListBox.Items.Count; i++)
        {
            // Get access to the encapsulated list box entry
            var entry = (ListEntry)checkedListBox.Items[i];

            // Is this entry checked in the list box?
            if (checkedListBox.GetItemChecked(i))
            {
                // Make sure the check button is in the check set
                if (!_checkSet.CheckButtons.Contains(entry.CheckButton))
                {
                    _checkSet.CheckButtons.Add(entry.CheckButton);
                }
                else
                {
                    copy.Remove(entry.CheckButton);
                }
            }
            else
            {
                // Make sure the check button is not in the check set
                if (_checkSet.CheckButtons.Contains(entry.CheckButton))
                {
                    _checkSet.CheckButtons.Remove(entry.CheckButton);
                    copy.Remove(entry.CheckButton);
                }
            }
        }

        // If there are any dangling references in the checkset that are
        // not in the component list from the list box then remove them
        foreach(KryptonCheckButton checkButton in copy)
        {
            _checkSet.CheckButtons.Remove(checkButton);
        }
    }
    #endregion
}