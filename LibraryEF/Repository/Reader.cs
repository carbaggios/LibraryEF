namespace Repository;

public class Reader
{
    public Guid Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public Guid? DocTypeId { get; set; }

    public string? DocNumber { get; set; }

    public virtual DocType? DocType { get; set; }
}
