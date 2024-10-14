using Entity.Models;

namespace WebApi.Dtos
{
    public record LendBookDto(
        Guid BookId,
        string Login,
        int TermLendDays
        );
}