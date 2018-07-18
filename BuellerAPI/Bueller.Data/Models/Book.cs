using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bueller.Data.Models
{
    [Table("Books", Schema = "Classes")]
    public class Book : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int BookId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Book name cannot be more than 100 characters")]
        public string BookTitle { get; set; }

        //[DataType(DataType.Upload)]     //not sure about this annotation
        //public string FileLocation { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "Description cannot be more than 500 characters")]
        public string BookDescription { get; set; }

        //[Required]
        //[ScaffoldColumn(false)]
        //public int GradeId { get; set; }
        //[ForeignKey("GradeId")]
        //public virtual Grade Grade { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? Modified { get; set; }
    }
}
