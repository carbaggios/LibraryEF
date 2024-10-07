using WebApi.Interfaces;
using Entity;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repositories
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly LibraryContext _libraryContext;

        public AccountRepository(LibraryContext libraryContext)
        {
            this._libraryContext = libraryContext;
        }

        public async Task<Librarian> AddLibrarian(Librarian account, CancellationToken cancellationToken = default)
        {
            await _libraryContext.AddAsync(account, cancellationToken);
            await _libraryContext.SaveChangesAsync(cancellationToken);
            return account;
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
