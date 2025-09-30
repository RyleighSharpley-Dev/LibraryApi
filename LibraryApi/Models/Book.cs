using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LibraryApi.Models
{
    public class Book
    {
        [Key]
        public Guid BookId { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public bool isAvailable { get; set; } = true;

        [ForeignKey(nameof(Member))]
        public Guid? MemberId { get; set; }
        public virtual Member? Member { get; set; }

    }
}
