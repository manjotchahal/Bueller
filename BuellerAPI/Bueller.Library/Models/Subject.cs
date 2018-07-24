using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bueller.Library.Models
{
    public class Subject
    {
        [ScaffoldColumn(false)]
        public int SubjectId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Name cannot be more than {1} characters")]
        public string Name { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[StringLength(100, ErrorMessage = "Department cannot be more than {1} characters")]
        //public string Department { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
