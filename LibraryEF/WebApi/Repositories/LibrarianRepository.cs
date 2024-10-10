using WebApi.Interfaces;
using Entity;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repositories
{
    internal class LibrarianRepository : ILibrarianRepository
    {
        private readonly LibraryContext _libraryContext;

        public LibrarianRepository(LibraryContext libraryContext)
        {
            this._libraryContext = libraryContext;
        }

        public async Task<Librarian> AddLibrarian(Librarian librarian, CancellationToken cancellationToken = default)
        {
            await _libraryContext.AddAsync(librarian, cancellationToken);
            await _libraryContext.SaveChangesAsync(cancellationToken);
            return librarian;
        }

        public async Task<Librarian?> GetLibrarian(string login, CancellationToken cancellationToken = default)
        {
            return await _libraryContext.Librarians
                .AsNoTracking()
                .SingleOrDefaultAsync(acc => acc.Login == login);
        }

        public Task<List<Librarian>> GetLibrarians(CancellationToken cancellationToken)
        {
            return _libraryContext.Librarians.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
