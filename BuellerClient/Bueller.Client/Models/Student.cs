using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bueller.Client.Models
{
    public class Student
    {
        [ScaffoldColumn(false)]
        public int StudentId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        [StringLength(100, ErrorMessage = "First name cannot be more than {1} characters")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Middle Name")]
        [StringLength(100, ErrorMessage = "Middle name cannot be more than {1} characters")]
        public string MiddleName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        [StringLength(100, ErrorMessage = "Last name cannot be more than {1} characters")]
        public string LastName { get; set; }

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
        [Display(Name = "Zip Code")]
        [RegularExpression("[0-9]{5}", ErrorMessage = "Zipcode must be 5 digits")]
        public string Zipcode { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(".{1,200}[@].{1,200}[.].{1,5}", ErrorMessage = "Email address cannot have more than 200 characters on each side of @")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [RegularExpression("[(]{1}[0-9]{3}[)]{1}[ ]{1}[0-9]{3}[-]{1}[0-9]{4}", ErrorMessage = "Format must be (###) ###-####")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Class Level")]
        [StringLength(100, ErrorMessage = "Class level cannot be more than 100 characters")]
        public string Grade { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        public int Credits
        {
            get
            {
                int a = 0;
                if (this.Classes != null)
                {
                    if (this.Classes.Any())
                    {
                        foreach (var classitem in Classes)
                        {
                            a += classitem.Credits;
                        }
                    }
                }

                return a;
            }
        }

        [Display(Name = "Enrollment Status")]
        public string StudentType
        {
            get
            {
                if (this.Credits <= 0)
                    return "Not Enrolled";
                if (this.Credits <= 16)
                    return "Part Time";
                return "Full Time";

            }
        }

        [Display(Name = "Grade")]
        public double AverageGrade { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Student;

            if (other == null)
                return false;

            if (FirstName != other.FirstName || MiddleName != other.MiddleName || LastName != other.LastName || PhoneNumber != other.PhoneNumber || Grade != other.Grade 
                || Street != other.Street || City != other.City || State != other.State || Country != other.Country || Zipcode != other.Zipcode)
                return false;

            return true;
        }
    }
}