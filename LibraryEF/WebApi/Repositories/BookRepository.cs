using WebApi.Interfaces;
using Entity;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace WebApi.Repositories
{
    internal class BookRepository : IBookRepository
    {
        private readonly LibraryContext _libraryContext;

        public BookRepository(LibraryContext libraryContext)
        {
            this._libraryContext = libraryContext;
        }

        public async Task<Book> AddBook(Book book, CancellationToken cancellationToken = default)
        {
            await _libraryContext.AddAsync(book, cancellationToken);
            await _libraryContext.SaveChangesAsync(cancellationToken);
            return book;
        }

        public async Task<Book?> GetBook(Guid id, CancellationToken cancellationToken = default)
        {
            return await _libraryContext.Books
                .AsNoTracking()
                .SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Book>> GetBooks(CancellationToken cancellationToken)
        {
            return await _libraryContext.Books
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Book>> FindBooksByName(string filter, CancellationToken cancellationToken)
        {
            return await _libraryContext.Books
                .Where(b => b.Name.ToLower().Contains(filter.ToLower()))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Book>> GetVacantBooks(CancellationToken cancellationToken)
        {
            var lendBooks = _libraryContext.LendBooks;

            return await _libraryContext.Books
                .Where(b => !lendBooks.Any(lb => lb.BookId == b.Id && !lb.ReturnDate.HasValue))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
