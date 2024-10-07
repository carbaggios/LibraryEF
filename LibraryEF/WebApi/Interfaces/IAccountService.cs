using WebApi.Dtos;
using Entity.Models;
using System.Threading;

namespace WebApi.Interfaces
{
    public interface IAccountService
    {
        Task<string> Register(RegisterDto registerDto, CancellationToken cancellationToken);

        Task<string> Login(LoginDto loginDto, CancellationToken cancellationToken);

        //Task<List<UserAccount>> GetUsers(CancellationToken cancellationToken);
    }
}
