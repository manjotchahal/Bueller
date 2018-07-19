﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Bueller.Data.Models;

namespace Bueller.Library.Models
{
    public class Assignment : BaseModel
    {
        [ScaffoldColumn(false)]
        public int AssignmentId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Assignment name cannot be more than {1} characters")]
        public string Name { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
