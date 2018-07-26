using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bueller.Client.Models
{
    public class Book
    {
        [ScaffoldColumn(false)]
        public int BookId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Title cannot be more than {1} characters")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "Description cannot be more than {1} characters")]
        public string Description { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        public bool NotModified(object obj)
        {
            var other = obj as Book;

            if (other == null)
                return false;

            if (Title != other.Title || Price != other.Price || Description != other.Description)
                return false;

            return true;
        }
    }
}