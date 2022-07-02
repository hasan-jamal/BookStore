using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            coverType = new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);
            company = new CompanyRepository(_db);
            applicationUser = new ApplicationUserRepository(_db);
            shoppingCart = new ShoppingRepository(_db);
            orderHeader = new OrderHeaderRepository(_db);
            orderDetail = new OrderDetailRepository(_db);




        }
        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository coverType { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository company { get; private set; }
        public IApplicationUserRepository applicationUser { get; private set; }
        public IShoppingCartRepository shoppingCart { get; private set; }
        public IOrderHeaderRepository orderHeader { get; private set; }
        public IOrderDetailRepository orderDetail { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
