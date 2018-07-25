using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bueller.Library.Models
{
    public class Grade
    {
        [ScaffoldColumn(false)]
        public int GradeId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Evaluation type cannot be more than {1} characters")]
        public string EvaluationType { get; set; }

        [Required]
        [Range(0, 200, ErrorMessage = "Score must be between 0 and 200")]
        public double Score { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(2, ErrorMessage = "Letter grade cannot be more than {1} characters")]
        public string LetterGrade { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "Comment cannot be more than {1} characters")]
        public string Comment { get; set; }

        //[Required]
        //[ScaffoldColumn(false)]
        //public int FileId { get; set; }
        public virtual File File { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
