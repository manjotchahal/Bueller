using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bueller.Client.Models
{
    public class Class
    {
        [ScaffoldColumn(false)]
        public int ClassId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Name cannot be more than {1} characters")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Room")]
        [StringLength(20, ErrorMessage = "Room number cannot be more than {1} characters")]
        public string RoomNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(10, ErrorMessage = "Section cannot be more than {1} characters")]
        public string Section { get; set; }

        [Required]
        [Range(1, 6, ErrorMessage = "Number of credits must be between 1 and 6")]
        public int Credits { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "Description cannot be more than {1} characters")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:h\\:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:h\\:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }

        //different ways to do this.. try 5 columns for now
        [Required]
        [Range(0, 1, ErrorMessage = "Enter 1 if class held on corresponding day, 0 otherwise")]
        public int Mon { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Enter 1 if class held on corresponding day, 0 otherwise")]
        public int Tues { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Enter 1 if class held on corresponding day, 0 otherwise")]
        public int Wed { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Enter 1 if class held on corresponding day, 0 otherwise")]
        public int Thurs { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Enter 1 if class held on corresponding day, 0 otherwise")]
        public int Fri { get; set; }

        [ScaffoldColumn(false)]
        public int? TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public string SubjectName { get; set; }

        [Display(Name = "Enrollment Count")]
        public int EnrollmentCount { get; set; }

        //[NotMapped]
        [Display(Name = "Class Days")]
        public string ClassDays
        {
            get
            {
                return ((Mon == 1) ? ("Mo") : "") + ((Tues == 1) ? ("Tu") : "") +
                       ((Wed == 1) ? ("We") : "") + ((Thurs == 1) ? ("Th") : "") +
                       ((Fri == 1) ? ("Fr") : "");
            }
        }
        [DataType(DataType.Text)]
        public string StartTimeFormatted
        {
            get
            {
                if (TimeSpan.Compare(StartTime, TimeSpan.FromHours(13)) >= 0)
                    return StartTime.Subtract(TimeSpan.FromHours(12)).ToString(@"h\:mm") + "PM";
                if (TimeSpan.Compare(StartTime, TimeSpan.FromHours(1)) < 0)
                    return StartTime.Add(TimeSpan.FromHours(12)).ToString(@"h\:mm") + "AM";
                if (TimeSpan.Compare(StartTime, TimeSpan.FromHours(12)) >= 0)
                    return StartTime.ToString(@"h\:mm") + "PM";
                return StartTime.ToString(@"h\:mm") + "AM";
                //return StartTime.ToString("{0:h\\:mm}") + "AM";
            }
        }
        [DataType(DataType.Text)]
        public string EndTimeFormatted
        {
            get
            {
                if (TimeSpan.Compare(EndTime, TimeSpan.FromHours(13)) >= 0)
                    return EndTime.Subtract(TimeSpan.FromHours(12)).ToString(@"h\:mm") + "PM";
                if (TimeSpan.Compare(EndTime, TimeSpan.FromHours(1)) < 0)
                    return EndTime.Add(TimeSpan.FromHours(12)).ToString(@"h\:mm") + "AM";
                if (TimeSpan.Compare(EndTime, TimeSpan.FromHours(12)) >= 0)
                    return EndTime.ToString(@"h\:mm") + "PM";
                return EndTime.ToString(@"h\:mm") + "AM";
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as Class;

            if (other == null)
                return false;

            if (Name != other.Name || RoomNumber != other.RoomNumber || Section != other.Section || Credits != other.Credits || Description != other.Description || SubjectName != other.SubjectName
                || StartTime != other.StartTime || EndTime != other.EndTime || Mon != other.Mon || Tues != other.Tues || Wed != other.Wed || Thurs != other.Thurs || Fri != other.Fri)
                return false;

            return true;
        }
    }
}