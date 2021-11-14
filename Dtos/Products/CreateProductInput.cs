using APITesting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APITesting.Dtos.Products
{
    public class CreateProductInput
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public Product MapToEntity(Product entity = null)
        {
            if (entity == null)
                entity = new Product();
            entity.Name = this.Name;
            entity.Description = this.Description;
            entity.Price = this.Price;
            entity.CategoryId = this.CategoryId;

            return entity;
        }
    }

    public class UpdateProductInput : CreateProductInput { }
}
