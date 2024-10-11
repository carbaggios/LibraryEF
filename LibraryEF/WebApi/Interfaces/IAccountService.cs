using WebApi.Dtos;
using Entity.Models;
using System.Threading;

namespace WebApi.Interfaces
{
    public interface IAccountService
    {
        Task<string> RegisterReader(RegisterDto registerDto, CancellationToken cancellationToken);

        Task<string> RegisterLibrarian(RegisterDto registerDto, CancellationToken cancellationToken);

        Task<string> Login(LoginDto loginDto, CancellationToken cancellationToken);

        Task<List<Librarian>> GetLibrarians(CancellationToken cancellationToken);

        Task<List<Reader>> GetReaders(CancellationToken cancellationToken);
    }
}
