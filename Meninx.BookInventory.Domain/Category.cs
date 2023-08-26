using System;

namespace Meninx.BookInventory
{
    public class Category : Entity<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}