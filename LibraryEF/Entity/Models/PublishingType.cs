namespace Entity.Models;

public class PublishingType : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
