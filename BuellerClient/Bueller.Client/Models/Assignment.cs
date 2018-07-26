using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Bueller.Client.Models
{
    public class Assignment
    {
        [ScaffoldColumn(false)]
        public int AssignmentId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Assignment name cannot be more than {1} characters")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Evaluation Type")]
        [StringLength(50, ErrorMessage = "Evaluation type cannot be more than {1} characters")]
        public string EvaluationType { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public IEnumerable<SelectListItem> AvailableClasses { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Assignment;

            if (other == null)
                return false;

            if (Name != other.Name || EvaluationType != other.EvaluationType || DueDate != other.DueDate)
                return false;

            return true;
        }
    }
}