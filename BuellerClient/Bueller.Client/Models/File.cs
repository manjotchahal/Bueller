﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bueller.Client.Models
{
    public class File
    {
        [ScaffoldColumn(false)]
        public int FileId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "File name cannot be more than {1} characters")]
        public string Name { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}