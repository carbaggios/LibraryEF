namespace Repository;

public class DocType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Reader> Readers { get; set; } = new List<Reader>();
}
