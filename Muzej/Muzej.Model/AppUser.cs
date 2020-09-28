using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Muzej.Model
{
    public class AppUser : IdentityUser
    {
        [Required]
        [RegularExpression("[0-9]{10}")]
        public string OIB { get; set; }
    }
}
