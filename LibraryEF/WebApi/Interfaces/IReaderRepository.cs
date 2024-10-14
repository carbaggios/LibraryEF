using Entity.Models;

namespace WebApi.Interfaces
{
    public interface IReaderRepository
    {
        Task<Reader> AddReader(Reader reader, CancellationToken cancellationToken = default);

        Task<Reader?> GetReader(string login, CancellationToken cancellationToken = default);

        Task<List<Reader>> GetReaders(CancellationToken cancellationToken);
    }
}
