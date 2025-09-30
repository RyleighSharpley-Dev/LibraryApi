using LibraryApi;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public BooksController(ApplicationDbContext db) => _db = db;

    // GET: api/Books
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _db.Books.ToListAsync();
        return Ok(books.Select(b => b.ToDto()));
    }

    // GET: api/Books/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBook(Guid id)
    {
        var book = await _db.Books.FindAsync(id);
        return book == null ? NotFound() : Ok(book.ToDto());

    }

    // POST: api/Books/AddBook
    [HttpPost("AddBook")]
    [AuthorizeRole("Admin")]
    public async Task<IActionResult> AddBook([FromBody] Book book)
    {
        _db.Books.Add(book);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
    }

    // DELETE: api/Books/DeleteBook/{id}
    [HttpDelete("DeleteBook/{id:guid}")]
    [AuthorizeRole("Admin")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        var book = await _db.Books.FindAsync(id);
        if (book == null) return NotFound();
        _db.Books.Remove(book);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // POST: api/Books/Borrow/{id}
    [HttpPost("Borrow/{id:guid}")]
    [AuthorizeRole("Member")]
    public async Task<IActionResult> BorrowBook(Guid id)
    {
        var book = await _db.Books.FindAsync(id);
        if (book == null || !book.isAvailable)
            return BadRequest("Book not available");

        var member = (Member)HttpContext.Items["Member"];
        book.MemberId = member.MemberId;
        book.isAvailable = false;

        await _db.SaveChangesAsync();
        return Ok(book.ToDto());
    }

    // POST: api/Books/Return/{id}
    [HttpPost("Return/{id:guid}")]
    [AuthorizeRole("Member")]
    public async Task<IActionResult> ReturnBook(Guid id)
    {
        var book = await _db.Books.FindAsync(id);
        var member = (Member)HttpContext.Items["Member"];

        if (book == null || book.MemberId != member.MemberId)
            return BadRequest("Cannot return this book");

        book.MemberId = null;
        book.isAvailable = true;

        await _db.SaveChangesAsync();

        return Ok(book.ToDto());
    }
}
