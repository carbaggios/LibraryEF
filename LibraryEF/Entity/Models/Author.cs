namespace Entity.Models;

public class Author : BaseEntity
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public DateOnly BirthDay { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
