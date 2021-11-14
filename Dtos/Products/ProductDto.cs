using APITesting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITesting.Dtos.Products
{
    public class ProductDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class DetailProductDto : ProductDto
    {
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
