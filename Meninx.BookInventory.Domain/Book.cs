using System;

namespace Meninx.BookInventory
{
    public class Book : Entity<Guid>
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public int PublicationYear { get; set; }

        public int Quantity { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}