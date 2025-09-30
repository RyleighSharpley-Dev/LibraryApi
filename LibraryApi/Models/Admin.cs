using System.ComponentModel.DataAnnotations;

public class Admin
{
    [Key]
    public Guid AdminId { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string ApiKey { get; set; } = Guid.NewGuid().ToString(); 
}
