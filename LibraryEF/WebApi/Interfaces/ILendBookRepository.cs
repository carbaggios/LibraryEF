using Entity.Models;

namespace WebApi.Interfaces
{
    public interface ILendBookRepository
    {
        Task<LendBook> AddLendBook(LendBook lendBook, CancellationToken cancellationToken = default);

        Task<LendBook> UpdateLendBook(LendBook lendBook, CancellationToken cancellationToken = default);

        Task<LendBook?> GetLendBook(Guid id, CancellationToken cancellationToken = default);

        Task<List<LendBook>> GetLendBooks(CancellationToken cancellationToken);
    }
}
