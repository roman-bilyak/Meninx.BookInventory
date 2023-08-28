using System;

namespace Meninx.BookInventory
{
    public class Book : Entity<Guid>
    {
        public const int TitleMaxLength = 255;

        public const int AuthorMaxLength = 255;

        public const int ISBNMaxLength = 20;

        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public int PublicationYear { get; set; }

        public int Quantity { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}