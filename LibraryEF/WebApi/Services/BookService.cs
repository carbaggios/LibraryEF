using WebApi.Dtos;
using WebApi.Interfaces;
using WebApi.Repositories;
using Entity.Models;
using System.Net;

namespace WebApi.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILendBookRepository _lendBookRepository;
        private readonly IReaderRepository _readerRepository;

        public BookService(IBookRepository bookRepository, ILendBookRepository lendBookRepository, IReaderRepository readerRepository)
        {
            _bookRepository = bookRepository;
            _lendBookRepository = lendBookRepository;
            _readerRepository = readerRepository;
        }

        public Task<List<Book>> GetBooks(CancellationToken cancellationToken)
        {
            return _bookRepository.GetBooks(cancellationToken);
        }

        public Task<List<Book>> FindBooksByName(string filter, CancellationToken cancellationToken)
        {
            return _bookRepository.FindBooksByName(filter, cancellationToken);
        }

        public Task<List<Book>> GetVacantBooks(CancellationToken cancellationToken)
        {
            return _bookRepository.GetVacantBooks(cancellationToken);
        }

        public async Task<string> LendBook(LendBookDto lendBookDto, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBook(lendBookDto.BookId);
            if (book == null)
            {
                throw new InvalidOperationException("Book doesn't exist.");
            }

            var reader = await _readerRepository.GetReader(lendBookDto.Login);
            if (reader == null)
            {
                throw new InvalidOperationException("Reader doesn't exist.");
            }

            var model = await _lendBookRepository.AddLendBook(
                new LendBook {
                    BookId = book.Id,
                    ReaderId = reader.Id,
                    TermLendDays = lendBookDto.TermLendDays,
                    TakenDate = DateOnly.FromDateTime(DateTime.Now),
                }, cancellationToken);

            return $"Lent book {book.Name} to {model.TakenDate.AddDays(model.TermLendDays)}";
        }

        public async Task<List<LendBook>> GetLendingList(CancellationToken cancellationToken)
        {
            return await _lendBookRepository.GetLendBooks(cancellationToken);
        }
    }
}
