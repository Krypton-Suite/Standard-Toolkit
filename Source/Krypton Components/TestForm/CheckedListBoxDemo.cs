namespace TestForm;

public partial class CheckedListBoxDemo : KryptonForm
{
    public CheckedListBoxDemo()
    {
        InitializeComponent();
    }

    private void CheckedListBoxDemo_Load(object sender, EventArgs e)
    {
        List<Person> people = new List<Person>
        {
            new Person { Id = 1, Name = "Alice" },
            new Person { Id = 2, Name = "Bob" },
            new Person { Id = 3, Name = "Charlie" },
            new Person { Id = 4, Name = "Diana" }
        };

        kryptonCheckedListBox1.DataSource = people;

        kryptonCheckedListBox1.DisplayMember = "Name";

        kryptonCheckedListBox1.ValueMember = "Id";
    }

    private void kbtnGetChecked_Click(object sender, EventArgs e)
    {
        var checkedValues = kryptonCheckedListBox1.CheckedItemList;
        klblNotification.Text = $"Checked IDs: {string.Join(", ", checkedValues)}";
    }
}