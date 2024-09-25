namespace Repository;

public class Author
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public DateOnly BirthDay { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
