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
    }
}