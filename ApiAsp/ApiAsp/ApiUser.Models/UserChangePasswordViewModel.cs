using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUser.Models
{
    public class UserChangePasswordViewModel : User
    {
        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [RegularExpression(@"^[a-zA-Z0-9]{8,}$")]
        public string? NewPassword { get; set; }
    }
}
