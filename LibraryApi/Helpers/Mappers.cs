using LibraryApi.DTOs;
using LibraryApi.Models;

namespace LibraryApi
{
    public static class Mappers
    {
        public static BookDto ToDto(this Book book)
        {
            return new BookDto
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                IsAvailable = book.isAvailable,
                MemberId = book.MemberId
            };
        }

        public static MemberDto ToDto(this Member member)
        {
            return new MemberDto
            {
                MemberId = member.MemberId,
                MemberFirstName = member.MemberFirstName,
                MemberLastName = member.MemberLastName,
                Role = member.Role,
                ApiKey = member.ApiKey,
                BorrowedBookIds = member.BorrowedBooks?.Select(b => b.BookId).ToList()
            };
        }
    }
}
