namespace TestForm;

public class Person
{
    public string Name { get; set; }

    public int Id { get; set; }

    public override string ToString() => Name;
}