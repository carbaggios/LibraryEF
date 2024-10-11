using WebApi.Interfaces;
using Entity;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace WebApi.Repositories
{
    internal class LendBookRepository : ILendBookRepository
    {
        private readonly LibraryContext _libraryContext;

        public LendBookRepository(LibraryContext libraryContext)
        {
            this._libraryContext = libraryContext;
        }

        public async Task<LendBook> AddLendBook(LendBook lendBook, CancellationToken cancellationToken = default)
        {
            await _libraryContext.AddAsync(lendBook, cancellationToken);
            await _libraryContext.SaveChangesAsync(cancellationToken);
            return lendBook;
        }

        public async Task<LendBook> UpdateLendBook(LendBook lendBook, CancellationToken cancellationToken = default)
        {
            _libraryContext.Update(lendBook);
            await _libraryContext.SaveChangesAsync(cancellationToken);
            return lendBook;
        }

        public async Task<LendBook?> GetLendBook(Guid id, CancellationToken cancellationToken = default)
        {
            return await _libraryContext.LendBooks
                .AsNoTracking()
                .SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<LendBook>> GetLendBooks(CancellationToken cancellationToken)
        {
            return await _libraryContext.LendBooks
                .Include(b => b.Book)
                .ThenInclude(a => a.Author)
                .Include(r => r.Reader)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
