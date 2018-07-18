using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bueller.Data.Models
{
    [Table("Classes", Schema = "Classes")]
    public class Class : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int ClassId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Name cannot be more than 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Room number is required")]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "Room number cannot be more than 100 characters")]
        public string RoomNumber { get; set; }
        [Required(ErrorMessage = "Section is required")]
        [DataType(DataType.Text)]
        [StringLength(10, ErrorMessage = "Section cannot be more than 100 characters")]
        public string Section { get; set; }
        [Required(ErrorMessage = "Credits is required")]
        [Range(1, 6, ErrorMessage = "Number of credits must be between 1 and 6")]
        public int Credits { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "Description cannot be more than 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        [Column(TypeName = "time")]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        [Required(ErrorMessage = "End time is required")]
        [Column(TypeName = "time")]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        //different ways to do this.. try 5 columns for now
        [Required]
        [Range(0, 1, ErrorMessage = "Enter 1 if class held on corresponsing day, 0 otherwise")]
        public int Mon { get; set; }
        [Required]
        [Range(0, 1, ErrorMessage = "Enter 1 if class held on corresponsing day, 0 otherwise")]
        public int Tues { get; set; }
        [Required]
        [Range(0, 1, ErrorMessage = "Enter 1 if class held on corresponsing day, 0 otherwise")]
        public int Wed { get; set; }
        [Required]
        [Range(0, 1, ErrorMessage = "Enter 1 if class held on corresponsing day, 0 otherwise")]
        public int Thurs { get; set; }
        [Required]
        [Range(0, 1, ErrorMessage = "Enter 1 if class held on corresponsing day, 0 otherwise")]
        public int Fri { get; set; }

        //no class level for now

        [ScaffoldColumn(false)]
        public int? TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? Modified { get; set; }
    }
}
