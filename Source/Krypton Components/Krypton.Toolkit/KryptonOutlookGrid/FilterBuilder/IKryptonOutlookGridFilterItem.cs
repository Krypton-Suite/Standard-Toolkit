namespace Krypton.Toolkit;

internal interface IKryptonOutlookGridFilterItem
{

    #region Properties

    KryptonOutlookGridFilterItemMenuButton.Items SelectedMenuItem { get; set; }
    string Filter { get; } // The filter string for the object
    string ReadableFilter { get; } // The readable filter for the object
    string Conjunction { get; } // The conjunction following the object
    KryptonOutlookGridFilterField FieldValue { get; set; } // The field for the object

    #endregion Properties

}