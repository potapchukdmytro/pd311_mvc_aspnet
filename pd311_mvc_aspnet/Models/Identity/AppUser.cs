using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace pd311_mvc_aspnet.Models.Identity
{
    public class AppUser : IdentityUser
    {
        [MaxLength(255)]
        public string? FirstName { get; set; }
        [MaxLength(255)]
        public string? LastName { get; set; }
        [Range(0, 200)]
        public int Age { get; set; }
    }
}
