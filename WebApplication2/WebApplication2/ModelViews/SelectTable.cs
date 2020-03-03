using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.ModelViews
{
    public class SelectTable
    {
        public IEnumerable<Winery> wineryTable { get; set; }
        public IEnumerable<Category> categoryTable { get; set; }
        public IEnumerable<Product> productTable { get; set; }
        public IEnumerable<Milliliter> milliliterTabel { get; set; }
        public IEnumerable<Shelf> shelfTable { get; set; }
    }
}