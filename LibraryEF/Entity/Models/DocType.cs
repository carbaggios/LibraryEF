namespace Entity.Models;

public class DocType : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Reader> Readers { get; set; } = new List<Reader>();
}
