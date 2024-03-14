using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string UserEmail { get; set; }
        [Required]
        [StringLength(500)]
        public string UserPassword { get; set; }

    }
}
