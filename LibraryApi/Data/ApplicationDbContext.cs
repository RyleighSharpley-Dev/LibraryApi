using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Member> Members { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>()
            .HasMany(m => m.BorrowedBooks)
            .WithOne(b => b.Member)
            .HasForeignKey(b => b.MemberId)
            .OnDelete(DeleteBehavior.SetNull);

        // Seed Admin (fixed GUID & API key)
        modelBuilder.Entity<Admin>().HasData(new Admin
        {
            AdminId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            FirstName = "Super",
            LastName = "Admin",
            ApiKey = "admin-api-key-1234567890"
        });

        // Seed a test Member
        modelBuilder.Entity<Member>().HasData(new Member
        {
            MemberId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            MemberFirstName = "John",
            MemberLastName = "Doe",
            Role = "Member",
            ApiKey = "member-api-key-1234567890"
        });

        // Seed 5 Books
        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                BookId = Guid.Parse("aaa11111-aaaa-1111-aaaa-111111aaaa11"),
                Title = "Clean Code",
                Author = "Robert C. Martin",
                isAvailable = true
            },
            new Book
            {
                BookId = Guid.Parse("aaa11111-aaaa-1111-aaaa-111111aaaa12"),
                Title = "The Pragmatic Programmer",
                Author = "Andrew Hunt & David Thomas",
                isAvailable = true
            },
            new Book
            {
                BookId = Guid.Parse("aaa11111-aaaa-1111-aaaa-111111aaaa13"),
                Title = "Design Patterns",
                Author = "Erich Gamma et al.",
                isAvailable = true
            },
            new Book
            {
                BookId = Guid.Parse("aaa11111-aaaa-1111-aaaa-111111aaaa14"),
                Title = "Refactoring",
                Author = "Martin Fowler",
                isAvailable = true
            },
            new Book
            {
                BookId = Guid.Parse("aaa11111-aaaa-1111-aaaa-111111aaaa15"),
                Title = "Domain-Driven Design",
                Author = "Eric Evans",
                isAvailable = true
            }
        );



        base.OnModelCreating(modelBuilder);
    }
}