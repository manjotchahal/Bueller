using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bueller.Data.Models
{
    [Table("Teachers", Schema = "Persons")]
    public class Employee : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int TeacherID { get; set; }

        public int? OfficeNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(200, ErrorMessage = "Name must be shorter than {1} characters")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [StringLength(200, ErrorMessage = "Name must be shorter than {1} charcters")]
        public string MiddleName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(200, ErrorMessage = "Name must be shorter than {1} characters")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Title must be shorter than {1} characters")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(200, ErrorMessage = "Street address must be shorter than {1} characters")]
        public string Street { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(200, ErrorMessage = "City must be shorter than {1} characters")]
        public string City { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(200, ErrorMessage = "State must be shorter than {1} characters")]
        public string State { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Country must be shorter than {1} characters")]
        public string Country { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression("[0-9]{5}", ErrorMessage = "Zipcode must be 5 digits")]
        public int Zipcode { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(".{1,200}[@].{1,200}[.].{1,5}", ErrorMessage = "Email is too long, max 200 character on each side of @")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("[(]{1}[0-9]{3}[)]{1}[ ]{1}[0-9]{3}[-]{1}[0-9]{4}", ErrorMessage = "Format must be (###) ###-####")]
        public string PersonalPhoneNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("[(]{1}[0-9]{3}[)]{1}[ ]{1}[0-9]{3}[-]{1}[0-9]{4}", ErrorMessage = "Format must be (###) ###-####")]
        public string OfficePhoneNumber { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[StringLength(100, ErrorMessage = "Employee type must be shorter than {1} characters")]
        //public string EmployeeType { get; set; }

        //[Required]
        //[ScaffoldColumn(false)]
        //public int EmployeeAccountId { get; set; }
        //[ForeignKey("EmployeeAccountId")]
        //public virtual EmployeeAccount EmployeeAccount { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? Modified { get; set; }
    }
}
