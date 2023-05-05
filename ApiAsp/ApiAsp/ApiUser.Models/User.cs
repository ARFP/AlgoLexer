using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiUser.Models
{
    [Table("Users")]
    public class User : Model
    {
        // [Column("user_name")]
        [Required(ErrorMessage = "")]
        [MaxLength(16, ErrorMessage = "")]
        [StringLength(16)]
        public string? UserName { get; set; }

        public string? Password { get; set; }

    }
}