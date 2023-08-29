using System;

namespace Meninx.BookInventory.App.Models
{
    public class BookDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public int PublicationYear { get; set; }

        public int Quantity { get; set; }

        public Guid CategoryId { get; set; }
    }
}