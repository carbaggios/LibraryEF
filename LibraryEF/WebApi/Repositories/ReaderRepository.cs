using WebApi.Interfaces;
using Entity;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repositories
{
    internal class ReaderRepository : IReaderRepository
    {
        private readonly LibraryContext _libraryContext;

        public ReaderRepository(LibraryContext libraryContext)
        {
            this._libraryContext = libraryContext;
        }

        public async Task<Reader> AddReader(Reader reader, CancellationToken cancellationToken = default)
        {
            await _libraryContext.AddAsync(reader, cancellationToken);
            await _libraryContext.SaveChangesAsync(cancellationToken);
            return reader;
        }

        public async Task<Reader?> GetReader(string login, CancellationToken cancellationToken = default)
        {
            return await _libraryContext.Readers
                .AsNoTracking()
                .SingleOrDefaultAsync(acc => acc.Login == login);
        }

        public Task<List<Reader>> GetReaders(CancellationToken cancellationToken)
        {
            return _libraryContext.Readers.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
