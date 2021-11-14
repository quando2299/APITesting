using APITesting.Dtos.Categories;
using APITesting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITesting.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly APITestingDemoContext _context;

        public CategoryController(APITestingDemoContext context)
        {
            this._context = context;
        }

        [HttpGet()]
        public IQueryable<CategoryDto> Get(string name = null)
        {
            var query = this._context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            var selectQuery = query.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name
            });

            return selectQuery;
        }

        [HttpGet("{id}")]
        public CategoryDto Get(int? id)
        {
            var query = this._context.Categories.Find(id);

            return new CategoryDto { Id = query.Id, Name = query.Name };
        }

        [HttpPost()]
        public int Create(CreateCategoryInput model)
        {
            var entity = model.MapToEntity();

            this._context.Categories.Add(entity);
            this._context.SaveChanges();

            return entity.Id;
        }

        [HttpPut()]
        public bool Update(int id, UpdateCategoryInput model)
        {
            if (model == null || id <= 0)
                return false;

            var entity = this._context.Categories.Find(id);

            entity = model.MapToEntity(entity);

            this._context.Update(entity);
            this._context.SaveChanges();

            return true;
        }

        [HttpDelete()]
        public bool Delete(int id)
        {
            if (id <= 0)
                return false;

            var entity = this._context.Categories.Find(id);
            this._context.Categories.Remove(entity);
            this._context.SaveChanges();

            return true;
        }
    }
}
