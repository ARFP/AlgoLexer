using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUser.Models
{
    public class UserReadViewModel : Model 
    {
        [Required(ErrorMessage = "Le nom d'utilisateur est requis")]
        [StringLength(maximumLength: 16, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-Z]+(?:\-[a-zA-Z]+)?$")]
        public string? UserName { get; set; }
    }
}
