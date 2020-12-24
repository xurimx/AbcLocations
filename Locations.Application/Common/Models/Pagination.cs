using System;
using System.Collections.Generic;
using System.Text;

namespace Locations.Application.Common.Models
{
    public class Pagination<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int FilteredItems { get; set; }
    }
}
