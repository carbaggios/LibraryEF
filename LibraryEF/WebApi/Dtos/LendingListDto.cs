using Entity.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi.Dtos
{
    public record LendingListDto(
        Guid Id,
        Guid BookId,
        string BookName,
        string BookAuthor,
        Guid ReaderId,
        string ReaderLogin,
        int TermLendDays,
        DateOnly TakenDate,
        DateOnly BookedTo,
        DateOnly? ReturnDate
        );
}