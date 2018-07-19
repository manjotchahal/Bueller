using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Bueller.Data.Models;

namespace Bueller.Library.Models
{
    public class Teacher : BaseModel
    {
        [ScaffoldColumn(false)]
        public int TeacherID { get; set; }

        public int? OfficeNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "First name cannot be more than {1} characters")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Middle name cannot be more than {1} charcters")]
        public string MiddleName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Last name cannot be more than {1} characters")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Title cannot be more than {1} characters")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(200, ErrorMessage = "Street address cannot be more than {1} characters")]
        public string Street { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "City cannot be more than {1} characters")]
        public string City { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "State cannot be more than {1} characters")]
        public string State { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Country cannot be more than {1} characters")]
        public string Country { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression("[0-9]{5}", ErrorMessage = "Zipcode must be 5 digits")]
        public string Zipcode { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(".{1,200}[@].{1,200}[.].{1,5}", ErrorMessage = "Email address cannot have more than 200 characters on each side of @")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("[(]{1}[0-9]{3}[)]{1}[ ]{1}[0-9]{3}[-]{1}[0-9]{4}", ErrorMessage = "Format must be (###) ###-####")]
        public string PersonalPhoneNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("[(]{1}[0-9]{3}[)]{1}[ ]{1}[0-9]{3}[-]{1}[0-9]{4}", ErrorMessage = "Format must be (###) ###-####")]
        public string OfficePhoneNumber { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
