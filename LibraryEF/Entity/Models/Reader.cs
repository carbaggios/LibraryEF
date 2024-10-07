namespace Entity.Models;

public class Reader : BaseUser
{
    public Guid? DocTypeId { get; set; }

    public string? DocNumber { get; set; }

    public virtual DocType? DocType { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

}
