using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bueller.Data.Models
{
    [Table("Grades", Schema = "Assignments")]
    public class Grade : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int GradeId { get; set; }

        [Required(ErrorMessage = "Evaluation type is required")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Evaluation type cannot be more than 50 characters")]
        public string EvaluationType { get; set; }

        [Required(ErrorMessage = "Score is required")]
        [Range(0, 200, ErrorMessage = "Score must be between 0 and 200")]
        public double Score { get; set; }

        [Required(ErrorMessage = "Grade letter is required")]
        [DataType(DataType.Text)]
        [StringLength(2, ErrorMessage = "Grade letter cannot be more than 2 characters")]
        public string GradeLetter { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "Comment cannot be more than 500 characters")]
        public string Comment { get; set; }

        //[Required]
        //[ScaffoldColumn(false)]
        //public int StudentId { get; set; }
        //[ForeignKey("StudentId")]
        //public virtual Student Student { get; set; }

        //[Required]
        //[ScaffoldColumn(false)]
        //public int TeacherId { get; set; }
        //[ForeignKey("TeacherId")]
        //public virtual Employee Teacher { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int FileId { get; set; }
        [ForeignKey("FileId")]
        public virtual File File { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? Modified { get; set; }
    }
}
