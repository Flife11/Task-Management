using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Task Title")]
        public string TaskTitle { get; set; }

        [DefaultValue("false")]
        [DisplayName("Task Status")]
        public bool TaskStatus { get; set; }

        [Required]
        [MaxLength(500)]
        [DisplayName("Task Description")]
        public string TaskDescription { get; set; }

        [Required]
        [DisplayName("Task Due")]
        public DateTime TaskDueDate { get; set; }

    }
}
