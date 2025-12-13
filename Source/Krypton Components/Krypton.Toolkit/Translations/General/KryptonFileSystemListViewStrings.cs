#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & tobitege et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace Krypton.Toolkit;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class KryptonFileSystemListViewStrings : Storage
{
    #region Static Strings

    private const string DEFAULT_COLUMN_NAME_NAME = @"Name";
    private const string DEFAULT_COLUMN_TYPE_NAME = @"Type";
    private const string DEFAULT_COLUMN_SIZE_NAME = @"Size";
    private const string DEFAULT_COLUMN_DATE_MODIFIED_NAME = @"Date Modified";
    private const string DEFAULT_COLUMN_DATE_CREATED_NAME = @"Date Created";
    private const string DEFAULT_COLUMN_ATTRIBUTES_NAME = @"Attributes";
    private const string DEFAULT_DIRECTORY_NOT_FOUND_MESSAGE = @"Directory not found";

    #endregion

    #region Identity

    public KryptonFileSystemListViewStrings()
    {
        Reset();
    }

    #endregion

    #region Properties

    /// <summary>Gets or sets the default name for the 'Name' column.</summary>
    /// <value>The default name for the 'Name' column.</value>
    [Category("Visuals")]
    [Description("Gets or sets the default name for the 'Name' column.")]
    [DefaultValue(DEFAULT_COLUMN_NAME_NAME)]
    [Localizable(true)]
    public string ColumnNameName { get; set; }

    /// <summary>Gets or sets the default name for the 'Type' column.</summary>
    /// <value>The default name for the 'Type' column.</value>
    [Category("Visuals")]
    [Description("Gets or sets the default name for the 'Type' column.")]
    [DefaultValue(DEFAULT_COLUMN_TYPE_NAME)]
    [Localizable(true)]
    public string ColumnTypeName { get; set; }

    /// <summary>Gets or sets the default name for the 'Size' column.</summary>
    /// <value>The default name for the 'Size' column.</value>
    [Category("Visuals")]
    [Description("Gets or sets the default name for the 'Size' column.")]
    [DefaultValue(DEFAULT_COLUMN_SIZE_NAME)]
    [Localizable(true)]
    public string ColumnSizeName { get; set; }

    /// <summary>Gets or sets the default name for the 'Date Modified' column.</summary>
    /// <value>The default name for the 'Date Modified' column.</value>
    [Category("Visuals")]
    [Description("Gets or sets the default name for the 'Date Modified' column.")]
    [DefaultValue(DEFAULT_COLUMN_DATE_MODIFIED_NAME)]
    [Localizable(true)]
    public string ColumnDateModifiedName { get; set; }

    /// <summary>Gets or sets the default name for the 'Date Created' column.</summary>
    /// <value>The default name for the 'Date Created' column.</value>
    [Category("Visuals")]
    [Description("Gets or sets the default name for the 'Date Created' column.")]
    [DefaultValue(DEFAULT_COLUMN_DATE_CREATED_NAME)]
    [Localizable(true)]
    public string ColumnDateCreatedName { get; set; }

    /// <summary>Gets or sets the default name for the 'Attributes' column.</summary>
    /// <value>The default name for the 'Attributes' column.</value>
    [Category("Visuals")]
    [Description("Gets or sets the default name for the 'Attributes' column.")]
    [DefaultValue(DEFAULT_COLUMN_ATTRIBUTES_NAME)]
    [Localizable(true)]
    public string ColumnAttributesName { get; set; }

    /// <summary>Gets or sets the message displayed when a directory is not found.</summary>
    /// <value>The message displayed when a directory is not found.</value>
    [Category("Messages")]
    [Description("Gets or sets the message displayed when a directory is not found.")]
    [DefaultValue(DEFAULT_DIRECTORY_NOT_FOUND_MESSAGE)]
    [Localizable(true)]
    public string DirectoryNotFoundMessage { get; set; }

    #endregion

    #region IsDefault

    /// <inheritdoc />
    [Browsable(false)]
    public override bool IsDefault => ColumnNameName.Equals(DEFAULT_COLUMN_NAME_NAME) &&
                                      ColumnTypeName.Equals(DEFAULT_COLUMN_TYPE_NAME) &&
                                      ColumnSizeName.Equals(DEFAULT_COLUMN_SIZE_NAME) &&
                                      ColumnDateModifiedName.Equals(DEFAULT_COLUMN_DATE_MODIFIED_NAME) &&
                                      ColumnDateCreatedName.Equals(DEFAULT_COLUMN_DATE_CREATED_NAME) &&
                                      ColumnAttributesName.Equals(DEFAULT_COLUMN_ATTRIBUTES_NAME) &&
                                      DirectoryNotFoundMessage.Equals(DEFAULT_DIRECTORY_NOT_FOUND_MESSAGE);

    #endregion

    #region Implementation

    /// <inheritdoc />
    public void Reset()
    {
        ColumnNameName = DEFAULT_COLUMN_NAME_NAME;
        ColumnTypeName = DEFAULT_COLUMN_TYPE_NAME;
        ColumnSizeName = DEFAULT_COLUMN_SIZE_NAME;
        ColumnDateModifiedName = DEFAULT_COLUMN_DATE_MODIFIED_NAME;
        ColumnDateCreatedName = DEFAULT_COLUMN_DATE_CREATED_NAME;
        ColumnAttributesName = DEFAULT_COLUMN_ATTRIBUTES_NAME;
        DirectoryNotFoundMessage = DEFAULT_DIRECTORY_NOT_FOUND_MESSAGE;
    }

    #endregion

    #region Overrides

    public override string ToString() => !IsDefault ? "Modified" : GlobalStaticValues.DEFAULT_EMPTY_STRING;

    #endregion
}