using APITesting.Dtos.Products;
using APITesting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITesting.Controllers
{
    public class ProductController : BaseController
    {
        private readonly APITestingDemoContext _context;

        public ProductController(APITestingDemoContext context)
        {
            this._context = context;
        }

        [HttpGet()]
        public IQueryable<ProductDto> Get(string name = null, int categoryId = 0)
        {
            var query = this._context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.Name.Contains(name));

            if (categoryId > 0)
                query = query.Where(x => x.CategoryId == categoryId);

            var selectQuery = query.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                CategoryName = x.Category.Name,
                CategoryId = x.CategoryId
            });

            return selectQuery;
        }

        [HttpGet("{id}")]
        public DetailProductDto Get(Guid? id)
        {
            var model = this._context.Products.Where(m => m.Id == id.Value).Select(x => new DetailProductDto
            {
                Id = x.Id,
                Name = x.Name,
                CategoryName = x.Category.Name,
                CategoryId = x.CategoryId,
                Description = x.Description,
                Price = x.Price
            });


            return model.First();
        }

        [HttpPost()]
        public Guid Create(CreateProductInput model)
        {
            if (!this.GetQueryCategory(model.CategoryId))
                throw new Exception("Danh mục không tồn tại");

            var entity = model.MapToEntity();
            this._context.Products.Add(entity);
            this._context.SaveChanges();

            return entity.Id;
        }

        [HttpPut()]
        public bool Update(Guid id, UpdateProductInput model)
        {
            if (!this.GetQueryCategory(model.CategoryId))
                throw new Exception("Danh mục không tồn tại");


            if (model == null)
                return false;

            var entity = this._context.Products.Find(id);
            entity = model.MapToEntity(entity);

            this._context.Products.Update(entity);
            this._context.SaveChanges();

            return true;
        }

        [HttpDelete()]
        public bool Delete(Guid? id)
        {
            if (!id.HasValue)
                return false;

            var entity = this._context.Products.Find(id);
            this._context.Products.Remove(entity);
            this._context.SaveChanges();

            return true;
        }

        private bool GetQueryCategory(int id)
        {
            var query = this._context.Categories.Find(id);
            if (query == null)
            {
                return false;
            }

            return true;
        }
    }
}
