using System;
using System.ComponentModel.DataAnnotations;

namespace Meninx.BookInventory.App.Models
{
    public class BookCreateDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [RegularExpression("^.{1,255}$", ErrorMessage = "Title can have a maximum of 255 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        [RegularExpression("^.{1,255}$", ErrorMessage = "Author can have a maximum of 255 characters.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        [RegularExpression("^.{1,20}$", ErrorMessage = "Invalid ISBN format.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Publication Year is required.")]
        [RegularExpression("^\\d{1,4}$", ErrorMessage = "Publication Year must be a valid year.")]
        public string PublicationYear { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [RegularExpression("^\\d+$", ErrorMessage = "Quantity must be a non-negative integer.")]
        public string Quantity { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public Guid? CategoryId { get; set; }
    }
}