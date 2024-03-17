using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class User : IdentityUser
    {
        // Core Identity properties                
        public override string UserName { get; set; }
        public override string Email { get; set; }
        public override string PhoneNumber { get; set; }
        public override bool EmailConfirmed { get; set; }
        public override bool PhoneNumberConfirmed { get; set; }
        public override string PasswordHash { get; set; }
        public override bool LockoutEnabled { get; set; }
        public override DateTimeOffset? LockoutEnd { get; set; }
        public override int AccessFailedCount { get; set; }
        public override string ConcurrencyStamp { get; set; }
        public override string SecurityStamp { get; set; }

        // Custom properties
        
    }
}
