using Entity.Models;

namespace WebApi.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> AddBook(Book book, CancellationToken cancellationToken = default);

        Task<Book?> GetBook(Guid id, CancellationToken cancellationToken = default);

        Task<List<Book>> GetBooks(CancellationToken cancellationToken);

        Task<List<Book>> FindBooksByName(string filter, CancellationToken cancellationToken);

        Task<List<Book>> GetVacantBooks(CancellationToken cancellationToken);
    }
}
