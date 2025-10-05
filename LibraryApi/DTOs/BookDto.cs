namespace LibraryApi.DTOs
{
    public class BookDto
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public Guid? MemberId { get; set; } // Only show ID, not full object
    }

    public class MemberDto
    {
        public Guid MemberId { get; set; }
        public string MemberFirstName { get; set; } = string.Empty;
        public string MemberLastName { get; set; } = string.Empty;
        public string Role { get; set; } = "Member";
        public string ApiKey { get; set; } = string.Empty;
        public List<Guid>? BorrowedBookIds { get; set; }
    }
}
