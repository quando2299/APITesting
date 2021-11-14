using APITesting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APITesting.Dtos.Categories
{
    public class CreateCategoryInput
    {
        [Required]
        public string Name { get; set; }

        public Category MapToEntity(Category entity = null)
        {
            if (entity == null)
                entity = new Category();
            entity.Name = this.Name;

            return entity;
        }
    }

    public class UpdateCategoryInput : CreateCategoryInput { }
}
