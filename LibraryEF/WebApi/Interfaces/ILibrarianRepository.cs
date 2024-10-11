using Entity.Models;

namespace WebApi.Interfaces
{
    public interface ILibrarianRepository
    {
        Task<Librarian> AddLibrarian(Librarian librarian, CancellationToken cancellationToken = default);

        Task<Librarian?> GetLibrarian(string login, CancellationToken cancellationToken = default);

        Task<List<Librarian>> GetLibrarians(CancellationToken cancellationToken);
    }
}
