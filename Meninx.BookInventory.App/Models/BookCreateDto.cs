using System;
using System.ComponentModel.DataAnnotations;

namespace Meninx.BookInventory.App.Models
{
    public class BookCreateDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}