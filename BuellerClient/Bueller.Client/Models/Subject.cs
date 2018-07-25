using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bueller.Client.Models
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

        public override bool Equals(object obj)
        {
            var other = obj as Subject;

            if (other == null)
                return false;

            if (Name != other.Name)
                return false;

            return true;
        }
    }
}