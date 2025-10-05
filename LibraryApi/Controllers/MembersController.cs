using LibraryApi;
using LibraryApi.DTOs;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public MembersController(ApplicationDbContext db) => _db = db;

    // GET: api/Members
    [HttpGet]
    [AuthorizeRole("Admin")]
    public async Task<IActionResult> GetAllMembers()
    {
        var members = await _db.Members
       .Include(m => m.BorrowedBooks)
       .AsNoTracking()
       .ToListAsync();

        return Ok(members.Select(m => m.ToDto()));
    }

    // GET: api/Members/{id}
    [HttpGet("{id:guid}")]
    [AuthorizeRole("Admin")]
    public async Task<IActionResult> GetMember(Guid id)
    {
        var member = await _db.Members
         .Include(m => m.BorrowedBooks)
         .AsNoTracking()
         .FirstOrDefaultAsync(m => m.MemberId == id);

        if (member == null) return NotFound();

        return Ok(member.ToDto());
    }

    // POST: api/Members/AddMember
    [HttpPost("AddMember")]
    [AuthorizeRole("Admin")]
    public async Task<IActionResult> AddMember([FromBody] MemberDto memberDto)
    {
        // Map DTO → entity
        var member = new Member
        {
            MemberId = Guid.NewGuid(),
            MemberFirstName = memberDto.MemberFirstName,
            MemberLastName = memberDto.MemberLastName,
            Role = memberDto.Role,
            ApiKey = Guid.NewGuid().ToString()
        };

        _db.Members.Add(member);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMember), new { id = member.MemberId }, member.ToDto());
    }


    // DELETE: api/Members/DeleteMember/{id}
    [HttpDelete("DeleteMember/{id:guid}")]
    [AuthorizeRole("Admin")]
    public async Task<IActionResult> DeleteMember(Guid id)
    {
        var member = await _db.Members
            .Include(m => m.BorrowedBooks) // Include borrowed books
            .FirstOrDefaultAsync(m => m.MemberId == id);

        if (member == null) return NotFound();

        // Make all borrowed books available
        if (member.BorrowedBooks != null)
        {
            foreach (var book in member.BorrowedBooks)
            {
                book.isAvailable = true;
                book.MemberId = null;
            }
        }

        _db.Members.Remove(member);
        await _db.SaveChangesAsync();

        return Ok($"Member: {member.MemberFirstName} {member.MemberLastName} has been removed successfully");
    }

    // GET: api/Members/Me
    [HttpGet("Me/Details")]
    [AuthorizeRole("Member")]
    public async Task<IActionResult> GetMyDetails()
    {
        var member = (Member)HttpContext.Items["Member"];

        var dbMember = await _db.Members
            .Include(m => m.BorrowedBooks)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MemberId == member.MemberId);

        if (dbMember == null) return NotFound();

        return Ok(dbMember.ToDto());
    }

    // GET: api/Members/Me/BorrowedBooks
    [HttpGet("Me/BorrowedBooks")]
    [AuthorizeRole("Member")]
    public async Task<IActionResult> GetMyBorrowedBooks()
    {
        var member = (Member)HttpContext.Items["Member"];

        var books = await _db.Books
            .Where(b => b.MemberId == member.MemberId)
            .Include(b => b.Member)
            .AsNoTracking()
            .ToListAsync();

        var dtoList = books.Select(b => b.ToDto()).ToList();
        if(dtoList.Count == 0)
        {
            return Ok("You have not borrowed any books");
        }
        return Ok(dtoList);
    }
}
