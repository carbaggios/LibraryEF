using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Interfaces;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class BookController : ControllerBase
    {
        private IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Список всіх книг
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult> GetBooks(CancellationToken cancellationToken)
        {
            try
            {
                var books = await _bookService.GetBooks(cancellationToken);
                return Ok(books.Select(b => 
                    new BookDto(
                        b.Id, 
                        b.AuthorId, 
                        b.Name, 
                        b.PublishingCode, 
                        b.PublishingTypeId, 
                        b.PublishingYear, 
                        b.PublishingCountry, 
                        b.PublishingCity, 
                        b.TermLendDays)
                    ));
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Список всіх книг
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("find")]
        public async Task<ActionResult> FindBooksByName(string filter, CancellationToken cancellationToken)
        {
            try
            {
                var books = await _bookService.FindBooksByName(filter, cancellationToken);
                return Ok(books.Select(b =>
                    new BookDto(
                        b.Id,
                        b.AuthorId,
                        b.Name,
                        b.PublishingCode,
                        b.PublishingTypeId,
                        b.PublishingYear,
                        b.PublishingCountry,
                        b.PublishingCity,
                        b.TermLendDays)
                    ));
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Список вільних книг
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("VacantBooks")]
        public async Task<ActionResult> GetVacantBooks(CancellationToken cancellationToken)
        {
            try
            {
                var books = await _bookService.GetVacantBooks(cancellationToken);
                return Ok(books.Select(b =>
                    new BookDto(
                        b.Id,
                        b.AuthorId,
                        b.Name,
                        b.PublishingCode,
                        b.PublishingTypeId,
                        b.PublishingYear,
                        b.PublishingCountry,
                        b.PublishingCity,
                        b.TermLendDays)
                    ));
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Взяти книгу в користування
        /// </summary>
        /// <param name="lendBookDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("LendBook")]
        public async Task<ActionResult> LendBook([FromBody] LendBookDto lendBookDto, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _bookService.LendBook(lendBookDto, cancellationToken));
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Список книг в користуванні
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("LendingList")]
        public async Task<ActionResult> GetLendingList(CancellationToken cancellationToken)
        {
            try
            {
                var lendBooks = await _bookService.GetLendingList(cancellationToken);

                return Ok(lendBooks.Select(b => new LendingListDto(
                    Id: b.Id,
                    BookId: b.BookId,
                    BookName: b.Book.Name,
                    BookAuthor: string.Join(" ", b.Book.Author.FirstName, b.Book.Author.LastName, b.Book.Author.MiddleName),
                    ReaderId: b.ReaderId,
                    ReaderLogin: b.Reader.Login,
                    TermLendDays: b.TermLendDays,
                    TakenDate: b.TakenDate,
                    BookedTo: b.TakenDate.AddDays(b.TermLendDays),
                    ReturnDate: b.ReturnDate
                    )));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
