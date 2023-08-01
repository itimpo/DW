using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocuWare.Infrastructure.Users;

[Table("Users")]
internal class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;
    [StringLength(150)]
    public string PasswordHash { get; set; } = string.Empty;
}
