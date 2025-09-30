using System.ComponentModel.DataAnnotations;
namespace LibraryApi.Models
{
    public class Member
    {
        [Key]
        public Guid MemberId { get; set; } =Guid.NewGuid();
        public string MemberFirstName { get; set; } = string.Empty;
        public string MemberLastName {  get; set; } = string.Empty;

        public string Role {  get; set; } = "Member";
        public string ApiKey { get; set; } = Guid.NewGuid().ToString();

        public List<Book> BorrowedBooks { get; set; } = new List<Book>();


    }
}
