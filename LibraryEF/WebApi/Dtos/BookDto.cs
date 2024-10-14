using Entity.Models;

namespace WebApi.Dtos
{
    public record BookDto(
        Guid Id,
        Guid AuthorId,
        string Name,
        string? PublishingCode,
        Guid? PublishingTypeId,
        int? PublishingYear,
        string? PublishingCountry,
        string? PublishingCity,
        int TermLendDays
        );
}