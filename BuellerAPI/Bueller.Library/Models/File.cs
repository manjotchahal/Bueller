using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bueller.Library.Models
{
    public class File
    {
        [ScaffoldColumn(false)]
        public int FileId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "File name cannot be more than {1} characters")]
        public string Name { get; set; }

        [Range(0, 200, ErrorMessage = "Score must be between 0 and 200")]
        public double? Score { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "Comment cannot be more than {1} characters")]
        public string Comment { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        //public virtual Grade Grade { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
