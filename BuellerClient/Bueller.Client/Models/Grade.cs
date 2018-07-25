using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bueller.Client.Models
{
    public class Grade
    {
        [ScaffoldColumn(false)]
        public int GradeId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Evaluation Type")]
        [StringLength(50, ErrorMessage = "Evaluation type cannot be more than {1} characters")]
        public string EvaluationType { get; set; }

        [Required]
        [Range(0, 200, ErrorMessage = "Score must be between 0 and 200")]
        public double Score { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Letter Grade")]
        [StringLength(2, ErrorMessage = "Letter grade cannot be more than {1} characters")]
        public string LetterGrade { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "Comment cannot be more than {1} characters")]
        public string Comment { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int FileId { get; set; }
        public virtual File File { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Grade;

            if (other == null)
                return false;

            if (EvaluationType != other.EvaluationType || Score != other.Score || LetterGrade != other.LetterGrade || Comment != other.Comment)
                return false;

            return true;
        }
    }
}