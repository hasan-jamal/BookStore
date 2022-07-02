using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product> , IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var productId = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (productId != null)
            {
                productId.Id = obj.Id;  
                productId.Title = obj.Title;
                productId.Description = obj.Description;
                productId.ISBN = obj.ISBN;
                productId.Author = obj.Author;
                productId.ListPrice = obj.ListPrice;
                productId.Price = obj.Price;
                productId.Price50 = obj.Price50;
                productId.Price100  = obj.Price100;
                productId.Category = obj.Category;
                productId.CoverType = obj.CoverType;
                if (obj.ImageUrl != null)
                {
                    productId.ImageUrl = obj.ImageUrl;
                }
            }
            
        }
    }
}
