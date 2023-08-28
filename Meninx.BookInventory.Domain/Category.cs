using System;

namespace Meninx.BookInventory
{
    public class Category : Entity<Guid>
    {
        public const int NameMaxLength = 100;

        public const int DescriptionMaxLength = 255;

        public string Name { get; set; }

        public string Description { get; set; }
    }
}