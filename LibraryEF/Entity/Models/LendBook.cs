namespace Entity.Models;

/// <summary>
/// Позичені книги
/// </summary>
public class LendBook : BaseEntity
{
    public Guid BookId { get; set; }

    /// <summary>
    /// Ідентифікатор читача, який взяв книгу
    /// </summary>
    public Guid ReaderId { get; set; }

    /// <summary>
    /// Термін повернення книги, днів
    /// </summary>
    public int TermLendDays { get; set; }

    /// <summary>
    /// Дата, коли взяв книгу
    /// </summary>
    public DateOnly TakenDate { get; set; }

    /// <summary>
    /// Дата повернення книги
    /// </summary>
    public DateOnly? ReturnDate { get; set; } = null;

    public virtual Book Book { get; set; } = null!;

    public virtual Reader Reader { get; set; } = null!;
}
