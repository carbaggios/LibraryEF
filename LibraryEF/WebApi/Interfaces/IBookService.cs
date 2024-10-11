using WebApi.Dtos;
using Entity.Models;
using System.Threading;

namespace WebApi.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetBooks(CancellationToken cancellationToken);

        Task<List<Book>> FindBooksByName(string filter, CancellationToken cancellationToken);

        Task<List<Book>> GetVacantBooks(CancellationToken cancellationToken);

        Task<string> LendBook(LendBookDto lendBookDto, CancellationToken cancellationToken);

        Task<List<LendBook>> GetLendingList(CancellationToken cancellationToken);
    }
}
