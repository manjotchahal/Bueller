using System;
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

        public DateTime Created { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        public DateTime? Modified { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as File;

            if (other == null)
                return false;

            if (Name != other.Name)
                return false;

            return true;
        }

        public bool EqualsGraded(object obj)
        {
            var other = obj as File;

            if (other == null)
                return false;

            if (Name != other.Name || Score != other.Score || Comment != other.Comment)
                return false;

            return true;
        }
    }
}