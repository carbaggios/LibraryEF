﻿using Entity.Models;

namespace WebApi.Interfaces
{
    public interface IAccountRepository
    {
        Task<Librarian> AddLibrarian(Librarian account, CancellationToken cancellationToken = default);

        Task<Librarian?> GetLibrarian(string login, CancellationToken cancellationToken = default);
        //Task<Reader?> GetReader(string login, CancellationToken cancellationToken = default);

        Task<List<Librarian>> GetLibrarians(CancellationToken cancellationToken);
    }
}
