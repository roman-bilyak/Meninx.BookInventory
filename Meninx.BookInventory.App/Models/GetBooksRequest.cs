using System;
using System.ComponentModel.DataAnnotations;

namespace Meninx.BookInventory.App.Models
{
    public class GetBooksRequest
    {
        [MaxLength(100)]
        public string Query { get; set; }

        [Range(0, 100)] 
        public int? Limit { get; set; }

        [Range(0, Int32.MaxValue)]
        public int? Offset { get; set; }

        [MaxLength(50)]
        public string SortBy { get; set; }

        [MaxLength(4)]
        public string SortOrder { get; set; }
    }
}