using Entity.Models;

namespace WebApi.Dtos
{
    public record AccountDto(Guid Id, string Login, string Email);
}
