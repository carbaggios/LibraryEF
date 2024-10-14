using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Entity;

public class LibraryContext : DbContext
{
    public LibraryContext()
    {
        //Database.EnsureCreated();
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }

    public DbSet<DocType> DocTypes { get; set; }

    public DbSet<Librarian> Librarians { get; set; }

    public DbSet<PublishingType> PublishingTypes { get; set; }

    public DbSet<Reader> Readers { get; set; }

    public DbSet<LendBook> LendBooks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Encrypt=False");
        //optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Author");

            entity.ToTable("Author");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.BirthDay);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Book");

            entity.ToTable("Book");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PublishingCity).HasMaxLength(255);
            entity.Property(e => e.PublishingCode).HasMaxLength(24);
            entity.Property(e => e.PublishingCountry).HasMaxLength(255);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_BookAuthor");

            entity.HasOne(d => d.PublishingType).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublishingTypeId)
                .HasConstraintName("fk_PublishingTypeId");
        });

        modelBuilder.Entity<DocType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_DocType");

            entity.ToTable("DocType");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Librarian>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Librarian");
            entity.HasIndex(e => e.Login).IsUnique();

            entity.ToTable("Librarian");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Email).HasMaxLength(320);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.PasswordHash);
            entity.Property(e => e.PasswordSalt);
        });

        modelBuilder.Entity<PublishingType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_PublishingType");

            entity.ToTable("PublishingType");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Reader");

            entity.ToTable("Reader");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DocNumber).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(320);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.PasswordHash);
            entity.Property(e => e.PasswordSalt);

            entity.HasOne(d => d.DocType).WithMany(p => p.Readers)
                .HasForeignKey(d => d.DocTypeId)
                .HasConstraintName("fk_ReaderDocType");
        });

        modelBuilder.Entity<LendBook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_LendBook");

            entity.ToTable("LendBook");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.TermLendDays);
            entity.Property(e => e.TakenDate);
            entity.Property(e => e.ReturnDate);

            entity.HasOne(d => d.Book).WithMany(p => p.LendBooks)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_LendBookBook");

            entity.HasOne(d => d.Reader).WithMany(p => p.LendBooks)
                .HasForeignKey(d => d.ReaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_LendBookReader");
        });

        //OnModelCreatingPartial(modelBuilder);
    }

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
