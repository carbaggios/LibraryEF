namespace Entity.Models;

public class Book : BaseEntity
{
    public Guid AuthorId { get; set; }

    public string Name { get; set; } = null!;

    public string? PublishingCode { get; set; }

    public Guid? PublishingTypeId { get; set; }

    public int? PublishingYear { get; set; }

    public string? PublishingCountry { get; set; }

    public string? PublishingCity { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual PublishingType? PublishingType { get; set; }
}
